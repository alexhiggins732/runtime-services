using System.Collections.Concurrent;
using static System.Runtime.ConversionServices.Conversions;

namespace System.Runtime.ConversionServices
{
    public class TypedReferenceDevelopHelper
    {
        public static void RunOperatorTests()
        {
            var intref = 1.ToTypedReference();
            var sbref = ((sbyte)2).ToTypedReference();
            var uintref = ((uint)3).ToTypedReference();


            var result = GenericArithmetic.Add(intref, sbref);

            var result2 = GenericArithmetic.AddUnchecked<int>(intref, sbref);

            //var resultTyped = GenericAdd<int, int>.Add<int>(intref, sbref);
            var inlineResult = sbref.Add(uintref);
            var inlineType = sbref.Add<int>(uintref);
        }
    }




    public struct GenericArithmetic
    {
        /// <summary>
        /// This method requires operators to be defined on <see cref="RuntimeTypedReference{T}"/>
        ///     Adding adding the numerous operators will make the definition become unweildy.
        ///     We certainly could manage with partial structs but... can we find a better way?
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static IRuntimeTypedReference Add(IRuntimeTypedReference a, IRuntimeTypedReference b)
        {
            return a.Add(b);
        }

        public static IRuntimeTypedReference AddUnchecked(IRuntimeTypedReference a, IRuntimeTypedReference b)
        {
            return TypedReferenceFactory.BinaryCall(a, b, OperatorType.Add, OperatorOptions.Unchecked);
        }
        public static T AddUnchecked<T>(IRuntimeTypedReference a, IRuntimeTypedReference b)
        {
            return TypedReferenceFactory.BinaryCall(a, b, OperatorType.Add, OperatorOptions.Unchecked).Cast<T>(); ;
        }

        static GenericArithmetic()
        {
            GenericArithmeticFactory.RegisterOperators();
        }

    }

    public static class GenericArithmeticFactory
    {
        internal static void RegisterOperators()
        {
            registerAdditionOperators();
        }

        private static void registerAdditionOperators()
        {
            GenericAdd<sbyte, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<sbyte, byte>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<sbyte, short>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<sbyte, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<sbyte, int>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<sbyte, uint>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericAdd<sbyte, long>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericAdd<sbyte, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>((ulong)a + b);
            GenericAdd<sbyte, float>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericAdd<sbyte, double>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericAdd<sbyte, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);

            GenericAdd<byte, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<byte, byte>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<byte, short>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<byte, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<byte, int>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<byte, uint>.Op_Add = (a, b) => new RuntimeTypedReference<uint>(a + b);
            GenericAdd<byte, long>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericAdd<byte, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>((ulong)a + b);
            GenericAdd<byte, float>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericAdd<byte, double>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericAdd<byte, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);


