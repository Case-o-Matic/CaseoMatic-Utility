using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;

namespace Caseomatic.Util
{
    [Synchronization]
    public class ConcurrentDictionary<TKey, TValue> : ContextBoundObject
    {
        private readonly Dictionary<TKey, TValue> dictionary;

        public TKey[] AllKeys
        {
            get
            {
                return dictionary.Keys.ToArray();
            }
        }
        public TValue[] AllValues
        {
            get
            {
                return dictionary.Values.ToArray();
            }
        }

        public int Count
        {
            get
            {
                return dictionary.Count;
            }
        }

        public TValue this[TKey key]
        {
            get { return dictionary[key]; }
            set { dictionary[key] = value; }
        }

        public ConcurrentDictionary(int estimatedValues = 300)
        {
            dictionary = new Dictionary<TKey, TValue>(estimatedValues);
        }

        public void Add(TKey key, TValue value)
        {
            dictionary.Add(key, value);
        }
        public void AddRange(KeyValuePair<TKey, TValue>[] keyValuePairs)
        {
            foreach (var keyValuePair in keyValuePairs)
            {
                dictionary.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }

        public bool Remove(TKey key)
        {
            return dictionary.Remove(key);
        }

        public bool ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        public KeyValuePair<TKey, TValue>FirstOrDefault(Func<KeyValuePair<TKey, TValue>, bool> predicate)
        {
            return dictionary.FirstOrDefault(predicate);
        }

        public void Clear()
        {
            dictionary.Clear();
        }
    }
}
