using Zenject;
using UnityEngine;
using _Project.Scripts.City.ConfigWrappers;

namespace _Project.Scripts.City.Builders.Grid
{
    public class GridBuilder : ICityBuilder
    {
        private CityConfig _cityConfig;

        private CityGrid _cityGrid;

        [Inject]
        private GridBuilder(CityConfig cityConfig)
        {
            _cityConfig = cityConfig;
        }

        public void BuildCity()
        {
            _cityGrid = new CityGrid(_cityConfig.Width, _cityConfig.Length);
        }

        public void PlaceBuilding(Vector3Int position, int buildingIndex)
        {
            _cityGrid[position.x, position.z] = CellType.Building;
        }

        public bool IsPositionFree(Vector3Int position)
        {
            return _cityGrid[position.x, position.z] == CellType.Empty;
        }
    }
}