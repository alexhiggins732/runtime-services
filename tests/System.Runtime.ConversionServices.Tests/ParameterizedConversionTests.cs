using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Xunit;
using static System.Runtime.ConversionServices.Conversions;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;
using static System.Runtime.ConversionServices.TypeConstants;

namespace System.Runtime.ConversionServices.Tests
{
    public class ParameterizedConversionTests
    {
        private readonly ITestOutputHelper output;

        public ParameterizedConversionTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        public class TestTypes
        {
            public const string Default = nameof(Default);
            public const string Min = nameof(Min);
            public const string Max = nameof(Max);
            public static readonly string[] AllTests = new string[] { Default, Min, Max };
        }

        public enum TestType
        {
            Default,
            Min,
            Max
        }

        public enum TestResult
        {
            None,
            InvalidCastExecption,
            NullResult,
        }

        public enum TestDelegateType
        {
            BoxedToBoxed,
            GenericToBoxed,
            BoxedToGeneric,
            GenericToGeneric
        }

        public struct TestDelegates
        {
            public static void BoxedToBoxedTest<TOut>(object src, Type destType, TOut expected)
            {
                var actual = src.To(destType);
                var equatable = (IEquatable<TOut>)actual;
                var equals = equatable.Equals(expected);
                var errorMessage = equals ? null : $"Boxed{src.GetType().Name}ToBoxed{typeof(TOut).Name} Failed =>" +
                    $" {nameof(src)}: {src}, {nameof(expected)}:{expected}, {nameof(actual)}:{actual},";
                Assert.Null(errorMessage);
                Assert.True(equals,
                    $"Boxed{src.GetType().Name}ToBoxed{typeof(TOut).Name} Failed =>" +
                    $" {nameof(src)}: {src}, {nameof(expected)}:{expected}, {nameof(actual)}:{actual},");
            }

            public static void BoxedToGenericTest<TOut>(object src, TOut expected)
            {
                var actual = src.To<TOut>();
                var equatable = (IEquatable<TOut>)actual;
                var equals = equatable.Equals(expected);
                var errorMessage = equals ? null : $"Boxed{src.GetType().Name}ToGeneric{typeof(TOut).Name} Failed =>" +
                    $" {nameof(src)}: {src}, {nameof(expected)}:{expected}, {nameof(actual)}:{actual},";
                Assert.Null(errorMessage);
                Assert.True(equals,
                    $"Boxed{src.GetType().Name}ToGeneric{typeof(TOut).Name} Failed =>" +
                    $" {nameof(src)}: {src}, {nameof(expected)}:{expected}, {nameof(actual)}:{actual},");
            }

            public static void GenericToBoxedTest<TIn, TOut>(TIn src, Type destType, TOut expected)
            {
                var actual = src.Convert().To(destType);
                var equatable = (IEquatable<TOut>)actual;
                var equals = equatable.Equals(expected);
                var errorMessage = equals ? null : $"Generic{typeof(TIn).Name}ToBoxed{typeof(TOut).Name} Failed =>" +
                    $" {nameof(src)}: {src}, {nameof(expected)}:{expected}, {nameof(actual)}:{actual},";
                Assert.Null(errorMessage);
                Assert.True(equals,
                    $"Generic{typeof(TIn).Name}ToBoxed{typeof(TOut).Name} Failed =>" +
                    $" {nameof(src)}: {src}, {nameof(expected)}:{expected}, {nameof(actual)}:{actual},");
            }

            public static void GenericToGenericTest<TIn, TOut>(TIn src, TOut expected)
            {
                var actual = src.To<TIn, TOut>();
                var equatable = (IEquatable<TOut>)actual;
                var equals = equatable.Equals(expected);
                var errorMessage = equals ? null : $"Generic{typeof(TIn).Name}ToGeneric{typeof(TOut).Name} Failed =>" +
                    $" {nameof(src)}: {src}, {nameof(expected)}:{expected}, {nameof(actual)}:{actual},";
                Assert.Null(errorMessage);
                Assert.True(equals,
                    $"Generic{typeof(TIn).Name}ToGeneric{typeof(TOut).Name} Failed =>" +
                    $" {nameof(src)}: {src}, {nameof(expected)}:{expected}, {nameof(actual)}:{actual},");
            }
        }

        public class ConvertTest
        {
            public static ConvertTest<TIn, TOut> Create<TIn, TOut>(TIn src, TOut dest)
                   => new ConvertTest<TIn, TOut>(src, dest);
        }

        public interface IConvertTest { void Run(); }

        public class ConvertTest<TIn, TOut> : IConvertTest
        {
            TIn Src;
            TOut Expected;
            Type DestType;
            public ConvertTest(TIn src, TOut expected)
            {
                this.Src = src;
                this.DestType = typeof(TOut);
                this.Expected = expected;
            }
            public void Run()
            {
                TestDelegates.BoxedToBoxedTest((object)Src, DestType, Expected);
                TestDelegates.BoxedToGenericTest((object)Src, Expected);
                TestDelegates.GenericToBoxedTest(Src, DestType, Expected);
                TestDelegates.GenericToGenericTest(Src, Expected);
            }
            public override string ToString() =>
                $"ConvertTest<{typeof(TIn).Name},{typeof(TOut).Name}> ({nameof(Src)}: {Src}, {nameof(Expected)}: {Expected})";
        }

  

        public static IEnumerable<IConvertTest> BoolConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(Bool.Default, Bool.Default),
                ConvertTest.Create(Bool.Default, Bool.Char.Default),
                ConvertTest.Create(Bool.Default, Bool.SByte.Default),
                ConvertTest.Create(Bool.Default, Bool.Byte.Default),
                ConvertTest.Create(Bool.Default, Bool.Short.Default),
                ConvertTest.Create(Bool.Default, Bool.UShort.Default),
                ConvertTest.Create(Bool.Default, Bool.Int.Default),
                ConvertTest.Create(Bool.Default, Bool.UInt.Default),
                ConvertTest.Create(Bool.Default, Bool.Long.Default),
                ConvertTest.Create(Bool.Default, Bool.ULong.Default),
                ConvertTest.Create(Bool.Default, Bool.Float.Default),
                ConvertTest.Create(Bool.Default, Bool.Double.Default),
                ConvertTest.Create(Bool.Default, Bool.Decimal.Default),
                ConvertTest.Create(Bool.Max, Bool.Max),
                ConvertTest.Create(Bool.Max, Bool.Char.Max),
                ConvertTest.Create(Bool.Max, Bool.SByte.Max),
                ConvertTest.Create(Bool.Max, Bool.Byte.Max),
                ConvertTest.Create(Bool.Max, Bool.Short.Max),
                ConvertTest.Create(Bool.Max, Bool.UShort.Max),
                ConvertTest.Create(Bool.Max, Bool.Int.Max),
                ConvertTest.Create(Bool.Max, Bool.UInt.Max),
                ConvertTest.Create(Bool.Max, Bool.Long.Max),
                ConvertTest.Create(Bool.Max, Bool.ULong.Max),
                ConvertTest.Create(Bool.Max, Bool.Float.Max),
                ConvertTest.Create(Bool.Max, Bool.Double.Max),
                ConvertTest.Create(Bool.Max, Bool.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> CharConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TypeConstants.Char.Default, TypeConstants.Char.Bool.Default),
                ConvertTest.Create(TypeConstants.Char.Default, TypeConstants.Char.Default),
                ConvertTest.Create(TypeConstants.Char.Default, TypeConstants.Char.Byte.Default),
                ConvertTest.Create(TypeConstants.Char.Default, TypeConstants.Char.SByte.Default),
                ConvertTest.Create(TypeConstants.Char.Default, TypeConstants.Char.Short.Default),
                ConvertTest.Create(TypeConstants.Char.Default, TypeConstants.Char.UShort.Default),
                ConvertTest.Create(TypeConstants.Char.Default, TypeConstants.Char.Int.Default),
                ConvertTest.Create(TypeConstants.Char.Default, TypeConstants.Char.UInt.Default),
                ConvertTest.Create(TypeConstants.Char.Default, TypeConstants.Char.Long.Default),
                ConvertTest.Create(TypeConstants.Char.Default, TypeConstants.Char.ULong.Default),
                ConvertTest.Create(TypeConstants.Char.Default, TypeConstants.Char.Float.Default),
                ConvertTest.Create(TypeConstants.Char.Default, TypeConstants.Char.Double.Default),
                ConvertTest.Create(TypeConstants.Char.Default, TypeConstants.Char.Decimal.Default),

