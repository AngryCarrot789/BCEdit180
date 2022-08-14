using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using BCEdit180.Core.CodeEditing;
using JavaAsm;

namespace BCEdit180.CodeEditing.Viewers {
    public class FieldDescriptorControl : Control {
        public TypeDescriptorViewModel TypeDescriptor {
            get => (TypeDescriptorViewModel) this.DataContext;
            set => this.DataContext = value;
        }
    }
}
