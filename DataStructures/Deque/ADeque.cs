using DataStructures.DoublyLinkedList;
using System;

namespace DataStructures.Deque
{
    public class ADeque<T>
    {
        // Backed by a Doubly Linked List
        private DoublyLinkedList<T> _Items = new DoublyLinkedList<T>();

        /// <summary>
        /// Adds an item to the head of the queue
        /// </summary>
        /// <param name="value"></param>
        public void EnqueueFirst(T value)
        {
            _Items.AddFirst(value);
        }

        /// <summary>
        /// Adds an items to the tail of the queue
        /// </summary>
        /// <param name="value"></param>
        public void EnqueueLast(T value)
        {
            _Items.AddLast(value);
        }

        /// <summary>
        /// Removes an item from the head of the queue
        /// </summary>
        /// <returns>The first item from the head of the queue</returns>
        public T DequeueFirst()
        {
            if (Count == 0) throw new InvalidOperationException();

            T value = _Items.Head.Value;
            _Items.RemoveFirst();
            return value;
        }

        /// <summary>
        /// Removes an item from the tail of the queue
        /// </summary>
        /// <returns>The first item from the tail of the queue</returns>
        public T DequeueLast()
        {
            if (Count == 0) throw new InvalidOperationException();

            T value = _Items.Tail.Value;
            _Items.RemoveLast();
            return value;
        }

        /// <summary>
        /// Returns the first item from the head of the queue, but does not remove it
        /// </summary>
        /// <returns>The first item from the head of the queue</returns>
        public T PeekFirst()
        {
            if (Count == 0) throw new InvalidOperationException();

            return _Items.Head.Value;
        }

        /// <summary>
        /// Returns the first item from the tail of the queue, but does not remove it
        /// </summary>
        /// <returns>The first item from the tail of the queue</returns>
        public T PeekLast()
        {
            if (Count == 0) throw new InvalidOperationException();

            return _Items.Tail.Value;
        }

        /// <summary>
        /// The number of items in the Deque
        /// </summary>
        public int Count
        {
            get
            {
                return _Items.Count;
            }
        }
    }
}
