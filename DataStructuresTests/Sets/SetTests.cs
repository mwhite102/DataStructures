using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Sets.Tests
{
    [TestClass()]
    public class SetTests
    {
        [TestMethod()]
        public void Add_Test()
        {
            int expectedCount = 2;
            Set<int> set = new Set<int>();

            set.Add(1);
            set.Add(2);

            Assert.AreEqual(set.Count, expectedCount);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Add_Should_Throw_Inavlid_Op_Exception_Test()
        {
            Set<int> set = new Set<int>();

            set.Add(1);
            set.Add(1);
        }

        [TestMethod()]
        public void Add_Range_Test()
        {
            int expectedCount = 2;
            Set<int> set = new Set<int>();

            List<int> items = new List<int>() { 1, 2 };

            set.AddRange(items);

            Assert.AreEqual(set.Count, expectedCount);
        }

        [TestMethod()]
        public void Remove_Item_Test()
        {
            int expectedCount = 2;
            Set<int> set = new Set<int>();

            List<int> items = new List<int>() { 1, 2, 3 };

            set.AddRange(items);

            set.Remove(3);

            Assert.AreEqual(set.Count, expectedCount);
        }

        [TestMethod()]
        public void Contains_Test()
        {
            Set<int> set = new Set<int>();

            set.Add(1);
            set.Add(2);

            Assert.IsTrue(set.Contains(1));
            Assert.IsTrue(set.Contains(2));
            Assert.IsFalse(set.Contains(3));
        }

        [TestMethod()]
        public void Union_Test()
        {
            string expectedString = "1,2,3,4,5,6";
            Set<int> set1 = new Set<int>(new List<int> { 1, 2, 3, 4 });
            Set<int> set2 = new Set<int>(new List<int> { 3, 4, 5, 6 });

            Set<int> output = set1.Union(set2);

            string resultString = string.Join(",", output.ToArray());

            Assert.AreEqual(expectedString, resultString);
        }

        [TestMethod()]
        public void Intersection_Test()
        {
            string expectedString = "3,4";
            Set<int> set1 = new Set<int>(new List<int> { 1, 2, 3, 4 });
            Set<int> set2 = new Set<int>(new List<int> { 3, 4, 5, 6 });

            Set<int> output = set1.Intersection(set2);

            string resultString = string.Join(",", output.ToArray());

            Assert.AreEqual(expectedString, resultString);
        }

        [TestMethod()]
        public void Difference_Test()
        {
            string expectedString = "1,2";
            Set<int> set1 = new Set<int>(new List<int> { 1, 2, 3, 4 });
            Set<int> set2 = new Set<int>(new List<int> { 3, 4, 5, 6 });

            Set<int> output = set1.Difference(set2);

            string resultString = string.Join(",", output.ToArray());

            Assert.AreEqual(expectedString, resultString);
        }

        [TestMethod()]
        public void Symmetric_Difference_Test()
        {
            string expectedString = "1,2,5,6";
            Set<int> set1 = new Set<int>(new List<int> { 1, 2, 3, 4 });
            Set<int> set2 = new Set<int>(new List<int> { 3, 4, 5, 6 });

            Set<int> output = set1.SymmetricalDifference(set2);

            string resultString = string.Join(",", output.ToArray());

            Assert.AreEqual(expectedString, resultString);
        }
    }
}