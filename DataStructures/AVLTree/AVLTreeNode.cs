using System;

namespace DataStructures.AVLTree
{
    enum TreeState
    {
        Balanced,
        LeftHeavy,
        RightHeavy
    }

    /// <summary>
    /// Represents a node in an AVL tree
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    public class AVLTreeNode<TNode> where TNode: IComparable<TNode>
    {
        AVLTree<TNode> _tree;
        AVLTreeNode<TNode> _left;
        AVLTreeNode<TNode> _right;

        /// <summary>
        /// AVLTreeNode constructor
        /// </summary>
        /// <param name="value">The value of the node</param>
        /// <param name="parent">The parent node of the node</param>
        /// <param name="tree">The tree the node is contained in</param>
        public AVLTreeNode(TNode value, AVLTreeNode<TNode> parent, AVLTree<TNode> tree)
        {
            Value = value;
            Parent = parent;
            _tree = tree;
        }

        /// <summary>
        /// Gets this node's left child node
        /// </summary>
        public AVLTreeNode<TNode> Left
        {
            get
            {
                return _left;
            }
            internal set
            {
                _left = value;
                if (_left != null)
                {
                    _left.Parent = this;
                }
            }
        }

        /// <summary>
        /// Gets this node's right child node
        /// </summary>
        public AVLTreeNode<TNode> Right
        {
            get
            {
                return _right;
            }
            internal set
            {
                _right = value;
                if (_right != null)
                {
                    _right.Parent = this;
                }
            }
        }

        /// <summary>
        /// Gets this node's parent node
        /// </summary>
        public AVLTreeNode<TNode> Parent { get; internal set; }

        /// <summary>
        /// Gets the value of the node
        /// </summary>
        public TNode Value { get; private set; }

        /// <summary>
        /// Compares this node to the specified node
        /// </summary>
        /// <param name="other">The node to compare this node to</param>
        /// <returns>1 if instance value greater the other value, -1 if less, 0 if equal</returns>
        public int CompareTo(TNode other)
        {
            return Value.CompareTo(other);
        }

        #region Private Methods

        /// <summary>
        /// Determines if rotation, and what type of rotation is required
        /// </summary>
        /// <remarks>
        /// The tree is unbalanced if one side has a height greater than one than the other side
        /// </remarks>
        internal void Balance()
        {
            if (State == TreeState.RightHeavy)
            {
                if (Right != null && Right.BalanceFactor < 0)
                {
                    LeftRightRotation();
                }
                else
                {
                    LeftRotation();
                }
            }
            else if (State == TreeState.LeftHeavy)
            {
                if (Left != null && Left.BalanceFactor > 0)
                {
                    RightLeftRotation();
                }
                else
                {
                    RightRotation();
                }
            }
        }

        /// <summary>
        /// Recursive method to determine the maximum child height of a node
        /// </summary>
        /// <param name="node">The node to calculate height for</param>
        /// <returns>the maximum child height of a node</returns>
        private int MaxChildHeight(AVLTreeNode<TNode> node)
        {
            if (node != null)
            {
                return 1 + Math.Max(MaxChildHeight(node.Left), MaxChildHeight(node.Right));
            }

            return 0;
        }

        /// <summary>
        /// Gets the left height of the tree
        /// </summary>
        private int LeftHeight
        {
            get
            {
                return MaxChildHeight(Left);
            }
        }

        /// <summary>
        /// Gets the right height of the tree
        /// </summary>
        private int RightHeight
        {
            get
            {
                return MaxChildHeight(Right);
            }
        }

        /// <summary>
        /// Determines if the tree is left heavy, right heavy, or balanced
        /// </summary>
        private TreeState State
        {
            get
            {
                if (LeftHeight - RightHeight > 1)
                {
                    return TreeState.LeftHeavy;
                }

                if (RightHeight - LeftHeight > 1)
                {
                    return TreeState.RightHeavy;
                }

                return TreeState.Balanced;
            }
        }

        /// <summary>
        /// Gets the BalanceFactor
        /// </summary>
        private int BalanceFactor
        {
            get
            {
                return RightHeight - LeftHeight;
            }
        }

        /// <summary>
        /// Performs a left rotation
        /// </summary>
        private void LeftRotation()
        {
            ///     a (this)                   b
            ///      \                        / \
            ///       b          =>          a   c
            ///        \
            ///         c

            AVLTreeNode<TNode> newRoot = Right;

            // Replace current root with new root
            ReplaceRoot(newRoot);

            // Make the right node the new root's left node
            Right = newRoot.Left;

            // The new root takes this as its left node
            newRoot.Left = this;
        }

        /// <summary>
        /// Performs a right rotation
        /// </summary>
        private void RightRotation()
        {
            ///         c (this)                 b
            ///        /                        / \
            ///       b          =>            a   c
            ///      /
            ///     a

            AVLTreeNode<TNode> newRoot = Left;

            // Replace current root with new root
            ReplaceRoot(newRoot);

            // Make the left node the new root's right node
            Left = newRoot.Right;

            // The new root takes this as its right node
            newRoot.Right = this;
        }

        /// <summary>
        /// Performs a left right rotation
        /// </summary>
        private void LeftRightRotation()
        {
            Right.RightRotation();
            LeftRotation();
        }

        /// <summary>
        /// Performs a left right rotation
        /// </summary>
        private void RightLeftRotation()
        {
            Left.LeftRotation();
            RightRotation();
        }

        /// <summary>
        /// Replaces the current root with a new root
        /// </summary>
        /// <param name="newRoot">The node to become the new root node</param>
        private void ReplaceRoot(AVLTreeNode<TNode> newRoot)
        {
            if (this.Parent != null)
            {
                if (this.Parent.Left == this)
                {
                    this.Parent.Left = newRoot;
                }
                else if (this.Parent.Right == this)
                {
                    this.Parent.Right = newRoot;
                }
            }
            else
            {
                _tree.Head = newRoot;
            }

            newRoot.Parent = this.Parent;
            this.Parent = newRoot;
        }

        #endregion
    }
}
