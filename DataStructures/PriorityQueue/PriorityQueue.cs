using DataStructures.Heap;
using System;

namespace DataStructures.PriorityQueue
{
    /// <summary>
    /// A queue based on a heap that always dequeues the item with the highest priority.
    /// It's a simple wrapper over a heap
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueue<T> where T: IComparable<T>
    {
        Heap<T> _heap = new Heap<T>();

        /// <summary>
        /// Adds an item to the PriorityQueue
        /// </summary>
        /// <param name="value"></param>
        public void Enqueue(T value)
        {
            _heap.Add(value);
        }

        /// <summary>
        /// Returns the highest priority item in the queue and removes it from the queue
        /// </summary>
        /// <returns>The highest priority item in the queue</returns>
        public T Dequeue()
        {
            return _heap.RemoveMax();
        }

        /// <summary>
        /// Returns the highest priority item in the queue, but does not remove it
        /// </summary>
        /// <returns>The highest priority item in the queue</returns>
        public T Peek()
        {
            return _heap.Peek();
        }

        /// <summary>
        /// Clears all items in the queue
        /// </summary>
        public void Clear()
        {
            _heap.Clear();
        }

        /// <summary>
        /// Gets the number of items in the queue
        /// </summary>
        public int Count
        {
            get
            {
                return _heap.Count;
            }
        }
    }
}
