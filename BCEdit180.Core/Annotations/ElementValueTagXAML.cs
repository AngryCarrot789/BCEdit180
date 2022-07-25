using System;
using JavaAsm.CustomAttributes.Annotation;

namespace BCEdit180.Core.Annotations {
    public enum ElementValueTagXAML {
        Annotation = 64,
        Byte = 66,
        Character = 67,
        Double = 68,
        Float = 70,
        Integer = 73,
        Long = 74,
        Short = 83,
        Boolean = 90,
        Array = 91,
        Class = 99,
        Enum = 101,
        String = 115
    }

    public static class ElementValueTagHelper {
        public static ElementValue.ElementValueTag ToJavaAsm(this ElementValueTagXAML tag) {
            switch (tag) {
                case ElementValueTagXAML.Annotation: return ElementValue.ElementValueTag.Annotation;
                case ElementValueTagXAML.Byte: return ElementValue.ElementValueTag.Byte;
                case ElementValueTagXAML.Character: return ElementValue.ElementValueTag.Character;
                case ElementValueTagXAML.Double: return ElementValue.ElementValueTag.Double;
                case ElementValueTagXAML.Float: return ElementValue.ElementValueTag.Float;
                case ElementValueTagXAML.Integer: return ElementValue.ElementValueTag.Integer;
                case ElementValueTagXAML.Long: return ElementValue.ElementValueTag.Long;
                case ElementValueTagXAML.Short: return ElementValue.ElementValueTag.Short;
                case ElementValueTagXAML.Boolean: return ElementValue.ElementValueTag.Boolean;
                case ElementValueTagXAML.Array: return ElementValue.ElementValueTag.Array;
                case ElementValueTagXAML.Class: return ElementValue.ElementValueTag.Class;
                case ElementValueTagXAML.Enum: return ElementValue.ElementValueTag.Enum;
                case ElementValueTagXAML.String: return ElementValue.ElementValueTag.String;
                default: throw new ArgumentOutOfRangeException(nameof(tag), tag, null);
            }
        }

        public static ElementValueTagXAML FromJavaAsm(this ElementValue.ElementValueTag tag) {
            switch (tag) {
                case ElementValue.ElementValueTag.Annotation: return ElementValueTagXAML.Annotation;
                case ElementValue.ElementValueTag.Byte: return ElementValueTagXAML.Byte;
                case ElementValue.ElementValueTag.Character: return ElementValueTagXAML.Character;
                case ElementValue.ElementValueTag.Double: return ElementValueTagXAML.Double;
                case ElementValue.ElementValueTag.Float: return ElementValueTagXAML.Float;
                case ElementValue.ElementValueTag.Integer: return ElementValueTagXAML.Integer;
                case ElementValue.ElementValueTag.Long: return ElementValueTagXAML.Long;
                case ElementValue.ElementValueTag.Short: return ElementValueTagXAML.Short;
                case ElementValue.ElementValueTag.Boolean: return ElementValueTagXAML.Boolean;
                case ElementValue.ElementValueTag.Array: return ElementValueTagXAML.Array;
                case ElementValue.ElementValueTag.Class: return ElementValueTagXAML.Class;
                case ElementValue.ElementValueTag.Enum: return ElementValueTagXAML.Enum;
                case ElementValue.ElementValueTag.String: return ElementValueTagXAML.String;
                default: throw new ArgumentOutOfRangeException(nameof(tag), tag, null);
            }
        }
    }
}