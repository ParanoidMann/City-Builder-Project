using Zenject;
using UnityEngine;
using _Project.Scripts.City.ConfigWrappers;

namespace _Project.Scripts.City.Builders.Terrain
{
    public class TerrainBuilder : ICityBuilder
    {
        private const float DefaultTerrainHeight = 1.0f;

        private CityConfig _cityConfig;
        private GameObject _terrainPrefab;
        private BuildingCreator _buildingCreator;

        private GameObject _terrain;

        [Inject]
        private TerrainBuilder(
            CityConfig cityConfig,
            GameObject terrainPrefab,
            BuildingCreator buildingCreator)
        {
            _cityConfig = cityConfig;
            _terrainPrefab = terrainPrefab;
            _buildingCreator = buildingCreator;
        }

        public void BuildCity()
        {
            _terrain = MonoBehaviour.Instantiate(_terrainPrefab);

            _terrain.transform.localScale = new Vector3(
                _cityConfig.Width, DefaultTerrainHeight, _cityConfig.Length);
        }

        public void PlaceBuilding(Vector3Int position, int buildingIndex)
        {
            var buildingConfig = _cityConfig.Buildings[buildingIndex];
            
            _buildingCreator.CreateBuilding(position, buildingConfig); // TODO : Add to Array
        }
    }
}