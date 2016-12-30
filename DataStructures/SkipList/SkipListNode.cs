namespace DataStructures.SkipList
{
    public class SkipListNode<T>
    {
        /// <summary>
        /// Creates a new node with value and indicated link height
        /// </summary>
        /// <param name="value">The value of the SkipListNode</param>
        /// <param name="height">The height of the node link list</param>
        public SkipListNode(T value, int height)
        {
            Value = value;
            Next = new SkipListNode<T>[height];
        }

        /// <summary>
        /// Gets the array of node links
        /// </summary>
        public SkipListNode<T>[] Next { get; private set; }

        /// <summary>
        /// Gets the SkipListNode Value
        /// </summary>
        public T Value { get; private set; }
    }
}
