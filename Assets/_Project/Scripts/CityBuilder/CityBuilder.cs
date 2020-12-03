using Zenject;
using UnityEngine;

using _Project.Scripts.CityBuilder.Data;
using _Project.Scripts.PrefabDictionary;

namespace _Project.Scripts.CityBuilder
{
    public class CityBuilder : MonoBehaviour
    {
        private City _cityConfig;
        private PrefabSerializableDictionary _prefabDictionary;

        [Inject]
        private void Construct(
            City cityConfig,
            PrefabSerializableDictionary prefabDictionary)
        {
            _cityConfig = cityConfig;
            _prefabDictionary = prefabDictionary;
        }
    }
}