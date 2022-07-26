namespace BCEdit180.CodeEditing.EditorControls {
    // I don't see any real reason to put these in separate files :)
    // I could also use use the BaseInstructionEditorControl and in the resource dictionary,
    // just use a key for a specific template... but meh. this allows for upgrades in the future :)
    public class FieldInstructionEditorControl : BaseInstructionEditorControl {}
    public class IncrementInstructionEditorControl : BaseInstructionEditorControl {}
    public class IntegerPushInstructionEditorControl : BaseInstructionEditorControl {}
    public class InvokeDynamicInstructionEditorControl : BaseInstructionEditorControl {}
    public class JumpInstructionEditorControl : BaseInstructionEditorControl {}
    public class LabelEditorControl : BaseInstructionEditorControl {}
    public class LdcInstructionEditorControl : BaseInstructionEditorControl {}
    public class LineNumberEditorControl : BaseInstructionEditorControl {}
    public class LookupSwitchInstructionEditorControl : BaseInstructionEditorControl {}
    public class MethodInstructionEditorControl : BaseInstructionEditorControl {}
    public class MultiANewArrayInstructionEditorControl : BaseInstructionEditorControl {}
    public class NewArrayInstructionEditorControl : BaseInstructionEditorControl {}
    public class SimpleInstructionEditorControl : BaseInstructionEditorControl {}
    public class StackMapFrameEditorControl : BaseInstructionEditorControl {}
    public class TableSwitchInstructionEditorControl : BaseInstructionEditorControl {}
    public class TypeInstructionEditorControl : BaseInstructionEditorControl {}
    public class VariableInstructionEditorControl : BaseInstructionEditorControl {}
}