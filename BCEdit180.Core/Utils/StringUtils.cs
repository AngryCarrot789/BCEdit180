using System;
using System.Text;

namespace BCEdit180.Core.Utils {
    public static class StringUtils {
        public static string Repeat(this char character, int count) {
            if (count < 0) {
                throw new IndexOutOfRangeException("Array size must be a positive number");
            }
            else if (count == 0) {
                return "";
            }
            else {
                return new string(character.RepeatAsArray(count));
            }
        }

        public static char[] RepeatAsArray(this char character, int count) {
            char[] chars = new char[count];
            for (int i = 0; i < count; i++) {
                chars[i] = character;
            }

            return chars;
        }

        public static string RemoveChar(this string value, char character, int startIndex = 0) {
            int index = value.IndexOf(character);
            if (index == -1) {
                return value;
            }

            StringBuilder sb = new StringBuilder(value.Length);
            for (int i = 0; i < value.Length; i++) {
                if (value[i] != character) {
                    sb.Append(value[i]);
                }
            }

            return sb.ToString();
        }

        public static int CountCharsAtStart(this string str, char character, int startIndex = 0) {
            for (int i = startIndex; i < str.Length; i++) {
                if (str[i] != character) {
                    return i;
                }
            }

            return str.Length - startIndex;
        }

        public static string GetTypeName(string className) {
            if (string.IsNullOrEmpty(className)) {
                return "";
            }

            int lastIndex = className.LastIndexOf('/');
            if (lastIndex == -1) {
                return className;
            }
            else {
                return className.Substring(lastIndex + 1);
            }
        }

        public static string GetNonDescriptiveTypeName(string className) {
            if (string.IsNullOrEmpty(className)) {
                return "";
            }

            // Remove L; (Lpackage/Class;)
            if (className.Length > 0 && className[0] == 'L' && className[className.Length - 1] == ';') {
                className = className.Substring(1, className.Length - 2);
            }

            int lastIndex = className.LastIndexOf('/');
            if (lastIndex != -1) {
                className = className.Substring(lastIndex + 1);
            }

            return className;
        }
    }
}
