using System.Windows.Controls;
using BCEdit180.Core.CodeEditing.Bytecode.Instructions;

namespace BCEdit180.CodeEditing.Bytecode.ListControls {
    /// <summary>
    /// Interaction logic for MethodInstructionItemControl.xaml
    /// </summary>
    public partial class MethodInstructionItemControl : UserControl {
        public MethodInstructionItemControl() {
            InitializeComponent();
        }

        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            if (e.ClickCount == 2) {
                ((BaseInstructionViewModel) this.DataContext).EditOpcode();
            }
        }
    }
}