                ConvertTest.Create(TypeConstants.Char.Min, TypeConstants.Char.Bool.Min),
                ConvertTest.Create(TypeConstants.Char.Min, TypeConstants.Char.Min),
                ConvertTest.Create(TypeConstants.Char.Min, TypeConstants.Char.Byte.Min),
                ConvertTest.Create(TypeConstants.Char.Min, TypeConstants.Char.SByte.Min),
                ConvertTest.Create(TypeConstants.Char.Min, TypeConstants.Char.Short.Min),
                ConvertTest.Create(TypeConstants.Char.Min, TypeConstants.Char.UShort.Min),
                ConvertTest.Create(TypeConstants.Char.Min, TypeConstants.Char.Int.Min),
                ConvertTest.Create(TypeConstants.Char.Min, TypeConstants.Char.UInt.Min),
                ConvertTest.Create(TypeConstants.Char.Min, TypeConstants.Char.Long.Min),
                ConvertTest.Create(TypeConstants.Char.Min, TypeConstants.Char.ULong.Min),
                ConvertTest.Create(TypeConstants.Char.Min, TypeConstants.Char.Float.Min),
                ConvertTest.Create(TypeConstants.Char.Min, TypeConstants.Char.Double.Min),
                ConvertTest.Create(TypeConstants.Char.Min, TypeConstants.Char.Decimal.Min),

                ConvertTest.Create(TypeConstants.Char.Max, TypeConstants.Char.Bool.Max),
                ConvertTest.Create(TypeConstants.Char.Max, TypeConstants.Char.Max),
                ConvertTest.Create(TypeConstants.Char.Max, TypeConstants.Char.SByte.Max),
                ConvertTest.Create(TypeConstants.Char.Max, TypeConstants.Char.Byte.Max),
                ConvertTest.Create(TypeConstants.Char.Max, TypeConstants.Char.Short.Max),
                ConvertTest.Create(TypeConstants.Char.Max, TypeConstants.Char.UShort.Max),
                ConvertTest.Create(TypeConstants.Char.Max, TypeConstants.Char.Int.Max),
                ConvertTest.Create(TypeConstants.Char.Max, TypeConstants.Char.UInt.Max),
                ConvertTest.Create(TypeConstants.Char.Max, TypeConstants.Char.Long.Max),
                ConvertTest.Create(TypeConstants.Char.Max, TypeConstants.Char.ULong.Max),
                ConvertTest.Create(TypeConstants.Char.Max, TypeConstants.Char.Float.Max),
                ConvertTest.Create(TypeConstants.Char.Max, TypeConstants.Char.Double.Max),
                ConvertTest.Create(TypeConstants.Char.Max, TypeConstants.Char.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> SByteConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TypeConstants.SByte.Default, TypeConstants.SByte.Bool.Default),
                ConvertTest.Create(TypeConstants.SByte.Default, TypeConstants.SByte.Char.Default),
                ConvertTest.Create(TypeConstants.SByte.Default, TypeConstants.SByte.Default),
                ConvertTest.Create(TypeConstants.SByte.Default, TypeConstants.SByte.Byte.Default),
                ConvertTest.Create(TypeConstants.SByte.Default, TypeConstants.SByte.Short.Default),
                ConvertTest.Create(TypeConstants.SByte.Default, TypeConstants.SByte.UShort.Default),
                ConvertTest.Create(TypeConstants.SByte.Default, TypeConstants.SByte.Int.Default),
                ConvertTest.Create(TypeConstants.SByte.Default, TypeConstants.SByte.UInt.Default),
                ConvertTest.Create(TypeConstants.SByte.Default, TypeConstants.SByte.Long.Default),
                ConvertTest.Create(TypeConstants.SByte.Default, TypeConstants.SByte.ULong.Default),
                ConvertTest.Create(TypeConstants.SByte.Default, TypeConstants.SByte.Float.Default),
                ConvertTest.Create(TypeConstants.SByte.Default, TypeConstants.SByte.Double.Default),
                ConvertTest.Create(TypeConstants.SByte.Default, TypeConstants.SByte.Decimal.Default),

                ConvertTest.Create(TypeConstants.SByte.Min, TypeConstants.SByte.Bool.Min),
                ConvertTest.Create(TypeConstants.SByte.Min, TypeConstants.SByte.Char.Min),
                ConvertTest.Create(TypeConstants.SByte.Min, TypeConstants.SByte.Min),
                ConvertTest.Create(TypeConstants.SByte.Min, TypeConstants.SByte.Byte.Min),
                ConvertTest.Create(TypeConstants.SByte.Min, TypeConstants.SByte.Short.Min),
                ConvertTest.Create(TypeConstants.SByte.Min, TypeConstants.SByte.UShort.Min),
                ConvertTest.Create(TypeConstants.SByte.Min, TypeConstants.SByte.Int.Min),
                ConvertTest.Create(TypeConstants.SByte.Min, TypeConstants.SByte.UInt.Min),
                ConvertTest.Create(TypeConstants.SByte.Min, TypeConstants.SByte.Long.Min),
                ConvertTest.Create(TypeConstants.SByte.Min, TypeConstants.SByte.ULong.Min),
                ConvertTest.Create(TypeConstants.SByte.Min, TypeConstants.SByte.Float.Min),
                ConvertTest.Create(TypeConstants.SByte.Min, TypeConstants.SByte.Double.Min),
                ConvertTest.Create(TypeConstants.SByte.Min, TypeConstants.SByte.Decimal.Min),

