using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.SkipList
{
    public class SkipList<T> : ICollection<T> where T : IComparable<T>
    {
        // Determines the random height of the node links
        private readonly Random _random = new Random();

        // Non-data node which starts the list
        private SkipListNode<T> _head;

        // Always one level of depth (base list)
        private int _levels = 1;

        // Number of items in the list
        private int _count = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public SkipList()
        {
            // Init the head
            _head = new SkipListNode<T>(default(T), 32 + 1);
        }

        #region ICollection Implementation

        /// <summary>
        /// Gets the number of items in the collection
        /// </summary>
        public int Count
        {
            get
            {
                return _count;
            }
        }

        /// <summary>
        /// Always returns false;
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Adds a new SkipListNode to the list
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            int level = PickRandomLevel();

            SkipListNode<T> newNode = new SkipListNode<T>(item, level + 1);
            SkipListNode<T> current = _head;

            for (int i = _levels -1; i >= 0; i--)
            {
                while(current.Next[i] != null)
                {
                    if (current.Next[i].Value.CompareTo(item) > 0)
                    {
                        break;
                    }

                    current = current.Next[i];
                }

                if (i <= level)
                {
                    // Add the item
                    // Example: Adding a "5" to list 3 -> 4 -> 6 -> 7
                    // The current node is "4"

                    // Link the new node "5" to to existing node "6"
                    newNode.Next[i] = current.Next[i];

                    // Insert the new "5" node after the current node
                    current.Next[i] = newNode;
                }
            }

            _count++;
        }

        /// <summary>
        /// Clears the list
        /// </summary>
        public void Clear()
        {
            _head = new SkipListNode<T>(default(T), 32 + 1);
            _count = 0;
        }

        /// <summary>
        /// Determines if an items exists in the list
        /// </summary>
        /// <param name="item">The item to find in the list</param>
        /// <returns>True if the item is found, false if it is not found</returns>
        public bool Contains(T item)
        {
            SkipListNode<T> current = _head;
            for (int i = _levels - 1; i >= 0; i--)
            {
                while (current.Next[i] != null)
                {
                    int cmp = current.Next[i].Value.CompareTo(item);

                    if (cmp > 0)
                    {
                        // The value is too large, go down a level
                        break;
                    }

                    if (cmp == 0)
                    {
                        // We have a match
                        return true;
                    }

                    current = current.Next[i];
                }
            }

            return false;
        }

        /// <summary>
        /// Copies this collection into a new array starting at the specified array index
        /// </summary>
        /// <param name="array">The array to copy the items into</param>
        /// <param name="arrayIndex">The starting point to copy items to</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) throw new ArithmeticException("array");

            int offset = 0;
            foreach(T item in this)
            {
                array[arrayIndex + offset++] = item;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            SkipListNode<T> current = _head.Next[0];
            while (current != null)
            {
                yield return current.Value;
                current = current.Next[0];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Removes the first node matching the specified item
        /// </summary>
        /// <param name="item">The item to be removed</param>
        /// <returns>True if found and removed, otherwise, returns false</returns>
        public bool Remove(T item)
        {
            SkipListNode<T> current = _head;

            bool removed = false;

            // Walk down each level in the list making big jumps
            for (int level = _levels - 1; level >= 0; level--)
            {
                while(current.Next[level] != null)
                {
                    if (current.Next[level].Value.CompareTo(item) == 0)
                    {
                        // We have a match, remove it
                        current.Next[level] = current.Next[level].Next[level];
                        removed = true;

                        break;
                    }

                    // If we went too far, go down a level
                    if (current.Next[level].Value.CompareTo(item) > 0)
                    {
                        break;
                    }

                    current = current.Next[level];
                }
            }

            if (removed)
            {
                _count--;
            }

            return removed;
        }
        
        #endregion

        #region Private Methods

        /// <summary>
        /// Returns a random level value.
        /// </summary>
        /// <returns>The random level value</returns>
        private int PickRandomLevel()
        {
            int rand = _random.Next();
            int level = 0;

            while ((rand & 1) == 1)
            {
                if (level == _levels)
                {
                    _levels++;
                    break;
                }

                // >>= is right shift assignment
                // is equivalent to "rand = rand >> 1"
                rand >>= 1;
                level++;
            }

            return level;
        }

        #endregion
    }
}
