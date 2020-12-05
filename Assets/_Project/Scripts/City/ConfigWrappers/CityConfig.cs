using System;

namespace _Project.Scripts.City.ConfigWrappers
{
    [Serializable]
    public class CityConfig
    {
        private int _width;
        private int _length;
        private BuildingConfig[] _buildings;

        public int Width => _width;
        public int Length => _length;
        public BuildingConfig[] Buildings => _buildings;

        public CityConfig(
            int width,
            int length,
            BuildingConfig[] buildings)
        {
            _width = width;
            _length = length;
            _buildings = buildings;
        }
    }
}