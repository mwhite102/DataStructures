using DataStructures.Stack;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The Binary Search Tree follows basic set of rules
/// 1. Each node can have 0, 1, or 2 children nodes.
/// 2. Any value less than the node's value goes to the left child, or a child of the left child.
/// 3. Any value greater than, or equal to the node's value goes to the right child, or a child of the right child.
/// 
/// Learn more about Binary trees on Wikipedia
/// https://en.wikipedia.org/wiki/Binary_tree
/// 
/// Learn more about tree traversal on Wikipedia
/// https://en.wikipedia.org/wiki/Tree_traversal
/// </summary>
namespace DataStructures.BinarySearchTree
{
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        /// <summary>
        /// Keeps track of the head (root) node
        /// </summary>
        private BinaryTreeNode<T> _head;

        /// <summary>
        /// Keeps track of the number of nodes in the tree
        /// </summary>
        private int _count;

        /// <summary>
        /// Adds a node to the binary tree
        /// </summary>
        /// <param name="value">The value to be added to the binary tree</param>
        public void Add(T value)
        {
            // If the tree is empty, just add the value as the head
            if (_head == null)
            {
                _head = new BinaryTreeNode<T>(value);
            }
            else
            {
                // The tree is not empty.  Recursively find the insert point
                AddTo(_head, value);
            }

            // Increment the count
            _count++;
        }

        /// <summary>
        /// Recursive method to add
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        private void AddTo(BinaryTreeNode<T> node, T value)
        {
            if (value.CompareTo(node.Value) < 0)
            {
                // The value being added is less than the node it's being added to
                if (node.Left == null)
                {
                    // There is no left node, so add it there
                    node.Left = new BinaryTreeNode<T>(value);
                }
                else
                {
                    // There is already a node.Left on the current node
                    // Recursively add the value to the node.Left node
                    AddTo(node.Left, value);
                }
            }
            else
            {
                // The value is greater than or equal to the current node
                if (node.Right == null)
                {
                    // There is no right node, so add it there
                    node.Right = new BinaryTreeNode<T>(value);
                }
                else
                {
                    // There is already a node.Right on the current node
                    // Recursively add the value to the node.Right node
                    AddTo(node.Right, value);
                }
            }
        }

        /// <summary>
        /// Determines if the specified value is in the binary tree
        /// </summary>
        /// <param name="value">The specified value</param>
        /// <returns>True if found, false if not</returns>
        public bool Contains(T value)
        {
            // Defer to FindWithParent function
            // Note, parent value is not used here.
            BinaryTreeNode<T> parent = null;
            return FindWithParent(value, out parent) != null;
        }

        /// <summary>
        /// Finds and returns first node with specified value.  
        /// Also finds the parent of the found node (or null is no parent).
        /// The parent value is used in Remove method
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private BinaryTreeNode<T> FindWithParent(T value, out BinaryTreeNode<T> parent)
        {
            BinaryTreeNode<T> current = _head;
            parent = null;

            // Loop until we find a match or run out of nodes
            while(current != null)
            {
                int result = current.CompareTo(value);

                if (result > 0)
                {
                    // Value is less than current. Go left
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    // Value is greater than current. Go right
                    parent = current;
                    current = current.Right;
                }
                else
                {
                    // Must be a match (result == 0)
                    // break out the loop
                    break;
                }
            }

            return current;
        }

        /// <summary>
        /// Removes an specified value from the binary tree
        /// </summary>
        /// <param name="value">The specified value</param>
        /// <returns>True if found and removed, false if not</returns>
        public bool Remove(T value)
        {
            BinaryTreeNode<T> current;
            BinaryTreeNode<T> parent;

            // Find node to remove using FindWithParent
            current = FindWithParent(value, out parent);

            if (current == null) return false;

            // Decrement the count, we're going to remove a node
            _count--;

            // 1st scenario: Current has no right child.
            // The current node's left child replaces the current node
            if (current.Right == null)
            {
                if (parent == null)
                {
                    // If the parent is null, make the current node's left the head
                    _head = current.Left;
                }
                else
                {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // If parent value is greater than the current value
                        // make the current left child a left child of parent
                        parent.Left = current.Left;
                    }
                    else if (result < 0)
                    {
                        // If parent value is less than the current value
                        // make the current lift child a right child of the parent
                        parent.Right = current.Left;
                    }
                }
            }

