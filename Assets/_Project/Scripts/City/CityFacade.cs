using Zenject;
using UnityEngine;

using _Project.Scripts.City.Builders.Grid;
using _Project.Scripts.City.ConfigWrappers;
using _Project.Scripts.City.Builders.Terrain;

namespace _Project.Scripts.City
{
    public class CityFacade : MonoBehaviour
    {
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

        public void OnPlaceBuilding(Vector3Int position)
        {
            if (_gridBuilder.IsPositionFree(position))
            {
                PlaceBuilding(position, GetRandomBuildingIndex());
            }
        }
    }
}