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
                var defaultFunc = new GenericDefault<int>();
                var fn = (IFuncObject)defaultFunc;
                //runtime result if IFuncResult<T>, but compile reuslt if FuncResult; 
                var resolved = FuncResolver.Call(fn);
                var boxedResult = resolved.BoxedResult;
                // var resolvedT = resolved.BoxedResult; <-- missing as expected;
                var castedResult = (IFuncResult<int>)resolved;
                //but we can cast if after.

                var fnt = (IFunc<int>)defaultFunc;
                var resolved2 = FuncResolver.Call(fnt);
                var boxedResult2 = resolved2.BoxedResult;
                //runtime result if IFuncResult<T>, but compile reuslt if FuncResult; 
                var tResult = resolved2.Result;


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
