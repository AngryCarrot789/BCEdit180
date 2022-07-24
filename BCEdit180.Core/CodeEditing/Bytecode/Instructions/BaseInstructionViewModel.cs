using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using BCEdit180.Core.Dialogs;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.CodeEditing.Bytecode.Instructions {
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
                    EditOpcode();
                }
            });
        }

        public async Task EditOpcode() {
            Opcode? opcode = await Dialog.TypeEditor.ChangeInstructionDialog(this.AvailableOpCodes);
            if (opcode.HasValue) {
                this.Opcode = opcode.Value;
            }
        }

        public static BaseInstructionViewModel ForInstruction(Instruction instruction) {
            if (instruction == null) {
                throw new NullReferenceException("Instruction cannot be null");
            }
            else if (instruction is FieldInstruction) {
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
                throw new ArgumentException("Unexpected instruction: " + instruction.GetType() + " -> " + instruction);
            }
        }

        public virtual void Load(Instruction instruction) {
            this.Instruction = instruction;
            this.Opcode = instruction.Opcode;
        }

        public virtual void Save(Instruction instruction) {
            if (this.CanEditOpCode) {
                instruction.Opcode = this.Opcode;
            }
        }

        public override string ToString() {
            return this.Instruction.ToString();
        }
    }
}