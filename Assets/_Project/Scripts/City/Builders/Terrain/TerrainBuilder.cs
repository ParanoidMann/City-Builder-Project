using System;
using Zenject;
using UnityEngine;

using _Project.Scripts.City.Data;
using _Project.Scripts.PrefabDictionary;

namespace _Project.Scripts.City.Builders.Terrain
{
    public class TerrainBuilder : ICityBuilder
    {
        private const float DefaultTerrainHeight = 1.0f;

        private CityConfig _cityConfig;
        private GameObject _terrainPrefab;
        private PrefabSerializableDictionary _prefabDictionary;

        private GameObject _terrain;

        [Inject]
        private TerrainBuilder(
            CityConfig cityConfig,
            GameObject terrainPrefab,
            PrefabSerializableDictionary prefabDictionary)
        {
            _cityConfig = cityConfig;
            _terrainPrefab = terrainPrefab;
            _prefabDictionary = prefabDictionary;
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

            if (_prefabDictionary.TryGetValue(buildingConfig.PrefabId, out var buildingPrefab))
            {
                var building = MonoBehaviour.Instantiate(buildingPrefab);
                building.transform.position = position;
            }
            else
            {
                throw new InvalidOperationException("No such element");
            }
        }
    }
}