using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BCEdit180.Core.CodeEditing.Bytecode.Instructions;
using BCEdit180.Core.Collections;
using BCEdit180.Core.Searching;
using BCEdit180.Core.Utils;
using JavaAsm;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.CodeEditing {
    public class BytecodeEditorViewModel : BaseViewModel, IDisposable {
        public static IListSelector<BaseInstructionViewModel> BytecodeList { get; set; }

        public ExtendedObservableCollection<BaseInstructionViewModel> Instructions { get; }

        public ExtendedObservableCollection<BaseInstructionViewModel> RemovedInstructions { get; }

        private int selectedInstructionIndex;
        public int SelectedInstructionIndex {
            get => this.selectedInstructionIndex;
            set => RaisePropertyChanged(ref this.selectedInstructionIndex, value);
        }

        private BaseInstructionViewModel selectedInstruction;
        public BaseInstructionViewModel SelectedInstruction {
            get => this.selectedInstruction;
            set => RaisePropertyChanged(ref this.selectedInstruction, value);
        }

        public ICommand DeleteSelectedInstructionsCommand { get; }
        public ICommand InsertInstructionCommand { get; }
        public ICommand InsertCodeCommand { get; }
        public ICommand ShowHideOptionsCommand { get; }

        public SearchInstructionViewModel SearchInstruction { get; }

        public CodeEditorViewModel CodeEditor { get; }

        public BytecodeEditorViewModel(CodeEditorViewModel codeEditor) {
            this.CodeEditor = codeEditor;
            this.Instructions = new ExtendedObservableCollection<BaseInstructionViewModel>();
            this.RemovedInstructions = new ExtendedObservableCollection<BaseInstructionViewModel>();
            this.DeleteSelectedInstructionsCommand = new RelayCommand(DeleteSelectedInstructions);
            this.SearchInstruction = new SearchInstructionViewModel(this);
        }

        public void DeleteSelectedInstructions() {
            IEnumerable<BaseInstructionViewModel> instructions = BytecodeList.SelectedItems;
            this.RemovedInstructions.AddRange(instructions);
            this.Instructions.RemoveRange(instructions);
        }

        public BaseInstructionViewModel CreateInstructionForHandle(Instruction instruction) {
            BaseInstructionViewModel vm = BaseInstructionViewModel.ForInstruction(instruction);
            vm.Load(instruction);
            return vm;
        }

        public void Load(MethodNode node) {
            this.RemovedInstructions.Clear();
            this.Instructions.Clear();
            Dictionary<long, LabelViewModel> labelMap = new Dictionary<long, LabelViewModel>();
            List<BaseInstructionViewModel> instructions = new List<BaseInstructionViewModel>();
            foreach(Instruction instruction in node.Instructions) {
                BaseInstructionViewModel baseInstruction = CreateInstructionForHandle(instruction);
                if (baseInstruction is LabelViewModel label) {
                    labelMap[label.Index] = label;
                }

                instructions.Add(baseInstruction);
            }

            foreach(BaseInstructionViewModel instruction in instructions) {
                if (instruction is JumpInstructionViewModel jump) {
                    if (labelMap.TryGetValue(jump.Target, out LabelViewModel label)) {
                        jump.JumpDestination = label;
                        jump.BytecodeEditor = this;
                    }
                }
            }

            this.Instructions.AddRange(instructions);
            // List<BaseInstructionViewModel> instructions = new List<BaseInstructionViewModel>(node.Instructions.Count);
            // for (Instruction instruction = node.Instructions.First; instruction != null; instruction = instruction.Next) {
            //     instructions.Add(CreateInstructionForHandle(instruction));
            // }
        }

        public void Save(MethodNode node) {
            foreach (BaseInstructionViewModel instruction in this.RemovedInstructions) {
                node.Instructions.Remove(instruction.Node);
            }

            this.RemovedInstructions.Clear();
            foreach (BaseInstructionViewModel instruction in this.Instructions) {
                instruction.Save(instruction.Node);
            }
        }

        public static Type GetInstructionTypeForOpCode(Opcode opcode) {
            switch (opcode) {
                case Opcode.NOP:
                case Opcode.ACONST_NULL:
                case Opcode.ICONST_M1:
                case Opcode.ICONST_0:
                case Opcode.ICONST_1:
                case Opcode.ICONST_2:
                case Opcode.ICONST_3:
                case Opcode.ICONST_4:
                case Opcode.ICONST_5:
                case Opcode.LCONST_0:
                case Opcode.LCONST_1:
                case Opcode.FCONST_0:
                case Opcode.FCONST_1:
                case Opcode.FCONST_2:
                case Opcode.DCONST_0:
                case Opcode.DCONST_1:
                case Opcode.IALOAD:
                case Opcode.LALOAD:
                case Opcode.FALOAD:
                case Opcode.DALOAD:
                case Opcode.AALOAD:
                case Opcode.BALOAD:
                case Opcode.CALOAD:
                case Opcode.SALOAD:
                case Opcode.IASTORE:
                case Opcode.LASTORE:
                case Opcode.FASTORE:
                case Opcode.DASTORE:
                case Opcode.AASTORE:
                case Opcode.BASTORE:
                case Opcode.CASTORE:
                case Opcode.SASTORE:
                case Opcode.POP:
                case Opcode.POP2:
                case Opcode.DUP:
                case Opcode.DUP_X1:
                case Opcode.DUP_X2:
                case Opcode.DUP2:
                case Opcode.DUP2_X1:
                case Opcode.DUP2_X2:
                case Opcode.SWAP:
                case Opcode.IADD:
                case Opcode.LADD:
                case Opcode.FADD:
                case Opcode.DADD:
                case Opcode.ISUB:
                case Opcode.LSUB:
                case Opcode.FSUB:
                case Opcode.DSUB:
                case Opcode.IMUL:
                case Opcode.LMUL:
                case Opcode.FMUL:
                case Opcode.DMUL:
                case Opcode.IDIV:
                case Opcode.LDIV:
                case Opcode.FDIV:
                case Opcode.DDIV:
                case Opcode.IREM:
                case Opcode.LREM:
                case Opcode.FREM:
                case Opcode.DREM:
                case Opcode.INEG:
                case Opcode.LNEG:
                case Opcode.FNEG:
                case Opcode.DNEG:
                case Opcode.ISHL:
                case Opcode.LSHL:
                case Opcode.ISHR:
                case Opcode.LSHR:
                case Opcode.IUSHR:
                case Opcode.LUSHR:
                case Opcode.IAND:
                case Opcode.LAND:
                case Opcode.IOR:
                case Opcode.LOR:
                case Opcode.IXOR:
                case Opcode.LXOR:
                case Opcode.I2L:
                case Opcode.I2F:
                case Opcode.I2D:
                case Opcode.L2I:
                case Opcode.L2F:
                case Opcode.L2D:
                case Opcode.F2I:
                case Opcode.F2L:
                case Opcode.F2D:
                case Opcode.D2I:
                case Opcode.D2L:
                case Opcode.D2F:
                case Opcode.I2B:
                case Opcode.I2C:
                case Opcode.I2S:
                case Opcode.LCMP:
                case Opcode.FCMPL:
                case Opcode.FCMPG:
                case Opcode.DCMPL:
                case Opcode.DCMPG:
                case Opcode.IRETURN:
                case Opcode.LRETURN:
                case Opcode.FRETURN:
                case Opcode.DRETURN:
                case Opcode.ARETURN:
                case Opcode.RETURN:
                case Opcode.ARRAYLENGTH:
                case Opcode.ATHROW:
                case Opcode.MONITORENTER:
                case Opcode.MONITOREXIT:
                    return typeof(SimpleInstruction);
                case Opcode.BIPUSH:
                case Opcode.SIPUSH:
                    return typeof(IntegerPushInstruction);
                case Opcode.LDC:
                case Opcode.LDC_W:
                case Opcode.LDC2_W:
                    return typeof(LdcInstruction);
                case Opcode.ILOAD:
                case Opcode.LLOAD:
                case Opcode.FLOAD:
                case Opcode.DLOAD:
                case Opcode.ALOAD:
                case Opcode.ISTORE:
                case Opcode.LSTORE:
                case Opcode.FSTORE:
                case Opcode.DSTORE:
                case Opcode.ASTORE:
                case Opcode.RET:
                case Opcode.ILOAD_0:
                case Opcode.ILOAD_1:
                case Opcode.ILOAD_2:
                case Opcode.ILOAD_3:
                case Opcode.LLOAD_0:
                case Opcode.LLOAD_1:
                case Opcode.LLOAD_2:
                case Opcode.LLOAD_3:
                case Opcode.FLOAD_0:
                case Opcode.FLOAD_1:
                case Opcode.FLOAD_2:
                case Opcode.FLOAD_3:
                case Opcode.DLOAD_0:
                case Opcode.DLOAD_1:
                case Opcode.DLOAD_2:
                case Opcode.DLOAD_3:
                case Opcode.ALOAD_0:
                case Opcode.ALOAD_1:
                case Opcode.ALOAD_2:
                case Opcode.ALOAD_3:
                case Opcode.ISTORE_0:
                case Opcode.ISTORE_1:
                case Opcode.ISTORE_2:
                case Opcode.ISTORE_3:
                case Opcode.LSTORE_0:
                case Opcode.LSTORE_1:
                case Opcode.LSTORE_2:
                case Opcode.LSTORE_3:
                case Opcode.FSTORE_0:
                case Opcode.FSTORE_1:
                case Opcode.FSTORE_2:
                case Opcode.FSTORE_3:
                case Opcode.DSTORE_0:
                case Opcode.DSTORE_1:
                case Opcode.DSTORE_2:
                case Opcode.DSTORE_3:
                case Opcode.ASTORE_0:
                case Opcode.ASTORE_1:
                case Opcode.ASTORE_2:
                case Opcode.ASTORE_3:
                    return typeof(VariableInstruction);
                case Opcode.IINC: return typeof(IncrementInstruction);
                case Opcode.IFEQ:
                case Opcode.IFNE:
                case Opcode.IFLT:
                case Opcode.IFGE:
                case Opcode.IFGT:
                case Opcode.IFLE:
                case Opcode.IF_ICMPEQ:
                case Opcode.IF_ICMPNE:
                case Opcode.IF_ICMPLT:
                case Opcode.IF_ICMPGE:
                case Opcode.IF_ICMPGT:
                case Opcode.IF_ICMPLE:
                case Opcode.IF_ACMPEQ:
                case Opcode.IF_ACMPNE:
                case Opcode.GOTO:
                case Opcode.JSR:
                case Opcode.IFNULL:
                case Opcode.IFNONNULL:
                    return typeof(JumpInstruction);
                case Opcode.TABLESWITCH: return typeof(TableSwitchInstruction);
                case Opcode.LOOKUPSWITCH: return typeof(LookupSwitchInstruction);
                case Opcode.GETSTATIC:
                case Opcode.PUTSTATIC:
                case Opcode.GETFIELD:
                case Opcode.PUTFIELD:
                    return typeof(FieldInstruction);
                case Opcode.INVOKEVIRTUAL:
                case Opcode.INVOKESPECIAL:
                case Opcode.INVOKESTATIC:
                case Opcode.INVOKEINTERFACE:
                    return typeof(MethodInstruction);
                case Opcode.INVOKEDYNAMIC: return typeof(InvokeDynamicInstruction);
                case Opcode.NEW:
                case Opcode.ANEWARRAY:
                case Opcode.CHECKCAST:
                case Opcode.INSTANCEOF:
                    return typeof(TypeInstruction);
                case Opcode.NEWARRAY: return typeof(NewArrayInstruction);
                case Opcode.MULTIANEWARRAY: return typeof(MultiANewArrayInstruction);
                case Opcode.GOTO_W:
                case Opcode.JSR_W:
                    return typeof(JumpInstruction);
                default: throw new ArgumentException(nameof(opcode), "Unsupported opcode: " + opcode);
            }
        }

        public void Dispose() {
            this.SearchInstruction.Dispose();
        }
    }
}