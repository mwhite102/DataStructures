using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructures.Queue.Tests
{
    [TestClass()]
    public class AQueueTests
    {
        private AQueue<int> InitQueue()
        {
            AQueue<int> queue = new AQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            return queue;
        }

        [TestMethod()]
        public void Should_Enqueue_Two_Items_Test()
        {
            int expectedCount = 2;
            AQueue<int> queue = InitQueue();
            Assert.AreEqual(queue.Count, expectedCount);
        }

        [TestMethod()]
        public void Should_Enqueue_Two_Items_And_Dequeue_First_Item_Test()
        {
            int expectedValue = 1;
            int expectedCount = 1;

            AQueue<int> queue = InitQueue();

            Assert.AreEqual(queue.Dequeue(), expectedValue);
            Assert.AreEqual(queue.Count, expectedCount);
        }

        [TestMethod()]
        public void Should_Enqueue_Two_Items_And_Peek_First_Item_Test()
        {
            int expectedValue = 1;
            int expectedCount = 2;

            AQueue<int> queue = InitQueue();

            Assert.AreEqual(queue.Peek(), expectedValue);
            Assert.AreEqual(queue.Count, expectedCount);
        }
    }
}