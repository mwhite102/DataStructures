using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DataStructures.BinarySearchTree.Tests
{
    [TestClass()]
    public class BinaryTreeTests
    {
        private BinaryTree<int> InitTraversalTestBinaryTree()
        {
            BinaryTree<int> tree = new BinaryTree<int>();

            tree.Add(4);
            tree.Add(2);
            tree.Add(5);
            tree.Add(1);
            tree.Add(3);
            tree.Add(7);
            tree.Add(6);
            tree.Add(8);

            return tree;
        }

        [TestMethod()]
        public void Contains_Specific_Nodes_Test()
        {
            BinaryTree<int> tree = new BinaryTree<int>();

            tree.Add(10);
            tree.Add(1);
            tree.Add(20);
            tree.Add(5);
            tree.Add(15);

            Assert.IsTrue(tree.Contains(10));
            Assert.IsTrue(tree.Contains(1));
            Assert.IsTrue(tree.Contains(20));
            Assert.IsTrue(tree.Contains(5));
            Assert.IsTrue(tree.Contains(15));
        }

        [TestMethod()]
        public void Remove_1st_Senario_Test()
        {
            // 1st Scenario
            // Node to be removed has no right child
            string expectedString = "8,2,10";
            List<int> results = new List<int>();

            BinaryTree<int> tree = new BinaryTree<int>();

            tree.Add(8);
            tree.Add(5);
            tree.Add(10);
            tree.Add(2);

            tree.Remove(5);

            tree.PreOrderTraversal(x => results.Add(x));
            string resultString = string.Join(",", results.ToArray());

            Assert.AreEqual(expectedString, resultString);
        }

        [TestMethod()]
        public void Remove_2nd_Senario_Test()
        {
            // 2nd Scenario
            // Node to be removed has a right child, but right child doesn't have a left child
            string expectedString = "8,6,2,7,10";
            List<int> results = new List<int>();

            BinaryTree<int> tree = new BinaryTree<int>();

            tree.Add(8);
            tree.Add(5);
            tree.Add(6);
            tree.Add(7);
            tree.Add(10);
            tree.Add(2);

            tree.Remove(5);

            tree.PreOrderTraversal(x => results.Add(x));
            string resultString = string.Join(",", results.ToArray());

            Assert.AreEqual(expectedString, resultString);
        }

        [TestMethod()]
        public void Remove_3rd_Senario_Test()
        {
            // 3rd Scenario
            // Node to be removed has a right child and that node has a left child
            string expectedString = "8,6,2,7,10";
            List<int> results = new List<int>();

            BinaryTree<int> tree = new BinaryTree<int>();

            tree.Add(8);
            tree.Add(5);
            tree.Add(7);
            tree.Add(6);
            tree.Add(10);
            tree.Add(2);

            tree.Remove(5);

            tree.PreOrderTraversal(x => results.Add(x));
            string resultString = string.Join(",", results.ToArray());

            Assert.AreEqual(expectedString, resultString);
        }

        [TestMethod()]
        public void Pre_Order_Traversal_Test()
        {
            string expectedString = "4,2,1,3,5,7,6,8";
            List<int> results = new List<int>();

            BinaryTree<int> tree = InitTraversalTestBinaryTree();

            tree.PreOrderTraversal(x => results.Add(x));

            string resultString = string.Join(",", results.ToArray());

            Assert.AreEqual(expectedString, resultString);
        }

        [TestMethod()]
        public void Post_Order_Traversal_Test()
        {
            string expectedString = "1,3,2,6,8,7,5,4";
            List<int> results = new List<int>();

            BinaryTree<int> tree = InitTraversalTestBinaryTree();

            tree.PostOrderTraversal(x => results.Add(x));

            string resultString = string.Join(",", results.ToArray());

            Assert.AreEqual(expectedString, resultString);
        }

        [TestMethod()]
        public void In_Order_Traversal_Test()
        {
            string expectedString = "1,2,3,4,5,6,7,8";
            List<int> results = new List<int>();

            BinaryTree<int> tree = InitTraversalTestBinaryTree();

            tree.InOrderTraversal(x => results.Add(x));

            string resultString = string.Join(",", results.ToArray());

            Assert.AreEqual(expectedString, resultString);
        }

        [TestMethod()]
        public void GetEnumerator_Test()
        {
            string expectedString = "1,2,3,4,5,6,7,8";
            List<int> results = new List<int>();

            BinaryTree<int> tree = InitTraversalTestBinaryTree();

            IEnumerator<int> enumerator = tree.GetEnumerator();

            while (enumerator.MoveNext())
            {
                results.Add(enumerator.Current);
            }

            string resultString = string.Join(",", results.ToArray());

            Assert.AreEqual(expectedString, resultString);
        }
    }
}