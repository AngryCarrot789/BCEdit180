using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BCEdit180.Converters {
    public class NullConverter : IValueConverter {
        public object NullValue { get; set; }
        public object NonNullValue { get; set; }

        public bool ReturnUnsetIfUnset { get; set; }

        public object UnsetValue { get; set; }

        public NullConverter() {
            this.ReturnUnsetIfUnset = true;
            this.UnsetValue = DependencyProperty.UnsetValue;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == this.UnsetValue && this.ReturnUnsetIfUnset) {
                return this.UnsetValue;
            }

            return value == null ? this.NullValue : this.NonNullValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}