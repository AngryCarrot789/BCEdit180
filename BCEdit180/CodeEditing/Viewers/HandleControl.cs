using System.Windows.Controls;
using BCEdit180.Core.Editors;

namespace BCEdit180.CodeEditing.Viewers {
    public class HandleControl : Control {
        public HandleViewModel Handle {
            get => (HandleViewModel) this.DataContext;
            set => this.DataContext = value;
        }
    }
}