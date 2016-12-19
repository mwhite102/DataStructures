using DataStructures.DoublyLinkedList;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructures.DoublyLinkedList.Tests
{
    [TestClass()]
    public class DoublyLinkedListTests
    {
        public DoublyLinkedList<int> InitLinkedList()
        {
            DoublyLinkedList<int> linkedList = new DoublyLinkedList<int>();

            // Add items to the list
            linkedList.Add(1);
            linkedList.Add(2);
            linkedList.Add(3);
            linkedList.Add(4);
            linkedList.Add(5);

            return linkedList;
        }

        [TestMethod()]
        public void Add_Five_Items_And_Check_Count_Test()
        {
            int expectedCount = 5;
            DoublyLinkedList<int> list = InitLinkedList();
            Assert.IsTrue(list.Count == expectedCount);
        }

        [TestMethod()]
        public void Add_First_Item_Test()
        {
            int expectedCount = 6;
            DoublyLinkedList<int> list = InitLinkedList();
            list.AddFirst(0);
            Assert.IsTrue(list.Count == expectedCount);
            Assert.IsTrue(list.Head.Value == 0);
        }

        [TestMethod()]
        public void Add_Last_Item_Test()
        {
            int expectedCount = 6;
            DoublyLinkedList<int> list = InitLinkedList();
            list.AddLast(0);
            Assert.IsTrue(list.Count == expectedCount);
            Assert.IsTrue(list.Tail.Value == 0);
        }

        [TestMethod()]
        public void Clear_List_Test()
        {
            int expectedCount = 0;
            DoublyLinkedList<int> list = InitLinkedList();
            list.Clear();
            Assert.IsTrue(list.Count == expectedCount);
        }

        [TestMethod()]
        public void Contains_Specific_Item_Test()
        {
            DoublyLinkedList<int> list = InitLinkedList();
            bool result = list.Contains(3);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Copy_To_Test()
        {
            int[] array = new int[5];
            DoublyLinkedList<int> list = InitLinkedList();
            list.CopyTo(array, 0);
            Assert.IsTrue(array[0] == 1);
            Assert.IsTrue(array[1] == 2);
            Assert.IsTrue(array[2] == 3);
            Assert.IsTrue(array[3] == 4);
            Assert.IsTrue(array[4] == 5);
        }

        [TestMethod()]
        public void Remove_Item_Test()
        {
            int expectedCount = 4;
            DoublyLinkedList<int> list = InitLinkedList();
            list.Remove(3);
            Assert.AreEqual(list.Count, expectedCount);
        }

        [TestMethod()]
        public void Remove_First_Item_Test()
        {
            int expectedCount = 4;
            int expectedValue = 2;
            DoublyLinkedList<int> list = InitLinkedList();
            list.RemoveFirst();
            Assert.AreEqual(list.Count, expectedCount);
            Assert.AreEqual(list.Head.Value, expectedValue);
        }

        [TestMethod()]
        public void Remove_Last_Item_Test()
        {
            int expectedCount = 4;
            int expectedValue = 4;
            DoublyLinkedList<int> list = InitLinkedList();
            list.RemoveLast();
            Assert.AreEqual(list.Count, expectedCount);
            Assert.AreEqual(list.Tail.Value, expectedValue);
        }
    }
}