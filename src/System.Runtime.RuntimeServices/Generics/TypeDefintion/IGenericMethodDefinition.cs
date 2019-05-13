using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Runtime.RuntimeServices.Conversions;

namespace System.Runtime.RuntimeServices
{
    public interface IDelegate { }
    public interface IMethod : IDelegate { }
    public interface IFunc : IMethod
    {
        IFuncResult Invoke();
    }

    public interface IFuncResult<T> : IFuncResult { T Result { get; } }


    public interface IFuncResult
    {
        object BoxedResult { get; }
    }



    public interface IFunc<T> : IFunc { new IFuncResult<T> Invoke(); }

    public class Fn<T> : IFunc
    {
        public Func<T> fn;
        public Fn(Func<T> fn) => this.fn = fn;
        FuncResult<T> Invoke() => new FuncResult<T>(fn());
        IFuncResult IFunc.Invoke() => Invoke();
    }

    //public struct FuncResult : IFuncResult
    //{
    //    public object BoxedResult { get; private set; }
    //    public FuncResult(object t) => BoxedResult = t;

    //}

    public struct FuncResult<T> : IFuncResult, IFuncResult<T>
    {
        public T Result { get; set; }

        public FuncResult(T value) => this.Result = value;

        public object BoxedResult => Result;
    }

    public static class Fn
    {
        [Obsolete("Duplicate functionaly ")]
        public static IFunc Create(IRuntimeTypedReference runtimeTypedReference)
            => runtimeTypedReference.As().FnDefault;
    }
    //TODO: Move to namespace System.Runtime.RuntimeServices once refactoring complete to unify public apis
    namespace Generics.TypeDefintion
    {
        public class GenericDelegate
        {
            public static void Test()
            {
                RTGeneric<int> a = 1;
                RTGeneric<int> b = 2;

                IRtGeneric ia = a;
                IRtGeneric ib = b;

                var inta = ((int)a);
                var intb = ((int)b);


                var comp = ia.RtCompare(ib);
                var compba = ib.RtCompare(ia);
            }
            public interface IMsil
            {
                int OpCmp<T>(T other);
            }
            public interface IRtGeneric : IMsil
            {
                int RtCompare(IRtGeneric other);
                //int OpCmp<T>(T other);
                int OpCmp(IRtGeneric other);
                int RtCompareFrom<TSource>(RTGeneric<TSource> rTGeneric);
                int RtCompareFrom2<TSource>(RTGeneric<TSource> rTGeneric);
                int RtCompareInterfaceCall(IRtGeneric generic);
            }

            public interface IRuntimeInterface
            {

            }
            public interface IRuntimeCompare : IRuntimeInterface { }

            public class RTGeneric<T> : IRtGeneric
            {
                public T value;
                public RTGeneric(T value) => this.value = value;

                // Can't we just
                public int OpCmp(IRtGeneric other) => other.OpCmp(this.value);

                // We can unbox the generics using the following pattern.
                //  But requires 4 methods for every operation. 
                //      1) Non-Generic Interface method.
                //      2) Handler to accept generic parameters passed from 1.
                //      3) A call to a static function to extract generics from 1 and 2.
                //      4) A call to the static handler wired to handle the method for the generic arguments.
                public int RtCompare(IRtGeneric other)
                    => other.RtCompareFrom(this);
                public int RtCompareFrom<TSource>(RTGeneric<TSource> source)
                    => Compare(source, this); // we could eliminate
                public static int Compare<TSource, T2>(RTGeneric<TSource> tsource, RTGeneric<T2> other) =>
                    GenericComparer<TSource, T2>.Compare(tsource.value, other.value);

                //We can reduce to 3) but would require making the method instance.
                public int RtCompare2(IRtGeneric other) => other.RtCompareFrom2(this);
                public int RtCompareFrom2<TSource>(RTGeneric<TSource> source)
                   => GenericComparer<TSource, T>.Compare(source.value, value);

                // Let's introduce another generic parameter representing the operation to be performed.
                //  Additionally, we will specify T as a second generic parameter and pass IRtGeneric as the third.
                //      We can let the static GenericInterfaceCall invoke the call back with the necessary parameters.
                public int RtCompareInterfaceCall(IRtGeneric generic)
                    => RuntimeInterfaceGenericMap<IRuntimeCompare, T, IRtGeneric>.Call(this.value, generic);
                public int OpCmp<TIn>(TIn tvalue) => GenericComparer<TIn, T>.Compare(tvalue, value);


                //Can we get this into single func? and does it work?
                public int RtCompareWithFunc2(IRtGeneric other)
                    => GenericFuncComparer.Compare(() => value, (x) => other.RtCompareFrom(this));







