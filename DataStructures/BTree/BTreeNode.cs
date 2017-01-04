using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.BTree
{
    internal class BTreeNode<T> where T: IComparable<T>
    {
        private readonly List<T> _values;
        private readonly List<BTreeNode<T>> _children;

        #region Constructor

        internal BTreeNode(BTreeNode<T> parent, 
            bool leaf, 
            int minimumDegree, 
            T[] values, 
            BTreeNode<T>[] children)
        {
            ValidatePotentialState(parent, leaf, minimumDegree, values, children);

            Parent = parent;
            Leaf = leaf;
            MinimumDegree = minimumDegree;

            _values = new List<T>(values);
            _children = new List<BTreeNode<T>>(children);
        }

        #endregion

        /// <summary>
        /// Returns true if the node has (2 * T - 1) nodes, otherwise false
        /// </summary>
        internal bool Full
        {
            get
            {
                throw new NotImplementedException();
                // ?? return 2 * MinimumDegree - 1 == _values.Count;
            }
        }

        /// <summary>
        /// True if this is a leaf node, otherwise false
        /// </summary>
        internal bool Leaf { get; private set; }

        /// <summary>
        /// The Values in the node
        /// </summary>
        internal IList<T> Values
        {
            get
            {
                return _values;
            }
        }

        /// <summary>
        /// The Children collection in the node
        /// </summary>
        internal IList<BTreeNode<T>> Children
        {
            get
            {
                return _children;
            }
        }

        /// <summary>
        /// The minimum degree of the tree.
        /// If minimum degree is T then the node must have at least T -1 values,
        /// but no more than 2 * T - 1
        /// </summary>
        internal int MinimumDegree { get; private set; }

        /// <summary>
        /// Gets or sets the Parent node.  null if this is the root node
        /// </summary>
        internal BTreeNode<T> Parent { get; set; }

        /// <summary>
        /// Splits a full child node, pulling split value into current node
        /// </summary>
        /// <param name="indexOfChildToSplit">The index of the child to be split</param>
        /// <example>
        /// Splits a child node by pulling the middle node up from it into the current (parent) node.
        /// 
        ///     [3           9]
        /// [1 2] [4 5 6 7 8] [10 11]
        /// 
        /// Splitting [4 5 6 7 8] would pull 6 up to its parent
        /// 
        ///     [3     6     9]
        /// [1 2] [4 5] [7 8] [10 11]
        /// 
        /// </example>
        internal void SplitFullChild(int indexOfChildToSplit)
        {
            // Get the median index of the child to be split
            int medianIndex = Children[indexOfChildToSplit].Values.Count / 2;

            // Determine if the child being split is a leaf node
            bool isChildLeaf = Children[indexOfChildToSplit].Leaf;

            // Get the value to pull up (in example, would be the value 6)
            T valueToPullUp = Children[indexOfChildToSplit].Values[medianIndex];

            // Build left node (in example, would be [4 5])
            BTreeNode<T> newLeftSide = new BTreeNode<T>(
                this,
                isChildLeaf, 
                MinimumDegree,
                Children[indexOfChildToSplit].Values.Take(medianIndex).ToArray(),
                Children[indexOfChildToSplit].Children.Take(medianIndex + 1).ToArray());

            // Build right node (in example, would be [7 8]
            BTreeNode<T> newRightSide = new BTreeNode<T>(
                this,
                isChildLeaf,
                MinimumDegree,
                Children[indexOfChildToSplit].Values.Skip(medianIndex + 1).ToArray(),
                Children[indexOfChildToSplit].Children.Skip(medianIndex + 1).ToArray());

            // Add the valueToPullUp to the root node (in example, would be adding 6 to [3 9]
            _values.Insert(indexOfChildToSplit, valueToPullUp);

            // Validation
            ValidateValues();

            // Remove the child pointing to old node ([4 5 6 7 8])
            _children.RemoveAt(indexOfChildToSplit);

            // Add the child pointing to new child nodes ([4 5] and [7 8])
            _children.InsertRange(indexOfChildToSplit, new[] { newLeftSide, newRightSide });
        }

        /// <summary>
        /// Splits the root node into a new root and two child nodes
        /// </summary>
        /// <returns>The new root node</returns>
        /// <example>
        /// The root of the tree
        /// 
        /// [1 2 3 4 5]
        /// 
        /// Pull out 3 and split the left and right side and the tree becomes
        /// 
        ///     [3]
        /// [1 2] [4 5]
        /// 
        /// </example>
        internal BTreeNode<T> SplitFullRootNode()
        {
            // Find the index of the value to pull up (in example, 3)
            int medianIndex = Values.Count / 2;

            // Get the value to pull up (in example, 3)
            T rootValue = Values[medianIndex];

            // Build a new empty root node
            BTreeNode<T> newRoot = new BTreeNode<T>(Parent, false, MinimumDegree, new T[0], new BTreeNode<T>[0]);

            // Build the left node (in example, [1 2])
            BTreeNode<T> newLeftNode = new BTreeNode<T>(newRoot, Leaf, MinimumDegree,
                Values.Take(medianIndex).ToArray(),
                Children.Take(medianIndex + 1).ToArray());

            // Build the right node (in example, [4 5])
            BTreeNode<T> newRightNode = new BTreeNode<T>(newRoot, Leaf, MinimumDegree,
                Values.Skip(medianIndex + 1).ToArray(),
                Children.Skip(medianIndex + 1).ToArray());

            // Add the rootValue to the root node (in example, 3)
            newRoot._values.Add(rootValue);

            // Add the left child ([1 2])
            newRoot._children.Add(newLeftNode);

            // Add the right child ([4 5])
            newRoot._children.Add(newRightNode);

            // Return the new root node
            return newRoot;
        }

        /// <summary>
        /// Inserts specified value into a non-full leaf node
        /// </summary>
        /// <param name="value">The value to be inserted</param>
        internal void InsertKeyToLeafNode(T value)
        {
            if (!Leaf) throw new InvalidOperationException("Can not insert into non-leaf node");
            if (Full) throw new InvalidOperationException("Can not insert into full node");

            // Locate insert index
            int index = 0;
            while (index < Values.Count && value.CompareTo(Values[index]) > 0)
            {
                index++;
            }

            // Do the insert
            _values.Insert(index, value);

            // Validation
            ValidateValues();
        }

        /// <summary>
        /// Deletes specified value from a leaf node
        /// </summary>
        /// <param name="value">The value to delete</param>
        /// <returns>True if deleted, otherwise false</returns>
        internal bool DeleteKeyFromLeafNode(T value)
        {
            if (!Leaf) throw new InvalidOperationException("Can not delete from  non-leaf node");

            return _values.Remove(value);
        }

        /// <summary>
        /// Replaces the value at specified index with new value
        /// </summary>
        /// <param name="valueIndex">The index of the item to be replaced</param>
        /// <param name="newValue">The new value</param>
        internal void ReplaceValue(int valueIndex, T newValue)
        {
            _values[valueIndex] = newValue;
            // Validation
            ValidateValues();
        }

        /// <summary>
        /// Pushes a value into the child nodes
        /// </summary>
        /// <param name="valueIndex">The index of the value to be pushed down</param>
        /// <returns>The new child node the the value is pushed down into</returns>
        /// <example>
        /// 
        ///     [3     6]
        /// [1 2] [4 5] [7 8]
        /// 
        ///      Becomes
        ///      
        ///           [6]
        /// [1 2 3 4 5] [7 8]
        /// 
        /// </example>
        internal BTreeNode<T> PushDown(int valueIndex)
        {
            // Create a list that will be the new left child node values
            List<T> values = new List<T>();

            // Add the current left child node (in example [1 2])
           values.AddRange(Children[valueIndex].Values);  // [1 2]

            // Add the item being pushed down (in example [3])
            values.Add(Values[valueIndex]); // [1 2 3]

            // Now add the second child to the values (in example, [4 5])
            values.AddRange(Children[valueIndex].Values);  // [1 2 3 4 5]

            // Build the list of children for the new node to be created (the new left node)
            List<BTreeNode<T>> children = new List<BTreeNode<T>>();
            children.AddRange(Children[valueIndex].Children);
            children.AddRange(Children[valueIndex + 1].Children);

            // Now create the new node based on the values list we created ([1 2 3 4 5])
            BTreeNode<T> newNode = new BTreeNode<T>(this,
                Children[valueIndex].Leaf,
                MinimumDegree,
                Values.ToArray(), // [1 2 3 4 5]
                children.ToArray());

            // Remove the item from the this node that was pushed down (in example, 3)
            _values.RemoveAt(valueIndex);

            // Remove the original items children
            _children.RemoveAt(valueIndex);

            // Assign the new node to be the new child
            _children[valueIndex] = newNode;

            // Return the new node
            return newNode;
        }

        /// <summary>
        /// Adds the specified value to the front of the values, and optional child node
        /// </summary>
        /// <param name="value">The new value</param>
        /// <param name="childNode">The options child node</param>
        internal void AddFront(T value, BTreeNode<T> childNode)
        {
            _values.Insert(0, value);

            // Validate
            ValidateValues();

            if (childNode != null)
            {
                _children.Insert(0, childNode);
            }

        }

        /// <summary>
        /// Adds the specified value to the node, and an optional child node
        /// </summary>
        /// <param name="value">The value to be pushed down</param>
        /// <param name="childNode">The optional child node</param>
        internal void AddEnd(T value, BTreeNode<T> childNode)
        {
            _values.Add(value);

            // Validate
            ValidateValues();

            if (childNode != null)
            {
                _children.Add(childNode);
            }
        }

        /// <summary>
        /// Removes the first value and child, if not a leaf node
        /// </summary>
        internal void RemoveFirst()
        {
            _values.RemoveAt(0);
            if (!Leaf)
            {
                _children.RemoveAt(0);
            }
        }

        /// <summary>
        /// Removes the last value and child, if not a leaf node
        /// </summary>
        internal void RemoveLast()
        {
            _values.RemoveAt(_values.Count -1);
            if (!Leaf)
            {
                _children.RemoveAt(_children.Count -1);
            }
        }

        #region Validation

        /// <summary>
        /// Validates the constructor parameters
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="leaf"></param>
        /// <param name="minimumDegree"></param>
        /// <param name="values"></param>
        /// <param name="children"></param>
        private void ValidatePotentialState(BTreeNode<T> parent, 
            bool leaf, 
            int minimumDegree, 
            T[] values, 
            BTreeNode<T>[] children)
        {
            // Is it the root?
            bool root = parent == null;

            if (values == null)
                throw new ArgumentNullException("values");

            if (children == null)
                throw new ArgumentNullException("children");

            if (minimumDegree < 2)
                throw new ArgumentOutOfRangeException("minimumDegree", "Minimum degree must be greater than 2");

            if (values.Length == 0)
            {
                if (children.Length != 0)
                    throw new ArgumentException("An empty node cannot have children");
            }
            else
            {
                if (values.Length > (2 * minimumDegree - 1))
                    throw new ArgumentException("There are too many values");

                if (!root)
                {
                    if (values.Length < minimumDegree - 1)
                        throw new ArgumentException("Non-root nodes must have a least degree - 1 children");
                }

                if (!leaf && !root)
                {
                    if (values.Length + 1 != children.Length)
                        throw new ArgumentException("There should be one more child than values");
                }
            }
        }

        /// <summary>
        /// Asserts that values are sorted correctly in debug mode
        /// </summary>
        [Conditional("DEBUG")]
        private void ValidateValues()
        {
            if (_values.Count > 1)
            {
                for (int i = 1; i < _values.Count; i++)
                {
                    Debug.Assert(_values[i - 1].CompareTo(_values[i]) < 0);
                }
            }
        }

        #endregion
    }
}
