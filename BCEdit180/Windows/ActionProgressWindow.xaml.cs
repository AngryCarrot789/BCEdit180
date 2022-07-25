using BCEdit180.Core.Modals;
using BCEdit180.Core.Window;

namespace BCEdit180.Windows {
    /// <summary>
    /// Interaction logic for LoadingClassWindow.xaml
    /// </summary>
    public partial class ActionProgressWindow : WindowModal, IModalWindow<ActionProgressViewModel> {
        public ActionProgressViewModel Model {
            get => (ActionProgressViewModel) this.DataContext;
            set => this.DataContext = value;
        }

        public ActionProgressWindow() {
            InitializeComponent();
        }
    }
}
