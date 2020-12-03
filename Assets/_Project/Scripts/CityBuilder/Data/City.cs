using System;

namespace _Project.Scripts.CityBuilder.Data
{
    [Serializable]
    public class City
    {
        private int _width;
        private int _height;
        private Building[] _buildings;

        public int Width => _width;
        public int Height => _height;
        public Building[] Buildings => _buildings;

        public City(
            int width,
            int height,
            Building[] buildings)
        {
            _width = width;
            _height = height;
            _buildings = buildings;
        }
    }
}