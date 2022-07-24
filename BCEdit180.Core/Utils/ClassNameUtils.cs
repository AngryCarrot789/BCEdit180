using JavaAsm;

namespace BCEdit180.Core.Utils {
    public static class ClassNameUtils {
        public static ClassName AsNonDescriptorName(this ClassName name) {
            if (string.IsNullOrEmpty(name.Name)) {
                return new ClassName(name.Name);
            }

            if (name.Name[0] == 'L' && name.Name[name.Name.Length - 1] == ';') {
                return new ClassName(name.Name.Substring(1, name.Name.Length - 2));
            }
            else {
                return new ClassName(name.Name);
            }
        }

        public static ClassName AsDescriptorName(this ClassName name) {
            if (string.IsNullOrEmpty(name.Name)) {
                return new ClassName(name.Name);
            }

            if (name.Name[0] == 'L' && name.Name[name.Name.Length - 1] == ';') {
                return new ClassName(name.Name);
            }
            else {
                return new ClassName("L" + name.Name + ";");
            }
        }
    }
}