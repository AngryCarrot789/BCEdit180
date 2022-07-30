using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using BCEdit180.Core.AttributeEditor.Classes;
using BCEdit180.Core.Collections;
using BCEdit180.Core.ErrorReporting;
using BCEdit180.Core.Utils;
using BCEdit180.Core.Window;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.ViewModels {
    public class ClassListViewModel : BaseViewModel {
        public ObservableCollection<ClassViewModel> Classes { get; }

        private ClassViewModel selectedClass;
        public ClassViewModel SelectedClass {
            get => this.selectedClass;
            set => RaisePropertyChanged(ref this.selectedClass, value);
        }

        private ClassViewModel blankClass;

        public ErrorReporterViewModel ErrorReporter { get; }

        public InstructionClipboardViewModel Clipboard { get; }

        public ICommand OpenFileCommand { get; }

        public ClassListViewModel() {
            this.Classes = new ExtendedObservableCollection<ClassViewModel>();
            this.Clipboard = new InstructionClipboardViewModel();
            ServiceManager.SetService(this.Clipboard);
            this.OpenFileCommand = new RelayCommand(OpenFileAction);
            this.ErrorReporter = new ErrorReporterViewModel(this);
        }

        public void CreateBalnkClass() {
            this.Classes.Add(this.blankClass = new ClassViewModel() { ClassList = this });
        }

        private void OpenFileAction() {
            if (Dialog.File.OpenFileDialog("Select a class file to open", "ClassFile|*.class|All|*.*", out string path).Result) {
                if (File.Exists(path)) {
                    OpenAndReadClassFile(path);
                }
                else {
                    Dialog.Message.ShowWarningDialog("No such file", "File does not exist: " + path);
                }
            }
        }

        public void OpenAndReadClassFiles(string[] files) {
            if (files == null || files.Length < 1) {
                return;
            }

            if (files.Length == 1) {
                if (File.Exists(files[0])) {
                    OpenAndReadClassFile(files[0], true);
                }
            }
            else {
                foreach (string path in files) {
                    if (File.Exists(path)) {
                        OpenAndReadClassFile(path, true, false);
                    }
                }
            }
        }

        public void OpenAndReadClassFile(string path, bool showDialog = true, bool readInner = true, ActionProgressViewModel actionProgress = null, bool closeDialog = true) {
            if (this.blankClass != null) {
                RemoveClass(this.blankClass);
                this.blankClass = null;
            }

            ClassViewModel viewModel = new ClassViewModel();
            viewModel.FilePath = path;
            viewModel.ClassList = this;
            viewModel.ClassInfo.ClassName = "Loading Class...";
            this.Classes.Add(viewModel);
            this.SelectedClass = viewModel;
            AppProxy.Proxy.InvokeSync(() => {
                viewModel.ReadClassFile(path, showDialog, actionProgress, closeDialog);
                //if (readInner) {
                //    OpenAdditionalInnerClasses(viewModel, showDialog);
                //}
            });
        }

        public void OpenAdditionalInnerClasses(ClassViewModel clazz, bool showDialog = true) {
            if (clazz.FilePath == null) {
                // should not reach here... usually
                return;
            }

            List<string> classes = new List<string>();
            string path = clazz.FilePath;
            string url = clazz.ClassInfo.ClassName.Replace('.', '\\') + ".class";
            int index = path.IndexOf(url);
            if (index == -1) {

            }
            else {
                string root = index > 0 ? path.Substring(0, index - 1) : ""; // index should never ever be 0... hopefully
                if (clazz.ClassAttributes.InnerClasses.Count > 0) {
                    foreach (InnerClassViewModel inner in clazz.ClassAttributes.InnerClasses) {
                        string innerPath = GetPathForInner_NormalStructure(root, clazz, inner);
                        if (innerPath != null) {
                            classes.Add(innerPath);
                        }
                    }
                }
            }

            if (classes.Count < 1) {
                return;
            }

            if (this.blankClass != null) {
                RemoveClass(this.blankClass);
                this.blankClass = null;
            }

            if (showDialog) {
                int i = 0;
                Task[] tasks = new Task[classes.Count];
                ActionProgressViewModel vm = Dialog.Message.ShowProgressWindow($"Loading {classes.Count} inner classes");
                foreach (string innerPath in classes) {
                    tasks[i++] = Task.Run(async () => {
                        ClassViewModel classViewModel = new ClassViewModel();
                        classViewModel.FilePath = innerPath;
                        classViewModel.ClassList = this;
                        AppProxy.Proxy.InvokeSync(() => {
                            vm.Description = "Loading file " + innerPath;
                        });

                        await Task.Delay(100);
                        AppProxy.Proxy.InvokeSync(() => {
                            classViewModel.ReadClassFile(innerPath, false);
                            this.Classes.Add(classViewModel);
                        });
                    });
                }

                Task.Run(() => Task.WaitAll(tasks)).ContinueWith((t) => {
                    AppProxy.Proxy.InvokeSync(() => {
                        vm.CloseDialog();
                    });
                });
            }
            else {
                foreach (string innerPath in classes) {
                    ClassViewModel classViewModel = new ClassViewModel();
                    classViewModel.FilePath = innerPath;
                    classViewModel.ClassList = this;
                    classViewModel.ReadClassFile(innerPath, false);
                    this.Classes.Add(classViewModel);
                }
            }
        }

        private string GetPathForInner_NormalStructure(string root, ClassViewModel clazz, InnerClassViewModel inner) {
            if (inner.InnerName == null) {
                return null;
            }

            string path = Path.Combine(root, inner.InnerName.Replace('.', '\\').Replace('/', '\\') + ".class");
            return File.Exists(path) ? path : null;
        }

        public void RemoveClass(ClassViewModel classViewModel, bool dispose = true) {
            this.Classes.Remove(classViewModel);
            if (dispose) {
                classViewModel.Dispose();
            }
        }
    }
}