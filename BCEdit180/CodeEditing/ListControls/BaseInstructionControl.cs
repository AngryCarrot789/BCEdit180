using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BCEdit180.Core.CodeEditing.Bytecode.Instructions;
using BCEdit180.Core.Commands;

namespace BCEdit180.CodeEditing.NewListControls {
    public class BaseInstructionControl : ContentControl {
        public static readonly DependencyProperty OpcodeTextBrushProperty =
            DependencyProperty.Register(
                "OpcodeTextBrush",
                typeof(Brush),
                typeof(BaseInstructionControl),
                new PropertyMetadata(new SolidColorBrush(Colors.Orange)));

        public static readonly DependencyProperty EditOpcodeCommandProperty =
            DependencyProperty.Register(
                "EditOpcodeCommand",
                typeof(ICommand),
                typeof(BaseInstructionControl),
                new PropertyMetadata(null));

        public static readonly DependencyProperty UserActionCommandProperty =
            DependencyProperty.Register(
                "UserActionCommand",
                typeof(ICommand),
                typeof(BaseInstructionControl),
                new PropertyMetadata(null));

        [Category("Brush")]
        public Brush OpcodeTextBrush {
            get => (Brush) GetValue(OpcodeTextBrushProperty);
            set => SetValue(OpcodeTextBrushProperty, value);
        }

        [Category("Actions")]
        public ICommand EditOpcodeCommand {
            get => (ICommand) GetValue(EditOpcodeCommandProperty);
            set => SetValue(EditOpcodeCommandProperty, value);
        }

        [Category("Actions")]
        public ICommand UserActionCommand {
            get => (ICommand) GetValue(UserActionCommandProperty);
            set => SetValue(UserActionCommandProperty, value);
        }

        public BaseInstructionControl() {
            this.EditOpcodeCommand = new ExtendedRelayCommand(EditOpcodeAction, () => ((BaseInstructionViewModel) this.DataContext).CanEditOpCode);
            this.UserActionCommand = new ExtendedRelayCommand(OnUserAction, CanExecuteUserAction);
        }

        public virtual void EditOpcodeAction() {
            if (((BaseInstructionViewModel) this.DataContext).CanEditOpCode) {
                ((BaseInstructionViewModel) this.DataContext).EditOpcode();
            }
        }

        public virtual bool CanExecuteUserAction() {
            return true;
        }

        public virtual void OnUserAction() {
            EditOpcodeAction();
        }

        public virtual void UserActionKeyboard() {
            OnUserAction();
        }

        public virtual void UserActionDoubleClick() {
            OnUserAction();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e) {
            base.OnPreviewKeyDown(e);
            if (e.Key == Key.Enter) {
                UserActionKeyboard();
            }
        }

        protected override void OnPreviewMouseDoubleClick(MouseButtonEventArgs e) {
            UserActionDoubleClick();
            base.OnPreviewMouseDoubleClick(e);
        }
    }
}