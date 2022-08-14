using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BCEdit180.Core.Messaging;
using BCEdit180.Core.Messaging.Messages;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.ViewModels {
    public class SourceCodeViewModel : BaseViewModel {
        private string text;
        public string Text {
            get => this.text;
            set => RaisePropertyChanged(ref this.text, value);
        }

        private bool isGenerating;
        public bool IsGenerating {
            get => this.isGenerating;
            set => RaisePropertyChanged(ref this.isGenerating, value);
        }

        public ClassViewModel Class { get; }

        public ICommand GenerateCommand { get; }

        public SourceCodeViewModel(ClassViewModel vm) {
            this.Class = vm;
            this.GenerateCommand = new RelayCommand(GenerateCode);
        }

        public void GenerateCode() {
            this.IsGenerating = true;
            MessageDispatcher.Publish(new BusyStateMessage(true));
            Task.Run(async () => {
                string code = await GenerateCodeAsync();
                this.IsGenerating = false;
                this.Text = code;
                MessageDispatcher.PublishUI(new BusyStateMessage(false));
            });
        }

        private async Task<string> GenerateCodeAsync() {
            StringBuilder sb = new StringBuilder();
            AppendAccessFlags(sb, this.Class.ClassInfo.AccessFlags);
            AppendType(sb, this.Class.ClassInfo.AccessFlags);
            sb.Append(GetTypeName(this.Class.ClassInfo.ClassName)).Append(" ");
            sb.Append("extends ").Append(GetTypeName(this.Class.ClassInfo.SuperName));
            if (this.Class.ClassInfo.Interfaces.Count > 0) {
                sb.Append(" implements ").Append(string.Join(", ", this.Class.ClassInfo.Interfaces.Select(a => GetTypeName(a.Value))));
            }

            sb.Append(" {\n");

            sb.Append($"    // Fields ({this.Class.FieldList.Fields.Count})\n");
            foreach (FieldInfoViewModel field in this.Class.FieldList.Fields) {
                sb.Append("    ");
                AppendAccessFlags(sb, field.Access);
                sb.Append(GetTypeReadable(field.FieldDescriptor)).Append(" ");
                sb.Append(field.FieldName);
                sb.Append(";\n");
            }

            sb.Append($"\n    // Methods ({this.Class.MethodList.Methods.Count})\n");
            foreach (MethodInfoViewModel method in this.Class.MethodList.Methods) {
                int param = (method.Access & MethodAccessModifiers.Static) != 0 ? 0 : 1;
                sb.Append("    ");
                AppendAccessFlags(sb, method.Access);
                sb.Append(GetTypeReadable(method.MethodDescriptor.ReturnType)).Append(" ");
                sb.Append(method.MethodName).Append("(");
                sb.Append(string.Join(", ", method.MethodDescriptor.ArgumentTypes.Select(s => GetTypeReadable(s) + " p" + param++)));
                sb.Append(")").Append(";\n");
            }

            return sb.Append("}").ToString();
        }

        private static StringBuilder AppendAccessFlags(StringBuilder sb, ClassAccessModifiers modifiers) {
            if ((modifiers & ClassAccessModifiers.Public) != 0) {
                sb.Append("public ");
            }
            if ((modifiers & ClassAccessModifiers.Protected) != 0) {
                sb.Append("protected ");
            }
            if ((modifiers & ClassAccessModifiers.Abstract) != 0) {
                sb.Append("abstract ");
            }
            if ((modifiers & ClassAccessModifiers.Final) != 0) {
                sb.Append("final ");
            }

            return sb;
        }

        private static StringBuilder AppendAccessFlags(StringBuilder sb, MethodAccessModifiers modifiers) {
            if ((modifiers & MethodAccessModifiers.Public) != 0) {
                sb.Append("public ");
            }

            if ((modifiers & MethodAccessModifiers.Protected) != 0) {
                sb.Append("protected ");
            }

            if ((modifiers & MethodAccessModifiers.Private) != 0) {
                sb.Append("private ");
            }

            if ((modifiers & MethodAccessModifiers.Static) != 0) {
                sb.Append("static ");
            }

            if ((modifiers & MethodAccessModifiers.Abstract) != 0) {
                sb.Append("abstract ");
            }

            if ((modifiers & MethodAccessModifiers.Final) != 0) {
                sb.Append("final ");
            }

            if ((modifiers & MethodAccessModifiers.Native) != 0) {
                sb.Append("native ");
            }

            return sb;
        }

        private static StringBuilder AppendAccessFlags(StringBuilder sb, FieldAccessModifiers modifiers) {
            if ((modifiers & FieldAccessModifiers.Public) != 0) {
                sb.Append("public ");
            }

            if ((modifiers & FieldAccessModifiers.Private) != 0) {
                sb.Append("private ");
            }

            if ((modifiers & FieldAccessModifiers.Protected) != 0) {
                sb.Append("protected ");
            }

            if ((modifiers & FieldAccessModifiers.Static) != 0) {
                sb.Append("static ");
            }

            if ((modifiers & FieldAccessModifiers.Volatile) != 0) {
                sb.Append("volatile ");
            }

            if ((modifiers & FieldAccessModifiers.Final) != 0) {
                sb.Append("final ");
            }

            return sb;
        }

        private static StringBuilder AppendType(StringBuilder sb, ClassAccessModifiers modifiers) {
            if ((modifiers & ClassAccessModifiers.Interface) != 0) {
                sb.Append("interface ");
            }
            else {
                sb.Append("class ");
            }

            return sb;
        }

        private static string GetTypeName(string className) {
            if (string.IsNullOrEmpty(className)) {
                return "";
            }

            int lastIndex = className.LastIndexOf('/');
            if (lastIndex == -1) {
                return className[0] == 'L' ? className.Substring(1) : className; // remove L at start
            }
            else {
                string name = className.Substring(lastIndex + 1);
                if (string.IsNullOrEmpty(name)) {
                    return name;
                }

                // name = name.Remove(name.Length - 1); // remove ;
                return name[name.Length - 1] == ';' ? name.Substring(0, name.Length - 1) : name;
            }
        }

        private static string GetTypeReadable(TypeDescriptor descriptor) {
            if (descriptor.PrimitiveType.HasValue) {
                switch (descriptor.PrimitiveType) {
                    case PrimitiveType.Boolean:   return "boolean";
                    case PrimitiveType.Byte:      return "byte";
                    case PrimitiveType.Character: return "char";
                    case PrimitiveType.Double:    return "double";
                    case PrimitiveType.Float:     return "float";
                    case PrimitiveType.Integer:   return "int";
                    case PrimitiveType.Long:      return "long";
                    case PrimitiveType.Short:     return "short";
                    case PrimitiveType.Void:      return "void";
                    default: throw new ArgumentOutOfRangeException();
                }
            }
            else {
                return GetTypeName(descriptor.ClassName.Name);
            }
        }
    }
}
