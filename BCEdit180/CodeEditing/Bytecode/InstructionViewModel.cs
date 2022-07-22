using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.CodeEditing.Bytecode {
    // non-abstract, so that opcodes can fall back to the base class and get ToString()'d instead of custom layouts
    public class BaseInstructionViewModel : BaseViewModel {
        public Instruction Instruction { get; protected set; }

        private Opcode opCode;
        public Opcode Opcode {
            get => this.opCode;
            set => RaisePropertyChanged(ref this.opCode, value);
        }


        private IEnumerable<Opcode> allOpCodes;
        public virtual IEnumerable<Opcode> AvailableOpcodes {
            get {
                if (this.allOpCodes == null) {
                    List<Opcode> codes = new List<Opcode>();
                    foreach (object value in Enum.GetValues(typeof(Opcode))) {
                        codes.Add((Opcode) value);
                    }

                    this.allOpCodes = codes;
                }

                return this.allOpCodes;
            }
        }

        public ICommand EditOpcodeCommand { get; }

        public BaseInstructionViewModel(Instruction instruction) {
            this.Instruction = instruction;
            this.EditOpcodeCommand = new RelayCommand(()=> {
                IEnumerable<Opcode> ops = this.AvailableOpcodes;
                if (ops != null) {
                    ViewManager.ShowEditInstructionView(ops, (o) => this.Opcode = o);
                }
            });
        }

        public static BaseInstructionViewModel ForInstruction(Instruction instruction) {
            if (instruction is FieldInstruction) {
                return new FieldInstructionViewModel(instruction);
            }
            else if (instruction is IncrementInstruction) {
                return new IncrementInstructionViewModel(instruction);
            }
            else if (instruction is IntegerPushInstruction) {
                return new IntegerPushInstructionViewModel(instruction);
            }
            else if (instruction is InvokeDynamicInstruction) {
                return new InvokeDynamicInstructionViewModel(instruction);
            }
            else if (instruction is JumpInstruction) {
                return new JumpInstructionViewModel(instruction);
            }
            else if (instruction is Label) {
                return new BaseInstructionViewModel(instruction);
            }
            else if (instruction is LdcInstruction) {
                return new LdcInstructionViewModel(instruction);
            }
            else if (instruction is LineNumber) {
                return new BaseInstructionViewModel(instruction);
            }
            else if (instruction is LookupSwitchInstruction) {
                return new BaseInstructionViewModel(instruction);
            }
            else if (instruction is MethodInstruction) {
                return new MethodInstructionViewModel(instruction);
            }
            else if (instruction is MultiANewArrayInstruction) {
                return new BaseInstructionViewModel(instruction);
            }
            else if (instruction is NewArrayInstruction) {
                return new BaseInstructionViewModel(instruction);
            }
            else if (instruction is SimpleInstruction) {
                return new BaseInstructionViewModel(instruction);
            }
            else if (instruction is StackMapFrame) {
                return new BaseInstructionViewModel(instruction);
            }
            else if (instruction is TableSwitchInstruction) {
                return new BaseInstructionViewModel(instruction);
            }
            else if (instruction is TypeInstruction) {
                return new TypeInstructionViewModel(instruction);
            }
            else if (instruction is VariableInstruction) {
                return new VariableInstructionViewModel(instruction);
            }
            else if (instruction != null) {
                Debug.WriteLine("Unexpected instruction type: " + instruction.GetType() + " -> " + instruction);
                return new BaseInstructionViewModel(instruction);
            }
            else {
                throw new NullReferenceException("Instruction cannot be null");
            }
        }

        public virtual void Load(Instruction instruction) {
            this.Opcode = instruction.Opcode;
        }

        public virtual void Save(Instruction instruction) {
            instruction.Opcode = this.Opcode;
        }
    }
}
