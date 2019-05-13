using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Runtime.RuntimeServices.Generics
{
    public interface IGenericValue
    {
        object GetT { get; }
        Type GenericType { get; }

        TResult UnaryCall<TResult, TIn>(Func<TIn, TResult> func);
        TResult NullaryCall<TResult>(Func<TResult> func);

        IGenericActionCaller GenericActionCaller();
        void StaticActionCall();
        void CallAction(Func<Action> binder);
        void CallActionFunc(Func<Action<Func<Delegate>>> binder);
        void CallStatic();
        void Print(IGenericValue other);
        void Print<T>(GenericValue<T> genericValue);
    }
    public interface IGenericValue<T>
    {
        T GetT { get; }

    }
    public struct GenericPrinter<T1, T2>
    {
        public static Action<T1, T2> Print = (a, b) => Console.WriteLine($"{typeof(T1).Name}:{typeof(T2).Name}");
    }
    public struct GenericValue
    {
        public IGenericValue Value;
        public T Get<T>() => GetGeneric<T>().GetT;
        public GenericValue<T> GetGeneric<T>() => (GenericValue<T>)Value;
        public GenericValue(IGenericValue value) => this.Value = value;
        public void CallStatic() => Value.CallStatic();

    }
    public struct GenericValue<T> : IGenericValue, IGenericValue<T>
    {
        T TValue;
        public T GetT => TValue;

        GenericValue NonGeneric => new GenericValue(this);
        object IGenericValue.GetT => GetT;

        public Type GenericType => typeof(T);

        public GenericValue(T value) { this.TValue = value; }
        public static implicit operator T(GenericValue<T> genericValue) => genericValue.TValue;
        public static implicit operator GenericValue<T>(T value) => new GenericValue<T>(value);

        public TResult NullaryCall<TResult>(Func<TResult> func) => func();
        public TResult UnaryCall<TResult, TIn>(Func<TIn, TResult> func) => func((TIn)(object)TValue);

        public IGenericActionCaller GenericActionCaller() => new GenericActionCaller<T>(TValue);
        public void StaticActionAction() => GenericActionCaller().Call();
        public void Print(IGenericValue other) => other.Print<T>(this);
        public void Print<TOther>(GenericValue<TOther> first) => GenericPrinter<TOther, T>.Print(first, this);
        public void StaticActionCall()
        {
            //throw new NotImplementedException();
        }

        public void CallAction(Func<Action> binder)
        {
            var action = binder;
            var target = action.Target;
            var fields = target.GetType().GetFields();

        }

        public void CallActionFunc(Func<Action<Func<Delegate>>> binder)
        {
            var action = binder();
        }

        public void CallStatic()
        {
            GenericActionCaller().CallStaticAction(TValue);
        }
    }
    public interface IGenericActionCaller
    {

        void Call();
        void SetCall<T>(Action<T> action);
        void CallStaticAction<T>(T value);
        void CallReciever(IActionCaller reciever);
    }
    public class GenericActionCaller<T> : IGenericActionCaller
    {
        private GenericValue<T> target;
        public GenericActionCaller() { }
        public GenericActionCaller(GenericValue<T> value) => this.target = value;

        public Action<T> Action = (x) => Console.WriteLine($"Called print {typeof(T).Name}: {x}");
        public Action InstanceAction = () => Console.WriteLine($"Called print {typeof(T).Name}");

        public void Call() => InstanceAction();
        public void Call(T value) => Action(value);
        public void CallStaticAction(T value) => StaticAction(value);

        public void CallStatic() => StaticAction(target);
        public void CallAction(Func<T> resolver) => Action(resolver());

        public void SetCall<T1>(Action<T1> action)
        {
            var t = (Action, action);
            // Action = action;
        }

        public void CallStaticAction<T1>(T1 value) => CallStaticAction((T)(object)value);

        public void CallReciever(IActionCaller reciever) => reciever.Call(target.GetT);


        public static Action<T> StaticAction = (x) => Console.WriteLine($"Called print {typeof(T).Name}: {x}");
    }

    public interface IActionCaller
    {
        void Call<T>(T value);
        void Call(IGenericValue value);
    }

    public class ActionCaller<T> : IActionCaller
    {
        private Action<T> action;
        public ActionCaller(Action<T> action) => this.action = action;

        public void Call<T1>(T1 value)
        {
            T valueAsT = (T)(object)value;
            action(valueAsT);
        }

        public void Call(IGenericValue value)
        {
            if (value is GenericValue<T> generic)
                action(generic.GetT);
            else
            {
                action(value.To<T>());
            }
        }
    }
    public interface IActionCaller2 { void Call(IGenericValue t1, IGenericValue t2); }
    public class ActionCaller<T1, T2> : IActionCaller2
    {
        private Action<T1, T2> action;
        public ActionCaller(Action<T1, T2> action) => this.action = action;
        public void Call(IGenericValue t1, IGenericValue t2)
        {
            if (t1 is GenericValue<T1> arg1 && t2 is GenericValue<T2> arg2)
                action(arg1, arg2);
        }
    }


    public class ActionCaller
    {
        public static void Call<T1>(Action<T1> action, IGenericValue t1)
        {
            action((T1)t1.GetT);//we can direct cast
            //if (t1 is GenericValue<T1> arg1) action(arg1);
            //else throw new NotImplementedException();
        }
        public static void Call<T1, T2>(Action<T1, T2> action, IGenericValue t1, IGenericValue t2)
        {
            if (t1 is GenericValue<T1> arg1 && t2 is GenericValue<T2> arg2) action(arg1, arg2);
            else throw new NotImplementedException();
        }

        public static void Call<T1, T2, T3>(Action<T1, T2, T3> action, IGenericValue t1, IGenericValue t2, IGenericValue t3)
        {
            if (t1 is GenericValue<T1> arg1 && t2 is GenericValue<T2> arg2 && t2 is GenericValue<T3> arg3) action(arg1, arg2, arg3);
            else throw new NotImplementedException();
        }

        public static void Call<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action,
            IGenericValue t1, IGenericValue t2, IGenericValue t3, IGenericValue t4)
        {
            if (t1 is GenericValue<T1> arg1 && t2 is GenericValue<T2> arg2 && t2 is GenericValue<T3> arg3 && t4 is GenericValue<T4> arg4)
                action(arg1, arg2, arg3, arg4);
            else throw new NotImplementedException();
        }

        public static void Call<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action,
            IGenericValue t1, IGenericValue t2, IGenericValue t3, IGenericValue t4,
            IGenericValue t5)
        {
            if (t1 is GenericValue<T1> arg1 && t2 is GenericValue<T2> arg2 && t2 is GenericValue<T3> arg3 && t4 is GenericValue<T4> arg4 &&
                t5 is GenericValue<T5> arg5)
                action(arg1, arg2, arg3, arg4, arg5);
            else throw new NotImplementedException();
        }

        public static void Call<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action,
           IGenericValue t1, IGenericValue t2, IGenericValue t3, IGenericValue t4,
           IGenericValue t5, IGeneric t6)
        {
            if (t1 is GenericValue<T1> arg1 && t2 is GenericValue<T2> arg2 && t2 is GenericValue<T3> arg3 && t4 is GenericValue<T4> arg4 &&
                t5 is GenericValue<T5> arg5 && t6 is GenericValue<T6> arg6)
                action(arg1, arg2, arg3, arg4, arg5, arg6);
            else throw new NotImplementedException();
        }

        public static void Call<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action,
          IGenericValue t1, IGenericValue t2, IGenericValue t3, IGenericValue t4,
          IGenericValue t5, IGeneric t6, IGeneric t7)
        {
            if (t1 is GenericValue<T1> arg1 && t2 is GenericValue<T2> arg2 && t2 is GenericValue<T3> arg3 && t4 is GenericValue<T4> arg4 &&
                t5 is GenericValue<T5> arg5 && t6 is GenericValue<T6> arg6 && t6 is GenericValue<T7> arg7)
                action(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            else throw new NotImplementedException();
        }

        public static void Call<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action,
            IGenericValue t1, IGenericValue t2, IGenericValue t3, IGenericValue t4,
            IGenericValue t5, IGeneric t6, IGeneric t7, IGenericValue t8)
        {
            if (t1 is GenericValue<T1> arg1 && t2 is GenericValue<T2> arg2 && t2 is GenericValue<T3> arg3 && t4 is GenericValue<T4> arg4 &&
                t5 is GenericValue<T5> arg5 && t6 is GenericValue<T6> arg6 && t6 is GenericValue<T7> arg7 && t6 is GenericValue<T8> arg8)
                action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            else throw new NotImplementedException();
        }


        public static void CallArray<TActionType>(TActionType action, params IGenericValue[] args)
        {
            var genericsArgs = action.GetType().GetGenericArguments();
            if (genericsArgs.Length != args.Length) throw new NotImplementedException();
            var typed = new object[genericsArgs.Length];
            for (var i = 0; i < genericsArgs.Length; i++)
            {
                if (args[i].GenericType == genericsArgs[i])
                { typed[i] = args[i].GetT; }
                else throw new NotImplementedException();
            }
            //Action<int> something = (z) => Console.WriteLine(z);
            ((Delegate)(object)action).DynamicInvoke(typed);

        }

    }


    public class GenericValueFactory
    {

        public Action<T> printAction<T>(T value) => (x) => Console.WriteLine($"Called print {typeof(T).Name}: {x}");


        public static void TestGenericAction()
        {


            void print<T>(T value) => Console.WriteLine($"Called print {typeof(T).Name}: {value}");
            Action<int> printIntAction = (a) => print(a);
            Action<int, int> printTwoIntAction = (a, b) => { print(a); print(b); };
            Action<int, sbyte> printIntSbyteAction = (a, b) => { print(a); print(b); };
            IGenericValue generic = (GenericValue<int>)1;
            IGenericValue generic2 = (GenericValue<sbyte>)2;

            var g = new GenericValue(generic);
            Func<GenericValue, T> GetTFunc<T>(GenericValue value) => (v) => v.Get<T>();
            T GetT<T>(GenericValue value) => value.Get<T>();
            var gValue = GetT<int>(g);
            Action<T> printGetT<T>(GenericValue v) => (x) => print<T>(GetT<T>(v));

            var tAction = printGetT<int>(g);
            tAction(10);
            int fromGeneric = ((object)generic.GetT as int?) ?? default(int);

            generic.Print(generic2);


            ActionCaller.Call(printIntAction, generic);
            ActionCaller.Call(printTwoIntAction, generic, generic2);
            ActionCaller.CallArray(printIntSbyteAction, generic, generic2);
            ActionCaller.CallArray(printTwoIntAction, generic, generic2);


            IActionCaller reciever = new ActionCaller<int>(print);
            reciever.Call(generic);


            var instance = new GenericValueFactory();
            var caller2 = new ActionCaller<int>((x) => instance.printAction(x));
            caller2.Call(generic);



            void bindaction<T>() => GenericActionCaller<T>.StaticAction = print;


            void bindfactory<T>(GenericActionCaller<T> actionCaller) => GenericActionCaller<T>.StaticAction = print;



            var factory = new GenericActionCaller<int>();

            bindfactory(factory);




            var caller = generic.GenericActionCaller();
            caller.CallReciever(reciever);

            caller.Call();

            generic.CallStatic();


            void genericBind(IGenericValue v)
            {
                void callBack()
                {
                    Action callBackTarget<T>()
                    {
                        return () => bindaction<T>();
                    }
                }

                generic.CallAction(() => callBack);

            }
            genericBind(generic);

        }
        public static void TestGenericFunc()
        {

            IGenericValue GetGeneric<T>(T value) => (GenericValue<T>)value;

            IGenericValue Negate<T>(T value)
            {
                var tName = typeof(T).Name;
                IGenericValue result = null;
                switch (tName)
                {
                    case "Int32":
                        result = GetGeneric(-((int)(object)value));
                        break;
                }
                return result;
            }
            Func<IGenericValue> funcNegate<TIn>(TIn t) => () => Negate(t);
            Func<IGenericValue> funcNegateCallback<TIn>(Func<TIn> t) => () => Negate(t());

            Func<IGenericValue> funcNegateGenericCallback(Func<IGenericValue> t) => () => Negate(t());


            IGenericValue NegateGeneric(IGenericValue value)
            {
                IGenericValue result = null;
                result = value.UnaryCall<IGenericValue, int>((x) => Negate(x));
                var r1 = value.NullaryCall(() => default(int));

                var r2 = value.NullaryCall(() => funcNegateCallback(() => default(int)));

                Func<int> intcallBack = () => default(int);
                var r3 = value.NullaryCall(() => funcNegateCallback(intcallBack));


                Func<object> genericCallBack = () => value.GetT;
                var r4 = value.NullaryCall(() => funcNegateCallback(genericCallBack));


                Func<IGenericValue> genericSelfCallBack = () => value;
                var r5 = value.NullaryCall(() => funcNegateGenericCallback(genericSelfCallBack));


                return result;
            }



            GenericValue<int> i1 = 1;
            GenericValue<sbyte> i2 = 2;

            var negated = NegateGeneric(i2);

        }
    }
}
