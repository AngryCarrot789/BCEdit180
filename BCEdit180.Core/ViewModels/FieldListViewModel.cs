using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BCEdit180.Core.Dialog;
using BCEdit180.Core.Editors;
using BCEdit180.Core.Searching;
using BCEdit180.Core.Utils;
using BCEdit180.Core.Window;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.ViewModels {
    public class FieldListViewModel : BaseViewModel, IDisposable, ISaveable<ClassNode> {
        public static IListSelector<FieldInfoViewModel> FieldList { get; set; }

        public ObservableCollection<FieldInfoViewModel> Fields { get; }
        public ObservableCollection<FieldInfoViewModel> RemovedFields { get; }

        private FieldInfoViewModel selectedField;
        public FieldInfoViewModel SelectedField {
            get => this.selectedField;
            set => RaisePropertyChanged(ref this.selectedField, value);
        }

        private int previousIndex;
        private int selectedIndex;
        public int SelectedIndex {
            get => this.selectedIndex;
            set {
                this.previousIndex = this.selectedIndex;
                RaisePropertyChanged(ref this.selectedIndex, value);
            }
        }

        public ICommand CreateFieldCommand { get; }

        public SearchFieldNameViewModel SearchField { get; }

        public ClassViewModel Class { get; }

        public FieldListViewModel(ClassViewModel clazz) {
            this.Class = clazz;
            this.Fields = new ObservableCollection<FieldInfoViewModel>();
            this.RemovedFields = new ObservableCollection<FieldInfoViewModel>();
            this.CreateFieldCommand = new RelayCommand(ShowCreateFieldDialog);
            this.SearchField = new SearchFieldNameViewModel(this);
        }

        public void ShowCreateFieldDialog() {
            if (Dialogs.TypeEditor.EditFieldDialog(out FieldEditorViewModel editor, true)) {
                CreateField(editor);
            }
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

            if (this.previousIndex >= 0 && this.previousIndex < this.Fields.Count) {
                this.SelectedIndex = this.previousIndex;
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

                field.Node.Attributes.Clear();
                field.Save(field.Node);
            }
        }

        public void Dispose() {
            this.SearchField.Dispose();
        }
    }
}