                ConvertTest.Create(TypeConstants.SByte.Max, TypeConstants.SByte.Bool.Max),
                ConvertTest.Create(TypeConstants.SByte.Max, TypeConstants.SByte.Char.Max),
                ConvertTest.Create(TypeConstants.SByte.Max, TypeConstants.SByte.Max),
                ConvertTest.Create(TypeConstants.SByte.Max, TypeConstants.SByte.Byte.Max),
                ConvertTest.Create(TypeConstants.SByte.Max, TypeConstants.SByte.Short.Max),
                ConvertTest.Create(TypeConstants.SByte.Max, TypeConstants.SByte.UShort.Max),
                ConvertTest.Create(TypeConstants.SByte.Max, TypeConstants.SByte.Int.Max),
                ConvertTest.Create(TypeConstants.SByte.Max, TypeConstants.SByte.UInt.Max),
                ConvertTest.Create(TypeConstants.SByte.Max, TypeConstants.SByte.Long.Max),
                ConvertTest.Create(TypeConstants.SByte.Max, TypeConstants.SByte.ULong.Max),
                ConvertTest.Create(TypeConstants.SByte.Max, TypeConstants.SByte.Float.Max),
                ConvertTest.Create(TypeConstants.SByte.Max, TypeConstants.SByte.Double.Max),
                ConvertTest.Create(TypeConstants.SByte.Max, TypeConstants.SByte.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> ByteConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TypeConstants.Byte.Default, TypeConstants.Byte.Bool.Default),
                ConvertTest.Create(TypeConstants.Byte.Default, TypeConstants.Byte.Char.Default),
                ConvertTest.Create(TypeConstants.Byte.Default, TypeConstants.Byte.SByte.Default),
                ConvertTest.Create(TypeConstants.Byte.Default, TypeConstants.Byte.Default),
                ConvertTest.Create(TypeConstants.Byte.Default, TypeConstants.Byte.Short.Default),
                ConvertTest.Create(TypeConstants.Byte.Default, TypeConstants.Byte.UShort.Default),
                ConvertTest.Create(TypeConstants.Byte.Default, TypeConstants.Byte.Int.Default),
                ConvertTest.Create(TypeConstants.Byte.Default, TypeConstants.Byte.UInt.Default),
                ConvertTest.Create(TypeConstants.Byte.Default, TypeConstants.Byte.Long.Default),
                ConvertTest.Create(TypeConstants.Byte.Default, TypeConstants.Byte.ULong.Default),
                ConvertTest.Create(TypeConstants.Byte.Default, TypeConstants.Byte.Float.Default),
                ConvertTest.Create(TypeConstants.Byte.Default, TypeConstants.Byte.Double.Default),
                ConvertTest.Create(TypeConstants.Byte.Default, TypeConstants.Byte.Decimal.Default),

                ConvertTest.Create(TypeConstants.Byte.Min, TypeConstants.Byte.Bool.Min),
                ConvertTest.Create(TypeConstants.Byte.Min, TypeConstants.Byte.Char.Min),
                ConvertTest.Create(TypeConstants.Byte.Min, TypeConstants.Byte.SByte.Min),
                ConvertTest.Create(TypeConstants.Byte.Min, TypeConstants.Byte.Min),
                ConvertTest.Create(TypeConstants.Byte.Min, TypeConstants.Byte.Short.Min),
                ConvertTest.Create(TypeConstants.Byte.Min, TypeConstants.Byte.UShort.Min),
                ConvertTest.Create(TypeConstants.Byte.Min, TypeConstants.Byte.Int.Min),
                ConvertTest.Create(TypeConstants.Byte.Min, TypeConstants.Byte.UInt.Min),
                ConvertTest.Create(TypeConstants.Byte.Min, TypeConstants.Byte.Long.Min),
                ConvertTest.Create(TypeConstants.Byte.Min, TypeConstants.Byte.ULong.Min),
                ConvertTest.Create(TypeConstants.Byte.Min, TypeConstants.Byte.Float.Min),
                ConvertTest.Create(TypeConstants.Byte.Min, TypeConstants.Byte.Double.Min),
                ConvertTest.Create(TypeConstants.Byte.Min, TypeConstants.Byte.Decimal.Min),

                ConvertTest.Create(TypeConstants.Byte.Max, TypeConstants.Byte.Bool.Max),
                ConvertTest.Create(TypeConstants.Byte.Max, TypeConstants.Byte.Char.Max),
                ConvertTest.Create(TypeConstants.Byte.Max, TypeConstants.Byte.SByte.Max),
                ConvertTest.Create(TypeConstants.Byte.Max, TypeConstants.Byte.Max),
                ConvertTest.Create(TypeConstants.Byte.Max, TypeConstants.Byte.Short.Max),
                ConvertTest.Create(TypeConstants.Byte.Max, TypeConstants.Byte.UShort.Max),
                ConvertTest.Create(TypeConstants.Byte.Max, TypeConstants.Byte.Int.Max),
                ConvertTest.Create(TypeConstants.Byte.Max, TypeConstants.Byte.UInt.Max),
                ConvertTest.Create(TypeConstants.Byte.Max, TypeConstants.Byte.Long.Max),
                ConvertTest.Create(TypeConstants.Byte.Max, TypeConstants.Byte.ULong.Max),
                ConvertTest.Create(TypeConstants.Byte.Max, TypeConstants.Byte.Float.Max),
                ConvertTest.Create(TypeConstants.Byte.Max, TypeConstants.Byte.Double.Max),
                ConvertTest.Create(TypeConstants.Byte.Max, TypeConstants.Byte.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> ShortConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TypeConstants.Short.Default, TypeConstants.Short.Bool.Default),
                ConvertTest.Create(TypeConstants.Short.Default, TypeConstants.Short.Char.Default),
                ConvertTest.Create(TypeConstants.Short.Default, TypeConstants.Short.SByte.Default),
                ConvertTest.Create(TypeConstants.Short.Default, TypeConstants.Short.Byte.Default),
                ConvertTest.Create(TypeConstants.Short.Default, TypeConstants.Short.Default),
                ConvertTest.Create(TypeConstants.Short.Default, TypeConstants.Short.UShort.Default),
                ConvertTest.Create(TypeConstants.Short.Default, TypeConstants.Short.Int.Default),
                ConvertTest.Create(TypeConstants.Short.Default, TypeConstants.Short.UInt.Default),
                ConvertTest.Create(TypeConstants.Short.Default, TypeConstants.Short.Long.Default),
                ConvertTest.Create(TypeConstants.Short.Default, TypeConstants.Short.ULong.Default),
                ConvertTest.Create(TypeConstants.Short.Default, TypeConstants.Short.Float.Default),
                ConvertTest.Create(TypeConstants.Short.Default, TypeConstants.Short.Double.Default),
                ConvertTest.Create(TypeConstants.Short.Default, TypeConstants.Short.Decimal.Default),

                ConvertTest.Create(TypeConstants.Short.Min, TypeConstants.Short.Bool.Min),
                ConvertTest.Create(TypeConstants.Short.Min, TypeConstants.Short.Char.Min),
                ConvertTest.Create(TypeConstants.Short.Min, TypeConstants.Short.SByte.Min),
                ConvertTest.Create(TypeConstants.Short.Min, TypeConstants.Short.Byte.Min),
                ConvertTest.Create(TypeConstants.Short.Min, TypeConstants.Short.Min),
                ConvertTest.Create(TypeConstants.Short.Min, TypeConstants.Short.UShort.Min),
                ConvertTest.Create(TypeConstants.Short.Min, TypeConstants.Short.Int.Min),
                ConvertTest.Create(TypeConstants.Short.Min, TypeConstants.Short.UInt.Min),
                ConvertTest.Create(TypeConstants.Short.Min, TypeConstants.Short.Long.Min),
                ConvertTest.Create(TypeConstants.Short.Min, TypeConstants.Short.ULong.Min),
                ConvertTest.Create(TypeConstants.Short.Min, TypeConstants.Short.Float.Min),
                ConvertTest.Create(TypeConstants.Short.Min, TypeConstants.Short.Double.Min),
                ConvertTest.Create(TypeConstants.Short.Min, TypeConstants.Short.Decimal.Min),

