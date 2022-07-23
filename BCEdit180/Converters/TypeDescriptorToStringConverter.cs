using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using JavaAsm;

namespace BCEdit180.Converters {
    public class TypeDescriptorToStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) {
                return null;
            }

            if (value == DependencyProperty.UnsetValue) {
                return DependencyProperty.UnsetValue;
            }

            if (value is TypeDescriptor || value is MethodDescriptor) {
                return value.ToString();
            }

            return "DEBUG_ERROR_NOT_DESCRIPTOR: " + (value == null ? "null" : (value.GetType() + " -> " + value));
            // throw new Exception("Cannot convert type " + value.GetType() + " to a string");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            string str = value?.ToString() ?? "";
            if (str.Length == 0) {
                return DependencyProperty.UnsetValue;
            }
            else if (str[0] == '(') {
                try {
                    return MethodDescriptor.Parse(str);
                }
                catch {
                    Debug.WriteLine("Failed to parse MethodDescriptor for value: " + value);
                    return DependencyProperty.UnsetValue;
                }
            }
            else {
                try {
                    return TypeDescriptor.Parse(str);
                }
                catch {
                    Debug.WriteLine("Failed to parse TypeDescriptor for value: " + value);
                    return DependencyProperty.UnsetValue;
                }
            }
        }
    }
}
