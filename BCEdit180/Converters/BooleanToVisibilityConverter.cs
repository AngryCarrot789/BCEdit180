using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BCEdit180.Converters {
    public class BooleanToVisibilityConverter : IValueConverter {
        public Visibility TrueValue { get; set; }
        public Visibility FalseValue { get; set; }
        public Visibility UnsetValue { get; set; }
        public Visibility NonBoolValue { get; set; }

        public Visibility NullBoolValue { get; set; }

        public bool ThrowOnInvalidValue { get; set; }

        public BooleanToVisibilityConverter() {
            this.TrueValue = Visibility.Visible;
            this.FalseValue = Visibility.Collapsed;
            this.UnsetValue = Visibility.Hidden;
            this.NonBoolValue = Visibility.Hidden;
            this.NullBoolValue = Visibility.Hidden;
            this.ThrowOnInvalidValue = false;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) {
                return this.NullBoolValue;
            }
            else if (value == DependencyProperty.UnsetValue) {
                return this.UnsetValue;
            }
            else if (value is bool state) {
                return state ? this.TrueValue : this.FalseValue;
            }
            else if (this.ThrowOnInvalidValue) {
                throw new Exception($"Cannot convert {value} ({value.GetType().Name}) to bool");
            }
            else {
                return this.NonBoolValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is Visibility visibility) {
                return visibility == this.TrueValue;
            }
            else if (this.ThrowOnInvalidValue) {
                throw new Exception($"Cannot convert back {value} to visibility; expected boolean -> visibility");
            }
            else if (value == null) {
                return null;
            }
            else {
                return DependencyProperty.UnsetValue;
            }
        }
    }
}
