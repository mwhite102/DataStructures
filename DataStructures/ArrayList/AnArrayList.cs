using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.ArrayList
{
    public class AnArrayList<T> : IList<T>
    {
        /// <summary>
        /// Internal Array to store items
        /// </summary>
        private T[] _items;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AnArrayList() 
            : this(0)
        {
        }

        /// <summary>
        /// Overloaded Constructor
        /// </summary>
        /// <param name="length"></param>
        public AnArrayList(int length)
        {
            if (length < 0) throw new ArgumentException("length");

            // Initialize the internal array
            _items = new T[length];
        }

        /// <summary>
        /// Gets or sets the value at the given index
        /// </summary>
        /// <param name="index">The index of the item</param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                if (index < Count)
                {
                    return _items[index];
                }
                throw new IndexOutOfRangeException();
            }

            set
            {
                if (index < Count)
                {
                    _items[index] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        public int Count { get; private set; }

        /// <summary>
        /// Always returns false
        /// </summary>
        public bool IsReadOnly
        {
            get{ return false; }
        }

        /// <summary>
        /// Add an item to the end of the array
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            // If the current length of the internal array is equal
            // to the current Count, grow the array
            if (_items.Length == Count)
            {
                GrowArray();
            }

            _items[Count++] = item;
        }

        /// <summary>
        /// Removes all items from the array
        /// </summary>
        public void Clear()
        {
            _items = new T[0];
            Count = 0;
        }

        /// <summary>
        /// Determines if a specified item is in the array
        /// </summary>
        /// <param name="item">The specified item</param>
        /// <returns>True if the array contains the item.  Otherwise, false</returns>
        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        /// <summary>
        /// Copies the content of the array to a specified array
        /// </summary>
        /// <param name="array">The array to copy items to</param>
        /// <param name="arrayIndex">The starting point to copy</param>
        /// <remarks>
        /// Note that we don't use the internal _items.Copy as this would
        /// copy the entire capacity of the array. We only want to copy up
        /// to the value of Count
        /// </remarks>
        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(
                _items,         // sourceArray
                0,              // sourceIndex
                array,          // destinationArray
                arrayIndex,     // destinationIndex
                Count           // length
                );         
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns the first index in the array of the item specified
        /// </summary>
        /// <param name="item">The item to find</param>
        /// <returns>The first index of the item, or -1 if not found</returns>
        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_items[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Inserts an item at the specified index
        /// </summary>
        /// <param name="index">The index in the array to insert the item</param>
        /// <param name="item">The item to be inserted in the array</param>
        public void Insert(int index, T item)
        {
            if (index >= Count) throw new IndexOutOfRangeException();

            // If the current length of the internal array is equal
            // to the current Count, grow the array
            if (_items.Length == this.Count)
            {
                GrowArray();
            }

            // Shift all items that are beyond the insertion point to the right
            // by using Array.Copy
            Array.Copy(
                _items,         // sourceArray
                index,          // sourceIndex
                _items,         // destinationArray
                index + 1,      // destinationIndex 
                Count - index   // length
                );

            // Add the item
            _items[index] = item;

            // Increment the count
            Count++;
        }

        /// <summary>
        /// Removes the first item in the collection that matches in input item
        /// </summary>
        /// <param name="item">The item to be removed</param>
        /// <returns>True if the item is found and removed</returns>
        public bool Remove(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_items[i].Equals(item))
                {
                    RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes an item at a specific index
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            if (index > Count) throw new IndexOutOfRangeException();

            // Shift all items to the right of the index to the left
            // by using Array.Copy
            int sourceIndex = index + 1;
            if (sourceIndex < Count)
            {
                Array.Copy(
                _items,                 // sourceArray
                sourceIndex,            // sourceIndex
                _items,                 // destinationArray
                index,                  // destinationIndex 
                Count - sourceIndex     // length
                );
            }

            // Decrement the count
            Count--;
        }
                
        /// <summary>
        /// Grows array by doubling the array size
        /// </summary>
        private void GrowArray()
        {
            // Double the array size
            int newLength = _items.Length == 0 ? 16 : _items.Length << 1;

            // Alternatively you can use an option with slower growth curve
            // int newLength = (_items.Length * 3) / 2 + 1;

            T[] newArray = new T[newLength];

            _items.CopyTo(newArray, 0);

            _items = newArray;
        }
    }
}
