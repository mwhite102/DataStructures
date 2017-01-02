using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructures.AVLTree.Tests
{
    [TestClass()]
    public class AVLTreeTests
    {
        private AVLTree<int> InitAVLTree()
        {
            AVLTree<int> tree = new AVLTree<int>();

            tree.Add(1);
            tree.Add(2);
            tree.Add(3);
            tree.Add(4);
            tree.Add(5);

            return tree;
        }

        [TestMethod()]
        public void Add_Items_Test()
        {
            int expectedCount = 5;
            AVLTree<int> tree = InitAVLTree();
            Assert.AreEqual(tree.Count, expectedCount);
        }

        [TestMethod()]
        public void Remove_Item_Test()
        {
            int expectedCount = 4;
            AVLTree<int> tree = InitAVLTree();
            tree.Remove(3);
            Assert.AreEqual(tree.Count, expectedCount);
        }

        [TestMethod()]
        public void Contains_Item_Test()
        {
            AVLTree<int> tree = InitAVLTree();
            Assert.IsTrue(tree.Contains(3));
        }


    }
}