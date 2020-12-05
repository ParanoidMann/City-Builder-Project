using System;

namespace _Project.Scripts.City.Data
{
    [Serializable]
    public class CityConfig
    {
        private int _prefabId;
        private int _width;
        private int _height;
        private BuildingConfig[] _buildings;

        public int PrefabId => _prefabId;
        public int Width => _width;
        public int Height => _height;
        public BuildingConfig[] Buildings => _buildings;

        public CityConfig(
            int prefabId,
            int width,
            int height,
            BuildingConfig[] buildings)
        {
            _prefabId = prefabId;
            _width = width;
            _height = height;
            _buildings = buildings;
        }
    }
}