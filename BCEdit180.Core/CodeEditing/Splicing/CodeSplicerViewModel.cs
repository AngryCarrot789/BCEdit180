using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BCEdit180.Core.Utils;
using BCEdit180.Core.ViewModels;
using BCEdit180.Core.Window;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.CodeEditing.Splicing {
    public class CodeSplicerViewModel : BaseViewModel {
        private static readonly string DecompilerJar;

        static CodeSplicerViewModel() {
            DecompilerJar = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "jd-cli.jar");
        }

        private string javaCode;
        public string JavaCode {
            get => this.javaCode;
            set => RaisePropertyChanged(ref this.javaCode, value);
        }

        public CodeEditorViewModel CodeEditor { get; }

        public ICommand DecompileCodeCommand { get; }

        public ICommand CompileCommand { get; }

        public CodeSplicerViewModel(CodeEditorViewModel codeEditor) {
            this.CodeEditor = codeEditor;
            this.DecompileCodeCommand = new RelayCommand(DecompileAction);
            this.CompileCommand = new RelayCommand(CompileAction);
        }

        private void DecompileAction() {
            string classFile = this.CodeEditor.MethodInfo.Class.FilePath;
            if (!File.Exists(classFile)) {
                Dialog.Message.ShowInformationDialog("File not found", "The original class file does not exist: " + classFile);
                return;
            }

            string cmd = $"-jar {DecompilerJar} \"{classFile}\"";
            ProcessStartInfo info = new ProcessStartInfo("java", cmd);
            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            Process process = new Process();
            process.StartInfo = info;
            process.Start();
            if (!process.WaitForExit(5000)) {
                Dialog.Message.ShowInformationDialog("Decompiler froze", "The decompiler has not exited in the last 5000 milliseconds");
            }

            this.JavaCode = process.StandardOutput.ReadToEnd();
        }

        private void CompileAction() {
            // Generate modified version of existing class; setting all fields and methods to public
            if (true) {
                Dialog.Message.ShowInformationDialog("Coming soon", "This feature is coming soon");
                return;
            }


            ActionProgressViewModel progress = Dialog.Message.ShowProgressWindow("Compiling", "Creating duplicate class for internal access edits");
            ClassViewModel clazz = this.CodeEditor.MethodInfo.Class;
            string javaCode = this.JavaCode;
            Task.Run(async () => {
                await Task.Delay(100);
                string original = clazz.FilePath;
                string backupFile = clazz.FilePath + "_backup";

                clazz.SaveClassToFile(backupFile);
                if (!File.Exists(clazz.FilePath)) {
                    await Dialog.Message.ShowInformationDialog("File not found", "File no longer exists: " + clazz.FilePath);
                    return;
                }

                ClassViewModel dupeClass = new ClassViewModel();
                progress.Description = "Loading dupe class";
                await Task.Delay(100);
                await AppProxy.Proxy.InvokeSyncAsync(() => dupeClass.ReadClassFile(backupFile, true, progress));

                dupeClass.ClassInfo.AccessFlags &= ~ClassAccessModifiers.Final;
                dupeClass.ClassInfo.AccessFlags &= ~ClassAccessModifiers.Abstract; // super duper dirty way to make this work

                foreach (MethodInfoViewModel method in dupeClass.MethodList.Methods) {
                    // keep out own access flags
                    if (method == this.CodeEditor.MethodInfo) {
                        continue;
                    }

                    method.Access |= MethodAccessModifiers.Public;
                    method.Access &= ~MethodAccessModifiers.Private;
                    method.Access &= ~MethodAccessModifiers.Protected;
                }

                foreach (FieldInfoViewModel field in dupeClass.FieldList.Fields) {
                    field.Access |= FieldAccessModifiers.Public;
                    field.Access &= ~FieldAccessModifiers.Private;
                    field.Access &= ~FieldAccessModifiers.Protected;
                }

                // TODO: also generate constructors...

                dupeClass.SaveClassToFile(original);

                // Generate mock class

                progress.Description = "Generating mock class";

                StringBuilder classContents = new StringBuilder(512);

                string package = ClassNameUtils.GetPackage(dupeClass.ClassInfo.ClassName);
                if (!string.IsNullOrEmpty(package)) {
                    classContents.Append($"package {package};\n");
                    classContents.Append($"public class ").
                                  Append(ClassNameUtils.GetName(dupeClass.ClassInfo.ClassName)).
                                  Append("_MOCK extends ").
                                  Append(ClassNameUtils.GetName(dupeClass.ClassInfo.ClassName)).
                                  Append(" {\n");
                }

                classContents.Append(javaCode);

                classContents.Append('}');

                await Task.Delay(100);
                // save to original file
                // then run javac including all classpaths + the original file, compiling the mock file (with our custom code)
                // hopefully, it should work...
            });
        }
    }
}