using JavaAsm.Instructions;

namespace BCEdit180.Core.CodeEditing {
    public static class InstructionDescriptionRegistry {
        public static string GetDescription(Opcode opcode) {
            switch (opcode) {
                case Opcode.None:
                    return "No instruction";
                case Opcode.AALOAD:
                    return "Loads a reference/object from an array\n\nOperand stack: ..., arrayref, index -> ..., value";
                case Opcode.AASTORE:
                    return "Stores a reference/object into an array\n\nOperand stack: ..., arrayref, index -> ...";
                case Opcode.ACONST_NULL:
                    return "Loads null into the operand stack";
                case Opcode.ALOAD:
                    return "Loads a reference/object from a local variable\n\nOperand stack: ... -> ..., objectref";
                case Opcode.ALOAD_0:
                    return "Loads a reference/object from local variable 0, which is usually 'this' for instance methods\n\nOperand stack: ... -> ..., objectref";
                case Opcode.ALOAD_1:
                    return "Loads a reference/object from local variable 1\n\nOperand stack: ... -> ..., objectref";
                case Opcode.ALOAD_2:
                    return "Loads a reference/object from local variable 2\n\nOperand stack: ... -> ..., objectref";
                case Opcode.ALOAD_3:
                    return "Loads a reference/object from local variable 3\n\nOperand stack: ... -> ..., objectref";
                case Opcode.ANEWARRAY:
                    return "No description currently";
                case Opcode.ARETURN:
                    return "No description currently";
                case Opcode.ARRAYLENGTH:
                    return "No description currently";
                case Opcode.ASTORE:
                    return "No description currently";
                case Opcode.ASTORE_0:
                    return "No description currently";
                case Opcode.ASTORE_1:
                    return "No description currently";
                case Opcode.ASTORE_2:
                    return "No description currently";
                case Opcode.ASTORE_3:
                    return "No description currently";
                case Opcode.ATHROW:
                    return "No description currently";
                case Opcode.BALOAD:
                    return "No description currently";
                case Opcode.BASTORE:
                    return "No description currently";
                case Opcode.BIPUSH:
                    return "No description currently";
                case Opcode.BREAKPOINT:
                    return "No description currently";
                case Opcode.CALOAD:
                    return "No description currently";
                case Opcode.CASTORE:
                    return "No description currently";
                case Opcode.CHECKCAST:
                    return "No description currently";
                case Opcode.D2F:
                    return "No description currently";
                case Opcode.D2I:
                    return "No description currently";
                case Opcode.D2L:
                    return "No description currently";
                case Opcode.DADD:
                    return "No description currently";
                case Opcode.DALOAD:
                    return "No description currently";
                case Opcode.DASTORE:
                    return "No description currently";
                case Opcode.DCMPG:
                    return "No description currently";
                case Opcode.DCMPL:
                    return "No description currently";
                case Opcode.DCONST_0:
                    return "No description currently";
                case Opcode.DCONST_1:
                    return "No description currently";
                case Opcode.DDIV:
                    return "No description currently";
                case Opcode.DLOAD:
                    return "No description currently";
                case Opcode.DLOAD_0:
                    return "No description currently";
                case Opcode.DLOAD_1:
                    return "No description currently";
                case Opcode.DLOAD_2:
                    return "No description currently";
                case Opcode.DLOAD_3:
                    return "No description currently";
                case Opcode.DMUL:
                    return "No description currently";
                case Opcode.DNEG:
                    return "No description currently";
                case Opcode.DREM:
                    return "No description currently";
                case Opcode.DRETURN:
                    return "No description currently";
                case Opcode.DSTORE:
                    return "No description currently";
                case Opcode.DSTORE_0:
                    return "No description currently";
                case Opcode.DSTORE_1:
                    return "No description currently";
                case Opcode.DSTORE_2:
                    return "No description currently";
                case Opcode.DSTORE_3:
                    return "No description currently";
                case Opcode.DSUB:
                    return "No description currently";
                case Opcode.DUP:
                    return "No description currently";
                case Opcode.DUP_X1:
                    return "No description currently";
                case Opcode.DUP_X2:
                    return "No description currently";
                case Opcode.DUP2:
                    return "No description currently";
                case Opcode.DUP2_X1:
                    return "No description currently";
                case Opcode.DUP2_X2:
                    return "No description currently";
                case Opcode.F2D:
                    return "No description currently";
                case Opcode.F2I:
                    return "No description currently";
                case Opcode.F2L:
                    return "No description currently";
                case Opcode.FADD:
                    return "No description currently";
                case Opcode.FALOAD:
                    return "No description currently";
                case Opcode.FASTORE:
                    return "No description currently";
                case Opcode.FCMPG:
                    return "No description currently";
                case Opcode.FCMPL:
                    return "No description currently";
                case Opcode.FCONST_0:
                    return "No description currently";
                case Opcode.FCONST_1:
                    return "No description currently";
                case Opcode.FCONST_2:
                    return "No description currently";
                case Opcode.FDIV:
                    return "No description currently";
                case Opcode.FLOAD:
                    return "No description currently";
                case Opcode.FLOAD_0:
                    return "No description currently";
                case Opcode.FLOAD_1:
                    return "No description currently";
                case Opcode.FLOAD_2:
                    return "No description currently";
                case Opcode.FLOAD_3:
                    return "No description currently";
                case Opcode.FMUL:
                    return "No description currently";
                case Opcode.FNEG:
                    return "No description currently";
                case Opcode.FREM:
                    return "No description currently";
                case Opcode.FRETURN:
                    return "No description currently";
                case Opcode.FSTORE:
                    return "No description currently";
                case Opcode.FSTORE_0:
                    return "No description currently";
                case Opcode.FSTORE_1:
                    return "No description currently";
                case Opcode.FSTORE_2:
                    return "No description currently";
                case Opcode.FSTORE_3:
                    return "No description currently";
                case Opcode.FSUB:
                    return "No description currently";
                case Opcode.GETFIELD:
                    return "Fetches a field's value from an object instance";
                case Opcode.GETSTATIC:
                    return "Fetches a static field's value";
                case Opcode.GOTO:
                    return "No description currently";
                case Opcode.GOTO_W:
                    return "No description currently";
                case Opcode.I2B:
                    return "No description currently";
                case Opcode.I2C:
                    return "No description currently";
                case Opcode.I2D:
                    return "No description currently";
                case Opcode.I2F:
                    return "No description currently";
                case Opcode.I2L:
                    return "No description currently";
                case Opcode.I2S:
                    return "No description currently";
                case Opcode.IADD:
                    return "No description currently";
                case Opcode.IALOAD:
                    return "No description currently";
                case Opcode.IAND:
                    return "No description currently";
                case Opcode.IASTORE:
                    return "No description currently";
                case Opcode.ICONST_M1:
                    return "No description currently";
                case Opcode.ICONST_0:
                    return "No description currently";
                case Opcode.ICONST_1:
                    return "No description currently";
                case Opcode.ICONST_2:
                    return "No description currently";
                case Opcode.ICONST_3:
                    return "No description currently";
                case Opcode.ICONST_4:
                    return "No description currently";
                case Opcode.ICONST_5:
                    return "No description currently";
                case Opcode.IDIV:
                    return "No description currently";
                case Opcode.IF_ACMPEQ:
                    return "No description currently";
                case Opcode.IF_ACMPNE:
                    return "No description currently";
                case Opcode.IF_ICMPEQ:
                    return "No description currently";
                case Opcode.IF_ICMPGE:
                    return "No description currently";
                case Opcode.IF_ICMPGT:
                    return "No description currently";
                case Opcode.IF_ICMPLE:
                    return "No description currently";
                case Opcode.IF_ICMPLT:
                    return "No description currently";
                case Opcode.IF_ICMPNE:
                    return "No description currently";
                case Opcode.IFEQ:
                    return "No description currently";
                case Opcode.IFGE:
                    return "No description currently";
                case Opcode.IFGT:
                    return "No description currently";
                case Opcode.IFLE:
                    return "No description currently";
                case Opcode.IFLT:
                    return "No description currently";
                case Opcode.IFNE:
                    return "No description currently";
                case Opcode.IFNONNULL:
                    return "No description currently";
                case Opcode.IFNULL:
                    return "No description currently";
                case Opcode.IINC:
                    return "No description currently";
                case Opcode.ILOAD:
                    return "No description currently";
                case Opcode.ILOAD_0:
                    return "No description currently";
                case Opcode.ILOAD_1:
                    return "No description currently";
                case Opcode.ILOAD_2:
                    return "No description currently";
                case Opcode.ILOAD_3:
                    return "No description currently";
                case Opcode.IMUL:
                    return "No description currently";
                case Opcode.INEG:
                    return "No description currently";
                case Opcode.INSTANCEOF:
                    return "No description currently";
                case Opcode.INVOKEDYNAMIC:
                    return "Too many words required to describe this op code. But in a nutshell, it returns a reference to a functional interface represented by a method handle (e.g method reference, lambda as a static method, etc)";
                case Opcode.INVOKEINTERFACE:
                    return "Invokes an interface function";
                case Opcode.INVOKESPECIAL:
                    return "Invokes an instance method in special conditions, such as directly invoking a superclass method (without virtual dispatch), private methods, and constructors";
                case Opcode.INVOKESTATIC:
                    return "Invokes a static method";
                case Opcode.INVOKEVIRTUAL:
                    return "Invokes an instance method. The method that is actually invoked depends on the object's hierarchy; virtual dispatch";
                case Opcode.IOR:
                    return "No description currently";
                case Opcode.IREM:
                    return "No description currently";
                case Opcode.IRETURN:
                    return "No description currently";
                case Opcode.ISHL:
                    return "No description currently";
                case Opcode.ISHR:
                    return "No description currently";
                case Opcode.ISTORE:
                    return "No description currently";
                case Opcode.ISTORE_0:
                    return "No description currently";
                case Opcode.ISTORE_1:
                    return "No description currently";
                case Opcode.ISTORE_2:
                    return "No description currently";
                case Opcode.ISTORE_3:
                    return "No description currently";
                case Opcode.ISUB:
                    return "No description currently";
                case Opcode.IUSHR:
                    return "No description currently";
                case Opcode.IXOR:
                    return "No description currently";
                case Opcode.JSR:
                    return "No description currently";
                case Opcode.JSR_W:
                    return "No description currently";
                case Opcode.L2D:
                    return "No description currently";
                case Opcode.L2F:
                    return "No description currently";
                case Opcode.L2I:
                    return "No description currently";
                case Opcode.LADD:
                    return "No description currently";
                case Opcode.LALOAD:
                    return "No description currently";
                case Opcode.LAND:
                    return "No description currently";
                case Opcode.LASTORE:
                    return "No description currently";
                case Opcode.LCMP:
                    return "No description currently";
                case Opcode.LCONST_0:
                    return "No description currently";
                case Opcode.LCONST_1:
                    return "No description currently";
                case Opcode.LDC:
                    return "No description currently";
                case Opcode.LDC_W:
                    return "No description currently";
                case Opcode.LDC2_W:
                    return "No description currently";
                case Opcode.LDIV:
                    return "No description currently";
                case Opcode.LLOAD:
                    return "No description currently";
                case Opcode.LLOAD_0:
                    return "No description currently";
                case Opcode.LLOAD_1:
                    return "No description currently";
                case Opcode.LLOAD_2:
                    return "No description currently";
                case Opcode.LLOAD_3:
                    return "No description currently";
                case Opcode.LMUL:
                    return "No description currently";
                case Opcode.LNEG:
                    return "No description currently";
                case Opcode.LOOKUPSWITCH:
                    return "No description currently";
                case Opcode.LOR:
                    return "No description currently";
                case Opcode.LREM:
                    return "No description currently";
                case Opcode.LRETURN:
                    return "No description currently";
                case Opcode.LSHL:
                    return "No description currently";
                case Opcode.LSHR:
                    return "No description currently";
                case Opcode.LSTORE:
                    return "No description currently";
                case Opcode.LSTORE_0:
                    return "No description currently";
                case Opcode.LSTORE_1:
                    return "No description currently";
                case Opcode.LSTORE_2:
                    return "No description currently";
                case Opcode.LSTORE_3:
                    return "No description currently";
                case Opcode.LSUB:
                    return "No description currently";
                case Opcode.LUSHR:
                    return "No description currently";
                case Opcode.LXOR:
                    return "No description currently";
                case Opcode.MONITORENTER:
                    return "Enters a monitor for a specific object";
                case Opcode.MONITOREXIT:
                    return "Exits a monitor for a specific object";
                case Opcode.MULTIANEWARRAY:
                    return "No description currently";
                case Opcode.NEW:
                    return "Allocates a new (uninitialised) object.";
                case Opcode.NEWARRAY:
                    return "No description currently";
                case Opcode.NOP:
                    return "No description currently";
                case Opcode.POP:
                    return "No description currently";
                case Opcode.POP2:
                    return "No description currently";
                case Opcode.PUTFIELD:
                    return "No description currently";
                case Opcode.PUTSTATIC:
                    return "No description currently";
                case Opcode.RET:
                    return "No description currently";
                case Opcode.RETURN:
                    return "No description currently";
                case Opcode.SALOAD:
                    return "No description currently";
                case Opcode.SASTORE:
                    return "No description currently";
                case Opcode.SIPUSH:
                    return "No description currently";
                case Opcode.SWAP:
                    return "No description currently";
                case Opcode.TABLESWITCH:
                    return "No description currently";
                case Opcode.WIDE:
                    return "No description currently";
                default:
                    return "No description currently";
            }
        }
    }
}
