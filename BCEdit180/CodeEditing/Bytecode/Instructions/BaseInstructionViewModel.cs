using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using JavaAsm;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.CodeEditing.Bytecode {
    // non-abstract, so that opcodes can fall back to the base class and get ToString()'d instead of custom layouts
    public abstract class BaseInstructionViewModel : BaseViewModel {
        public Instruction Instruction { get; protected set; }

        public abstract IEnumerable<Opcode> AvailableOpCodes { get; }

        private Opcode opCode;

        public Opcode Opcode {
            get => this.opCode;
            set => RaisePropertyChanged(ref this.opCode, value);
        }

        public virtual bool CanEditOpCode { get; } = true;

        public ICommand EditOpcodeCommand { get; }

        protected BaseInstructionViewModel() {
            this.EditOpcodeCommand = new RelayCommand(() => {
                if (this.CanEditOpCode) {
                    ViewManager.ShowEditInstructionView(this.AvailableOpCodes, (o) => this.Opcode = o);
                }
            });
        }

        public static BaseInstructionViewModel ForInstruction(Instruction instruction) {
            if (instruction is FieldInstruction) {
                return new FieldInstructionViewModel();
            }
            else if (instruction is IncrementInstruction) {
                return new IncrementInstructionViewModel();
            }
            else if (instruction is IntegerPushInstruction) {
                return new IntegerPushInstructionViewModel();
            }
            else if (instruction is InvokeDynamicInstruction) {
                return new InvokeDynamicInstructionViewModel();
            }
            else if (instruction is JumpInstruction) {
                return new JumpInstructionViewModel();
            }
            else if (instruction is Label) {
                return new LabelViewModel();
            }
            else if (instruction is LdcInstruction) {
                return new LdcInstructionViewModel();
            }
            else if (instruction is LineNumber) {
                return new LineNumberViewModel();
            }
            else if (instruction is LookupSwitchInstruction) {
                return new LookupSwitchInstructionViewModel();
            }
            else if (instruction is MethodInstruction) {
                return new MethodInstructionViewModel();
            }
            else if (instruction is MultiANewArrayInstruction) {
                return new MultiANewArrayInstructionViewModel();
            }
            else if (instruction is NewArrayInstruction) {
                return new NewArrayInstructionViewModel();
            }
            else if (instruction is SimpleInstruction) {
                return new SimpleInstructionViewModel();
            }
            else if (instruction is StackMapFrame) {
                return new StackMapFrameViewModel();
            }
            else if (instruction is TableSwitchInstruction) {
                return new TableSwitchInstructionViewModel();
            }
            else if (instruction is TypeInstruction) {
                return new TypeInstructionViewModel();
            }
            else if (instruction is VariableInstruction) {
                return new VariableInstructionViewModel();
            }
            else {
                if (instruction == null) {
                    throw new NullReferenceException("Instruction cannot be null");
                }
                else {
                    throw new ArgumentException("Unexpected instruction: " + instruction.GetType() + " -> " + instruction);
                }
            }
        }

        public virtual void Load(Instruction instruction) {
            this.Opcode = instruction.Opcode;
        }

        public virtual void Save(Instruction instruction) {
            if (this.Opcode != Opcode.None) {
                instruction.Opcode = this.Opcode;
            }
            // predict if set will throw
            //else if (!this.AvailableOpCodes.Contains(Opcode.None)) {
            //    instruction.Opcode = Opcode.None;
            //}
        }
    }

    public class FieldInstructionViewModel : BaseInstructionViewModel {
        private string fieldOwner;
        public string FieldOwner {
            get => this.fieldOwner;
            set => RaisePropertyChanged(ref this.fieldOwner, value);
        }

        private string fieldName;
        public string FieldName {
            get => this.fieldName;
            set => RaisePropertyChanged(ref this.fieldName, value);
        }

        private string fieldDescriptor;
        public string FieldDescriptor {
            get => this.fieldDescriptor;
            set => RaisePropertyChanged(ref this.fieldDescriptor, value);
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.GETFIELD, Opcode.GETSTATIC, Opcode.PUTFIELD, Opcode.PUTSTATIC};

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            FieldInstruction field = (FieldInstruction) instruction;
            this.FieldOwner = field.Owner.Name;
            this.FieldName = field.Name;
            this.FieldDescriptor = field.Descriptor.ToString();
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            FieldInstruction field = (FieldInstruction) instruction;
            field.Owner = new ClassName(this.FieldOwner);
            field.Name = this.FieldName;
            field.Descriptor = TypeDescriptor.Parse(this.FieldDescriptor);
        }
    }

    public class IncrementInstructionViewModel : BaseInstructionViewModel {
        private ushort varIndex;
        public ushort VarIndex {
            get => this.varIndex;
            set => RaisePropertyChanged(ref this.varIndex, value);
        }

        private short value;
        public short Value {
            get => this.value;
            set => RaisePropertyChanged(ref this.value, value);
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.IINC};

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            IncrementInstruction increment = (IncrementInstruction) instruction;
            this.VarIndex = increment.VariableIndex;
            this.Value = increment.Value;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            IncrementInstruction increment = (IncrementInstruction) instruction;
            increment.VariableIndex = (ushort) this.VarIndex;
            increment.Value = (short) this.Value;
        }
    }

    public class IntegerPushInstructionViewModel : BaseInstructionViewModel {
        private ushort value;
        public ushort Value {
            get => this.value;
            set => RaisePropertyChanged(ref this.value, value);
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.BIPUSH, Opcode.SIPUSH};

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            IntegerPushInstruction insn = (IntegerPushInstruction) instruction;
            this.Value = insn.Value;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            IntegerPushInstruction insn = (IntegerPushInstruction) instruction;
            insn.Value = (ushort) this.Value;
        }
    }

    public class InvokeDynamicInstructionViewModel : BaseInstructionViewModel {
        private string name;
        public string Name {
            get => this.name;
            set => RaisePropertyChanged(ref this.name, value);
        }

        private string descriptor;
        public string Descriptor {
            get => this.descriptor;
            set => RaisePropertyChanged(ref this.descriptor, value);
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.INVOKEDYNAMIC};

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            InvokeDynamicInstruction insn = (InvokeDynamicInstruction) instruction;
            this.Name = insn.Name;
            this.Descriptor = insn.Descriptor.ToString();
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            InvokeDynamicInstruction insn = (InvokeDynamicInstruction) instruction;
            insn.Name = this.Name;
            insn.Descriptor = MethodDescriptor.Parse(this.Descriptor);
        }
    }

    public class JumpInstructionViewModel : BaseInstructionViewModel {
        private long target;
        public long Target {
            get => this.target;
            set => RaisePropertyChanged(ref this.target, value);
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.IFEQ, Opcode.IFNE, Opcode.IFLT, Opcode.IFGE, Opcode.IFGT, Opcode.IFLE, Opcode.IF_ICMPEQ, Opcode.IF_ICMPNE, Opcode.IF_ICMPLT, Opcode.IF_ICMPGE, Opcode.IF_ICMPGT, Opcode.IF_ICMPLE, Opcode.IF_ACMPEQ, Opcode.IF_ACMPNE, Opcode.GOTO, Opcode.JSR, Opcode.IFNULL, Opcode.IFNONNULL};

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            JumpInstruction insn = (JumpInstruction) instruction;
            this.Target = insn.Target.Index;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            // JumpInstruction insn = (JumpInstruction) instruction;
            // insn.Target = new Label();
            // insn.Descriptor = MethodDescriptor.Parse(this.Descriptor);
        }
    }

    public class LabelViewModel : BaseInstructionViewModel {
        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.None};

        public override void Load(Instruction instruction) {
            base.Load(instruction);
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
        }
    }

    public class LdcInstructionViewModel : BaseInstructionViewModel {
        private object value;
        public object Value {
            get => this.value;
            set => RaisePropertyChanged(ref this.value, value);
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.LDC, Opcode.LDC_W, Opcode.LDC2_W};

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            LdcInstruction insn = (LdcInstruction) instruction;
            this.Value = insn.Value;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            LdcInstruction insn = (LdcInstruction) instruction;
            insn.Value = this.Value;
        }
    }

    public class LineNumberViewModel : BaseInstructionViewModel {
        private ushort lineNumber;
        public ushort LineNumber {
            get => this.lineNumber;
            set => RaisePropertyChanged(ref this.lineNumber, value);
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.None};

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            LineNumber insn = (LineNumber) instruction;
            this.LineNumber = insn.Line;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            LineNumber insn = (LineNumber) instruction;
            insn.Line = this.LineNumber;
        }
    }

    public class LookupSwitchInstructionViewModel : BaseInstructionViewModel {
        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {};

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            LookupSwitchInstruction insn = (LookupSwitchInstruction) instruction;

        }

        public override void Save(Instruction instruction) {
            LookupSwitchInstruction insn = (LookupSwitchInstruction) instruction;
            base.Save(instruction);
        }
    }

    public class MethodInstructionViewModel : BaseInstructionViewModel {
        private string methodOwner;
        public string MethodOwner {
            get => this.methodOwner;
            set => RaisePropertyChanged(ref this.methodOwner, value);
        }

        private string methodName;
        public string MethodName {
            get => this.methodName;
            set => RaisePropertyChanged(ref this.methodName, value);
        }

        private string methodDescriptor;
        public string MethodDescriptor {
            get => this.methodDescriptor;
            set => RaisePropertyChanged(ref this.methodDescriptor, value);
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.INVOKESTATIC, Opcode.INVOKEVIRTUAL, Opcode.INVOKEINTERFACE, Opcode.INVOKESPECIAL};

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            MethodInstruction insn = (MethodInstruction) instruction;
            this.MethodOwner = insn.Owner.Name;
            this.MethodName = insn.Name;
            this.MethodDescriptor = insn.Descriptor.ToString();
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            MethodInstruction insn = (MethodInstruction) instruction;
            insn.Owner = new ClassName(this.MethodOwner);
            insn.Name = this.MethodName;
            insn.Descriptor = JavaAsm.MethodDescriptor.Parse(this.MethodDescriptor);
        }
    }

    public class MultiANewArrayInstructionViewModel : BaseInstructionViewModel {
        private string componentType;
        public string ComponentType {
            get => this.componentType;
            set => RaisePropertyChanged(ref this.componentType, value);
        }

        private byte dimensions;
        public byte Dimensions {
            get => this.dimensions;
            set => RaisePropertyChanged(ref this.dimensions, value);
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {};

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            MultiANewArrayInstruction insn = (MultiANewArrayInstruction) instruction;
            this.ComponentType = insn.Type.Name;
            this.Dimensions = insn.Dimensions;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            MultiANewArrayInstruction insn = (MultiANewArrayInstruction) instruction;
            insn.Type = new ClassName(this.ComponentType);
            insn.Dimensions = this.Dimensions;
        }
    }

    public class NewArrayInstructionViewModel : BaseInstructionViewModel {
        private static readonly ObservableCollection<NewArrayTypeCode> CODES;

        static NewArrayInstructionViewModel() {
            CODES = new ObservableCollection<NewArrayTypeCode> {
                NewArrayTypeCode.Boolean,
                NewArrayTypeCode.Character,
                NewArrayTypeCode.Float,
                NewArrayTypeCode.Double,
                NewArrayTypeCode.Byte,
                NewArrayTypeCode.Short,
                NewArrayTypeCode.Integer,
                NewArrayTypeCode.Long
            };
        }

        public ObservableCollection<NewArrayTypeCode> TypeCodes => CODES;

        private NewArrayTypeCode selectedCode;

        public NewArrayTypeCode SelectedCode {
            get => this.selectedCode;
            set => RaisePropertyChanged(ref this.selectedCode, value);
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.NEWARRAY};

        public override bool CanEditOpCode => false;

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            NewArrayInstruction insn = (NewArrayInstruction) instruction;
            this.SelectedCode = insn.ArrayType;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            NewArrayInstruction insn = (NewArrayInstruction) instruction;
            switch (this.SelectedCode) {
                case NewArrayTypeCode.Boolean:
                case NewArrayTypeCode.Character:
                case NewArrayTypeCode.Float:
                case NewArrayTypeCode.Double:
                case NewArrayTypeCode.Byte:
                case NewArrayTypeCode.Short:
                case NewArrayTypeCode.Integer:
                case NewArrayTypeCode.Long:
                    insn.ArrayType = this.SelectedCode;
                    break;
            }
        }
    }

    public class SimpleInstructionViewModel : BaseInstructionViewModel {
        private static readonly Opcode[] CODES = new Opcode[] {
            Opcode.NOP,
            Opcode.ACONST_NULL,
            Opcode.ICONST_M1,
            Opcode.ICONST_0,
            Opcode.ICONST_1,
            Opcode.ICONST_2,
            Opcode.ICONST_3,
            Opcode.ICONST_4,
            Opcode.ICONST_5,
            Opcode.LCONST_0,
            Opcode.LCONST_1,
            Opcode.FCONST_0,
            Opcode.FCONST_1,
            Opcode.FCONST_2,
            Opcode.DCONST_0,
            Opcode.DCONST_1,
            Opcode.IALOAD,
            Opcode.LALOAD,
            Opcode.FALOAD,
            Opcode.DALOAD,
            Opcode.AALOAD,
            Opcode.BALOAD,
            Opcode.CALOAD,
            Opcode.SALOAD,
            Opcode.IASTORE,
            Opcode.LASTORE,
            Opcode.FASTORE,
            Opcode.DASTORE,
            Opcode.AASTORE,
            Opcode.BASTORE,
            Opcode.CASTORE,
            Opcode.SASTORE,
            Opcode.POP,
            Opcode.POP2,
            Opcode.DUP,
            Opcode.DUP_X1,
            Opcode.DUP_X2,
            Opcode.DUP2,
            Opcode.DUP2_X1,
            Opcode.DUP2_X2,
            Opcode.SWAP,
            Opcode.IADD,
            Opcode.LADD,
            Opcode.FADD,
            Opcode.DADD,
            Opcode.ISUB,
            Opcode.LSUB,
            Opcode.FSUB,
            Opcode.DSUB,
            Opcode.IMUL,
            Opcode.LMUL,
            Opcode.FMUL,
            Opcode.DMUL,
            Opcode.IDIV,
            Opcode.LDIV,
            Opcode.FDIV,
            Opcode.DDIV,
            Opcode.IREM,
            Opcode.LREM,
            Opcode.FREM,
            Opcode.DREM,
            Opcode.INEG,
            Opcode.LNEG,
            Opcode.FNEG,
            Opcode.DNEG,
            Opcode.ISHL,
            Opcode.LSHL,
            Opcode.ISHR,
            Opcode.LSHR,
            Opcode.IUSHR,
            Opcode.LUSHR,
            Opcode.IAND,
            Opcode.LAND,
            Opcode.IOR,
            Opcode.LOR,
            Opcode.IXOR,
            Opcode.LXOR,
            Opcode.I2L,
            Opcode.I2F,
            Opcode.I2D,
            Opcode.L2I,
            Opcode.L2F,
            Opcode.L2D,
            Opcode.F2I,
            Opcode.F2L,
            Opcode.F2D,
            Opcode.D2I,
            Opcode.D2L,
            Opcode.D2F,
            Opcode.I2B,
            Opcode.I2C,
            Opcode.I2S,
            Opcode.LCMP,
            Opcode.FCMPL,
            Opcode.FCMPG,
            Opcode.DCMPL,
            Opcode.DCMPG,
            Opcode.IRETURN,
            Opcode.LRETURN,
            Opcode.FRETURN,
            Opcode.DRETURN,
            Opcode.ARETURN,
            Opcode.RETURN,
            Opcode.ARRAYLENGTH,
            Opcode.ATHROW,
            Opcode.MONITORENTER,
            Opcode.MONITOREXIT
        };

        public override IEnumerable<Opcode> AvailableOpCodes => CODES;
    }

    public class StackMapFrameViewModel : BaseInstructionViewModel {
        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {};

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            StackMapFrame insn = (StackMapFrame) instruction;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            StackMapFrame insn = (StackMapFrame) instruction;
        }
    }

    public class TableSwitchInstructionViewModel : BaseInstructionViewModel {
        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {};

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            TableSwitchInstruction insn = (TableSwitchInstruction) instruction;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            TableSwitchInstruction insn = (TableSwitchInstruction) instruction;
        }
    }

    public class TypeInstructionViewModel : BaseInstructionViewModel {
        private string type;
        public string Type {
            get => this.type;
            set => RaisePropertyChanged(ref this.type, value);
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.NEW, Opcode.ANEWARRAY, Opcode.CHECKCAST, Opcode.INSTANCEOF};

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            TypeInstruction insn = (TypeInstruction) instruction;
            this.Type = insn.Type.Name;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            TypeInstruction insn = (TypeInstruction) instruction;
            insn.Type = new ClassName(this.Type);
        }
    }

    public class VariableInstructionViewModel : BaseInstructionViewModel {
        private ushort varIndex;
        public ushort VarIndex {
            get => this.varIndex;
            set => RaisePropertyChanged(ref this.varIndex, value);
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.ILOAD, Opcode.LLOAD, Opcode.FLOAD, Opcode.DLOAD, Opcode.ALOAD, Opcode.ISTORE, Opcode.LSTORE, Opcode.FSTORE, Opcode.DSTORE, Opcode.ASTORE, Opcode.RET};

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            VariableInstruction insn = (VariableInstruction) instruction;
            this.VarIndex = insn.VariableIndex;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            VariableInstruction insn = (VariableInstruction) instruction;
            insn.VariableIndex = this.VarIndex;
        }
    }
}