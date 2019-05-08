using System;
using System.Text;
using System.Threading.Tasks;

namespace StaticConversionsGenerator
{
    public class GenericArithmetic
    {
        public static void Test()
        {

            GenericAdder<int, int, int>.FnAdd = (a, b) => a + b;
            GenericAdd<int, int>.FnAdd = (a, b) => new RuntimeReference<int> { Value = a + b };


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
            ulong iu8 = 0;

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
    public interface IRuntimeReference { }
    public interface IGenericAdder
    {
        IRuntimeReference Add(IRuntimeReference a, IRuntimeReference b);
    }
    public struct RuntimeReference<T> : IRuntimeReference
    {
        public T Value;
        public static implicit operator RuntimeReference<T>(T value) => new RuntimeReference<T> { Value = value };
    }

    public struct GenericAdder
    {
        public static IRuntimeReference Add(object a, object b)
        {
            var tca = TypeCodeInfoFactory.GetTypeCodeInfo(a.GetType());
            var tcb = TypeCodeInfoFactory.GetTypeCodeInfo(b.GetType());
            return null;
            //return tca.OpAdd<tcb.Default>().OpAdd(a, b);
            //tca.Add(b);

        }
    }

    public struct GenericAdder<T1, T2, TResult>
    //: IGenericAdder
    {
        public static Func<T1, T2, TResult> FnAdd = null;

        //Support for runtime boxed objects
        public static IRuntimeReference Add(IRuntimeReference a, IRuntimeReference b)
        {
            var refa = (RuntimeReference<T1>)(object)a;
            var refb = (RuntimeReference<T2>)(object)b;
            return (IRuntimeReference)Add(refa, refb);
        }

        //Support for statically compiled linkage
        public static TResult Add(RuntimeReference<T1> a, RuntimeReference<T2> b)
        {
            if (FnAdd != null)
                return FnAdd(a.Value, b.Value);
            return default(TResult);
        }

        //not sure if this is needed.
        public static RuntimeReference<TResult>
            AddRef(RuntimeReference<T1> a, RuntimeReference<T2> b)
        {
            //return default(RuntimeReference<TResult>);
            if (FnAdd != null)
                return new RuntimeReference<TResult> { Value = FnAdd(a.Value, b.Value) };
            return default(RuntimeReference<TResult>);
        }
    }

    public struct GenericAdd<T1, T2> : IBinaryOperator
    {
        public static Func<T1, T2, IRuntimeReference> FnAdd = null;
        public IRuntimeReference OpAdd<TIn1,TIn2> (TIn1 a, TIn2 b) 
            => FnAdd((T1)(object)a, (T2)(object)b);

   
    }

    public interface IBinaryOperator
    {
        IRuntimeReference OpAdd<T1, T2>(T1 a, T2 b);
    }

    public interface ITypeCodeInfo
    {
        object Default { get; }
        IBinaryOperator OpAdd<TOther>();
    }

}
