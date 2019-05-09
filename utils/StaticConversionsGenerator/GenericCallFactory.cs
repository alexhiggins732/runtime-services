using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticConversionsGenerator
{
    public class GenericCallFactory
    {
        //the Func, probably needs 1) FuncDef, Func Resolver, Func Result;
        //   does each one need a generic overload for the method def or will the resolve be responsible
        //  for implicit determination of the require generic args?
        //      probably need generics for each, other wise the func itself and the result will have to be 
        //      define as objects at compile time.


        public class ResolverFactoryTest
        {
            public static void Test()
            {


                CtorHelper<bool>.SetResolver(() => default(bool));
                CtorHelper<Nullable<bool>, bool>.SetResolver((a) => (bool?)a);
                CtorHelper<int, int, int>.SetResolver((a, b) => a + b);
                // note. The func can be set directly
                CtorHelper<int, int, int>.fn = (a, b) => a + b;


                CtorHelper<SomeClassOf<int, int, bool>, int, int, bool>.SetResolver(
                    (a, b, c) => new SomeClassOf<int, int, bool>(a, b, c)
                    );
                CtorHelper<SomeClassOf<int, int, int>, int, int, int>.fn = (a, b, c) => new SomeClassOf<int, int, int>(a, b, c);

                var ctorAResult = CtorHelper<bool>.fn();
                var ctorBResult = CtorHelper<bool?, bool>.fn(true);
                var ctorCResult = CtorHelper<int, int, int>.fn(1, 2);
                var ctorDResult = CtorHelper<SomeClassOf<int, int, bool>, int, int, bool>.fn(1, 2, true);
                var defaultFunc = new GenericDefault<int>();
                var fn = (IFuncObject)defaultFunc;

                //runtime result if IFuncResult<T>, but compile result if FuncResult; 
                var resolved = FuncResolver.Call(fn);
                var boxedResult = resolved.BoxedResult;

                // var resolvedT = resolved.BoxedResult; <-- missing as expected;
                var castedResult = (IFuncResult<int>)resolved;

                //but we can cast if after.
                var fnt = (IFunc<int>)defaultFunc;
                var resolved2 = FuncResolver.Call(fnt);
                var boxedResult2 = resolved2.BoxedResult;

                //runtime result IFuncResult<T>,
                var tResult = resolved2.Result;

                Func<int, bool> fnBool = (i) => FnBool(i);

                //need to make this generic
                Func<bool, bool, int, SomeClassOf<bool, bool, int>> ctor = (a, b, c)
                    => new SomeClassOf<bool, bool, int>(a, b, c);



                ctor(true, true, 1);
                FnResolver<bool, bool, int, SomeClassOf<bool, bool, int>>
                    .SetFn(
                        (a, b, c) => new SomeClassOf<bool, bool, int>(a, b, c)
                    );

            }

         

            public class CtorHelper<TResult>
            {
                public static Func<TResult> fn = null;
                public static void SetResolver(Func<TResult> ctor) => fn = ctor;
            }
            public class CtorHelper<TResult, T1>
            {
                public static Func<T1, TResult> fn = null;
                public static void SetResolver(Func<T1, TResult> ctor) => fn = ctor;
            }

            public class CtorHelper<TResult, T1, T2>
            {
                public static Func<T1, T2, TResult> fn = null;
                //this will set fn to a func to set the result
                //  public static Func<T1, T2, Func<T1, T2, TResult>> fnSetResolver = null;
                //  public static void SetResolver(Func<T1, T2, TResult> ctor) => fnSetResolver = ((c, d) => (a, b) => ctor(a, b));
                public static void SetResolver(Func<T1, T2, TResult> ctor) => fn = ctor;
            }
            public class CtorHelper<TResult, T1, T2, T3>
            {
                // use this to return func to set the resolver
                //public static Func<T1, T2, T3, Func<T1, T2, T3, TResult>> fnSetResolver = null;
                //public static void SetResolver(Func<T1, T2, T3, TResult> ctor) => fn = ((d, e, f) => (a, b, c) => ctor(a, b, c));
                public static Func<T1, T2, T3, TResult> fn;
                public static void SetResolver(Func<T1, T2, T3, TResult> ctor) => fn = ctor;
            }

            public class CtorHelperAutoInitSomeClassOf<TResult, T1, T2, T3>
            {
                static CtorHelperAutoInitSomeClassOf() { fn = (a, b, c) => (TResult)(object)(new SomeClassOf<T1, T2, T3>(a, b, c)); }
                public static Func<T1, T2, T3, TResult> fn;
                public static void SetResolver(Func<T1, T2, T3, TResult> ctor) => fn = ctor;

            }

            public class CtorHelperAutoInitInlineSomeClassOf<TResult, T1, T2, T3>
            {
                public static Func<T1, T2, T3, TResult> fn=
                    (a, b, c) => (TResult)(object)(new SomeClassOf<T1, T2, T3>(a, b, c));
            }

            //invalid to declare a generic class using a generic definition, EG:
            //public class CtorHelperAutoInitInlineSomeClassOfTyped<SomeClassOf<T1, T2, T3>, T1, T2, T3>
            //{
            //    public static Func<T1, T2, T3, TResult> fn =
            //        (a, b, c) => (TResult)(object)(new SomeClassOf<T1, T2, T3>(a, b, c));
            //}



            public static void SetFnResolver<T1, T2, T3>()
            {
                FnResolver<T1, T2, T3, SomeClassOf<T1, T2, T3>>
                    .SetFn(
                        (a, b, c) => new SomeClassOf<T1, T2, T3>(a, b, c)
                    );
            }
            public class FnResolver<T1, T2, T3, TResult>
            {
                static FnResolver()
                {
                    //SetFnResolver<T1, T2, T3>();

                }
                public static Func<T1, T2, T3, Func<T1, T2, T3, TResult>, TResult> fn = null;
                public static void SetFn(Func<T1, T2, T3, TResult> func)
                {

                }
                public static void SetFnResolver()
                {
                    FnResolver<T1, T2, T3, SomeClassOf<T1, T2, T3>>
                        .SetFn(
                            (a, b, c) => new SomeClassOf<T1, T2, T3>(a, b, c)
                        );
                }
            }
            //needs to be in generic type to use this syntax
            //public static Func<T1, T2, T3, Func<T1, T2, T3, TResult>, TResult> fn = null;

            public static bool FnBool(int n) => true;
            public static Func<T1, T2, T3, TResult> resolveFn<T1, T2, T3, TResult>(
                Func<T1> a,
                Func<T2> b,
                Func<T3> c,
                Func<T1, T2, T3, TResult> target
                )
            {
                //var d = a();
                //var e = b();
                //var f = c();
                return (d, e, f) => target(a(), b(), c());
                //return () => target(a(), b(), c());
            }
            public static SomeClassOf<T1, T2, T3> SomeClassCtor<T1, T2, T3>(T1 a, T2 b, T3 c) => new SomeClassOf<T1, T2, T3>(a, b, c);
            public class SomeClassOf<T1, T2, T3>
            {
                public T1 Value1;
                public T2 Value2;
                public T3 Value3;
                public SomeClassOf(T1 arg, T2 arg2, T3 arg3)
                {
                    Value1 = arg; Value2 = arg2; Value3 = arg3;
                }

            }
        }

        #region interfaces
        public interface IFunc { }
        public interface IFuncResult { object BoxedResult { get; } }
        public interface IFuncObject : IFunc
        {
            IFuncResult Invoke();
        }
        public interface IFunc<T> : IFunc { IFuncResult<T> Invoke(); }
        public interface IFuncResult<T> : IFuncResult { T Result { get; } }

        public interface IFunc<T1, T2> : IFunc { IFuncResult<T2> Invoke(); }
        public interface IFunc<T1, T2, T3> : IFunc { IFuncResult<T3> Invoke(); }

        public interface IFuncResolver { } // The factory responsible for converting IFunc to Func<Args> 

        #endregion

        public struct FuncResolver : IFuncResolver
        {
            public static IFuncResult Call(IFuncObject func) => (IFuncResult)func.Invoke();
            public static IFuncResult<T> Call<T>(IFunc<T> func) => (IFuncResult<T>)func.Invoke();
            public static IFuncResult<T> Call<T>(IFunc<T, T> func) => (IFuncResult<T>)func.Invoke();
            public static IFuncResult<T2> Call<T1, T2>(IFunc<T1, T2> func) => (IFuncResult<T2>)func.Invoke();
            public static IFuncResult<T2> Call<T1, T2>(IFunc<T1, T1, T2> func) => (IFuncResult<T2>)func.Invoke();
            public static IFuncResult<T3> Call<T1, T2, T3>(IFunc<T1, T1, T2> func) => (IFuncResult<T3>)func.Invoke();
        }
        public struct GenericDefault<T> : IFunc<T>, IFuncObject
        {
            public IFuncResult<T> Invoke() => new FuncResult<T>(default(T));

            IFuncResult IFuncObject.Invoke() => Invoke();
        }

        public struct FuncResult<T> : IFuncResult<T>
        {
            public T Result { get; set; }

            public FuncResult(T value) => this.Result = value;

            public object BoxedResult => Result;
        }

        public struct FuncResult : GenericCallFactory.IFuncResult
        {
            public object BoxedResult { get; private set; }
            public FuncResult(object t) => BoxedResult = t;

        }
    }


}
