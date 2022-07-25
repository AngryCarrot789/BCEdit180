using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BCEdit180.Core.Window;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Editors {
    public class MethodDescEditorViewModel : BaseViewModel {
        private TypeDescriptor returnType;
        public TypeDescriptor ReturnType {
            get => this.returnType;
            set => RaisePropertyChanged(ref this.returnType, value);
        }

        public ObservableCollection<TypeDescriptorViewModel> Parameters { get; }

        private MethodAccessModifiers access;
        public MethodAccessModifiers Access {
            get => this.access;
            set => RaisePropertyChanged(ref this.access, value);
        }

        public ICommand EditReturnTypeCommand { get; }

        public ICommand AddNewParameterCommand { get; }

        public ICommand ApplyChangesCommand { get; }

        public ICommand CancelChangesCommand { get; }

        public ICommand EditAccessCommand { get; }

        public bool IsCancelled { get; set; }

        public MethodDescEditorViewModel() {
            this.Parameters = new ObservableCollection<TypeDescriptorViewModel>();
            this.ReturnType = new TypeDescriptor(PrimitiveType.Void, 0);
            this.Access = MethodAccessModifiers.Public;
            this.AddNewParameterCommand = new RelayCommand(() => {
                AddNewParameter();
            });

            this.EditReturnTypeCommand = new RelayCommand(()=> {
                EditReturnType();
            });

            this.EditAccessCommand = new RelayCommand(() => {
                EditAccess();
            });

            this.ApplyChangesCommand = new RelayCommand(() => this.IsCancelled = false);
            this.CancelChangesCommand = new RelayCommand(() => this.IsCancelled = true);
        }

        public async Task EditReturnType() {
            this.ReturnType = await Dialog.TypeEditor.EditTypeDescriptorDialog(this.ReturnType);
        }

        public async Task AddNewParameter() {
            TypeDescriptor descriptor = await Dialog.TypeEditor.EditTypeDescriptorDialog(new TypeDescriptor(PrimitiveType.Integer, 0));
            this.Parameters.Add(new TypeDescriptorViewModel() {
                Descriptor = descriptor
            });
        }

        public async Task EditAccess() {
            MethodAccessModifiers? modifier = await Dialog.AccessEditor.EditMethodAccess(this.Access);
            if (modifier.HasValue) {
                this.Access = modifier.Value;
            }
        }
    }
}