using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BCEdit180.Converters {
    public class NullToBoolConverter : IValueConverter {
        public bool NullValue { get; set; }

        public bool ReturnUnsetIfUnset { get; set; }

        public object UnsetValue { get; set; }

        public NullToBoolConverter() {
            this.ReturnUnsetIfUnset = true;
            this.UnsetValue = DependencyProperty.UnsetValue;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == this.UnsetValue && this.ReturnUnsetIfUnset) {
                return this.UnsetValue;
            }

            if (value == null) {
                return this.NullValue;
            }
            else {
                return this.NonNullValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}