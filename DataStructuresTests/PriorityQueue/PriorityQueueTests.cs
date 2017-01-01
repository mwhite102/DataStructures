using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructures.PriorityQueue.Tests
{
    [TestClass()]
    public class PriorityQueueTests
    {
        public PriorityQueue<int> InitPriorityQueue()
        {
            PriorityQueue<int> priorityQueue = new PriorityQueue<int>();

            priorityQueue.Enqueue(5);
            priorityQueue.Enqueue(10);

            return priorityQueue;
        }

        [TestMethod()]
        public void Priority_Queue_Add_Test()
        {
            int expectedCount = 2;

            PriorityQueue<int> queue = InitPriorityQueue();

            Assert.AreEqual(queue.Count, expectedCount);
        }

        [TestMethod()]
        public void Priority_Queue_Dequeue_Test()
        {
            int expectedCount = 1;
            int expectedValue = 10;

            PriorityQueue<int> queue = InitPriorityQueue();

            Assert.AreEqual(queue.Dequeue(), expectedValue);
            Assert.AreEqual(queue.Count, expectedCount);
        }

        [TestMethod()]
        public void Priority_Queue_Peek_Test()
        {
            int expectedCount = 2;
            int expectedValue = 10;

            PriorityQueue<int> queue = InitPriorityQueue();

            Assert.AreEqual(queue.Peek(), expectedValue);
            Assert.AreEqual(queue.Count, expectedCount);
        }
    }
}