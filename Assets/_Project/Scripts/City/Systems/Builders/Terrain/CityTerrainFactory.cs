using Zenject;
using UnityEngine;
using _Project.Scripts.Helpers;

namespace _Project.Scripts.City.Systems.Builders.Terrain
{
    public class CityTerrainFactory : IFactory<GameObject>
    {
        private GameObject _terrainPrefab;

        [Inject]
        public CityTerrainFactory(
            [Inject(Id = ZenjectTags.TerrainPrefab)] GameObject terrainPrefab)
        {
            _terrainPrefab = terrainPrefab;
        }

        public GameObject Create()
        {
            return MonoBehaviour.Instantiate(_terrainPrefab);
        }
    }
}