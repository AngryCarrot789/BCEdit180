using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using JavaAsm;

namespace BCEdit180.Converters {
    [ValueConversion(typeof(PrimitiveType), typeof(string))]
    public class PrimitiveConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || value == DependencyProperty.UnsetValue) {
                return value;
            }

            if (value is PrimitiveType) {
                switch ((PrimitiveType) value) {
                    case PrimitiveType.Boolean:   return "boolean";
                    case PrimitiveType.Byte:      return "byte";
                    case PrimitiveType.Character: return "char";
                    case PrimitiveType.Double:    return "double";
                    case PrimitiveType.Float:     return "float";
                    case PrimitiveType.Integer:   return "int";
                    case PrimitiveType.Long:      return "long";
                    case PrimitiveType.Short:     return "short";
                    case PrimitiveType.Void:      return "void";
                    default: throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
            }
            else {
                return "DEBUG_ERROR_CONVERT_NOT_PRIMITIVE_TYPE: " + (value.GetType() + " -> " + value);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || value == DependencyProperty.UnsetValue) {
                return value;
            }

            if (value is string) {
                switch ((string) value) {
                    case "boolean":
                    case "bool":
                    case "Z":
                        return PrimitiveType.Boolean;
                    case "byte":
                    case "B":
                        return PrimitiveType.Byte;
                    case "char":
                    case "C":
                        return PrimitiveType.Character;
                    case "double":
                    case "D":
                        return PrimitiveType.Double;
                    case "float":
                    case "F":
                        return PrimitiveType.Float;
                    case "int":
                    case "I":
                        return PrimitiveType.Integer;
                    case "long":
                    case "J":
                        return PrimitiveType.Long;
                    case "short":
                    case "S":
                        return PrimitiveType.Short;
                    case "void":
                    case "V":
                        return PrimitiveType.Void;
                    default: return DependencyProperty.UnsetValue;
                }
            }
            else {
                return "DEBUG_ERROR_CONVERTBACK_NOT_STRING: " + (value.GetType() + " -> " + value);
            }
        }
    }
}