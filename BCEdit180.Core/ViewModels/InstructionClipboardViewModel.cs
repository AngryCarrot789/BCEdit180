using System.Collections.Generic;
using BCEdit180.Core.CodeEditing.Bytecode.Instructions;
using BCEdit180.Core.Collections;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.ViewModels {
    public class InstructionClipboardViewModel : BaseViewModel {
        public ExtendedObservableCollection<BaseInstructionViewModel> Instructions { get; }

        public InstructionClipboardViewModel() {
            this.Instructions = new ExtendedObservableCollection<BaseInstructionViewModel>();
        }

        public void SetClipboard(IEnumerable<BaseInstructionViewModel> list) {
            this.Instructions.Clear();
            this.Instructions.AddRange(list);
        }
    }
}