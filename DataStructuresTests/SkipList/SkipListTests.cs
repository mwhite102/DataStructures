using DataStructures.SkipList;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructures.SkipList.Tests
{
    [TestClass()]
    public class SkipListTests
    {
        private SkipList<int> InitSkipList()
        {
            SkipList<int> skipList = new SkipList<int>();

            skipList.Add(0);
            skipList.Add(1);
            skipList.Add(2);
            skipList.Add(3);
            skipList.Add(4);
            skipList.Add(5);
            skipList.Add(6);
            skipList.Add(7);
            skipList.Add(8);
            skipList.Add(9);

            return skipList;
        }

        [TestMethod()]
        public void Add_Items_Test()
        {
            int expectedCount = 10;
            SkipList<int> skipList = InitSkipList();

            Assert.AreEqual(skipList.Count, expectedCount);
        }

        [TestMethod()]
        public void Contains_Test()
        {
            SkipList<int> skipList = InitSkipList();
            Assert.IsTrue(skipList.Contains(5));
        }

        [TestMethod()]
        public void Remove_Test()
        {
            int expectedCount = 9;
            SkipList<int> skipList = InitSkipList();

            skipList.Remove(5);

            Assert.AreEqual(skipList.Count, expectedCount);
            Assert.IsFalse(skipList.Contains(5));
        }
    }
}