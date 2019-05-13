using System;
using System.Text;
using System.Threading.Tasks;

namespace System.Runtime.RuntimeServices
{
    public class GenericArithmeticTest
    {
        interface IFuncStub
        {
            void CreateGeneric<TOut>(Func<TOut> other);
            void CreateGenericCaller<TOut, TTargetFunc>(Func<TOut> other, TTargetFunc target);
            IFuncResult Invoke();
        }
        private class FnStub<T> : IFuncStub
        {
            public Func<T> fn;
            public IFuncResult Invoke() => new FuncResult<T>(fn());
            public FnStub(Func<T> fn) => this.fn = fn;
            /// <summary>
            /// ambiguous
            /// </summary>
            /// <typeparam name="TOut"></typeparam>
            /// <param name="other"></param>
            public void CreateGeneric<TOut>(Func<TOut> other)
            {
                var at = other();
                var t = fn();
                var s = funcStub<TOut>(other);
                var result = s(fn, other);

            }

            public void CreateGenericCaller<TOut, TTargetFunc>(Func<TOut> other, TTargetFunc Target)
            {


            }


            Func<Func<T>, Func<TOut>, Func<Func<T>, Func<TOut>>> funcStub<TOut>(Func<TOut> other)
            {
                return null;
            }
        }
        private class FuncStub<T1, T2>
        {
            private Func<T1, T2> funcTarget;
        }
        private T GetT<T>() => default;


        private class FuncPtr
        {
            public IFuncStub fncPtr;



            public IFuncResult Invoke() => fncPtr.Invoke();
            public void CreateGeneric<TOut>(Func<TOut> other)
            {
                fncPtr.CreateGeneric<TOut>(other);
            }

            internal void callGeneric<T1, T2, TResult>(Func<T1> fn1, Func<T2> Fn2, Func<Func<T1, T2, TResult>> func)
            {
                throw new NotImplementedException();
            }

            internal void callGenericFunc(FuncPtr ptr, FuncPtr ptr2, Func<FuncPtr, FuncPtr, Func<IFuncDelegate>> targetResolver)
            {
                throw new NotImplementedException();
            }
        }

        public interface IFuncDelegate { }
        private class DelegatePtr<T1, T2, TResult> : IFuncDelegate
        {
            private Func<T1, T2, TResult> funcDelegate;

            public DelegatePtr(Func<T1, T2, TResult> funcDelegate) => this.funcDelegate = funcDelegate;
        }
        private void callDelegate(object add) { }
        private void test()
        {

            T GetDefault<T>() => default;
            Func<T1, T2, TResult> add<T1, T2, TResult>() => GenericAdder<T1, T2, TResult>.FnAdd;


            TResult callDelegate<T1, T2, TResult>(T1 t1, T2 t2) => GenericAdder<T1, T2, TResult>.FnAdd(t1, t2);


            void CreateGeneric<T>(FuncPtr fptr, Func<T> fn) => fptr.CreateGeneric<T>(fn);
            FuncPtr AsFuncPtr<T>(T value)
            {
                var stub = new FnStub<T>(() => value);

                FuncPtr ptrResult = new FuncPtr();
                ptrResult.fncPtr = new FnStub<T>(stub.fn);
                return ptrResult;
            };
            DelegatePtr<T1, T2, TResult> DelegatePtr<T1, T2, TResult>()
            {
                return new DelegatePtr<T1, T2, TResult>(GenericAdder<T1, T2, TResult>.FnAdd);
            }

            IFuncDelegate funcDelegate<T1, T2>(T1 t1, T2 t2)
            {
                return DelegatePtr<T1, T2, int>();
            }

            IFuncDelegate resolverFuncDelegate<T1, T2>(T1 t1, T2 t2)
            {
                IFuncDelegate delegateResult = null;
                //return DelegatePtr<T1, T2, int>();
                return delegateResult;
            }

            Func<IFuncDelegate> resolveDelegateT1<T1>(T1 t1, FuncPtr pt2)
            {
                return () => resolverFuncDelegate(t1, pt2);
            }
            Func<IFuncDelegate> resolveDelegate(FuncPtr pt1, FuncPtr pt2)
            {
                //TODO: need to resolve pt1.T1 and pt2.T1
                //get pt1 to call resolveDelegateT1
                return () => resolverFuncDelegate(pt1, pt2);
            }
           
            FuncPtr ptr = AsFuncPtr(1);
            FuncPtr ptr2 = AsFuncPtr(2ul);

            var result = GenericAdder<int, byte, int>.FnAdd(1, 2);

            ptr.callGeneric(GetDefault<byte>, GetDefault<int>, add<byte, int, int>);
            ptr.callGenericFunc(ptr, ptr2, resolveDelegate);
            //ptr.CallGeneric<Func<T2>, Func<T1, T2, TResult>>(() => GetDefault<byte>, callDelegate);




            IFuncResult funcResult = ptr.fncPtr.Invoke();


            //ptr.CreateGeneric<T>(() => getDefault);


        }
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

    public struct GenericArithmetic
    {
        static GenericArithmetic()
        {
            GenericArithmeticFactory.RegisterOperators();
        }

    }

    public interface IGenericAdder
    {
        IRuntimeTypedReference Add(IRuntimeTypedReference a, IRuntimeTypedReference b);
    }


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

    //T1, T2 aren't going to know TResult.
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
            return default;
        }

        //not sure if this is needed. probably is because T1,T2 don't know TResult.
        public static RuntimeTypedReference<TResult>
            AddRef(RuntimeTypedReference<T1> a, RuntimeTypedReference<T2> b)
        {
            //return default(RuntimeReference<TResult>);
            if (FnAdd != null)
                return new RuntimeTypedReference<TResult> { Value = FnAdd(a.Value, b.Value) };
            return default;// (RuntimeTypedReference<TResult>);
        }
    }

    //[Obsolete] or refactor. Perhap add an opcode parameter here unless there will be a map from opcode to FN.
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

    //[Obsolete]
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
