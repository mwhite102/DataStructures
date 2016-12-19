using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.SinglyLinkedList
{
    public class SinglyLinkedList<T> : ICollection<T>
    {
        private SinglyLinkedListNode<T> _head;
        private SinglyLinkedListNode<T> _tail;

        /// <summary>
        /// Gets the number of items in the list
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Always returns false
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Adds an item to the end of the list
        /// </summary>
        /// <param name="item">The item to add to the end of the list</param>
        public void Add(T item)
        {
            // Create a new node
            SinglyLinkedListNode<T> node = new SinglyLinkedListNode<T>(item);

            if (_head == null)
            {
                // The list is empty, the new node will be head and the tail
                _head = node;
                _tail = node;
            }
            else
            {
                // Set the current tails Next property to the new node
                _tail.Next = node;
                // Make the new node the tail
                _tail = node;
            }

            // Increment the Count
            Count++;
        }

        /// <summary>
        /// Removes all items from the list
        /// </summary>
        public void Clear()
        {
            _head = null;
            _tail = null;
            Count = 0;
        }

        /// <summary>
        /// Determines if the item is in the list
        /// </summary>
        /// <param name="item">The item to find in the list</param>
        /// <returns>True if the item is found</returns>
        public bool Contains(T item)
        {
            SinglyLinkedListNode<T> current = _head;

            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    // Found the item
                    return true;
                }

                // Next node
                current = current.Next;
            }

            return false;
        }

        /// <summary>
        /// Copies all items into the given array
        /// </summary>
        /// <param name="array">The array to copy items into</param>
        /// <param name="arrayIndex">The start index in the array to begin copying items</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException("array");

            SinglyLinkedListNode<T> current = _head;
            while (current != null)
            {
                array[arrayIndex++] = current.Value;
                current = current.Next;
            }
        }

        /// <summary>
        /// Removes the item from the list if it is found
        /// </summary>
        /// <param name="item">The item to be removed</param>
        /// <returns>True if the item is removed</returns>
        public bool Remove(T item)
        {
            SinglyLinkedListNode<T> previous = null;
            SinglyLinkedListNode<T> current = _head;

            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    // Found match
                    if (previous != null)
                    {
                        // It's not the first node (previous would be null if it was)
                        // Do the remove
                        previous.Next = current.Next;

                        if (current.Next == null)
                        {
                            // The current node was the tail, so set the previous as the new tail
                            _tail = previous;
                        }
                    }
                    else
                    {
                        // The node to be removed is the head
                        // Set the head.Next as the new head
                        _head = _head.Next;

                        if (_head == null)
                        {
                            // The list is now empty. Null the tail
                            _tail = null;
                        }
                    }

                    // The item has been removed, decrement the count
                    Count--;

                    return true;
                }

                // Next node
                previous = current;
                current = current.Next;
            }

            // No matching item found
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            SinglyLinkedListNode<T> current = _head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

    }
}
