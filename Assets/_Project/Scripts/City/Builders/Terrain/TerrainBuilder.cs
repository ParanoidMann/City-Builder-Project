using Zenject;
using UnityEngine;
using _Project.Scripts.City.Data;

namespace _Project.Scripts.City.Builders.Terrain
{
    public class TerrainBuilder : ICityBuilder
    {
        private const float DefaultTerrainHeight = 1.0f;

        private CityConfig _cityConfigConfig;
        private GameObject _terrainPrefab;

        private GameObject _terrain;

        [Inject]
        private TerrainBuilder(
            CityConfig cityConfigConfig,
            GameObject terrainPrefab)
        {
            _cityConfigConfig = cityConfigConfig;
            _terrainPrefab = terrainPrefab;
        }

        public void Build()
        {
            _terrain = MonoBehaviour.Instantiate(_terrainPrefab);

            _terrain.transform.localScale = new Vector3(
                _cityConfigConfig.Width, DefaultTerrainHeight, _cityConfigConfig.Height);
        }
    }
}