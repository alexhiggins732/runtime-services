using System.Numerics;

namespace System.Runtime.RuntimeServices
{
    //TODO: Need to move extensions to TypedReference
    public static class GenericArithmeticExtensions
    {
        //public static IGenericArithmetic Arithmetic
        //    (this IRuntimeTypedReference value) //=> new ArithmeticTest<ITypedReference> { Value = value };
        //   => value.As().Arithmetic;
        //public static Arithmetic<T> Arithmetic<T>
        //    (this RuntimeTypedReference<T> value) => new Arithmetic<T> { Value = value.Value };

        //public static IGenericArithmetic Arithmetic
        //           (this TypedReference<T> value) => new ArithmeticTest<T> { Value = value.Value };

        public static Func<T, Numeric<T>> ArithmeticTestFunc<T>(T Instance) => CreateArithmeticTest;
        public static Numeric<T> CreateArithmeticTest<T>(T value) => new Numeric<T> { Value = value };
        public static IGenericNumeric Add
            (this IGenericNumeric value, IGenericNumeric other)
        {
            return value.Op<IAdd>(other);
        }
        public static IGenericNumeric Add<T>
         (this IGenericNumeric value, T other)
        {
            return value.Op<IAdd>(new Numeric<T>(other));
        }

        public static IGenericNumeric AddUnchecked
            (this IGenericNumeric value, IGenericNumeric other)
        {
            return value.Op<IAddUnchecked>(other);
        }
        public static IGenericNumeric Subtract<T>
            (this IGenericNumeric value, T other)
        {
            return value.Op<ISubtract>(new Numeric<T>(other));
        }
        public static IGenericNumeric SubtractUnchecked
            (this IGenericNumeric value, IGenericNumeric other)
        {
            return value.Op<ISubtract>(other);
        }

        public static T To<T>(this IGenericNumeric arithmetic) => arithmetic.As().To<T>();




        public static Numeric<bool> AsNumeric(this bool value) => (Numeric<bool>)value;
        public static Numeric<sbyte> AsNumeric(this sbyte value) => (Numeric<sbyte>)value;
        public static Numeric<byte> AsNumeric(this byte value) => (Numeric<byte>)value;
        public static Numeric<short> AsNumeric(this short value) => (Numeric<short>)value;
        public static Numeric<ushort> AsNumeric(this ushort value) => (Numeric<ushort>)value;
        public static Numeric<char> AsNumeric(this char value) => (Numeric<char>)value;
        public static Numeric<int> AsNumeric(this int value) => (Numeric<int>)value;
        public static Numeric<uint> AsNumeric(this uint value) => (Numeric<uint>)value;

        public static Numeric<long> AsNumeric(this long value) => (Numeric<long>)value;
        public static Numeric<ulong> AsNumeric(this ulong value) => (Numeric<ulong>)value;
        public static Numeric<float> AsNumeric(this float value) => (Numeric<float>)value;
        public static Numeric<double> AsNumeric(this double value) => (Numeric<double>)value;
        public static Numeric<decimal> AsNumeric(this decimal value) => (Numeric<decimal>)value;
        public static Numeric<BigInteger> AsNumeric(this BigInteger value) => (Numeric<BigInteger>)value;



        public static IGenericNumeric Add<T>(this int value, T other) => Op<int, T, IAdd>.Call(value, other);
        public static IGenericNumeric Add<T>(this int value, IGenericNumeric other) => other.Add(value);
        public static IGenericNumeric Subtract<T>(this int value, T other) => Op<int, T, ISubtract>.Call(value, other);
        public static IGenericNumeric Subtract<T>(this int value, IGenericNumeric other) => other.Subtract(value);



    }
}
