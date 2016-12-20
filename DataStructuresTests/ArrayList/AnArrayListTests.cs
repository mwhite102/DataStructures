using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures.ArrayList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.ArrayList.Tests
{
    [TestClass()]
    public class AnArrayListTests
    {
        /// <summary>
        /// Initializes an ArrayList<int></int>
        /// </summary>
        /// <returns></returns>
        private AnArrayList<int> InitArrayList()
        {
            AnArrayList<int> result = new AnArrayList<int>();

            result.Add(1);
            result.Add(2);
            result.Add(3);
            result.Add(4);

            return result;
        }

        [TestMethod()]
        public void Insert_Item_Test()
        {
            int expectedValue = 5;
            int expectedIndex = 2;
            int expectedLength = 5;

            AnArrayList<int> arrayList = InitArrayList();

            // Insert a 5 at index 2
            arrayList.Insert(2, 5);

            Assert.AreEqual(arrayList[expectedIndex], expectedValue);
            Assert.AreEqual(arrayList.Count, expectedLength);
        }

        [TestMethod()]
        public void Remove_Item_From_Middle_Of_Array_Test()
        {
            int expectedValue = 4;
            int expectedLength = 3;

            AnArrayList<int> arrayList = InitArrayList();

            // Remove the 3rd item in the array
            arrayList.RemoveAt(2);

            // The last item should be 4 and the array count should be 3
            Assert.AreEqual(arrayList[arrayList.Count - 1], expectedValue);
            Assert.AreEqual(arrayList.Count, expectedLength);
        }

        [TestMethod()]
        public void Remove_Last_Item_From_Array_Test()
        {
            int expectedValue = 3;
            int expectedLength = 3;

            AnArrayList<int> arrayList = InitArrayList();

            // Remove the last item in the array
            arrayList.RemoveAt(arrayList.Count - 1);

            // The last item should be 3 and the array count should be 3
            Assert.AreEqual(arrayList[arrayList.Count - 1], expectedValue);
            Assert.AreEqual(arrayList.Count, expectedLength);
        }

        [TestMethod()]
        public void Remove_First_Item_From_Array_Test()
        {
            int expectedValue = 4;
            int expectedLength = 3;

            AnArrayList<int> arrayList = InitArrayList();

            // Remove the last item in the array
            arrayList.RemoveAt(0);

            // The last item should be 3 and the array count should be 3
            Assert.AreEqual(arrayList[arrayList.Count - 1], expectedValue);
            Assert.AreEqual(arrayList.Count, expectedLength);
        }

        [TestMethod()]
        public void Remove_Specific_Value_Test()
        {
            int expectedValue = 4;
            int expectedLength = 3;

            AnArrayList<int> arrayList = InitArrayList();

            // Remove the 3 from the array
            arrayList.Remove(3);

            // The last item should be 4 and the array count should be 3
            Assert.AreEqual(arrayList[arrayList.Count - 1], expectedValue);
            Assert.AreEqual(arrayList.Count, expectedLength);
        }

        [TestMethod()]
        public void IndexOfTest()
        {
            int expectedValue = 2;
            AnArrayList<int> arrayList = InitArrayList();

            var index = arrayList.IndexOf(3);

            Assert.AreEqual(index, expectedValue);
        }

        [TestMethod()]
        public void ClearTest()
        {
            int expectedValue = 0;
            AnArrayList<int> arrayList = InitArrayList();

            // Clear the array
            arrayList.Clear();

            Assert.AreEqual(arrayList.Count, expectedValue);
        }

        [TestMethod()]
        public void Copy_To_Test()
        {
            int[] array = new int[5];
            AnArrayList<int> arrayList = InitArrayList();
            arrayList.CopyTo(array, 0);
            Assert.IsTrue(array[0] == 1);
            Assert.IsTrue(array[1] == 2);
            Assert.IsTrue(array[2] == 3);
            Assert.IsTrue(array[3] == 4);
        }
    }
}