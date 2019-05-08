using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Runtime.ConversionServices
{
    public static partial class Conversions
    {
        public static bool IsNullable<T>(T o) => Nullable.GetUnderlyingType(typeof(T)) is null;
        public static bool IsNullableIConvertible<T>(T o)
        {
            var underylingType = Nullable.GetUnderlyingType(typeof(T));
            return underylingType != null;
        }
        public static T UnderlyingDefault<T>() => 
            (T)((object)null).To(Nullable.GetUnderlyingType(typeof(T)));

        /// <summary>
        /// Generates runtime conversion function
        /// </summary>
        /// <typeparam name="TIn">The input <see cref="Type"/></typeparam>
        /// <typeparam name="TOut">The output <see cref="Type"/></typeparam>
        /// <returns></returns>
        public static Func<TIn, TOut> CreateConverter<TIn, TOut>()
        {
            var staticConverter = CompileTimeConverters.StaticConverters.Find<TIn, TOut>();
            if (staticConverter != null) return staticConverter;

            Func<TIn, TOut> result = null;
            var inType = typeof(TIn);
            var outType = typeof(TOut);
            try
            {
                var source = Expression.Parameter(inType, "source");
                // the next will throw if no conversion exists
                UnaryExpression convert = null;
                //TODO: provide DI for overrides
                if (outType == typeof(string) || (outType == typeof(char[]) && !inType.IsArray))
                {
                    if (inType == typeof(char[]))
                    {
                        result = ((Value) => (TOut)(object)new string((char[])(object)Value));
                    }
                    else
                    {
                        if (outType == typeof(string))
                            result = ((Value) => (TOut)(object)Value.ToString());
                        else
                            result = ((Value) => (TOut)(object)Value.ToString().ToCharArray());
                    }
                }
                else
                {
                    convert = Expression.Convert(source, outType);
                }
                result = result ?? Expression.Lambda<Func<TIn, TOut>>(convert, source).Compile();
            }
            catch (Exception ex)
            {
                //TODO: handle coersion for nullables.
                var defaultIn = default(TIn);
                var defaultOut = default(TOut);

                var underlyingIn = Nullable.GetUnderlyingType(inType);
                var underlyingOut = Nullable.GetUnderlyingType(outType);
                if (underlyingIn != null) defaultIn = UnderlyingDefault<TIn>();
                if (underlyingOut != null) defaultOut = UnderlyingDefault<TOut>();


                if (defaultIn is IConvertible && defaultOut is IConvertible)
                {
                    //Func<TIn, TOut> fn = (Value) => (TOut)System.Convert.ChangeType(Value, outTypeCode);
                    //{
                    //    var fnResult = System.Convert.ChangeType(Value, outTypeCode);
                    //    return (TOut)fnResult;
                    //};
                    var outTypeCode = System.Convert.GetTypeCode(defaultOut);
                    return ((Value) => (TOut)System.Convert.ChangeType(Value, outTypeCode));
                }

                if (result == null)
                    throw ex;
            }
            return result;
        }

        /// <summary>
        /// Statically cached empty array to eliminate duplicative object creation for method calls
        /// that require an empty array
        /// </summary>
        public static readonly object[] emptyArray = new object[] { };

        public static readonly Type[] singleObjectParameter = new[] { typeof(object) };

        public static readonly Type[] twoObjectParameter = new[] { typeof(object), typeof(object) };
        //public static TOut ToArray<TIn, TOut>(this TIn array) => Array.ConvertAll(array, (x) => RuntimeConverter<TIn, TOut>.Convert(x));

        public static TOut[] ToArray<TIn, TOut>(this TIn[] array) => Array.ConvertAll(array, (x) => RuntimeConverter<TIn, TOut>.Convert(x));
        /// <summary>
        /// Provides runtime conversion services for the source <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Converter<T> Convert<T>(this T value)
            { return new Converter<T> { Value = value }; }

        public static IRuntimeConvert Compare<T>(this T value)
            { return new Converter<T> { Value = value }; }

        public static int Compare<T>(this T value, object other)
            { return value.Compare().Compare(other); }

        public static IRuntimeConvert Unbox<T>(this T value)
        {
            var converter = Convert(value);
            return converter.Unbox();
        }
        /// <summary>
        /// Performs runtime conversion of the boxed object <paramref name="value"/> to the target <paramref name="type"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object To(this object value, Type type) => value.Convert().To(type);

        /// <summary>
        /// Converts ths source <paramref name="value"/> to the <typeparamref name="TOut"/> <see cref="Type"/>
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TOut To<TOut>(this object value) => value.Convert().To<TOut>();

        /// <summary>
        /// Performs a boxed conversion of the <typeparamref name="TIn"/> <paramref name="value"/> to the target <paramref name="type"/>
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object To<TIn>(this TIn value, Type type) => value.Convert().To(type);

        /// <summary>
        /// Converts the <typeparamref name="TIn"/> <paramref name="value"/> to the target <typeparamref name="TOut"/> <see cref="Type"/>
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TOut To<TIn, TOut>(this TIn value) => value.Convert().To<TOut>();

        /// <summary>
        /// Converts the <see cref="IConvertible"/> value to the target IConvertible <typeparamref name="TOut"/> <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IConvertible ChangeType<TOut>(this IConvertible value) => (IConvertible)System.Convert.ChangeType(value, typeof(TOut));


    }
}
