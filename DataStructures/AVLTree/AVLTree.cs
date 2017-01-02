using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.AVLTree
{
    /// <summary>
    /// An AVL Tree implementation    
    /// </summary>
    /// <remarks>
    /// Rules for AVL Tree
    /// --------------------------------------------------------------------------
    /// 1. Each node will have 0, 1, or 2 child nodes
    /// 2. Smaller values than the current node go to the left
    /// 3. Equal or larger values than the current node go to the right
    /// 4. The height of the left and right nodes will never differ by more that 1
    /// --------------------------------------------------------------------------
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public class AVLTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        public AVLTreeNode<T> Head { get; internal set; }

        /// <summary>
        /// Adds a value to the tree and ensures the tree is balanced
        /// </summary>
        /// <param name="value"></param>
        public void Add(T value)
        {
            if (Head == null)
            {
                // Tree is empty, set value as the head node
                Head = new AVLTreeNode<T>(value, null, this);
            }
            else
            {
                // Tree is not empty 
                AddTo(Head, value);
            }

            Count++;
        }

        /// <summary>
        /// Determines if a value is in the tree
        /// </summary>
        /// <param name="value">The value to find in the tree</param>
        /// <returns>True if value is found, false if not</returns>
        public bool Contains(T value)
        {
            return Find(value) != null;
        }

        /// <summary>
        /// Removes the specified value from the tree if found
        /// </summary>
        /// <param name="value">The node value to find</param>
        /// <returns>True if found and removed, otherwise, false</returns>
        public bool Remove(T value)
        {
            AVLTreeNode<T> current = Find(value);

            if (current == null)
            {
                // Not found
                return false;
            }

            // Grab the parent of the node to be removed
            AVLTreeNode<T> treeToBalance = current.Parent;

            // Decrement the count
            Count--;

            // If Current has no right child, current's left node replaces current
            if (current.Right == null)
            {
                if (current.Parent == null)
                {
                    Head = current.Left;
                    if (Head != null)
                    {
                        Head.Parent = null;
                    }
                }
                else
                {
                    int result = current.Parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // If parent value is greater than current value,
                        // make the current left child a left child of parent
                        current.Parent.Left = current.Left;
                    }
                    else if (result < 0)
                    {
                        // If parent value is less than the current value,
                        // make the current left child a right child of parent
                        current.Parent.Right = current.Left;
                    }
                }
            }
            else if (current.Right.Left == null)
            {
                // Current's right child has no left child, current's right child replaces current
                current.Right.Left = current.Left;

                if (current.Parent == null)
                {
                    Head = current.Right;
                    if (Head != null)
                    {
                        Head.Parent = null;
                    }
                }
                else
                {
                    int result = current.Parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // If parent value is greater than the current value,
                        // make the current right child a left child of parent
                        current.Parent.Left = current.Right;
                    }
                    else if (result < 0)
                    {
                        // If parent value is less than the current value
                        // make the current right child a right child of parent
                        current.Parent.Right = current.Right;
                    }
                }
            }
            else
            {
                // Current's right child has a left child
                // Replace current with current's right child's leftmost child

                // Get the right's leftmost child
                AVLTreeNode<T> leftmost = current.Right.Left;

                while (leftmost.Left != null)
                {
                    leftmost = leftmost.Left;
                }

                // The parent's left subtree becomes the leftmost's right subtree
                leftmost.Parent.Left = leftmost.Right;

                // Assign leftmost's child nodes
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;
                
                if (current.Parent == null)
                {
                    Head = leftmost;
                    if (Head != null)
                    {
                        Head.Parent = null;
                    }
                }
                else
                {
                    int result = current.Parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // If parent value is greater than current value
                        // make leftmost the parent's left child
                        current.Parent.Left = leftmost;
                    }
                    else if (result < 0)
                    {
                        // If parent value is less than current value
                        // make leftmost the the parent's right child
                        current.Parent.Right = leftmost;
                    }
                }
            }

            // Rebalance
            if (treeToBalance != null)
            {
                treeToBalance.Balance();
            }
            else
            {
                if (Head != null)
                {
                    Head.Balance();
                }
            }

            return true;
        }

        public void Clear()
        {
            Head = null;
            Count = 0;
        }

        public int Count { get; private set; }

        /// <summary>
        /// Enumerates the values in the binary tree in inorder traversal order
        /// </summary>
        /// <returns>An enumerator</returns>
        public IEnumerator<T> InOrderTraversal()
        {
            if (Head != null)
            {
                Stack<AVLTreeNode<T>> stack = new Stack<AVLTreeNode<T>>();

                AVLTreeNode<T> current = Head;

                bool goLeftNext = true;

                // Start by pushing Head onto the stack
                stack.Push(current);

                while (stack.Count > 0)
                {
                    // If going left
                    if (goLeftNext)
                    {
                        // Push all but leftmost node on stack
                        // We'll yield the leftmost after this block
                        while (current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }

                    // Inorder is left => yield -> right
                    yield return current.Value;

                    // If we can go right, do it
                    if (current.Right != null)
                    {
                        current = current.Right;

                        // Once we've gone right once, we need to start left again
                        goLeftNext = true;
                    }
                    else
                    {
                        // if we can't go right pop off the parent node
                        // so we can process it and then go to its right node
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }

        /// <summary>
        /// Gets an enumerator that performs inorder traversal of the tree
        /// </summary>
        /// <returns>An enumerator that performs inorder traversal of the tree</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region private methods

        /// <summary>
        /// Adds a value, finding the correct place to insert it
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        private void AddTo(AVLTreeNode<T> node, T value)
        {
            // Is value less than the current node value?
            if (value.CompareTo(node.Value) < 0)
            {
                // If the left node is null, make this value the left node
                if (node.Left == null)
                {
                    node.Left = new AVLTreeNode<T>(value, node, this);
                }
                else
                {
                    // Add the value to the left node recursively
                    AddTo(node.Left, value);
                }
            }
            else
            {
                // value is equal to or greater than the current value
                // If the right node in null, make this value the right node
                if (node.Right == null)
                {
                    node.Right = new AVLTreeNode<T>(value, node, this);
                }
                else
                {
                    // Add the value to the right node recursively
                    AddTo(node.Right, value);
                }
            }

            // Finally, balance the node
            node.Balance();
        }

        /// <summary>
        /// Finds a specific item in the tree
        /// </summary>
        /// <param name="value">The value to find</param>
        /// <returns>The AVLTreeNode if found, otherwise, null</returns>
        private AVLTreeNode<T> Find(T value)
        {
            AVLTreeNode<T> current = Head;

            while (current != null)
            {
                int result = current.CompareTo(value);

                if (result > 0)
                {
                    // If the value is less than current, go left
                    current = current.Left;
                }
                else if (result < 0)
                {
                    // If the value is greater than current, go right
                    current = current.Right;
                }
                else
                {
                    // Found it
                    break;
                }
            }

            return current;
        }

        #endregion
    }
}
