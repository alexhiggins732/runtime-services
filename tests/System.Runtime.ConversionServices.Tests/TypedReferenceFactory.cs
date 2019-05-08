using System.Collections.Concurrent;
using static System.Runtime.ConversionServices.Conversions;

/// <summary>
/// Development moved to StaticConversionsGenerator.csproj
/// for NetFX Edit and Continue functionality not working in CoreFx.
/// </summary>
namespace System.Runtime.ConversionServices.Tests
{
    public class TypedReferenceDevelopHelper
    {

    }

    public class TypedReferenceFactory
    {
        static ConcurrentDictionary<Type, ITypedReference> typedReferenceCache
           = new ConcurrentDictionary<Type, ITypedReference>();

        public static ITypedReference GetTypedReference(object objectReference)
        {
            return typedReferenceCache.
                GetOrAdd(objectReference.GetType(), (x) => CreateTypedReference(x))
                .Clone()
                .SetValue(objectReference);;
        }

        public static ITypedReference GetTypedReference<T>(T typeReference)
        {
            var genericType = typeof(T);
            if (genericType == typeof(object)) return GetTypedReference(typeReference);

            var reference = typedReferenceCache.
                GetOrAdd(typeof(T), (x) => CreateTypedReference(x)).Clone();
            reference.SetValue(typeReference);
            return (ITypedReference)reference;
        }

        private static ITypedReference CreateTypedReference(Type x)
        {
            var genericReferenceType = typeof(TypedReference<>).MakeGenericType(x);
            var instance = Activator.CreateInstance(genericReferenceType);
            return (ITypedReference)instance;
        }
    }

    public interface IObjectReference { }

    public struct ObjectReference : IObjectReference
    {
        object Value;
        public ITypedReference TypedReference { get; private set; }
        public ObjectReference(object value)
        {
            this.Value = value;
            this.TypedReference = TypedReferenceFactory.GetTypedReference(value);
        }
    }

    public interface ITypedReference
    {
        T Cast<T>();
        ITypedReference Clone(); //interface to create new instances of TypedReference<T> without using reflection
        ITypedReference SetValue(object objectReference); // interface to unbox object references
    }

    public struct TypedReference<T> : ITypedReference
    {
        T Value;
        public TypedReference(T value) => this.Value = value;
        public TOut Cast<TOut>() => TypedReferenceConverter<T, TOut>.Convert(Value);

        public ITypedReference Clone()
        {
            return SetValue(this, default(T));
        }
        public ITypedReference SetValue(object objectReference)
        {
            return SetValue(this, objectReference);
        }
        private ITypedReference SetValue(ITypedReference typedReference, object objectReference)
        {
            var other = (TypedReference<T>)typedReference;
            other.Value = (T)objectReference;
            return (ITypedReference)other;
        }
        public static implicit operator T(TypedReference<T> typedReference) => typedReference.Value;
        public static explicit operator TypedReference<T>(T value) => (TypedReference<T>)TypedReferenceFactory.GetTypedReference<T>(value);
    }

    public struct TypedReferenceConverter<T, TOut>
    {
        public static Func<T, TOut> Convert = CreateConverter<T, TOut>();
    }

    public static class TypedReferenceExtensions
    {
        public static ObjectReference ToObjectReference(this object value)
        {
            if (value is ObjectReference objectReference)
                return objectReference;
            return new ObjectReference(value);
        }
        public static ITypedReference ToTypedReference(this object value)
        {
            if (value is ObjectReference objectReference)
                return objectReference.TypedReference;
            return new ObjectReference(value).TypedReference;
        }
        public static T
            Cast<T>(this ObjectReference objectReference) => objectReference.TypedReference.Cast<T>();
        public static T
           Cast<T>(this ITypedReference typedReference) => typedReference.Cast<T>();


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
