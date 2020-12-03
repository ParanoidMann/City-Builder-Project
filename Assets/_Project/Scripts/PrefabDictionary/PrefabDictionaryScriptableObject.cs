using UnityEngine;

namespace _Project.Scripts.PrefabDictionary
{
    [CreateAssetMenu(
        fileName = "PrefabDictionary",
        menuName = "ScriptableObjects/PrefabDictionary",
        order = 1)]
    public class PrefabDictionaryScriptableObject : ScriptableObject
    {
        [SerializeField]
        private PrefabSerializableDictionary _prefabSerializableDictionary;

        public PrefabSerializableDictionary PrefabSerializableDictionary => _prefabSerializableDictionary;
    }
}