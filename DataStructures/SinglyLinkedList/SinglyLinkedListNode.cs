namespace DataStructures.SinglyLinkedList
{
    public class SinglyLinkedListNode<T>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value of the LinkedListNode</param>
        public SinglyLinkedListNode(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets Value
        /// </summary>
        public T Value { get; internal set; }

        /// <summary>
        /// The next node in the list
        /// </summary>
        public SinglyLinkedListNode<T> Next { get; set; }
    }
}
