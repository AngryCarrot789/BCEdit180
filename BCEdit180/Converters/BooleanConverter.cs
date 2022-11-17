using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BCEdit180.Converters {
    public class BooleanConverter : IValueConverter {
        public object TrueValue { get; set; }

        public object FalseValue { get; set; }

        public bool ReturnFalseIfUnset { get; set; }

        public object UnsetValue { get; set; }

        public BooleanConverter() {
            this.ReturnFalseIfUnset = false;
            this.UnsetValue = DependencyProperty.UnsetValue;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == this.UnsetValue) {
                return this.ReturnFalseIfUnset ? this.FalseValue : this.UnsetValue;
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
                return this.ReturnFalseIfUnset ? false : this.UnsetValue;
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
