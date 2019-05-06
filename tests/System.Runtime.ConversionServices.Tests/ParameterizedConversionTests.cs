using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Xunit;
using static System.Runtime.ConversionServices.Conversions;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;
using static System.Runtime.ConversionServices.Tests.ParameterizedConversionTests.TestConstants;

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

        public class TestConstants
        {
            public class Bool
            {
                public const bool Default = default(bool);
                public const bool Min = default(bool);
                public const bool Max = true;
                public class Char
                {
                    public const char Default = TestConstants.Char.Default;
                    public const char Max = (char)1;
                }
                public class SByte
                {
                    public const sbyte Default = 0;
                    public const sbyte Max = 1;
                }
                public class Byte
                {
                    public const byte Default = 0;
                    public const byte Max = 1;
                }
                public class Short
                {
                    public const short Default = 0;
                    public const short Max = 1;
                }
                public class UShort
                {
                    public const ushort Default = 0;
                    public const ushort Max = 1;
                }
                public class Int
                {
                    public const int Default = 0;
                    public const int Max = 1;
                }
                public class UInt
                {
                    public const uint Default = 0;
                    public const uint Max = 1;
                }
                public class Long
                {
                    public const long Default = 0;
                    public const long Max = 1;
                }
                public class ULong
                {
                    public const ulong Default = 0;
                    public const ulong Max = 1;
                }
                public class Float
                {
                    public const float Default = 0;
                    public const float Max = 1;
                }
                public class Double
                {
                    public const double Default = 0;
                    public const double Max = 1;
                }
                public class Decimal
                {
                    public const decimal Default = 0;
                    public const decimal Max = 1;
                }
            }

            public class Char
            {
                public const char Default = default(char);
                public const char Min = char.MinValue;
                public const char Max = char.MaxValue;
                public class Bool
                {
                    public const bool Default = TestConstants.Bool.Default;
                    public const bool Min = TestConstants.Bool.Min;
                    public const bool Max = TestConstants.Bool.Max;
                }
                public class SByte
                {
                    public const sbyte Default = (sbyte)Char.Default;
                    public const sbyte Min = unchecked((sbyte)Char.Min);
                    public const sbyte Max = unchecked((sbyte)Char.Max);
                }
                public class Byte
                {
                    public const byte Default = (byte)Char.Default;
                    public const byte Min = unchecked((byte)Char.Min);
                    public const byte Max = unchecked((byte)Char.Max);
                }
                public class Short
                {
                    public const short Default = (short)Char.Default;
                    public const short Min = unchecked((short)Char.Min);
                    public const short Max = unchecked((short)Char.Max);
                }
                public class UShort
                {
                    public const ushort Default = (ushort)Char.Default;
                    public const ushort Min = (ushort)Char.Min;
                    public const ushort Max = (ushort)Char.Max;
                }
                public class Int
                {
                    public const int Default = (int)Char.Default;
                    public const int Min = (int)Char.Min;
                    public const int Max = (int)Char.Max;
                }
                public class UInt
                {
                    public const uint Default = (uint)Char.Default;
                    public const uint Min = (uint)Char.Min;
                    public const uint Max = (uint)Char.Max;
                }
                public class Long
                {
                    public const long Default = (long)Char.Default;
                    public const long Min = (long)Char.Min;
                    public const long Max = (long)Char.Max;
                }
                public class ULong
                {
                    public const ulong Default = (ulong)Char.Default;
                    public const ulong Min = (ulong)Char.Min;
                    public const ulong Max = (ulong)Char.Max;
                }
                public class Float
                {
                    public const float Default = (float)Char.Default;
                    public const float Min = (float)Char.Min;
                    public const float Max = (float)Char.Max;
                }
                public class Double
                {
                    public const double Default = (double)Char.Default;
                    public const double Min = (double)Char.Min;
                    public const double Max = (double)Char.Max;
                }
                public class Decimal
                {
                    public const decimal Default = (decimal)Char.Default;
                    public const decimal Min = (decimal)Char.Min;
                    public const decimal Max = (decimal)Char.Max;
                }
            }

            public class SByte
            {
                public const sbyte Default = default(sbyte);
                public const sbyte Min = sbyte.MinValue;
                public const sbyte Max = sbyte.MaxValue;
                public class Bool
                {
                    public const bool Default = TestConstants.Bool.Default;
                    public const bool Min = TestConstants.Bool.Max;
                    public const bool Max = TestConstants.Bool.Max;
                }
                public class Char
                {
                    public const char Default = (char)SByte.Default;
                    public const char Min = unchecked((char)SByte.Min);
                    public const char Max = (char)SByte.Max;
                }
                public class Byte
                {
                    public const byte Default = (byte)SByte.Default;
                    public const byte Min = unchecked((byte)SByte.Min);
                    public const byte Max = (byte)SByte.Max;
                }
                public class Short
                {
                    public const short Default = (short)SByte.Default;
                    public const short Min = (short)SByte.Min;
                    public const short Max = (short)SByte.Max;
                }
                public class UShort
                {
                    public const ushort Default = (ushort)SByte.Default;
                    public const ushort Min = unchecked((ushort)SByte.Min);
                    public const ushort Max = (ushort)SByte.Max;
                }
                public class Int
                {
                    public const int Default = (int)SByte.Default;
                    public const int Min = (int)SByte.Min;
                    public const int Max = (int)SByte.Max;
                }
                public class UInt
                {
                    public const uint Default = (uint)SByte.Default;
                    public const uint Min = unchecked((uint)SByte.Min);
                    public const uint Max = (uint)SByte.Max;
                }
                public class Long
                {
                    public const long Default = (long)SByte.Default;
                    public const long Min = (long)SByte.Min;
                    public const long Max = (long)SByte.Max;
                }
                public class ULong
                {
                    public const ulong Default = (ulong)SByte.Default;
                    public const ulong Min = unchecked((ulong)SByte.Min);
                    public const ulong Max = (ulong)SByte.Max;
                }
                public class Float
                {
                    public const float Default = (float)SByte.Default;
                    public const float Min = (float)SByte.Min;
                    public const float Max = (float)SByte.Max;
                }
                public class Double
                {
                    public const double Default = (double)SByte.Default;
                    public const double Min = (double)SByte.Min;
                    public const double Max = (double)SByte.Max;
                }
                public class Decimal
                {
                    public const decimal Default = (decimal)SByte.Default;
                    public const decimal Min = (decimal)SByte.Min;
                    public const decimal Max = (decimal)SByte.Max;
                }
            }

            public class Byte
            {
                public const byte Default = default(byte);
                public const byte Min = byte.MinValue;
                public const byte Max = byte.MaxValue;
                public class Bool
                {
                    public const bool Default = TestConstants.Bool.Default;
                    public const bool Min = TestConstants.Bool.Min;
                    public const bool Max = TestConstants.Bool.Max;
                }
                public class Char
                {
                    public const char Default = (char)Byte.Default;
                    public const char Min = unchecked((char)Byte.Min);
                    public const char Max = (char)Byte.Max;
                }
                public class SByte
                {
                    public const sbyte Default = (sbyte)Byte.Default;
                    public const sbyte Min = unchecked((sbyte)Byte.Min);
                    public const sbyte Max = unchecked((sbyte)Byte.Max);
                }
                public class Short
                {
                    public const short Default = (short)Byte.Default;
                    public const short Min = (short)Byte.Min;
                    public const short Max = (short)Byte.Max;
                }
                public class UShort
                {
                    public const ushort Default = (ushort)Byte.Default;
                    public const ushort Min = unchecked((ushort)Byte.Min);
                    public const ushort Max = (ushort)Byte.Max;
                }
                public class Int
                {
                    public const int Default = (int)Byte.Default;
                    public const int Min = (int)Byte.Min;
                    public const int Max = (int)Byte.Max;
                }
                public class UInt
                {
                    public const uint Default = (uint)Byte.Default;
                    public const uint Min = unchecked((uint)Byte.Min);
                    public const uint Max = (uint)Byte.Max;
                }
                public class Long
                {
                    public const long Default = (long)Byte.Default;
                    public const long Min = (long)Byte.Min;
                    public const long Max = (long)Byte.Max;
                }
                public class ULong
                {
                    public const ulong Default = (ulong)Byte.Default;
                    public const ulong Min = unchecked((ulong)Byte.Min);
                    public const ulong Max = (ulong)Byte.Max;
                }
                public class Float
                {
                    public const float Default = (float)Byte.Default;
                    public const float Min = (float)Byte.Min;
                    public const float Max = (float)Byte.Max;
                }
                public class Double
                {
                    public const double Default = (double)Byte.Default;
                    public const double Min = (double)Byte.Min;
                    public const double Max = (double)Byte.Max;
                }
                public class Decimal
                {
                    public const decimal Default = (decimal)Byte.Default;
                    public const decimal Min = (decimal)Byte.Min;
                    public const decimal Max = (decimal)Byte.Max;
                }
            }

            public class Short
            {
                public const short Default = default(short);
                public const short Min = short.MinValue;
                public const short Max = short.MaxValue;
                public class Bool
                {
                    public const bool Default = TestConstants.Bool.Default;
                    public const bool Min = TestConstants.Bool.Max;
                    public const bool Max = TestConstants.Bool.Max;
                }
                public class Char
                {
                    public const char Default = (char)Short.Default;
                    public const char Min = unchecked((char)Short.Min);
                    public const char Max = (char)Short.Max;
                }
                public class Byte
                {
                    public const byte Default = (byte)Short.Default;
                    public const byte Min = unchecked((byte)Short.Min);
                    public const byte Max = unchecked((byte)Short.Max);
                }
                public class SByte
                {
                    public const sbyte Default = (sbyte)Short.Default;
                    public const sbyte Min = unchecked((sbyte)Short.Min);
                    public const sbyte Max = unchecked((sbyte)Short.Max);
                }
                public class UShort
                {
                    public const ushort Default = (ushort)Short.Default;
                    public const ushort Min = unchecked((ushort)Short.Min);
                    public const ushort Max = unchecked((ushort)Short.Max);
                }
                public class Int
                {
                    public const int Default = (int)Short.Default;
                    public const int Min = (int)Short.Min;
                    public const int Max = (int)Short.Max;
                }
                public class UInt
                {
                    public const uint Default = (uint)Short.Default;
                    public const uint Min = unchecked((uint)Short.Min);
                    public const uint Max = (uint)Short.Max;
                }
                public class Long
                {
                    public const long Default = (long)Short.Default;
                    public const long Min = (long)Short.Min;
                    public const long Max = (long)Short.Max;
                }
                public class ULong
                {
                    public const ulong Default = (ulong)Short.Default;
                    public const ulong Min = unchecked((ulong)Short.Min);
                    public const ulong Max = (ulong)Short.Max;
                }
                public class Float
                {
                    public const float Default = (float)Short.Default;
                    public const float Min = (float)Short.Min;
                    public const float Max = (float)Short.Max;
                }
                public class Double
                {
                    public const double Default = (double)Short.Default;
                    public const double Min = (double)Short.Min;
                    public const double Max = (double)Short.Max;
                }
                public class Decimal
                {
                    public const decimal Default = (decimal)Short.Default;
                    public const decimal Min = (decimal)Short.Min;
                    public const decimal Max = (decimal)Short.Max;
                }
            }

            public class UShort
            {
                public const ushort Default = default(ushort);
                public const ushort Min = ushort.MinValue;
                public const ushort Max = ushort.MaxValue;
                public class Bool
                {
                    public const bool Default = TestConstants.Bool.Default;
                    public const bool Min = TestConstants.Bool.Min;
                    public const bool Max = TestConstants.Bool.Max;
                }
                public class Char
                {
                    public const char Default = (char)UShort.Default;
                    public const char Min = unchecked((char)UShort.Min);
                    public const char Max = (char)UShort.Max;
                }
                public class SByte
                {
                    public const sbyte Default = (sbyte)UShort.Default;
                    public const sbyte Min = unchecked((sbyte)UShort.Min);
                    public const sbyte Max = unchecked((sbyte)UShort.Max);
                }
                public class Byte
                {
                    public const byte Default = (byte)UShort.Default;
                    public const byte Min = unchecked((byte)UShort.Min);
                    public const byte Max = unchecked((byte)UShort.Max);
                }
                public class Short
                {
                    public const short Default = (short)UShort.Default;
                    public const short Min = unchecked((short)UShort.Min);
                    public const short Max = unchecked((short)UShort.Max);
                }
                public class Int
                {
                    public const int Default = (int)UShort.Default;
                    public const int Min = (int)UShort.Min;
                    public const int Max = (int)UShort.Max;
                }
                public class UInt
                {
                    public const uint Default = (uint)UShort.Default;
                    public const uint Min = unchecked((uint)UShort.Min);
                    public const uint Max = (uint)UShort.Max;
                }
                public class Long
                {
                    public const long Default = (long)UShort.Default;
                    public const long Min = (long)UShort.Min;
                    public const long Max = (long)UShort.Max;
                }
                public class ULong
                {
                    public const ulong Default = (ulong)UShort.Default;
                    public const ulong Min = unchecked((ulong)UShort.Min);
                    public const ulong Max = (ulong)UShort.Max;
                }
                public class Float
                {
                    public const float Default = (float)UShort.Default;
                    public const float Min = (float)UShort.Min;
                    public const float Max = (float)UShort.Max;
                }
                public class Double
                {
                    public const double Default = (double)UShort.Default;
                    public const double Min = (double)UShort.Min;
                    public const double Max = (double)UShort.Max;
                }
                public class Decimal
                {
                    public const decimal Default = (decimal)UShort.Default;
                    public const decimal Min = (decimal)UShort.Min;
                    public const decimal Max = (decimal)UShort.Max;
                }
            }

            public class Int
            {
                public const int Default = default(int);
                public const int Min = int.MinValue;
                public const int Max = int.MaxValue;
                public class Bool
                {
                    public const bool Default = TestConstants.Bool.Default;
                    public const bool Min = TestConstants.Bool.Max;
                    public const bool Max = TestConstants.Bool.Max;
                }
                public class Char
                {
                    public const char Default = (char)Int.Default;
                    public const char Min = unchecked((char)Int.Min);
                    public const char Max = unchecked((char)Int.Max);
                }
                public class Byte
                {
                    public const byte Default = (byte)Int.Default;
                    public const byte Min = unchecked((byte)Int.Min);
                    public const byte Max = unchecked((byte)Int.Max);
                }
                public class SByte
                {
                    public const sbyte Default = (sbyte)Int.Default;
                    public const sbyte Min = unchecked((sbyte)Int.Min);
                    public const sbyte Max = unchecked((sbyte)Int.Max);
                }
                public class Short
                {
                    public const short Default = (short)Int.Default;
                    public const short Min = unchecked((short)Int.Min);
                    public const short Max = unchecked((short)Int.Max);
                }
                public class UShort
                {
                    public const ushort Default = (ushort)Int.Default;
                    public const ushort Min = unchecked((ushort)Int.Min);
                    public const ushort Max = unchecked((ushort)Int.Max);
                }
                public class UInt
                {
                    public const uint Default = (uint)Int.Default;
                    public const uint Min = unchecked((uint)Int.Min);
                    public const uint Max = (uint)Int.Max;
                }
                public class Long
                {
                    public const long Default = (long)Int.Default;
                    public const long Min = (long)Int.Min;
                    public const long Max = (long)Int.Max;
                }
                public class ULong
                {
                    public const ulong Default = (ulong)Int.Default;
                    public const ulong Min = unchecked((ulong)Int.Min);
                    public const ulong Max = (ulong)Int.Max;
                }
                public class Float
                {
                    public const float Default = (float)Int.Default;
                    public const float Min = (float)Int.Min;
                    public const float Max = (float)Int.Max;
                }
                public class Double
                {
                    public const double Default = (double)Int.Default;
                    public const double Min = (double)Int.Min;
                    public const double Max = (double)Int.Max;
                }
                public class Decimal
                {
                    public const decimal Default = (decimal)Int.Default;
                    public const decimal Min = (decimal)Int.Min;
                    public const decimal Max = (decimal)Int.Max;
                }
            }

            public class UInt
            {
                public const uint Default = default(uint);
                public const uint Min = uint.MinValue;
                public const uint Max = uint.MaxValue;
                public class Bool
                {
                    public const bool Default = TestConstants.Bool.Default;
                    public const bool Min = TestConstants.Bool.Min;
                    public const bool Max = TestConstants.Bool.Max;
                }
                public class Char
                {
                    public const char Default = (char)UInt.Default;
                    public const char Min = unchecked((char)UInt.Min);
                    public const char Max = unchecked((char)UInt.Max);
                }
                public class SByte
                {
                    public const sbyte Default = (sbyte)UInt.Default;
                    public const sbyte Min = unchecked((sbyte)UInt.Min);
                    public const sbyte Max = unchecked((sbyte)UInt.Max);
                }
                public class Byte
                {
                    public const byte Default = (byte)UInt.Default;
                    public const byte Min = unchecked((byte)UInt.Min);
                    public const byte Max = unchecked((byte)UInt.Max);
                }
                public class Short
                {
                    public const short Default = (short)UInt.Default;
                    public const short Min = unchecked((short)UInt.Min);
                    public const short Max = unchecked((short)UInt.Max);
                }
                public class UShort
                {
                    public const ushort Default = (ushort)UInt.Default;
                    public const ushort Min = unchecked((ushort)UInt.Min);
                    public const ushort Max = unchecked((ushort)UInt.Max);
                }
                public class Int
                {
                    public const int Default = (int)UInt.Default;
                    public const int Min = unchecked((int)UInt.Min);
                    public const int Max = unchecked((int)UInt.Max);
                }
                public class Long
                {
                    public const long Default = (long)UInt.Default;
                    public const long Min = (long)UInt.Min;
                    public const long Max = (long)UInt.Max;
                }
                public class ULong
                {
                    public const ulong Default = (ulong)UInt.Default;
                    public const ulong Min = unchecked((ulong)UInt.Min);
                    public const ulong Max = (ulong)UInt.Max;
                }
                public class Float
                {
                    public const float Default = (float)UInt.Default;
                    public const float Min = (float)UInt.Min;
                    public const float Max = (float)UInt.Max;
                }
                public class Double
                {
                    public const double Default = (double)UInt.Default;
                    public const double Min = (double)UInt.Min;
                    public const double Max = (double)UInt.Max;
                }
                public class Decimal
                {
                    public const decimal Default = (decimal)UInt.Default;
                    public const decimal Min = (decimal)UInt.Min;
                    public const decimal Max = (decimal)UInt.Max;
                }
            }

            public class Long
            {
                public const long Default = default(long);
                public const long Min = long.MinValue;
                public const long Max = long.MaxValue;
                public class Bool
                {
                    public const bool Default = TestConstants.Bool.Default;
                    public const bool Min = TestConstants.Bool.Max;
                    public const bool Max = TestConstants.Bool.Max;
                }
                public class Char
                {
                    public const char Default = (char)Long.Default;
                    public const char Min = unchecked((char)Long.Min);
                    public const char Max = unchecked((char)Long.Max);
                }
                public class Byte
                {
                    public const byte Default = (byte)Long.Default;
                    public const byte Min = unchecked((byte)Long.Min);
                    public const byte Max = unchecked((byte)Long.Max);
                }
                public class SByte
                {
                    public const sbyte Default = (sbyte)Long.Default;
                    public const sbyte Min = unchecked((sbyte)Long.Min);
                    public const sbyte Max = unchecked((sbyte)Long.Max);
                }
                public class Short
                {
                    public const short Default = (short)Long.Default;
                    public const short Min = unchecked((short)Long.Min);
                    public const short Max = unchecked((short)Long.Max);
                }
                public class UShort
                {
                    public const ushort Default = (ushort)Long.Default;
                    public const ushort Min = unchecked((ushort)Long.Min);
                    public const ushort Max = unchecked((ushort)Long.Max);
                }
                public class Int
                {
                    public const int Default = (int)Long.Default;
                    public const int Min = unchecked((int)Long.Min);
                    public const int Max = unchecked((int)Long.Max);
                }
                public class UInt
                {
                    public const uint Default = (uint)Long.Default;
                    public const uint Min = unchecked((uint)Long.Min);
                    public const uint Max = unchecked((uint)Long.Max);
                }
                public class ULong
                {
                    public const ulong Default = (ulong)Long.Default;
                    public const ulong Min = unchecked((ulong)Long.Min);
                    public const ulong Max = (ulong)Long.Max;
                }
                public class Float
                {
                    public const float Default = (float)Long.Default;
                    public const float Min = (float)Long.Min;
                    public const float Max = (float)Long.Max;
                }
                public class Double
                {
                    public const double Default = (double)Long.Default;
                    public const double Min = (double)Long.Min;
                    public const double Max = (double)Long.Max;
                }
                public class Decimal
                {
                    public const decimal Default = (decimal)Long.Default;
                    public const decimal Min = (decimal)Long.Min;
                    public const decimal Max = (decimal)Long.Max;
                }
            }

            public class ULong
            {
                public const ulong Default = default(ulong);
                public const ulong Min = ulong.MinValue;
                public const ulong Max = ulong.MaxValue;
                public class Bool
                {
                    public const bool Default = TestConstants.Bool.Default;
                    public const bool Min = TestConstants.Bool.Min;
                    public const bool Max = TestConstants.Bool.Max;
                }
                public class Char
                {
                    public const char Default = (char)ULong.Default;
                    public const char Min = unchecked((char)ULong.Min);
                    public const char Max = unchecked((char)ULong.Max);
                }
                public class SByte
                {
                    public const sbyte Default = (sbyte)ULong.Default;
                    public const sbyte Min = unchecked((sbyte)ULong.Min);
                    public const sbyte Max = unchecked((sbyte)ULong.Max);
                }
                public class Byte
                {
                    public const byte Default = (byte)ULong.Default;
                    public const byte Min = unchecked((byte)ULong.Min);
                    public const byte Max = unchecked((byte)ULong.Max);
                }
                public class Short
                {
                    public const short Default = (short)ULong.Default;
                    public const short Min = unchecked((short)ULong.Min);
                    public const short Max = unchecked((short)ULong.Max);
                }
                public class UShort
                {
                    public const ushort Default = (ushort)ULong.Default;
                    public const ushort Min = unchecked((ushort)ULong.Min);
                    public const ushort Max = unchecked((ushort)ULong.Max);
                }
                public class Int
                {
                    public const int Default = (int)ULong.Default;
                    public const int Min = unchecked((int)ULong.Min);
                    public const int Max = unchecked((int)ULong.Max);
                }
                public class UInt
                {
                    public const uint Default = (uint)ULong.Default;
                    public const uint Min = unchecked((uint)ULong.Min);
                    public const uint Max = unchecked((uint)ULong.Max);
                }
                public class Long
                {
                    public const long Default = (long)ULong.Default;
                    public const long Min = (long)ULong.Min;
                    public const long Max = unchecked((long)ULong.Max);
                }
                public class Float
                {
                    public const float Default = (float)ULong.Default;
                    public const float Min = (float)ULong.Min;
                    public const float Max = (float)ULong.Max;
                }
                public class Double
                {
                    public const double Default = (double)ULong.Default;
                    public const double Min = (double)ULong.Min;
                    public const double Max = (double)ULong.Max;
                }
                public class Decimal
                {
                    public const decimal Default = (decimal)ULong.Default;
                    public const decimal Min = (decimal)ULong.Min;
                    public const decimal Max = (decimal)ULong.Max;
                }
            }

            public class Float
            {
                public const float Default = default(float);
                public const float Min = float.MinValue;
                public const float Max = float.MaxValue;
                public class Bool
                {
                    public const bool Default = TestConstants.Bool.Default;
                    public const bool Min = TestConstants.Bool.Max;
                    public const bool Max = TestConstants.Bool.Max;
                }
                public class Char
                {
                    public const char Default = (char)Float.Default;
                    public const char Min = unchecked((char)Float.Min);
                    public const char Max = unchecked((char)Float.Max);
                }
                public class Byte
                {
                    public const byte Default = (byte)Float.Default;
                    public const byte Min = unchecked((byte)Float.Min);
                    public const byte Max = unchecked((byte)Float.Max);
                }
                public class SByte
                {
                    public const sbyte Default = (sbyte)Float.Default;
                    public const sbyte Min = unchecked((sbyte)Float.Min);
                    public const sbyte Max = unchecked((sbyte)Float.Max);
                }
                public class Short
                {
                    public const short Default = (short)Float.Default;
                    public const short Min = unchecked((short)Float.Min);
                    public const short Max = unchecked((short)Float.Max);
                }
                public class UShort
                {
                    public const ushort Default = (ushort)Float.Default;
                    public const ushort Min = unchecked((ushort)Float.Min);
                    public const ushort Max = unchecked((ushort)Float.Max);
                }
                public class Int
                {
                    public const int Default = (int)Float.Default;
                    public const int Min = unchecked((int)Float.Min);
                    public const int Max = unchecked((int)Float.Max);
                }
                public class UInt
                {
                    public const uint Default = (uint)Float.Default;
                    public const uint Min = unchecked((uint)Float.Min);
                    public const uint Max = unchecked((uint)Float.Max);
                }
                public class Long
                {
                    public const long Default = (long)Float.Default;
                    public const long Min = unchecked((long)Float.Min);
                    public const long Max = unchecked((long)Float.Max);
                }
                public class ULong
                {
                    public const ulong Default = (ulong)Float.Default;
                    public const ulong Min = unchecked((ulong)Float.Min);
                    public const ulong Max = unchecked((ulong)Float.Max);
                }
                public class Double
                {
                    public const double Default = (double)Float.Default;
                    public const double Min = (double)Float.Min;
                    public const double Max = (double)Float.Max;
                }
                public class Decimal
                {
                    public const decimal Default = (decimal)Float.Default;
                    //TODO: Assert Throws
                    //public static readonly decimal Min = Convert.ToDecimal(Float.Min);// decimal.Parse(Float.Min.ToString());
                    //public static readonly decimal Max = Convert.ToDecimal(Float.Max);// decimal.Parse(Float.Max.ToString());
                }
            }

            public class Double
            {
                public const double Default = default(double);
                public const double Min = double.MinValue;
                public const double Max = double.MaxValue;
                public class Bool
                {
                    public const bool Default = TestConstants.Bool.Default;
                    public const bool Min = TestConstants.Bool.Max;
                    public const bool Max = TestConstants.Bool.Max;
                }
                public class Char
                {
                    public const char Default = (char)Double.Default;
                    public const char Min = unchecked((char)Double.Min);
                    public const char Max = unchecked((char)Double.Max);
                }
                public class Byte
                {
                    public const byte Default = (byte)Double.Default;
                    public const byte Min = unchecked((byte)Double.Min);
                    public const byte Max = unchecked((byte)Double.Max);
                }
                public class SByte
                {
                    public const sbyte Default = (sbyte)Double.Default;
                    public const sbyte Min = unchecked((sbyte)Double.Min);
                    public const sbyte Max = unchecked((sbyte)Double.Max);
                }
                public class Short
                {
                    public const short Default = (short)Double.Default;
                    public const short Min = unchecked((short)Double.Min);
                    public const short Max = unchecked((short)Double.Max);
                }
                public class UShort
                {
                    public const ushort Default = (ushort)Double.Default;
                    public const ushort Min = unchecked((ushort)Double.Min);
                    public const ushort Max = unchecked((ushort)Double.Max);
                }
                public class Int
                {
                    public const int Default = (int)Double.Default;
                    public const int Min = unchecked((int)Double.Min);
                    public const int Max = unchecked((int)Double.Max);
                }
                public class UInt
                {
                    public const uint Default = (uint)Double.Default;
                    public const uint Min = unchecked((uint)Double.Min);
                    public const uint Max = unchecked((uint)Double.Max);
                }
                public class Long
                {
                    public const long Default = (long)Double.Default;
                    public const long Min = unchecked((long)Double.Min);
                    public const long Max = unchecked((long)Double.Max);
                }
                public class ULong
                {
                    public const ulong Default = (ulong)Double.Default;
                    public const ulong Min = unchecked((ulong)Double.Min);
                    public const ulong Max = unchecked((ulong)Double.Max);
                }
                public class Float
                {
                    public const float Default = (float)Double.Default;
                    public const float Min = (float)Double.Min;
                    public const float Max = (float)Double.Max;
                }
                public class Decimal
                {
                    public const decimal Default = (decimal)Double.Default;
                    //TODO: Assert Throws
                    //public static readonly decimal Min = decimal.Parse(Double.Min.ToString());
                    //TODO: Assert Throws
                    //public static readonly decimal Max = decimal.Parse(Double.Max.ToString());
                }
            }

            public class Decimal
            {
                public const decimal Default = default(decimal);
                public const decimal Min = decimal.MinValue;
                public const decimal Max = decimal.MaxValue;
                public class Bool
                {
                    public const bool Default = TestConstants.Bool.Default;
                    public const bool Min = TestConstants.Bool.Max;
                    public const bool Max = TestConstants.Bool.Max;
                }
                public class Byte
                {
                    public const byte Default = (byte)Decimal.Default;
                    //TODO: Assert Throws
                    //public static readonly byte Min = decimal.ToByte(Decimal.Min); //unchecked((byte)Decimal.Min);
                    //TODO: Assert Throws
                    //public static readonly byte Max = decimal.ToByte(Decimal.Max); //unchecked((byte)Decimal.Max);
                }
                public class Char
                {
                    public const char Default = (char)Double.Default;
                    //TODO: Assert Throws
                    //public const char Min = unchecked((char)Decimal.Min);
                    //TODO: Assert Throws
                    //public const char Max = unchecked((char)Decimal.Max);
                }
                public class SByte
                {
                    public const sbyte Default = (sbyte)Decimal.Default;
                    //TODO: Assert Throws
                    //public static readonly sbyte Min = decimal.ToSByte(Decimal.Min);
                    //TODO: Assert Throws
                    //public static readonly sbyte Max = decimal.ToSByte(Decimal.Max);
                }
                public class Short
                {
                    public const short Default = (short)Decimal.Default;
                    //TODO: Assert Throws
                    //public static readonly short Min = decimal.ToInt16(Decimal.Min);
                    //TODO: Assert Throws
                    //public static readonly short Max = decimal.ToInt16(Decimal.Max);
                }
                public class UShort
                {
                    public const ushort Default = (ushort)Decimal.Default;
                    //TODO: Assert Throws
                    //public static readonly ushort Min = decimal.ToUInt16(Decimal.Min);
                    //TODO: Assert Throws
                    //public static readonly ushort Max = decimal.ToUInt16(Decimal.Max);
                }
                public class Int
                {
                    public const int Default = (int)Decimal.Default;
                    //TODO: Assert Throws
                    //public static readonly int Min = decimal.ToInt32(Decimal.Min);
                    //TODO: Assert Throws
                    //public static readonly int Max = decimal.ToInt32(Decimal.Max);
                }
                public class UInt
                {
                    public const uint Default = (uint)Decimal.Default;
                    //TODO: Assert Throws
                    //public static readonly uint Min = decimal.ToUInt32(Decimal.Min);
                    //TODO: Assert Throws
                    //public static readonly uint Max = decimal.ToUInt32(Decimal.Max);
                }
                public class Long
                {
                    public const long Default = (long)Decimal.Default;
                    //TODO: Assert Throws
                    //public static readonly long Min = decimal.ToInt64(Decimal.Min);
                    //TODO: Assert Throws
                    //public static readonly long Max = decimal.ToInt64(Decimal.Max);
                }
                public class ULong
                {
                    public const ulong Default = (ulong)Decimal.Default;
                    //TODO: Assert Throws
                    //public static readonly ulong Min = decimal.ToUInt64(Decimal.Min);
                    //TODO: Assert Throws
                    //public static readonly ulong Max = decimal.ToUInt64(Decimal.Max);
                }
                public class Float
                {
                    public const float Default = (float)Decimal.Default;
                    public const float Min = (float)Decimal.Min;
                    public const float Max = (float)Decimal.Max;
                }
                public class Double
                {
                    public const double Default = (double)Decimal.Default;
                    public const double Min = (double)Decimal.Min;
                    public const double Max = (double)Decimal.Max;
                }
            }
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
                ConvertTest.Create(TestConstants.Char.Default, TestConstants.Char.Bool.Default),
                ConvertTest.Create(TestConstants.Char.Default, TestConstants.Char.Default),
                ConvertTest.Create(TestConstants.Char.Default, TestConstants.Char.Byte.Default),
                ConvertTest.Create(TestConstants.Char.Default, TestConstants.Char.SByte.Default),
                ConvertTest.Create(TestConstants.Char.Default, TestConstants.Char.Short.Default),
                ConvertTest.Create(TestConstants.Char.Default, TestConstants.Char.UShort.Default),
                ConvertTest.Create(TestConstants.Char.Default, TestConstants.Char.Int.Default),
                ConvertTest.Create(TestConstants.Char.Default, TestConstants.Char.UInt.Default),
                ConvertTest.Create(TestConstants.Char.Default, TestConstants.Char.Long.Default),
                ConvertTest.Create(TestConstants.Char.Default, TestConstants.Char.ULong.Default),
                ConvertTest.Create(TestConstants.Char.Default, TestConstants.Char.Float.Default),
                ConvertTest.Create(TestConstants.Char.Default, TestConstants.Char.Double.Default),
                ConvertTest.Create(TestConstants.Char.Default, TestConstants.Char.Decimal.Default),

                ConvertTest.Create(TestConstants.Char.Min, TestConstants.Char.Bool.Min),
                ConvertTest.Create(TestConstants.Char.Min, TestConstants.Char.Min),
                ConvertTest.Create(TestConstants.Char.Min, TestConstants.Char.Byte.Min),
                ConvertTest.Create(TestConstants.Char.Min, TestConstants.Char.SByte.Min),
                ConvertTest.Create(TestConstants.Char.Min, TestConstants.Char.Short.Min),
                ConvertTest.Create(TestConstants.Char.Min, TestConstants.Char.UShort.Min),
                ConvertTest.Create(TestConstants.Char.Min, TestConstants.Char.Int.Min),
                ConvertTest.Create(TestConstants.Char.Min, TestConstants.Char.UInt.Min),
                ConvertTest.Create(TestConstants.Char.Min, TestConstants.Char.Long.Min),
                ConvertTest.Create(TestConstants.Char.Min, TestConstants.Char.ULong.Min),
                ConvertTest.Create(TestConstants.Char.Min, TestConstants.Char.Float.Min),
                ConvertTest.Create(TestConstants.Char.Min, TestConstants.Char.Double.Min),
                ConvertTest.Create(TestConstants.Char.Min, TestConstants.Char.Decimal.Min),

                ConvertTest.Create(TestConstants.Char.Max, TestConstants.Char.Bool.Max),
                ConvertTest.Create(TestConstants.Char.Max, TestConstants.Char.Max),
                ConvertTest.Create(TestConstants.Char.Max, TestConstants.Char.SByte.Max),
                ConvertTest.Create(TestConstants.Char.Max, TestConstants.Char.Byte.Max),
                ConvertTest.Create(TestConstants.Char.Max, TestConstants.Char.Short.Max),
                ConvertTest.Create(TestConstants.Char.Max, TestConstants.Char.UShort.Max),
                ConvertTest.Create(TestConstants.Char.Max, TestConstants.Char.Int.Max),
                ConvertTest.Create(TestConstants.Char.Max, TestConstants.Char.UInt.Max),
                ConvertTest.Create(TestConstants.Char.Max, TestConstants.Char.Long.Max),
                ConvertTest.Create(TestConstants.Char.Max, TestConstants.Char.ULong.Max),
                ConvertTest.Create(TestConstants.Char.Max, TestConstants.Char.Float.Max),
                ConvertTest.Create(TestConstants.Char.Max, TestConstants.Char.Double.Max),
                ConvertTest.Create(TestConstants.Char.Max, TestConstants.Char.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> SByteConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TestConstants.SByte.Default, TestConstants.SByte.Bool.Default),
                ConvertTest.Create(TestConstants.SByte.Default, TestConstants.SByte.Char.Default),
                ConvertTest.Create(TestConstants.SByte.Default, TestConstants.SByte.Default),
                ConvertTest.Create(TestConstants.SByte.Default, TestConstants.SByte.Byte.Default),
                ConvertTest.Create(TestConstants.SByte.Default, TestConstants.SByte.Short.Default),
                ConvertTest.Create(TestConstants.SByte.Default, TestConstants.SByte.UShort.Default),
                ConvertTest.Create(TestConstants.SByte.Default, TestConstants.SByte.Int.Default),
                ConvertTest.Create(TestConstants.SByte.Default, TestConstants.SByte.UInt.Default),
                ConvertTest.Create(TestConstants.SByte.Default, TestConstants.SByte.Long.Default),
                ConvertTest.Create(TestConstants.SByte.Default, TestConstants.SByte.ULong.Default),
                ConvertTest.Create(TestConstants.SByte.Default, TestConstants.SByte.Float.Default),
                ConvertTest.Create(TestConstants.SByte.Default, TestConstants.SByte.Double.Default),
                ConvertTest.Create(TestConstants.SByte.Default, TestConstants.SByte.Decimal.Default),

                ConvertTest.Create(TestConstants.SByte.Min, TestConstants.SByte.Bool.Min),
                ConvertTest.Create(TestConstants.SByte.Min, TestConstants.SByte.Char.Min),
                ConvertTest.Create(TestConstants.SByte.Min, TestConstants.SByte.Min),
                ConvertTest.Create(TestConstants.SByte.Min, TestConstants.SByte.Byte.Min),
                ConvertTest.Create(TestConstants.SByte.Min, TestConstants.SByte.Short.Min),
                ConvertTest.Create(TestConstants.SByte.Min, TestConstants.SByte.UShort.Min),
                ConvertTest.Create(TestConstants.SByte.Min, TestConstants.SByte.Int.Min),
                ConvertTest.Create(TestConstants.SByte.Min, TestConstants.SByte.UInt.Min),
                ConvertTest.Create(TestConstants.SByte.Min, TestConstants.SByte.Long.Min),
                ConvertTest.Create(TestConstants.SByte.Min, TestConstants.SByte.ULong.Min),
                ConvertTest.Create(TestConstants.SByte.Min, TestConstants.SByte.Float.Min),
                ConvertTest.Create(TestConstants.SByte.Min, TestConstants.SByte.Double.Min),
                ConvertTest.Create(TestConstants.SByte.Min, TestConstants.SByte.Decimal.Min),

                ConvertTest.Create(TestConstants.SByte.Max, TestConstants.SByte.Bool.Max),
                ConvertTest.Create(TestConstants.SByte.Max, TestConstants.SByte.Char.Max),
                ConvertTest.Create(TestConstants.SByte.Max, TestConstants.SByte.Max),
                ConvertTest.Create(TestConstants.SByte.Max, TestConstants.SByte.Byte.Max),
                ConvertTest.Create(TestConstants.SByte.Max, TestConstants.SByte.Short.Max),
                ConvertTest.Create(TestConstants.SByte.Max, TestConstants.SByte.UShort.Max),
                ConvertTest.Create(TestConstants.SByte.Max, TestConstants.SByte.Int.Max),
                ConvertTest.Create(TestConstants.SByte.Max, TestConstants.SByte.UInt.Max),
                ConvertTest.Create(TestConstants.SByte.Max, TestConstants.SByte.Long.Max),
                ConvertTest.Create(TestConstants.SByte.Max, TestConstants.SByte.ULong.Max),
                ConvertTest.Create(TestConstants.SByte.Max, TestConstants.SByte.Float.Max),
                ConvertTest.Create(TestConstants.SByte.Max, TestConstants.SByte.Double.Max),
                ConvertTest.Create(TestConstants.SByte.Max, TestConstants.SByte.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> ByteConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TestConstants.Byte.Default, TestConstants.Byte.Bool.Default),
                ConvertTest.Create(TestConstants.Byte.Default, TestConstants.Byte.Char.Default),
                ConvertTest.Create(TestConstants.Byte.Default, TestConstants.Byte.SByte.Default),
                ConvertTest.Create(TestConstants.Byte.Default, TestConstants.Byte.Default),
                ConvertTest.Create(TestConstants.Byte.Default, TestConstants.Byte.Short.Default),
                ConvertTest.Create(TestConstants.Byte.Default, TestConstants.Byte.UShort.Default),
                ConvertTest.Create(TestConstants.Byte.Default, TestConstants.Byte.Int.Default),
                ConvertTest.Create(TestConstants.Byte.Default, TestConstants.Byte.UInt.Default),
                ConvertTest.Create(TestConstants.Byte.Default, TestConstants.Byte.Long.Default),
                ConvertTest.Create(TestConstants.Byte.Default, TestConstants.Byte.ULong.Default),
                ConvertTest.Create(TestConstants.Byte.Default, TestConstants.Byte.Float.Default),
                ConvertTest.Create(TestConstants.Byte.Default, TestConstants.Byte.Double.Default),
                ConvertTest.Create(TestConstants.Byte.Default, TestConstants.Byte.Decimal.Default),

                ConvertTest.Create(TestConstants.Byte.Min, TestConstants.Byte.Bool.Min),
                ConvertTest.Create(TestConstants.Byte.Min, TestConstants.Byte.Char.Min),
                ConvertTest.Create(TestConstants.Byte.Min, TestConstants.Byte.SByte.Min),
                ConvertTest.Create(TestConstants.Byte.Min, TestConstants.Byte.Min),
                ConvertTest.Create(TestConstants.Byte.Min, TestConstants.Byte.Short.Min),
                ConvertTest.Create(TestConstants.Byte.Min, TestConstants.Byte.UShort.Min),
                ConvertTest.Create(TestConstants.Byte.Min, TestConstants.Byte.Int.Min),
                ConvertTest.Create(TestConstants.Byte.Min, TestConstants.Byte.UInt.Min),
                ConvertTest.Create(TestConstants.Byte.Min, TestConstants.Byte.Long.Min),
                ConvertTest.Create(TestConstants.Byte.Min, TestConstants.Byte.ULong.Min),
                ConvertTest.Create(TestConstants.Byte.Min, TestConstants.Byte.Float.Min),
                ConvertTest.Create(TestConstants.Byte.Min, TestConstants.Byte.Double.Min),
                ConvertTest.Create(TestConstants.Byte.Min, TestConstants.Byte.Decimal.Min),

                ConvertTest.Create(TestConstants.Byte.Max, TestConstants.Byte.Bool.Max),
                ConvertTest.Create(TestConstants.Byte.Max, TestConstants.Byte.Char.Max),
                ConvertTest.Create(TestConstants.Byte.Max, TestConstants.Byte.SByte.Max),
                ConvertTest.Create(TestConstants.Byte.Max, TestConstants.Byte.Max),
                ConvertTest.Create(TestConstants.Byte.Max, TestConstants.Byte.Short.Max),
                ConvertTest.Create(TestConstants.Byte.Max, TestConstants.Byte.UShort.Max),
                ConvertTest.Create(TestConstants.Byte.Max, TestConstants.Byte.Int.Max),
                ConvertTest.Create(TestConstants.Byte.Max, TestConstants.Byte.UInt.Max),
                ConvertTest.Create(TestConstants.Byte.Max, TestConstants.Byte.Long.Max),
                ConvertTest.Create(TestConstants.Byte.Max, TestConstants.Byte.ULong.Max),
                ConvertTest.Create(TestConstants.Byte.Max, TestConstants.Byte.Float.Max),
                ConvertTest.Create(TestConstants.Byte.Max, TestConstants.Byte.Double.Max),
                ConvertTest.Create(TestConstants.Byte.Max, TestConstants.Byte.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> ShortConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TestConstants.Short.Default, TestConstants.Short.Bool.Default),
                ConvertTest.Create(TestConstants.Short.Default, TestConstants.Short.Char.Default),
                ConvertTest.Create(TestConstants.Short.Default, TestConstants.Short.SByte.Default),
                ConvertTest.Create(TestConstants.Short.Default, TestConstants.Short.Byte.Default),
                ConvertTest.Create(TestConstants.Short.Default, TestConstants.Short.Default),
                ConvertTest.Create(TestConstants.Short.Default, TestConstants.Short.UShort.Default),
                ConvertTest.Create(TestConstants.Short.Default, TestConstants.Short.Int.Default),
                ConvertTest.Create(TestConstants.Short.Default, TestConstants.Short.UInt.Default),
                ConvertTest.Create(TestConstants.Short.Default, TestConstants.Short.Long.Default),
                ConvertTest.Create(TestConstants.Short.Default, TestConstants.Short.ULong.Default),
                ConvertTest.Create(TestConstants.Short.Default, TestConstants.Short.Float.Default),
                ConvertTest.Create(TestConstants.Short.Default, TestConstants.Short.Double.Default),
                ConvertTest.Create(TestConstants.Short.Default, TestConstants.Short.Decimal.Default),

                ConvertTest.Create(TestConstants.Short.Min, TestConstants.Short.Bool.Min),
                ConvertTest.Create(TestConstants.Short.Min, TestConstants.Short.Char.Min),
                ConvertTest.Create(TestConstants.Short.Min, TestConstants.Short.SByte.Min),
                ConvertTest.Create(TestConstants.Short.Min, TestConstants.Short.Byte.Min),
                ConvertTest.Create(TestConstants.Short.Min, TestConstants.Short.Min),
                ConvertTest.Create(TestConstants.Short.Min, TestConstants.Short.UShort.Min),
                ConvertTest.Create(TestConstants.Short.Min, TestConstants.Short.Int.Min),
                ConvertTest.Create(TestConstants.Short.Min, TestConstants.Short.UInt.Min),
                ConvertTest.Create(TestConstants.Short.Min, TestConstants.Short.Long.Min),
                ConvertTest.Create(TestConstants.Short.Min, TestConstants.Short.ULong.Min),
                ConvertTest.Create(TestConstants.Short.Min, TestConstants.Short.Float.Min),
                ConvertTest.Create(TestConstants.Short.Min, TestConstants.Short.Double.Min),
                ConvertTest.Create(TestConstants.Short.Min, TestConstants.Short.Decimal.Min),

                ConvertTest.Create(TestConstants.Short.Max, TestConstants.Short.Bool.Max),
                ConvertTest.Create(TestConstants.Short.Max, TestConstants.Short.Char.Max),
                ConvertTest.Create(TestConstants.Short.Max, TestConstants.Short.SByte.Max),
                ConvertTest.Create(TestConstants.Short.Max, TestConstants.Short.Byte.Max),
                ConvertTest.Create(TestConstants.Short.Max, TestConstants.Short.Max),
                ConvertTest.Create(TestConstants.Short.Max, TestConstants.Short.UShort.Max),
                ConvertTest.Create(TestConstants.Short.Max, TestConstants.Short.Int.Max),
                ConvertTest.Create(TestConstants.Short.Max, TestConstants.Short.UInt.Max),
                ConvertTest.Create(TestConstants.Short.Max, TestConstants.Short.Long.Max),
                ConvertTest.Create(TestConstants.Short.Max, TestConstants.Short.ULong.Max),
                ConvertTest.Create(TestConstants.Short.Max, TestConstants.Short.Float.Max),
                ConvertTest.Create(TestConstants.Short.Max, TestConstants.Short.Double.Max),
                ConvertTest.Create(TestConstants.Short.Max, TestConstants.Short.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> UShortConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TestConstants.UShort.Default, TestConstants.UShort.Bool.Default),
                ConvertTest.Create(TestConstants.UShort.Default, TestConstants.UShort.Char.Default),
                ConvertTest.Create(TestConstants.UShort.Default, TestConstants.UShort.SByte.Default),
                ConvertTest.Create(TestConstants.UShort.Default, TestConstants.UShort.Byte.Default),
                ConvertTest.Create(TestConstants.UShort.Default, TestConstants.UShort.Short.Default),
                ConvertTest.Create(TestConstants.UShort.Default, TestConstants.UShort.Default),
                ConvertTest.Create(TestConstants.UShort.Default, TestConstants.UShort.Int.Default),
                ConvertTest.Create(TestConstants.UShort.Default, TestConstants.UShort.UInt.Default),
                ConvertTest.Create(TestConstants.UShort.Default, TestConstants.UShort.Long.Default),
                ConvertTest.Create(TestConstants.UShort.Default, TestConstants.UShort.ULong.Default),
                ConvertTest.Create(TestConstants.UShort.Default, TestConstants.UShort.Float.Default),
                ConvertTest.Create(TestConstants.UShort.Default, TestConstants.UShort.Double.Default),
                ConvertTest.Create(TestConstants.UShort.Default, TestConstants.UShort.Decimal.Default),

                ConvertTest.Create(TestConstants.UShort.Min, TestConstants.UShort.Bool.Min),
                ConvertTest.Create(TestConstants.UShort.Min, TestConstants.UShort.Char.Min),
                ConvertTest.Create(TestConstants.UShort.Min, TestConstants.UShort.SByte.Min),
                ConvertTest.Create(TestConstants.UShort.Min, TestConstants.UShort.Byte.Min),
                ConvertTest.Create(TestConstants.UShort.Min, TestConstants.UShort.Short.Min),
                ConvertTest.Create(TestConstants.UShort.Min, TestConstants.UShort.Min),
                ConvertTest.Create(TestConstants.UShort.Min, TestConstants.UShort.Int.Min),
                ConvertTest.Create(TestConstants.UShort.Min, TestConstants.UShort.UInt.Min),
                ConvertTest.Create(TestConstants.UShort.Min, TestConstants.UShort.Long.Min),
                ConvertTest.Create(TestConstants.UShort.Min, TestConstants.UShort.ULong.Min),
                ConvertTest.Create(TestConstants.UShort.Min, TestConstants.UShort.Float.Min),
                ConvertTest.Create(TestConstants.UShort.Min, TestConstants.UShort.Double.Min),
                ConvertTest.Create(TestConstants.UShort.Min, TestConstants.UShort.Decimal.Min),

                ConvertTest.Create(TestConstants.UShort.Max, TestConstants.UShort.Bool.Max),
                ConvertTest.Create(TestConstants.UShort.Max, TestConstants.UShort.Char.Max),
                ConvertTest.Create(TestConstants.UShort.Max, TestConstants.UShort.SByte.Max),
                ConvertTest.Create(TestConstants.UShort.Max, TestConstants.UShort.Byte.Max),
                ConvertTest.Create(TestConstants.UShort.Max, TestConstants.UShort.Short.Max),
                ConvertTest.Create(TestConstants.UShort.Max, TestConstants.UShort.Max),
                ConvertTest.Create(TestConstants.UShort.Max, TestConstants.UShort.Int.Max),
                ConvertTest.Create(TestConstants.UShort.Max, TestConstants.UShort.UInt.Max),
                ConvertTest.Create(TestConstants.UShort.Max, TestConstants.UShort.Long.Max),
                ConvertTest.Create(TestConstants.UShort.Max, TestConstants.UShort.ULong.Max),
                ConvertTest.Create(TestConstants.UShort.Max, TestConstants.UShort.Float.Max),
                ConvertTest.Create(TestConstants.UShort.Max, TestConstants.UShort.Double.Max),
                ConvertTest.Create(TestConstants.UShort.Max, TestConstants.UShort.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> IntConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TestConstants.Int.Default, TestConstants.Int.Bool.Default),
                ConvertTest.Create(TestConstants.Int.Default, TestConstants.Int.Char.Default),
                ConvertTest.Create(TestConstants.Int.Default, TestConstants.Int.SByte.Default),
                ConvertTest.Create(TestConstants.Int.Default, TestConstants.Int.Byte.Default),
                ConvertTest.Create(TestConstants.Int.Default, TestConstants.Int.Short.Default),
                ConvertTest.Create(TestConstants.Int.Default, TestConstants.Int.UShort.Default),
                ConvertTest.Create(TestConstants.Int.Default, TestConstants.Int.Default),
                ConvertTest.Create(TestConstants.Int.Default, TestConstants.Int.UInt.Default),
                ConvertTest.Create(TestConstants.Int.Default, TestConstants.Int.Long.Default),
                ConvertTest.Create(TestConstants.Int.Default, TestConstants.Int.ULong.Default),
                ConvertTest.Create(TestConstants.Int.Default, TestConstants.Int.Float.Default),
                ConvertTest.Create(TestConstants.Int.Default, TestConstants.Int.Double.Default),
                ConvertTest.Create(TestConstants.Int.Default, TestConstants.Int.Decimal.Default),

                ConvertTest.Create(TestConstants.Int.Min, TestConstants.Int.Bool.Min),
                ConvertTest.Create(TestConstants.Int.Min, TestConstants.Int.Char.Min),
                ConvertTest.Create(TestConstants.Int.Min, TestConstants.Int.SByte.Min),
                ConvertTest.Create(TestConstants.Int.Min, TestConstants.Int.Byte.Min),
                ConvertTest.Create(TestConstants.Int.Min, TestConstants.Int.Short.Min),
                ConvertTest.Create(TestConstants.Int.Min, TestConstants.Int.UShort.Min),
                ConvertTest.Create(TestConstants.Int.Min, TestConstants.Int.Min),
                ConvertTest.Create(TestConstants.Int.Min, TestConstants.Int.UInt.Min),
                ConvertTest.Create(TestConstants.Int.Min, TestConstants.Int.Long.Min),
                ConvertTest.Create(TestConstants.Int.Min, TestConstants.Int.ULong.Min),
                ConvertTest.Create(TestConstants.Int.Min, TestConstants.Int.Float.Min),
                ConvertTest.Create(TestConstants.Int.Min, TestConstants.Int.Double.Min),
                ConvertTest.Create(TestConstants.Int.Min, TestConstants.Int.Decimal.Min),

                ConvertTest.Create(TestConstants.Int.Max, TestConstants.Int.Bool.Max),
                ConvertTest.Create(TestConstants.Int.Max, TestConstants.Int.Char.Max),
                ConvertTest.Create(TestConstants.Int.Max, TestConstants.Int.SByte.Max),
                ConvertTest.Create(TestConstants.Int.Max, TestConstants.Int.Byte.Max),
                ConvertTest.Create(TestConstants.Int.Max, TestConstants.Int.Short.Max),
                ConvertTest.Create(TestConstants.Int.Max, TestConstants.Int.UShort.Max),
                ConvertTest.Create(TestConstants.Int.Max, TestConstants.Int.Max),
                ConvertTest.Create(TestConstants.Int.Max, TestConstants.Int.UInt.Max),
                ConvertTest.Create(TestConstants.Int.Max, TestConstants.Int.Long.Max),
                ConvertTest.Create(TestConstants.Int.Max, TestConstants.Int.ULong.Max),
                ConvertTest.Create(TestConstants.Int.Max, TestConstants.Int.Float.Max),
                ConvertTest.Create(TestConstants.Int.Max, TestConstants.Int.Double.Max),
                ConvertTest.Create(TestConstants.Int.Max, TestConstants.Int.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> UIntConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TestConstants.UInt.Default, TestConstants.UInt.Bool.Default),
                ConvertTest.Create(TestConstants.UInt.Default, TestConstants.UInt.Char.Default),
                ConvertTest.Create(TestConstants.UInt.Default, TestConstants.UInt.SByte.Default),
                ConvertTest.Create(TestConstants.UInt.Default, TestConstants.UInt.Byte.Default),
                ConvertTest.Create(TestConstants.UInt.Default, TestConstants.UInt.Short.Default),
                ConvertTest.Create(TestConstants.UInt.Default, TestConstants.UInt.UShort.Default),
                ConvertTest.Create(TestConstants.UInt.Default, TestConstants.UInt.Int.Default),
                ConvertTest.Create(TestConstants.UInt.Default, TestConstants.UInt.Default),
                ConvertTest.Create(TestConstants.UInt.Default, TestConstants.UInt.Long.Default),
                ConvertTest.Create(TestConstants.UInt.Default, TestConstants.UInt.ULong.Default),
                ConvertTest.Create(TestConstants.UInt.Default, TestConstants.UInt.Float.Default),
                ConvertTest.Create(TestConstants.UInt.Default, TestConstants.UInt.Double.Default),
                ConvertTest.Create(TestConstants.UInt.Default, TestConstants.UInt.Decimal.Default),

                ConvertTest.Create(TestConstants.UInt.Min, TestConstants.UInt.Bool.Min),
                ConvertTest.Create(TestConstants.UInt.Min, TestConstants.UInt.Char.Min),
                ConvertTest.Create(TestConstants.UInt.Min, TestConstants.UInt.SByte.Min),
                ConvertTest.Create(TestConstants.UInt.Min, TestConstants.UInt.Byte.Min),
                ConvertTest.Create(TestConstants.UInt.Min, TestConstants.UInt.Short.Min),
                ConvertTest.Create(TestConstants.UInt.Min, TestConstants.UInt.UShort.Min),
                ConvertTest.Create(TestConstants.UInt.Min, TestConstants.UInt.Int.Min),
                ConvertTest.Create(TestConstants.UInt.Min, TestConstants.UInt.Min),
                ConvertTest.Create(TestConstants.UInt.Min, TestConstants.UInt.Long.Min),
                ConvertTest.Create(TestConstants.UInt.Min, TestConstants.UInt.ULong.Min),
                ConvertTest.Create(TestConstants.UInt.Min, TestConstants.UInt.Float.Min),
                ConvertTest.Create(TestConstants.UInt.Min, TestConstants.UInt.Double.Min),
                ConvertTest.Create(TestConstants.UInt.Min, TestConstants.UInt.Decimal.Min),

                ConvertTest.Create(TestConstants.UInt.Max, TestConstants.UInt.Bool.Max),
                ConvertTest.Create(TestConstants.UInt.Max, TestConstants.UInt.Char.Max),
                ConvertTest.Create(TestConstants.UInt.Max, TestConstants.UInt.SByte.Max),
                ConvertTest.Create(TestConstants.UInt.Max, TestConstants.UInt.Byte.Max),
                ConvertTest.Create(TestConstants.UInt.Max, TestConstants.UInt.Short.Max),
                ConvertTest.Create(TestConstants.UInt.Max, TestConstants.UInt.UShort.Max),
                ConvertTest.Create(TestConstants.UInt.Max, TestConstants.UInt.Int.Max),
                ConvertTest.Create(TestConstants.UInt.Max, TestConstants.UInt.Max),
                ConvertTest.Create(TestConstants.UInt.Max, TestConstants.UInt.Long.Max),
                ConvertTest.Create(TestConstants.UInt.Max, TestConstants.UInt.ULong.Max),
                ConvertTest.Create(TestConstants.UInt.Max, TestConstants.UInt.Float.Max),
                ConvertTest.Create(TestConstants.UInt.Max, TestConstants.UInt.Double.Max),
                ConvertTest.Create(TestConstants.UInt.Max, TestConstants.UInt.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> LongConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TestConstants.Long.Default, TestConstants.Long.Bool.Default),
                ConvertTest.Create(TestConstants.Long.Default, TestConstants.Long.Char.Default),
                ConvertTest.Create(TestConstants.Long.Default, TestConstants.Long.SByte.Default),
                ConvertTest.Create(TestConstants.Long.Default, TestConstants.Long.Byte.Default),
                ConvertTest.Create(TestConstants.Long.Default, TestConstants.Long.Short.Default),
                ConvertTest.Create(TestConstants.Long.Default, TestConstants.Long.UShort.Default),
                ConvertTest.Create(TestConstants.Long.Default, TestConstants.Long.Int.Default),
                ConvertTest.Create(TestConstants.Long.Default, TestConstants.Long.UInt.Default),
                ConvertTest.Create(TestConstants.Long.Default, TestConstants.Long.Default),
                ConvertTest.Create(TestConstants.Long.Default, TestConstants.Long.ULong.Default),
                ConvertTest.Create(TestConstants.Long.Default, TestConstants.Long.Float.Default),
                ConvertTest.Create(TestConstants.Long.Default, TestConstants.Long.Double.Default),
                ConvertTest.Create(TestConstants.Long.Default, TestConstants.Long.Decimal.Default),

                ConvertTest.Create(TestConstants.Long.Min, TestConstants.Long.Bool.Min),
                ConvertTest.Create(TestConstants.Long.Min, TestConstants.Long.Char.Min),
                ConvertTest.Create(TestConstants.Long.Min, TestConstants.Long.SByte.Min),
                ConvertTest.Create(TestConstants.Long.Min, TestConstants.Long.Byte.Min),
                ConvertTest.Create(TestConstants.Long.Min, TestConstants.Long.Short.Min),
                ConvertTest.Create(TestConstants.Long.Min, TestConstants.Long.UShort.Min),
                ConvertTest.Create(TestConstants.Long.Min, TestConstants.Long.Int.Min),
                ConvertTest.Create(TestConstants.Long.Min, TestConstants.Long.UInt.Min),
                ConvertTest.Create(TestConstants.Long.Min, TestConstants.Long.Min),
                ConvertTest.Create(TestConstants.Long.Min, TestConstants.Long.ULong.Min),
                ConvertTest.Create(TestConstants.Long.Min, TestConstants.Long.Float.Min),
                ConvertTest.Create(TestConstants.Long.Min, TestConstants.Long.Double.Min),
                ConvertTest.Create(TestConstants.Long.Min, TestConstants.Long.Decimal.Min),

                ConvertTest.Create(TestConstants.Long.Max, TestConstants.Long.Bool.Max),
                ConvertTest.Create(TestConstants.Long.Max, TestConstants.Long.Char.Max),
                ConvertTest.Create(TestConstants.Long.Max, TestConstants.Long.SByte.Max),
                ConvertTest.Create(TestConstants.Long.Max, TestConstants.Long.Byte.Max),
                ConvertTest.Create(TestConstants.Long.Max, TestConstants.Long.Short.Max),
                ConvertTest.Create(TestConstants.Long.Max, TestConstants.Long.UShort.Max),
                ConvertTest.Create(TestConstants.Long.Max, TestConstants.Long.Int.Max),
                ConvertTest.Create(TestConstants.Long.Max, TestConstants.Long.UInt.Max),
                ConvertTest.Create(TestConstants.Long.Max, TestConstants.Long.Max),
                ConvertTest.Create(TestConstants.Long.Max, TestConstants.Long.ULong.Max),
                ConvertTest.Create(TestConstants.Long.Max, TestConstants.Long.Float.Max),
                ConvertTest.Create(TestConstants.Long.Max, TestConstants.Long.Double.Max),
                ConvertTest.Create(TestConstants.Long.Max, TestConstants.Long.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> ULongConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TestConstants.ULong.Default, TestConstants.ULong.Bool.Default),
                ConvertTest.Create(TestConstants.ULong.Default, TestConstants.ULong.Char.Default),
                ConvertTest.Create(TestConstants.ULong.Default, TestConstants.ULong.SByte.Default),
                ConvertTest.Create(TestConstants.ULong.Default, TestConstants.ULong.SByte.Default),
                ConvertTest.Create(TestConstants.ULong.Default, TestConstants.ULong.Short.Default),
                ConvertTest.Create(TestConstants.ULong.Default, TestConstants.ULong.UShort.Default),
                ConvertTest.Create(TestConstants.ULong.Default, TestConstants.ULong.Int.Default),
                ConvertTest.Create(TestConstants.ULong.Default, TestConstants.ULong.UInt.Default),
                ConvertTest.Create(TestConstants.ULong.Default, TestConstants.ULong.Long.Default),
                ConvertTest.Create(TestConstants.ULong.Default, TestConstants.ULong.Default),
                ConvertTest.Create(TestConstants.ULong.Default, TestConstants.ULong.Float.Default),
                ConvertTest.Create(TestConstants.ULong.Default, TestConstants.ULong.Double.Default),
                ConvertTest.Create(TestConstants.ULong.Default, TestConstants.ULong.Decimal.Default),

                ConvertTest.Create(TestConstants.ULong.Min, TestConstants.ULong.Bool.Min),
                ConvertTest.Create(TestConstants.ULong.Min, TestConstants.ULong.Char.Min),
                ConvertTest.Create(TestConstants.ULong.Min, TestConstants.ULong.SByte.Min),
                ConvertTest.Create(TestConstants.ULong.Min, TestConstants.ULong.Byte.Min),
                ConvertTest.Create(TestConstants.ULong.Min, TestConstants.ULong.Short.Min),
                ConvertTest.Create(TestConstants.ULong.Min, TestConstants.ULong.UShort.Min),
                ConvertTest.Create(TestConstants.ULong.Min, TestConstants.ULong.Int.Min),
                ConvertTest.Create(TestConstants.ULong.Min, TestConstants.ULong.UInt.Min),
                ConvertTest.Create(TestConstants.ULong.Min, TestConstants.ULong.Long.Min),
                ConvertTest.Create(TestConstants.ULong.Min, TestConstants.ULong.Min),
                ConvertTest.Create(TestConstants.ULong.Min, TestConstants.ULong.Float.Min),
                ConvertTest.Create(TestConstants.ULong.Min, TestConstants.ULong.Double.Min),
                ConvertTest.Create(TestConstants.ULong.Min, TestConstants.ULong.Decimal.Min),

                ConvertTest.Create(TestConstants.ULong.Max, TestConstants.ULong.Bool.Max),
                ConvertTest.Create(TestConstants.ULong.Max, TestConstants.ULong.Char.Max),
                ConvertTest.Create(TestConstants.ULong.Max, TestConstants.ULong.SByte.Max),
                ConvertTest.Create(TestConstants.ULong.Max, TestConstants.ULong.Byte.Max),
                ConvertTest.Create(TestConstants.ULong.Max, TestConstants.ULong.Short.Max),
                ConvertTest.Create(TestConstants.ULong.Max, TestConstants.ULong.UShort.Max),
                ConvertTest.Create(TestConstants.ULong.Max, TestConstants.ULong.Int.Max),
                ConvertTest.Create(TestConstants.ULong.Max, TestConstants.ULong.UInt.Max),
                ConvertTest.Create(TestConstants.ULong.Max, TestConstants.ULong.Long.Max),
                ConvertTest.Create(TestConstants.ULong.Max, TestConstants.ULong.Max),
                ConvertTest.Create(TestConstants.ULong.Max, TestConstants.ULong.Float.Max),
                ConvertTest.Create(TestConstants.ULong.Max, TestConstants.ULong.Double.Max),
                ConvertTest.Create(TestConstants.ULong.Max, TestConstants.ULong.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> FloatConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TestConstants.Float.Default, TestConstants.Float.Bool.Default),
                ConvertTest.Create(TestConstants.Float.Default, TestConstants.Float.Char.Default),
                ConvertTest.Create(TestConstants.Float.Default, TestConstants.Float.SByte.Default),
                ConvertTest.Create(TestConstants.Float.Default, TestConstants.Float.Byte.Default),
                ConvertTest.Create(TestConstants.Float.Default, TestConstants.Float.Short.Default),
                ConvertTest.Create(TestConstants.Float.Default, TestConstants.Float.UShort.Default),
                ConvertTest.Create(TestConstants.Float.Default, TestConstants.Float.Int.Default),
                ConvertTest.Create(TestConstants.Float.Default, TestConstants.Float.UInt.Default),
                ConvertTest.Create(TestConstants.Float.Default, TestConstants.Float.Long.Default),
                ConvertTest.Create(TestConstants.Float.Default, TestConstants.Float.ULong.Default),
                ConvertTest.Create(TestConstants.Float.Default, TestConstants.Float.Default),
                ConvertTest.Create(TestConstants.Float.Default, TestConstants.Float.Double.Default),
                ConvertTest.Create(TestConstants.Float.Default, TestConstants.Float.Decimal.Default),

                ConvertTest.Create(TestConstants.Float.Min, TestConstants.Float.Bool.Min),
                ConvertTest.Create(TestConstants.Float.Min, TestConstants.Float.Char.Min),
                ConvertTest.Create(TestConstants.Float.Min, TestConstants.Float.SByte.Min),
                ConvertTest.Create(TestConstants.Float.Min, TestConstants.Float.Byte.Min),
                ConvertTest.Create(TestConstants.Float.Min, TestConstants.Float.Short.Min),
                ConvertTest.Create(TestConstants.Float.Min, TestConstants.Float.UShort.Min),
                ConvertTest.Create(TestConstants.Float.Min, TestConstants.Float.Int.Min),
                ConvertTest.Create(TestConstants.Float.Min, TestConstants.Float.UInt.Min),
                ConvertTest.Create(TestConstants.Float.Min, TestConstants.Float.Long.Min),
                ConvertTest.Create(TestConstants.Float.Min, TestConstants.Float.ULong.Min),
                ConvertTest.Create(TestConstants.Float.Min, TestConstants.Float.Min),
                ConvertTest.Create(TestConstants.Float.Min, TestConstants.Float.Double.Min),
                //TODO: Assert Throws
                //ConvertTest.Create(TestConstants.Float.Min, TestConstants.Float.Decimal.Min),

                ConvertTest.Create(TestConstants.Float.Max, TestConstants.Float.Bool.Max),
                ConvertTest.Create(TestConstants.Float.Max, TestConstants.Float.Char.Max),
                ConvertTest.Create(TestConstants.Float.Max, TestConstants.Float.SByte.Max),
                ConvertTest.Create(TestConstants.Float.Max, TestConstants.Float.Byte.Max),
                ConvertTest.Create(TestConstants.Float.Max, TestConstants.Float.Short.Max),
                ConvertTest.Create(TestConstants.Float.Max, TestConstants.Float.UShort.Max),
                ConvertTest.Create(TestConstants.Float.Max, TestConstants.Float.Int.Max),
                ConvertTest.Create(TestConstants.Float.Max, TestConstants.Float.UInt.Max),
                ConvertTest.Create(TestConstants.Float.Max, TestConstants.Float.Long.Max),
                ConvertTest.Create(TestConstants.Float.Max, TestConstants.Float.ULong.Max),
                ConvertTest.Create(TestConstants.Float.Max, TestConstants.Float.Max),
                ConvertTest.Create(TestConstants.Float.Max, TestConstants.Float.Double.Max),
                //TODO: Assert Throws
                //ConvertTest.Create(TestConstants.Float.Max, TestConstants.Float.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> DoubleConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TestConstants.Double.Default, TestConstants.Double.Bool.Default),
                ConvertTest.Create(TestConstants.Double.Default, TestConstants.Double.Char.Default),
                ConvertTest.Create(TestConstants.Double.Default, TestConstants.Double.SByte.Default),
                ConvertTest.Create(TestConstants.Double.Default, TestConstants.Double.Byte.Default),
                ConvertTest.Create(TestConstants.Double.Default, TestConstants.Double.Short.Default),
                ConvertTest.Create(TestConstants.Double.Default, TestConstants.Double.UShort.Default),
                ConvertTest.Create(TestConstants.Double.Default, TestConstants.Double.Int.Default),
                ConvertTest.Create(TestConstants.Double.Default, TestConstants.Double.UInt.Default),
                ConvertTest.Create(TestConstants.Double.Default, TestConstants.Double.Long.Default),
                ConvertTest.Create(TestConstants.Double.Default, TestConstants.Double.ULong.Default),
                ConvertTest.Create(TestConstants.Double.Default, TestConstants.Double.Float.Default),
                ConvertTest.Create(TestConstants.Double.Default, TestConstants.Double.Default),
                ConvertTest.Create(TestConstants.Double.Default, TestConstants.Double.Decimal.Default),

                ConvertTest.Create(TestConstants.Double.Min, TestConstants.Double.Bool.Min),
                ConvertTest.Create(TestConstants.Double.Min, TestConstants.Double.Char.Min),
                ConvertTest.Create(TestConstants.Double.Min, TestConstants.Double.SByte.Min),
                ConvertTest.Create(TestConstants.Double.Min, TestConstants.Double.Byte.Min),
                ConvertTest.Create(TestConstants.Double.Min, TestConstants.Double.Short.Min),
                ConvertTest.Create(TestConstants.Double.Min, TestConstants.Double.UShort.Min),
                ConvertTest.Create(TestConstants.Double.Min, TestConstants.Double.Int.Min),
                ConvertTest.Create(TestConstants.Double.Min, TestConstants.Double.UInt.Min),
                ConvertTest.Create(TestConstants.Double.Min, TestConstants.Double.Long.Min),
                ConvertTest.Create(TestConstants.Double.Min, TestConstants.Double.ULong.Min),
                ConvertTest.Create(TestConstants.Double.Min, TestConstants.Double.Float.Min),
                ConvertTest.Create(TestConstants.Double.Min, TestConstants.Double.Min),
                //TODO: Assert Throws
                //ConvertTest.Create(TestConstants.Double.Min, TestConstants.Double.Decimal.Min),

                ConvertTest.Create(TestConstants.Double.Max, TestConstants.Double.Bool.Max),
                ConvertTest.Create(TestConstants.Double.Max, TestConstants.Double.Char.Max),
                ConvertTest.Create(TestConstants.Double.Max, TestConstants.Double.SByte.Max),
                ConvertTest.Create(TestConstants.Double.Max, TestConstants.Double.Byte.Max),
                ConvertTest.Create(TestConstants.Double.Max, TestConstants.Double.Short.Max),
                ConvertTest.Create(TestConstants.Double.Max, TestConstants.Double.UShort.Max),
                ConvertTest.Create(TestConstants.Double.Max, TestConstants.Double.Int.Max),
                ConvertTest.Create(TestConstants.Double.Max, TestConstants.Double.UInt.Max),
                ConvertTest.Create(TestConstants.Double.Max, TestConstants.Double.Long.Max),
                ConvertTest.Create(TestConstants.Double.Max, TestConstants.Double.ULong.Max),
                ConvertTest.Create(TestConstants.Double.Max, TestConstants.Double.Float.Max),
                ConvertTest.Create(TestConstants.Double.Max, TestConstants.Double.Max),
                //TODO: Assert Throws
                //ConvertTest.Create(TestConstants.Double.Max, TestConstants.Double.Decimal.Max),
            };

        public static IEnumerable<IConvertTest> DecimalConvertTests =>
            new List<IConvertTest>
            {
                ConvertTest.Create(TestConstants.Decimal.Default, TestConstants.Decimal.Bool.Default),
                ConvertTest.Create(TestConstants.Decimal.Default, TestConstants.Decimal.Char.Default),
                ConvertTest.Create(TestConstants.Decimal.Default, TestConstants.Decimal.SByte.Default),
                ConvertTest.Create(TestConstants.Decimal.Default, TestConstants.Decimal.Byte.Default),
                ConvertTest.Create(TestConstants.Decimal.Default, TestConstants.Decimal.Short.Default),
                ConvertTest.Create(TestConstants.Decimal.Default, TestConstants.Decimal.UShort.Default),
                ConvertTest.Create(TestConstants.Decimal.Default, TestConstants.Decimal.Int.Default),
                ConvertTest.Create(TestConstants.Decimal.Default, TestConstants.Decimal.UInt.Default),
                ConvertTest.Create(TestConstants.Decimal.Default, TestConstants.Decimal.Long.Default),
                ConvertTest.Create(TestConstants.Decimal.Default, TestConstants.Decimal.ULong.Default),
                ConvertTest.Create(TestConstants.Decimal.Default, TestConstants.Decimal.Float.Default),
                ConvertTest.Create(TestConstants.Decimal.Default, TestConstants.Decimal.Double.Default),
                ConvertTest.Create(TestConstants.Decimal.Default, TestConstants.Decimal.Default),

                ConvertTest.Create(TestConstants.Decimal.Min, TestConstants.Decimal.Bool.Min),
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
                ConvertTest.Create(TestConstants.Decimal.Min, TestConstants.Decimal.Float.Min),
                ConvertTest.Create(TestConstants.Decimal.Min, TestConstants.Decimal.Double.Min),
                ConvertTest.Create(TestConstants.Decimal.Min, TestConstants.Decimal.Min),

                ConvertTest.Create(TestConstants.Decimal.Max, TestConstants.Decimal.Bool.Max),
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
                ConvertTest.Create(TestConstants.Decimal.Max, TestConstants.Decimal.Float.Max),
                ConvertTest.Create(TestConstants.Decimal.Max, TestConstants.Decimal.Double.Max),
                ConvertTest.Create(TestConstants.Decimal.Max, TestConstants.Decimal.Max),
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
