using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace BCEdit180.Converters {
    public class BindLeftBorderToTabConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            if (values == null || values == DependencyProperty.UnsetValue) {
                return values == null ? null : DependencyProperty.UnsetValue;
            }
            else if (values.Length != 2) {
                return "DEBUG_VALUES_NOT_SIZE_2: " + string.Join(", ", values.Select(e => $"{e.GetType()} -> {e}"));
            }

            if (values[0] is Thickness tabItem && values[1] is Thickness tabControl) {
                return new Thickness(tabControl.Left, tabItem.Top, tabItem.Right, tabItem.Bottom);
            }
            else {
                return "DEBUG_VALUES_NOT_THICKNESS: " + string.Join(", ", values.Select(e => $"{e.GetType()} -> {e}"));
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
