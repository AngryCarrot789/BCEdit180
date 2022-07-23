using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
using BCEdit180.Utils;
using JavaAsm;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.CodeEditing.Bytecode.Instructions {
    public class InvokeDynamicInstructionViewModel : BaseInstructionViewModel {
        private string name;
        public string Name {
            get => this.name;
            set => RaisePropertyChanged(ref this.name, value);
        }

        private MethodDescriptor descriptor;
        public MethodDescriptor Descriptor {
            get => this.descriptor;
            set => RaisePropertyChanged(ref this.descriptor, value);
        }

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

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.INVOKEDYNAMIC};

        public override bool CanEditOpCode => false;

        public InvokeDynamicInstructionViewModel() {
            this.BootstrapMethodArgs = new ObservableCollection<object>();
        }

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            InvokeDynamicInstruction insn = (InvokeDynamicInstruction) instruction;
            this.BootstrapMethodArgs.Clear();
            this.Name = insn.Name;
            this.Descriptor = insn.Descriptor;
            this.BootstrapReferenceType = insn.BootstrapMethod.Type;
            this.BootstrapMethodOwner = insn.BootstrapMethod.Owner.Name;
            this.BootstrapMethodName = insn.BootstrapMethod.Name;
            this.BootstrapMethodDescriptor = insn.BootstrapMethod.Descriptor;
            this.BootstrapMethodArgs.AddAll(insn.BootstrapMethodArgs);
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            InvokeDynamicInstruction insn = (InvokeDynamicInstruction) instruction;
            insn.Name = this.Name;
            insn.Descriptor = this.Descriptor;
            insn.BootstrapMethod.Type = this.BootstrapReferenceType;
            insn.BootstrapMethod.Owner = new ClassName(this.BootstrapMethodOwner);
            insn.BootstrapMethod.Name = this.BootstrapMethodName;

            insn.BootstrapMethod.Descriptor = this.BootstrapMethodDescriptor;
            // switch (this.BootstrapReferenceType) {
            //     case ReferenceKindType.GetField:
            //     case ReferenceKindType.GetStatic:
            //     case ReferenceKindType.PutField:
            //     case ReferenceKindType.PutStatic:
            //         insn.BootstrapMethod.Descriptor = TypeDescriptor.Parse(this.BootstrapMethodDescriptor);
            //         break;
            //     case ReferenceKindType.InvokeVirtual:
            //     case ReferenceKindType.InvokeStatic:
            //     case ReferenceKindType.InvokeSpecial:
            //     case ReferenceKindType.NewInvokeSpecial:
            //     case ReferenceKindType.InvokeReference:
            //         insn.BootstrapMethod.Descriptor = MethodDescriptor.Parse(this.BootstrapMethodDescriptor);
            //         break;
            //     default: throw new Exception("Unknown bootstrap reference type: " + this.BootstrapReferenceType);
            // }

            insn.BootstrapMethodArgs = new List<object>(this.BootstrapMethodArgs);
        }
    }
}