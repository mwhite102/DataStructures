using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Sets
{
    public class Set<T> : IEnumerable<T> where T: IComparable<T>
    {
        private readonly List<T> _items = new List<T>();

        public Set()
        {

        }

        public Set(IEnumerable<T> items)
        {
            AddRange(items);
        }
        
        /// <summary>
        /// Adds an item the set
        /// </summary>
        /// <param name="item">The item to be added to the set</param>
        public void Add(T item)
        {
            // Do not allow duplicates
            if (_items.Contains(item)) throw new InvalidOperationException("Item already exists");

            _items.Add(item);
        }

        /// <summary>
        /// Adds a range of items to the set
        /// </summary>
        /// <param name="items">The items to be added to the set</param>
        public void AddRange(IEnumerable<T> items)
        {
            foreach(var item in items)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Removes an item from the set
        /// </summary>
        /// <param name="item">The item to be removed from the set</param>
        /// <returns>True if the item is removed, false if is not found</returns>
        public bool Remove(T item)
        {
            return _items.Remove(item);
        }

        /// <summary>
        /// Determines if an item is in the set
        /// </summary>
        /// <param name="item">The item to find in the set</param>
        /// <returns>True if the item is in the set, false if it is not</returns>
        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        /// <summary>
        /// Gets how many items are in the set
        /// </summary>
        public int Count
        {
            get
            {
                return _items.Count;
            }
        }

        /// <summary>
        /// Produces a set that contains all items in both sets"/> 
        /// </summary>
        /// <param name="other">The set of items to union this set with</param>
        /// <returns>A set that contains all items in both sets</returns>
        public Set<T> Union(Set<T> other)
        {
            Set<T> result = new Set<T>(_items);

            foreach (var item in other._items)
            {
                if (!Contains(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        /// <summary>
        /// Produces a set that contains the common members in both sets
        /// </summary>
        /// <param name="other">The set of items to intersect this set with</param>
        /// <returns>A set that contains the common members in both sets</returns>
        public Set<T> Intersection(Set<T> other)
        {
            Set<T> result = new Set<T>();

            foreach (var item in other._items)
            {
                if (Contains(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        /// <summary>
        /// Produces a set that contains all the items in the first set, 
        /// but do not exist in the second set
        /// </summary>
        /// <param name="other">The set of items to difference this set with</param>
        /// <returns>A that contains all of the items in the first set, but 
        /// do not exist in the second set</returns>
        public Set<T> Difference(Set<T> other)
        {
            // Create a result with all the current items in this set
            Set<T> result = new Set<T>(_items);

            foreach(var item in other._items)
            {
                result.Remove(item);
            }

            return result;
        }

        /// <summary>
        /// Produces a set that is the result of the symmetric difference of the current
        /// and input sets
        /// </summary>
        /// <param name="other">The other set to compare to this one</param>
        /// <returns>A set that is the result of the symmetric difference of the current
        /// and input sets</returns>
        public Set<T> SymmetricalDifference(Set<T> other)
        {
            Set<T> union = Union(other);
            Set<T> intersection = Intersection(other);
            return union.Difference(intersection);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
