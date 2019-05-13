using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.RuntimeServices.OperandGenerator;

namespace System.Runtime.RuntimeServices
{
    public enum OperandType
    {
        None = 0,
        Default,
        Negate,
        NegateUnchecked,
        Convert,
        ConvertUnchecked,
        Add,
        AddUnchecked,
        Not

    }
    public static class OperandFactory
    {
        public static Func<T1> CreateNullary<T1>(OperandType operation)
        {
            Func<T1> result = null;
            var aType = typeof(T1);
            try
            {
                switch (operation)
                {
                    case OperandType.Default:
                        return Expression.Lambda<Func<T1>>(Expression.Default(aType)).Compile();
                    default:
                        throw new InvalidOperationException($"Invalid '{operation}' specified for {nameof(CreateNullary)}[{typeof(T1).Name}]");
                }
            }
            catch (Exception ex)
            {
                if (result == null)
                    throw ex;
            }
            return result;
        }
        public static Func<T1, T2, TOut> CreateBinary<T1, T2, TOut>(OperandType operation)
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
                    case OperandType.AddUnchecked:
                    case OperandType.Add:
                        var converter = Expression.Convert(b, aType);
                        expression = Expression.Add(a, converter);
                        break;
                    //case Operation.AddUnchecked:
                    //    expression = Expression.Add(a, b); ;
                    //    break;

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

        public static Func<TIn, TOut> CreateUnary<TIn, TOut, TOpCode>()
        {
            OperandType operation = OpCodeOperation<TOpCode>.Operation;
            return CreateUnary<TIn, TOut>(operation);
        }
        public static Func<TIn, TOut> CreateUnary<TIn, TOut>(OperandType operation)
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
                    case OperandType.Convert:
                        expression = Expression.Convert(source, outType);
                        break;
                    case OperandType.ConvertUnchecked:
                        expression = Expression.ConvertChecked(source, outType);
                        break;
                    case OperandType.Negate:
                        expression = Expression.Negate(source);
                        break;
                    case OperandType.NegateUnchecked:
                        expression = Expression.NegateChecked(source);
                        break;
                    case OperandType.Not:
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
    }

    public struct OperandFactory<T1, T2>
    {
        public static IRuntimeTypedReference Call
            (T1 a, T2 b, OperatorType operatorType, OperatorOptions none)
        {
            //TODO: We don't wan't to have to switch on every operator call.
            //  but the switch prevents needing to hard code every operator
            //  in TypedRefence<T> and ITypedRefeence.
            switch (operatorType)
            {
                case OperatorType.Add:
                    return GenericBinaryOp<T1, T2>.Op_Add(a, b);
                case OperatorType.AddUnchecked:
                    return GenericBinaryOp<T1, T2>.Op_Add(a, b);
                case OperatorType.Subtract:
                case OperatorType.SubtractUnchecked:
                    return GenericBinaryOp<T1, T2>.Op_Add(a, b);
                default: throw new NotImplementedException();
            }
        }
    }
}
