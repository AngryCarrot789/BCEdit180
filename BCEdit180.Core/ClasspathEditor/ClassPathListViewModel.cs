using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using BCEdit180.Core.Modals;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.ClasspathEditor {
    public class ClassPathListViewModel : BaseViewModel {
        public static ClassPathListViewModel Instance {
            get => ServiceManager.GetViewModel<ClassPathListViewModel>();
            private set => ServiceManager.SetViewModel(value);
        }

        public static ICommand ShowEditorCommand { get; }

        static ClassPathListViewModel() {
            Instance = new ClassPathListViewModel();
            ShowEditorCommand = new RelayCommand(()=> {
                ClassPathListViewModel list = Instance;
                ServiceManager.GetService<IModalManager>().ShowDialog(in list, out ClassPathListViewModel _);
            });
        }

        public ObservableCollection<ClassPathItemViewModel> ClassPathItems { get; }

        private string classPathString;

        public string ClassPathString {
            get => this.classPathString;
            set => RaisePropertyChanged(ref this.classPathString, value);
        }

        public ICommand AddFileCommand { get; }

        public ICommand AddFolderCommand { get; }

        public ICommand GenerateListCommand { get; }
        public ICommand GenerateStringCommand { get; }

        public ClassPathListViewModel() {
            this.ClassPathItems = new ObservableCollection<ClassPathItemViewModel>();
            this.AddFileCommand = new RelayCommand(AddFileAction);
            this.AddFolderCommand = new RelayCommand(AddFolderAction);
            this.GenerateListCommand = new RelayCommand(() => GenerateClasspathList());
            this.GenerateStringCommand = new RelayCommand(() => GenerateClasspathString());
        }

        public void AddFileAction() {
            ClassPathItemViewModel classPath = new ClassPathItemViewModel();
            classPath.SelectFileDialogAction();
            if (!string.IsNullOrEmpty(classPath.FilePath)) {
                this.ClassPathItems.Add(classPath);
                GenerateClasspathString();
            }
        }

        public void AddFolderAction() {
            ClassPathItemViewModel classPath = new ClassPathItemViewModel();
            classPath.SelectFolderDialogAction();
            if (!string.IsNullOrEmpty(classPath.FilePath)) {
                this.ClassPathItems.Add(classPath);
                GenerateClasspathString();
            }
        }

        public void GenerateClasspathString() {
            StringBuilder sb = new StringBuilder();
            for (int index = 0; index < (this.ClassPathItems.Count - 1); index++) {
                sb.Append(this.ClassPathItems[index].FilePath).Append(';');
            }

            sb.Append(this.ClassPathItems[this.ClassPathItems.Count - 1].FilePath);
            this.ClassPathString = sb.ToString();
        }

        private void GenerateClasspathList() {
            this.ClassPathItems.Clear();
            if (string.IsNullOrEmpty(this.ClassPathString)) {
                return;
            }

            foreach (string path in this.ClassPathString.Split(';')) {
                this.ClassPathItems.Add(new ClassPathItemViewModel() { FilePath = path});
            }
        }
    }
}