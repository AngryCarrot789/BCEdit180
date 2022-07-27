using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.CodeEditing.Generate {
    public class CodeGeneratorViewModel : BaseViewModel {
        public ICommand GenerateCodeSequenceCommand { get; }

        public CodeGeneratorViewModel() {
            this.GenerateCodeSequenceCommand = new RelayCommand(GenerateCodeSequenceAction);
        }

        public void GenerateCodeSequenceAction() {

        }
    }
}
