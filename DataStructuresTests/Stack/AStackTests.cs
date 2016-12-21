using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructures.Stack.Tests
{
    [TestClass()]
    public class AStackTests
    {
        private AStack<int> InitStack()
        {
            AStack<int> stack = new AStack<int>();
            stack.Push(1);
            stack.Push(2);
            return stack;
        }

        [TestMethod()]
        public void Should_Push_Two_Items_On_Stack_Test()
        {
            int expectedValue = 2;
            AStack<int> stack = InitStack();

            Assert.AreEqual(stack.Peek(), expectedValue);
        }

        [TestMethod()]
        public void Should_Push_Two_Items_And_Pop_One_Off_Stack_Test()
        {
            int expectedValue = 1;
            AStack<int> stack = InitStack();

            int result = stack.Pop();

            Assert.AreEqual(stack.Peek(), expectedValue);
            Assert.AreEqual(stack.Count, expectedValue);
        }
    }
}