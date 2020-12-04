using Zenject;
using UnityEngine;
using _Project.Scripts.CityBuilder.Data;

namespace _Project.Scripts.CityBuilder
{
    public class TerrainManager
    {
        private const float DefaultTerrainHeight = 1.0f;

        private City _cityConfig;
        private Transform _terrain;

        [Inject]
        private TerrainManager(City cityConfig)
        {
            _cityConfig = cityConfig;
        }

        public void InitTerrain(Transform terrain)
        {
            _terrain = terrain;

            _terrain.localScale = new Vector3(_cityConfig.Width, DefaultTerrainHeight, _cityConfig.Height);
        }
    }
}