                ConvertTest.Create(TypeConstants.Short.Max, TypeConstants.Short.Bool.Max),
                ConvertTest.Create(TypeConstants.Short.Max, TypeConstants.Short.Char.Max),
                ConvertTest.Create(TypeConstants.Short.Max, TypeConstants.Short.SByte.Max),
                ConvertTest.Create(TypeConstants.Short.Max, TypeConstants.Short.Byte.Max),
                ConvertTest.Create(TypeConstants.Short.Max, TypeConstants.Short.Max),
                ConvertTest.Create(TypeConstants.Short.Max, TypeConstants.Short.UShort.Max),
                ConvertTest.Create(TypeConstants.Short.Max, TypeConstants.Short.Int.Max),
                ConvertTest.Create(TypeConstants.Short.Max, TypeConstants.Short.UInt.Max),
                ConvertTest.Create(TypeConstants.Short.Max, TypeConstants.Short.Long.Max),
                ConvertTest.Create(TypeConstants.Short.Max, TypeConstants.Short.ULong.Max),
                ConvertTest.Create(TypeConstants.Short.Max, TypeConstants.Short.Float.Max),
                ConvertTest.Create(TypeConstants.Short.Max, TypeConstants.Short.Double.Max),
                ConvertTest.Create(TypeConstants.Short.Max, TypeConstants.Short.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> UShortConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TypeConstants.UShort.Default, TypeConstants.UShort.Bool.Default),
                ConvertTest.Create(TypeConstants.UShort.Default, TypeConstants.UShort.Char.Default),
                ConvertTest.Create(TypeConstants.UShort.Default, TypeConstants.UShort.SByte.Default),
                ConvertTest.Create(TypeConstants.UShort.Default, TypeConstants.UShort.Byte.Default),
                ConvertTest.Create(TypeConstants.UShort.Default, TypeConstants.UShort.Short.Default),
                ConvertTest.Create(TypeConstants.UShort.Default, TypeConstants.UShort.Default),
                ConvertTest.Create(TypeConstants.UShort.Default, TypeConstants.UShort.Int.Default),
                ConvertTest.Create(TypeConstants.UShort.Default, TypeConstants.UShort.UInt.Default),
                ConvertTest.Create(TypeConstants.UShort.Default, TypeConstants.UShort.Long.Default),
                ConvertTest.Create(TypeConstants.UShort.Default, TypeConstants.UShort.ULong.Default),
                ConvertTest.Create(TypeConstants.UShort.Default, TypeConstants.UShort.Float.Default),
                ConvertTest.Create(TypeConstants.UShort.Default, TypeConstants.UShort.Double.Default),
                ConvertTest.Create(TypeConstants.UShort.Default, TypeConstants.UShort.Decimal.Default),

                ConvertTest.Create(TypeConstants.UShort.Min, TypeConstants.UShort.Bool.Min),
                ConvertTest.Create(TypeConstants.UShort.Min, TypeConstants.UShort.Char.Min),
                ConvertTest.Create(TypeConstants.UShort.Min, TypeConstants.UShort.SByte.Min),
                ConvertTest.Create(TypeConstants.UShort.Min, TypeConstants.UShort.Byte.Min),
                ConvertTest.Create(TypeConstants.UShort.Min, TypeConstants.UShort.Short.Min),
                ConvertTest.Create(TypeConstants.UShort.Min, TypeConstants.UShort.Min),
                ConvertTest.Create(TypeConstants.UShort.Min, TypeConstants.UShort.Int.Min),
                ConvertTest.Create(TypeConstants.UShort.Min, TypeConstants.UShort.UInt.Min),
                ConvertTest.Create(TypeConstants.UShort.Min, TypeConstants.UShort.Long.Min),
                ConvertTest.Create(TypeConstants.UShort.Min, TypeConstants.UShort.ULong.Min),
                ConvertTest.Create(TypeConstants.UShort.Min, TypeConstants.UShort.Float.Min),
                ConvertTest.Create(TypeConstants.UShort.Min, TypeConstants.UShort.Double.Min),
                ConvertTest.Create(TypeConstants.UShort.Min, TypeConstants.UShort.Decimal.Min),

                ConvertTest.Create(TypeConstants.UShort.Max, TypeConstants.UShort.Bool.Max),
                ConvertTest.Create(TypeConstants.UShort.Max, TypeConstants.UShort.Char.Max),
                ConvertTest.Create(TypeConstants.UShort.Max, TypeConstants.UShort.SByte.Max),
                ConvertTest.Create(TypeConstants.UShort.Max, TypeConstants.UShort.Byte.Max),
                ConvertTest.Create(TypeConstants.UShort.Max, TypeConstants.UShort.Short.Max),
                ConvertTest.Create(TypeConstants.UShort.Max, TypeConstants.UShort.Max),
                ConvertTest.Create(TypeConstants.UShort.Max, TypeConstants.UShort.Int.Max),
                ConvertTest.Create(TypeConstants.UShort.Max, TypeConstants.UShort.UInt.Max),
                ConvertTest.Create(TypeConstants.UShort.Max, TypeConstants.UShort.Long.Max),
                ConvertTest.Create(TypeConstants.UShort.Max, TypeConstants.UShort.ULong.Max),
                ConvertTest.Create(TypeConstants.UShort.Max, TypeConstants.UShort.Float.Max),
                ConvertTest.Create(TypeConstants.UShort.Max, TypeConstants.UShort.Double.Max),
                ConvertTest.Create(TypeConstants.UShort.Max, TypeConstants.UShort.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> IntConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TypeConstants.Int.Default, TypeConstants.Int.Bool.Default),
                ConvertTest.Create(TypeConstants.Int.Default, TypeConstants.Int.Char.Default),
                ConvertTest.Create(TypeConstants.Int.Default, TypeConstants.Int.SByte.Default),
                ConvertTest.Create(TypeConstants.Int.Default, TypeConstants.Int.Byte.Default),
                ConvertTest.Create(TypeConstants.Int.Default, TypeConstants.Int.Short.Default),
                ConvertTest.Create(TypeConstants.Int.Default, TypeConstants.Int.UShort.Default),
                ConvertTest.Create(TypeConstants.Int.Default, TypeConstants.Int.Default),
                ConvertTest.Create(TypeConstants.Int.Default, TypeConstants.Int.UInt.Default),
                ConvertTest.Create(TypeConstants.Int.Default, TypeConstants.Int.Long.Default),
                ConvertTest.Create(TypeConstants.Int.Default, TypeConstants.Int.ULong.Default),
                ConvertTest.Create(TypeConstants.Int.Default, TypeConstants.Int.Float.Default),
                ConvertTest.Create(TypeConstants.Int.Default, TypeConstants.Int.Double.Default),
                ConvertTest.Create(TypeConstants.Int.Default, TypeConstants.Int.Decimal.Default),

                ConvertTest.Create(TypeConstants.Int.Min, TypeConstants.Int.Bool.Min),
                ConvertTest.Create(TypeConstants.Int.Min, TypeConstants.Int.Char.Min),
                ConvertTest.Create(TypeConstants.Int.Min, TypeConstants.Int.SByte.Min),
                ConvertTest.Create(TypeConstants.Int.Min, TypeConstants.Int.Byte.Min),
                ConvertTest.Create(TypeConstants.Int.Min, TypeConstants.Int.Short.Min),
                ConvertTest.Create(TypeConstants.Int.Min, TypeConstants.Int.UShort.Min),
                ConvertTest.Create(TypeConstants.Int.Min, TypeConstants.Int.Min),
                ConvertTest.Create(TypeConstants.Int.Min, TypeConstants.Int.UInt.Min),
                ConvertTest.Create(TypeConstants.Int.Min, TypeConstants.Int.Long.Min),
                ConvertTest.Create(TypeConstants.Int.Min, TypeConstants.Int.ULong.Min),
                ConvertTest.Create(TypeConstants.Int.Min, TypeConstants.Int.Float.Min),
                ConvertTest.Create(TypeConstants.Int.Min, TypeConstants.Int.Double.Min),
                ConvertTest.Create(TypeConstants.Int.Min, TypeConstants.Int.Decimal.Min),

