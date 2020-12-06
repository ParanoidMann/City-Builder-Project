using System;
using Zenject;
using ModestTree;

using UnityEngine;
using Random = UnityEngine.Random;

using _Project.Scripts.Helpers;
using _Project.Scripts.City.Builders.Grid;
using _Project.Scripts.City.ConfigWrappers;
using _Project.Scripts.City.Builders.Terrain;

namespace _Project.Scripts.City
{
    public class CityFacade : MonoBehaviour
    {
        private event Action BuildStoppedEvent;
        private event Action<int> BuildCompletedEvent;

        private const float FullVisibilityAlpha = 1.0f;

        [Header("Materials")]
        [SerializeField]
        private float _transparentAlpha = 0.5f;
        
        [SerializeField]
        private Material[] _materials;

        private CityConfig _cityConfig;
        private GridBuilder _gridBuilder;
        private TerrainBuilder _terrainBuilder;

        [Inject]
        private void Construct(
            CityConfig cityConfig,
            GridBuilder gridBuilder,
            TerrainBuilder terrainBuilder)
        {
            _cityConfig = cityConfig;
            _gridBuilder = gridBuilder;
            _terrainBuilder = terrainBuilder;
        }

        private void Awake()
        {
            BuildCity();
        }

        private int GetRandomBuildingIndex()
        {
            return Random.Range(0, _cityConfig.Buildings.Length);
        }

        private void BuildCity()
        {
            _gridBuilder.BuildCity();
            _terrainBuilder.BuildCity();
        }

        private void PlaceBuilding(Vector3Int position, int buildingIndex)
        {
            _gridBuilder.PlaceBuilding(position, buildingIndex);
            _terrainBuilder.PlaceBuilding(position, buildingIndex);
        }

        private void ChangeMaterialsVisibility(float alpha)
        {
            foreach (var material in _materials)
            {
                MaterialChanger.ChangeAlpha(material, alpha);
            }
        }

        private void InvokeBuildingCompleted(int buildingIndex)
        {
            var might = _cityConfig.Buildings[buildingIndex].Might;

            BuildCompletedEvent?.Invoke(might);
            BuildStoppedEvent?.Invoke();
        }

        public void OnBuildingStarted()
        {
            ChangeMaterialsVisibility(_transparentAlpha);
        }

        public void OnPlaceBuilding(Vector3Int position)
        {
            if (_gridBuilder.IsPositionFree(position))
            {
                var buildingIndex = GetRandomBuildingIndex();

                PlaceBuilding(position, buildingIndex);
                ChangeMaterialsVisibility(FullVisibilityAlpha);

                InvokeBuildingCompleted(buildingIndex);
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