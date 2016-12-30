using System;
using System.Collections.Generic;

namespace DataStructures.HashTable
{
    public class HashTable<TKey, TValue>
    {
        // If the array exceeds this fill factor percentage, it will grow
        const double _fillFactor = 0.75;

        // Maximum number of items to store before growing.
        int _maxItemsAtCurrentSize;

        // Number of items in the hash table
        int _count;

        // The array that stores the items
        HashTableArray<TKey, TValue> _array;

        /// <summary>
        /// Creates a hash table with default capacity
        /// </summary>
        public HashTable()
            : this(1000)
        {
        }

        /// <summary>
        /// Creates a hash table with a specified capacity
        /// </summary>
        /// <param name="initialCapacity">The initial capacity of the hash table</param>
        public HashTable(int initialCapacity)
        {
            if (initialCapacity < 1) throw new ArgumentOutOfRangeException("capacity");

            _array = new HashTableArray<TKey, TValue>(initialCapacity);

            // Calculate the _maxItemsAtCurrentSize value
            _maxItemsAtCurrentSize = (int)(initialCapacity * _fillFactor) + 1;
        }

        /// <summary>
        /// Adds key value pair to the hash table
        /// </summary>
        /// <param name="key">The key to be added</param>
        /// <param name="value">The value to be added</param>
        public void Add(TKey key, TValue value)
        {
            // Are we at capacity?  Do we need to grow the array?
            if (_count >= _maxItemsAtCurrentSize)
            {
                GrowArray();
            }

            // Do the add
            _array.Add(key, value);
            _count++;
        }

        /// <summary>
        /// Removes an item with a specified key
        /// </summary>
        /// <param name="key">The specified key</param>
        /// <returns>True if the item is removed, otherwise false</returns>
        public bool Remove(TKey key)
        {
            bool isRemvoed = _array.Remove(key);
            if (isRemvoed)
            {
                _count--;
            }

            return isRemvoed;
        }

        /// <summary>
        /// Gets or sets the value based on the current key
        /// </summary>
        /// <param name="key">The key of the value to retrieve</param>
        /// <returns>The value associated with the specific key</returns>
        /// <exception cref="ArgumentException">Thrown if key is not found</exception>
        public TValue this[TKey key]
        {
            get
            {
                TValue value;
                if (!_array.TryGetValue(key, out value))
                {
                    throw new ArgumentException("key");
                }

                return value;
            }
            set
            {
                _array.Update(key, value);
            }
        }

        /// <summary>
        /// Tries to find and return the value for a specified key
        /// </summary>
        /// <param name="key">The specified key</param>
        /// <param name="value">The value associated with the specified key</param>
        /// <returns>True if value is found, otherwise false</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _array.TryGetValue(key, out value);
        }

        /// <summary>
        /// Determines if a key in the hash table
        /// </summary>
        /// <param name="key">The specified key</param>
        /// <returns>True if the key is in the hash table, otherwise false</returns>
        public bool ContainsKey(TKey key)
        {
            TValue value;
            return _array.TryGetValue(key, out value);
        }

        /// <summary>
        /// Determines if a specified value is in the hash table
        /// </summary>
        /// <param name="value">The value to find in the hash table</param>
        /// <returns>True if the item is found, otherwise false</returns>
        public bool ContainsValue(TValue value)
        {
            foreach(var foundValue in _array.Values)
            {
                if (value.Equals(foundValue))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets an enumerator for all of the keys in the hash table
        /// </summary>
        public IEnumerable<TKey> Keys
        {
            get
            {
                foreach (var key in _array.Keys)
                {
                    yield return key;
                }
            }
        }

        /// <summary>
        /// Gets an enumerator for all of the value in the hash table
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get
            {
                foreach (var value in _array.Values)
                {
                    yield return value;
                }
            }
        }

        /// <summary>
        /// Clears the hash table of all items
        /// </summary>
        public void Clear()
        {
            _array.Clear();
            _count = 0;
        }

        /// <summary>
        /// Gets the current number of items in the hash table
        /// </summary>
        public int Count
        {
            get
            {
                return _count;
            }
        }

        #region Private Methods

        /// <summary>
        /// Grows the array by twice the current size
        /// </summary>
        private void GrowArray()
        {
            //Create an array twice a large
            HashTableArray<TKey, TValue> largerArray = new HashTableArray<TKey, TValue>(_array.Capacity * 2);

            // Add each existing item to the new array
            foreach (var node in _array.Items)
            {
                largerArray.Add(node.Key, node.Value);
            }

            // Make the larger array the new hash table storage array
            _array = largerArray;

            // Calculate new max items value
            _maxItemsAtCurrentSize = (int)(_array.Capacity * _fillFactor) + 1;
        }

        #endregion

    }
}
