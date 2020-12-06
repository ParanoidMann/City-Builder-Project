using Zenject;
using UnityEngine;
using _Project.Scripts.City.ConfigWrappers;

namespace _Project.Scripts.City.Builders.Grid
{
    public class GridBuilder : ICityBuilder
    {
        private const int EmptyCellsOffset = 1;

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
            var building = _cityConfig.Buildings[buildingIndex];
            var baseSize = building.BaseSize;

            for (var x = position.x - EmptyCellsOffset; x <= position.x + baseSize; x++)
            {
                for (var z = position.z - EmptyCellsOffset; z <= position.z + baseSize; z++)
                {
                    if (x < position.x || x >= position.x + baseSize ||
                        z < position.z || z >= position.z + baseSize)
                    {
                        _cityGrid[x, z] = CellType.Closed;
                    }
                    else
                    {
                        _cityGrid[x, z] = CellType.Building;
                    }
                }
            }
        }

        public bool IsPositionFree(Vector3Int position)
        {
            return _cityGrid[position.x, position.z] == CellType.Empty;
        }
    }
}