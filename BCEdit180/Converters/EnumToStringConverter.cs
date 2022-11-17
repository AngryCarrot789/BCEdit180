using System;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace BCEdit180.Converters {
    public class EnumToStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || value == DependencyProperty.UnsetValue) {
                return value;
            }

            if (value is Enum e) {
                return new StringBuilder().Append("0x").Append(e.ToString("X")).Append(" (").Append(e).Append(")").ToString();
            }
            else {
                return "[Unknown type: " + value + "]";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
