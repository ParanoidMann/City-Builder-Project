using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DictionarySerialization.Source
{
    public abstract class BaseSerializableDictionary
    {
        public abstract class Storage
        {
        }

        protected class Dictionary<TKey, TValue> : System.Collections.Generic.Dictionary<TKey, TValue>
        {
            public Dictionary()
            {
            }

            public Dictionary(IDictionary<TKey, TValue> dict) : base(dict)
            {
            }

            public Dictionary(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }

    [Serializable]
    public abstract class BaseSerializableDictionary<TKey, TValue, TValueStorage>
        : BaseSerializableDictionary, IDictionary<TKey, TValue>,
            IDictionary, ISerializationCallbackReceiver, IDeserializationCallback, ISerializable
    {
        [SerializeField]
        private TKey[] _keys;

        [SerializeField]
        private TValueStorage[] _values;

        private Dictionary<TKey, TValue> _dictionary;

        protected BaseSerializableDictionary(SerializationInfo info, StreamingContext context)
        {
            _dictionary = new Dictionary<TKey, TValue>(info, context);
        }

        public BaseSerializableDictionary()
        {
            _dictionary = new Dictionary<TKey, TValue>();
        }

        public BaseSerializableDictionary(IDictionary<TKey, TValue> dict)
        {
            _dictionary = new Dictionary<TKey, TValue>(dict);
        }

        protected abstract TValue GetValue(TValueStorage[] storage, int i);
        protected abstract void SetValue(TValueStorage[] storage, int i, TValue value);

        public void CopyFrom(IDictionary<TKey, TValue> dict)
        {
            _dictionary.Clear();

            foreach (var kvp in dict)
            {
                _dictionary[kvp.Key] = kvp.Value;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable) _dictionary).GetObjectData(info, context);
        }

        public void OnAfterDeserialize()
        {
            if (_keys != null && _values != null && _keys.Length == _values.Length)
            {
                _dictionary.Clear();

                var n = _keys.Length;
                for (var i = 0; i < n; ++i)
                {
                    _dictionary[_keys[i]] = GetValue(_values, i);
                }

                _keys = null;
                _values = null;
            }
        }

        public void OnBeforeSerialize()
        {
            var count = _dictionary.Count;
            _keys = new TKey[count];
            _values = new TValueStorage[count];

            var i = 0;
            foreach (var kvp in _dictionary)
            {
                _keys[i] = kvp.Key;
                SetValue(_values, i, kvp.Value);

                ++i;
            }
        }

        public void OnDeserialization(object sender)
        {
            ((IDeserializationCallback) _dictionary).OnDeserialization(sender);
        }

        #region IDictionary<TKey, TValue>

        public ICollection<TKey> Keys => ((IDictionary<TKey, TValue>) _dictionary).Keys;
        public ICollection<TValue> Values => ((IDictionary<TKey, TValue>) _dictionary).Values;

        public int Count => ((IDictionary<TKey, TValue>) _dictionary).Count;
        public bool IsReadOnly => ((IDictionary<TKey, TValue>) _dictionary).IsReadOnly;

        public TValue this[TKey key]
        {
            get => ((IDictionary<TKey, TValue>) _dictionary)[key];
            set => ((IDictionary<TKey, TValue>) _dictionary)[key] = value;
        }

        public void Add(TKey key, TValue value)
        {
            ((IDictionary<TKey, TValue>) _dictionary).Add(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            return ((IDictionary<TKey, TValue>) _dictionary).ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            return ((IDictionary<TKey, TValue>) _dictionary).Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return ((IDictionary<TKey, TValue>) _dictionary).TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            ((IDictionary<TKey, TValue>) _dictionary).Add(item);
        }

        public void Clear()
        {
            ((IDictionary<TKey, TValue>) _dictionary).Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<TKey, TValue>) _dictionary).Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((IDictionary<TKey, TValue>) _dictionary).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<TKey, TValue>) _dictionary).Remove(item);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return ((IDictionary<TKey, TValue>) _dictionary).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<TKey, TValue>) _dictionary).GetEnumerator();
        }

        #endregion

        #region IDictionary

        ICollection IDictionary.Keys => ((IDictionary) _dictionary).Keys;
        ICollection IDictionary.Values => ((IDictionary) _dictionary).Values;

        public object SyncRoot => ((IDictionary) _dictionary).SyncRoot;

        public bool IsFixedSize => ((IDictionary) _dictionary).IsFixedSize;
        public bool IsSynchronized => ((IDictionary) _dictionary).IsSynchronized;

        public object this[object key]
        {
            get => ((IDictionary) _dictionary)[key];
            set => ((IDictionary) _dictionary)[key] = value;
        }

        public void Add(object key, object value)
        {
            ((IDictionary) _dictionary).Add(key, value);
        }

        public bool Contains(object key)
        {
            return ((IDictionary) _dictionary).Contains(key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary) _dictionary).GetEnumerator();
        }

        public void Remove(object key)
        {
            ((IDictionary) _dictionary).Remove(key);
        }

        public void CopyTo(Array array, int index)
        {
            ((IDictionary) _dictionary).CopyTo(array, index);
        }

        #endregion
    }
}