using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using BCEdit180.Core.Utils;

namespace BCEdit180.Converters {
    public class FQCNToWindowTitleConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || value == DependencyProperty.UnsetValue) {
                return value;
            }

            if (value is string fullName) {
                string className = StringUtils.GetTypeName(fullName);
                if (parameter is string format) {
                    return string.Format(format, className);
                }
                else {
                    return className;
                }
            }
            else {
                return "DEBUG_ERROR_NOT_STRING: " + value.GetType() + " -> " + value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException("Cannot convert back window title to class name");
        }
    }
}
