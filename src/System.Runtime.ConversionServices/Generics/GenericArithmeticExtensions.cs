namespace System.Runtime.ConversionServices
{
    //TODO: Need to move extensions to TypedReference
    public static class GenericArithmeticExtensions
    {
        public static IGenericArithmetic Arithmetic
            (this IRuntimeTypedReference value) //=> new ArithmeticTest<ITypedReference> { Value = value };
           => value.Generic.Arithmetic;
        public static Arithmetic<T> Arithmetic<T>
            (this RuntimeTypedReference<T> value) => new Arithmetic<T> { Value = value.Value };

        //public static IGenericArithmetic Arithmetic
        //           (this TypedReference<T> value) => new ArithmeticTest<T> { Value = value.Value };

        public static Func<T, Arithmetic<T>> ArithmeticTestFunc<T>(T Instance) => CreateArithmeticTest;
        public static Arithmetic<T> CreateArithmeticTest<T>(T value) => new Arithmetic<T> { Value = value };
        public static IGenericArithmetic Add
            (this IGenericArithmetic value, IGenericArithmetic other)
        {
            return value.Op<IAdd>(other);
        }
        public static IGenericArithmetic Add<T>
         (this IGenericArithmetic value, T other)
        {
            return value.Op<IAdd>(new Arithmetic<T>(other));
        }

        public static IGenericArithmetic AddUnchecked
            (this IGenericArithmetic value, IGenericArithmetic other)
        {
            return value.Op<IAddUnchecked>(other);
        }
        public static IGenericArithmetic Subtract<T>
            (this IGenericArithmetic value, T other)
        {
            return value.Op<ISubtract>(new Arithmetic<T>(other));
        }
        public static IGenericArithmetic SubtractUnchecked
            (this IGenericArithmetic value, IGenericArithmetic other)
        {
            return value.Op<ISubtract>(other);
        }

        public static T To<T>(this IGenericArithmetic arithmetic) => arithmetic.Generic.To<T>();




        public static Arithmetic<bool>
            Arithmetic(this bool value) => new Arithmetic<bool>(value);
        //public static T
        //   Cast<T>(this char value) => TypedReferenceConverter<char, T>.Convert(value);
        //public static T
        //    Cast<T>(this sbyte value) => TypedReferenceConverter<sbyte, T>.Convert(value);
        //public static T
        //    Cast<T>(this byte value) => TypedReferenceConverter<byte, T>.Convert(value);
        //public static T
        //    Cast<T>(this short value) => TypedReferenceConverter<short, T>.Convert(value);
        //public static T
        //    Cast<T>(this ushort value) => TypedReferenceConverter<ushort, T>.Convert(value);
        public static Arithmetic<int> ArithmeticTest(this int value) => new Arithmetic<int>(value);
        //public static T
        //    Cast<T>(this uint value) => TypedReferenceConverter<uint, T>.Convert(value);
        //public static T
        //    Cast<T>(this long value) => TypedReferenceConverter<long, T>.Convert(value);
        //public static T
        //    Cast<T>(this ulong value) => TypedReferenceConverter<ulong, T>.Convert(value);
        //public static T
        //    Cast<T>(this float value) => TypedReferenceConverter<float, T>.Convert(value);
        //public static T
        //    Cast<T>(this double value) => TypedReferenceConverter<double, T>.Convert(value);
        //public static T
        //    Cast<T>(this DateTime value) => TypedReferenceConverter<DateTime, T>.Convert(value);
        //public static T
        //    Cast<T>(this DateTimeOffset value) => TypedReferenceConverter<DateTimeOffset, T>.Convert(value);

        public static IGenericArithmetic Add<T>(this int value, T other) => Op<int, T, IAdd>.Call(value, other);
        public static IGenericArithmetic Add<T>(this int value, IGenericArithmetic other) => other.Add(value);
        public static IGenericArithmetic Subtract<T>(this int value, T other) => Op<int, T, ISubtract>.Call(value, other);
        public static IGenericArithmetic Subtract<T>(this int value, IGenericArithmetic other) => other.Subtract(value);



    }
}
