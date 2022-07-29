using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace BCEdit180.Converters {
    public class BooleanToColourConverter : IValueConverter {
        public SolidColorBrush TrueColour { get; set; }

        public SolidColorBrush FalseColour { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || value == DependencyProperty.UnsetValue) {
                return DependencyProperty.UnsetValue;
            }

            if (value is bool state) {
                bool invert = parameter is string text && text == "Invert";
                if (state) {
                    return invert ? this.FalseColour : this.TrueColour;
                }
                else {
                    return invert ? this.TrueColour : this.FalseColour;
                }
            }
            else {
                return new SolidColorBrush(Colors.Red);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}