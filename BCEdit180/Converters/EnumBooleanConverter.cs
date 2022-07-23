using System;
using System.Globalization;
using System.Windows.Data;

namespace BCEdit180.Converters {
    public class EnumBooleanConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return ((bool) value) ? parameter : Binding.DoNothing;
        }

        // public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
        //     if (parameter is string parameterString) {
        //         if (Enum.IsDefined(value.GetType(), value)) {
        //             return Enum.Parse(value.GetType(), parameterString).Equals(value);
        //         }
        //     }
        // 
        //     return DependencyProperty.UnsetValue;
        // }
        // 
        // public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
        //     if (parameter is string str) {
        //         return Enum.Parse(targetType, str);
        //     }
        // 
        //     return DependencyProperty.UnsetValue;
        // }
    }
}