                ConvertTest.Create(TypeConstants.Int.Max, TypeConstants.Int.Bool.Max),
                ConvertTest.Create(TypeConstants.Int.Max, TypeConstants.Int.Char.Max),
                ConvertTest.Create(TypeConstants.Int.Max, TypeConstants.Int.SByte.Max),
                ConvertTest.Create(TypeConstants.Int.Max, TypeConstants.Int.Byte.Max),
                ConvertTest.Create(TypeConstants.Int.Max, TypeConstants.Int.Short.Max),
                ConvertTest.Create(TypeConstants.Int.Max, TypeConstants.Int.UShort.Max),
                ConvertTest.Create(TypeConstants.Int.Max, TypeConstants.Int.Max),
                ConvertTest.Create(TypeConstants.Int.Max, TypeConstants.Int.UInt.Max),
                ConvertTest.Create(TypeConstants.Int.Max, TypeConstants.Int.Long.Max),
                ConvertTest.Create(TypeConstants.Int.Max, TypeConstants.Int.ULong.Max),
                ConvertTest.Create(TypeConstants.Int.Max, TypeConstants.Int.Float.Max),
                ConvertTest.Create(TypeConstants.Int.Max, TypeConstants.Int.Double.Max),
                ConvertTest.Create(TypeConstants.Int.Max, TypeConstants.Int.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> UIntConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TypeConstants.UInt.Default, TypeConstants.UInt.Bool.Default),
                ConvertTest.Create(TypeConstants.UInt.Default, TypeConstants.UInt.Char.Default),
                ConvertTest.Create(TypeConstants.UInt.Default, TypeConstants.UInt.SByte.Default),
                ConvertTest.Create(TypeConstants.UInt.Default, TypeConstants.UInt.Byte.Default),
                ConvertTest.Create(TypeConstants.UInt.Default, TypeConstants.UInt.Short.Default),
                ConvertTest.Create(TypeConstants.UInt.Default, TypeConstants.UInt.UShort.Default),
                ConvertTest.Create(TypeConstants.UInt.Default, TypeConstants.UInt.Int.Default),
                ConvertTest.Create(TypeConstants.UInt.Default, TypeConstants.UInt.Default),
                ConvertTest.Create(TypeConstants.UInt.Default, TypeConstants.UInt.Long.Default),
                ConvertTest.Create(TypeConstants.UInt.Default, TypeConstants.UInt.ULong.Default),
                ConvertTest.Create(TypeConstants.UInt.Default, TypeConstants.UInt.Float.Default),
                ConvertTest.Create(TypeConstants.UInt.Default, TypeConstants.UInt.Double.Default),
                ConvertTest.Create(TypeConstants.UInt.Default, TypeConstants.UInt.Decimal.Default),

                ConvertTest.Create(TypeConstants.UInt.Min, TypeConstants.UInt.Bool.Min),
                ConvertTest.Create(TypeConstants.UInt.Min, TypeConstants.UInt.Char.Min),
                ConvertTest.Create(TypeConstants.UInt.Min, TypeConstants.UInt.SByte.Min),
                ConvertTest.Create(TypeConstants.UInt.Min, TypeConstants.UInt.Byte.Min),
                ConvertTest.Create(TypeConstants.UInt.Min, TypeConstants.UInt.Short.Min),
                ConvertTest.Create(TypeConstants.UInt.Min, TypeConstants.UInt.UShort.Min),
                ConvertTest.Create(TypeConstants.UInt.Min, TypeConstants.UInt.Int.Min),
                ConvertTest.Create(TypeConstants.UInt.Min, TypeConstants.UInt.Min),
                ConvertTest.Create(TypeConstants.UInt.Min, TypeConstants.UInt.Long.Min),
                ConvertTest.Create(TypeConstants.UInt.Min, TypeConstants.UInt.ULong.Min),
                ConvertTest.Create(TypeConstants.UInt.Min, TypeConstants.UInt.Float.Min),
                ConvertTest.Create(TypeConstants.UInt.Min, TypeConstants.UInt.Double.Min),
                ConvertTest.Create(TypeConstants.UInt.Min, TypeConstants.UInt.Decimal.Min),

                ConvertTest.Create(TypeConstants.UInt.Max, TypeConstants.UInt.Bool.Max),
                ConvertTest.Create(TypeConstants.UInt.Max, TypeConstants.UInt.Char.Max),
                ConvertTest.Create(TypeConstants.UInt.Max, TypeConstants.UInt.SByte.Max),
                ConvertTest.Create(TypeConstants.UInt.Max, TypeConstants.UInt.Byte.Max),
                ConvertTest.Create(TypeConstants.UInt.Max, TypeConstants.UInt.Short.Max),
                ConvertTest.Create(TypeConstants.UInt.Max, TypeConstants.UInt.UShort.Max),
                ConvertTest.Create(TypeConstants.UInt.Max, TypeConstants.UInt.Int.Max),
                ConvertTest.Create(TypeConstants.UInt.Max, TypeConstants.UInt.Max),
                ConvertTest.Create(TypeConstants.UInt.Max, TypeConstants.UInt.Long.Max),
                ConvertTest.Create(TypeConstants.UInt.Max, TypeConstants.UInt.ULong.Max),
                ConvertTest.Create(TypeConstants.UInt.Max, TypeConstants.UInt.Float.Max),
                ConvertTest.Create(TypeConstants.UInt.Max, TypeConstants.UInt.Double.Max),
                ConvertTest.Create(TypeConstants.UInt.Max, TypeConstants.UInt.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> LongConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TypeConstants.Long.Default, TypeConstants.Long.Bool.Default),
                ConvertTest.Create(TypeConstants.Long.Default, TypeConstants.Long.Char.Default),
                ConvertTest.Create(TypeConstants.Long.Default, TypeConstants.Long.SByte.Default),
                ConvertTest.Create(TypeConstants.Long.Default, TypeConstants.Long.Byte.Default),
                ConvertTest.Create(TypeConstants.Long.Default, TypeConstants.Long.Short.Default),
                ConvertTest.Create(TypeConstants.Long.Default, TypeConstants.Long.UShort.Default),
                ConvertTest.Create(TypeConstants.Long.Default, TypeConstants.Long.Int.Default),
                ConvertTest.Create(TypeConstants.Long.Default, TypeConstants.Long.UInt.Default),
                ConvertTest.Create(TypeConstants.Long.Default, TypeConstants.Long.Default),
                ConvertTest.Create(TypeConstants.Long.Default, TypeConstants.Long.ULong.Default),
                ConvertTest.Create(TypeConstants.Long.Default, TypeConstants.Long.Float.Default),
                ConvertTest.Create(TypeConstants.Long.Default, TypeConstants.Long.Double.Default),
                ConvertTest.Create(TypeConstants.Long.Default, TypeConstants.Long.Decimal.Default),

