using System.Windows.Controls;
using BCEdit180.Core.CodeEditing.Descriptors;
using BCEdit180.Core.Editors;

namespace BCEdit180.CodeEditing.Viewers {
    public class TypeDescriptorControl : Control {
        public TypeDescViewModel TypeDesc {
            get => (TypeDescViewModel) this.DataContext;
            set => this.DataContext = value;
        }
    }
}
