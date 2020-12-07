using UnityEngine;

namespace _Project.Scripts.City.Systems.Builders
{
    public interface IBuildingPlacer
    {
        void PlaceBuilding(Vector3Int position, int buildingIndex);
    }
}