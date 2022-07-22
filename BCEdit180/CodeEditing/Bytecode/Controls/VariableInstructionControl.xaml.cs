using System.Windows.Controls;
using System.Windows.Input;
using JavaAsm.Instructions;

namespace BCEdit180.CodeEditing.Bytecode.Controls {
    /// <summary>
    /// Interaction logic for VariableInstructionControl.xaml
    /// </summary>
    public partial class VariableInstructionControl : UserControl {
        public static readonly Opcode[] ValidOpcodes = {
            Opcode.ILOAD, Opcode.LLOAD, Opcode.FLOAD, Opcode.DLOAD, Opcode.ALOAD, Opcode.ISTORE, Opcode.LSTORE, Opcode.FSTORE, Opcode.DSTORE, Opcode.ASTORE, Opcode.RET
        };

        public VariableInstructionControl() {
            InitializeComponent();
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            ViewManager.ShowEditInstructionView(ValidOpcodes, (o) => ((VariableInstructionViewModel) this.DataContext).Opcode = o);
        }
    }
}
