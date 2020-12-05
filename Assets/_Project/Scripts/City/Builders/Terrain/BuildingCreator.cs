using System;
using Zenject;
using UnityEngine;

using _Project.Scripts.PrefabDictionary;
using _Project.Scripts.City.ConfigWrappers;

namespace _Project.Scripts.City
{
    public class BuildingCreator
    {
        private PrefabSerializableDictionary _prefabDictionary;

        [Inject]
        private BuildingCreator(PrefabSerializableDictionary prefabDictionary)
        {
            _prefabDictionary = prefabDictionary;
        }

        public GameObject CreateBuilding(Vector3Int position, BuildingConfig buildingConfig)
        {
            if (_prefabDictionary.TryGetValue(buildingConfig.PrefabId, out var buildingPrefab))
            {
                var building = MonoBehaviour.Instantiate(buildingPrefab);
                building.transform.position = position;

                return building;
            }

            throw new InvalidOperationException("No such element");
        }
    }
}