using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using JavaAsm;

namespace BCEdit180.Converters {
    public class MethodDescriptorToStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) {
                return null;
            }

            if (value == DependencyProperty.UnsetValue) {
                return DependencyProperty.UnsetValue;
            }

            if (value is MethodDescriptor descriptor) {
                return descriptor.ToString();
            }

            return "DEBUG_ERROR";
            // throw new Exception("Cannot convert type " + value.GetType() + " to a string");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            try {
                return MethodDescriptor.Parse(value == null ? "()V" : value.ToString());
            }
            catch {
                return DependencyProperty.UnsetValue;
            }
        }
    }
}
