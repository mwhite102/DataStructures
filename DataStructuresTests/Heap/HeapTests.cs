using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructures.Heap.Tests
{
    [TestClass()]
    public class HeapTests
    {
        private Heap<int> InitHeap()
        {
            Heap<int> heap = new Heap<int>();

            heap.Add(8);
            heap.Add(6);
            heap.Add(5);
            heap.Add(3);
            heap.Add(4);

            return heap;
        }

        [TestMethod()]
        public void Add_Test()
        {
            Heap<int> heap = InitHeap();

            // Verify that the max value is 8 and count is 5
            Assert.AreEqual(heap.Peek(), 8);
            Assert.AreEqual(heap.Count, 5);

            // Add 10 to the heap
            heap.Add(10);

            // Verify that the max value is 10 and count is 6
            Assert.AreEqual(heap.Peek(), 10);
            Assert.AreEqual(heap.Count, 6);
        }

        [TestMethod()]
        public void RemoveMax_Test()
        {
            Heap<int> heap = InitHeap();

            // Verify that the max value is 8 and count is 5
            Assert.AreEqual(heap.Peek(), 8);
            Assert.AreEqual(heap.Count, 5);

            // RemoveMax
            int max = heap.RemoveMax();

            // Verify that the max value is 8 and count is 4
            Assert.AreEqual(max, 8);
            Assert.AreEqual(heap.Count, 4);
        }
    }
}