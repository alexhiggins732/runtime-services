using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using static System.Runtime.RuntimeServices.Conversions;

namespace System.Runtime.RuntimeServices.CompileTimeConverters
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
        //TODO: Move outside of this class and inject via public dependency injection interface.
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

        public static void RegisterConverter2(Func<object, string> p)
        {
            throw new NotImplementedException();
        }

        public struct BoolToDateTime : IConvertFunc<bool>
        {
            public static Func<bool, DateTime> Convert = (value) => throw new InvalidCastException();
        }
        public struct CharToBool : IConvertFunc<char>
        {
            public static Func<char, bool> Convert = (value) => value != default(char);
        }

        //TODO: Expose a static interface to allow dependency injection
        //  via ServiceCollection or offer a ContainerExtension type api.

        public static void RegisterConverter<TIn, TOut>(Func<TIn,TOut> converter)
        {
            RuntimeConverter<TIn, TOut>.Convert = converter;
        }

 
        public static void RegisterConverters(IEnumerable<Delegate> converters)
        {
            //GEEZ alot of work to allow registration of multiple at a time.
            // So much easier and efficient to call RegisterConverter<TIn, TOut>(Func<TIn,TOut> converter)
            //  and directly set the converter via RuntimeConverter<TIn, TOut>.Convert = converter;
            foreach (var converterFunc in converters)
            {
                var methodInfo = converterFunc.Method;
                var fromType = methodInfo.GetParameters()[0].ParameterType;
                var toType = methodInfo.ReturnType;
                //TODO: validate the method type arguments and throw an exception  if the method 
                // doesn't match the Func<TIn,TOut> AKA, public static <TOut> MethodName(<TIn> value)
                //TODO: check the method is statically callable.


                // Sete the cache here before initialize the static RuntimeConverter.
                //      to prevent any issues with Conversions.CreateConverter creating
                //      some invalid converter the consumer is trying to override/fix.
                // Additionally, do a direct set. Add is only used in the initializers
                //      to guarantee uniqueness if internally statically compiled converts.

                cache[fromType][methodInfo.ReturnType]= converterFunc;

                //Get a reference to the static RuntimeConverter<TIn,TOut>.Convert Field
                //      This will initialize the struct if hasn't been yet.
                var t = typeof(RuntimeConverter<,>).MakeGenericType(fromType, methodInfo.ReturnType);

                // If the struct was initialized prior to this call the convert delegate 
                //      will already be set and We need to update it. If it wasn't.
                //      then it already pulled a reference to this function from the cache.
                //      but we'll set here to be safe.

                var converterChache = t.GetField("Convert");
                converterChache.SetValue(null, converterFunc);

            }
            
        }

       
    }

}
