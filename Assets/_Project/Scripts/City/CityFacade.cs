using System;
using Zenject;
using ModestTree;
using UnityEngine;

using _Project.Scripts.City.Systems;
using _Project.Scripts.City.ConfigWrappers;
using _Project.Scripts.City.Systems.Builders.Grid;
using _Project.Scripts.City.Systems.TextureChanger;
using _Project.Scripts.City.Systems.Builders.Terrain;
using _Project.Scripts.City.Systems.BuildingSelectors;

namespace _Project.Scripts.City
{
    public class CityFacade : MonoBehaviour
    {
        private event Action BuildStoppedEvent;
        private event Action<int> BuildCompletedEvent;

        [Header("Grid Size")]
        [SerializeField]
        private int _minGridSize = 100;

        [SerializeField]
        private int _maxGridSize = 1000;

        [Header("Preview Offset")]
        [SerializeField]
        private Vector3 _previewOffset;

        [Header("Systems")]
        [SerializeField]
        private CityMaterialChanger _cityMaterialChanger;

        [SerializeField]
        private PositionConverter _positionConverter;

        private CityConfig _cityConfig;
        private CityGridBuilder _gridBuilder;
        private CityTerrainBuilder _terrainBuilder;
        private IBuildingSelector _buildingSelector;
        private TerrainGridHolder _terrainGridHolder;

        private int _newBuildingIndex;

        [Inject]
        private void Construct(
            CityConfig cityConfig,
            CityGridBuilder gridBuilder,
            CityTerrainBuilder cityTerrainBuilder,
            IBuildingSelector buildingSelector,
            TerrainGridHolder terrainGridHolder)
        {
            _cityConfig = cityConfig;
            _gridBuilder = gridBuilder;
            _terrainBuilder = cityTerrainBuilder;
            _buildingSelector = buildingSelector;
            _terrainGridHolder = terrainGridHolder;
        }

        private void Awake()
        {
            CheckCityConfig();
            InitSystems();
        }

        private void Update()
        {
            if (_terrainBuilder.BuildingPreview != null)
            {
                var mousePosition = Input.mousePosition + _previewOffset;
                _terrainBuilder.BuildingPreview.position = Camera.main.ScreenToWorldPoint(mousePosition);
            }
        }

        private void CheckCityConfig()
        {
            if (_cityConfig.Width < _minGridSize ||
                _cityConfig.Width > _maxGridSize ||
                _cityConfig.Length < _minGridSize ||
                _cityConfig.Length > _maxGridSize)
            {
                throw new ApplicationException("game_config.json is not valid.");
            }
        }

        private void InitSystems()
        {
            _terrainBuilder.InitCityBuilder();

            _terrainGridHolder.InitHolder();
            _terrainGridHolder.HideTerrainGrid();
        }

        private void PlaceBuilding(Vector3Int position, int buildingIndex)
        {
            _gridBuilder.PlaceBuilding(position, buildingIndex);

            var fixedPosition = _positionConverter.AddPositionOffset(position);
            _terrainBuilder.PlaceBuilding(fixedPosition);
        }

        private void InvokeBuildingCompleted(int buildingIndex)
        {
            OnBuildStopped();

            var might = _cityConfig.Buildings[buildingIndex].Might;

            BuildCompletedEvent?.Invoke(might);
            BuildStoppedEvent?.Invoke();
        }

        public void OnBuildStarted()
        {
            _terrainGridHolder.UpdateTexture();
            _terrainGridHolder.ShowTerrainGrid();

            _cityMaterialChanger.MakeBuildingsTransparent();

            _newBuildingIndex = _buildingSelector.GetBuildingIndex();
            _terrainBuilder.CreateBuilding(_newBuildingIndex);
        }

        public void OnPlaceBuilding(Vector3Int position)
        {
            var downgradedPosition = _positionConverter.DowngradeToZeroMinimum(position);

            if (_gridBuilder.IsPositionFree(downgradedPosition, _newBuildingIndex))
            {
                PlaceBuilding(downgradedPosition, _newBuildingIndex);
                InvokeBuildingCompleted(_newBuildingIndex);
            }
        }

        public void OnBuildStopped()
        {
            _terrainGridHolder.HideTerrainGrid();
            _terrainBuilder.RemovePreviewBuilding();
            _cityMaterialChanger.MakeBuildingsVisible();
        }

        #region SUBSCRIBE_METHODS

        public void SubscribeBuildStopped(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            BuildStoppedEvent += action;
        }

        public void UnsubscribeBuildStopped(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            BuildStoppedEvent -= action;
        }

        public void SubscribeBuildCompleted(Action<int> action)
        {
            Assert.IsNotNull(action, "Action is null");
            BuildCompletedEvent += action;
        }

        public void UnsubscribeBuildCompleted(Action<int> action)
        {
            Assert.IsNotNull(action, "Action is null");
            BuildCompletedEvent -= action;
        }

        #endregion
    }
}