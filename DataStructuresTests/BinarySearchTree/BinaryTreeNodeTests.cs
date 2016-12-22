using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructures.BinarySearchTree.Tests
{
    [TestClass()]
    public class BinaryTreeNodeTests
    {
        [TestMethod()]
        public void Compare_Should_Return_Minus_One()
        {
            int expectedValue = -1;
            BinaryTreeNode<int> node = new BinaryTreeNode<int>(3);
            BinaryTreeNode<int> otherNode = new BinaryTreeNode<int>(5);

            Assert.AreEqual(node.CompareTo(otherNode.Value), expectedValue);
        }

        [TestMethod()]
        public void Compare_Should_Return_One()
        {
            int expectedValue = 1;
            BinaryTreeNode<int> node = new BinaryTreeNode<int>(5);
            BinaryTreeNode<int> otherNode = new BinaryTreeNode<int>(3);

            Assert.AreEqual(node.CompareTo(otherNode.Value), expectedValue);
        }

        [TestMethod()]
        public void Compare_Should_Return_Zero()
        {
            int expectedValue = 0;
            BinaryTreeNode<int> node = new BinaryTreeNode<int>(5);
            BinaryTreeNode<int> otherNode = new BinaryTreeNode<int>(5);

            Assert.AreEqual(node.CompareTo(otherNode.Value), expectedValue);
        }
    }
}