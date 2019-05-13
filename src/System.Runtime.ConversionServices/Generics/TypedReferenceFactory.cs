using System.Collections.Concurrent;
using System.Reflection;
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


            //var resultTyped = GenericAdd<int, int>.Add<int>(intref, sbref);
            var inlineResult = sbref.Arithmetic().Add(uintref);
            var inlineType = sbref.Arithmetic().Add(1);
        }
    }




    public struct GenericArithmetic
    {
        //TODO: Move to Arithmetic
        /// <summary>
        /// This method requires operators to be defined on <see cref="RuntimeTypedReference{T}"/>
        ///     Adding adding the numerous operators will make the definition become unweildy.
        ///     We certainly could manage with partial structs but... can we find a better way?
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        //public static IRuntimeTypedReference Add(IRuntimeTypedReference a, IRuntimeTypedReference b)
        //{
        //    return a.Add(b);
        //}

        //public static IRuntimeTypedReference AddUnchecked(IRuntimeTypedReference a, IRuntimeTypedReference b)
        //{
        //    return TypedReferenceFactory.BinaryCall(a, b, OperatorType.Add, OperatorOptions.Unchecked);
        //}
        //public static T AddUnchecked<T>(IRuntimeTypedReference a, IRuntimeTypedReference b)
        //{
        //    return TypedReferenceFactory.BinaryCall(a, b, OperatorType.Add, OperatorOptions.Unchecked).Cast<T>(); ;
        //}

        static GenericArithmetic()
        {
            GenericArithmeticFactory.RegisterOperators();
        }

    }

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

        public static IRuntimeTypedReference GetTypedReference(Type type)
        {
            return typedReferenceCache.
                GetOrAdd(type, (x) => CreateTypedReference(x))
                .Clone()
                .SetValue(type.New()); ;
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

        //internal static IRuntimeTypedReference BinaryCall
        //    (IRuntimeTypedReference a, IRuntimeTypedReference b, OperatorType operatorType, OperatorOptions operatorOptions)
        //{
        //    return a.BinaryCall(b, operatorType, operatorOptions);
        //}

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


    /// <summary>
    /// Interface to provide Runtime Services for with compiler boxed objects as strongly typed generics at runtime.
    /// </summary>
    public interface IRuntimeTypedReference : IGeneric
    {
        T Cast<T>();
        object Cast(Type outType);
        T DirectCast<T>();

        object DirectCast(Type outType);
        object BoxedValue { get; }
        Type GenericArgumentType { get; }

        IRuntimeTypedReference Clone(); //interface to create new instances of TypedReference<T> without using reflection
        IRuntimeTypedReference SetValue(object objectReference); // interface to unbox object references
        TResult RuntimeCall<TIn, TResult>(IRuntimeTypedReference instance, TIn Other, Func<IRuntimeTypedReference, TIn, TResult> callback);

        //IRuntimeTypedReference Op<IOp, TIn>(TIn lhs);
        IRuntimeConvert Comparer { get; }
        //can we turn this into a generic pattern.
        //IRuntimeTypedReference Add(IRuntimeTypedReference other);
        //IRuntimeTypedReference Add<TOther>(RuntimeTypedReference<TOther> other);
        //TOut Add<TOut>(IRuntimeTypedReference other);

        //IRuntimeTypedReference BinaryCall(IRuntimeTypedReference other, OperatorType opType);
        //IRuntimeTypedReference BinaryCall(IRuntimeTypedReference other, OperatorType opType, OperatorOptions operatorOptions);
        //IRuntimeTypedReference BinaryCallHandler<TOther>(RuntimeTypedReference<TOther> other, OperatorType opType);
        //IRuntimeTypedReference BinaryCallHandler<TOther>(RuntimeTypedReference<TOther> other, OperatorType opType, OperatorOptions operatorOptions);


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
                    return GenericBinaryOp<T1, T2>.Op_Subtract(a, b);
                default: throw new NotImplementedException();
            }
        }
    }

    public struct RuntimeTypedReference<T> : IRuntimeTypedReference//, IGenericType<T>
    {
        public T TDefault() => default;

        //public IGenericFactory GenericFactory2 => GenericFactory<T>.Instance;
        public IGenericStruct Generic => new Generic<T> { Value = Value };


        public T Value;
        public static Type GenericType => typeof(T);
        public Type GenericArgumentType => GenericType;
        public object BoxedValue => Value;


        public Converter<T> Converter =>  new Converter<T> { Value = Value };
        public IRuntimeConvert Comparer => Converter;
        public RuntimeTypedReference(T value) => this.Value = value;
        public TOut Cast<TOut>() => TypedReferenceConverter<T, TOut>.Convert(Value);
        public object Cast(Type outType) => (GenericType == outType) ? Value : Value.To(outType);


        public object DirectCast(Type outType) => (GenericType == outType) ? Value : throw new InvalidOperationException($"Cannot DirecCast {GenericType.Name} t {outType.Name}");


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

        public TResult RuntimeCall<TIn, TResult>(IRuntimeTypedReference instance, TIn Other, Func<IRuntimeTypedReference, TIn, TResult> callback)
        {
            return RuntimeCall((RuntimeTypedReference<T>)instance, Other, callback);


        }
        public TResult RuntimeCall<TIn, TResult>(RuntimeTypedReference<T> instance, 
            TIn Other, Func<IRuntimeTypedReference, TIn, TResult> callback)
        {
            return callback(instance, Other).To<TResult>();
        }

        //public IRuntimeTypedReference Op<IOp, TIn>(TIn lhs)
        //{
        //   return MsilOpcodes.ILUnaryOpCode<IOp, TIn, T>.Op(lhs).ToTypedReference<T>();
        //    //throw new NotImplementedException();
        //}



        //public IRuntimeTypedReference BinaryCall(IRuntimeTypedReference other, OperatorType operatorType)
        //{
        //    return other.BinaryCallHandler<T>(this, operatorType, OperatorOptions.None);
        //}
        //public IRuntimeTypedReference BinaryCall(IRuntimeTypedReference other, OperatorType operatorType, OperatorOptions operatorOptions)
        //{
        //    return other.BinaryCallHandler<T>(this, operatorType, operatorOptions);
        //}
        //public IRuntimeTypedReference BinaryCallHandler<TOther>(RuntimeTypedReference<TOther> other, OperatorType operatorType)
        //{
        //    return OperandFactory<T, TOther>.Call(this.Value, other.Value, operatorType, OperatorOptions.None);
        //}
        //public IRuntimeTypedReference BinaryCallHandler<TOther>(RuntimeTypedReference<TOther> other, OperatorType operatorType, OperatorOptions operatorOptions)
        //{
        //    return OperandFactory<T, TOther>.Call(this.Value, other.Value, operatorType, operatorOptions);
        //}

        //public IRuntimeTypedReference Add(IRuntimeTypedReference other)
        //{
        //    return other.Add<T>(this);
        //}
        //public TOther Add<TOther>(IRuntimeTypedReference other)
        //{
        //    var result = other.Add<T>(this);
        //    if (result.GenericArgumentType != typeof(TOther))
        //        return result.Cast<TOther>();
        //    return (TOther)result.BoxedValue;
        //}

        //public IRuntimeTypedReference Add<TOther>(RuntimeTypedReference<TOther> other)
        //{
        //    var thisRefType = typeof(T);
        //    var thisType = Value.GetType();
        //    var otherRefType = typeof(TOther);
        //    var othertype = other.GetType();
        //    return GenericAdd<T, TOther>.Op_Add(this.Value, other.Value);
        //}



        public static implicit operator T(RuntimeTypedReference<T> typedReference) => typedReference.Value;
        public static explicit operator RuntimeTypedReference<T>(T value) => (RuntimeTypedReference<T>)TypedReferenceFactory.GetTypedReference<T>(value);
    }

    public struct TypedReferenceConverter<T, TOut>
    {
        public static Func<T, TOut> Convert = CreateConverter<T, TOut>();
    }

    /// <summary>
    /// Extensions for working with compiler boxed objects as strongly typed generics at runtime.
    /// </summary>
    public static class TypedReferenceExtensions
    {
        //public static ObjectReference ToObjectReference(this object value)
        //{
        //    if (value is ObjectReference objectReference)
        //        return objectReference;
        //    return new ObjectReference(value);
        //}

        public static IRuntimeTypedReference FromFieldInfo(this FieldInfo value)
                => TypedReferenceFactory.GetTypedReference(value.FieldType);


        public static IRuntimeTypedReference FromType(this Type value)
            => TypedReferenceFactory.GetTypedReference(value);


        public static IRuntimeTypedReference ToTypedReference<T>(this T value)
        {
            if (value is ObjectReference objectReference)
                return objectReference.TypedReference;
            if (value.GetType() != typeof(object)) return new RuntimeTypedReference<T> { Value = value };
            return new ObjectReference(value).TypedReference;
        }

        //TODO: Nice extension when needed but clutter's up intellisense. Performance implication
        //      Is an extra call of compiled time type back to a TypedReference which SHOULD be minimal.
        //      If there is a reason to keep the TypedReference<T> around or to expose this api for other purposes.
        //      Perhaps it can be exposed as in a nested interface (Generic?)
        //public static TypedReference<T> ToTypedReference<T>(this T value)
        //{
        //    if (value is ObjectReference objectReference)
        //        return (TypedReference<T>)objectReference.TypedReference;
        //    if (value.GetType() != typeof(object)) return new TypedReference<T> { Value = value };
        //    return (TypedReference<T>)new ObjectReference(value).TypedReference;
        //}

        //TODO: 1) Move to Arithmetic Extensions. 
        //     2) Rename to Arithmetic, or perhaps Numeric or AsNumeric? Need define conventions.
        //public static IGenericArithmetic ToArithmetic(this IRuntimeTypedReference value)
        //{
        //    return value.Generic.Arithmetic;
        //}
        //public static Arithmetic<T> ToArithmetic<T>(this RuntimeTypedReference<T> value)
        //{
        //    return new Arithmetic<T> { Value = value.Value };
        //}

        public static IRuntimeTypedReference ToTypedReference(this object value)
        {
            if (value is ObjectReference objectReference)
                return objectReference.TypedReference;
            return new ObjectReference(value).TypedReference;
        }

        public static IRuntimeTypedReference ToTypedReference(this object value, Type objectType)
        {
            var type = value?.GetType() ?? objectType;
            if (value is ObjectReference objectReference)
                return objectReference.TypedReference;
            return new ObjectReference(value).TypedReference;
        }

        //TODO: Spike! Solidify public Cast/Convert/DirectCast/To/Unbox/ChangeType Apis.
        //      Need to balance functionality requirements vs filling developer's intellisense with noise.
        //      It might be a few extra keystrokes to access API's via a nested property but 
        //          usability will also be hindered by filling auto-complete options with too options.
        //      1) Define a minimal set of required extensions.
        //      2) Profile performance costs for moving methods into nested properties/interfaces. 
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
