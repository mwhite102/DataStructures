using DataStructures.Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructures.Sorting.Tests
{
    [TestClass()]
    public class SortingAlgorithmsTests
    {
        int[] _Items = new int[] { 5, 4, 3, 2, 1 };
        string _ExpectedResult = "1,2,3,4,5";

        [TestMethod()]
        public void BubbleSortTest()
        {
            SortingAlgorithms<int>.BubbleSort(_Items);
            string tmp = string.Join(",", _Items);
            Assert.AreEqual(tmp, _ExpectedResult);
        }

        [TestMethod()]
        public void InsertionSortTest()
        {
            SortingAlgorithms<int>.InsertionSort(_Items);
            string tmp = string.Join(",", _Items);
            Assert.AreEqual(tmp, _ExpectedResult);
        }

        [TestMethod()]
        public void SelectionSortTest()
        {
            SortingAlgorithms<int>.SelectionSort(_Items);
            string tmp = string.Join(",", _Items);
            Assert.AreEqual(tmp, _ExpectedResult);
        }

        [TestMethod()]
        public void MergeSortTest()
        {
            SortingAlgorithms<int>.MergeSort(_Items);
            string tmp = string.Join(",", _Items);
            Assert.AreEqual(tmp, _ExpectedResult);
        }

        [TestMethod()]
        public void QuickSortTest()
        {
            SortingAlgorithms<int>.QuickSort(_Items);
            string tmp = string.Join(",", _Items);
            Assert.AreEqual(tmp, _ExpectedResult);
        }
    }
}