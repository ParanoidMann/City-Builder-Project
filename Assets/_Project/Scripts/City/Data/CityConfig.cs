using System;

namespace _Project.Scripts.City.Data
{
    [Serializable]
    public class CityConfig
    {
        private int _prefabId;
        private int _width;
        private int _length;
        private BuildingConfig[] _buildings;

        public int PrefabId => _prefabId;
        public int Width => _width;
        public int Length => _length;
        public BuildingConfig[] Buildings => _buildings;

        public CityConfig(
            int prefabId,
            int width,
            int length,
            BuildingConfig[] buildings)
        {
            _prefabId = prefabId;
            _width = width;
            _length = length;
            _buildings = buildings;
        }
    }
}