using System;

namespace _Project.Scripts.CityBuilder.Data
{
    [Serializable]
    public class City
    {
        private int _prefabId;
        private int _width;
        private int _height;
        private Building[] _buildings;

        public int PrefabId => _prefabId;
        public int Width => _width;
        public int Height => _height;
        public Building[] Buildings => _buildings;

        public City(
            int prefabId,
            int width,
            int height,
            Building[] buildings)
        {
            _prefabId = prefabId;
            _width = width;
            _height = height;
            _buildings = buildings;
        }
    }
}