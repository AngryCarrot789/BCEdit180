using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BCEdit180.Core.CodeEditing.Bytecode.Instructions;

namespace BCEdit180.CodeEditing.Bytecode.ListControls {
    /// <summary>
    /// Interaction logic for LdcInstructionItemControl.xaml
    /// </summary>
    public partial class LdcInstructionItemControl : UserControl {
        public LdcInstructionItemControl() {
            InitializeComponent();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ClickCount == 2) {
                ((LdcInstructionViewModel) this.DataContext).EditOpcode();
            }
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e) {
            if (e.ClickCount == 2) {
                ((LdcInstructionViewModel) this.DataContext).EditValueAction();
            }
        }
    }
}
