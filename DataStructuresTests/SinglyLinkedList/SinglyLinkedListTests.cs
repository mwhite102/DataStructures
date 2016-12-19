using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures.SinglyLinkedList;
using System.Collections.Generic;

namespace DataStructures.SinglySinglyLinkedList.Tests
{
    [TestClass()]
    public class SinglySinglyLinkedListTests
    {
        private SinglyLinkedList<int> InitSinglyLinkedList()
        {
            SinglyLinkedList<int> SinglyLinkedList = new SinglyLinkedList<int>();

            SinglyLinkedList.Add(1);
            SinglyLinkedList.Add(2);
            SinglyLinkedList.Add(3);

            return SinglyLinkedList;
        }

        [TestMethod()]
        public void Should_Add_Two_Items_And_Count_Equals_Two()
        {
            SinglyLinkedList<int> SinglyLinkedList = InitSinglyLinkedList();
            Assert.AreEqual(3, SinglyLinkedList.Count);
        }

        [TestMethod()]
        public void Should_Add_Two_Items_And_Clear_List_And_Count_Equals_Zero()
        {
            SinglyLinkedList<int> SinglyLinkedList = InitSinglyLinkedList();
            Assert.AreEqual(3, SinglyLinkedList.Count);
            SinglyLinkedList.Clear();
            Assert.AreEqual(0, SinglyLinkedList.Count);
        }

        [TestMethod()]
        public void Should_Contain_The_Number_Two()
        {
            SinglyLinkedList<int> SinglyLinkedList = InitSinglyLinkedList();
            Assert.IsTrue(SinglyLinkedList.Contains(2));
        }

        [TestMethod()]
        public void Copy_All_Items_To_Array()
        {
            int[] array = new int[3];
            SinglyLinkedList<int> SinglyLinkedList = InitSinglyLinkedList();
            SinglyLinkedList.CopyTo(array, 0);
            Assert.IsTrue(array[0] == 1);
            Assert.IsTrue(array[1] == 2);
            Assert.IsTrue(array[2] == 3);
        }

        [TestMethod()]
        public void Remove_First_Item()
        {
            SinglyLinkedList<int> SinglyLinkedList = InitSinglyLinkedList();
            SinglyLinkedList.Remove(1);
            Assert.AreEqual(2, SinglyLinkedList.Count);
            Assert.IsFalse(SinglyLinkedList.Contains(1));
        }

        [TestMethod()]
        public void Remove_Last_Item()
        {
            SinglyLinkedList<int> SinglyLinkedList = InitSinglyLinkedList();
            SinglyLinkedList.Remove(3);
            Assert.AreEqual(2, SinglyLinkedList.Count);
            Assert.IsFalse(SinglyLinkedList.Contains(3));
        }

        [TestMethod()]
        public void Remove_Middle_Item()
        {
            SinglyLinkedList<int> SinglyLinkedList = InitSinglyLinkedList();
            SinglyLinkedList.Remove(2);
            Assert.AreEqual(2, SinglyLinkedList.Count);
            Assert.IsFalse(SinglyLinkedList.Contains(2));
        }

        [TestMethod()]
        public void Get_Enumerator_Test()
        {
            SinglyLinkedList<int> SinglyLinkedList = InitSinglyLinkedList();
            IEnumerator<int> enumerator = SinglyLinkedList.GetEnumerator();
            int count = 0;
            while (enumerator.MoveNext())
            {
                count++;
            }
            Assert.AreEqual(3, count);
        }
    }
}