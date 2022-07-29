using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BCEdit180.Converters {
    public class TypeEqualityToTrueConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || value == DependencyProperty.UnsetValue) {
                return DependencyProperty.UnsetValue;
            }

            if (parameter is Type target) {
                return target.IsAssignableFrom(value is Type type ? type : value.GetType());
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}