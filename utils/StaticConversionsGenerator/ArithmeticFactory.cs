using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Runtime.ConversionServices
{
    public interface ICallable
    {
        IGenericArithmetic Call(IGenericArithmetic a, IGenericArithmetic b);
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
        public static Func<T1, T2, IGenericArithmetic> Op = (a, b) =>
            new Arithmetic<dynamic> { Value = (dynamic)a + (dynamic)b };

        public static ICallable Callable => new Add<T1, T2>();

        public IGenericArithmetic Call(Arithmetic<T1> a, Arithmetic<T2> b)
        {
            return Op(a.Value, b.Value);
        }

        public IGenericArithmetic Call(IGenericArithmetic a, IGenericArithmetic b)
        {

            return Call((Arithmetic<T1>)a, (Arithmetic<T2>)b);
        }
    }

    public struct Subtract<T1, T2> : ICallable
    {
        public static Func<T1, T2, IGenericArithmetic> Op = (a, b) =>
            new Arithmetic<dynamic> { Value = (dynamic)a - (dynamic)b };

        public static ICallable Callable => new Subtract<T1, T2>();

        public IGenericArithmetic Call(Arithmetic<T1> a, Arithmetic<T2> b)
        {
            return Op(a.Value, b.Value);
        }

        public IGenericArithmetic Call(IGenericArithmetic a, IGenericArithmetic b)
        {

            return Call((Arithmetic<T1>)a, (Arithmetic<T2>)b);
        }
    }

    //public interface IAddCaller : IGenericArithmetic
    //{
    //    new IGenericArithmetic Call(IGenericArithmetic a, IGenericArithmetic);
    //}
    public interface IAddCaller : IGenericArithmetic
    {
        new IGenericArithmetic Call(IGenericArithmetic a, IGenericArithmetic b);
    }
    public interface IGenericArithmetic : IGeneric
    {
        IGenericArithmetic Call(IGenericArithmetic a, IGenericArithmetic b);
        ICallable ResolveCall<TOther>(Arithmetic<TOther> other);
        IGenericArithmetic Op<IOpType>(IGenericArithmetic other);
        IGenericArithmetic Op<TOther, IOpType>(Arithmetic<TOther> other);
    }
    public struct Op<T1, T2, IOpType>
    {
        public static Func<T1, T2, IGenericArithmetic> Call;
    }
    public struct Arithmetic<T> : IGenericArithmetic
    {
        public T Value;

        public Arithmetic(T value) => this.Value = value;


        public IGenericArithmetic Call(IGenericArithmetic a, IGenericArithmetic b)
        {
            var callable = b.ResolveCall(this);
            return callable.Call(a, b);
        }
        public IGenericArithmetic Op<IOpType>(IGenericArithmetic other)
        {
            return other.Op<T, IOpType>(this);
        }
        public IGenericArithmetic Op<TOther, IOpType>(Arithmetic<TOther> other)
        {
            return Op<TOther, T, IOpType>.Call(other.Value, this.Value);
        }
        public ICallable ResolveCall<TOther>(Arithmetic<TOther> other)
        {
            return Add<T, TOther>.Callable;
        }

        public static Func<T, Arithmetic<T>> Create = (value) => new Arithmetic<T> { Value = value };

        public IGenericStruct Generic => new Generic<T> { Value = Value };
    }

    public class ArithmeticFactory
    {
        public static void TestInterfaceCall()
        {
            Op<int, int, IAdd>.Call = (a, b) => new Arithmetic<int> { Value = a + b };
            Op<int, int, IAddUnchecked>.Call = (a, b) => new Arithmetic<int> { Value = unchecked(a + b) };

            var tr1 = 1.ToTypedReference().Arithmetic();
            var tr2 = 2.ToTypedReference().Arithmetic();
            var callableResult = tr1.Add(tr2);

            1.ArithmeticTest().Add(2);
            var result = 1.Add(2).Subtract(3);


            var tr1Unchecked = int.MaxValue.ToTypedReference().Arithmetic();
            var tr2Unchecked = int.MaxValue.ToTypedReference().Arithmetic();

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
