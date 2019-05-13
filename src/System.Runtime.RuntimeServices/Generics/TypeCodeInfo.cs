using System;

namespace System.Runtime.RuntimeServices
{
    public struct TypeCodeInfo<T> : ITypeCodeInfo
    {
        //public static readonly T Default = default(T);
        public static Type TType => typeof(T);
        public static Type ArrayType => TType.MakeArrayType();
        public static Type MakeGenericType(Type type) => TType.MakeGenericType(type);
        public static int TypeCodeValue = (int)TypeCode;
        public static TypeCode TypeCode = Type.GetTypeCode(typeof(T));

        public T Default => default;

        object ITypeCodeInfo.Default => Default;

        public static Func<T, TOther, IRuntimeTypedReference> AdditionOperator<TOther>()
        {
            return GenericBinaryOp<T, TOther>.FnAdd;
        }

        public IBinaryOperator OpAdd<TOther>()
        {
            return new GenericBinaryOp<T, TOther>();
        }

    }

}
