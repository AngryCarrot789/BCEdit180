namespace BCEdit180 {
    public static class DebugUtils {
        public static string GetDebugString(object value) {
            return value == null ? "null (null)" : $"{value.GetType().Name} ({value})";
        }
    }
}