using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BCEdit180.Core.CodeEditing.Bytecode.Instructions;
using BCEdit180.Core.Commands;

namespace BCEdit180.CodeEditing.ListControls {
    public class BaseInstructionControl : Control {
        public static readonly DependencyProperty OpcodeTextBrushProperty =
            DependencyProperty.Register(
                "OpcodeTextBrush",
                typeof(Brush),
                typeof(BaseInstructionControl),
                new PropertyMetadata(new SolidColorBrush(Colors.Orange)));

        [Category("Brush")]
        public Brush OpcodeTextBrush {
            get => (Brush) GetValue(OpcodeTextBrushProperty);
            set => SetValue(OpcodeTextBrushProperty, value);
        }

        public BaseInstructionControl() {

        }
    }
}