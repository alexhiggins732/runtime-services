using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;
using Xunit;
using System.Linq;

namespace System.Runtime.ConversionServices.Tests
{
    public static class ArrayWrapper
    {
        public static ArrayWrapper<T> Wrapper<T>(this IEnumerable<T> data) => new ArrayWrapper<T>(data);
    }
    public class ArrayWrapper<T> : IArrayWrapper
    {
        public T[] Data;
        public object[] BoxedData => Data.Select(x => (object)x).ToArray();
        public ArrayWrapper(T[] data) => this.Data = data;
        public ArrayWrapper(IEnumerable<T> data) => this.Data = data.ToArray();
    }
    public interface IArrayWrapper
    {
        object[] BoxedData { get; }

    }
    public class CompareExtensionTests
    {



    


        public static IEnumerable<int> SeedData = new[] { 0, 1, 2, 3 };
        public static IEnumerable<char> CharConvertTests = SeedData.To<char[]>();
        public static IEnumerable<sbyte> SByteConvertTests = SeedData.To<sbyte[]>();
        public static IEnumerable<byte> ByteConvertTests = SeedData.To<byte[]>();
        public static IEnumerable<short> ShortConvertTests = SeedData.To<short[]>();
        public static IEnumerable<ushort> UShortConvertTests = SeedData.To<ushort[]>();
        public static IEnumerable<int> IntConvertTests = SeedData.To<int[]>();
        public static IEnumerable<uint> UIntConvertTests = SeedData.To<uint[]>();
        public static IEnumerable<long> LongConvertTests = SeedData.To<long[]>();
        public static IEnumerable<ulong> ULongConvertTests = SeedData.To<ulong[]>();
        public static IEnumerable<float> FloatConvertTests = SeedData.To<float[]>();
        public static IEnumerable<double> DoubleConvertTests = SeedData.To<double[]>();
        public static IEnumerable<decimal> DecimalConvertTests = SeedData.To<decimal[]>();



        public static IEnumerable<object[]> GetTestData()
        {
            var a = new object[][]
            {
                new object [] { CharConvertTests.Wrapper() },
                new object [] { SByteConvertTests.Wrapper() },
                new object [] { ByteConvertTests.Wrapper() },
                new object [] { ShortConvertTests.Wrapper() },
                new object [] { UShortConvertTests.Wrapper() },
                new object [] { IntConvertTests.Wrapper() },
                new object [] { UIntConvertTests.Wrapper() },
                new object [] { LongConvertTests.Wrapper() },
                new object [] { ULongConvertTests.Wrapper() },
                new object [] { FloatConvertTests.Wrapper() },
                new object [] { DoubleConvertTests.Wrapper() },
                new object [] { DecimalConvertTests.Wrapper() },
            };
            return a;
        }

        public static IEnumerable<object[]> AllTests => GetTestData();


        [Theory]
        [MemberData(nameof(AllTests))]
        public void TestOrderedCompareLessThan(IArrayWrapper wrapper)
        {
            var array = wrapper.BoxedData;
            for (var i = 0; i < array.Length - 1; i++)
            {
                var current = array[i];
                var next = array[i + 1];
                bool result = current.Compare().LessThan(next);
                Assert.True(result);
            }

        }

        [Theory]
        [MemberData(nameof(AllTests))]
        public void TestOrderedCompareLessThanOrEqual(IArrayWrapper wrapper)
        {
            var array = wrapper.BoxedData;
            for (var i = 0; i < array.Length - 1; i++)
            {
                var current = array[i];
                var next = array[i];
                bool resultGreaterThan = current.Compare().LessThanOrEqual(next);
                Assert.True(resultGreaterThan);
                bool resultGreaterThanOrEqualCurrent = current.Compare().LessThanOrEqual(current);
                Assert.True(resultGreaterThan);
                bool resultGreaterThanOrEqualNext = next.Compare().LessThanOrEqual(next);
                Assert.True(resultGreaterThan);
            }
        }

        [Theory]
        [MemberData(nameof(AllTests))]
        public void TestOrderedCompareEquals(IArrayWrapper wrapper)
        {
            var array = wrapper.BoxedData;
            for (var i = 0; i < array.Length; i++)
            {
                var current = array[i];

                bool result = ((object)current).Compare().Equals((object)current);
                int compare = ((object)current).Compare().Compare((object)current);
                Assert.True(result);
                Assert.True(compare == 0);

            }
        }

        [Theory]
        [MemberData(nameof(AllTests))]
        public void TestOrderedCompareGreaterThan(IArrayWrapper wrapper)
        {
            var array = wrapper.BoxedData;
            for (var i = array.Length - 1; i > 0; i--)
            {
                var current = array[i];
                var next = array[i - 1];
                bool result = current.Compare().GreaterThan(next);
                Assert.True(result);
            }
        }

        [Theory]
        [MemberData(nameof(AllTests))]
        public void TestOrderedCompareGreaterThanEqualOr(IArrayWrapper wrapper)
        {
            var array = wrapper.BoxedData;
            for (var i = array.Length - 1; i > 0; i--)
            {
                var current = array[i];
                var next = array[i - 1];
                bool resultGreaterThan = current.Compare().GreaterThanOrEqual(next);
                Assert.True(resultGreaterThan);
                bool resultGreaterThanOrEqualCurrent = current.Compare().GreaterThanOrEqual(current);
                Assert.True(resultGreaterThan);
                bool resultGreaterThanOrEqualNext = next.Compare().GreaterThanOrEqual(next);
                Assert.True(resultGreaterThan);
            }
        }

    }
}
