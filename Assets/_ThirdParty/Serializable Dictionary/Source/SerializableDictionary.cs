using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DictionarySerialization.Source
{
    public static class SerializableDictionary
    {
        public class Storage<T> : BaseSerializableDictionary.Storage
        {
            public T data;
        }
    }

    public class SerializableDictionary<TKey, TValue> : BaseSerializableDictionary<TKey, TValue, TValue>
    {
        public SerializableDictionary()
        {
        }

        public SerializableDictionary(IDictionary<TKey, TValue> dict) : base(dict)
        {
        }

        protected SerializableDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected override TValue GetValue(TValue[] storage, int i)
        {
            return storage[i];
        }

        protected override void SetValue(TValue[] storage, int i, TValue value)
        {
            storage[i] = value;
        }
    }

    public class SerializableDictionary<TKey, TValue, TValueStorage>
        : BaseSerializableDictionary<TKey, TValue, TValueStorage>
        where TValueStorage : SerializableDictionary.Storage<TValue>, new()
    {
        public SerializableDictionary()
        {
        }

        public SerializableDictionary(IDictionary<TKey, TValue> dict) : base(dict)
        {
        }

        protected SerializableDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected override TValue GetValue(TValueStorage[] storage, int i)
        {
            return storage[i].data;
        }

        protected override void SetValue(TValueStorage[] storage, int i, TValue value)
        {
            storage[i] = new TValueStorage {data = value};
        }
    }
}