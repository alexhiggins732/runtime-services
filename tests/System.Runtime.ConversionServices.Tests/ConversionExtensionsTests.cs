using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using static System.Runtime.ConversionServices.Conversions;
using static System.Runtime.ConversionServices.Tests.BoxExtensions;


namespace System.Runtime.ConversionServices.Tests
{
    public static class BoxExtensions
    {
        public static object New(this Type type, object value) => ((TypeInfo)type).New(value);
        public static object New(this TypeInfo type, object value) =>
            type.DeclaredConstructors.First().Invoke(new[] { value });

        public static TypeInfo BoxType = (TypeInfo)typeof(BoxTest<>);
        public static BoxTest<T> Wrap<T>(this T source) => new BoxTest<T>(source);
        public interface IUnbox
        {
            object Unbox(object value);
        }

        public struct BoxTest<T> : IUnbox
        {
            public T source;
            public Type GenericType;
            public Type RuntimeType;
            public BoxTest(T instance)
            {
                source = instance;
                GenericType = typeof(T);
                RuntimeType = instance.GetType();
            }
            public object Unbox(object value) => (T)value;
            public bool IsInt => source is int;
            public object Unwrap() => BoxType.MakeGenericType(RuntimeType).New(source);
            public object UnwrapTo(Type type) => BoxType.MakeGenericType(type).New(source);
            public BoxTest<U> Unwrap<U>() => new BoxTest<U>(Unbox<U>());
            public U Unbox<U>() => (U)(object)source;
        }
        public static TypeInfo DirectCastType = (TypeInfo)typeof(DirectCast<>);
        public static class DirectCast
        {
            public static Dictionary<Type, IUnbox> casters = new Dictionary<Type, IUnbox>();
            public static object Unbox(object src)
            {
                if (src is null) return src;
                var srcType = src.GetType();
                if (!casters.ContainsKey(srcType))
                {
                    var instance = (IUnbox)Activator.CreateInstance(DirectCastType.MakeGenericType(srcType));
                    casters.Add(srcType, instance);
                    return instance.Unbox(src);
                }
                return casters[srcType].Unbox(src);
            }
        }

        public struct DirectCast<T> : IUnbox
        {
            public object Unbox(object value) => (T)value;
        }
        

    }

    public class ConversionExtensionsTests
    {
      
        

        [Fact]
        public void TestBoxUnbox()
        {
            Stack<object> stack = new Stack<object>();
            var expected = 1;
            stack.Push(expected);
            var boxedInt = stack.Pop();
            var boxTest = boxedInt.Wrap();
            Assert.Throws<System.InvalidCastException>(() => (BoxTest<int>)(object)boxTest);
            var unwrapped = (BoxTest<int>)boxTest.Unwrap();
            Assert.True(boxTest.GenericType == typeof(object));
            Assert.True(boxTest.GenericType != boxTest.source.GetType());
            Assert.True(boxTest.source is int);
            Assert.True(boxTest.IsInt);

           
     
            var unwrappedBox = boxTest.Unwrap<int>();
            var unwrappedBoxAsboxed = boxTest.UnwrapTo(typeof(int));
            Assert.IsType(unwrappedBox.GetType(), unwrappedBoxAsboxed);
            var unboxed = boxTest.Unbox<int>();

            Assert.True(unwrapped.GenericType == typeof(int));
            Assert.True(unwrapped.GenericType == unwrapped.source.GetType());
            Assert.True(unwrapped.source is int);
            Assert.True(unwrapped.IsInt);

            RuntimeConverter<int, byte>.Convert(1);
            Assert.IsType<int>(boxedInt);// this is fails.
            Assert.IsNotType<object>(boxedInt);
            boxedInt.To(typeof(int));
            var unboxedTo = boxedInt.To<int>();
            Assert.IsType<int>(unboxed);

        }

