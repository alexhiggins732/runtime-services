using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.ConversionServices.MsilOpcodes.MsilOpcodeInterfaces;

namespace System.Runtime.ConversionServices.MsilOpcodes
{
    public struct IOpCode_Neg<T>
    {
        public static Func<T, T> Op;// => NegateTest.Negate
    }

    public struct ILUnaryOpCode<TOpCode, TOperand, TResult>
    {
        public static Func<TOperand, TResult> Op;// => NegateTest.Negate
    }

    public class UnaryBuilder<T>
    {
        public Func<Func<T, T>> NullUnaryFunc()
        {
            return null;
        }
        public Func<Func<T, T>> GetUnaryFuncCaller(Func<T, T> callback)
        {
            return () => callback;
        }
    }

    public class NegateTest
    {
        public static Func<int, int> Negate = (i) => -i;

        public Func<Func<T, T>> BuildNegateFunc<T>()
        {
            return null;
        }
        public static Func<Func<T, T>> GetUnaryFuncCaller<T>(Func<T, T> callback)
        {
            return () => callback;
        }
        public static Func<T, T> GetUnaryFunc<T>(Func<T, T> callback)
        {
            return callback;
        }

        public static void SetNegate<T>(Func<T, T> callback)
        {
            IOpCode_Neg<T>.Op = callback;
        }

        public static void SetOpCodeCallBack<ILUnaryOpCode, T, TResult>(Func<T, TResult> callback)
        {
            ILUnaryOpCode<ILUnaryOpCode, T, TResult>.Op = callback;
        }

        public static void Run()
        {
            IOpCode_Neg<int>.Op = Negate;
            var unaryBuilder = new UnaryBuilder<int>();
            var fn = unaryBuilder.NullUnaryFunc();
            var fn2 = unaryBuilder.GetUnaryFuncCaller((i) => -1);

            var sbyteFnCallback = GetUnaryFuncCaller<sbyte>((i) => (sbyte)-i);
            var sbyteFn = GetUnaryFunc<sbyte>((i) => (sbyte)-i);


            var fnResult = sbyteFn(1);

            IOpCode_Neg<sbyte>.Op = GetUnaryFunc<sbyte>((i) => (sbyte)-i);

            SetNegate<sbyte>((i) => (sbyte)-i);
            SetNegate<byte>((i) => (byte)-i);
            SetNegate<short>((i) => (short)-i);
            SetNegate<ushort>((i) => (ushort)-i);

            SetOpCodeCallBack<IOpCode_Neg, bool, bool>((i) => !i);
            SetOpCodeCallBack<IOpCode_Neg, char, int>((i) => -i);
            SetOpCodeCallBack<IOpCode_Neg, short, int>((i) => -i);
            SetOpCodeCallBack<IOpCode_Neg, byte, int>((i) => -i);
            SetOpCodeCallBack<IOpCode_Neg, short, int>((i) => -i);
            SetOpCodeCallBack<IOpCode_Neg, ushort, int>((i) => -i);
            SetOpCodeCallBack<IOpCode_Neg, int, int>((i) => -i);
            SetOpCodeCallBack<IOpCode_Neg, uint, long>((i) => -i);
            SetOpCodeCallBack<IOpCode_Neg, long, long>((i) => -i);
            SetOpCodeCallBack<IOpCode_Neg, ulong, long>((i) => -(long)i);
            SetOpCodeCallBack<IOpCode_Neg, float, float>((i) => -i);
            SetOpCodeCallBack<IOpCode_Neg, double, double>((i) => -i);
            SetOpCodeCallBack<IOpCode_Neg, decimal, decimal>((i) => -i);
            SetOpCodeCallBack<IOpCode_Neg, BigInteger, BigInteger>((i) => -i);

            SetOpCodeCallBack<IOpCode_Not, bool, bool>((i) => !i);
            SetOpCodeCallBack<IOpCode_Not, char, int>((i) => ~i);
            SetOpCodeCallBack<IOpCode_Not, short, int>((i) => ~i);
            SetOpCodeCallBack<IOpCode_Not, byte, int>((i) => ~i);
            SetOpCodeCallBack<IOpCode_Not, short, int>((i) => ~i);
            SetOpCodeCallBack<IOpCode_Not, ushort, int>((i) => ~i);
            SetOpCodeCallBack<IOpCode_Not, int, int>((i) => ~i);
            SetOpCodeCallBack<IOpCode_Not, uint, long>((i) => ~i);
            SetOpCodeCallBack<IOpCode_Not, long, long>((i) => ~i);
            SetOpCodeCallBack<IOpCode_Not, ulong, ulong>((i) => ~i);
            SetOpCodeCallBack<IOpCode_Not, float, float>((i) => throw new InvalidOperationException());
            SetOpCodeCallBack<IOpCode_Not, double, double>((i) => throw new InvalidOperationException());
            SetOpCodeCallBack<IOpCode_Not, decimal, decimal>((i) => throw new InvalidOperationException());
            SetOpCodeCallBack<IOpCode_Not, BigInteger, BigInteger>((i) => ~i);




        }
        public static void SetStackFrameMethods()
        {
            FnMocker.SetArrayValue = (stack, array, index) =>
            {
                array.SetValue(stack.Pop(), index);
            };



            FnMocker.PushFromArray = (stack, array, index) =>
            {
                stack.Push(array.GetValue(index));
            };



        }
        public static void TestMockStack()
        {
            var args = new object[] { "a", 1ul, 1m, DateTime.Now };
            var locals = new object[] { "a", 1ul, 1m, DateTime.Now };
            var stack = new Stack<object>();
            FnMocker.LocalsResolver = () => locals;
            FnMocker.ArgsResolver = () => args;
            FnMocker.StackResolver = () => stack;

            FnMocker.PushFromArray(stack, locals, 0);
            FnMocker.SetArrayValue(stack, locals, 0);

        }
        public class FnMocker
        {
            public static Func<Array> ArrayResolver;
            public static Func<Array> LocalsResolver;
            public static Func<Array> ArgsResolver;
            public static Func<Stack<Object>> StackResolver;

