using System;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;
using JavaAsm;
using JavaAsm.Instructions.Types;

namespace BCEdit180.Converters {
    public class ConstPreviewConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || value == DependencyProperty.UnsetValue) {
                return value;
            }
            else if (value is int || value is long) {
                return value;
            }
            else if (value is float) {
                return value + "f";
            }
            else if (value is double) {
                return value + "d";
            }
            else if (value is string) {
                return new StringBuilder().Append('\"').Append(value).Append('\"');
            }
            else if (value is ClassName) {
                return "class: " + value;
            }
            else if (value is Handle) {
                return "handle: " + value;
            }
            else if (value is MethodDescriptor) {
                return "MD: " + value;
            }
            else {
                return "DEBUG_ERROR_UNKNOWN_TYPE: " + DebugUtils.GetDebugString(value);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException("Cannot convert preview back to actual value");
        }
    }
}