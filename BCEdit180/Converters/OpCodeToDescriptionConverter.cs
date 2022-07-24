using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using BCEdit180.CodeEditing;
using BCEdit180.Core.CodeEditing;
using JavaAsm.Instructions;

namespace BCEdit180.Converters {
    public class OpCodeToDescriptionConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || value == DependencyProperty.UnsetValue || !(value is Opcode)) {
                return value;
            }

            return InstructionDescriptionRegistry.GetDescription((Opcode) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
