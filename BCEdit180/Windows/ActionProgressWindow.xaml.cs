using BCEdit180.Core.Modals;
using BCEdit180.Core.Window;
using BCEdit180.Windows.Base;

namespace BCEdit180.Windows {
    /// <summary>
    /// Interaction logic for LoadingClassWindow.xaml
    /// </summary>
    public partial class ActionProgressWindow : DialogBase, IModalWindow<ActionProgressViewModel> {
        public ActionProgressViewModel Model {
            get => (ActionProgressViewModel) this.DataContext;
            set => this.DataContext = value;
        }

        public ActionProgressWindow() {
            InitializeComponent();
        }
    }
}
