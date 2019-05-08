using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace System.Runtime.ConversionServices
{
    public static partial class GenericArithmetic
    {
        public enum Operation
        {
            None = 0,
            Negate,
            NegateUnchecked,
            Convert,
            ConvertUnchecked,
            Add,
            AddUnchecked,
            Not

        }
        private static Func<T1, T2, TResult> CreateAddFunc<T1, T2, TResult>()
            => CreateBinaryFunc<T1, T2, TResult>(Operation.Add);



        public struct RuntimeArithmetic<T1, T2, TResult>
        {
            static Func<T1, T2, TResult> add;
            public static Func<T1, T2, TResult> Add = (add ?? CreateAddFunc<T1, T2, TResult>());

            static Func<T1, T1> not;
            public static Func<T1, T1> Not = (not ?? CreateUnary<T1,T1>(Operation.Not));

            static Func<T1, T1> negate;
            public static Func<T1, T1> Negate = (not ?? CreateUnary<T1, T1>(Operation.Negate));

        }

        //DefaultExpression Default(Type type)
        //DefaultExpression Empty();


        //UnaryExpression ArrayLength(Expression array)
        //UnaryExpression Convert(Expression expression, Type type)
        //UnaryExpression Decrement(Expression expression)
        //UnaryExpression Increment(Expression expression);
        //UnaryExpression IsFalse(Expression expression);
        //UnaryExpression IsTrue(Expression expression)
        public static Func<TIn, TOut> CreateUnary<TIn, TOut>(Operation operation)
        {

            Func<TIn, TOut> result = null;
            var inType = typeof(TIn);
            var outType = typeof(TOut);
            try
            {
                var source = Expression.Parameter(inType, "source");
                // the next will throw if no conversion exists
                UnaryExpression expression = null;

                switch (operation)
                {
                    case Operation.Convert:
                        expression = Expression.Convert(source, outType);
                        break;
                    case Operation.ConvertUnchecked:
                        expression = Expression.ConvertChecked(source, outType);
                        break;
                    case Operation.Negate:
                        expression = Expression.Negate(source);
                        break;
                    case Operation.NegateUnchecked:
                        expression = Expression.NegateChecked(source);
                        break;
                    case Operation.Not:
                        expression = Expression.Not(source);
                        break;
                    default:
                        throw new NotImplementedException();
                }

                result = Expression.Lambda<Func<TIn, TOut>>(expression, source).Compile();
            }
            catch (Exception ex)
            {
                if (result == null)
                    throw ex;
            }
            return result;
        }


        public static Func<T1, T2, TOut> CreateBinaryFunc<T1, T2, TOut>(Operation operation)
        {

            Func<T1, T2, TOut> result = null;
            var aType = typeof(T1);
            var bType = typeof(T2);
            var outType = typeof(TOut);
            try
            {
                var a = Expression.Parameter(aType, "a");
                var b = Expression.Parameter(bType, "b");
                // the next will throw if no conversion exists
                BinaryExpression expression = null;

                switch (operation)
                {
                    case Operation.AddUnchecked:
                    case Operation.Add:
                        var converter = Expression.Convert(b, aType);
                        expression = Expression.Add(a, converter);
                        break;
                    default:
                        throw new NotImplementedException();
                }

                result = Expression.Lambda<Func<T1, T2, TOut>>(Expression.Convert(expression, outType), a, b).Compile();
            }
            catch (Exception ex)
            {
                if (result == null)
                    throw ex;
            }
            return result;
        }

   
    }
  
}
