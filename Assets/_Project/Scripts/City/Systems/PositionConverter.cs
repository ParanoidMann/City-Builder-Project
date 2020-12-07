using System;
using UnityEngine;

namespace _Project.Scripts.City.Systems
{
    [Serializable]
    public class PositionConverter
    {
        [SerializeField]
        private Vector3Int _positionOffset;

        private Vector3Int DowngradeToZeroMinimum(Vector3Int position)
        {
            position.x--;
            position.z--;

            return position;
        }

        public Vector3Int NormalizePosition(Vector3Int position)
        {
            var downgradedPosition = DowngradeToZeroMinimum(position);
            downgradedPosition += _positionOffset;

            return downgradedPosition;
        }
    }
}