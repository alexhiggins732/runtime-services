using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticConversionsGenerator
{
    public class GenericArithmetic
    {
        public static void Test()
        {

            GenericAdder<int, int, int>.FnAdd = (a, b) => a + b;
            GenericAdd<int, int>.FnAdd = (a, b) => new RuntimeReference<int> { Value = a + b };


            var result = GenericAdder<int, int, int>.Add(1, 2);

            var numerics = TypeCodeInfoFactory.NumericTypeCodeInfos;

            foreach (var source in numerics)
            {
                foreach (var dest in numerics)
                {
                    var additionResult = GenericAdder.Add(source.Default, dest.Default);
                }
            }



            sbyte i1 = 0;
            short i2 = 0;
            int i4 = 1;
            long i8 = 0;

            byte u1 = 0;
            ushort u2 = 0;
            uint u4 = 1;
            ulong iu8 = 0;

            var i1i2 = i1 + i2;
            var resultI1I2 = GenericAdder<int, int, int>.Add(i1, i2);


            var i2i1 = i2 + i1;

            var i1i4 = i1 + i4;
            var i1i8 = i1 + i8;

            var i2i4 = i2 + i4;
            var i2i8 = i2 + i8;

            var i4i4 = i4 + i4;
            var i4i8 = i4 + i8;

        }
    }
    public interface IRuntimeReference { }
    public interface IGenericAdder
    {
        IRuntimeReference Add(IRuntimeReference a, IRuntimeReference b);
    }
    public struct RuntimeReference<T> : IRuntimeReference
    {
        public T Value;
        public static implicit operator RuntimeReference<T>(T value) => new RuntimeReference<T> { Value = value };
    }

    public struct GenericAdder
    {
        public static IRuntimeReference Add(object a, object b)
        {
            var tca = TypeCodeInfoFactory.GetTypeCodeInfo(a.GetType());
            var tcb = TypeCodeInfoFactory.GetTypeCodeInfo(b.GetType());
            return null;
            //return tca.OpAdd<tcb.Default>().OpAdd(a, b);
            //tca.Add(b);

        }
    }

    public struct GenericAdder<T1, T2, TResult>
    //: IGenericAdder
    {
        public static Func<T1, T2, TResult> FnAdd = null;

        //Support for runtime boxed objects
        public static IRuntimeReference Add(IRuntimeReference a, IRuntimeReference b)
        {
            var refa = (RuntimeReference<T1>)(object)a;
            var refb = (RuntimeReference<T2>)(object)b;
            return (IRuntimeReference)Add(refa, refb);
        }

        //Support for statically compiled linkage
        public static TResult Add(RuntimeReference<T1> a, RuntimeReference<T2> b)
        {
            if (FnAdd != null)
                return FnAdd(a.Value, b.Value);
            return default(TResult);
        }

        //not sure if this is needed.
        public static RuntimeReference<TResult>
            AddRef(RuntimeReference<T1> a, RuntimeReference<T2> b)
        {
            //return default(RuntimeReference<TResult>);
            if (FnAdd != null)
                return new RuntimeReference<TResult> { Value = FnAdd(a.Value, b.Value) };
            return default(RuntimeReference<TResult>);
        }
    }

    public struct GenericAdd<T1, T2> : IBinaryOperator
    {
        public static Func<T1, T2, IRuntimeReference> FnAdd = null;
        public IRuntimeReference OpAdd<TIn1,TIn2> (TIn1 a, TIn2 b) 
            => FnAdd((T1)(object)a, (T2)(object)b);

   
    }

    public interface IBinaryOperator
    {
        IRuntimeReference OpAdd<T1, T2>(T1 a, T2 b);
    }

    public interface ITypeCodeInfo
    {
        object Default { get; }
        IBinaryOperator OpAdd<TOther>();
    }
    public struct TypeCodeInfoFactory
    {
        public static Dictionary<TypeCode, ITypeCodeInfo> TypeCodeInfoCache;
        public static readonly TypeCode[] NonNumericTypeCodes;
        public static readonly TypeCode[] NumericTypeCodes;
        public static readonly TypeCode[] AllTypeCodes;
        public static ITypeCodeInfo[] NumericTypeCodeInfos =>
            TypeCodeInfoCache
            .Where(x => NumericTypeCodes.Contains(x.Key)).
            Select(x => x.Value)
            .ToArray();

        static TypeCodeInfoFactory()
        {
            TypeCodeInfoCache = new Dictionary<TypeCode, ITypeCodeInfo>
            {
                //
                // Summary:
                //     A null reference.
                //{TypeCode.Empty,  = 0,
                //
                // Summary:
                //     A general type representing any reference or value type not explicitly represented
                //     by another TypeCode.
                { TypeCode.Object, new TypeCodeInfo<object>()  }, // {TypeCodeValue=1 } }, //= 1,
                //
                //// Summary:
                ////     A database null (column) value.
                { TypeCode.DBNull, new TypeCodeInfo<DBNull>()  },//DBNull = 2,
                ////
                //// Summary:
                ////     A simple type representing Boolean values of true or false.
                { TypeCode.Boolean, new TypeCodeInfo<bool>()  },//Boolean = 3,
                ////
                //// Summary:
                ////     An integral type representing unsigned 16-bit integers with values between 0
                ////     and 65535. The set of possible values for the System.TypeCode.Char type corresponds
                ////     to the Unicode character set.
                { TypeCode.Char, new TypeCodeInfo<char>()  },//Char = 4,
                ////
                //// Summary:
                ////     An integral type representing signed 8-bit integers with values between -128
                ////     and 127.
                 { TypeCode.SByte, new TypeCodeInfo<sbyte>()  },//SByte = 5,
                ////
                //// Summary:
                ////     An integral type representing unsigned 8-bit integers with values between 0 and
                ////     255.
                 { TypeCode.Byte, new TypeCodeInfo<byte>()  },//Byte = 6,
                ////
                //// Summary:
                ////     An integral type representing signed 16-bit integers with values between -32768
                ////     and 32767.
                 { TypeCode.Int16, new TypeCodeInfo<short>()  },//Int16 = 7,
                ////
                //// Summary:
                ////     An integral type representing unsigned 16-bit integers with values between 0
                ////     and 65535.
                 { TypeCode.UInt16, new TypeCodeInfo<ushort>()  },//UInt16 = 8,
                ////
                //// Summary:
                ////     An integral type representing signed 32-bit integers with values between -2147483648
                ////     and 2147483647.
                 { TypeCode.Int32, new TypeCodeInfo<int>()  },//Int32 = 9,
                ////
                //// Summary:
                ////     An integral type representing unsigned 32-bit integers with values between 0
                ////     and 4294967295.
                 { TypeCode.UInt32, new TypeCodeInfo<uint>()  },//UInt32 = 10,
                ////
                //// Summary:
                ////     An integral type representing signed 64-bit integers with values between -9223372036854775808
                ////     and 9223372036854775807.
                 { TypeCode.Int64, new TypeCodeInfo<long>()  },//Int64 = 11,
                ////
                //// Summary:
                ////     An integral type representing unsigned 64-bit integers with values between 0
                ////     and 18446744073709551615.
                 { TypeCode.UInt64, new TypeCodeInfo<ulong>()  },//UInt64 = 12,
                ////
                //// Summary:
                ////     A floating point type representing values ranging from approximately 1.5 x 10
                ////     -45 to 3.4 x 10 38 with a precision of 7 digits.
                 { TypeCode.Single, new TypeCodeInfo<float>()  },//Single = 13,
                ////
                //// Summary:
                ////     A floating point type representing values ranging from approximately 5.0 x 10
                ////     -324 to 1.7 x 10 308 with a precision of 15-16 digits.
                 { TypeCode.Double, new TypeCodeInfo<double>()  },//Double = 14,
                ////
                //// Summary:
                ////     A simple type representing values ranging from 1.0 x 10 -28 to approximately
                ////     7.9 x 10 28 with 28-29 significant digits.
                 { TypeCode.Decimal, new TypeCodeInfo<decimal>()  },//Decimal = 15,
                ////
                //// Summary:
                ////     A type representing a date and time value.
                 { TypeCode.DateTime, new TypeCodeInfo<DateTime>()  },//DateTime = 16,
                ////
                //// Summary:
                ////     A sealed class type representing Unicode character strings.
                 { TypeCode.String, new TypeCodeInfo<string>()  },//String = 18
            };
            NonNumericTypeCodes = new[]
            {
                TypeCode.Empty,
                TypeCode.Object,
                TypeCode.DBNull,
                TypeCode.DateTime,
                TypeCode.String,
            };
            NumericTypeCodes = TypeCodeInfoCache.Keys.Except(NonNumericTypeCodes).ToArray();
            AllTypeCodes = (new[] { TypeCode.Empty }).Concat(TypeCodeInfoCache.Keys).ToArray();

        }

        public static ITypeCodeInfo GetTypeCodeInfo(Type type)
            => GetTypeCodeInfo(Type.GetTypeCode(type));

        public static ITypeCodeInfo GetTypeCodeInfo(TypeCode typeCode)
        {
            return TypeCodeInfoCache[typeCode];
        }
    }
    public struct TypeCodeInfo<T> : ITypeCodeInfo
    {
        //public static readonly T Default = default(T);
        public static Type TType => typeof(T);
        public static Type ArrayType => TType.MakeArrayType();
        public static Type MakeGenericType(Type type) => TType.MakeGenericType(type);
        public static int TypeCodeValue = (int)TypeCode;
        public static TypeCode TypeCode = Type.GetTypeCode(typeof(T));

        public T Default => default(T);

        object ITypeCodeInfo.Default => Default;

        public static Func<T, TOther, IRuntimeReference> AdditionOperator<TOther>()
        {
            return GenericAdd<T, TOther>.FnAdd;
        }

        public IBinaryOperator OpAdd<TOther>()
        {
            return new GenericAdd<T, TOther>();
        }

    }

}
