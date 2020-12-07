using System;
using UnityEngine;

namespace _Project.Scripts.City.Systems
{
    [Serializable]
    public class PositionConverter
    {
        [SerializeField]
        private Vector3Int _positionOffset;

        public Vector3Int DowngradeToZeroMinimum(Vector3Int position)
        {
            position.x--;
            position.z--;

            return position;
        }

        public Vector3Int AddPositionOffset(Vector3Int position)
        {
            position += _positionOffset;
            return position;
        }
    }
}