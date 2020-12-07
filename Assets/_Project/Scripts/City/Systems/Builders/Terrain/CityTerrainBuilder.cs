using Zenject;
using UnityEngine;
using _Project.Scripts.City.ConfigWrappers;

namespace _Project.Scripts.City.Systems.Builders.Terrain
{
    public class CityTerrainBuilder : IBuildingPlacer
    {
        private const float DefaultTerrainHeight = 1.0f;

        private GameObject _terrain;
        private CityConfig _cityConfig;
        private BuildingCreator _buildingCreator;

        [Inject]
        private CityTerrainBuilder(
            CityConfig cityConfig,
            [Inject(Id = ZenjectTags.Terrain)] GameObject terrain,
            BuildingCreator buildingCreator)
        {
            _cityConfig = cityConfig;
            _terrain = terrain;
            _buildingCreator = buildingCreator;
        }

        public void InitCityBuilder()
        {
            _terrain.transform.localScale = new Vector3(
                _cityConfig.Width, DefaultTerrainHeight, _cityConfig.Length);
        }

        public void PlaceBuilding(Vector3Int position, int buildingIndex)
        {
            var buildingConfig = _cityConfig.Buildings[buildingIndex];

            _buildingCreator.CreateBuilding(position, buildingConfig);
        }
    }
}