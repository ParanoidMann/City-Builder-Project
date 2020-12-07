using Zenject;
using UnityEngine;
using _Project.Scripts.City.ConfigWrappers;

namespace _Project.Scripts.City.Systems.Builders.Grid
{
    public class CityGridBuilder : ICityBuilder
    {
        private const int EmptyCellsOffset = 1;

        private CityGrid _cityGrid;
        private CityConfig _cityConfig;

        [Inject]
        private CityGridBuilder(
            CityGrid cityGrid,
            CityConfig cityConfig)
        {
            _cityGrid = cityGrid;
            _cityConfig = cityConfig;
        }

        public void InitCityBuilder()
        {
        }

        public void PlaceBuilding(Vector3Int position, int buildingIndex)
        {
            var building = _cityConfig.Buildings[buildingIndex];
            var baseSize = building.BaseSize;

            for (var x = position.x - EmptyCellsOffset; x < position.x + baseSize + EmptyCellsOffset; x++)
            {
                for (var z = position.z - EmptyCellsOffset; z < position.z + baseSize + EmptyCellsOffset; z++)
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

        public bool IsPositionFree(Vector3Int position, int buildingIndex)
        {
            var building = _cityConfig.Buildings[buildingIndex];
            var baseSize = building.BaseSize;

            for (var x = position.x; x < position.x + baseSize; x++)
            {
                for (var z = position.z; z < position.z + baseSize; z++)
                {
                    if (_cityGrid[x, z] != CellType.Empty)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}