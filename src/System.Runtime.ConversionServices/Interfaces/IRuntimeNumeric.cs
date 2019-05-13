using System.Globalization;
using System.Numerics;

namespace System.Runtime.ConversionServices.Interfaces
{


    public interface IRuntimeNumericSigned
    {
        IRuntimeNumeric MinusOne { get; }
        int Sign { get; }
        IRuntimeNumeric Abs(IRuntimeNumeric value);
        IRuntimeNumeric Op_Negate(IRuntimeNumeric value); //only for signed
    }
    public interface IRuntimeNumericMinMax
    {
        IRuntimeNumeric MinValue { get; }
        IRuntimeNumeric MaxValue { get; }

    }
    //TODO: Should we define dpecific implementations, EG INumericI8,INumericI16,INumericI32,INumericI32,INumericU8, etc,
    //TODO: If so should they be bound to corresponding system types? 
    //      Not sure of the benefit given that for every operator, be it cast/convert/compare, or any other.
    //      there is always atleast one exception thrown by one combination of the numbers.
    //      otherwise we wouldn't need to wrap them in an interface in the first place.
    //      additionally, the baked in system operands actually don't exist at runtime.
    //      Then need to be referenced at compile time because they the calls operands themselves are handled in
    //      in native code.


    public interface IRuntimeNumeric :
        IRuntimeTypedReference, IComparable,
        IRuntimeComparable, IRuntimeEquatable,
        IComparable<IRuntimeNumeric>, IEquatable<IRuntimeNumeric>,
        //TODO: Implement IComparable<T>, IEquatable<T>, for the Reference <T>
        //TODO: Implement IComparable<IRuntimeTypedReference>, IEquatable<IRuntimeTypedReference>, for the Reference IRuntimeTypedReference
        IFormattable //TODO: need implementation.
    {
        // TODO: Define a constructor operator name
        //  New() is already in use by GenericTypeDefinition as a generic constructor.
        // TODO: Define an interface that can be wired to actual CTOR calls.
        // TODO: These NewFrom definition are not valid CTOR args for all numeric types.
        // TODO: Define string constructor interface method even if it passes through to the Parse methods.
        IRuntimeNumeric NewFrom(byte[] value);//TODO:
        IRuntimeNumeric NewFrom(sbyte value);
        IRuntimeNumeric NewFrom(byte value);
        IRuntimeNumeric NewFrom(short value);
        IRuntimeNumeric NewFrom(ushort value);
        IRuntimeNumeric NewFrom(int value);
        IRuntimeNumeric NewFrom(uint value);
        IRuntimeNumeric NewFrom(long value);
        IRuntimeNumeric NewFrom(ulong value);
        IRuntimeNumeric NewFrom(decimal value);
        IRuntimeNumeric NewFrom(double value);
        IRuntimeNumeric NewFrom(float value);
        IRuntimeNumeric NewFrom(BigInteger value);
        IRuntimeNumeric NewFrom(IRuntimeNumeric value);
        IRuntimeNumeric NewFrom(IRuntimeTypedReference value);

        //TODO: wire up as static readonly or constants as applicable.
        IRuntimeNumeric One { get; } // these shold be constants but interfaces don't allow. not sure of the benefits here
        IRuntimeNumeric Zero { get; } // these shold be constants but interfaces don't allow. 


        bool IsOne { get; } // not sure of the benefits here
        bool IsZero { get; } // not sure of the benefits here
        bool IsPowerOfTwo { get; } // not sure of the benefits here
        bool IsEven { get; } // not sure of the benefits here


        //TODO: decide on named operators vs opcode names vs op_[name]
        //      If we are using reflection we probably want to try to bind
        //      to op_[Name] and then try provide an implementation either via interface, compiled lamba or dynamics (DLR);

        // TODO: Define convention for operator names as structs<T1,T2,Op> will be wired by dependency injection.
        //      to allow user defined operator hooks for certian unary and binary expressions.

        IRuntimeNumeric Add(IRuntimeNumeric left, IRuntimeNumeric right);
  
        IRuntimeNumeric Divide(IRuntimeNumeric dividend, IRuntimeNumeric divisor);
        IRuntimeNumeric DivRem(IRuntimeNumeric dividend, IRuntimeNumeric divisor, out IRuntimeNumeric remainder);
        IRuntimeNumeric GreatestCommonDivisor(IRuntimeNumeric left, IRuntimeNumeric right);
        double Log(IRuntimeNumeric value, double baseValue);
        double Log(IRuntimeNumeric value);
        double Log10(IRuntimeNumeric value);
        IRuntimeNumeric Max(IRuntimeNumeric left, IRuntimeNumeric right);
        IRuntimeNumeric Min(IRuntimeNumeric left, IRuntimeNumeric right);
        //not sure if we need to back this in.
        IRuntimeNumeric ModPow(IRuntimeNumeric value, IRuntimeNumeric exponent, IRuntimeNumeric modulus);
        IRuntimeNumeric Multiply(IRuntimeNumeric left, IRuntimeNumeric right);
        IRuntimeNumeric Negate(IRuntimeNumeric value);
        IRuntimeNumeric Subtract(IRuntimeNumeric left, IRuntimeNumeric right);


        //not sure if we need to bake this in.
        IRuntimeNumeric Pow(IRuntimeNumeric value, int exponent);
        IRuntimeNumeric Remainder(IRuntimeNumeric dividend, IRuntimeNumeric divisor);



        //account for localization and globalization.....
        IRuntimeNumeric Parse(string value, NumberStyles style);
        IRuntimeNumeric Parse(string value);
        IRuntimeNumeric Parse(string value, IFormatProvider provider);
        IRuntimeNumeric Parse(string value, NumberStyles style, IFormatProvider provider);
        bool TryParse(string value, NumberStyles style, IFormatProvider provider, out IRuntimeNumeric result);
        bool TryParse(string value, out IRuntimeNumeric result);
        string ToString(IFormatProvider provider);
        string ToString(string format);
        string ToString();
        //string ToString(string format, IFormatProvider provider);


        //Comparable
        int Compare(IRuntimeNumeric left, IRuntimeNumeric right);

        //Comparer
        int CompareTo(long other);
        int CompareTo(ulong other);
        //int CompareTo(IRuntimeNumeric other);
        int CompareTo(IRuntimeTypedReference other);
        //int CompareTo(object obj);

        //Equality
        bool Equals(long other);
        //bool Equals(IRuntimeNumeric other);
        bool Equals(IRuntimeTypedReference other);
        bool Equals(object obj);
        bool Equals(ulong other);


        int GetHashCode();
        byte[] ToByteArray();//TODO:




        //https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/equality-operators
        //TODO: Tie interfaces to opcodes
        //TODO: Add additional MsilOperators (Add_Unchecked, OVF, OFV_UN, etc.)
        //IRuntimeNumeric operator +(IRuntimeNumeric value);
        //IRuntimeNumeric operator +(IRuntimeNumeric left, IRuntimeNumeric right);
        //IRuntimeNumeric operator -(IRuntimeNumeric value);
        //IRuntimeNumeric operator -(IRuntimeNumeric left, IRuntimeNumeric right);
        //IRuntimeNumeric operator ~(IRuntimeNumeric value);
        //IRuntimeNumeric operator ++(IRuntimeNumeric value);
        //IRuntimeNumeric operator --(IRuntimeNumeric value);
        //IRuntimeNumeric operator *(IRuntimeNumeric left, IRuntimeNumeric right);
        //IRuntimeNumeric operator /(IRuntimeNumeric dividend, IRuntimeNumeric divisor);
        //IRuntimeNumeric operator %(IRuntimeNumeric dividend, IRuntimeNumeric divisor);
        //IRuntimeNumeric operator &(IRuntimeNumeric left, IRuntimeNumeric right);
        //IRuntimeNumeric operator |(IRuntimeNumeric left, IRuntimeNumeric right);
        //IRuntimeNumeric operator ^(IRuntimeNumeric left, IRuntimeNumeric right);
        //IRuntimeNumeric operator <<(IRuntimeNumeric value, int shift);
        //IRuntimeNumeric operator >>(IRuntimeNumeric value, int shift);


        // Operators
        IRuntimeNumeric Op_UnaryPlus(IRuntimeNumeric value);
        IRuntimeNumeric Op_Addition(IRuntimeNumeric left, IRuntimeNumeric right);
        IRuntimeNumeric Op_Subtract(IRuntimeNumeric left, IRuntimeNumeric right);
        /// <summary>
        /// Bitwise complement operator ~ (negate)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IRuntimeNumeric Op_OnesComplement(IRuntimeNumeric value);
        IRuntimeNumeric Op_Increment(IRuntimeNumeric value);
        IRuntimeNumeric Op_Decrement(IRuntimeNumeric value);
        IRuntimeNumeric Op_Multiply(IRuntimeNumeric left, IRuntimeNumeric right);
        IRuntimeNumeric Op_Divide(IRuntimeNumeric dividend, IRuntimeNumeric divisor);
        IRuntimeNumeric Op_Rem(IRuntimeNumeric dividend, IRuntimeNumeric divisor);
        IRuntimeNumeric Op_And(IRuntimeNumeric left, IRuntimeNumeric right);
        IRuntimeNumeric Op_Or(IRuntimeNumeric left, IRuntimeNumeric right);
        IRuntimeNumeric Op_Xor(IRuntimeNumeric left, IRuntimeNumeric right);
        IRuntimeNumeric Op_LeftShift(IRuntimeNumeric value, int shift);
        IRuntimeNumeric Op_RightShift(IRuntimeNumeric value, int shift);


        bool Op_Equals(ulong left, IRuntimeNumeric right);
        bool Op_Equals(long left, IRuntimeNumeric right);
        bool Op_Equals(IRuntimeNumeric left, long right);
        bool Op_Equals(IRuntimeNumeric left, IRuntimeNumeric right);
        bool Op_Equals(IRuntimeNumeric left, ulong right);
        bool Op_NotEqual(long left, IRuntimeNumeric right);
        bool Op_NotEqual(IRuntimeNumeric left, long right);
        bool Op_NotEqual(IRuntimeNumeric left, IRuntimeNumeric right);
        bool Op_NotEqual(IRuntimeNumeric left, ulong right);
        bool Op_NotEqual(ulong left, IRuntimeNumeric right);
        bool Op_LessThan(long left, IRuntimeNumeric right);
        bool Op_LessThan(ulong left, IRuntimeNumeric right);
        bool Op_LessThan(IRuntimeNumeric left, ulong right);
        bool Op_LessThan(IRuntimeNumeric left, IRuntimeNumeric right);
        bool Op_LessThan(IRuntimeNumeric left, long right);
        bool Op_GreaterThan(IRuntimeNumeric left, ulong right);
        bool Op_GreaterThan(IRuntimeNumeric left, IRuntimeNumeric right);
        bool Op_GreaterThan(long left, IRuntimeNumeric right);
        bool Op_GreaterThan(IRuntimeNumeric left, long right);
        bool Op_GreaterThan(ulong left, IRuntimeNumeric right);
        /// <summary>
        /// The &lt;= operator returns true if its first operand is less than or equal to its second operand, false otherwise:
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        bool Op_LessThanOrEqual(long left, IRuntimeNumeric right);
        bool Op_LessThanOrEqual(IRuntimeNumeric left, long right);
        bool Op_LessThanOrEqual(IRuntimeNumeric left, IRuntimeNumeric right);
        bool Op_LessThanOrEqual(IRuntimeNumeric left, ulong right);
        bool Op_LessThanOrEqual(ulong left, IRuntimeNumeric right);
        bool Op_GreaterThanOrEqual(long left, IRuntimeNumeric right);
        bool Op_GreaterThanOrEqual(IRuntimeNumeric left, long right);
        bool Op_GreaterThanOrEqual(IRuntimeNumeric left, IRuntimeNumeric right);
        bool Op_GreaterThanOrEqual(ulong left, IRuntimeNumeric right);
        bool Op_GreaterThanOrEqual(IRuntimeNumeric left, ulong right);

        //Cast/Convert
        //TODO: implicit vs explicit is different via numeric.
        IRuntimeNumeric Op_Implicit(byte value);
        IRuntimeNumeric Op_Implicit(sbyte value);
        IRuntimeNumeric Op_Implicit(short value);
        IRuntimeNumeric Op_Implicit(ushort value);
        IRuntimeNumeric Op_Implicit(int value);
        IRuntimeNumeric Op_Implicit(uint value);
        IRuntimeNumeric Op_Implicit(long value);
        IRuntimeNumeric Op_Implicit(ulong value);
        IRuntimeNumeric Op_Explicit(decimal value);
        IRuntimeNumeric Op_Explicit(float value);
        IRuntimeNumeric Op_Explicit(double value);
        IRuntimeNumeric Op_Explicit(BigInteger value);

        byte Op_ExplicitToByte(IRuntimeNumeric value);
        decimal Op_ExplicitToDecimal(IRuntimeNumeric value);
        double Op_ExplicitToDouble(IRuntimeNumeric value);
        short Op_ExplicitToShort(IRuntimeNumeric value);
        long Op_ExplicitToLong(IRuntimeNumeric value);
        sbyte Op_ExplicitToSByte(IRuntimeNumeric value);
        ushort Op_ExplicitToUShort(IRuntimeNumeric value);
        uint Op_ExplicitToUInt(IRuntimeNumeric value);
        ulong Op_ExplicitToULong(IRuntimeNumeric value);
        IRuntimeNumeric Op_ExplicitFrom(float value);
        int Op_ExplicitToInt(IRuntimeNumeric value);
        float Op_ExplicitToFloat(IRuntimeNumeric value);
    }
}
