using System;

namespace DataStructures.BinarySearchTree
{
    public class BinaryTreeNode<TNode> : IComparable<TNode> where TNode : IComparable<TNode>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value of the node</param>
        public BinaryTreeNode(TNode value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the Value of the node
        /// </summary>
        public TNode Value { get; private set; }

        /// <summary>
        /// Gets or sets the Left BinaryTreeNode
        /// </summary>
        public BinaryTreeNode<TNode> Left { get; set; }

        /// <summary>
        /// Gets or sets the Right BinaryTreeNode
        /// </summary>
        public BinaryTreeNode<TNode> Right { get; set; }

        /// <summary>
        /// Compares this node to another node
        /// </summary>
        /// <param name="other">The another node value to compare to</param>
        /// <returns>
        /// 1 if this node value is greater than the other node value.
        /// -1 if this node value is less than the other node value
        /// 0 if this node value is equal to the other node value
        /// </returns>
        public int CompareTo(TNode other)
        {
            return Value.CompareTo(other);
        }
    }
}
