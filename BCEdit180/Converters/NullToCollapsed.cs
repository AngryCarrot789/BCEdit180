using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BCEdit180.Converters {
    public class NullToCollapsed : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Visibility state1 = (parameter is string str && str == "Invert") ? Visibility.Visible : Visibility.Collapsed;
            Visibility state2 = state1 == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

            if (value == null || value == DependencyProperty.UnsetValue) {
                return state1;
            }
            else {
                return state2;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}