using System;
using System.Collections.Generic;
using JavaAsm.Instructions;

namespace BCEdit180.Core.CodeEditing {
    public static class InstructionDescriptionRegistry {
        private static readonly Dictionary<Opcode, OpcodeDescriptorViewModel> DescriptorMap;

        static InstructionDescriptionRegistry() {
            DescriptorMap = new Dictionary<Opcode, OpcodeDescriptorViewModel>();
            SetDescription(Opcode.None, "Nothing", null, "No instruction");
            SetDescription(Opcode.AALOAD, "Loads a reference/object from an array",   "..., arrayref, index -> ..., value");
            SetDescription(Opcode.AASTORE, "Stores a reference/object into an array", ".., arrayref, index, value -> ...");
            SetDescription(Opcode.ACONST_NULL, "Pushes null into the operand stack",  "... -> ..., objectref", "Loads null into the operand stack");
            SetDescription(Opcode.ALOAD,   "Loads a reference/object from a local variable", "... -> ..., objectref(null)");
            SetDescription(Opcode.ALOAD_0, "Loads a reference/object from local variable 0, which is usually 'this' for instance methods", "... -> ..., objectref");
            SetDescription(Opcode.ALOAD_1, "Loads a reference/object from local variable 1", "... -> ..., objectref");
            SetDescription(Opcode.ALOAD_2, "Loads a reference/object from local variable 2", "... -> ..., objectref");
            SetDescription(Opcode.ALOAD_3, "Loads a reference/object from local variable 3", "... -> ..., objectref");
            SetDescription(Opcode.ANEWARRAY, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ARETURN, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ARRAYLENGTH, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ASTORE, "Stores a reference/object into a local variable", "..., objectref -> ...", "(Description to be added)");
            SetDescription(Opcode.ASTORE_0, "Stores a reference/object into the local variable at index 0", "..., objectref -> ...", "(Description to be added)");
            SetDescription(Opcode.ASTORE_1, "Stores a reference/object into the local variable at index 1", "..., objectref -> ...", "(Description to be added)");
            SetDescription(Opcode.ASTORE_2, "Stores a reference/object into the local variable at index 2", "..., objectref -> ...", "(Description to be added)");
            SetDescription(Opcode.ASTORE_3, "Stores a reference/object into the local variable at index 3", "..., objectref -> ...", "(Description to be added)");
            SetDescription(Opcode.ATHROW, "Throws an exception", "..., objectref -> objectref", "(Description to be added)");
            SetDescription(Opcode.BALOAD, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.BASTORE, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.BIPUSH, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.BREAKPOINT, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.CALOAD, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.CASTORE, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.CHECKCAST, "Check whether an object is of a given type", "(Operand stack to be added)", "This is done when you cast an instance to another type (e.g casting an object to a string)");
            SetDescription(Opcode.D2F, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.D2I, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.D2L, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DADD, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DALOAD, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DASTORE, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DCMPG, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DCMPL, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DCONST_0, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DCONST_1, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DDIV, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DLOAD, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DLOAD_0, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DLOAD_1, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DLOAD_2, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DLOAD_3, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DMUL, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DNEG, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DREM, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DRETURN, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DSTORE, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DSTORE_0, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DSTORE_1, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DSTORE_2, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DSTORE_3, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DSUB, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DUP, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DUP_X1, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DUP_X2, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DUP2, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DUP2_X1, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.DUP2_X2, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.F2D, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.F2I, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.F2L, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FADD, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FALOAD, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FASTORE, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FCMPG, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FCMPL, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FCONST_0, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FCONST_1, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FCONST_2, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FDIV, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FLOAD, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FLOAD_0, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FLOAD_1, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FLOAD_2, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FLOAD_3, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FMUL, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FNEG, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FREM, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FRETURN, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FSTORE, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FSTORE_0, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FSTORE_1, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FSTORE_2, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FSTORE_3, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.FSUB, "(to be added)", "(to be added)", "(to be added)");
            SetDescription(Opcode.GETFIELD, "Fetches a field's value from an object instance", "..., objectref -> ..., value");
            SetDescription(Opcode.GETSTATIC, "Fetches a static field's value", "... -> ..., value");
            SetDescription(Opcode.GOTO, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.GOTO_W, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.I2B, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.I2C, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.I2D, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.I2F, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.I2L, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.I2S, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IADD, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IALOAD, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IAND, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IASTORE, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ICONST_M1, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ICONST_0, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ICONST_1, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ICONST_2, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ICONST_3, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ICONST_4, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ICONST_5, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IDIV, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IF_ACMPEQ, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IF_ACMPNE, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IF_ICMPEQ, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IF_ICMPGE, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IF_ICMPGT, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IF_ICMPLE, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IF_ICMPLT, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IF_ICMPNE, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IFEQ, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IFGE, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IFGT, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IFLE, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IFLT, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IFNE, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IFNONNULL, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IFNULL, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IINC, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ILOAD, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ILOAD_0, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ILOAD_1, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ILOAD_2, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ILOAD_3, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IMUL, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.INEG, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.INSTANCEOF, "(to be added)", "(to be added)", "(to be added)");
            SetDescription(Opcode.INVOKEDYNAMIC, "Dynamically invokes a function", "..., [arg1, [arg2 ...]] -> ...", "In a nutshell, it returns a reference to a functional interface represented by a method handle (e.g method reference, lambda as a static method, etc)");
            SetDescription(Opcode.INVOKEINTERFACE, "Invokes an interface function", "..., objectref, [arg1, [arg2 ...]] -> ...", "(to be added)");
            SetDescription(Opcode.INVOKESPECIAL, "Invokes an instance method in special conditions", "..., objectref, [arg1, [arg2 ...]] -> ...", "These special conditions include directly invoking a superclass method (without virtual dispatch), private methods, and constructors");
            SetDescription(Opcode.INVOKESTATIC, "Invokes a static method", "..., [arg1, [arg2 ...]] -> ...", "(to be added)");
            SetDescription(Opcode.INVOKEVIRTUAL, "Invokes an instance method", "..., objectref, [arg1, [arg2 ...]] -> ...", "The method that is actually invoked depends on the object's hierarchy; virtual dispatch");
            SetDescription(Opcode.IOR, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IREM, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IRETURN, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ISHL, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ISHR, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ISTORE, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ISTORE_0, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ISTORE_1, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ISTORE_2, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ISTORE_3, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.ISUB, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IUSHR, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.IXOR, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.JSR, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.JSR_W, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.L2D, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.L2F, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.L2I, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LADD, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LALOAD, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LAND, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LASTORE, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LCMP, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LCONST_0, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LCONST_1, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LDC, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LDC_W, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LDC2_W, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LDIV, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LLOAD, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LLOAD_0, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LLOAD_1, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LLOAD_2, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LLOAD_3, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LMUL, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LNEG, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LOOKUPSWITCH, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LOR, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LREM, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LRETURN, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LSHL, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LSHR, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LSTORE, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LSTORE_0, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LSTORE_1, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LSTORE_2, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LSTORE_3, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LSUB, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LUSHR, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.LXOR, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.MONITORENTER, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.MONITOREXIT, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.MULTIANEWARRAY, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.NEW, "Allocates a new (uninitialised) object", "(to be added)", "After NEW is called, the object's constructor must be invoked before it can be used");
            SetDescription(Opcode.NEWARRAY, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.NOP, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.POP, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.POP2, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.PUTFIELD, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.PUTSTATIC, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.RET, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.RETURN, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.SALOAD, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.SASTORE, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.SIPUSH, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.SWAP, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.TABLESWITCH, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
            SetDescription(Opcode.WIDE, "(Header to be added)", "(Operand stack to be added)", "(Description to be added)");
        }

        private static void SetDescription(Opcode opcode, OpcodeDescriptorViewModel descriptor) {
            DescriptorMap[opcode] = descriptor;
        }

        private static void SetDescription(Opcode opcode, string header, string stackTransition, string description = null) {
            DescriptorMap[opcode] = new OpcodeDescriptorViewModel() {
                Header = header,
                StackTransition = stackTransition ?? "(Operand stack remains unmodified)",
                Description = description ?? "(Description to be added)"
            };
        }

        public static OpcodeDescriptorViewModel GetDescription(Opcode opcode) {
            if (DescriptorMap.TryGetValue(opcode, out OpcodeDescriptorViewModel vm)) {
                return vm;
            }

            throw new Exception("Missing description for opcode: " + opcode);
        }
    }
}
