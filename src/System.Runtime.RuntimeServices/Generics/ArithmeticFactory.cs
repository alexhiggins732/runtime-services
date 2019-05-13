using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Runtime.RuntimeServices
{
    public interface ICallable
    {
        IGenericNumeric Call(IGenericNumeric a, IGenericNumeric b);
    }
    public interface IGenericArithmeticCall
    {
        IRuntimeTypedReference Add<T1, T2>(T1 a, T2 b);
        IRuntimeTypedReference AddUnchecked<T1, T2>(T1 a, T2 b);
    }

    public interface IAdd { }
    public interface IAddUnchecked { }
    public interface ISubtract { }
    public interface ISubtractUnchecked { }

    public struct Add<T1, T2> : ICallable
    {
        public static Func<T1, T2, IGenericNumeric> Op = (a, b) =>
            new Numeric<dynamic> { Value = (dynamic)a + (dynamic)b };

        public static ICallable Callable => new Add<T1, T2>();

        public IGenericNumeric Call(Numeric<T1> a, Numeric<T2> b)
        {
            return Op(a.Value, b.Value);
        }

        public IGenericNumeric Call(IGenericNumeric a, IGenericNumeric b)
        {

            return Call((Numeric<T1>)a, (Numeric<T2>)b);
        }
    }

    public struct Subtract<T1, T2> : ICallable
    {
        public static Func<T1, T2, IGenericNumeric> Op = (a, b) =>
            new Numeric<dynamic> { Value = (dynamic)a - (dynamic)b };

        public static ICallable Callable => new Subtract<T1, T2>();

        public IGenericNumeric Call(Numeric<T1> a, Numeric<T2> b)
        {
            return Op(a.Value, b.Value);
        }

        public IGenericNumeric Call(IGenericNumeric a, IGenericNumeric b)
        {

            return Call((Numeric<T1>)a, (Numeric<T2>)b);
        }
    }

    //public interface IAddCaller : IGenericArithmetic
    //{
    //    new IGenericArithmetic Call(IGenericArithmetic a, IGenericArithmetic);
    //}
    public interface IAddCaller : IGenericNumeric
    {
        new IGenericNumeric Call(IGenericNumeric a, IGenericNumeric b);
    }
    public interface IGenericNumeric : IGeneric
    {
        IGenericNumeric Call(IGenericNumeric a, IGenericNumeric b);
        ICallable ResolveCall<TOther>(Numeric<TOther> other);
        IGenericNumeric Op<IOpType>(IGenericNumeric other);
        IGenericNumeric Op<TOther, IOpType>(Numeric<TOther> other);
    }
    public struct Op<T1, T2, IOpType>
    {
        public static Func<T1, T2, IGenericNumeric> Call;
    }
    public struct Numeric<T> : IGenericNumeric
    {
        public T Value;

        public Numeric(T value) => this.Value = value;


        public IGenericNumeric Call(IGenericNumeric a, IGenericNumeric b)
        {
            var callable = b.ResolveCall(this);
            return callable.Call(a, b);
        }
        public IGenericNumeric Op<IOpType>(IGenericNumeric other)
        {
            return other.Op<T, IOpType>(this);
        }
        public IGenericNumeric Op<TOther, IOpType>(Numeric<TOther> other)
        {
            return Op<TOther, T, IOpType>.Call(other.Value, this.Value);
        }
        public ICallable ResolveCall<TOther>(Numeric<TOther> other)
        {
            return Add<T, TOther>.Callable;
        }

        IGenericAs IGeneric.As() => new As<T> { Value = Value };


        public static Func<T, Numeric<T>> Create = (value) => new Numeric<T> { Value = value };

        //TODO: code correct operators as defined in IRuntimeNumeric
        public static implicit operator T(Numeric<T> numeric) => numeric.Value;
        public static implicit operator Numeric<T>(T value) => new Numeric<T>(value);


    }

    public class ArithmeticFactory
    {
        public static void TestInterfaceCall()
        {
            Op<int, int, IAdd>.Call = (a, b) => new Numeric<int> { Value = a + b };
            Op<int, int, IAddUnchecked>.Call = (a, b) => new Numeric<int> { Value = unchecked(a + b) };

            var tr1 = 1.AsNumeric();
            var tr2 = 2.ToTypedReference().As().Numeric;
            var callableResult = tr1.Add(tr2);

            1.AsNumeric().Add(2);
            var result = 1.Add(2).Subtract(3);


            var tr1Unchecked = int.MaxValue.ToTypedReference().As().Numeric;
            var tr2Unchecked = int.MaxValue.AsNumeric();

            var uncheckedResult = tr1Unchecked.AddUnchecked(tr2);

            var convertedUnchecked = uncheckedResult.To<ulong>();


            var res1 = OperandGenerator.Add<int, int, int>.Op(1, 2);
            var res2 = OperandGenerator.Add<int, sbyte, ulong>.Op(1, (sbyte)2);
            var res3 = OperandGenerator.Negate<int>.Op(1);
            var res4 = OperandGenerator.Negate<long>.Op((long)res2);

            var res5 = OperandGenerator.Not<long>.Op((long)res4);
        }
    }
}
