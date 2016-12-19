namespace DataStructures.DoublyLinkedList
{
    public class DoublyLinkedListNode<T>
    {
        /// <summary>
        /// DoublyLinkedListNode Constructor
        /// </summary>
        /// <param name="value">The value of the LinkedListNode</param>
        public DoublyLinkedListNode(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets Value
        /// </summary>
        public T Value { get; internal set; }

        /// <summary>
        /// The previous node in the list
        /// </summary>
        public DoublyLinkedListNode<T> Previous { get; set; }

        /// <summary>
        /// The next node in the list
        /// </summary>
        public DoublyLinkedListNode<T> Next { get; set; }
    }
}
