using BCEdit180.Core.CodeEditing.Bytecode.Instructions;

namespace BCEdit180.Core.CodeEditing.Bytecode {
    public interface ILabelTargeter {
        LabelViewModel TargetLabel { get; set; }
    }
}