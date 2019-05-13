namespace System.Runtime.MsilEngine
{
    //TODO: merge with System.Runtime.ConversionServices.MsilOpcodes
    public static class MsilOpCode
    {

        public interface IMsilOpCode { }

        public interface IOpCodeEmtpy { } // NOOP, Action() (){};
        public interface IOpCodeAction { };
        public interface IOpCodeNotImplemented { }
        public interface IOpCodeNotSupported { }
        public interface IOpCodeNullary { } //func<T>
        public interface IOpCodeUnary { }
        public interface IOpCodeBinary { }
        public interface IOpCodePushBinary : IOpCodeBinary { }

        public interface Nop : IMsilOpCode, IOpCodeEmtpy { }
        public interface Break : IMsilOpCode, IOpCodeAction { }
        public interface Ldarg_0 : IMsilOpCode, IOpCodePushBinary { } // Define Stack.Push(IOpCodeBinary);
        public interface Ldarg_1 : IMsilOpCode, IOpCodePushBinary { }       //Stack.Push(Array[Index])
        public interface Ldarg_2 : IMsilOpCode, IOpCodePushBinary { } // Action<Stack, Func<Array, Int, Object>();
        public interface Ldarg_3 : IMsilOpCode, IOpCodePushBinary { }
        public interface Ldloc_0 : IMsilOpCode, IOpCodePushBinary { }
        public interface Ldloc_1 : IMsilOpCode, IOpCodePushBinary { }
        public interface Ldloc_2 : IMsilOpCode, IOpCodePushBinary { }
        public interface Ldloc_3 : IMsilOpCode, IOpCodePushBinary { }
        public interface Stloc_0 : IMsilOpCode { } // Array[Index] = Stack.Pop())
        public interface Stloc_1 : IMsilOpCode { }
        public interface Stloc_2 : IMsilOpCode { }
        public interface Stloc_3 : IMsilOpCode { }
        public interface Ldarg_S : IMsilOpCode { }
        public interface Ldarga_S : IMsilOpCode { }
        public interface Starg_S : IMsilOpCode { }
        public interface Ldloc_S : IMsilOpCode { }
        public interface Ldloca_S : IMsilOpCode { }
        public interface Stloc_S : IMsilOpCode { }
        public interface Ldnull : IMsilOpCode { }
        public interface Ldc_I4_M1 : IMsilOpCode { }
        public interface Ldc_I4_0 : IMsilOpCode { }
        public interface Ldc_I4_1 : IMsilOpCode { }
        public interface Ldc_I4_2 : IMsilOpCode { }
        public interface Ldc_I4_3 : IMsilOpCode { }
        public interface Ldc_I4_4 : IMsilOpCode { }
        public interface Ldc_I4_5 : IMsilOpCode { }
        public interface Ldc_I4_6 : IMsilOpCode { }
        public interface Ldc_I4_7 : IMsilOpCode { }
        public interface Ldc_I4_8 : IMsilOpCode { }
        public interface Ldc_I4_S : IMsilOpCode { }
        public interface Ldc_I4 : IMsilOpCode { }
        public interface Ldc_I8 : IMsilOpCode { }
        public interface Ldc_R4 : IMsilOpCode { }
        public interface Ldc_R8 : IMsilOpCode { }
        public interface Dup : IMsilOpCode { }
        public interface Pop : IMsilOpCode { }
        public interface Jmp : IMsilOpCode { }
        public interface Call : IMsilOpCode { }
        public interface Calli : IMsilOpCode { }
        public interface Ret : IMsilOpCode { }
        public interface Br_S : IMsilOpCode, IOpCodeNullary { } //return Target address
        public interface Brfalse_S : IMsilOpCode, IOpCodeUnary { } //return +1 or Target address
        public interface Brtrue_S : IMsilOpCode, IOpCodeUnary { }
        public interface Beq_S : IMsilOpCode, IOpCodeBinary { }
        public interface Bge_S : IMsilOpCode, IOpCodeBinary { }
        public interface Bgt_S : IMsilOpCode, IOpCodeBinary { }
        public interface Ble_S : IMsilOpCode, IOpCodeBinary { }
        public interface Blt_S : IMsilOpCode, IOpCodeBinary { }
        public interface Bne_Un_S : IMsilOpCode, IOpCodeBinary { }
        public interface Bge_Un_S : IMsilOpCode, IOpCodeBinary { }
        public interface Bgt_Un_S : IMsilOpCode, IOpCodeBinary { }
        public interface Ble_Un_S : IMsilOpCode, IOpCodeBinary { }
        public interface Blt_Un_S : IMsilOpCode, IOpCodeBinary { }
        public interface Br : IMsilOpCode, IOpCodeNullary { } //return Target address 
        public interface Brfalse : IMsilOpCode, IOpCodeUnary { }
        public interface Brtrue : IMsilOpCode, IOpCodeUnary { }
        public interface Beq : IMsilOpCode, IOpCodeBinary { }
        public interface Bge : IMsilOpCode, IOpCodeBinary { }
        public interface Bgt : IMsilOpCode, IOpCodeBinary { }
        public interface Ble : IMsilOpCode, IOpCodeBinary { }
        public interface Blt : IMsilOpCode, IOpCodeBinary { }
        public interface Bne_Un : IMsilOpCode, IOpCodeBinary { }
        public interface Bge_Un : IMsilOpCode, IOpCodeBinary { }
        public interface Bgt_Un : IMsilOpCode, IOpCodeBinary { }
        public interface Ble_Un : IMsilOpCode, IOpCodeBinary { }
        public interface Blt_Un : IMsilOpCode, IOpCodeBinary { }
        public interface Switch : IMsilOpCode { }
        public interface Ldind_I1 : IMsilOpCode { }
        public interface Ldind_U1 : IMsilOpCode { }
        public interface Ldind_I2 : IMsilOpCode { }
        public interface Ldind_U2 : IMsilOpCode { }
        public interface Ldind_I4 : IMsilOpCode { }
        public interface Ldind_U4 : IMsilOpCode { }
        public interface Ldind_I8 : IMsilOpCode { }
        public interface Ldind_I : IMsilOpCode { }
        public interface Ldind_R4 : IMsilOpCode { }
        public interface Ldind_R8 : IMsilOpCode { }
        public interface Ldind_Ref : IMsilOpCode { }
        public interface Stind_Ref : IMsilOpCode { }
        public interface Stind_I1 : IMsilOpCode { }
        public interface Stind_I2 : IMsilOpCode { }
        public interface Stind_I4 : IMsilOpCode { }
        public interface Stind_I8 : IMsilOpCode { }
        public interface Stind_R4 : IMsilOpCode { }
        public interface Stind_R8 : IMsilOpCode { }
        public interface Add : IMsilOpCode, IOpCodeBinary { }
        public interface Sub : IMsilOpCode, IOpCodeBinary { }
        public interface Mul : IMsilOpCode, IOpCodeBinary { }
        public interface Div : IMsilOpCode, IOpCodeBinary { }
        public interface Div_Un : IMsilOpCode, IOpCodeBinary { }
        public interface Rem : IMsilOpCode, IOpCodeBinary { }
        public interface Rem_Un : IMsilOpCode, IOpCodeBinary { }
        public interface And : IMsilOpCode, IOpCodeBinary { }
        public interface Or : IMsilOpCode, IOpCodeBinary { }
        public interface Xor : IMsilOpCode, IOpCodeBinary { }
        public interface Shl : IMsilOpCode, IOpCodeBinary { }
        public interface Shr : IMsilOpCode, IOpCodeBinary { }
        public interface Shr_Un : IMsilOpCode, IOpCodeBinary { }
        public interface Neg : IMsilOpCode, IOpCodeUnary { }
        public interface Not : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_I1 : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_I2 : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_I4 : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_I8 : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_R4 : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_R8 : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_U4 : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_U8 : IMsilOpCode, IOpCodeUnary { }
        public interface Callvirt : IMsilOpCode { }
        public interface Cpobj : IMsilOpCode { }
        public interface Ldobj : IMsilOpCode { }
        public interface Ldstr : IMsilOpCode { }
        public interface Newobj : IMsilOpCode { }
        public interface Castclass : IMsilOpCode { }
        public interface Isinst : IMsilOpCode { }
        public interface Conv_R_Un : IMsilOpCode { }
        public interface Unbox : IMsilOpCode { }
        public interface Throw : IMsilOpCode { }
        public interface Ldfld : IMsilOpCode { }
        public interface Ldflda : IMsilOpCode { }
        public interface Stfld : IMsilOpCode { }
        public interface Ldsfld : IMsilOpCode { }
        public interface Ldsflda : IMsilOpCode { }
        public interface Stsfld : IMsilOpCode { }
        public interface Stobj : IMsilOpCode { }
        public interface Conv_Ovf_I1_Un : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_Ovf_I2_Un : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_Ovf_I4_Un : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_Ovf_I8_Un : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_Ovf_U1_Un : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_Ovf_U2_Un : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_Ovf_U4_Un : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_Ovf_U8_Un : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_Ovf_I_Un : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_Ovf_U_Un : IMsilOpCode, IOpCodeUnary { }
        public interface Box : IMsilOpCode, IOpCodeUnary { }
        public interface Newarr : IMsilOpCode { }
        public interface Ldlen : IMsilOpCode { }
        public interface Ldelema : IMsilOpCode { }
        public interface Ldelem_I1 : IMsilOpCode { }
        public interface Ldelem_U1 : IMsilOpCode { }
        public interface Ldelem_I2 : IMsilOpCode { }
        public interface Ldelem_U2 : IMsilOpCode { }
        public interface Ldelem_I4 : IMsilOpCode { }
        public interface Ldelem_U4 : IMsilOpCode { }
        public interface Ldelem_I8 : IMsilOpCode { }
        public interface Ldelem_I : IMsilOpCode { }
        public interface Ldelem_R4 : IMsilOpCode { }
        public interface Ldelem_R8 : IMsilOpCode { }
        public interface Ldelem_Ref : IMsilOpCode { }
        public interface Stelem_I : IMsilOpCode { }
        public interface Stelem_I1 : IMsilOpCode { }
        public interface Stelem_I2 : IMsilOpCode { }
        public interface Stelem_I4 : IMsilOpCode { }
        public interface Stelem_I8 : IMsilOpCode { }
        public interface Stelem_R4 : IMsilOpCode { }
        public interface Stelem_R8 : IMsilOpCode { }
        public interface Stelem_Ref : IMsilOpCode { }
        public interface Ldelem : IMsilOpCode { }
        public interface Stelem : IMsilOpCode { }
        public interface Unbox_Any : IMsilOpCode, IOpCodeBinary { }
        public interface Conv_Ovf_I1 : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_Ovf_U1 : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_Ovf_I2 : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_Ovf_U2 : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_Ovf_I4 : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_Ovf_U4 : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_Ovf_I8 : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_Ovf_U8 : IMsilOpCode, IOpCodeUnary { }
        public interface Refanyval : IMsilOpCode { }
        public interface Ckfinite : IMsilOpCode { }
        public interface Mkrefany : IMsilOpCode { }
        public interface Ldtoken : IMsilOpCode { }
        public interface Conv_U2 : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_U1 : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_I : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_Ovf_I : IMsilOpCode, IOpCodeUnary { }
        public interface Conv_Ovf_U : IMsilOpCode, IOpCodeUnary { }
        public interface Add_Ovf : IMsilOpCode, IOpCodeBinary { }
        public interface Add_Ovf_Un : IMsilOpCode, IOpCodeBinary { }
        public interface Mul_Ovf : IMsilOpCode, IOpCodeBinary { }
        public interface Mul_Ovf_Un : IMsilOpCode, IOpCodeBinary { }
        public interface Sub_Ovf : IMsilOpCode, IOpCodeBinary { }
        public interface Sub_Ovf_Un : IMsilOpCode, IOpCodeBinary { }
        public interface Endfinally : IMsilOpCode { }
        public interface Leave : IMsilOpCode { }
        public interface Leave_S : IMsilOpCode { }
        public interface Stind_I : IMsilOpCode { }
        public interface Conv_U : IMsilOpCode { }
        public interface Prefix7 : IMsilOpCode { }
        public interface Prefix6 : IMsilOpCode { }
        public interface Prefix5 : IMsilOpCode { }
        public interface Prefix4 : IMsilOpCode { }
        public interface Prefix3 : IMsilOpCode { }
        public interface Prefix2 : IMsilOpCode { }
        public interface Prefix1 : IMsilOpCode { }
        public interface Prefix0 : IMsilOpCode { }
        public interface Prefixref : IMsilOpCode { }
        public interface Arglist : IMsilOpCode { }
        public interface Ceq : IMsilOpCode, IOpCodeBinary { }
        public interface Cgt : IMsilOpCode, IOpCodeBinary { }
        public interface Cgt_Un : IMsilOpCode, IOpCodeBinary { }
        public interface Clt : IMsilOpCode, IOpCodeBinary { }
        public interface Clt_Un : IMsilOpCode, IOpCodeBinary { }
        public interface Ldftn : IMsilOpCode { }
        public interface Ldvirtftn : IMsilOpCode { }
        public interface Ldarg : IMsilOpCode { }
        public interface Ldarga : IMsilOpCode { }
        public interface Starg : IMsilOpCode { }
        public interface Ldloc : IMsilOpCode { }
        public interface Ldloca : IMsilOpCode { }
        public interface Stloc : IMsilOpCode { }
        public interface Localloc : IMsilOpCode { }
        public interface Endfilter : IMsilOpCode { }
        public interface Unaligned : IMsilOpCode { }
        public interface Volatile : IMsilOpCode { }
        public interface Tailcall : IMsilOpCode { }
        public interface Initobj : IMsilOpCode { }
        public interface Constrained : IMsilOpCode { }
        public interface Cpblk : IMsilOpCode { }
        public interface Initblk : IMsilOpCode { }
        public interface Rethrow : IMsilOpCode { }
        public interface Sizeof : IMsilOpCode { }
        public interface Refanytype : IMsilOpCode { }
        public interface Readonly : IMsilOpCode { }

    }
}
