using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.HashTable
{
    class HashTableArray<TKey, TValue>
    {
        private HashTableArrayNode<TKey, TValue>[] _array;

        /// <summary>
        /// Creates a new HashTableArray with a specific capacity
        /// </summary>
        /// <param name="capacity">The capacity of the array</param>
        public HashTableArray(int capacity)
        {
            _array = new HashTableArrayNode<TKey, TValue>[capacity];
        }

        /// <summary>
        /// Adds key value pair to the node.
        /// </summary>
        /// <param name="key">The key value</param>
        /// <param name="value">The value to be added</param>
        /// <exception cref="ArgumentException">Thrown when key already exists</exception>
        public void Add(TKey key, TValue value)
        {
            int index = GetIndex(key);
            HashTableArrayNode<TKey, TValue> nodes = _array[index];
            if (nodes == null)
            {
                // Create the HashTableArrayNode and add it to the array
                nodes = new HashTableArrayNode<TKey, TValue>();
                _array[index] = nodes;
            }
            // add the key value pair to the nodes
            nodes.Add(key, value);
        }

        /// <summary>
        /// Updates the value of existing key value pair in the node array
        /// </summary>
        /// <param name="key">The key value</param>
        /// <param name="value">The value to be added</param>
        /// <exception cref="ArgumentException">Thrown when key does not exists</exception>
        public void Update(TKey key, TValue value)
        {
            HashTableArrayNode<TKey, TValue> nodes = _array[GetIndex(key)];
            if (nodes == null)
            {
                throw new ArgumentException("The key does not exists", "key");
            }

            nodes.Update(key, value);
        }

        /// <summary>
        /// Removes the key value pair that has the specified key
        /// </summary>
        /// <param name="key">The key of the key value pair to be removed</param>
        /// <returns>True if the key value pair is removed, false if it is not</returns>
        public bool Remove(TKey key)
        {
            HashTableArrayNode<TKey, TValue> nodes = _array[GetIndex(key)];
            if (nodes != null)
            {
                return nodes.Remove(key);
            }

            return false;
        }

        /// <summary>
        /// Tries to get the value associated with the key and set the out value to the found value
        /// </summary>
        /// <param name="key">The key to look for in the collection</param>
        /// <param name="value">The out parameter the value will be assigned to if found</param>
        /// <returns>True if the key is found, false if it is not</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            HashTableArrayNode<TKey, TValue> nodes = _array[GetIndex(key)];
            if (nodes != null)
            {
                return nodes.TryGetValue(key, out value);
            }

            // Key does not exists, return default value for TValue
            value = default(TValue);
            return false;
        }

        /// <summary>
        /// The capacity of the hash table array
        /// </summary>
        public int Capacity
        {
            get
            {
                return _array.Length;
            }
        }

        /// <summary>
        /// Removes all items from the hash table array
        /// </summary>
        public void Clear()
        {
            foreach(var node in _array.Where(o => o != null))
            {
                node.Clear();
            }
        }

        /// <summary>
        /// Gets an enumerator for all of the keys in the node array
        /// </summary>
        public IEnumerable<TKey> Keys
        {
            get
            {
                foreach(var node in _array.Where(o => o != null))
                {
                    foreach(var key in node.Keys)
                    {
                        yield return key;
                    }
                }
            }
        }

        /// <summary>
        /// Gets an enumerator for all of the values in the node array
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get
            {
                foreach (var node in _array.Where(o => o != null))
                {
                    foreach (var value in node.Values)
                    {
                        yield return value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets an enumerator for all of the items in the node array
        /// </summary>
        public IEnumerable<HashTableNodePair<TKey, TValue>> Items
        {
            get
            {
                foreach (var node in _array.Where(o => o != null))
                {
                    foreach (var nodePair in node.Items)
                    {
                        yield return nodePair;
                    }
                }
            }
        }

        /// <summary>
        /// Maps key to the array index based on hash code
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int GetIndex(TKey key)
        {
            return Math.Abs(key.GetHashCode() % Capacity);
        }
    }
}
