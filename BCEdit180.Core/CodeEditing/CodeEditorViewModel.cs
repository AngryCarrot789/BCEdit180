﻿using System;
using BCEdit180.Core.CodeEditing.Bytecode.Locals;
using BCEdit180.Core.CodeEditing.ExceptionTable;
using BCEdit180.Core.CodeEditing.Splicing;
using BCEdit180.Core.ViewModels;
using JavaAsm;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.CodeEditing {
    public class CodeEditorViewModel : BaseViewModel, IDisposable {
        public BytecodeEditorViewModel ByteCodeEditor { get; }
        public ExceptionTableViewModel ExceptionEditor { get; }
        public LocalVariableTableViewModel LocalVariableTable { get; }
        public LocalVariableTypeTableViewModel LocalVariableTypeTable { get; }
        public CodeSplicerViewModel CodeSplicer { get; }

        public MethodInfoViewModel MethodInfo { get; }

        public CodeEditorViewModel(MethodInfoViewModel methodInfo) {
            this.MethodInfo = methodInfo;
            this.ByteCodeEditor = new BytecodeEditorViewModel(this);
            this.ExceptionEditor = new ExceptionTableViewModel(this);
            this.LocalVariableTable = new LocalVariableTableViewModel(this);
            this.LocalVariableTypeTable = new LocalVariableTypeTableViewModel(this);
            this.CodeSplicer = new CodeSplicerViewModel(this);
        }

        public void Load(MethodNode node) {
            this.ByteCodeEditor.Load(node);
            this.ExceptionEditor.Load(node);
            this.LocalVariableTable.Load(node);
            this.LocalVariableTypeTable.Load(node);
        }

        public void Save(MethodNode node) {
            this.ByteCodeEditor.Save(node);
            this.ExceptionEditor.Save(node);
            this.LocalVariableTable.Save(node);
            this.LocalVariableTypeTable.Save(node);
        }

        public void Dispose() {
            this.ByteCodeEditor.Dispose();
        }
    }
}
