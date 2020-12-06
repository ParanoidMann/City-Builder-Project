using UnityEngine;

namespace _Project.Scripts.City.Systems.Builders
{
    public interface ICityBuilder
    {
        void BuildCity();
        void PlaceBuilding(Vector3Int position, int buildingIndex);
    }
}