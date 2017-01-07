using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.BTree
{
    public class BTree<T> : ICollection<T> where T : IComparable<T>
    {
        BTreeNode<T> root = null;

        const int MinimumDegree = 2;

        /// <summary>
        /// Gets the number of items in the tree
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Always returns false
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Adds a specified value to the tree
        /// </summary>
        /// <param name="value">The value to be added</param>
        public void Add(T value)
        {
            if (root == null)
            {
                // Make this value the root
                root = new BTreeNode<T>(null, true, MinimumDegree, new[] { value }, new BTreeNode<T>[] { });
            }
            else
            {
                // If the root is full, split the root node 
                if (root.Full)
                {
                    root = root.SplitFullRootNode();
                }

                // Insert the value
                InsertNonFull(root, value);
            }

            Count++;
        }

        /// <summary>
        /// Clears the tree
        /// </summary>
        public void Clear()
        {
            root = null;
            Count = 0;
        }

        /// <summary>
        /// Determines if an item is in the tree
        /// </summary>
        /// <param name="item">The item to find in the tree</param>
        /// <returns>True if the item is in the tree, otherwise false</returns>
        public bool Contains(T item)
        {
            BTreeNode<T> node;
            int valueIndex;
            return TryFindNodeContainingValue(item, out node, out valueIndex);
        }
        
        /// <summary>
        /// Copies all items in the tree into the target array staring at the specified index
        /// </summary>
        /// <param name="array">The target array</param>
        /// <param name="arrayIndex">The starting index in the target array</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (T value in InOrderEnumerator(root))
            {
                array[arrayIndex++] = value;
            }
        }

        /// <summary>
        /// Gets an enumerator that allows enumerating each of the values in the tree
        /// </summary>
        /// <returns>Returns an enumerator that allows enumerating each of the values in the tree
        /// using an inorder enumeration</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return InOrderEnumerator(root).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Removes the first item with the specified value from tree.
        /// </summary>
        /// <param name="value">The value to be removed</param>
        /// <returns>True if removed, otherwise false</returns>
        public bool Remove(T value)
        {
            bool removed = false;

            if (Count > 0)
            {
                // Attempt to remove the value
                removed = RemoveValue(root, value);
                if (removed)
                {
                    // Decrement the count
                    Count--;

                    // Was it the root?
                    if (Count == 0)
                    {
                        root = null;
                    }
                    else if (root.Values.Count == 0)
                    {
                        // It was the last value in the root
                        // Make the root's first child the root
                        root = root.Children[0];
                    }
                }
            }

            return removed;
        }

        #region Private Methods

        private void InsertNonFull(BTreeNode<T> node, T value)
        {
            if (node.Leaf)
            {
                // It's a leaf node
                node.InsertKeyToLeafNode(value);
            }
            else
            {
                // Find the insert index
                int index = node.Values.Count - 1;
                while (index >= 0 && value.CompareTo(node.Values[index]) < 0)
                {
                    index--;
                }

                index++;

                // If the insert node is full
                if (node.Children[index].Full)
                {
                    // Split the full child and determine which node to insert into
                    node.SplitFullChild(index);
                    if (value.CompareTo(node.Values[index]) > 0)
                    {
                        index++;
                    }
                }

                InsertNonFull(node.Children[index], value);
            }
        }

        internal static bool RemoveValue(BTreeNode<T> node, T value)
        {
            if (node.Leaf)
            {
                // The node is a leaf
                // If we are in a leaf node, we have either pushed down values such that the leaf node
                // has minimum degree children and can have one removed, or the root node is also a leaf
                // nod and we can violate the minimum rule.
                return node.DeleteKeyFromLeafNode(value);
            }

            int valueIndex;
            if (TryGetIndexOf(node, value, out valueIndex))
            {
                // We found the non-leaf node the value is in.
                // We can only delete values from a leaf node, so push the value to delete down into a child.

                // If the left child has at least the minimum degree of children
                if (node.Children[valueIndex].Values.Count >= node.Children[valueIndex].MinimumDegree)
                {
                    //     [3     6       10]
                    // [1 2] [4 5] [7 8 9]  [11 12]

                    // Deleting 10

                    // Find the largest value in the child node that contains smaller values that 10 (9)
                    T valuePrime = FindPredecessor(node, valueIndex);

                    // Replace the value to delete with the next largest value
                    node.ReplaceValue(valueIndex, valuePrime);

                    // We now have:
                    //     [3     6       9]
                    // [1 2] [4 5] [7 8 9] [11 12]

                    // Delete the value moved up (9) from the child
                    // which may push it down until it gets to a leaf
                    return RemoveValue(node.Children[valueIndex], valuePrime);

                    // Finally:
                    //     [3     6     9]
                    // [1 2] [4 5] [7 8] [11 12]
                }
                else
                {
                    // The left child did not have enough values to move a value up, does the right?
                    if (node.Children[valueIndex + 1].Values.Count >= node.Children[valueIndex + 1].MinimumDegree)
                    {
                        // Do the opposite of the previous block

                        //     [3     6       10]
                        // [1 2] [4 5] [7 8 9]  [11 12]

                        // Deleting 6, 7 is the successor
                        T valuePrime = FindSuccessor(node, valueIndex);
                        node.ReplaceValue(valueIndex, valuePrime);

                        // We now have:
                        //     [3     7       10]
                        // [1 2] [4 5] [7 8 9]  [11 12]

                        // Remove 7 from the child
                        return RemoveValue(node.Children[valueIndex + 1], valuePrime);

                        // Finally:
                        //     [3     7     10]
                        // [1 2] [4 5] [8 9]  [11 12]
                    }
                    else
                    {
                        // Neither child as the minimum degree of children.
                        // Both have minimum degree - 1 children
                        // Merge the two nodes into a single child

                        //     [3     6     9]
                        // [1 2] [4 5] [7 8] [10 11]

                        // Deleting 6

                        // Push down 6
                        BTreeNode<T> newChildNode = node.PushDown(valueIndex);

                        // Now that we've pushed the value down, remove the 6 from the child node
                        return RemoveValue(newChildNode, value);
                    }
                }
            }
            else
            {
                // We have an internal node that does not contain the value we want to delete.
                // Find the child path the value to delete would be in, if it is in the tree
                int childIndex;
                FindPotentialPath(node, value, out valueIndex, out childIndex);

                // We know where the value should be, does the node we are going to have the minimum
                // number of values required to delete from?
                if (node.Children[childIndex].Values.Count == node.Children[childIndex].MinimumDegree - 1)
                {
                    // Not enough values
                    // Borrow a value from a sibling that has enough values

                    // What sibling has the most values?
                    int indexOfMaxSiblilng = GetIndexOfMaxSibling(childIndex, node);

                    // If a sibling with values exists
                    if (indexOfMaxSiblilng >= 0 
                        && node.Children[indexOfMaxSiblilng].Values.Count 
                        >= node.Children[indexOfMaxSiblilng].MinimumDegree)
                    {
                        // Rotate appropriate value from sibling through the parent and into the current node
                        // to have enough values in the current node to push a value down into the child to check next

                        //     [3       7]
                        // [1 2] [4 5 6] [7 8]

                        // We want to travel through [1 2] but we need another node in it.
                        // Rotate 4 up to the root and push 3 down into [1 2]

                        //        [4      7]
                        // [1 2 3] [4 5 6] [7 8]

                        RotateAndPushDown(node, childIndex, indexOfMaxSiblilng);
                    }
                    else
                    {
                        // Merge (might be only node in root)
                        BTreeNode<T> pushedDownNode = node.PushDown(valueIndex);

                        // Now find node just pushed down
                        childIndex = 0;
                        while (pushedDownNode != node.Children[childIndex])
                        {
                            childIndex++;
                        }
                    }
                }

                return RemoveValue(node.Children[childIndex], value);
            }
        }

        private static void RotateAndPushDown(BTreeNode<T> node, int childIndex, int indexOfMaxSiblilng)
        {
            int valueIndex;
            if (childIndex < indexOfMaxSiblilng)
            {
                valueIndex = childIndex;
            }
            else
            {
                valueIndex = childIndex - 1;
            }

            if (indexOfMaxSiblilng > childIndex)
            {
                // Moving leftmost key from the right sibling into parent
                // and pushing parent down into child

                //   [6       10]
                // [1] [7 8 9 ] [11] 

                // Deleting something < 6

                //     [7    10]
                // [1 6] [8 9] [11]

                // Get the 7
                T valueToMoveToX = node.Children[indexOfMaxSiblilng].Values.First();
                // Get 7's left child if it has one
                BTreeNode<T> childToMoveToNode = node.Children[indexOfMaxSiblilng].Leaf
                    ? null
                    : node.Children[indexOfMaxSiblilng].Children.First();

                // Get the value to be pushed down (in example, 6)
                T valueToMoveDown = node.Values[valueIndex];

                // Move the 7 into the parent
                node.ReplaceValue(valueIndex, valueToMoveToX);

                // Move the 6 down into the child
                node.Children[childIndex].AddEnd(valueToMoveDown, childToMoveToNode);

                // Remove first value and child from sibling now that they are moved
                node.Children[indexOfMaxSiblilng].RemoveFirst();
            }
            else
            {
                // Moving rightmost key from the left sibling into parent
                // and pushing parent down into the child

                //   [6       10]
                // [1] [7 8 9 ] [11] 

                // Deleting something > than 10

                //     [7    9]
                // [1 6] [7 8] [10 11]

                // Grab the 9
                T valueToMoveToX = node.Children[indexOfMaxSiblilng].Values.Last();

                // Get the 9's right child if it has one
                BTreeNode<T> childToMoveToNode = node.Children[indexOfMaxSiblilng].Leaf
                    ? null
                    : node.Children[indexOfMaxSiblilng].Children.Last();

                // Get the value to push down (in example, 10)
                T valueToMoveDown = node.Values[valueIndex];

                // Move 9 in th the parent
                node.ReplaceValue(valueIndex, valueToMoveToX);

                // Move 10 down into the child
                node.Children[childIndex].AddFront(valueToMoveDown, childToMoveToNode);

                // Remove the last value and child from the sibling not that they are moved
                node.Children[indexOfMaxSiblilng].RemoveLast();
            }
        }

        private static int GetIndexOfMaxSibling(int index, BTreeNode<T> node)
        {
            // Gets the index of the child node that has the most values in it

            //     [3       7]
            // [1 2] [4 5 6] [8 9]

            // If node is [3 7] with index = 0
            // [1 2] [4 5 6] would be checked and and 1 would be returned (index of [4 5 6])

            // If node is [3 7] with index = 1
            // [4 5 6] [8 9] would be checked and and 1 would be returned (index of [4 5 6])

            int indexOfMaxSibling = -1;

            BTreeNode<T> leftSibling = null;
            if (index > 0)
            {
                leftSibling = node.Children[index - 1];
            }

            BTreeNode<T> rightSibling = null;
            if (index + 1 < node.Children.Count)
            {
                rightSibling = node.Children[index + 1];
            }

            if (leftSibling != null || rightSibling != null)
            {
                if (leftSibling != null && rightSibling != null)
                {
                    indexOfMaxSibling = leftSibling.Values.Count > rightSibling.Values.Count
                        ? index - 1
                        : index + 1;
                }
                else
                {
                    indexOfMaxSibling = leftSibling != null 
                        ? index - 1 
                        : index + 1;
                }
            }

            return indexOfMaxSibling;
        }

        private static void FindPotentialPath(BTreeNode<T> node, T value, out int valueIndex, out int childIndex)
        {
            // Find out which child the value we are searching for would be in if the value were in the tree
            childIndex = node.Children.Count - 1;
            valueIndex = node.Values.Count - 1;

            // Starting at rightmost child and value indexes and working backwards until
            // we are at less than the value we want
            while (valueIndex > 0)
            {
                int compare = value.CompareTo(node.Values[valueIndex]);
                if (compare > 0)
                {
                    break;
                }

                childIndex--;
                valueIndex--;
            }

            // Did we make it all the way to the last value?
            if (valueIndex == 0)
            {
                // If value we're looking for is less that the first value in the node,
                // then child is the 0 index child, not 1
                if (value.CompareTo(node.Values[valueIndex]) < 0)
                {
                    childIndex--;
                }
            }
        }

        private static T FindSuccessor(BTreeNode<T> node, int index)
        {
            // Finds the successor value of a specific value

            //     [3     6]
            // [1 2] [4 5] [7 8] 

            // The successor of 3 is 4

            node = node.Children[index + 1];

            while (!node.Leaf)
            {
                node = node.Children.First();
            }

            return node.Values.First();
        }

        private static T FindPredecessor(BTreeNode<T> node, int index)
        {
            // Finds the predecessor value of a specific value

            //     [3     6]
            // [1 2] [4 5] [7 8] 

            // The predecessor of 3 is 2

            node = node.Children[index];

            while (!node.Leaf)
            {
                node = node.Children.Last();
            }

            return node.Values.Last();
        }

        internal static bool TryGetIndexOf(BTreeNode<T> node, T value, out int valueIndex)
        {
            for (int index = 0; index < node.Values.Count; index++)
            {
                if (value.CompareTo(node.Values[index]) == 0)
                {
                    valueIndex = index;
                    return true;
                }
            }

            valueIndex = -1;
            return false;
        }

        internal bool TryFindNodeContainingValue(T value, out BTreeNode<T> node, out int valueIndex)
        {
            BTreeNode<T> current = root;

            while (current != null)
            {
                int index = 0;

                // Check each value in the node
                while (index < current.Values.Count)
                {
                    int compare = value.CompareTo(current.Values[index]);

                    if (compare == 0)
                    {
                        // Found it
                        node = current;
                        valueIndex = index;
                        return true;
                    }

                    // If value is less than current node value then go left
                    if (compare < 0)
                    {
                        break;
                    }

                    // Move to the next value in the node
                    index++;
                }

                if (current.Leaf)
                {
                    // If it's a leaf node, there is no child to go down to
                    break;
                }
                else
                {
                    // Go to the child we determined must contain the value
                    current = current.Children[index];
                }
            }

            node = null;
            valueIndex = -1;
            return false;
        }

        private IEnumerable<T> InOrderEnumerator(BTreeNode<T> node)
        {
            if (node != null)
            {
                if (node.Leaf)
                {
                    foreach(T value in node.Values)
                    {
                        yield return value;
                    }
                }
                else
                {
                    IEnumerator<BTreeNode<T>> children = node.Children.GetEnumerator();
                    IEnumerator<T> values = node.Values.GetEnumerator();

                    while (children.MoveNext())
                    {
                        foreach (T childValue in InOrderEnumerator(children.Current))
                        {
                            yield return childValue;
                        }

                        if (values.MoveNext())
                        {
                            yield return values.Current;
                        }
                    }
                }
            }
        }


        #endregion
    }
}
