using System.Collections.Generic;

namespace BCEdit180.Core.Utils {
    public static class CollectionUtils {
        public static void AddAll<T>(this ICollection<T> collection, IEnumerable<T> items) {
            foreach (T item in items) {
                collection.Add(item);
            }
        }
    }
}