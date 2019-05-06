using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System.Runtime.ConversionServices.CompileTimeConverters
{
    public struct StaticConverters
    {
        static Dictionary<Type, Dictionary<Type, Delegate>> cache = new Dictionary<Type, Dictionary<Type, Delegate>>();
        static StaticConverters()
        {
            //cache.Add(typeof(bool), BoolConverter.GetConverters());

            var allNestedTypes = typeof(StaticConverters)
                .GetNestedTypes().ToList();

            var nestedTypes = typeof(StaticConverters)
                .GetNestedTypes()
                .Where(t => t.GetInterfaces().Any(x =>
                 x.IsGenericType &&
                 x.GetGenericTypeDefinition() == typeof(IConvertFunc<>)))
                .ToList();

            foreach (var nestedType in nestedTypes)
            {
                var imp = nestedType.GetInterface("IConvertFunc`1");

                var converterType = imp.GetGenericArguments()[0];
                if (!cache.ContainsKey(converterType)) cache.Add(converterType, new Dictionary<Type, Delegate>());

                var converter = nestedType
                    .GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Where(x => x.Name == "Convert")
                    .Select(x => (Delegate)x.GetValue(null))
                    .Single();
                var methodInfo = converter.Method;
                var args = methodInfo.GetGenericArguments();
                cache[converterType].Add(methodInfo.ReturnType, converter);

            }

        }

        private static Delegate CreateDelegate(MethodInfo x)
        {
            var delegatetype = typeof(Func<,>).MakeGenericType(x.GetParameters()[0].ParameterType, x.ReturnType);
            return Delegate.CreateDelegate(delegatetype, x);
        }

        internal static Func<TIn, TOut> Find<TIn, TOut>()
        {
            if (cache.TryGetValue(typeof(TIn), out Dictionary<Type, Delegate> typeCache))
            {
                if (typeCache.TryGetValue(typeof(TOut), out Delegate result))
                {
                    return (Func<TIn, TOut>)result;
                }
            }
            return null;
        }
        //TODO: implement depency injection pattern.
        public interface IConvertFunc { }
        public interface IConvertFunc<T> : IConvertFunc
        {

        }

        public struct BoolToFloat : IConvertFunc<bool>
        {
            public static Func<bool, float> Convert = (value) => ((bool)value) ? 1f : 0f;

            public static Dictionary<Type, Delegate> GetConverters()
            {
                return new Dictionary<Type, Delegate>
                {
                    { typeof(float), Convert }
                };
            }
        }
        public struct BoolToDateTime : IConvertFunc<bool>
        {
            public static Func<bool, DateTime> Convert = (value) => throw new InvalidCastException();
        }
        public struct CharToBool : IConvertFunc<char>
        {
            public static Func<char, bool> Convert = (value) => value != default(char);
        }
    }

}
