using DataStructures.DoublyLinkedList;
using System;

namespace DataStructures.Stack
{
    public class AStack<T>
    {
        // Use a Doubly LinkedList as a backing store
        DoublyLinkedList<T> _items = new DoublyLinkedList<T>();

        /// <summary>
        /// Pushes and item onto the stack
        /// </summary>
        /// <param name="value">The value to be pushed on the stack</param>
        public void Push(T value)
        {
            _items.AddLast(value);
        }

        /// <summary>
        /// Pops and item off of the top of the stack
        /// </summary>
        /// <returns>The item popped off of the stack</returns>
        public T Pop()
        {
            if (_items.Count == 0) throw new InvalidOperationException();

            T result = _items.Tail.Value;
            _items.RemoveLast();

            return result;
        }

        /// <summary>
        /// Returns an item on top of the stack, but doesn't remove it
        /// </summary>
        /// <returns>The item on top of the stack</returns>
        public T Peek()
        {
            if (_items.Count == 0) throw new InvalidOperationException();

            return _items.Tail.Value;
        }

        /// <summary>
        /// Gets the number of items in the stack
        /// </summary>
        public int Count
        {
            get { return _items.Count; }
        }
    }
}
