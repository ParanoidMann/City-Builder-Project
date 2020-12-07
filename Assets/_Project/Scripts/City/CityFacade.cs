using System;
using Zenject;
using ModestTree;
using UnityEngine;

using _Project.Scripts.City.Systems;
using _Project.Scripts.City.ConfigWrappers;
using _Project.Scripts.City.Systems.Builders.Grid;
using _Project.Scripts.City.Systems.Builders.Terrain;
using _Project.Scripts.City.Systems.BuildingSelectors;

namespace _Project.Scripts.City
{
    public class CityFacade : MonoBehaviour
    {
        private event Action BuildStoppedEvent;
        private event Action<int> BuildCompletedEvent;

        [SerializeField]
        private CityMaterialChanger _cityMaterialChanger;

        private CityConfig _cityConfig;
        private CityGridBuilder _gridBuilder;
        private CityTerrainBuilder _terrainBuilder;
        private IBuildingSelector _buildingSelector;

        private int _newBuildingIndex;

        [Inject]
        private void Construct(
            CityConfig cityConfig,
            CityGridBuilder gridBuilder,
            CityTerrainBuilder cityTerrainBuilder,
            IBuildingSelector buildingSelector)
        {
            _cityConfig = cityConfig;
            _gridBuilder = gridBuilder;
            _terrainBuilder = cityTerrainBuilder;
            _buildingSelector = buildingSelector;
        }

        private void Awake()
        {
            InitBuilders();
        }

        private void InitBuilders()
        {
            _gridBuilder.InitCityBuilder();
            _terrainBuilder.InitCityBuilder();
        }

        private void PlaceBuilding(Vector3Int position, int buildingIndex)
        {
            _gridBuilder.PlaceBuilding(position, buildingIndex);
            _terrainBuilder.PlaceBuilding(position, buildingIndex);
        }

        private void InvokeBuildingCompleted(int buildingIndex)
        {
            var might = _cityConfig.Buildings[buildingIndex].Might;

            BuildCompletedEvent?.Invoke(might);
            BuildStoppedEvent?.Invoke();
        }

        public void OnBuildingStarted()
        {
            _cityMaterialChanger.MakeBuildingsTransparent();
            _newBuildingIndex = _buildingSelector.GetBuildingIndex();
        }

        public void OnPlaceBuilding(Vector3Int position)
        {
            if (_gridBuilder.IsPositionFree(position, _newBuildingIndex))
            {
                PlaceBuilding(position, _newBuildingIndex);

                _cityMaterialChanger.MakeBuildingsVisible();
                InvokeBuildingCompleted(_newBuildingIndex);
            }
        }

        #region SUBSCRIBE_METHODS

        public void SubscribeBuildingStopped(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            BuildStoppedEvent += action;
        }

        public void UnsubscribeBuildingStopped(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            BuildStoppedEvent -= action;
        }

        public void SubscribeBuildingCompleted(Action<int> action)
        {
            Assert.IsNotNull(action, "Action is null");
            BuildCompletedEvent += action;
        }

        public void UnsubscribeBuildingCompleted(Action<int> action)
        {
            Assert.IsNotNull(action, "Action is null");
            BuildCompletedEvent -= action;
        }

        #endregion
    }
}