                ConvertTest.Create(TypeConstants.Long.Min, TypeConstants.Long.Bool.Min),
                ConvertTest.Create(TypeConstants.Long.Min, TypeConstants.Long.Char.Min),
                ConvertTest.Create(TypeConstants.Long.Min, TypeConstants.Long.SByte.Min),
                ConvertTest.Create(TypeConstants.Long.Min, TypeConstants.Long.Byte.Min),
                ConvertTest.Create(TypeConstants.Long.Min, TypeConstants.Long.Short.Min),
                ConvertTest.Create(TypeConstants.Long.Min, TypeConstants.Long.UShort.Min),
                ConvertTest.Create(TypeConstants.Long.Min, TypeConstants.Long.Int.Min),
                ConvertTest.Create(TypeConstants.Long.Min, TypeConstants.Long.UInt.Min),
                ConvertTest.Create(TypeConstants.Long.Min, TypeConstants.Long.Min),
                ConvertTest.Create(TypeConstants.Long.Min, TypeConstants.Long.ULong.Min),
                ConvertTest.Create(TypeConstants.Long.Min, TypeConstants.Long.Float.Min),
                ConvertTest.Create(TypeConstants.Long.Min, TypeConstants.Long.Double.Min),
                ConvertTest.Create(TypeConstants.Long.Min, TypeConstants.Long.Decimal.Min),

                ConvertTest.Create(TypeConstants.Long.Max, TypeConstants.Long.Bool.Max),
                ConvertTest.Create(TypeConstants.Long.Max, TypeConstants.Long.Char.Max),
                ConvertTest.Create(TypeConstants.Long.Max, TypeConstants.Long.SByte.Max),
                ConvertTest.Create(TypeConstants.Long.Max, TypeConstants.Long.Byte.Max),
                ConvertTest.Create(TypeConstants.Long.Max, TypeConstants.Long.Short.Max),
                ConvertTest.Create(TypeConstants.Long.Max, TypeConstants.Long.UShort.Max),
                ConvertTest.Create(TypeConstants.Long.Max, TypeConstants.Long.Int.Max),
                ConvertTest.Create(TypeConstants.Long.Max, TypeConstants.Long.UInt.Max),
                ConvertTest.Create(TypeConstants.Long.Max, TypeConstants.Long.Max),
                ConvertTest.Create(TypeConstants.Long.Max, TypeConstants.Long.ULong.Max),
                ConvertTest.Create(TypeConstants.Long.Max, TypeConstants.Long.Float.Max),
                ConvertTest.Create(TypeConstants.Long.Max, TypeConstants.Long.Double.Max),
                ConvertTest.Create(TypeConstants.Long.Max, TypeConstants.Long.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> ULongConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TypeConstants.ULong.Default, TypeConstants.ULong.Bool.Default),
                ConvertTest.Create(TypeConstants.ULong.Default, TypeConstants.ULong.Char.Default),
                ConvertTest.Create(TypeConstants.ULong.Default, TypeConstants.ULong.SByte.Default),
                ConvertTest.Create(TypeConstants.ULong.Default, TypeConstants.ULong.SByte.Default),
                ConvertTest.Create(TypeConstants.ULong.Default, TypeConstants.ULong.Short.Default),
                ConvertTest.Create(TypeConstants.ULong.Default, TypeConstants.ULong.UShort.Default),
                ConvertTest.Create(TypeConstants.ULong.Default, TypeConstants.ULong.Int.Default),
                ConvertTest.Create(TypeConstants.ULong.Default, TypeConstants.ULong.UInt.Default),
                ConvertTest.Create(TypeConstants.ULong.Default, TypeConstants.ULong.Long.Default),
                ConvertTest.Create(TypeConstants.ULong.Default, TypeConstants.ULong.Default),
                ConvertTest.Create(TypeConstants.ULong.Default, TypeConstants.ULong.Float.Default),
                ConvertTest.Create(TypeConstants.ULong.Default, TypeConstants.ULong.Double.Default),
                ConvertTest.Create(TypeConstants.ULong.Default, TypeConstants.ULong.Decimal.Default),

                ConvertTest.Create(TypeConstants.ULong.Min, TypeConstants.ULong.Bool.Min),
                ConvertTest.Create(TypeConstants.ULong.Min, TypeConstants.ULong.Char.Min),
                ConvertTest.Create(TypeConstants.ULong.Min, TypeConstants.ULong.SByte.Min),
                ConvertTest.Create(TypeConstants.ULong.Min, TypeConstants.ULong.Byte.Min),
                ConvertTest.Create(TypeConstants.ULong.Min, TypeConstants.ULong.Short.Min),
                ConvertTest.Create(TypeConstants.ULong.Min, TypeConstants.ULong.UShort.Min),
                ConvertTest.Create(TypeConstants.ULong.Min, TypeConstants.ULong.Int.Min),
                ConvertTest.Create(TypeConstants.ULong.Min, TypeConstants.ULong.UInt.Min),
                ConvertTest.Create(TypeConstants.ULong.Min, TypeConstants.ULong.Long.Min),
                ConvertTest.Create(TypeConstants.ULong.Min, TypeConstants.ULong.Min),
                ConvertTest.Create(TypeConstants.ULong.Min, TypeConstants.ULong.Float.Min),
                ConvertTest.Create(TypeConstants.ULong.Min, TypeConstants.ULong.Double.Min),
                ConvertTest.Create(TypeConstants.ULong.Min, TypeConstants.ULong.Decimal.Min),

                ConvertTest.Create(TypeConstants.ULong.Max, TypeConstants.ULong.Bool.Max),
                ConvertTest.Create(TypeConstants.ULong.Max, TypeConstants.ULong.Char.Max),
                ConvertTest.Create(TypeConstants.ULong.Max, TypeConstants.ULong.SByte.Max),
                ConvertTest.Create(TypeConstants.ULong.Max, TypeConstants.ULong.Byte.Max),
                ConvertTest.Create(TypeConstants.ULong.Max, TypeConstants.ULong.Short.Max),
                ConvertTest.Create(TypeConstants.ULong.Max, TypeConstants.ULong.UShort.Max),
                ConvertTest.Create(TypeConstants.ULong.Max, TypeConstants.ULong.Int.Max),
                ConvertTest.Create(TypeConstants.ULong.Max, TypeConstants.ULong.UInt.Max),
                ConvertTest.Create(TypeConstants.ULong.Max, TypeConstants.ULong.Long.Max),
                ConvertTest.Create(TypeConstants.ULong.Max, TypeConstants.ULong.Max),
                ConvertTest.Create(TypeConstants.ULong.Max, TypeConstants.ULong.Float.Max),
                ConvertTest.Create(TypeConstants.ULong.Max, TypeConstants.ULong.Double.Max),
                ConvertTest.Create(TypeConstants.ULong.Max, TypeConstants.ULong.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> FloatConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TypeConstants.Float.Default, TypeConstants.Float.Bool.Default),
                ConvertTest.Create(TypeConstants.Float.Default, TypeConstants.Float.Char.Default),
                ConvertTest.Create(TypeConstants.Float.Default, TypeConstants.Float.SByte.Default),
                ConvertTest.Create(TypeConstants.Float.Default, TypeConstants.Float.Byte.Default),
                ConvertTest.Create(TypeConstants.Float.Default, TypeConstants.Float.Short.Default),
                ConvertTest.Create(TypeConstants.Float.Default, TypeConstants.Float.UShort.Default),
                ConvertTest.Create(TypeConstants.Float.Default, TypeConstants.Float.Int.Default),
                ConvertTest.Create(TypeConstants.Float.Default, TypeConstants.Float.UInt.Default),
                ConvertTest.Create(TypeConstants.Float.Default, TypeConstants.Float.Long.Default),
                ConvertTest.Create(TypeConstants.Float.Default, TypeConstants.Float.ULong.Default),
                ConvertTest.Create(TypeConstants.Float.Default, TypeConstants.Float.Default),
                ConvertTest.Create(TypeConstants.Float.Default, TypeConstants.Float.Double.Default),
                ConvertTest.Create(TypeConstants.Float.Default, TypeConstants.Float.Decimal.Default),

