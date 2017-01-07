using DataStructures.BTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructures.BTree.Tests
{
    [TestClass()]
    public class BTreeTests
    {
        private BTree<int> InitBTree()
        {
            BTree<int> bTree = new BTree<int>();

            for (int i = 0; i < 10; i++)
            {
                bTree.Add(i);
            }

            return bTree;
        }

        [TestMethod()]
        public void Add_Items_Test()
        {
            BTree<int> bTree = InitBTree();
            Assert.AreEqual(bTree.Count, 10);
        }

        [TestMethod()]
        public void Contains_Item_Test()
        {
            BTree<int> bTree = InitBTree();
            Assert.IsTrue(bTree.Contains(5));
        }

        [TestMethod()]
        public void Remove_Item_Test()
        {
            BTree<int> bTree = InitBTree();
            bTree.Remove(5);
            Assert.IsFalse(bTree.Contains(5));
            Assert.AreEqual(bTree.Count, 9);
        }
    }
}