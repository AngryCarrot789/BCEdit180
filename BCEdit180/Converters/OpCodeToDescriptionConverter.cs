using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using BCEdit180.CodeEditing.InstructionEdit;
using JavaAsm.Instructions;

namespace BCEdit180.Converters {
    public class OpCodeToDescriptionConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || value == DependencyProperty.UnsetValue) {
                return value;
            }

            return value is Opcode ? InstructionDescriptionRegistry.GetDescription((Opcode) value) : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
