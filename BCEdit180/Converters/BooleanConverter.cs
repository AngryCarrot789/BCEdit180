using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BCEdit180.Converters {
    public class BooleanConverter : IValueConverter {
        public object TrueValue { get; set; }

        public object FalseValue { get; set; }

        public bool ReturnUnsetIfUnset { get; set; }

        public object UnsetValue { get; set; }

        public BooleanConverter() {
            this.ReturnUnsetIfUnset = true;
            this.UnsetValue = DependencyProperty.UnsetValue;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == this.UnsetValue) {
                return this.ReturnUnsetIfUnset ? this.UnsetValue : this.FalseValue;
            }
            else if (value is bool state && state) {
                return this.TrueValue;
            }
            else {
                return this.FalseValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == this.UnsetValue) {
                return this.ReturnUnsetIfUnset ? this.UnsetValue : false;
            }
            else if (value == this.TrueValue) {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
