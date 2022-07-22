﻿using System.Collections.ObjectModel;
using BCEdit180.CodeEditing.Bytecode;
using JavaAsm;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.CodeEditing {
    public class BytecodeEditorViewModel : BaseViewModel {
        public CodeEditorViewModel CodeEditor { get; }

        public ObservableCollection<BaseInstructionViewModel> Instructions { get; }

        public BytecodeEditorViewModel(CodeEditorViewModel codeEditor) {
            this.CodeEditor = codeEditor;
            this.Instructions = new ObservableCollection<BaseInstructionViewModel>();
        }

        public void Load(MethodNode node) {
            this.Instructions.Clear();
            for (Instruction instruction = node.Instructions.First; instruction != null; instruction = instruction.Next) {
                LoadInstruction(instruction);
            }
        }

        public void Save(MethodNode node) {

        }

        public void LoadInstruction(Instruction instruction) {
            if (instruction is VariableInstruction) {
                this.Instructions.Add(new VariableInstructionViewModel(instruction));
            }
            else {
                this.Instructions.Add(new BaseInstructionViewModel(instruction));
            }

            // if (instruction is FieldInstruction) {
            //     LoadFieldInstruction((VariableInstruction) instruction);
            // }
            // else if (instruction is IncrementInstruction) {
            //     LoadIncrementInstruction((VariableInstruction) instruction);
            // }
            // else if (instruction is IntegerPushInstruction) {
            //     LoadIntegerPushInstruction((VariableInstruction) instruction);
            // }
            // else if (instruction is InvokeDynamicInstruction) {
            //     LoadInvokeDynamicInstruction((VariableInstruction) instruction);
            // }
            // else if (instruction is JumpInstruction) {
            //     LoadJumpInstruction((VariableInstruction) instruction);
            // }
            // else if (instruction is Label) {
            //     LoadLabel((VariableInstruction) instruction);
            // }
            // else if (instruction is LdcInstruction) {
            //     LoadLdcInstruction((VariableInstruction) instruction);
            // }
            // else if (instruction is LineNumber) {
            //     LoadLineNumber((VariableInstruction) instruction);
            // }
            // else if (instruction is LookupSwitchInstruction) {
            //     LoadLookupSwitchInstruction((VariableInstruction) instruction);
            // }
            // else if (instruction is MethodInstruction) {
            //     LoadMethodInstruction((VariableInstruction) instruction);
            // }
            // else if (instruction is MultiANewArrayInstruction) {
            //     LoadMultiANewArrayInstruction((VariableInstruction) instruction);
            // }
            // else if (instruction is NewArrayInstruction) {
            //     LoadNewArrayInstruction((VariableInstruction) instruction);
            // }
            // else if (instruction is SimpleInstruction) {
            //     LoadSimpleInstruction((VariableInstruction) instruction);
            // }
            // else if (instruction is StackMapFrame) {
            //     LoadStackMapFrame((VariableInstruction) instruction);
            // }
            // else if (instruction is TableSwitchInstruction) {
            //     LoadTableSwitchInstruction((VariableInstruction) instruction);
            // }
            // else if (instruction is TypeInstruction) {
            //     LoadTypeInstruction((VariableInstruction) instruction);
            // }
            // else if (instruction is VariableInstruction) {
            //     LoadVariableInstruction((VariableInstruction) instruction);
            // }
        }

        public void LoadVariableInstruction(VariableInstruction instruction) {

        }
    }
}