            // 2nd scenario: Current has a right child, but no left child
            // The current node's right child  replaces the current node
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;

                if (parent == null)
                {
                    _head = current.Right;
                }
                else
                {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // If parent value is greater than current value
                        // make the current right child a left child of the parent
                        parent.Left = current.Right;
                    }
                    else if (result < 0)
                    {
                        // If parent value is less than current value
                        // make the current right child a right child of the parent
                        parent.Right = current.Right;
                    }
                }
            }

            // 3rd scenario: Current has a right child, and that node has a left child
            // Replace the current node with the right child's left most child node
            // This on is the most complex scenario
            else
            {
                // Find the right's left most child
                BinaryTreeNode<T> leftMost = current.Right.Left;
                BinaryTreeNode<T> leftMostParent = current.Right;

                while (leftMost.Left != null)
                {
                    leftMostParent = leftMost;
                    leftMost = leftMost.Left;
                }

                // The parent's left mode subtree becomes the leftmost's right subtree
                leftMostParent.Left = leftMost.Right;

                // Assign leftmost's left and right to current's left and right children
                leftMost.Left = current.Left;
                leftMost.Right = current.Right;

                if (parent == null)
                {
                    _head = leftMost;
                }
                else
                {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // If parent value is greater than current value
                        // make the leftmost the parent's left child
                        parent.Left = leftMost;
                    }
                    else if (result < 0)
                    {
                        // If parent value is less than current value
                        // make the leftmost the parent's right child
                        parent.Right = leftMost;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// A depth first traversal that starts at the root, then the left node and all it's 
        /// children are processed, followed by the right node and all it's children
        /// </summary>
        /// <param name="action">The action to perform on each node</param>
        public void PreOrderTraversal(Action<T> action)
        {
            PreOrderTraversal(action, _head);
        }

        private void PreOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                action(node.Value);
                PreOrderTraversal(action, node.Left);
                PreOrderTraversal(action, node.Right);
            }
        }

        /// <summary>
        /// A depth first traversal that starts at the left subtree, then the right subtree and then the root (head)
        /// </summary>
        /// <param name="action"></param>
        public void PostOrderTraversal(Action<T> action)
        {
            PostOrderTraversal(action, _head);
        }

        private void PostOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                PostOrderTraversal(action, node.Left);
                PostOrderTraversal(action, node.Right);
                action(node.Value);
            }
        }

        /// <summary>
        /// A depth first traversal that processes the nodes in sort order
        /// </summary>
        /// <param name="action"></param>
        public void InOrderTraversal(Action<T> action)
        {
            InOrderTraversal(action, _head);
        }

        private void InOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                InOrderTraversal(action, node.Left);
                action(node.Value);
                InOrderTraversal(action, node.Right);
            }
        }

        /// <summary>
        /// A non-recursive method using a stack to remove recursion
        /// Will be called by GetEnumerator
        /// </summary>
        /// <remarks>
        /// This method took me a while to get my head around it.
        /// Stepping through the code with the debugger helped make it click.
        /// </remarks>
        /// <returns>An IEnumerator<T></returns>
        private IEnumerator<T> InOrderTraversal()
        {
            if (_head != null)
            {
                // Used to store nodes we skip
                AStack<BinaryTreeNode<T>> stack = new AStack<BinaryTreeNode<T>>();

                BinaryTreeNode<T> current = _head;

                // When not using recursion, we need to keep track of whether we
                // should be processing left or right nodes
                bool goLeftNext = true;

                // Push the current node on the stack
                stack.Push(current);

                while (stack.Count > 0)
                {
                    // If heading left
                    if (goLeftNext)
                    {
                        // Push all but the left-most node on the stack
                        // yield the left-most after this block
                        while(current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }

                    // InOrder is left->yield->right
                    yield return current.Value;

                    // If we can go right, do it
                    if (current.Right != null)
                    {
                        current = current.Right;

                        // Once we've gone right once, start going left again
                        goLeftNext = true;
                    }
                    else
                    {
                        // If can't go right, pop off parent node
                        // so it can be processed and then go to its right side
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }

        /// <summary>
        /// Removes all nodes from the binary tree
        /// </summary>
        public void Clear()
        {
            _head = null;
            _count = 0;
        }

        /// <summary>
        /// Gets the number of nodes in the binary tree
        /// </summary>
        public int Count
        {
            get
            {
                return _count;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
