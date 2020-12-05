using System;
using Zenject;
using UnityEngine;

using _Project.Scripts.PrefabDictionary;
using _Project.Scripts.City.ConfigWrappers;

namespace _Project.Scripts.City.Builders.Terrain
{
    public class BuildingCreator
    {
        private PrefabSerializableDictionary _prefabDictionary;

        [Inject]
        private BuildingCreator(PrefabSerializableDictionary prefabDictionary)
        {
            _prefabDictionary = prefabDictionary;
        }

        private GameObject InstantiateBuilding(BuildingConfig buildingConfig, GameObject buildingPrefab)
        {
            var building = new GameObject();

            for (var i = 0; i < buildingConfig.Height; i++)
            {
                var buildingFloor = MonoBehaviour.Instantiate(buildingPrefab, building.transform);
                
                var stairTransform = buildingFloor.transform;
                stairTransform.position = new Vector3(0.0f, buildingFloor.transform.localScale.y * i, 0.0f);
            }

            // TODO : Create might
            
            return building;
        }

        public GameObject CreateBuilding(Vector3Int position, BuildingConfig buildingConfig)
        {
            if (_prefabDictionary.TryGetValue(buildingConfig.PrefabId, out var buildingPrefab))
            {
                var building = InstantiateBuilding(buildingConfig, buildingPrefab);
                building.transform.position = position;
                
                return building;
            }

            throw new InvalidOperationException("No such element");
        }
    }
}