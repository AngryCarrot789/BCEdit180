using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BCEdit180.Converters {
    public class BooleanToVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) {
                return null;
            }

            if (value == DependencyProperty.UnsetValue) {
                return DependencyProperty.UnsetValue;
            }

            if (value is bool visibility) {
                if (visibility) {
                    return Visibility.Visible;
                }
                else {
                    return parameter != null ? Visibility.Hidden : Visibility.Collapsed;
                }
            }

            return "DEBUG_ERROR";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