            GenericAdd<short, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<short, byte>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<short, short>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<short, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<short, int>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<short, uint>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericAdd<short, long>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericAdd<short, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>((ulong)a + b);
            GenericAdd<short, float>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericAdd<short, double>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericAdd<short, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);



            GenericAdd<ushort, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<ushort, byte>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<ushort, short>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<ushort, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<ushort, int>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<ushort, uint>.Op_Add = (a, b) => new RuntimeTypedReference<uint>(a + b);
            GenericAdd<ushort, long>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericAdd<ushort, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>((ulong)a + b);
            GenericAdd<ushort, float>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericAdd<ushort, double>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericAdd<ushort, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);




            GenericAdd<int, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<int, byte>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<int, short>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<int, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<int, int>.Op_Add = (a, b) => new RuntimeTypedReference<int>(a + b);
            GenericAdd<int, uint>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericAdd<int, long>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericAdd<int, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>((ulong)a + b);
            GenericAdd<int, float>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericAdd<int, double>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericAdd<int, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);



            GenericAdd<uint, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericAdd<uint, byte>.Op_Add = (a, b) => new RuntimeTypedReference<uint>(a + b);
            GenericAdd<uint, short>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericAdd<uint, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<uint>(a + b);
            GenericAdd<uint, int>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericAdd<uint, uint>.Op_Add = (a, b) => new RuntimeTypedReference<uint>(a + b);
            GenericAdd<uint, long>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericAdd<uint, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>((ulong)a + b);
            GenericAdd<uint, float>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericAdd<uint, double>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericAdd<uint, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);

            GenericAdd<long, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericAdd<long, byte>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericAdd<long, short>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericAdd<long, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericAdd<long, int>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericAdd<long, uint>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericAdd<long, long>.Op_Add = (a, b) => new RuntimeTypedReference<long>(a + b);
            GenericAdd<long, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>((ulong)a + b);
            GenericAdd<long, float>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericAdd<long, double>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericAdd<long, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);

            GenericAdd<ulong, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>(a + (ulong)b);
            GenericAdd<ulong, byte>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>(a + b);
            GenericAdd<ulong, short>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>(a + (ulong)b);
            GenericAdd<ulong, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>(a + b);
            GenericAdd<ulong, int>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>(a + (ulong)b);
            GenericAdd<ulong, uint>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>(a + b);
            GenericAdd<ulong, long>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>(a + (ulong)b);
            GenericAdd<ulong, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<ulong>(a + b);
            GenericAdd<ulong, float>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericAdd<ulong, double>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericAdd<ulong, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);

            GenericAdd<float, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericAdd<float, byte>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericAdd<float, short>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericAdd<float, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericAdd<float, int>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericAdd<float, uint>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericAdd<float, long>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericAdd<float, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericAdd<float, float>.Op_Add = (a, b) => new RuntimeTypedReference<float>(a + b);
            GenericAdd<float, double>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericAdd<float, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>((decimal)a + b);

            GenericAdd<double, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericAdd<double, byte>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericAdd<double, short>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericAdd<double, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericAdd<double, int>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericAdd<double, uint>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericAdd<double, long>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericAdd<double, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericAdd<double, float>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericAdd<double, double>.Op_Add = (a, b) => new RuntimeTypedReference<double>(a + b);
            GenericAdd<double, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>((decimal)a + b);

            GenericAdd<decimal, sbyte>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);
            GenericAdd<decimal, byte>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);
            GenericAdd<decimal, short>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);
            GenericAdd<decimal, ushort>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);
            GenericAdd<decimal, int>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);
            GenericAdd<decimal, uint>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);
            GenericAdd<decimal, long>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);
            GenericAdd<decimal, ulong>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);
            GenericAdd<decimal, float>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + (decimal)b);
            GenericAdd<decimal, double>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + (decimal)b);
            GenericAdd<decimal, decimal>.Op_Add = (a, b) => new RuntimeTypedReference<decimal>(a + b);


        }
    }

    public struct GenericAdd<T1, T2>
    {
        public static Func<T1, T2, IRuntimeTypedReference> Op_Add = null;
        //public static Func<ITypedReference, ITypedReference, ITypedReference> Add =
        //    (a, b) => Op_Add((T1)a.BoxedValue, (T2)b.BoxedValue);
        //public static ITypedReference Add(ITypedReference a, ITypedReference b)
        //    => Op_Add((T1)a.BoxedValue, (T2)b.BoxedValue);

        //public static
        //    TOut Add<TOut>(ITypedReference a, ITypedReference b) => 
        //    Add(a, b).DirectCast<TOut>();

    }

    public enum OperatorType
    {
        Add,
        AddUnchecked,
        Subtract,
        SubtractUnchecked,
    }
    public enum OperatorOptions
    {
        None = 0,
        Overflow = 1,
        Unchecked = 1,
        Unsafe = 2
    }
    public class TypedReferenceFactory
    {
        static ConcurrentDictionary<Type, IRuntimeTypedReference> typedReferenceCache
           = new ConcurrentDictionary<Type, IRuntimeTypedReference>();

        public static IRuntimeTypedReference GetTypedReference(object objectReference)
        {
            return typedReferenceCache.
                GetOrAdd(objectReference.GetType(), (x) => CreateTypedReference(x))
                .Clone()
                .SetValue(objectReference); ;
        }

        public static IRuntimeTypedReference GetTypedReference<T>(T typeReference)
        {
            var genericType = typeof(T);
            if (genericType == typeof(object)) return GetTypedReference(typeReference);

            var reference = typedReferenceCache.
                GetOrAdd(typeof(T), (x) => CreateTypedReference(x)).Clone();
            reference.SetValue(typeReference);
            return (IRuntimeTypedReference)reference;
        }

        internal static IRuntimeTypedReference BinaryCall
            (IRuntimeTypedReference a, IRuntimeTypedReference b, OperatorType operatorType, OperatorOptions operatorOptions)
        {
            return a.BinaryCall(b, operatorType, operatorOptions);
        }

        private static IRuntimeTypedReference CreateTypedReference(Type x)
        {
            var genericReferenceType = typeof(RuntimeTypedReference<>).MakeGenericType(x);
            var instance = Activator.CreateInstance(genericReferenceType);
            return (IRuntimeTypedReference)instance;
        }
    }

    public interface IObjectReference { }

    public struct ObjectReference : IObjectReference
    {
        object Value;
        public IRuntimeTypedReference TypedReference { get; private set; }
        public ObjectReference(object value)
        {
            this.Value = value;
            this.TypedReference = TypedReferenceFactory.GetTypedReference(value);
        }
    }


    public interface IRuntimeTypedReference : IGeneric
    {
        T Cast<T>();
        T DirectCast<T>();
        object BoxedValue { get; }
        Type GenericArgumentType { get; }
        IRuntimeTypedReference Clone(); //interface to create new instances of TypedReference<T> without using reflection
        IRuntimeTypedReference SetValue(object objectReference); // interface to unbox object references

        //can we turn this into a generic pattern.
        IRuntimeTypedReference Add(IRuntimeTypedReference other);
        IRuntimeTypedReference Add<TOther>(RuntimeTypedReference<TOther> other);
        TOut Add<TOut>(IRuntimeTypedReference other);

        IRuntimeTypedReference BinaryCall(IRuntimeTypedReference other, OperatorType opType);
        IRuntimeTypedReference BinaryCall(IRuntimeTypedReference other, OperatorType opType, OperatorOptions operatorOptions);
        IRuntimeTypedReference BinaryCallHandler<TOther>(RuntimeTypedReference<TOther> other, OperatorType opType);
        IRuntimeTypedReference BinaryCallHandler<TOther>(RuntimeTypedReference<TOther> other, OperatorType opType, OperatorOptions operatorOptions);


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
                    return GenericAdd<T1, T2>.Op_Add(a, b);
                case OperatorType.AddUnchecked:
                    return GenericAdd<T1, T2>.Op_Add(a, b);
                case OperatorType.Subtract:
                case OperatorType.SubtractUnchecked:
                    return GenericAdd<T1, T2>.Op_Add(a, b);
                default: throw new NotImplementedException();
            }
        }
    }

    public struct RuntimeTypedReference<T> : IRuntimeTypedReference//, IGenericType<T>
    {
        public T TDefault() => default(T);

        //public IGenericFactory GenericFactory2 => GenericFactory<T>.Instance;
        public IGenericStruct Generic => new Generic<T> { Value = Value };


        public T Value;
        public static Type GenericArgumentTypeCache => typeof(T);
        public Type GenericArgumentType => GenericArgumentTypeCache;
        public object BoxedValue => Value;



        public RuntimeTypedReference(T value) => this.Value = value;
        public TOut Cast<TOut>() => TypedReferenceConverter<T, TOut>.Convert(Value);
        public TOut DirectCast<TOut>() => (TOut)(object)Value;
        public IRuntimeTypedReference Clone()
        {
            return SetValue(this, default(T));
        }
        public IRuntimeTypedReference SetValue(object objectReference)
        {
            return SetValue(this, objectReference);
        }
        private IRuntimeTypedReference SetValue(IRuntimeTypedReference typedReference, object objectReference)
        {
            var other = (RuntimeTypedReference<T>)typedReference;
            other.Value = (T)objectReference;
            return (IRuntimeTypedReference)other;
        }

        public IRuntimeTypedReference BinaryCall(IRuntimeTypedReference other, OperatorType operatorType)
        {
            return other.BinaryCallHandler<T>(this, operatorType, OperatorOptions.None);
        }
        public IRuntimeTypedReference BinaryCall(IRuntimeTypedReference other, OperatorType operatorType, OperatorOptions operatorOptions)
        {
            return other.BinaryCallHandler<T>(this, operatorType, operatorOptions);
        }
        public IRuntimeTypedReference BinaryCallHandler<TOther>(RuntimeTypedReference<TOther> other, OperatorType operatorType)
        {
            return OperandFactory<T, TOther>.Call(this.Value, other.Value, operatorType, OperatorOptions.None);
        }
        public IRuntimeTypedReference BinaryCallHandler<TOther>(RuntimeTypedReference<TOther> other, OperatorType operatorType, OperatorOptions operatorOptions)
        {
            return OperandFactory<T, TOther>.Call(this.Value, other.Value, operatorType, operatorOptions);
        }

        public IRuntimeTypedReference Add(IRuntimeTypedReference other)
        {
            return other.Add<T>(this);
        }
        public TOther Add<TOther>(IRuntimeTypedReference other)
        {
            var result = other.Add<T>(this);
            if (result.GenericArgumentType != typeof(TOther))
                return result.Cast<TOther>();
            return (TOther)result.BoxedValue;
        }

        public IRuntimeTypedReference Add<TOther>(RuntimeTypedReference<TOther> other)
        {
            var thisRefType = typeof(T);
            var thisType = Value.GetType();
            var otherRefType = typeof(TOther);
            var othertype = other.GetType();
            return GenericAdd<T, TOther>.Op_Add(this.Value, other.Value);
        }



        public static implicit operator T(RuntimeTypedReference<T> typedReference) => typedReference.Value;
        public static explicit operator RuntimeTypedReference<T>(T value) => (RuntimeTypedReference<T>)TypedReferenceFactory.GetTypedReference<T>(value);
    }

    public struct TypedReferenceConverter<T, TOut>
    {
        public static Func<T, TOut> Convert = CreateConverter<T, TOut>();
    }

    public static class TypedReferenceExtensions
    {
        //public static ObjectReference ToObjectReference(this object value)
        //{
        //    if (value is ObjectReference objectReference)
        //        return objectReference;
        //    return new ObjectReference(value);
        //}
        public static IRuntimeTypedReference ToTypedReference<T>(this T value)
        {
            if (value is ObjectReference objectReference)
                return objectReference.TypedReference;
            if (value.GetType() != typeof(object)) return new RuntimeTypedReference<T> { Value = value };
            return new ObjectReference(value).TypedReference;
        }
        //public static TypedReference<T> ToTypedReference<T>(this T value)
        //{
        //    if (value is ObjectReference objectReference)
        //        return (TypedReference<T>)objectReference.TypedReference;
        //    if (value.GetType() != typeof(object)) return new TypedReference<T> { Value = value };
        //    return (TypedReference<T>)new ObjectReference(value).TypedReference;
        //}

        public static IGenericArithmetic ToArithmetic(this IRuntimeTypedReference value)
        {
            return value.Generic.Arithmetic;
        }
    

        public static Arithmetic<T> ToArithmetic<T>(this RuntimeTypedReference<T> value)
        {
            return new Arithmetic<T> { Value = value.Value };
        }

        public static IRuntimeTypedReference ToTypedReference(this object value)
        {
            if (value is ObjectReference objectReference)
                return objectReference.TypedReference;
            return new ObjectReference(value).TypedReference;
        }
        public static T
            Cast<T>(this ObjectReference objectReference) => objectReference.TypedReference.Cast<T>();
        public static T
           Cast<T>(this IRuntimeTypedReference typedReference) => typedReference.Cast<T>();


        public static T
            Cast<T>(this bool value) => TypedReferenceConverter<bool, T>.Convert(value);
        public static T
           Cast<T>(this char value) => TypedReferenceConverter<char, T>.Convert(value);
        public static T
            Cast<T>(this sbyte value) => TypedReferenceConverter<sbyte, T>.Convert(value);
        public static T
            Cast<T>(this byte value) => TypedReferenceConverter<byte, T>.Convert(value);
        public static T
            Cast<T>(this short value) => TypedReferenceConverter<short, T>.Convert(value);
        public static T
            Cast<T>(this ushort value) => TypedReferenceConverter<ushort, T>.Convert(value);
        public static T
            Cast<T>(this int value) => TypedReferenceConverter<int, T>.Convert(value);
        public static T
            Cast<T>(this uint value) => TypedReferenceConverter<uint, T>.Convert(value);
        public static T
            Cast<T>(this long value) => TypedReferenceConverter<long, T>.Convert(value);
        public static T
            Cast<T>(this ulong value) => TypedReferenceConverter<ulong, T>.Convert(value);
        public static T
            Cast<T>(this float value) => TypedReferenceConverter<float, T>.Convert(value);
        public static T
            Cast<T>(this double value) => TypedReferenceConverter<double, T>.Convert(value);
        public static T
            Cast<T>(this DateTime value) => TypedReferenceConverter<DateTime, T>.Convert(value);
        public static T
            Cast<T>(this DateTimeOffset value) => TypedReferenceConverter<DateTimeOffset, T>.Convert(value);
        public static T
            Cast<T>(this Enum value) => TypedReferenceConverter<Enum, T>.Convert(value);
    }
}
