using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures.Deque;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Deque.Tests
{
    [TestClass()]
    public class ADequeTests
    {
        private ADeque<int> InitDeque()
        {
            ADeque<int> deque = new ADeque<int>();

            deque.EnqueueFirst(1);
            deque.EnqueueFirst(2);
            deque.EnqueueFirst(3);
            deque.EnqueueFirst(4);

            return deque;
        }

        [TestMethod()]
        public void Enqueue_First_Item_Test()
        {
            int expectedValue = 0;
            int expectedCount = 5;

            ADeque<int> deque = InitDeque();

            deque.EnqueueFirst(0);

            Assert.AreEqual(deque.PeekFirst(), expectedValue);
            Assert.AreEqual(deque.Count, expectedCount);
        }

        [TestMethod()]
        public void Enqueue_Last_Item_Test()
        {
            int expectedValue = 0;
            int expectedCount = 5;

            ADeque<int> deque = InitDeque();

            deque.EnqueueLast(0);

            Assert.AreEqual(deque.PeekLast(), expectedValue);
            Assert.AreEqual(deque.Count, expectedCount);
        }

        [TestMethod()]
        public void Dequeue_First_Item_Test()
        {
            int expectedValue = 4;
            int expectedCount = 3;

            ADeque<int> deque = InitDeque();

            int value = deque.DequeueFirst();

            Assert.AreEqual(value, expectedValue);
            Assert.AreEqual(deque.Count, expectedCount);
        }

        [TestMethod()]
        public void Dequeue_Last_Item_Test()
        {
            int expectedValue = 1;
            int expectedCount = 3;

            ADeque<int> deque = InitDeque();

            int value = deque.DequeueLast();

            Assert.AreEqual(value, expectedValue);
            Assert.AreEqual(deque.Count, expectedCount);
        }
    }
}