                ConvertTest.Create(TypeConstants.Float.Min, TypeConstants.Float.Bool.Min),
                ConvertTest.Create(TypeConstants.Float.Min, TypeConstants.Float.Char.Min),
                ConvertTest.Create(TypeConstants.Float.Min, TypeConstants.Float.SByte.Min),
                ConvertTest.Create(TypeConstants.Float.Min, TypeConstants.Float.Byte.Min),
                ConvertTest.Create(TypeConstants.Float.Min, TypeConstants.Float.Short.Min),
                ConvertTest.Create(TypeConstants.Float.Min, TypeConstants.Float.UShort.Min),
                ConvertTest.Create(TypeConstants.Float.Min, TypeConstants.Float.Int.Min),
                ConvertTest.Create(TypeConstants.Float.Min, TypeConstants.Float.UInt.Min),
                ConvertTest.Create(TypeConstants.Float.Min, TypeConstants.Float.Long.Min),
                ConvertTest.Create(TypeConstants.Float.Min, TypeConstants.Float.ULong.Min),
                ConvertTest.Create(TypeConstants.Float.Min, TypeConstants.Float.Min),
                ConvertTest.Create(TypeConstants.Float.Min, TypeConstants.Float.Double.Min),
                //TODO: Assert Throws
                //ConvertTest.Create(TestConstants.Float.Min, TestConstants.Float.Decimal.Min),

                ConvertTest.Create(TypeConstants.Float.Max, TypeConstants.Float.Bool.Max),
                ConvertTest.Create(TypeConstants.Float.Max, TypeConstants.Float.Char.Max),
                ConvertTest.Create(TypeConstants.Float.Max, TypeConstants.Float.SByte.Max),
                ConvertTest.Create(TypeConstants.Float.Max, TypeConstants.Float.Byte.Max),
                ConvertTest.Create(TypeConstants.Float.Max, TypeConstants.Float.Short.Max),
                ConvertTest.Create(TypeConstants.Float.Max, TypeConstants.Float.UShort.Max),
                ConvertTest.Create(TypeConstants.Float.Max, TypeConstants.Float.Int.Max),
                ConvertTest.Create(TypeConstants.Float.Max, TypeConstants.Float.UInt.Max),
                ConvertTest.Create(TypeConstants.Float.Max, TypeConstants.Float.Long.Max),
                ConvertTest.Create(TypeConstants.Float.Max, TypeConstants.Float.ULong.Max),
                ConvertTest.Create(TypeConstants.Float.Max, TypeConstants.Float.Max),
                ConvertTest.Create(TypeConstants.Float.Max, TypeConstants.Float.Double.Max),
                //TODO: Assert Throws
                //ConvertTest.Create(TestConstants.Float.Max, TestConstants.Float.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> DoubleConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TypeConstants.Double.Default, TypeConstants.Double.Bool.Default),
                ConvertTest.Create(TypeConstants.Double.Default, TypeConstants.Double.Char.Default),
                ConvertTest.Create(TypeConstants.Double.Default, TypeConstants.Double.SByte.Default),
                ConvertTest.Create(TypeConstants.Double.Default, TypeConstants.Double.Byte.Default),
                ConvertTest.Create(TypeConstants.Double.Default, TypeConstants.Double.Short.Default),
                ConvertTest.Create(TypeConstants.Double.Default, TypeConstants.Double.UShort.Default),
                ConvertTest.Create(TypeConstants.Double.Default, TypeConstants.Double.Int.Default),
                ConvertTest.Create(TypeConstants.Double.Default, TypeConstants.Double.UInt.Default),
                ConvertTest.Create(TypeConstants.Double.Default, TypeConstants.Double.Long.Default),
                ConvertTest.Create(TypeConstants.Double.Default, TypeConstants.Double.ULong.Default),
                ConvertTest.Create(TypeConstants.Double.Default, TypeConstants.Double.Float.Default),
                ConvertTest.Create(TypeConstants.Double.Default, TypeConstants.Double.Default),
                ConvertTest.Create(TypeConstants.Double.Default, TypeConstants.Double.Decimal.Default),

                ConvertTest.Create(TypeConstants.Double.Min, TypeConstants.Double.Bool.Min),
                ConvertTest.Create(TypeConstants.Double.Min, TypeConstants.Double.Char.Min),
                ConvertTest.Create(TypeConstants.Double.Min, TypeConstants.Double.SByte.Min),
                ConvertTest.Create(TypeConstants.Double.Min, TypeConstants.Double.Byte.Min),
                ConvertTest.Create(TypeConstants.Double.Min, TypeConstants.Double.Short.Min),
                ConvertTest.Create(TypeConstants.Double.Min, TypeConstants.Double.UShort.Min),
                ConvertTest.Create(TypeConstants.Double.Min, TypeConstants.Double.Int.Min),
                ConvertTest.Create(TypeConstants.Double.Min, TypeConstants.Double.UInt.Min),
                ConvertTest.Create(TypeConstants.Double.Min, TypeConstants.Double.Long.Min),
                ConvertTest.Create(TypeConstants.Double.Min, TypeConstants.Double.ULong.Min),
                ConvertTest.Create(TypeConstants.Double.Min, TypeConstants.Double.Float.Min),
                ConvertTest.Create(TypeConstants.Double.Min, TypeConstants.Double.Min),
                //TODO: Assert Throws
                //ConvertTest.Create(TestConstants.Double.Min, TestConstants.Double.Decimal.Min),

                ConvertTest.Create(TypeConstants.Double.Max, TypeConstants.Double.Bool.Max),
                ConvertTest.Create(TypeConstants.Double.Max, TypeConstants.Double.Char.Max),
                ConvertTest.Create(TypeConstants.Double.Max, TypeConstants.Double.SByte.Max),
                ConvertTest.Create(TypeConstants.Double.Max, TypeConstants.Double.Byte.Max),
                ConvertTest.Create(TypeConstants.Double.Max, TypeConstants.Double.Short.Max),
                ConvertTest.Create(TypeConstants.Double.Max, TypeConstants.Double.UShort.Max),
                ConvertTest.Create(TypeConstants.Double.Max, TypeConstants.Double.Int.Max),
                ConvertTest.Create(TypeConstants.Double.Max, TypeConstants.Double.UInt.Max),
                ConvertTest.Create(TypeConstants.Double.Max, TypeConstants.Double.Long.Max),
                ConvertTest.Create(TypeConstants.Double.Max, TypeConstants.Double.ULong.Max),
                ConvertTest.Create(TypeConstants.Double.Max, TypeConstants.Double.Float.Max),
                ConvertTest.Create(TypeConstants.Double.Max, TypeConstants.Double.Max),
                //TODO: Assert Throws
                //ConvertTest.Create(TestConstants.Double.Max, TestConstants.Double.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> DecimalConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TypeConstants.Decimal.Default, TypeConstants.Decimal.Bool.Default),
                ConvertTest.Create(TypeConstants.Decimal.Default, TypeConstants.Decimal.Char.Default),
                ConvertTest.Create(TypeConstants.Decimal.Default, TypeConstants.Decimal.SByte.Default),
                ConvertTest.Create(TypeConstants.Decimal.Default, TypeConstants.Decimal.Byte.Default),
                ConvertTest.Create(TypeConstants.Decimal.Default, TypeConstants.Decimal.Short.Default),
                ConvertTest.Create(TypeConstants.Decimal.Default, TypeConstants.Decimal.UShort.Default),
                ConvertTest.Create(TypeConstants.Decimal.Default, TypeConstants.Decimal.Int.Default),
                ConvertTest.Create(TypeConstants.Decimal.Default, TypeConstants.Decimal.UInt.Default),
                ConvertTest.Create(TypeConstants.Decimal.Default, TypeConstants.Decimal.Long.Default),
                ConvertTest.Create(TypeConstants.Decimal.Default, TypeConstants.Decimal.ULong.Default),
                ConvertTest.Create(TypeConstants.Decimal.Default, TypeConstants.Decimal.Float.Default),
                ConvertTest.Create(TypeConstants.Decimal.Default, TypeConstants.Decimal.Double.Default),
                ConvertTest.Create(TypeConstants.Decimal.Default, TypeConstants.Decimal.Default),

