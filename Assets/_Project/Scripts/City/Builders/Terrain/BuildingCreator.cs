using System;
using Zenject;
using UnityEngine;

using _Project.Scripts.Interaction.UI;
using _Project.Scripts.PrefabDictionary;
using _Project.Scripts.City.ConfigWrappers;

namespace _Project.Scripts.City.Builders.Terrain
{
    public class BuildingCreator
    {
        private GameObject _mightPrefab;
        private PrefabSerializableDictionary _prefabDictionary;

        [Inject]
        private BuildingCreator(
            [Inject(Id = ZenjectTags.Might)] GameObject mightPrefab,
            PrefabSerializableDictionary prefabDictionary)
        {
            _mightPrefab = mightPrefab;
            _prefabDictionary = prefabDictionary;
        }

        private GameObject InstantiateMight(Transform root, int might) // TODO : Remove getComponent
        {
            var mightCanvas = MonoBehaviour.Instantiate(_mightPrefab, root);

            var mightWorldCanvas = mightCanvas.GetComponent<MightWorldCanvas>();
            mightWorldCanvas.SetText(might.ToString());

            return mightCanvas;
        }

        private GameObject InstantiateFloor(Transform root, GameObject buildingPrefab, int floorNumber)
        {
            var buildingFloor = MonoBehaviour.Instantiate(buildingPrefab, root);

            var floorPositionY = buildingFloor.transform.localScale.y * floorNumber;
            buildingFloor.transform.position = new Vector3(0.0f, floorPositionY, 0.0f); // TODO : Make normal height

            return buildingFloor;
        }

        private GameObject InstantiateBuilding(BuildingConfig buildingConfig, GameObject buildingPrefab)
        {
            var building = new GameObject();
            GameObject upperBuildingFloor = null;

            for (var i = 0; i < buildingConfig.Height; i++)
            {
                upperBuildingFloor = InstantiateFloor(building.transform, buildingPrefab, i);
            }

            if (upperBuildingFloor != null)
            {
                InstantiateMight(upperBuildingFloor.transform, buildingConfig.Might);
            }

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