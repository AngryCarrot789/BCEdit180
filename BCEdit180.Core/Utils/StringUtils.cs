namespace BCEdit180.Core.Utils {
    public static class StringUtils {
        public static string Repeat(this char character, int count) {
            if (count < 0) {
            }

            return new string(character.RepeatAsArray(count));
        }

        public static char[] RepeatAsArray(this char character, int count) {
            char[] chars = new char[count];
            for (int i = 0; i < count; i++) {
                chars[i] = character;
            }

            return chars;
        }
    }
}
