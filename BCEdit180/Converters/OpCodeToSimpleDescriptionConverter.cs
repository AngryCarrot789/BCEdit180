using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using BCEdit180.Core.CodeEditing;
using JavaAsm.Instructions;

namespace BCEdit180.Converters {
    public class OpCodeToSimpleDescriptionConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || value == DependencyProperty.UnsetValue) {
                return value;
            }

            if (value is Opcode opcode) {
                return InstructionDescriptionRegistry.GetDescription(opcode).Header;
            }

            return $"(Unknown object type: {value.GetType().Name} -> {value})";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}