                ConvertTest.Create(TypeConstants.Decimal.Min, TypeConstants.Decimal.Bool.Min),
                //TODO: Assert Throws
                //ConvertTest.Create(TestConstants.Decimal.Min, TestConstants.Decimal.Char.Min),
                //ConvertTest.Create(TestConstants.Decimal.Min, TestConstants.Decimal.SByte.Min),
                //ConvertTest.Create(TestConstants.Decimal.Min, TestConstants.Decimal.Byte.Min),
                //ConvertTest.Create(TestConstants.Decimal.Min, TestConstants.Decimal.Short.Min),
                //ConvertTest.Create(TestConstants.Decimal.Min, TestConstants.Decimal.UShort.Min),
                //ConvertTest.Create(TestConstants.Decimal.Min, TestConstants.Decimal.Int.Min),
                //ConvertTest.Create(TestConstants.Decimal.Min, TestConstants.Decimal.UInt.Min),
                //ConvertTest.Create(TestConstants.Decimal.Min, TestConstants.Decimal.Long.Min),
                //ConvertTest.Create(TestConstants.Decimal.Min, TestConstants.Decimal.ULong.Min),
                ConvertTest.Create(TypeConstants.Decimal.Min, TypeConstants.Decimal.Float.Min),
                ConvertTest.Create(TypeConstants.Decimal.Min, TypeConstants.Decimal.Double.Min),
                ConvertTest.Create(TypeConstants.Decimal.Min, TypeConstants.Decimal.Min),

                ConvertTest.Create(TypeConstants.Decimal.Max, TypeConstants.Decimal.Bool.Max),
                //TODO: Assert Throws
                //ConvertTest.Create(TestConstants.Decimal.Max, TestConstants.Decimal.Char.Max),
                //ConvertTest.Create(TestConstants.Decimal.Max, TestConstants.Decimal.SByte.Max),
                //ConvertTest.Create(TestConstants.Decimal.Max, TestConstants.Decimal.Byte.Max),
                //ConvertTest.Create(TestConstants.Decimal.Max, TestConstants.Decimal.Short.Max),
                //ConvertTest.Create(TestConstants.Decimal.Max, TestConstants.Decimal.UShort.Max),
                //ConvertTest.Create(TestConstants.Decimal.Max, TestConstants.Decimal.Int.Max),
                //ConvertTest.Create(TestConstants.Decimal.Max, TestConstants.Decimal.UInt.Max),
                //ConvertTest.Create(TestConstants.Decimal.Max, TestConstants.Decimal.Long.Max),
                //ConvertTest.Create(TestConstants.Decimal.Max, TestConstants.Decimal.ULong.Max),
                ConvertTest.Create(TypeConstants.Decimal.Max, TypeConstants.Decimal.Float.Max),
                ConvertTest.Create(TypeConstants.Decimal.Max, TypeConstants.Decimal.Double.Max),
                ConvertTest.Create(TypeConstants.Decimal.Max, TypeConstants.Decimal.Max),
            };



        public static IEnumerable<object[]> BoolTestData => BoolConvertTests.Select(x => new object[] { x });
        public static IEnumerable<object[]> CharTestData => CharConvertTests.Select(x => new object[] { x });
        public static IEnumerable<object[]> SByteTestData => SByteConvertTests.Select(x => new object[] { x });
        public static IEnumerable<object[]> ByteTestData => ByteConvertTests.Select(x => new object[] { x });
        public static IEnumerable<object[]> ShortTestData => ShortConvertTests.Select(x => new object[] { x });
        public static IEnumerable<object[]> UShortTestData => UShortConvertTests.Select(x => new object[] { x });
        public static IEnumerable<object[]> IntTestData => IntConvertTests.Select(x => new object[] { x });
        public static IEnumerable<object[]> UIntTestData => UIntConvertTests.Select(x => new object[] { x });
        public static IEnumerable<object[]> LongTestData => LongConvertTests.Select(x => new object[] { x });
        public static IEnumerable<object[]> ULongTestData => ULongConvertTests.Select(x => new object[] { x });
        public static IEnumerable<object[]> FloatTestData => FloatConvertTests.Select(x => new object[] { x });
        public static IEnumerable<object[]> DoubleTestData => DoubleConvertTests.Select(x => new object[] { x });
        public static IEnumerable<object[]> DecimalTestData => DecimalConvertTests.Select(x => new object[] { x });



        [Theory]
        [MemberData(nameof(BoolTestData))]
        public void BoolTests(IConvertTest test)
        {
            test.Run();
        }

        [Theory]
        [MemberData(nameof(CharTestData))]
        public void CharTests(IConvertTest test)
        {
            test.Run();
        }

        [Theory]
        [MemberData(nameof(SByteTestData))]
        public void SByteTests(IConvertTest test)
        {
            test.Run();
        }

        [Theory]
        [MemberData(nameof(ByteTestData))]
        public void ByteTests(IConvertTest test)
        {
            test.Run();
        }

        [Theory]
        [MemberData(nameof(ShortTestData))]
        public void ShortTests(IConvertTest test)
        {
            test.Run();
        }

        [Theory]
        [MemberData(nameof(UShortTestData))]
        public void UShortTests(IConvertTest test)
        {
            test.Run();
        }

        [Theory]
        [MemberData(nameof(IntTestData))]
        public void IntTests(IConvertTest test)
        {
            test.Run();
        }

        [Theory]
        [MemberData(nameof(UIntTestData))]
        public void UIntTests(IConvertTest test)
        {
            test.Run();
        }

        [Theory]
        [MemberData(nameof(LongTestData))]
        public void LongTests(IConvertTest test)
        {
            test.Run();
        }

        [Theory]
        [MemberData(nameof(ULongTestData))]
        public void ULongTests(IConvertTest test)
        {
            test.Run();
        }

        [Theory]
        [MemberData(nameof(FloatTestData))]
        public void FloatTests(IConvertTest test)
        {
            test.Run();
        }

        [Theory]
        [MemberData(nameof(DoubleTestData))]
        public void DoubleTests(IConvertTest test)
        {
            test.Run();
        }

        [Theory]
        [MemberData(nameof(DecimalTestData))]
        public void DecimalTests(IConvertTest test)
        {
            test.Run();
        }
    }
}
