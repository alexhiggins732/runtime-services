using System;
using System.Text;
using System.Threading.Tasks;

namespace System.Runtime.ConversionServices
{
    public class GenericArithmeticTest
    {
        public static void Test()
        {

            GenericAdder<int, int, int>.FnAdd = (a, b) => a + b;
            GenericBinaryOp<int, int>.FnAdd = (a, b) => new RuntimeTypedReference<int> { Value = a + b };


            var result = GenericAdder<int, int, int>.Add(1, 2);

            var numerics = TypeCodeInfoFactory.NumericTypeCodeInfos;

            foreach (var source in numerics)
            {
                foreach (var dest in numerics)
                {
                    var additionResult = GenericAdder.Add(source.Default, dest.Default);
                }
            }



            sbyte i1 = 0;
            short i2 = 0;
            int i4 = 1;
            long i8 = 0;

            byte u1 = 0;
            ushort u2 = 0;
            uint u4 = 1;
            ulong u8 = 0;

            var u1u2 = u1 + u2;
            var u4u8 = u4 + u8;

            var i1i2 = i1 + i2;
            var resultI1I2 = GenericAdder<int, int, int>.Add(i1, i2);


            var i2i1 = i2 + i1;

            var i1i4 = i1 + i4;
            var i1i8 = i1 + i8;

            var i2i4 = i2 + i4;
            var i2i8 = i2 + i8;

            var i4i4 = i4 + i4;
            var i4i8 = i4 + i8;

        }
    }
    //public interface IRuntimeReference { }
    public interface IGenericAdder
    {
        IRuntimeTypedReference Add(IRuntimeTypedReference a, IRuntimeTypedReference b);
    }
    //public struct RuntimeReference<T> : IRuntimeReference
    //{
    //    public T Value;
    //    public static implicit operator RuntimeReference<T>(T value) => new RuntimeReference<T> { Value = value };
    //}


    //TODO: Unify GenericAdder, GenericAdder, GenericAdd
    public struct GenericAdder
    {
        public static IRuntimeTypedReference Add(object a, object b)
        {
            var tca = TypeCodeInfoFactory.GetTypeCodeInfo(a.GetType());
            var tcb = TypeCodeInfoFactory.GetTypeCodeInfo(b.GetType());
            return null;
            //return tca.OpAdd<tcb.Default>().OpAdd(a, b);
            //tca.Add(b);

        }
    }

    public struct GenericAdder<T1, T2, TResult>
    {
        public static Func<T1, T2, TResult> FnAdd = null;

        //Support for runtime boxed objects
        public static IRuntimeTypedReference Add(IRuntimeTypedReference a, IRuntimeTypedReference b)
        {
            var refa = (RuntimeTypedReference<T1>)(object)a;
            var refb = (RuntimeTypedReference<T2>)(object)b;
            return (IRuntimeTypedReference)Add(refa, refb);
        }

        public static TResult Add(T1 a, T2 b)
        {
            var refa = (RuntimeTypedReference<T1>)(object)a;
            var refb = (RuntimeTypedReference<T2>)(object)b;
            return Add(refa, refb).To<TResult>();
        }

        //Support for statically compiled linkage
        public static TResult Add(RuntimeTypedReference<T1> a, RuntimeTypedReference<T2> b)
        {
            if (FnAdd != null)
                return FnAdd(a.Value, b.Value);
            return default;// (TResult);
        }

        //not sure if this is needed.
        public static RuntimeTypedReference<TResult>
            AddRef(RuntimeTypedReference<T1> a, RuntimeTypedReference<T2> b)
        {
            //return default(RuntimeReference<TResult>);
            if (FnAdd != null)
                return new RuntimeTypedReference<TResult> { Value = FnAdd(a.Value, b.Value) };
            return default; // (RuntimeTypedReference<TResult>);
        }
    }

    public struct GenericBinaryOp<T1, T2> : IBinaryOperator
    {
        public static Func<T1, T2, IRuntimeTypedReference> FnAdd = null;

        public IRuntimeTypedReference OpAdd<TIn1, TIn2>(TIn1 a, TIn2 b)
            => FnAdd((T1)(object)a, (T2)(object)b);

        public static Func<T1, T2, IRuntimeTypedReference> Op_Add = null;

        public static Func<T1, T2, IRuntimeTypedReference> FnSubtract = null;

        public IRuntimeTypedReference OpSubtract<TIn1, TIn2>(TIn1 a, TIn2 b)
            => FnSubtract((T1)(object)a, (T2)(object)b);

        public static Func<T1, T2, IRuntimeTypedReference> Op_Subtract = null;

    }



    public interface IBinaryOperator
    {
        IRuntimeTypedReference OpAdd<T1, T2>(T1 a, T2 b);
        IRuntimeTypedReference OpSubtract<T1, T2>(T1 a, T2 b);
    }


    public interface ITypeCodeInfo
    {
        object Default { get; }
        IBinaryOperator OpAdd<TOther>();
    }

}
