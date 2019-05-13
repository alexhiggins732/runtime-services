using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Runtime.RuntimeServices
{

    public interface IGeneric
    {
        IGenericAs As();
    }
    public interface IGenericAs
    {
        IGenericNumeric Numeric { get; }
        IRuntimeTypedReference IRuntimeTypedReference { get; }
        IGenericRuntimeTypeDefinition IGenericRuntimeTypeDefinition { get; }
        TOut To<TOut>();
        IRuntimeTypedReference GetField(IInstanceFieldDefinition instanceFieldDefinition);
        IRuntimeTypedReference GetField<TOut>(IInstanceFieldDefinition instanceFieldDefinition);
        IFunc FnDefault { get; }
    
    }
    public struct As<T> : IGenericAs
    {
        public T Value;
        public IGenericNumeric Numeric => new Numeric<T> { Value = Value };

        public RuntimeTypedReference<T> RuntimeTypedReference => new RuntimeTypedReference<T> { Value = Value };
        public IRuntimeTypedReference IRuntimeTypedReference => RuntimeTypedReference;

        public GenericRuntimeTypeDefinition<T> GenericRuntimeTypeDefinition => new GenericRuntimeTypeDefinition<T>();
        public IGenericRuntimeTypeDefinition IGenericRuntimeTypeDefinition => GenericRuntimeTypeDefinition;

        public IRuntimeTypedReference GetField(IInstanceFieldDefinition instanceFieldDefinition)
           => instanceFieldDefinition.FieldInfo.GetValue(Value).ToTypedReference(instanceFieldDefinition.FieldInfo.FieldType);
        public IRuntimeTypedReference GetField<TOut>(IInstanceFieldDefinition instanceFieldDefinition)
        => ((TOut)instanceFieldDefinition.FieldInfo.GetValue(Value)).ToTypedReference();

        public IFunc FnDefault => new Fn<T>(Generic<T>.Default);

        public TOut To<TOut>() => Value.To<TOut>();

      
    }
    public interface IGenericFactory
    {
        IGenericFactory GenericFactory { get; }
    }
    public interface IGenericUnaryFactory : IGenericFactory
    {
        IGenericNullaryCall GenericDefault { get; }

        IGenericCallResult Call(IGenericNullaryCall unaryCall);
    }
    public interface IGenericCallResult { }
    public interface IGenericFactory<T1> : IGenericUnaryFactory { }

    public struct GenericFactory<T1> : IGenericFactory<T1>
    {
        public static GenericFactory<T1> Instance = new GenericFactory<T1>();
        IGenericFactory IGenericFactory.GenericFactory => Instance;

        public IGenericNullaryCall GenericDefault => new GenericNullaryCall<T1> { Func = () => default };
        public T1 DefaultT => default;

        public IGenericCallResult Call(IGenericNullaryCall unaryCall)
        {
            throw new NotImplementedException();
        }
    }

    public interface IGenericType { }
    public interface IGenericUnary : IGenericType
    {
        IGenericFactory GenericFactory { get; }
    }
    public interface IGenericType<T1> : IGenericUnary { }
    public interface IGenenicUnaryArgs { }
    public interface IGenericBinary : IGenericType { }
    public interface IGenericType<T1, T2> : IGenericBinary { }
    public interface IGenericTernary : IGenericType { }
    public interface IGenericType<T1, T2, T3> : IGenericTernary { }

    public interface IGenericCall { }


    public interface IGenericNullaryCall : IGenericCall
    {

    }
    public interface IGenericCall<T> : IGenericNullaryCall
    {
        Func<T> Func { get; set; }
        T Invoke();
    }
    public struct GenericNullaryCall<T> : IGenericCall<T>
    {
        public Func<T> Func { get; set; }
        public T Invoke() => Func();
    }


    public interface IGenericUnaryCall : IGenericCall { }
    public interface IGenericCall<T, TResult> : IGenericUnaryCall { }
    public struct GenericUnaryCall<T, TResult> : IGenericCall<T, TResult>
    {
        public Func<T, TResult> Call;
    }
    public struct GenericUnaryCall<T> : IGenericUnaryCall
    {
        public Func<T, T> Call;
    }
    public class GenericBinder
    {

        public struct GenericType : IGenericType
        {
            public static void Call(IGenericUnary unary)
            {

            }
        }
        public struct GenericType<T1> : IGenericType<T1>
        {
            public IGenericFactory GenericFactory => GenericFactory<T1>.Instance;
        }
        public struct GenericType<T1, T2> : IGenericType<T1, T2>
        {
            public void Call(IGenericUnary unary)
            {
                Console.WriteLine($"Resolved {unary}");
                Call((IGenericType<T1>)unary);
            }
            public void Call(IGenericType<T1> generic1)
            {
                Console.WriteLine($"Resolved {typeof(T1)}");
            }
            public void Call(IGenericBinary binary)
            {
                Console.WriteLine($"Resolved {binary}");
                Call((IGenericType<T1, T2>)binary);
            }
            public void Call(IGenericType<T1, T2> generic1)
            {
                Console.WriteLine($"Resolved {typeof(T1)}");
            }
        }

        public class GenericBinderTest
        {
            public static void Run()
            {
                var t = new GenericType<int>();
                var unary = (IGenericUnary)t;
                var factory = (IGenericUnaryFactory)unary;

                var unaryCall = ((IGenericUnaryFactory)unary.GenericFactory).GenericDefault;
                var result = factory.Call(unaryCall);


                var t2 = new GenericType<int, int>();
                t2.Call(t);
                t2.Call(t2);
            }
            public static void CallTest<T>(GenericUnaryCall<T> instance)
            {
            }

        }
    }
    /// <summary>
    /// Make call pattern in <see cref="ArithmeticFactory"/> generic for binary and unary expression calls
    /// </summary>
    public static partial class GenericCall
    {
        public interface ICallableExpression { }
        public interface ICallFunc { }
        public interface ICallableUnaryExpression
        {
            IRuntimeTypedReference Call(IRuntimeTypedReference a);
        }
        public interface ICallableBinaryExpression { }

        //ITypedReference -> I
    }
}
