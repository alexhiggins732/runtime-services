namespace System.Runtime.RuntimeServices
{
    public static class GenericArithmeticFactory
    {
        internal static void RegisterOperators()
        {
            RegisterAdditionOperators();
        }

        private static void RegisterAdditionOperators()
        {
            GenericBinaryOp<sbyte, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<sbyte, byte>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<sbyte, short>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<sbyte, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<sbyte, int>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<sbyte, uint>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericBinaryOp<sbyte, long>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericBinaryOp<sbyte, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>((ulong)a + b);
            GenericBinaryOp<sbyte, float>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericBinaryOp<sbyte, double>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericBinaryOp<sbyte, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);

            GenericBinaryOp<byte, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<byte, byte>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<byte, short>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<byte, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<byte, int>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<byte, uint>.Op_Add = (a, b) => new RuntimeTypedReference<uint>(a + b);
            GenericBinaryOp<byte, long>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericBinaryOp<byte, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>((ulong)a + b);
            GenericBinaryOp<byte, float>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericBinaryOp<byte, double>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericBinaryOp<byte, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);


            GenericBinaryOp<short, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<short, byte>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<short, short>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<short, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<short, int>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<short, uint>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericBinaryOp<short, long>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericBinaryOp<short, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>((ulong)a + b);
            GenericBinaryOp<short, float>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericBinaryOp<short, double>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericBinaryOp<short, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);



            GenericBinaryOp<ushort, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<ushort, byte>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<ushort, short>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<ushort, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<ushort, int>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<ushort, uint>.Op_Add = (a, b) => new RuntimeTypedReference<uint>(a + b);
            GenericBinaryOp<ushort, long>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericBinaryOp<ushort, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>((ulong)a + b);
            GenericBinaryOp<ushort, float>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericBinaryOp<ushort, double>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericBinaryOp<ushort, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);




            GenericBinaryOp<int, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<int, byte>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<int, short>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<int, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<int, int>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericBinaryOp<int, uint>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericBinaryOp<int, long>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericBinaryOp<int, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>((ulong)a + b);
            GenericBinaryOp<int, float>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericBinaryOp<int, double>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericBinaryOp<int, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);



            GenericBinaryOp<uint, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericBinaryOp<uint, byte>.Op_Add = (a, b) => new RuntimeTypedReference<uint>(a + b);
            GenericBinaryOp<uint, short>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericBinaryOp<uint, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<uint>(a + b);
            GenericBinaryOp<uint, int>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericBinaryOp<uint, uint>.Op_Add = (a, b) => new RuntimeTypedReference<uint>(a + b);
            GenericBinaryOp<uint, long>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericBinaryOp<uint, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>((ulong)a + b);
            GenericBinaryOp<uint, float>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericBinaryOp<uint, double>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericBinaryOp<uint, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);

            GenericBinaryOp<long, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericBinaryOp<long, byte>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericBinaryOp<long, short>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericBinaryOp<long, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericBinaryOp<long, int>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericBinaryOp<long, uint>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericBinaryOp<long, long>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericBinaryOp<long, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>((ulong)a + b);
            GenericBinaryOp<long, float>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericBinaryOp<long, double>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericBinaryOp<long, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);

            GenericBinaryOp<ulong, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>(a + (ulong)b);
            GenericBinaryOp<ulong, byte>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>(a + b);
            GenericBinaryOp<ulong, short>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>(a + (ulong)b);
            GenericBinaryOp<ulong, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>(a + b);
            GenericBinaryOp<ulong, int>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>(a + (ulong)b);
            GenericBinaryOp<ulong, uint>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>(a + b);
            GenericBinaryOp<ulong, long>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>(a + (ulong)b);
            GenericBinaryOp<ulong, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>(a + b);
            GenericBinaryOp<ulong, float>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericBinaryOp<ulong, double>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericBinaryOp<ulong, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);

            GenericBinaryOp<float, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericBinaryOp<float, byte>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericBinaryOp<float, short>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericBinaryOp<float, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericBinaryOp<float, int>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericBinaryOp<float, uint>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericBinaryOp<float, long>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericBinaryOp<float, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericBinaryOp<float, float>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericBinaryOp<float, double>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericBinaryOp<float, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>((decimal)a + b);

            GenericBinaryOp<double, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericBinaryOp<double, byte>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericBinaryOp<double, short>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericBinaryOp<double, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericBinaryOp<double, int>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericBinaryOp<double, uint>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericBinaryOp<double, long>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericBinaryOp<double, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericBinaryOp<double, float>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericBinaryOp<double, double>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericBinaryOp<double, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>((decimal)a + b);

            GenericBinaryOp<decimal, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);
            GenericBinaryOp<decimal, byte>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);
            GenericBinaryOp<decimal, short>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);
            GenericBinaryOp<decimal, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);
            GenericBinaryOp<decimal, int>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);
            GenericBinaryOp<decimal, uint>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);
            GenericBinaryOp<decimal, long>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);
            GenericBinaryOp<decimal, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);
            GenericBinaryOp<decimal, float>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + (decimal)b);
            GenericBinaryOp<decimal, double>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + (decimal)b);
            GenericBinaryOp<decimal, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);


        }
    }
}
