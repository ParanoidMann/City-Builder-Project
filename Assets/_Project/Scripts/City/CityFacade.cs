using System;
using Zenject;
using ModestTree;

using UnityEngine;
using Random = UnityEngine.Random;

using _Project.Scripts.City.Builders.Grid;
using _Project.Scripts.City.ConfigWrappers;
using _Project.Scripts.City.Builders.Terrain;

namespace _Project.Scripts.City
{
    public class CityFacade : MonoBehaviour
    {
        private event Action BuildStoppedEvent;
        private event Action<int> BuildCompletedEvent;

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
            return Random.Range(0, _cityConfig.Buildings.Length - 1);
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

        private void InvokeBuildingCompleted(int buildingIndex)
        {
            var might = _cityConfig.Buildings[buildingIndex].Might;

            BuildCompletedEvent?.Invoke(might);
            BuildStoppedEvent?.Invoke();
        }

        public void OnPlaceBuilding(Vector3Int position)
        {
            if (_gridBuilder.IsPositionFree(position))
            {
                var buildingIndex = GetRandomBuildingIndex();

                PlaceBuilding(position, buildingIndex);
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
        
        // [SerializeField]
        // private Material _material;
        //
        // [SerializeField]
        // private Material _material2;
        //
        // private void OnGUI()
        // {
        //     if (GUILayout.Button("Invisible"))
        //     {
        //         MaterialChanger.ChangeAlpha(_material, 0.1f);
        //         MaterialChanger.ChangeAlpha(_material2, 0.1f);
        //     }
        // }
    }
}