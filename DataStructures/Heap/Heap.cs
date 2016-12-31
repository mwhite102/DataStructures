using System;

namespace DataStructures.Heap
{
    /// <summary>
    /// A max heap implementation that stores the tree projected into an array
    /// 
    /// To find the children of any node in the 'tree'
    /// Left Child Index = 2 * CurrentIndex + 1
    /// Right Child Index = 2 * CurrentIndex + 2
    /// 
    /// To find the parent of a node
    /// Parent = (index - 1) / 2
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Heap<T> where T: IComparable<T>
    {
        const int DEFAULT_LENGTH = 100;
        T[] _items;

        int _count;

        public Heap()
            : this(DEFAULT_LENGTH)
        {
        }

        public Heap(int length)
        {
            _items = new T[length];
            _count = 0;
        }

        /// <summary>
        /// Adds an item to the heap
        /// </summary>
        /// <param name="value">The value to be added</param>
        public void Add(T value)
        {
            // Do we need to grow the array?
            if (_count >= _items.Length)
            {
                GrowArray();
            }

            // Add the item to the end of the array
            _items[_count] = value;

            // Now move it into the correct position
            int index = _count;
            while (index > 0 && _items[index].CompareTo(_items[Parent(index)]) > 0)
            {
                Swap(index, Parent(index));
                index = Parent(index);
            }

            // Increment the count
            _count++;
        }

        /// <summary>
        /// Peeks the maximum value in the heap
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            if (Count > 0)
            {
                return _items[0];
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Removes the largest value from the heap
        /// </summary>
        /// <returns>The largest value from the heap</returns>
        public T RemoveMax()
        {
            if (_count <= 0)
            {
                throw new InvalidOperationException("The heap is empty");
            }

            // The max value is always the first item
            T max = _items[0];

            _items[0] = _items[_count - 1];
            _count--;

            int index = 0;

            while (index < _count)
            {
                // Get left and right child index
                int left = (2 * index) + 1;
                int right = (2 * index) + 2;

                // Are we still in the heap?
                if (left >= _count)
                {
                    break;
                }

                // Find the largest child 
                int maxChildIndex = IndexOfMaxChild(left, right);

                if (_items[index].CompareTo(_items[maxChildIndex]) > 0)
                {
                    // Current item is larger than its children
                    break;
                }

                Swap(index, maxChildIndex);
                index = maxChildIndex;
            }

            return max;
        }

        /// <summary>
        /// Gets the number of items in the heap
        /// </summary>
        public int Count
        {
            get
            {
                return _count;
            }
        }

        /// <summary>
        /// Clears the heap of all items
        /// </summary>
        public void Clear()
        {
            _count = 0;
            _items = new T[DEFAULT_LENGTH];
        }

        #region Private Methods

        private void GrowArray()
        {
            T[] newItems = new T[_items.Length * 2];
            for (int i = 0; i < _items.Length; i++)
            {
                newItems[i] = _items[i];
            }
            _items = newItems;
        }

        private int IndexOfMaxChild(int left, int right)
        {
            // Find index of child with largest value
            int maxChildIndex = -1;
            if (right >= _count)
            {
                // No right child
                maxChildIndex = left;
            }
            else
            {
                if (_items[left].CompareTo(_items[right]) > 0)
                {
                    maxChildIndex = left;
                }
                else
                {
                    maxChildIndex = right;
                }
            }

            return maxChildIndex;
        }

        private int Parent(int index)
        {
            return (index - 1) / 2;
        }

        private void Swap(int left, int right)
        {
            T temp = _items[left];
            _items[left] = _items[right];
            _items[right] = temp;
        }

        #endregion
    }
}
