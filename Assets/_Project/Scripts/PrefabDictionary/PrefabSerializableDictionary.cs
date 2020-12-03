using System;
using UnityEngine;
using DictionarySerialization.Source;

namespace _Project.Scripts.PrefabDictionary
{
    [Serializable]
    public class PrefabSerializableDictionary : SerializableDictionary<int, GameObject>
    {
    }
}