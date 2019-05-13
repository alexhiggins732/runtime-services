using System.Collections.Concurrent;
using System.Reflection;
using static System.Runtime.RuntimeServices.Conversions;

namespace System.Runtime.RuntimeServices
{
    public class TypedReferenceDevelopHelper
    {
        public static void RunOperatorTests()
        {
            var intref = 1.ToTypedReference();
            var sbref = ((sbyte)2).ToTypedReference();
            var uintref = ((uint)3).ToTypedReference();


            //var resultTyped = GenericAdd<int, int>.Add<int>(intref, sbref);
            var inlineResult = sbref.As().Numeric.Add(uintref);
            var inlineType = sbref.As().Numeric.Add(1);
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

        public static IFunc GetDefaultFunc(Type type)
        {
            var reference = GetTypedReference(type);
            return reference.As().FnDefault;
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


    
        Type GenericArgumentType { get; }

        IRuntimeTypedReference Clone(); //interface to create new instances of TypedReference<T> without using reflection
        IRuntimeTypedReference SetValue(object objectReference); // interface to unbox object references
        TResult RuntimeCall<TIn, TResult>(IRuntimeTypedReference instance, TIn Other, Func<IRuntimeTypedReference, TIn, TResult> callback);

        IRuntimeConvert Comparer { get; }



    }



    public struct RuntimeTypedReference<T> : IRuntimeTypedReference//, IGenericType<T>
    {


        public RuntimeTypedReference(T value) => this.Value = value;


        IGenericAs IGeneric.As() => new As<T> { Value = Value };


        public T Value;
        public static Type GenericType => typeof(T);
        public Type GenericArgumentType => GenericType;



        public Converter<T> Converter =>  new Converter<T> { Value = Value };
        public IRuntimeConvert Comparer => Converter;
      

        public TOut Cast<TOut>() => TypedReferenceConverter<T, TOut>.Convert(Value);
        public object Cast(Type outType) => (GenericType == outType) ? Value : Value.To(outType);
        public object DirectCast(Type outType) => (GenericType == outType) ? Value : throw new InvalidOperationException($"Cannot DirecCast {GenericType.Name} t {outType.Name}");


        public TOut DirectCast<TOut>() => (TOut)(object)Value;
        public IRuntimeTypedReference Clone() => SetValue(this, default(T));        
        public IRuntimeTypedReference SetValue(object objectReference) => SetValue(this, objectReference);
     
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
