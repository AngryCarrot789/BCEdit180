namespace CSJavaClassLib {
    public static class Opcodes {

    // ASM API versions

    public const int ASM4 = 4 << 16 | 0 << 8 | 0;
    public const int ASM5 = 5 << 16 | 0 << 8 | 0;

    // versions

    public const int V1_1 = 3 << 16 | 45;
    public const int V1_2 = 0 << 16 | 46;
    public const int V1_3 = 0 << 16 | 47;
    public const int V1_4 = 0 << 16 | 48;
    public const int V1_5 = 0 << 16 | 49;
    public const int V1_6 = 0 << 16 | 50;
    public const int V1_7 = 0 << 16 | 51;
    public const int V1_8 = 0 << 16 | 52;

    // access flags

    public const int ACC_PUBLIC = 0x0001; // class, field, method
    public const int ACC_PRIVATE = 0x0002; // class, field, method
    public const int ACC_PROTECTED = 0x0004; // class, field, method
    public const int ACC_STATIC = 0x0008; // field, method
    public const int ACC_FINAL = 0x0010; // class, field, method, parameter
    public const int ACC_SUPER = 0x0020; // class
    public const int ACC_SYNCHRONIZED = 0x0020; // method
    public const int ACC_VOLATILE = 0x0040; // field
    public const int ACC_BRIDGE = 0x0040; // method
    public const int ACC_VARARGS = 0x0080; // method
    public const int ACC_TRANSIENT = 0x0080; // field
    public const int ACC_NATIVE = 0x0100; // method
    public const int ACC_INTERFACE = 0x0200; // class
    public const int ACC_ABSTRACT = 0x0400; // class, method
    public const int ACC_STRICT = 0x0800; // method
    public const int ACC_SYNTHETIC = 0x1000; // class, field, method, parameter
    public const int ACC_ANNOTATION = 0x2000; // class
    public const int ACC_ENUM = 0x4000; // class(?) field inner
    public const int ACC_MANDATED = 0x8000; // parameter

    // ASM specific pseudo access flags

    public const int ACC_DEPRECATED = 0x20000; // class, field, method

    // types for NEWARRAY

    public const int T_BOOLEAN = 4;
    public const int T_CHAR = 5;
    public const int T_FLOAT = 6;
    public const int T_DOUBLE = 7;
    public const int T_BYTE = 8;
    public const int T_SHORT = 9;
    public const int T_INT = 10;
    public const int T_LONG = 11;

    // tags for Handle

    public const int H_GETFIELD = 1;
    public const int H_GETSTATIC = 2;
    public const int H_PUTFIELD = 3;
    public const int H_PUTSTATIC = 4;
    public const int H_INVOKEVIRTUAL = 5;
    public const int H_INVOKESTATIC = 6;
    public const int H_INVOKESPECIAL = 7;
    public const int H_NEWINVOKESPECIAL = 8;
    public const int H_INVOKEINTERFACE = 9;

    // stack map frame types

    /**
     * Represents an expanded frame. See {@link ClassReader#EXPAND_FRAMES}.
     */
    public const int F_NEW = -1;

    /**
     * Represents a compressed frame with complete frame data.
     */
    public const int F_FULL = 0;

    /**
     * Represents a compressed frame where locals are the same as the locals in
     * the previous frame, except that additional 1-3 locals are defined, and
     * with an empty stack.
     */
    public const int F_APPEND = 1;

    /**
     * Represents a compressed frame where locals are the same as the locals in
     * the previous frame, except that the last 1-3 locals are absent and with
     * an empty stack.
     */
    public const int F_CHOP = 2;

    /**
     * Represents a compressed frame with exactly the same locals as the
     * previous frame and with an empty stack.
     */
    public const int F_SAME = 3;

    /**
     * Represents a compressed frame with exactly the same locals as the
     * previous frame and with a single value on the stack.
     */
    public const int F_SAME1 = 4;

    // Do not try to change the following code to use auto-boxing,
    // these values are compared by reference and not by value
    // The constructor of Integer was deprecated in 9
    // but we are stuck with it by backward compatibility
    public const int TOP = 0;
    public const int INTEGER = 1;
    public const int FLOAT = 2;
    public const int DOUBLE = 3;
    public const int LONG = 4;
    public const int NULL = 5;
    public const int UNINITIALIZED_THIS = 6;

    // opcodes // visit method (- = idem)

    public const int NOP = 0; // visitInsn
    public const int ACONST_NULL = 1; // -
    public const int ICONST_M1 = 2; // -
    public const int ICONST_0 = 3; // -
    public const int ICONST_1 = 4; // -
    public const int ICONST_2 = 5; // -
    public const int ICONST_3 = 6; // -
    public const int ICONST_4 = 7; // -
    public const int ICONST_5 = 8; // -
    public const int LCONST_0 = 9; // -
    public const int LCONST_1 = 10; // -
    public const int FCONST_0 = 11; // -
    public const int FCONST_1 = 12; // -
    public const int FCONST_2 = 13; // -
    public const int DCONST_0 = 14; // -
    public const int DCONST_1 = 15; // -
    public const int BIPUSH = 16; // visitIntInsn
    public const int SIPUSH = 17; // -
    public const int LDC = 18; // visitLdcInsn
    // public const int LDC_W = 19; // -
    // public const int LDC2_W = 20; // -
    public const int ILOAD = 21; // visitVarInsn
    public const int LLOAD = 22; // -
    public const int FLOAD = 23; // -
    public const int DLOAD = 24; // -
    public const int ALOAD = 25; // -
    // public const int ILOAD_0 = 26; // -
    // public const int ILOAD_1 = 27; // -
    // public const int ILOAD_2 = 28; // -
    // public const int ILOAD_3 = 29; // -
    // public const int LLOAD_0 = 30; // -
    // public const int LLOAD_1 = 31; // -
    // public const int LLOAD_2 = 32; // -
    // public const int LLOAD_3 = 33; // -
    // public const int FLOAD_0 = 34; // -
    // public const int FLOAD_1 = 35; // -
    // public const int FLOAD_2 = 36; // -
    // public const int FLOAD_3 = 37; // -
    // public const int DLOAD_0 = 38; // -
    // public const int DLOAD_1 = 39; // -
    // public const int DLOAD_2 = 40; // -
    // public const int DLOAD_3 = 41; // -
    // public const int ALOAD_0 = 42; // -
    // public const int ALOAD_1 = 43; // -
    // public const int ALOAD_2 = 44; // -
    // public const int ALOAD_3 = 45; // -
    public const int IALOAD = 46; // visitInsn
    public const int LALOAD = 47; // -
    public const int FALOAD = 48; // -
    public const int DALOAD = 49; // -
    public const int AALOAD = 50; // -
    public const int BALOAD = 51; // -
    public const int CALOAD = 52; // -
    public const int SALOAD = 53; // -
    public const int ISTORE = 54; // visitVarInsn
    public const int LSTORE = 55; // -
    public const int FSTORE = 56; // -
    public const int DSTORE = 57; // -
    public const int ASTORE = 58; // -
    // public const int ISTORE_0 = 59; // -
    // public const int ISTORE_1 = 60; // -
    // public const int ISTORE_2 = 61; // -
    // public const int ISTORE_3 = 62; // -
    // public const int LSTORE_0 = 63; // -
    // public const int LSTORE_1 = 64; // -
    // public const int LSTORE_2 = 65; // -
    // public const int LSTORE_3 = 66; // -
    // public const int FSTORE_0 = 67; // -
    // public const int FSTORE_1 = 68; // -
    // public const int FSTORE_2 = 69; // -
    // public const int FSTORE_3 = 70; // -
    // public const int DSTORE_0 = 71; // -
    // public const int DSTORE_1 = 72; // -
    // public const int DSTORE_2 = 73; // -
    // public const int DSTORE_3 = 74; // -
    // public const int ASTORE_0 = 75; // -
    // public const int ASTORE_1 = 76; // -
    // public const int ASTORE_2 = 77; // -
    // public const int ASTORE_3 = 78; // -
    public const int IASTORE = 79; // visitInsn
    public const int LASTORE = 80; // -
    public const int FASTORE = 81; // -
    public const int DASTORE = 82; // -
    public const int AASTORE = 83; // -
    public const int BASTORE = 84; // -
    public const int CASTORE = 85; // -
    public const int SASTORE = 86; // -
    public const int POP = 87; // -
    public const int POP2 = 88; // -
    public const int DUP = 89; // -
    public const int DUP_X1 = 90; // -
    public const int DUP_X2 = 91; // -
    public const int DUP2 = 92; // -
    public const int DUP2_X1 = 93; // -
    public const int DUP2_X2 = 94; // -
    public const int SWAP = 95; // -
    public const int IADD = 96; // -
    public const int LADD = 97; // -
    public const int FADD = 98; // -
    public const int DADD = 99; // -
    public const int ISUB = 100; // -
    public const int LSUB = 101; // -
    public const int FSUB = 102; // -
    public const int DSUB = 103; // -
    public const int IMUL = 104; // -
    public const int LMUL = 105; // -
    public const int FMUL = 106; // -
    public const int DMUL = 107; // -
    public const int IDIV = 108; // -
    public const int LDIV = 109; // -
    public const int FDIV = 110; // -
    public const int DDIV = 111; // -
    public const int IREM = 112; // -
    public const int LREM = 113; // -
    public const int FREM = 114; // -
    public const int DREM = 115; // -
    public const int INEG = 116; // -
    public const int LNEG = 117; // -
    public const int FNEG = 118; // -
    public const int DNEG = 119; // -
    public const int ISHL = 120; // -
    public const int LSHL = 121; // -
    public const int ISHR = 122; // -
    public const int LSHR = 123; // -
    public const int IUSHR = 124; // -
    public const int LUSHR = 125; // -
    public const int IAND = 126; // -
    public const int LAND = 127; // -
    public const int IOR = 128; // -
    public const int LOR = 129; // -
    public const int IXOR = 130; // -
    public const int LXOR = 131; // -
    public const int IINC = 132; // visitIincInsn
    public const int I2L = 133; // visitInsn
    public const int I2F = 134; // -
    public const int I2D = 135; // -
    public const int L2I = 136; // -
    public const int L2F = 137; // -
    public const int L2D = 138; // -
    public const int F2I = 139; // -
    public const int F2L = 140; // -
    public const int F2D = 141; // -
    public const int D2I = 142; // -
    public const int D2L = 143; // -
    public const int D2F = 144; // -
    public const int I2B = 145; // -
    public const int I2C = 146; // -
    public const int I2S = 147; // -
    public const int LCMP = 148; // -
    public const int FCMPL = 149; // -
    public const int FCMPG = 150; // -
    public const int DCMPL = 151; // -
    public const int DCMPG = 152; // -
    public const int IFEQ = 153; // visitJumpInsn
    public const int IFNE = 154; // -
    public const int IFLT = 155; // -
    public const int IFGE = 156; // -
    public const int IFGT = 157; // -
    public const int IFLE = 158; // -
    public const int IF_ICMPEQ = 159; // -
    public const int IF_ICMPNE = 160; // -
    public const int IF_ICMPLT = 161; // -
    public const int IF_ICMPGE = 162; // -
    public const int IF_ICMPGT = 163; // -
    public const int IF_ICMPLE = 164; // -
    public const int IF_ACMPEQ = 165; // -
    public const int IF_ACMPNE = 166; // -
    public const int GOTO = 167; // -
    public const int JSR = 168; // -
    public const int RET = 169; // visitVarInsn
    public const int TABLESWITCH = 170; // visiTableSwitchInsn
    public const int LOOKUPSWITCH = 171; // visitLookupSwitch
    public const int IRETURN = 172; // visitInsn
    public const int LRETURN = 173; // -
    public const int FRETURN = 174; // -
    public const int DRETURN = 175; // -
    public const int ARETURN = 176; // -
    public const int RETURN = 177; // -
    public const int GETSTATIC = 178; // visitFieldInsn
    public const int PUTSTATIC = 179; // -
    public const int GETFIELD = 180; // -
    public const int PUTFIELD = 181; // -
    public const int INVOKEVIRTUAL = 182; // visitMethodInsn
    public const int INVOKESPECIAL = 183; // -
    public const int INVOKESTATIC = 184; // -
    public const int INVOKEINTERFACE = 185; // -
    public const int INVOKEDYNAMIC = 186; // visitInvokeDynamicInsn
    public const int NEW = 187; // visitTypeInsn
    public const int NEWARRAY = 188; // visitIntInsn
    public const int ANEWARRAY = 189; // visitTypeInsn
    public const int ARRAYLENGTH = 190; // visitInsn
    public const int ATHROW = 191; // -
    public const int CHECKCAST = 192; // visitTypeInsn
    public const int INSTANCEOF = 193; // -
    public const int MONITORENTER = 194; // visitInsn
    public const int MONITOREXIT = 195; // -
    // public const int WIDE = 196; // NOT VISITED
    public const int MULTIANEWARRAY = 197; // visitMultiANewArrayInsn
    public const int IFNULL = 198; // visitJumpInsn
    public const int IFNONNULL = 199; // -
    // public const int GOTO_W = 200; // -
    // public const int JSR_W = 201; // -
    }
}