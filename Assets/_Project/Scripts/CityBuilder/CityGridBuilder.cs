using Zenject;
using UnityEngine;

using _Project.Scripts.CityBuilder.Data;
using _Project.Scripts.PrefabDictionary;

namespace _Project.Scripts.CityBuilder
{
    public class CityGridBuilder
    {
        private City _cityConfig;
        private PrefabSerializableDictionary _prefabDictionary;

        [Inject]
        private CityGridBuilder(
            City cityConfig,
            PrefabSerializableDictionary prefabDictionary)
        {
            _cityConfig = cityConfig;
            _prefabDictionary = prefabDictionary;
        }
        
        public void BuildGrid(Transform root, float xOffsetMult, float yOffsetMult)
        {
            var prefab = _prefabDictionary[_cityConfig.PrefabId];
            
            for (var x = 0; x < _cityConfig.Width; x++)
            {
                for (var y = 0; y < _cityConfig.Height; y++)
                {
                    var cellPosition = new Vector3(x * xOffsetMult, 0.0f, y * yOffsetMult);
                    var cell = MonoBehaviour.Instantiate(prefab, cellPosition, Quaternion.identity, root);
                }
            }
        }
    }
}