        [Fact]
        public void TestConvertTestsSucceed()
        {
            Stack<object> stack = new Stack<object>();

            stack.Push(null);
            //Test null reference handling.
            var result = stack.Pop().Convert().To<bool>();

            stack.Push(1);
            var convertdx = stack.Pop().To<char>();
            stack.Push(1);
            var convertdt = stack.Pop().To(typeof(double));
            //var eq = convertdx == convertdt;// <-- fails since convertdt will be boxed; 
            //Test fallback from Lamba Convert to System.Convert.ChangeType for IConvertible.
            //"No coercion operator is defined between types 'System.Int32' and 'System.Boolean'.
            stack.Push(1.Convert().To<bool>());// 

            // (object)true
            var boxedIntAsBool = stack.Pop();
            var staticConvert = Converter<object>.To(boxedIntAsBool, typeof(int));
            var t = staticConvert.GetType();


            stack.Push(ulong.MaxValue);
            var boxedUlong = stack.Pop();
            var boxedBoolConvertFromUlong = Converter<object>.To(boxedUlong, typeof(bool));
            var boxedBoolFromUlong = boxedUlong.To(typeof(bool));
            var boxedBoolFromUlongType = boxedBoolFromUlong.GetType();
            var UlongFromBoxedBool = boxedBoolFromUlong.Convert().To<ulong>();

            // Lamba convert bool => int succeeds
            stack.Push(true.Convert().To<int>());

            // (object)1;
            var boxedBoolAsInt = stack.Pop();


            //Test boxed int conversion to int,
            var boxedIntToInt = boxedBoolAsInt.Convert().To<int>();

            //Test boxed bool conversion to bool,
            var boxedBoolToBool = boxedIntAsBool.Convert().To<bool>();



            //test boxed int conversion to bool
            // this works, as the first call to the converter throw a coercion exception and fellback to system.convert.
            var unboxedBoolAsIntToBool = boxedBoolAsInt.Convert().To<int>().Convert().To<bool>();
            // this won't even call the conversion function. Throws an exception trying to call the converter
            var boxedIntToBool = boxedBoolAsInt.Convert().To<bool>();

            //test boxed bool conversion to int
            var boxedBoolToInt = boxedIntAsBool.Convert().To<int>();
            var boxedBoolToInt2 = boxedIntAsBool.Convert().To<int>();

            stack.Push(true);
            var boxedBool = stack.Pop();
            var unboxed = boxedBool.Convert().To<int>();
            var unboxedFromType = boxedBool.Convert().To(typeof(int));
            var unboxedToIConvertible = unboxed.ChangeType<bool>();
            var ub2 = unboxed.Convert().To<bool>();
            var ub3 = unboxed.Convert().To<bool>();
           
            var seed = 1;
            var seedBool = seed.Convert().To<bool>();
            var seedBoolNull = seed.To<bool?>();
            var seedByte = seedBoolNull.Convert().To<byte>();
            var seedByteNull = seedByte.Convert().To(typeof(byte?));
            var seedSByte = seedByteNull.To<sbyte>();
            var seedSByteNull = seedSByte.To(typeof(sbyte?));
            var seedShort = seedSByteNull.To<short>();
            var seedShortNull = seedSByteNull.To<short?>();
            var seedInt = seedShortNull.To<int>();
            var seedIntNull = seedInt.To<int?>();
            var seedUInt = seedInt.To<uint>();
            var seedUIntNull = seedIntNull.To<uint?>();
            var seedLong = seedUIntNull.To<long>();
            var seedLongNull = seedUIntNull.To<long?>();
            var seedULong = seedLongNull.To<ulong>();
            var seedULongNull = seedULong.To<ulong?>();
            var seedSingle = seedULong.To<float>();
            var seedSingleNull = seedULong.To<float?>();
            var seedDouble = seedULong.To<double>();
            var seedDoubleNull = seedULong.To<double?>();
            var seedDecimal = seedULong.To<decimal>();
            var seedDecimalNull = seedULong.To<decimal?>();
            var seedChar = seedDecimalNull.To<char>();
            var seedCharNull = seedDecimalNull.To<char?>();

         


        }

        [Fact]
        public void TestConvertArray()
        {
            var seed = new[] { 1, 2, 4, 5, 6 };
            var seedBool = seed.Convert().To<bool[]>();
            var seedBoolNull = seed.To<bool?[]>();
            var seedByte = seedBoolNull.Convert().To<byte[]>();
            var seedByteNull = seedByte.Convert().To(typeof(byte?[]));
            var seedSByte = seedByteNull.To<sbyte[]>();
            var seedSByteNull = seedSByte.To(typeof(sbyte?[]));
            var seedShort = seedSByteNull.To<short[]>();
            var seedShortNull = seedSByteNull.To<short?[]>();
            var seedInt = seedShortNull.To<int[]>();
            var seedIntNull = seedInt.To<int?[]>();
            var seedUInt = seedInt.To<uint[]>();
            var seedUIntNull = seedIntNull.To<uint?[]>();
            var seedLong = seedUIntNull.To<long[]>();
            var seedLongNull = seedUIntNull.To<long?[]>();
            var seedULong = seedLongNull.To<ulong[]>();
            var seedULongNull = seedULong.To<ulong?[]>();
            var seedSingle = seedULong.To<float[]>();
            var seedSingleNull = seedULong.To<float?[]>();
            var seedDouble = seedULong.To<double[]>();
            var seedDoubleNull = seedULong.To<double?[]>();
            var seedDecimal = seedULong.To<decimal[]>();
            var seedDecimalNull = seedULong.To<decimal?[]>();
            var seedChar = seedDecimalNull.To<char[]>();
            var seedCharNull = seedDecimalNull.To<char?[]>();
        }
    }
}
