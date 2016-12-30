namespace DataStructures.HashTable
{
    /// <summary>
    /// A node in hash table array
    /// </summary>
    /// <typeparam name="TKey">The type of the key</typeparam>
    /// <typeparam name="TValue">The type of the value</typeparam>
    public class HashTableNodePair<TKey, TValue>
    {
        /// <summary>
        /// Creates a HashTableNodePair object
        /// </summary>
        /// <param name="key">The key value</param>
        /// <param name="value">The value</param>
        public HashTableNodePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Gets the key value.  Used for indexing in the hash table
        /// </summary>
        public TKey Key { get; private set; }

        /// <summary>
        /// Gets or sets the Value
        /// </summary>
        public TValue Value { get; set; }
    }
}