            public static Action<Stack<object>, Array, int> SetArrayValue;
            public static Action<Stack<object>, Func<Array>, int> SetArrayValueWithResolver;

            public static Action<Func<Stack<object>>, Func<Array>, Func<int>> SetArrayValueWithAllResolvers
                => (stack, array, index) => { array().SetValue(stack().Pop(), index()); };
            public static Action<Func<Stack<object>>, Func<Array>, Func<int>> PushFromArrayWithAllResolvers
                => (stack, array, index) => { stack().Push(array().GetValue(index())); };

            public static Action<Stack<object>, Array, int> PushFromArray;

            public static Action LdLoc0_WithResolvers
                => () => PushFromArrayWithAllResolvers(StackResolver, LocalsResolver, () => 0);

            public static Action LdLoc0 => () => PushFromArray(StackResolver(), LocalsResolver(), 0);
            public static Action LdLoc1 => () => PushFromArray(StackResolver(), LocalsResolver(), 1);
            public static Action LdLoc2 => () => PushFromArray(StackResolver(), LocalsResolver(), 2);
            public static Action LdLoc3 => () => PushFromArray(StackResolver(), LocalsResolver(), 3);

            public static Action LdLoc_s(int index) => () => PushFromArray(StackResolver(), LocalsResolver(), index);
            public static Action LdLoc(int index) => () => PushFromArray(StackResolver(), LocalsResolver(), index);

            public static Action StLoc0 => () => SetArrayValue(StackResolver(), LocalsResolver(), 0);
            public static Action StLoc1 => () => SetArrayValue(StackResolver(), LocalsResolver(), 1);
            public static Action StLoc2 => () => SetArrayValue(StackResolver(), LocalsResolver(), 2);
            public static Action StLoc3 => () => SetArrayValue(StackResolver(), LocalsResolver(), 3);

            public static Action LdArg0 => () => PushFromArray(StackResolver(), ArgsResolver(), 0);
            public static Action LdArg1 => () => PushFromArray(StackResolver(), ArgsResolver(), 1);
            public static Action LdArg2 => () => PushFromArray(StackResolver(), ArgsResolver(), 2);
            public static Action LdArg3 => () => PushFromArray(StackResolver(), ArgsResolver(), 3);

