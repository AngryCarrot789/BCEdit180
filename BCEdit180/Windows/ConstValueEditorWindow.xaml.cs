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
using System.Windows.Shapes;
using System.Windows.Threading;
using BCEdit180.Core.Editors.Const;

namespace BCEdit180.Windows {
    /// <summary>
    /// Interaction logic for ConstValueEditorWindow.xaml
    /// </summary>
    public partial class ConstValueEditorWindow : WindowModal {
        public ConstValueEditorWindow() {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            switch (((ConstValueEditorViewModel) this.DataContext).Type) {
                case ConstType.Integer:
                    ApplySelection(this.TextBox_I);
                    break;
                case ConstType.Long:
                    ApplySelection(this.TextBox_L);
                    break;
                case ConstType.Float:
                    ApplySelection(this.TextBox_F);
                    break;
                case ConstType.Double:
                    ApplySelection(this.TextBox_D);
                    break;
                case ConstType.String:
                    ApplySelection(this.TextBox_S);
                    break;
                case ConstType.Class:
                    ApplySelection(this.TextBox_CN);
                    break;
                case ConstType.Handle:
                    break;
                case ConstType.MethodDescriptor:
                    ApplySelection(this.TextBox_MD);
                    break;
            }
        }

        private static void ApplySelection(TextBox text) {
            Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => {
                text.Focus();
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => {
                    text.SelectAll();

                }));
            }));

            // TODO: fix :)

            // somehow it breaks the tab control... selection gets all weird
            // Application.Current.Dispatcher.Invoke(text.Focus, DispatcherPriority.Background);
            // Application.Current.Dispatcher.Invoke(text.SelectAll, DispatcherPriority.Background);
        }
    }
}
