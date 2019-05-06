using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Runtime.ConversionServices
{
    public static partial class Conversions
    {
        /// <summary>
        /// Statically links <see cref="Converter{TIn}"/> 
        /// to <see cref="RuntimeConverter{TIn, TOut}"/> to provide conversion services.
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        public struct Converter<TIn> : IRuntimeConvert
        {

            /// <summary>
            /// The source &lt;<typeparamref name="TIn"/>&gt; object to convert from.
            /// </summary>
            public TIn Value;
            /// <summary>
            /// Provides the Runtime <see cref="Type"/> of the &lt;<typeparamref name="TIn"/>&gt; type parameter.
            /// </summary>
            public Type InType => typeof(TIn);

            /// <summary>
            /// Converts the <see cref="Value"/> to &lt;<typeparamref name="TOut"/>&gt;
            /// </summary>
            /// <typeparam name="TOut">The <see cref="Type"/> to convert to.</typeparam>
            /// <returns></returns>
            public TOut To<TOut>()
            {
                try
                {
                    if (InType == typeof(object)) //can we eliminate type check? probably not.
                        return UnboxTo<TOut>(); // need to cache to eliminate repeated reflection typeof(<>).MakeGenericType
                    //TODO: Let the converter function handle this.
                    if (Value is Array && typeof(TOut).IsArray)
                    {
                        var arr = (Array)(object)(Value);
                        var outType = typeof(TOut).GetElementType();
                        if (arr.Length == 0) return (TOut)(object)Array.CreateInstance(outType, 0);

                        var elType = InType.GetElementType();
                        var runtimeConverterType = typeof(RuntimeConverter<,>);
                        var converterGenericType = runtimeConverterType.MakeGenericType(elType, outType);
                        var boxedMethod = converterGenericType
                                .GetMethod("BoxedConvert", singleObjectParameter);
                        var elementConvert = (Func<object, object>)boxedMethod
                            .CreateDelegate(typeof(Func<object, object>));

                        var res = Array.CreateInstance(outType, arr.Length);
                        int index = 0;
                        foreach (var el in arr)
                        {
                            res.SetValue(elementConvert(el), index++);
                        }
                        return (TOut)(object)res;

                    }

                    return RuntimeConverter<TIn, TOut>.Convert(Value);
                }
                catch (TypeLoadException e)
                {
                    throw e.InnerException;
                }
            }


            /// <summary>
            /// Internal interface to support unboxing runtime types statically
            /// typed as <see cref="object"/> at compile time.
            /// </summary>
            private IRuntimeConvert boxedConverter;


            // Can this be a global reference so we don't have dictionary for each ConvertSource<T>?
            // For now it is only used and called for boxed objects so keep it here.
            // TODO: Benchmark use of Concurrent Dictionary
            /// <summary>
            /// Static <see cref="ConvertSource{TIn}"/> cache generated at Runtime to support unboxing 
            /// objects statically typed as <see cref="object"/> references at compile time.
            /// </summary>
            private static ConcurrentDictionary<Type, Type> unboxtypes = new ConcurrentDictionary<Type, Type>();

            /// <summary>
            /// Internal method to convert a boxed <see cref="Value"/> to &lt;<typeparamref name="TOut"/>&gt;
            /// </summary>
            /// <typeparam name="TOut"></typeparam>
            /// <returns></returns>
            private TOut UnboxTo<TOut>()
            {
                if (Value == null) return default(TOut);
                if (boxedConverter is null)
                    boxedConverter = (IRuntimeConvert)Activator.CreateInstance(unboxtypes.GetOrAdd(Value.GetType(), (x) => typeof(Converter<>).MakeGenericType(x)));
                boxedConverter.Unbox(Value);
                return boxedConverter.To<TOut>();
            }

            // private Func<object, object> GetFunc = RuntimeConverter<TIn, TOut>.BoxedConvert;

            /// <summary>
            /// Returns the <see cref="Converter{TIn}"/> with the <see cref="Value"/> unboxed 
            /// from the <paramref name="boxed"/> object reference.
            /// </summary>
            /// <param name="boxed">A boxed object reference &lt;<typeparamref name="TIn"/>&gt;</param>
            /// <returns></returns>
            public IRuntimeConvert Unbox(object boxed)
            {
                Value = (TIn)boxed;
                return this;
            }

            public IRuntimeConvert Unbox()
            {
                if (InType == typeof(object))
                {
                    var converter = (IRuntimeConvert)Activator.CreateInstance(unboxtypes.GetOrAdd(Value.GetType(), (x) => typeof(Converter<>).MakeGenericType(x)));
                    converter.Unbox(Value);
                    return converter;
                }
                else
                    return this;
            }

            /// <summary>
            /// Statically cached <see cref="MethodInfo"/> reference to <see cref="To{TOut}"/> 
            /// to support Runtime conversions using <see cref="MethodInfo.MakeGenericMethod(Type[])"/>.
            /// </summary>
            private static MethodInfo toMethod;


            /// <summary>
            /// Returns or initializes the statically cached <see cref="toMethod"/> based on the Runtime <see cref="Type"/>
            /// of this instance.
            /// </summary>
            private MethodInfo ToMethod => (toMethod ?? (toMethod = this.GetType().GetMethod(nameof(To), Type.EmptyTypes)));

            /// <summary>
            /// Converts the <see cref="Value"/> to the target Runtime <see cref="Type"/>.
            /// </summary>
            /// <param name="type"></param>
            /// <returns></returns>
            public object To(Type type)
            {
                var genericMethod = ToMethod.MakeGenericMethod(new[] { type });
                try { return genericMethod.Invoke(this, emptyArray); }
                catch (Exception ex) { throw ex.InnerException; } //avoid throwing useless System.Reflection.TargetInvocationException
            }

            /// <summary>
            /// Statically unboxes a boxed &lt;<typeparamref name="TIn"/>&gt; object reference and converts it to the target Runtime <see cref="Type"/>.
            /// </summary>
            /// <param name="source"></param>
            /// <param name="type"></param>
            /// <returns></returns>
            public static object To(object source, Type type) => (new Converter<TIn> { Value = (TIn)source }).To(type);

            public static implicit operator TIn(Converter<TIn> converter) => converter.Value;
            public static implicit operator Converter<TIn>(TIn value) => new Converter<TIn> { Value = value };

            bool IRuntimeConvert.Equals(object other)
            {
                var otherValue = other.Convert().To<TIn>();
                if (Value is IComparable)
                {
                    return ((IComparable)Value).CompareTo(otherValue) == 0;
                }
                else if (Value is IComparable<TIn>)
                {
                    return ((IComparable<TIn>)Value).CompareTo(otherValue) == 0;
                }
                else if (Value is IEquatable<TIn>)
                {
                    return ((IEquatable<TIn>)Value).Equals(otherValue);
                }
                else // fall back to object comparison
                {
                    return ((dynamic)Value) == ((dynamic)otherValue);
                }
            }

            int IRuntimeConvert.Compare(object other)
            {
                var otherValue = other.Convert().To<TIn>();
                if (Value is IComparable)
                {
                    return ((IComparable)Value).CompareTo(otherValue);
                }
                else if (Value is IComparable<TIn>)
                {
                    return ((IComparable<TIn>)Value).CompareTo(otherValue);
                }
                else // fall back to dynamic runtime comparison
                {
                    if (((dynamic)Value) == ((dynamic)otherValue)) return 0;
                    if (((dynamic)Value) < ((dynamic)otherValue)) return -1;
                    return 1;
                }
            }

            bool IRuntimeConvert.LessThan(object other)
            {
                var otherValue = other.Convert().To<TIn>();
                if (Value is IComparable)
                {
                    return ((IComparable)Value).CompareTo(otherValue) < 0;
                }
                else if (Value is IComparable<TIn>)
                {
                    return ((IComparable<TIn>)Value).CompareTo(otherValue) < 0;
                }
                else // fall back to the slower dynamic runtime 
                {
                    return ((dynamic)Value) < ((dynamic)otherValue);
                }
            }
            bool IRuntimeConvert.LessThanOrEqual(object other)
            {
                var otherValue = other.Convert().To<TIn>();
                if (Value is IComparable)
                {
                    return ((IComparable)Value).CompareTo(otherValue) <= 0;
                }
                else if (Value is IComparable<TIn>)
                {
                    return ((IComparable<TIn>)Value).CompareTo(otherValue) <= 0;
                }
                else // fall back to object comparison
                {
                    return ((dynamic)Value) <= ((dynamic)otherValue);
                }
            }

            bool IRuntimeConvert.GreaterThan(object other)
            {
                var otherValue = other.Convert().To<TIn>();
                if (Value is IComparable)
                {
                    return ((IComparable)Value).CompareTo(otherValue) > 0;
                }
                else if (Value is IComparable<TIn>)
                {
                    return ((IComparable<TIn>)Value).CompareTo(otherValue) > 0;
                }
                else // fall back to object comparison
                {
                    return ((dynamic)Value) > ((dynamic)otherValue); 
                }
            }
            bool IRuntimeConvert.GreaterThanOrEqual(object other)
            {
                var otherValue = other.Convert().To<TIn>();
                if (Value is IComparable)
                {
                    return ((IComparable)Value).CompareTo(otherValue) >= 0;
                }
                else if (Value is IComparable<TIn>)
                {
                    return ((IComparable<TIn>)Value).CompareTo(otherValue) >= 0;
                }
                else // fall back to object comparison
                {
                    return ((dynamic)Value) >= ((dynamic)otherValue);
                }
            }


            //TODO: create a statically cache comparer interface.
            private TInComparer GetComparer()
            {
                return new TInComparer();
            }
            //TODO
            private class TInComparer : Comparer<TIn>
            {
                public override int Compare(TIn x, TIn y)
                {
                    return 0;
                }
            }

        }


    }
}
