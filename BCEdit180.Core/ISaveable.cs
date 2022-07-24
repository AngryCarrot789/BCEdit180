namespace BCEdit180.Core {
    // This doesn't have any purpose, apart from readability

    /// <summary>
    /// A saveable object, that can load from an object, and save into an object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISaveable<in T> {
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