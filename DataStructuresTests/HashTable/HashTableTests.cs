using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructures.HashTable.Tests
{
    [TestClass()]
    public class HashTableTests
    {
        private HashTable<string, int> InitHashTable()
        {
            HashTable<string, int> hashTable = new HashTable<string, int>(10);

            hashTable.Add("One", 1);
            hashTable.Add("Two", 2);
            hashTable.Add("Three", 3);
            hashTable.Add("Four", 4);
            hashTable.Add("Five", 5);

            return hashTable;
        }

        [TestMethod()]
        public void Remove_Item_Test()
        {
            int expectedCount = 4;
            HashTable<string, int> hashTable = InitHashTable();

            hashTable.Remove("Three");

            Assert.AreEqual(hashTable.Count, expectedCount);
        }

        [TestMethod()]
        public void Should_Get_Indexed_Value()
        {
            int expectedValue = 3;
            HashTable<string, int> hashTable = InitHashTable();

            int value = hashTable["Three"];

            Assert.AreEqual(value, expectedValue);
        }

        [TestMethod()]
        public void TryGetValue_Test()
        {
            int expectedValue = 3;
            HashTable<string, int> hashTable = InitHashTable();

            int value = 0;

            Assert.IsTrue(hashTable.TryGetValue("Three", out value));
            Assert.AreEqual(value, expectedValue);

            Assert.IsFalse(hashTable.TryGetValue("Ten", out value));
            Assert.AreEqual(value, 0);
        }

        [TestMethod()]
        public void ContainsValue_Test()
        {
            HashTable<string, int> hashTable = InitHashTable();

            Assert.IsTrue(hashTable.ContainsValue(3));
            Assert.IsFalse(hashTable.ContainsValue(13));
        }

        [TestMethod()]
        public void Clear_Test()
        {
            int expectedCount = 0;
            HashTable<string, int> hashTable = InitHashTable();

            hashTable.Clear();

            Assert.AreEqual(hashTable.Count, expectedCount);
        }

    }
}