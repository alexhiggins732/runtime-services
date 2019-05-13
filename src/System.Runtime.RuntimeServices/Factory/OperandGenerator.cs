using static System.Runtime.RuntimeServices.MsilOpcodes.MsilOpcodeInterfaces;
using static System.Runtime.RuntimeServices.OperandFactory;
namespace System.Runtime.RuntimeServices
{
    public static class OperandGenerator
    {
        static OperandGenerator()
        {
            OpCodeOperation<IOpCode_Neg>.Operation = OperandType.Negate;
            OpCodeOperation<IOpCode_Castclass>.Operation = OperandType.Convert;
            OpCodeOperation<IOpCode_Add>.Operation = OperandType.AddUnchecked;
            OpCodeOperation<IOpCode_Add_ovf>.Operation = OperandType.Add;
            OpCodeOperation<IOpCode_Add_ovf_un>.Operation = OperandType.Add;
            OpCodeOperation<IOpCode_Not>.Operation = OperandType.Not;
        }
        //TODO:

        public struct OpCodeOperation<T>
        {
            public static OperandType Operation;
        }

         
        public struct Negate<T>
        {
            public static Func<T, T> Op = CreateUnary<T, T>(OperandType.Negate);
        }

        public struct Add<T1, T2, TOut>
        {
            public static Func<T1, T2, TOut> Op
                = CreateBinary<T1, T2, TOut>(OperandType.Add);
        }

        public struct AddUnchecked<T1, T2, TOut>
        {
            public static Func<T1, T2, TOut> Op
                = CreateBinary<T1, T2, TOut>(OperandType.AddUnchecked);
        }

        public struct Not<T>
        {
            public static Func<T, T> Op
                = CreateUnary<T, T>(OperandType.Not);
        }

  
    }
}
