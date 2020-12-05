using Zenject;
using UnityEngine;

using _Project.Scripts.City.Builders.Grid;
using _Project.Scripts.City.Builders.Terrain;

namespace _Project.Scripts.City
{
    public class CityFacade : MonoBehaviour
    {
        private GridBuilder _gridBuilder;
        private TerrainBuilder _terrainBuilder;

        [Inject]
        private void Construct(
            GridBuilder gridBuilder,
            TerrainBuilder terrainBuilder)
        {
            _gridBuilder = gridBuilder;
            _terrainBuilder = terrainBuilder;
        }

        private void Awake()
        {
            BuildCity();
        }

        private void BuildCity()
        {
            _gridBuilder.Build();
            _terrainBuilder.Build();
        }

        public void OnBuildHouse(Vector3Int position)
        {
            Debug.Log($"position = {position}");
        }
    }
}