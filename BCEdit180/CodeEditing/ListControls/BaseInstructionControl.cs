using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BCEdit180.Core.CodeEditing.Bytecode.Instructions;
using BCEdit180.Core.Commands;

namespace BCEdit180.CodeEditing.ListControls {
    public class BaseInstructionControl : Control {
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

        public static readonly DependencyProperty UserAction1CommandProperty = DependencyProperty.Register("UserAction1Command", typeof(ICommand), typeof(BaseInstructionControl), new PropertyMetadata(null));
        public static readonly DependencyProperty UserAction2CommandProperty = DependencyProperty.Register("UserAction2Command", typeof(ICommand), typeof(BaseInstructionControl), new PropertyMetadata(null));
        public static readonly DependencyProperty UserAction3CommandProperty = DependencyProperty.Register("UserAction3Command", typeof(ICommand), typeof(BaseInstructionControl), new PropertyMetadata(null));
        public static readonly DependencyProperty UserAction4CommandProperty = DependencyProperty.Register("UserAction4Command", typeof(ICommand), typeof(BaseInstructionControl), new PropertyMetadata(null));

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
        public ICommand UserAction1Command {
            get => (ICommand) GetValue(UserAction1CommandProperty);
            set => SetValue(UserAction1CommandProperty, value);
        }

        [Category("Actions")]
        public ICommand UserAction2Command {
            get => (ICommand) GetValue(UserAction2CommandProperty);
            set => SetValue(UserAction2CommandProperty, value);
        }

        [Category("Actions")]
        public ICommand UserAction3Command {
            get => (ICommand) GetValue(UserAction3CommandProperty);
            set => SetValue(UserAction3CommandProperty, value);
        }

        [Category("Actions")]
        public ICommand UserAction4Command {
            get => (ICommand) GetValue(UserAction4CommandProperty);
            set => SetValue(UserAction4CommandProperty, value);
        }

        public BaseInstructionControl() {
            this.EditOpcodeCommand = new ExtendedRelayCommand(EditOpcodeAction, () => ((BaseInstructionViewModel) this.DataContext).CanEditOpCode);
            this.UserAction1Command = new ExtendedRelayCommand(OnUserAction1, CanExecuteUserAction1);
            this.UserAction2Command = new ExtendedRelayCommand(OnUserAction2, CanExecuteUserAction2);
            this.UserAction3Command = new ExtendedRelayCommand(OnUserAction3, CanExecuteUserAction3);
            this.UserAction4Command = new ExtendedRelayCommand(OnUserAction4, CanExecuteUserAction4);
        }

        public virtual void EditOpcodeAction() {
            if (((BaseInstructionViewModel) this.DataContext).CanEditOpCode) {
                ((BaseInstructionViewModel) this.DataContext).EditOpcode();
            }
        }

        public virtual bool CanExecuteUserAction1() {
            return true;
        }

        public virtual bool CanExecuteUserAction2() {
            return true;
        }

        public virtual bool CanExecuteUserAction3() {
            return true;
        }

        public virtual bool CanExecuteUserAction4() {
            return true;
        }

        public virtual void OnUserAction1() {
            EditOpcodeAction();
        }

        public virtual void OnUserAction2() {
            EditOpcodeAction();
        }

        public virtual void OnUserAction3() {
            EditOpcodeAction();
        }

        public virtual void OnUserAction4() {
            EditOpcodeAction();
        }

        public virtual void UserActionKeyboard() {
            OnUserAction1();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e) {
            base.OnPreviewKeyDown(e);
            if (e.Key == Key.Enter) {
                UserActionKeyboard();
            }
        }
    }
}