using DataStructures.DoublyLinkedList;
using System;

namespace DataStructures.Queue
{
    public class AQueue<T>
    {
        // Use a LinkedList<T> as a backing store
        DoublyLinkedList<T> _items = new DoublyLinkedList<T>();

        /// <summary>
        /// Adds an item to the queue
        /// </summary>
        /// <param name="value"></param>
        public void Enqueue(T value)
        {
            _items.AddFirst(value);
        }

        /// <summary>
        /// Removes an items from the queue
        /// </summary>
        /// <returns>The next item in the queue</returns>
        public T Dequeue()
        {
            if (Count == 0) throw new InvalidOperationException();

            T value = _items.Tail.Value;
            _items.RemoveLast();
            return value;
        }

        /// <summary>
        /// Returns an item from the queue, but does not remove it
        /// </summary>
        /// <returns>The next item in the queue, but does not remove it</returns>
        public T Peek()
        {
            if (Count == 0) throw new InvalidOperationException();
            return _items.Tail.Value;
        }

        /// <summary>
        /// Gets the number of items in the queue
        /// </summary>
        public int Count
        {
            get
            {
                return _items.Count;
            }
        }
    }
}
