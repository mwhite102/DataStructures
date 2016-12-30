using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.HashTable
{
    internal class HashTableArrayNode<TKey, TValue>
    {
        // Holds the actual data in the hash table and chains together data collisions.
        private LinkedList<HashTableNodePair<TKey, TValue>> _items;

        /// <summary>
        /// Adds the key value pair to the node
        /// </summary>
        /// <param name="key">The key being added</param>
        /// <param name="value">The value being added</param>
        /// <exception cref="ArgumentException">Thrown when key already exists in the collection</exception>
        public void Add(TKey key, TValue value)
        {
            // Lazy init
            if (_items == null)
            {
                _items = new LinkedList<HashTableNodePair<TKey, TValue>>();
            }
            else
            {
                // Make sure key does not already exists in the list
                foreach (var nodePair in _items)
                {
                    if (nodePair.Key.Equals(key))
                    {
                        throw new ArgumentException("Key already exists");
                    }
                }
            }

            // Add the item
            _items.AddFirst(new HashTableNodePair<TKey, TValue>(key, value));
        }

        /// <summary>
        /// Updates the value of an existing key value pair
        /// </summary>
        /// <param name="key">The key of the item to be updated</param>
        /// <param name="value">The new value of the key value pair</param>
        /// /// <exception cref="ArgumentException">Thrown when the key does not exists in the collection</exception>
        public void Update(TKey key, TValue value)
        {
            bool updated = false;

            if (_items != null)
            {
                foreach (var nodePair in _items)
                {
                    if (nodePair.Key.Equals(key))
                    {
                        nodePair.Value = value;
                        updated = true;
                        break;
                    }
                }
            }

            if (!updated)
            {
                throw new ArgumentException("Key not found");
            }
        }

        /// <summary>
        /// Returns value for a specified key
        /// </summary>
        /// <param name="key">The key to find the value for</param>
        /// <param name="value">The value of the key</param>
        /// <returns>True if found, false if it is not</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default(TValue);

            bool isFound = false;

            if (_items != null)
            {
                foreach (var nodePair in _items)
                {
                    if (nodePair.Key.Equals(key))
                    {
                        value = nodePair.Value;
                        isFound = true;
                        break;
                    }
                }
            }

            return isFound;
        }

        /// <summary>
        /// Removes the item with the specified key
        /// </summary>
        /// <param name="key">The key of the item to remove</param>
        /// <returns>True if the item is removed, otherwise false</returns>
        public bool Remove(TKey key)
        {
            bool isRemoved = false;

            if (_items != null)
            {
                LinkedListNode<HashTableNodePair<TKey, TValue>> current = _items.First;
                while (current != null)
                {
                    if (current.Value.Key.Equals(key))
                    {
                        _items.Remove(current);
                        isRemoved = true;
                        break;
                    }

                    current = current.Next;
                }
            }

            return isRemoved;
        }

        /// <summary>
        /// Clears the items in the collection.
        /// Another option is to set _items = null
        /// </summary>
        public void Clear()
        {
            if (_items != null)
            {
                _items.Clear();
            }
        }

        /// <summary>
        /// An enumerator for all of the keys
        /// </summary>
        public IEnumerable<TKey> Keys
        {
            get
            {
                if (_items != null)
                {
                    foreach (HashTableNodePair<TKey, TValue> nodePair in _items)
                    {
                        yield return nodePair.Key;
                    }
                }
            }
        }

        /// <summary>
        /// An enumerator for all of the values
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get
            {
                if (_items != null)
                {
                    foreach (HashTableNodePair<TKey, TValue> nodePair in _items)
                    {
                        yield return nodePair.Value;
                    }
                }
            }
        }

        /// <summary>
        /// An enumerator for all of the items
        /// </summary>
        public IEnumerable<HashTableNodePair<TKey, TValue>> Items
        {
            get
            {
                if (_items != null)
                {
                    foreach (HashTableNodePair<TKey, TValue> nodePair in _items)
                    {
                        yield return nodePair;
                    }
                }
            }
        }
    }
}
