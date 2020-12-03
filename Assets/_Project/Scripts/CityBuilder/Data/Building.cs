using System;

namespace _Project.Scripts.CityBuilder.Data
{
    [Serializable]
    public class Building
    {
        private int _prefabId;
        private int _baseSize;
        private int _height;
        private int _might;

        public int PrefabId => _prefabId;
        public int BaseSize => _baseSize;
        public int Height => _height;
        public int Might => _might;

        public Building(
            int prefabId,
            int baseSize,
            int height,
            int might)
        {
            _prefabId = prefabId;
            _baseSize = baseSize;
            _height = height;
            _might = might;
        }
    }
}