using System.Collections.ObjectModel;
using System.Windows.Input;
using BCEdit180.Core.Dialogs;
using BCEdit180.Core.Editors;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.ViewModels {
    public class FieldListViewModel : BaseViewModel, ISaveable<ClassNode> {
        public ObservableCollection<FieldInfoViewModel> Fields { get; }
        public ObservableCollection<FieldInfoViewModel> RemovedFields { get; }

        private FieldInfoViewModel selectedField;
        public FieldInfoViewModel SelectedField {
            get => this.selectedField;
            set => RaisePropertyChanged(ref this.selectedField, value);
        }

        public ICommand CreateFieldCommand { get; }

        public ClassViewModel Class { get; }

        public FieldListViewModel(ClassViewModel classViewModel) {
            this.Class = classViewModel;
            this.Fields = new ObservableCollection<FieldInfoViewModel>();
            this.RemovedFields = new ObservableCollection<FieldInfoViewModel>();
            this.CreateFieldCommand = new RelayCommand(ShowCreateFieldDialog);
        }

        public async void ShowCreateFieldDialog() {
            CreateField(await Dialog.TypeEditor.EditFieldDialog(true, null));
        }

        public void CreateField(FieldEditorViewModel editor) {
            this.Fields.Add(new FieldInfoViewModel(this, CreateFieldForVM(editor)) { IsCreatedRuntime = true });
        }

        private FieldNode CreateFieldForVM(FieldEditorViewModel editor) {
            FieldNode method = new FieldNode() {
                Owner = this.Class.Node,
                Name = editor.FieldName,
                Descriptor =  editor.Descriptor,
                Access = editor.Access,
            };

            return method;
        }

        public void Load(ClassNode node) {
            this.Fields.Clear();
            this.RemovedFields.Clear();
            foreach (FieldNode field in node.Fields) {
                this.Fields.Add(new FieldInfoViewModel(this, field));
            }
        }

        public void Save(ClassNode node) {
            foreach (FieldInfoViewModel removed in this.RemovedFields) {
                node.Fields.Remove(removed.Node);
            }

            this.RemovedFields.Clear();
            foreach (FieldInfoViewModel field in this.Fields) {
                if (field.IsCreatedRuntime) {
                    field.IsCreatedRuntime = false;
                    node.Fields.Add(field.Node);
                }

                field.Save(field.Node);
            }
        }
    }
}
