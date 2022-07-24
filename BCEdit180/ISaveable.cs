namespace BCEdit180 {
    // This doesn't have any purpose, apart from readability
    public interface ISaveable<T> {
        /// <summary>
        /// Loads the contents from the value into this class
        /// </summary>
        void Load(T node);

        /// <summary>
        /// Saves this class's content into the given value's contents
        /// </summary>
        void Save(T node);
    }
}