            public static Action StArg0 => () => SetArrayValue(StackResolver(), ArgsResolver(), 0);
            public static Action StArg1 => () => SetArrayValue(StackResolver(), ArgsResolver(), 1);
            public static Action StArg2 => () => SetArrayValue(StackResolver(), ArgsResolver(), 2);
            public static Action StArg3 => () => SetArrayValue(StackResolver(), ArgsResolver(), 3);
        }
    }


    public class MsilOpcodeInterfaces
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

        public interface IOpCode_Nop : IMsilOpCode, IOpCodeEmtpy { }
        public interface IOpCode_Break : IMsilOpCode, IOpCodeAction { }
        public interface IOpCode_Ldarg_0 : IMsilOpCode, IOpCodePushBinary { } // Define Stack.Push(IOpCodeBinary);
        public interface IOpCode_Ldarg_1 : IMsilOpCode, IOpCodePushBinary { }       //Stack.Push(Array[Index])
        public interface IOpCode_Ldarg_2 : IMsilOpCode, IOpCodePushBinary { } // Action<Stack, Func<Array, Int, Object>();
        public interface IOpCode_Ldarg_3 : IMsilOpCode, IOpCodePushBinary { }
        public interface IOpCode_Ldloc_0 : IMsilOpCode, IOpCodePushBinary { }
        public interface IOpCode_Ldloc_1 : IMsilOpCode, IOpCodePushBinary { }
        public interface IOpCode_Ldloc_2 : IMsilOpCode, IOpCodePushBinary { }
        public interface IOpCode_Ldloc_3 : IMsilOpCode, IOpCodePushBinary { }
        public interface IOpCode_Stloc_0 : IMsilOpCode { } // Array[Index] = Stack.Pop())
        public interface IOpCode_Stloc_1 : IMsilOpCode { }
        public interface IOpCode_Stloc_2 : IMsilOpCode { }
        public interface IOpCode_Stloc_3 : IMsilOpCode { }
        public interface IOpCode_Ldarg_s : IMsilOpCode { }
        public interface IOpCode_Ldarga_s : IMsilOpCode { }
        public interface IOpCode_Starg_s : IMsilOpCode { }
        public interface IOpCode_Ldloc_s : IMsilOpCode { }
        public interface IOpCode_Ldloca_s : IMsilOpCode { }
        public interface IOpCode_Stloc_s : IMsilOpCode { }
        public interface IOpCode_Ldnull : IMsilOpCode { }
        public interface IOpCode_Ldc_i4_m1 : IMsilOpCode { }
        public interface IOpCode_Ldc_i4_0 : IMsilOpCode { }
        public interface IOpCode_Ldc_i4_1 : IMsilOpCode { }
        public interface IOpCode_Ldc_i4_2 : IMsilOpCode { }
        public interface IOpCode_Ldc_i4_3 : IMsilOpCode { }
        public interface IOpCode_Ldc_i4_4 : IMsilOpCode { }
        public interface IOpCode_Ldc_i4_5 : IMsilOpCode { }
        public interface IOpCode_Ldc_i4_6 : IMsilOpCode { }
        public interface IOpCode_Ldc_i4_7 : IMsilOpCode { }
        public interface IOpCode_Ldc_i4_8 : IMsilOpCode { }
        public interface IOpCode_Ldc_i4_s : IMsilOpCode { }
        public interface IOpCode_Ldc_i4 : IMsilOpCode { }
        public interface IOpCode_Ldc_i8 : IMsilOpCode { }
        public interface IOpCode_Ldc_r4 : IMsilOpCode { }
        public interface IOpCode_Ldc_r8 : IMsilOpCode { }
        public interface IOpCode_Dup : IMsilOpCode { }
        public interface IOpCode_Pop : IMsilOpCode { }
        public interface IOpCode_Jmp : IMsilOpCode { }
        public interface IOpCode_Call : IMsilOpCode { }
        public interface IOpCode_Calli : IMsilOpCode { }
        public interface IOpCode_Ret : IMsilOpCode { }
        public interface IOpCode_Br_s : IMsilOpCode, IOpCodeNullary { } //return Target address
        public interface IOpCode_Brfalse_s : IMsilOpCode, IOpCodeUnary { } //return +1 or Target address
        public interface IOpCode_Brtrue_s : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Beq_s : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Bge_s : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Bgt_s : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Ble_s : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Blt_s : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Bne_un_s : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Bge_un_s : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Bgt_un_s : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Ble_un_s : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Blt_un_s : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Br : IMsilOpCode, IOpCodeNullary { } //return Target address 
        public interface IOpCode_Brfalse : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Brtrue : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Beq : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Bge : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Bgt : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Ble : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Blt : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Bne_un : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Bge_un : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Bgt_un : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Ble_un : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Blt_un : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Switch : IMsilOpCode { }
        public interface IOpCode_Ldind_i1 : IMsilOpCode { }
        public interface IOpCode_Ldind_u1 : IMsilOpCode { }
        public interface IOpCode_Ldind_i2 : IMsilOpCode { }
        public interface IOpCode_Ldind_u2 : IMsilOpCode { }
        public interface IOpCode_Ldind_i4 : IMsilOpCode { }
        public interface IOpCode_Ldind_u4 : IMsilOpCode { }
        public interface IOpCode_Ldind_i8 : IMsilOpCode { }
        public interface IOpCode_Ldind_i : IMsilOpCode { }
        public interface IOpCode_Ldind_r4 : IMsilOpCode { }
        public interface IOpCode_Ldind_r8 : IMsilOpCode { }
        public interface IOpCode_Ldind_ref : IMsilOpCode { }
        public interface IOpCode_Stind_ref : IMsilOpCode { }
        public interface IOpCode_Stind_i1 : IMsilOpCode { }
        public interface IOpCode_Stind_i2 : IMsilOpCode { }
        public interface IOpCode_Stind_i4 : IMsilOpCode { }
        public interface IOpCode_Stind_i8 : IMsilOpCode { }
        public interface IOpCode_Stind_r4 : IMsilOpCode { }
        public interface IOpCode_Stind_r8 : IMsilOpCode { }
        public interface IOpCode_Add : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Sub : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Mul : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Div : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Div_un : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Rem : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Rem_un : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_And : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Or : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Xor : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Shl : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Shr : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Shr_un : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Neg : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Not : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_i1 : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_i2 : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_i4 : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_i8 : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_r4 : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_r8 : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_u4 : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_u8 : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Callvirt : IMsilOpCode { }
        public interface IOpCode_Cpobj : IMsilOpCode { }
        public interface IOpCode_Ldobj : IMsilOpCode { }
        public interface IOpCode_Ldstr : IMsilOpCode { }
        public interface IOpCode_Newobj : IMsilOpCode { }
        public interface IOpCode_Castclass : IMsilOpCode { }
        public interface IOpCode_Isinst : IMsilOpCode { }
        public interface IOpCode_Conv_r_un : IMsilOpCode { }
        public interface IOpCode_Unbox : IMsilOpCode { }
        public interface IOpCode_Throw : IMsilOpCode { }
        public interface IOpCode_Ldfld : IMsilOpCode { }
        public interface IOpCode_Ldflda : IMsilOpCode { }
        public interface IOpCode_Stfld : IMsilOpCode { }
        public interface IOpCode_Ldsfld : IMsilOpCode { }
        public interface IOpCode_Ldsflda : IMsilOpCode { }
        public interface IOpCode_Stsfld : IMsilOpCode { }
        public interface IOpCode_Stobj : IMsilOpCode { }
        public interface IOpCode_Conv_ovf_i1_un : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_ovf_i2_un : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_ovf_i4_un : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_ovf_i8_un : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_ovf_u1_un : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_ovf_u2_un : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_ovf_u4_un : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_ovf_u8_un : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_ovf_i_un : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_ovf_u_un : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Box : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Newarr : IMsilOpCode { }
        public interface IOpCode_Ldlen : IMsilOpCode { }
        public interface IOpCode_Ldelema : IMsilOpCode { }
        public interface IOpCode_Ldelem_i1 : IMsilOpCode { }
        public interface IOpCode_Ldelem_u1 : IMsilOpCode { }
        public interface IOpCode_Ldelem_i2 : IMsilOpCode { }
        public interface IOpCode_Ldelem_u2 : IMsilOpCode { }
        public interface IOpCode_Ldelem_i4 : IMsilOpCode { }
        public interface IOpCode_Ldelem_u4 : IMsilOpCode { }
        public interface IOpCode_Ldelem_i8 : IMsilOpCode { }
        public interface IOpCode_Ldelem_i : IMsilOpCode { }
        public interface IOpCode_Ldelem_r4 : IMsilOpCode { }
        public interface IOpCode_Ldelem_r8 : IMsilOpCode { }
        public interface IOpCode_Ldelem_ref : IMsilOpCode { }
        public interface IOpCode_Stelem_i : IMsilOpCode { }
        public interface IOpCode_Stelem_i1 : IMsilOpCode { }
        public interface IOpCode_Stelem_i2 : IMsilOpCode { }
        public interface IOpCode_Stelem_i4 : IMsilOpCode { }
        public interface IOpCode_Stelem_i8 : IMsilOpCode { }
        public interface IOpCode_Stelem_r4 : IMsilOpCode { }
        public interface IOpCode_Stelem_r8 : IMsilOpCode { }
        public interface IOpCode_Stelem_ref : IMsilOpCode { }
        public interface IOpCode_Ldelem : IMsilOpCode { }
        public interface IOpCode_Stelem : IMsilOpCode { }
        public interface IOpCode_Unbox_any : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Conv_ovf_i1 : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_ovf_u1 : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_ovf_i2 : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_ovf_u2 : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_ovf_i4 : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_ovf_u4 : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_ovf_i8 : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_ovf_u8 : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Refanyval : IMsilOpCode { }
        public interface IOpCode_Ckfinite : IMsilOpCode { }
        public interface IOpCode_Mkrefany : IMsilOpCode { }
        public interface IOpCode_Ldtoken : IMsilOpCode { }
        public interface IOpCode_Conv_u2 : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_u1 : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_i : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_ovf_i : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Conv_ovf_u : IMsilOpCode, IOpCodeUnary { }
        public interface IOpCode_Add_ovf : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Add_ovf_un : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Mul_ovf : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Mul_ovf_un : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Sub_ovf : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Sub_ovf_un : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Endfinally : IMsilOpCode { }
        public interface IOpCode_Leave : IMsilOpCode { }
        public interface IOpCode_Leave_s : IMsilOpCode { }
        public interface IOpCode_Stind_i : IMsilOpCode { }
        public interface IOpCode_Conv_u : IMsilOpCode { }
        public interface IOpCode_Prefix7 : IMsilOpCode { }
        public interface IOpCode_Prefix6 : IMsilOpCode { }
        public interface IOpCode_Prefix5 : IMsilOpCode { }
        public interface IOpCode_Prefix4 : IMsilOpCode { }
        public interface IOpCode_Prefix3 : IMsilOpCode { }
        public interface IOpCode_Prefix2 : IMsilOpCode { }
        public interface IOpCode_Prefix1 : IMsilOpCode { }
        public interface IOpCode_255 : IMsilOpCode { }
        public interface IOpCode_Arglist : IMsilOpCode { }
        public interface IOpCode_Ceq : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Cgt : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Cgt_un : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Clt : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Clt_un : IMsilOpCode, IOpCodeBinary { }
        public interface IOpCode_Ldftn : IMsilOpCode { }
        public interface IOpCode_Ldvirtftn : IMsilOpCode { }
        public interface IOpCode_Ldarg : IMsilOpCode { }
        public interface IOpCode_Ldarga : IMsilOpCode { }
        public interface IOpCode_Starg : IMsilOpCode { }
        public interface IOpCode_Ldloc : IMsilOpCode { }
        public interface IOpCode_Ldloca : IMsilOpCode { }
        public interface IOpCode_Stloc : IMsilOpCode { }
        public interface IOpCode_Localloc : IMsilOpCode { }
        public interface IOpCode_Endfilter : IMsilOpCode { }
        public interface IOpCode_Unaligned : IMsilOpCode { }
        public interface IOpCode_Volatile : IMsilOpCode { }
        public interface IOpCode_Tail : IMsilOpCode { }
        public interface IOpCode_Initobj : IMsilOpCode { }
        public interface IOpCode_Constrained : IMsilOpCode { }
        public interface IOpCode_Cpblk : IMsilOpCode { }
        public interface IOpCode_Initblk : IMsilOpCode { }
        public interface IOpCode_Rethrow : IMsilOpCode { }
        public interface IOpCode_Sizeof : IMsilOpCode { }
        public interface IOpCode_Refanytype : IMsilOpCode { }
        public interface IOpCode_Readonly : IMsilOpCode { }
    }
  
}


