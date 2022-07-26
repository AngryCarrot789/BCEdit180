using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BCEdit180.Core.Window;
using JavaAsm;
using JavaAsm.Instructions.Types;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.AttributeEditor.Classes {
    public class BootstrapMethodViewModel : BaseViewModel {
        private ReferenceKindType bootstrapReferenceType;
        public ReferenceKindType BootstrapReferenceType {
            get => this.bootstrapReferenceType;
            set => RaisePropertyChanged(ref this.bootstrapReferenceType, value);
        }

        private string bootstrapMethodOwner;
        public string BootstrapMethodOwner {
            get => this.bootstrapMethodOwner;
            set => RaisePropertyChanged(ref this.bootstrapMethodOwner, value);
        }

        private string bootstrapMethodName;
        public string BootstrapMethodName {
            get => this.bootstrapMethodName;
            set => RaisePropertyChanged(ref this.bootstrapMethodName, value);
        }

        private IDescriptor bootstrapMethodDescriptor;
        public IDescriptor BootstrapMethodDescriptor {
            get => this.bootstrapMethodDescriptor;
            set => RaisePropertyChanged(ref this.bootstrapMethodDescriptor, value);
        }

        public ObservableCollection<object> BootstrapMethodArgs { get; }

        public ICommand EditDescriptorCommand { get; }

        public BootstrapMethodViewModel() {
            this.BootstrapMethodArgs = new ObservableCollection<object>();
            this.EditDescriptorCommand = new RelayCommand(() => {
                switch (this.BootstrapReferenceType) {
                    case ReferenceKindType.GetField: break;
                    case ReferenceKindType.GetStatic: break;
                    case ReferenceKindType.PutField: break;
                    case ReferenceKindType.PutStatic:
                        if (Dialog.TypeEditor.EditTypeDescriptorDialog(this.BootstrapMethodDescriptor as TypeDescriptor, out TypeDescriptor typeDesc).Result) {
                            this.BootstrapMethodDescriptor = typeDesc;
                        }
                        break;
                    case ReferenceKindType.InvokeVirtual: break;
                    case ReferenceKindType.InvokeStatic: break;
                    case ReferenceKindType.InvokeSpecial: break;
                    case ReferenceKindType.NewInvokeSpecial: break;
                    case ReferenceKindType.InvokeReference:
                        if (Dialog.TypeEditor.EditMethodDescriptorDialog(this.BootstrapMethodDescriptor as MethodDescriptor, out MethodDescriptor methodDesc).Result) {
                            this.BootstrapMethodDescriptor = methodDesc;
                        }
                        break;
                    default: throw new ArgumentOutOfRangeException();
                }
            });
        }
    }
}