using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.ConversionServices.MsilOpcodes.MsilOpcodeInterfaces;

namespace System.Runtime.ConversionServices
{

    public static class OperandGenerator
    {
        static OperandGenerator()
        {
            OpCodeOperation<IOpCode_Neg>.Operation = Operation.Negate;
            OpCodeOperation<IOpCode_Castclass>.Operation = Operation.Convert;
            OpCodeOperation<IOpCode_Add>.Operation = Operation.AddUnchecked;
            OpCodeOperation<IOpCode_Add_ovf>.Operation = Operation.Add;
            OpCodeOperation<IOpCode_Add_ovf_un>.Operation = Operation.Add;
            OpCodeOperation<IOpCode_Not>.Operation = Operation.Not;
        }
        //TODO:
        public struct OpCodeOperation<T>
        {
            public static Operation Operation;
        }
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

        public struct Negate<T>
        {
            public static Func<T, T> Op = CreateUnary<T, T>(Operation.Negate);
        }
        public struct Add<T1, T2, TOut>
        {
            public static Func<T1, T2, TOut> Op
                = CreateBinary<T1, T2, TOut>(Operation.Add);
        }
        public struct AddUnchecked<T1, T2, TOut>
        {
            public static Func<T1, T2, TOut> Op
                = CreateBinary<T1, T2, TOut>(Operation.AddUnchecked);
        }
        public struct Not<T>
        {
            public static Func<T, T> Op
                = CreateUnary<T, T>(Operation.Not);
        }

        public static Func<T1, T2, TOut> CreateBinary<T1, T2, TOut>(Operation operation)
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
            Operation operation = OpCodeOperation<TOpCode>.Operation;
            return CreateUnary<TIn, TOut>(operation);
        }
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
    }
}
