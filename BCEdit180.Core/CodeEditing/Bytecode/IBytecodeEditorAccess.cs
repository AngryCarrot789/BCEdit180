namespace BCEdit180.Core.CodeEditing.Bytecode {
    /// <summary>
    /// This class requires access to the bytecode editor which created this instruction
    /// <para>
    /// The bytecode editor reference is injected after the constructor for the instruction view model
    /// </para>
    /// </summary>
    public interface IBytecodeEditorAccess {
        BytecodeEditorViewModel BytecodeEditor { get; set; }
    }
}
