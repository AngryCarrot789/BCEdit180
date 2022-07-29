using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BCEdit180.Converters {
    public class ItemNullToEnabledConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            bool invert = (parameter is string str && str == "Invert") ? false : true;

            if (value != null && value != DependencyProperty.UnsetValue) {
                return invert;
            }
            else {
                return !invert;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}