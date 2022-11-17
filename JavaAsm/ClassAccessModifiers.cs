using System;
using System.Linq;

namespace JavaAsm {
    [Flags]
    public enum ClassAccessModifiers : ushort {
        Public = 1,
        Protected = 4,
        Private = 2,
        Static = 8,
        Abstract = 1024,
        Final = 16,
        Strict = 2048,
        Annotation = 8192,
        Enum = 16384,
        Interface = 512,
        Super = 32,
        Synthetic = 4096
    }

    [Flags]
    public enum MethodAccessModifiers : ushort {
        Public = 1,
        Protected = 4,
        Private = 2,
        Static = 8,
        Abstract = 1024,
        Syncrionized = 32,
        Final = 16,
        Native = 256,
        Strict = 2048,
        Bridge = 64,
        Synthetic = 4096,
        Varargs = 128
    }

    [Flags]
    public enum FieldAccessModifiers : ushort {
        Public = 1,
        Protected = 4,
        Private = 2,
        Static = 8,
        Transient = 128,
        Volatile = 64,
        Final = 16,
        Synthetic = 4096
    }

    public static class AccessModifiersExtensions {
        public static string ToString(ClassAccessModifiers accessModifiers) => string.Join(" ", Enum.GetValues(typeof(ClassAccessModifiers)).OfType<ClassAccessModifiers>().Where(x => accessModifiers.HasFlag(x)).Select(x => x.ToString().ToLower()));

        public static string ToString(MethodAccessModifiers accessModifiers) => string.Join(" ", Enum.GetValues(typeof(MethodAccessModifiers)).OfType<MethodAccessModifiers>().Where(x => accessModifiers.HasFlag(x)).Select(x => x.ToString().ToLower()));

        public static string ToString(FieldAccessModifiers accessModifiers) => string.Join(" ", Enum.GetValues(typeof(FieldAccessModifiers)).OfType<FieldAccessModifiers>().Where(x => accessModifiers.HasFlag(x)).Select(x => x.ToString().ToLower()));
    }
}