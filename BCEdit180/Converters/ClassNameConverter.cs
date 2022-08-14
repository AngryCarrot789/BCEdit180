using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using JavaAsm;

namespace BCEdit180.Converters {
    public class ClassNameConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || value == DependencyProperty.UnsetValue) {
                return DependencyProperty.UnsetValue;
            }

            if (value is ClassName name) {
                return name.Name;
            }

            return "DEBUG_ERROR_NOT_CLASSNAME: " + value.GetType() + " -> " + value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || value == DependencyProperty.UnsetValue) {
                return DependencyProperty.UnsetValue;
            }

            if (value is string name) {
                if (ClassName.TryParse(name, out ClassName className)) {
                    return className;
                }
                else {
                    return new ClassName("");
                }
            }

            return "DEBUG_ERROR_NOT_STRING: " + value.GetType() + " -> " + value;
        }
    }
}