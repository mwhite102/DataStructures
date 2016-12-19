using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.DoublyLinkedList
{
    public class DoublyLinkedList<T> : ICollection<T>
    {
        private DoublyLinkedListNode<T> _head;
        private DoublyLinkedListNode<T> _tail;

        /// <summary>
        /// Gets the number of items in the list
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the first node in the list
        /// </summary>
        public DoublyLinkedListNode<T> Head
        {
            get { return _head; }
        }

        /// <summary>
        /// Gets the last node in the list
        /// </summary>
        public DoublyLinkedListNode<T> Tail
        {
            get { return _tail; }
        }

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
            AddLast(item);
        }

        /// <summary>
        /// Adds an item tot he beginning of the list
        /// </summary>
        /// <param name="item">The item to add to the beginning of the list</param>
        public void AddFirst(T item)
        {
            // Create a new node
            DoublyLinkedListNode<T> node = new DoublyLinkedListNode<T>(item);

            // Save the current head to a temp value
            DoublyLinkedListNode<T> temp = _head;

            // Set the new node as the head
            _head = node;

            // Point the node Next property to the previous head node
            _head.Next = temp;

            if (Count == 0)
            {
                // First item added to the list
                // node should be both head and tail
                _tail = _head;
            }
            else
            {
                // Set the Previous property on the temp node to the new head
                temp.Previous = _head;
            }

            // Increment the count
            Count++;
        }

        /// <summary>
        /// Adds an item to the end of the list
        /// </summary>
        /// <param name="item">The item to add to the end of the list</param>
        public void AddLast(T item)
        {
            // Create a new node
            DoublyLinkedListNode<T> node = new DoublyLinkedListNode<T>(item);

            if (Count == 0)
            {
                // The list is empty, the new node will be head and the tail
                _head = node;
            }
            else
            {
                // Set the current tails Next property to the new node
                _tail.Next = node;
                // Set the nodes Previous property to the current tail
                node.Previous = _tail;    
            }

            // Make the new node the tail
            _tail = node;

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
            DoublyLinkedListNode<T> current = _head;

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

            DoublyLinkedListNode<T> current = _head;
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
            DoublyLinkedListNode<T> previous = null;
            DoublyLinkedListNode<T> current = _head;

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
                        else
                        {
                            // Update the current's next node's Previous property
                            // to point to the previous node (almost confusing)
                            current.Next.Previous = previous;
                        }

                        // The item has been removed, decrement the count
                        Count--;
                    }
                    else
                    {
                        // The node to be removed is the head
                        RemoveFirst();
                    }

                    return true;
                }

                // Next node
                previous = current;
                current = current.Next;
            }

            // No matching item found
            return false;
        }

        /// <summary>
        /// Removes the first item in the list
        /// </summary>
        public void RemoveFirst()
        {
            if (Count != 0)
            {
                // Remove the head node
                _head = _head.Next;
                // Decrement the count
                Count--;

                if (Count == 0)
                {
                    // List is now empty, null the tail
                    _tail = null;
                }
                else
                {
                    // null the heads previous property
                    _head.Previous = null;
                }
            }
        }

        public void RemoveLast()
        {
            if (Count != 0)
            {
                if (Count == 1)
                {
                    // Removing only node in list
                    // null the head and tail
                    _head = null;
                    _tail = null;
                }
                else
                { 
                    // Set the preceding node as the tail
                    _tail = _tail.Previous;
                    // Set the tail's Next property to null
                    _tail.Next = null;
                }

                // Decrement the count
                Count--;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            DoublyLinkedListNode<T> current = _head;
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