                // // BROKE:
                //=> GenericFuncComparer.CompareFromOther((x) => other.RtCompareFrom(this));


                public int RtCompareWithFunc3(IRtGeneric other)
                {
                    //other.RtCompareFrom(() => value);

                    //GenericFuncComparer.CompareFromOther(other, (x) => other.RtCompareFrom);
                    return 1;
                }






                public static implicit operator T(RTGeneric<T> generic) => generic.value;
                public static implicit operator RTGeneric<T>(T value) => new RTGeneric<T>(value);
            }

            public class RuntimeInterfaceGenericMap<IRuntimeInterface, T, IRtGeneric>
            {
                //TODO, how to handle generics?
                internal static int Call(T value, GenericDelegate.IRtGeneric generic)
                {
                    return generic.OpCmp<T>(value);
                    //throw new NotImplementedException();
                }
            }
            public class RuntimeInterfaceMap<IRuntimeInterface, T, IRtGeneric>
            {
                //TODO, how to handle generics?
                internal static int Call(T value, GenericDelegate.IRtGeneric generic)
                    => generic.OpCmp<T>(value);
            }
            public class RuntimeInterfaces<TInterfaceType>
            {
                //TODO: TInterfaceType to implementation.
                public static void Call<T1, T2>(T1 t1, T2 t2) => GenericComparer<T1, T2>.Compare(t1, t2);
            }




            public class GenericFuncComparer
            {
                internal static int Compare<T>(Func<T> p, Func<Func<T>, int> del)
                {
                    return del(p);
                }
                internal static int Compare<T>(
                    Func<RTGeneric<T>> source,
                    Func<IRtGeneric> other,
                    Func<IRtGeneric, Func<RTGeneric<T>>, int> del)
                {
                    var withoutCallBack = (other().RtCompare(source()));
                    return (other().RtCompare(source()));
                }

            }




            public class GenericComparer<T1, T2>
            {
                public static int CompareDebug(T1 a, T2 b)
                {
                    var inta = (int)(object)a;
                    var intb = (int)(object)b;
                    return inta.CompareTo(intb);
                }
                public static int Compare(T1 a, T2 b) => ((int)(object)a).CompareTo((int)(object)b);
            }
        }


        public class GenericMethodTest
        {


            public class Fn0<T> : IFn0
            {
                public IFunc F0 { get; }


                public Fn0(Func<T> fn) => this.F0 = new Fn<T>(fn);

                public virtual IFuncResult Invoke() => F0.Invoke();

            }
            public class Fn1<T0, T1> : Fn0<T0>, IFn1
            {
                public IFunc F1 { get; }
                public override IFuncResult Invoke() => F1.Invoke();
                public Fn1(Func<T0> f0, Func<T1> f1) : base(f0) { this.F1 = new Fn<T1>(f1); }
            }
            public class Fn2<T0, T1, T2> : Fn1<T0, T1>, IFn2
            {
                public IFunc F2 { get; }
                public override IFuncResult Invoke() => F2.Invoke();
                public Fn2(Func<T0> f0, Func<T1> f1, Func<T2> f2) : base(f0, f1) { this.F2 = new Fn<T2>(f2); }
            }


            public class FnArray : IFnArray
            {
                public List<IFunc> Fns { get; }
                public FnArray(IFunc[] fns) => Fns = fns.ToList();

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


            //public static void Run() => Run();
            public void Run()
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

            public static IFn0 GetIfn<T0>(Func<T0> fn0) => new Fn0<T0>(fn0);
            public static IFn0 GetIfn<T0>(Func<Func<T0>> fn0) => new Fn0<T0>(fn0());

            public IFn1 GetIfn<T0, T1>(Func<T0> fn0, Func<T1> fn1) => new Fn1<T0, T1>(fn0, fn1);
            public IFn2 GetIfn<T0, T1, T2>(Func<T0> fn0, Func<T1> fn1, Func<T2> fn2) => new Fn2<T0, T1, T2>(fn0, fn1, fn2);

            public IFnArray GetIfnArray(params IFunc[] fns) => new FnArray(fns);

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

        public interface IMethodArgs { }//store the actual generics/funcs in the interface definition?
        public interface IFnArray : IMethodArgs { List<IFunc> Fns { get; } }

        public interface IFn0 : IFunc { IFunc F0 { get; } }
        public interface IFn1 : IFn0 { IFunc F1 { get; } }
        public interface IFn2 : IFn1 { IFunc F2 { get; } }
        public interface IAct : IMethod { }
        public interface IAct0 : IMethod { }
        public interface IAct1 : IMethod { }
        public interface IAct2 : IMethod { }
        public interface IMethodCaller { } //store the actual generics/funcs in the interface definition?
        public interface IActCaller : IMethodCaller { }
    }
}
