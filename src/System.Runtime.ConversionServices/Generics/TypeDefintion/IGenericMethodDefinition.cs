using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Runtime.ConversionServices.Generics.TypeDefintion
{
    public class GenericMethodTest
    {
        public class Fn<T> : IFn
        {
            public Func<T> fn;
            public Fn(Func<T> fn) => this.fn = fn;
        }
        public class Fn0<T> : IFn0
        {
            public IFn F0 { get; }
            public Fn0(Func<T> fn) => this.F0 = new Fn<T>(fn);
        }
        public class Fn1<T0, T1> : Fn0<T0>, IFn1
        {
            public IFn F1 { get; }
            public Fn1(Func<T0> f0, Func<T1> f1) : base(f0) { this.F1 = new Fn<T1>(f1); }
        }
        public class Fn2<T0, T1, T2> : Fn1<T0, T1>, IFn2
        {
            public IFn F2 { get; }
            public Fn2(Func<T0> f0, Func<T1> f1, Func<T2> f2) : base(f0, f1) { this.F2 = new Fn<T2>(f2); }
        }


        public class FnArray : IFnArray
        {
            public List<IFn> Fns { get; }
            public FnArray(IFn[] fns) => Fns = fns.ToList();

        }

        public class ActCaller : IActCaller
        {
            private Func<object, object, object, Action<Func<object>, Func<object>, Func<object>>> actionResolverWithImp;

            public ActCaller(Func<object, object, object, Action<Func<object>, Func<object>, Func<object>>> actionResolverWithImp)
            {
                this.actionResolverWithImp = actionResolverWithImp;
            }
            public void Call(IMethodArgs args)
            {

            }
        }

        public class ActCallerGeneric<T1, T2, T3> : IActCaller
        {
            private Func<T1, T2, T3, Action<Func<T1>, Func<T2>, Func<T3>>> actionResolverWithImp;

            public ActCallerGeneric(Func<T1, T2, T3, Action<Func<T1>, Func<T2>, Func<T3>>> actionResolverWithImp)
            {
                this.actionResolverWithImp = actionResolverWithImp;
            }
            public void Call(IMethodArgs args)
            {
                if (args is IFnArray fnArray)
                {
                    var fns = fnArray.Fns;
                    InvokeCallBackWithFuncArray((Fn<T1>)(IFn0)fns[0], (Fn<T2>)(IFn0)fns[1], (Fn<T3>)(IFn0)fns[2]);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            private void InvokeCallBackWithFuncArray(Fn<T1> fn0, Fn<T2> fn1, Fn<T3> fn2)
            {
                var t0 = fn0.fn();
                var t1 = fn1.fn();
                var t2 = fn2.fn();
                var callback = actionResolverWithImp(fn0.fn(), fn1.fn(), fn2.fn());
                callback(fn0.fn, fn1.fn, fn2.fn);


            }
        }


        public static void Run() => Run();
        public void run()
        {
            Func<int> fna = () => 1;
            Func<string> fnb = () => "2";
            Func<char> fnc = () => '3';

            Action<int, string, char> act = (a, b, c) => Console.WriteLine($"{a}, {b}, {c}");

            //can't inline generics
            //Action<T1, T2, T3> actgeneric<T1, T2, T3> = (a, b, c) => Console.WriteLine($"{a}, {b}, {c}");
            //Action<T1, T2, T3> actgeneric = (a, b, c) => Console.WriteLine($"{a}, {b}, {c}");
            //Action<T1, T2, T3> actgeneric<T1, T2, T3>;

            Action<Func<int>, Func<string>, Func<char>> actresolver = (a, b, c) => act(a(), b(), c());
            actresolver(fna, fnb, fnc);

            var ifna = GetIfn(fna);
            var ifnb = GetIfn(fnb);
            var ifnc = GetIfn(fnc);

            //overrides, not scalable, not inline compilable.
            //var ifn2 = GetIfn(ifna, ifnb, ifnc);
            //array of funcs easier to deal with
            var fnArray = GetIfnArray(ifna, ifnb, ifnc);
            //var actionTarget = ActionResolverWithImp;
            var actCaller = new ActCaller(ActionResolverWithImp);
            CallIAct(actCaller, fnArray);

        }

        public IFn0 GetIfn<T0>(Func<T0> fn0) => new Fn0<T0>(fn0);
        public IFn1 GetIfn<T0, T1>(Func<T0> fn0, Func<T1> fn1) => new Fn1<T0, T1>(fn0, fn1);
        public IFn2 GetIfn<T0, T1, T2>(Func<T0> fn0, Func<T1> fn1, Func<T2> fn2) => new Fn2<T0, T1, T2>(fn0, fn1, fn2);

        public IFnArray GetIfnArray(params IFn[] fns) => new FnArray(fns);

        public Action<T1, T2, T3> Act<T1, T2, T3>(T1 t1, T2 t2, T3 t3)
            => (a, b, c) => { };

        public Action<Func<T1>, Func<T2>, Func<T3>> ActionResolver<T1, T2, T3>(T1 t1, T2 t2, T3 t3)
           => (a, b, c) => { Console.WriteLine($"{a} {b}, {c}"); };

        public Action<Func<T1>, Func<T2>, Func<T3>> ActionResolverWithImp<T1, T2, T3>(T1 t1, T2 t2, T3 t3)
         => (a, b, c) => Act(a(), b(), c());

        public void CallIAct(IMethodCaller methodCaller, IMethodArgs methodArgs)
        {

        }

    }
    interface IGenericMethodDefinition
    {
    }
    public interface IMethod { }
    public interface IMethodArgs { }//store the actual generics/funcs in the interface definition?
    public interface IFnArray : IMethodArgs { List<IFn> Fns { get; } }
    public interface IFn : IMethod { }
    public interface IFn0 : IFn { IFn F0 { get; } }
    public interface IFn1 : IFn0 { IFn F1 { get; } }
    public interface IFn2 : IFn1 { IFn F2 { get; } }
    public interface IAct : IMethod { }
    public interface IAct0 : IMethod { }
    public interface IAct1 : IMethod { }
    public interface IAct2 : IMethod { }
    public interface IMethodCaller { } //store the actual generics/funcs in the interface definition?
    public interface IActCaller : IMethodCaller { }
}
