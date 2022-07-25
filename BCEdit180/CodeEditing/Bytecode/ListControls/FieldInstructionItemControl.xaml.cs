using System.Windows.Controls;
using BCEdit180.Core.CodeEditing.Bytecode.Instructions;

namespace BCEdit180.CodeEditing.Bytecode.ListControls {
    /// <summary>
    /// Interaction logic for FieldInstructionItemControl.xaml
    /// </summary>
    public partial class FieldInstructionItemControl : UserControl {
        public FieldInstructionItemControl() {
            InitializeComponent();
        }

        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            if (e.ClickCount == 2) {
                ((BaseInstructionViewModel) this.DataContext).EditOpcode();
            }
        }
    }
}
