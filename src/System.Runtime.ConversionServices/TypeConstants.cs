using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace System.Runtime.ConversionServices
{
    public class TypeConstantInfo<T> : TypeConstantInfo
    {
        public TypeConstantInfo(T t, T min, T max, Type type) : base(t, min, max, type) { }
        public T DefaultValue => (T)Default;
        public T MinValue => (T)base.Min;
        public T MaxValue => (T)base.Max;
    }

    public class TypeConstantInfo
    {
        public object Default { get; }
        public object Min { get; }
        public object Max { get; }
        public Type Type { get; }
        public TypeConstantInfo(object obj, object minValue, object maxValue, Type type)
        {
            Default = obj; Min = (minValue ?? obj); Max = maxValue ?? obj; Type = type;
        }

        public object[] GetDefaultValues() => Default.To(Type).Equals(Max.To(Type))
            ? new[] { Default } : new[] { Default, Min, Max };


        public static TypeConstantInfo<T> ToTyped<T>(TypeConstantInfo constantInfo)
            => new TypeConstantInfo<T>((T)constantInfo.Default, (T)constantInfo.Min, (T)constantInfo.Max, constantInfo.Type);

        private static TypeConstantInfo Create(object defaultValue, object minValue, object maxValue, Type type)
        {
            var min = minValue ?? defaultValue;
            var max = maxValue ?? defaultValue;
            var generic = typeof(TypeConstantInfo<>).MakeGenericType(defaultValue.GetType());
            return (TypeConstantInfo)Activator.CreateInstance(generic, new object[] { defaultValue, min, max,type });

        }
        private static TypeConstantInfo Create<T>(T defaultValue, T minValue, T maxValue, Type type)
        {
            if (typeof(T) == typeof(object))
            {
                var generic = typeof(TypeConstantInfo<>).MakeGenericType(defaultValue.GetType());
                return (TypeConstantInfo)Activator.
                    CreateInstance(generic, new object[] { defaultValue, minValue, maxValue, type });
            }
            else
            {
                return new TypeConstantInfo<T>(defaultValue, minValue, maxValue, type);
            }
        }

        public static List<TypeConstantInfo> GetTypeConstants()
        {
            var constantsType = typeof(TypeConstants);
            var result = constantsType
                .GetNestedTypes()
                .Where(x => x.IsClass)
                .Select(x => GetConstants(x))
                .ToList();
            return result;
        }
        private static TypeConstantInfo GetConstants(Type x)
        {
            var defaultValue = x.GetField(nameof(TypeConstants.Bool.Default)).GetValue(null);
            var minValue = x.GetField(nameof(TypeConstants.Bool.Min))?.GetValue(null);
            var maxValue = x.GetField(nameof(TypeConstants.Bool.Max))?.GetValue(null);
            var type = (Type)x.GetField(nameof(TypeConstants.Bool.Type)).GetValue(null);
            var result = TypeConstantInfo.Create(defaultValue, minValue, maxValue, type);
            return result;

        }
    }


    public static class TypeConstants
    {
        public interface ITypeConstant { }

        public class Bool : ITypeConstant
        {
            public const bool Default = default(bool);
            public const bool Min = default(bool);
            public const bool Max = true;
            public static readonly Type Type = Default.GetType();
            public class Char
            {
                public const char Default = TypeConstants.Char.Default;
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
            public static readonly Type Type = Default.GetType();
            public class Bool
            {
                public const bool Default = TypeConstants.Bool.Default;
                public const bool Min = TypeConstants.Bool.Min;
                public const bool Max = TypeConstants.Bool.Max;
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
            public static readonly Type Type = Default.GetType();
            public class Bool
            {
                public const bool Default = TypeConstants.Bool.Default;
                public const bool Min = TypeConstants.Bool.Max;
                public const bool Max = TypeConstants.Bool.Max;
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
            public static readonly Type Type = Default.GetType();
            public class Bool
            {
                public const bool Default = TypeConstants.Bool.Default;
                public const bool Min = TypeConstants.Bool.Min;
                public const bool Max = TypeConstants.Bool.Max;
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
            public static readonly Type Type = Default.GetType();
            public class Bool
            {
                public const bool Default = TypeConstants.Bool.Default;
                public const bool Min = TypeConstants.Bool.Max;
                public const bool Max = TypeConstants.Bool.Max;
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
            public static readonly Type Type = Default.GetType();
            public class Bool
            {
                public const bool Default = TypeConstants.Bool.Default;
                public const bool Min = TypeConstants.Bool.Min;
                public const bool Max = TypeConstants.Bool.Max;
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
            public static readonly Type Type = Default.GetType();
            public class Bool
            {
                public const bool Default = TypeConstants.Bool.Default;
                public const bool Min = TypeConstants.Bool.Max;
                public const bool Max = TypeConstants.Bool.Max;
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
            public static readonly Type Type = Default.GetType();
            public class Bool
            {
                public const bool Default = TypeConstants.Bool.Default;
                public const bool Min = TypeConstants.Bool.Min;
                public const bool Max = TypeConstants.Bool.Max;
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
            public static readonly Type Type = Default.GetType();
            public class Bool
            {
                public const bool Default = TypeConstants.Bool.Default;
                public const bool Min = TypeConstants.Bool.Max;
                public const bool Max = TypeConstants.Bool.Max;
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
            public static readonly Type Type = Default.GetType();
            public class Bool
            {
                public const bool Default = TypeConstants.Bool.Default;
                public const bool Min = TypeConstants.Bool.Min;
                public const bool Max = TypeConstants.Bool.Max;
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
            public static readonly Type Type = Default.GetType();
            public class Bool
            {
                public const bool Default = TypeConstants.Bool.Default;
                public const bool Min = TypeConstants.Bool.Max;
                public const bool Max = TypeConstants.Bool.Max;
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
            public static readonly Type Type = Default.GetType();
            public class Bool
            {
                public const bool Default = TypeConstants.Bool.Default;
                public const bool Min = TypeConstants.Bool.Max;
                public const bool Max = TypeConstants.Bool.Max;
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
            public static readonly Type Type = Default.GetType();
            public class Bool
            {
                public const bool Default = TypeConstants.Bool.Default;
                public const bool Min = TypeConstants.Bool.Max;
                public const bool Max = TypeConstants.Bool.Max;
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

        public class BigInt
        {
            public static readonly BigInteger Default = default(BigInteger);
            //public static readonly BigInteger Min = BigInteger.;
            //public static readonly BigInteger Max = BigInteger.MaxValue;
            public static readonly Type Type = Default.GetType();
        }
    }
}
