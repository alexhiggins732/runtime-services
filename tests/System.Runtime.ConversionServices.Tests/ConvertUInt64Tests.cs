using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using static System.Runtime.ConversionServices.Conversions;

namespace System.Runtime.ConversionServices.Tests 
{
    public class ConvertUInt64Tests
    {

        //initalize default, min and maxed as well as boxed versions of each
        public ulong srcDefault = ulong.Parse("0");
        public ulong srcMin = ulong.Parse("0");
        public ulong srcMax = ulong.Parse("18446744073709551615");
        public object srcDefaultBoxed => (object)srcDefault;
        public object srcMinBoxed =>   (object)srcMin;
        public object srcMaxBoxed =>  (object)srcMax;

        public string srcDefaultFloatingPoint = "0";
        public string srcMinFloatingPoint = "0";
        public string srcMaxFloatingPoint = "18446744073709551615";

        //perform boxed and unboxed conversions using the various cast operators.





        [Fact]
        public void ConvertToBooleanFromUInt64DefaultTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcDefaultBoxedToObjectResult = srcDefaultBoxed.To(typeof(bool));
                
            // Test: TOut To<TOut>(this object value)
            bool srcDefaultBoxedToGenericResult = srcDefaultBoxed.To<bool>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcDefaultGenericToBoxedResult = srcDefault.To(typeof(bool));

            // Test: TOut To<TIn, TOut>(this TIn value)
             bool srcDefaultGenericToGenericResult = srcDefault.To<ulong, bool>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (bool)srcDefault can be used.
            //      2) If not can srcDefault.Tobool(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcDefault, typeof(bool)) be used? (accounting for overflow)
            //      4) If not use unsafe

            
            var expectedDefaultBoxedToObjectResult = false;
            var expectedDefaultBoxedToGenericResult = false;
            var expectedDefaultGenericToBoxedResult = false;
            var expectedDefaultGenericToGenericResult = false;


            Assert.True((bool)srcDefaultBoxedToObjectResult == expectedDefaultBoxedToObjectResult);
            Assert.True(srcDefaultBoxedToGenericResult == expectedDefaultBoxedToGenericResult);
            Assert.True((bool)srcDefaultGenericToBoxedResult == expectedDefaultGenericToBoxedResult);
            Assert.True(srcDefaultGenericToGenericResult == expectedDefaultGenericToGenericResult);
        }



        [Fact]
        public void ConvertToBooleanFromUInt64MinTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMinBoxedToObjectResult = srcMinBoxed.To(typeof(bool));
                
            // Test: TOut To<TOut>(this object value)
            bool srcMinBoxedToGenericResult = srcMinBoxed.To<bool>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMinGenericToBoxedResult = srcMin.To(typeof(bool));

            // Test: TOut To<TIn, TOut>(this TIn value)
             bool srcMinGenericToGenericResult = srcMin.To<ulong, bool>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (bool)srcMin can be used.
            //      2) If not can srcMin.Tobool(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMin, typeof(bool)) be used? (accounting for overflow)
            //      4) If not use unsafe

            
            var expectedMinBoxedToObjectResult = false;
            var expectedMinBoxedToGenericResult = false;
            var expectedMinGenericToBoxedResult = false;
            var expectedMinGenericToGenericResult = false;


            Assert.True((bool)srcMinBoxedToObjectResult == expectedMinBoxedToObjectResult);
            Assert.True(srcMinBoxedToGenericResult == expectedMinBoxedToGenericResult);
            Assert.True((bool)srcMinGenericToBoxedResult == expectedMinGenericToBoxedResult);
            Assert.True(srcMinGenericToGenericResult == expectedMinGenericToGenericResult);
        }



        [Fact]
        public void ConvertToBooleanFromUInt64MaxTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMaxBoxedToObjectResult = srcMaxBoxed.To(typeof(bool));
                
            // Test: TOut To<TOut>(this object value)
            bool srcMaxBoxedToGenericResult = srcMaxBoxed.To<bool>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMaxGenericToBoxedResult = srcMax.To(typeof(bool));

            // Test: TOut To<TIn, TOut>(this TIn value)
             bool srcMaxGenericToGenericResult = srcMax.To<ulong, bool>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (bool)srcMax can be used.
            //      2) If not can srcMax.Tobool(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMax, typeof(bool)) be used? (accounting for overflow)
            //      4) If not use unsafe

            
            var expectedMaxBoxedToObjectResult = true;
            var expectedMaxBoxedToGenericResult = true;
            var expectedMaxGenericToBoxedResult = true;
            var expectedMaxGenericToGenericResult = true;


            Assert.True((bool)srcMaxBoxedToObjectResult == expectedMaxBoxedToObjectResult);
            Assert.True(srcMaxBoxedToGenericResult == expectedMaxBoxedToGenericResult);
            Assert.True((bool)srcMaxGenericToBoxedResult == expectedMaxGenericToBoxedResult);
            Assert.True(srcMaxGenericToGenericResult == expectedMaxGenericToGenericResult);
        }



        [Fact]
        public void ConvertToCharFromUInt64DefaultTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcDefaultBoxedToObjectResult = srcDefaultBoxed.To(typeof(char));
                
            // Test: TOut To<TOut>(this object value)
            char srcDefaultBoxedToGenericResult = srcDefaultBoxed.To<char>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcDefaultGenericToBoxedResult = srcDefault.To(typeof(char));

            // Test: TOut To<TIn, TOut>(this TIn value)
             char srcDefaultGenericToGenericResult = srcDefault.To<ulong, char>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (char)srcDefault can be used.
            //      2) If not can srcDefault.Tochar(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcDefault, typeof(char)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedDefaultBoxedToObjectResult = default(char);
            var expectedDefaultBoxedToGenericResult = default(char);
            var expectedDefaultGenericToBoxedResult = default(char);
            var expectedDefaultGenericToGenericResult = default(char);

            unsafe
            {
                ulong src = srcDefault;
                ulong* srcPtr = &src;
                expectedDefaultBoxedToObjectResult = *(char*)srcPtr;
                expectedDefaultBoxedToGenericResult = *(char*)srcPtr;
                expectedDefaultGenericToBoxedResult = *(char*)srcPtr;
                expectedDefaultGenericToGenericResult = *(char*)srcPtr;
            }


            Assert.True((char)srcDefaultBoxedToObjectResult == expectedDefaultBoxedToObjectResult);
            Assert.True(srcDefaultBoxedToGenericResult == expectedDefaultBoxedToGenericResult);
            Assert.True((char)srcDefaultGenericToBoxedResult == expectedDefaultGenericToBoxedResult);
            Assert.True(srcDefaultGenericToGenericResult == expectedDefaultGenericToGenericResult);
        }



        [Fact]
        public void ConvertToCharFromUInt64MinTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMinBoxedToObjectResult = srcMinBoxed.To(typeof(char));
                
            // Test: TOut To<TOut>(this object value)
            char srcMinBoxedToGenericResult = srcMinBoxed.To<char>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMinGenericToBoxedResult = srcMin.To(typeof(char));

            // Test: TOut To<TIn, TOut>(this TIn value)
             char srcMinGenericToGenericResult = srcMin.To<ulong, char>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (char)srcMin can be used.
            //      2) If not can srcMin.Tochar(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMin, typeof(char)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedMinBoxedToObjectResult = default(char);
            var expectedMinBoxedToGenericResult = default(char);
            var expectedMinGenericToBoxedResult = default(char);
            var expectedMinGenericToGenericResult = default(char);

            unsafe
            {
                ulong src = srcMin;
                ulong* srcPtr = &src;
                expectedMinBoxedToObjectResult = *(char*)srcPtr;
                expectedMinBoxedToGenericResult = *(char*)srcPtr;
                expectedMinGenericToBoxedResult = *(char*)srcPtr;
                expectedMinGenericToGenericResult = *(char*)srcPtr;
            }


            Assert.True((char)srcMinBoxedToObjectResult == expectedMinBoxedToObjectResult);
            Assert.True(srcMinBoxedToGenericResult == expectedMinBoxedToGenericResult);
            Assert.True((char)srcMinGenericToBoxedResult == expectedMinGenericToBoxedResult);
            Assert.True(srcMinGenericToGenericResult == expectedMinGenericToGenericResult);
        }



        [Fact]
        public void ConvertToCharFromUInt64MaxTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMaxBoxedToObjectResult = srcMaxBoxed.To(typeof(char));
                
            // Test: TOut To<TOut>(this object value)
            char srcMaxBoxedToGenericResult = srcMaxBoxed.To<char>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMaxGenericToBoxedResult = srcMax.To(typeof(char));

            // Test: TOut To<TIn, TOut>(this TIn value)
             char srcMaxGenericToGenericResult = srcMax.To<ulong, char>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (char)srcMax can be used.
            //      2) If not can srcMax.Tochar(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMax, typeof(char)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedMaxBoxedToObjectResult = default(char);
            var expectedMaxBoxedToGenericResult = default(char);
            var expectedMaxGenericToBoxedResult = default(char);
            var expectedMaxGenericToGenericResult = default(char);

            unsafe
            {
                ulong src = srcMax;
                ulong* srcPtr = &src;
                expectedMaxBoxedToObjectResult = *(char*)srcPtr;
                expectedMaxBoxedToGenericResult = *(char*)srcPtr;
                expectedMaxGenericToBoxedResult = *(char*)srcPtr;
                expectedMaxGenericToGenericResult = *(char*)srcPtr;
            }


            Assert.True((char)srcMaxBoxedToObjectResult == expectedMaxBoxedToObjectResult);
            Assert.True(srcMaxBoxedToGenericResult == expectedMaxBoxedToGenericResult);
            Assert.True((char)srcMaxGenericToBoxedResult == expectedMaxGenericToBoxedResult);
            Assert.True(srcMaxGenericToGenericResult == expectedMaxGenericToGenericResult);
        }



        [Fact]
        public void ConvertToSByteFromUInt64DefaultTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcDefaultBoxedToObjectResult = srcDefaultBoxed.To(typeof(sbyte));
                
            // Test: TOut To<TOut>(this object value)
            sbyte srcDefaultBoxedToGenericResult = srcDefaultBoxed.To<sbyte>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcDefaultGenericToBoxedResult = srcDefault.To(typeof(sbyte));

            // Test: TOut To<TIn, TOut>(this TIn value)
             sbyte srcDefaultGenericToGenericResult = srcDefault.To<ulong, sbyte>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (sbyte)srcDefault can be used.
            //      2) If not can srcDefault.Tosbyte(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcDefault, typeof(sbyte)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedDefaultBoxedToObjectResult = default(sbyte);
            var expectedDefaultBoxedToGenericResult = default(sbyte);
            var expectedDefaultGenericToBoxedResult = default(sbyte);
            var expectedDefaultGenericToGenericResult = default(sbyte);

            unsafe
            {
                ulong src = srcDefault;
                ulong* srcPtr = &src;
                expectedDefaultBoxedToObjectResult = *(sbyte*)srcPtr;
                expectedDefaultBoxedToGenericResult = *(sbyte*)srcPtr;
                expectedDefaultGenericToBoxedResult = *(sbyte*)srcPtr;
                expectedDefaultGenericToGenericResult = *(sbyte*)srcPtr;
            }


            Assert.True((sbyte)srcDefaultBoxedToObjectResult == expectedDefaultBoxedToObjectResult);
            Assert.True(srcDefaultBoxedToGenericResult == expectedDefaultBoxedToGenericResult);
            Assert.True((sbyte)srcDefaultGenericToBoxedResult == expectedDefaultGenericToBoxedResult);
            Assert.True(srcDefaultGenericToGenericResult == expectedDefaultGenericToGenericResult);
        }



        [Fact]
        public void ConvertToSByteFromUInt64MinTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMinBoxedToObjectResult = srcMinBoxed.To(typeof(sbyte));
                
            // Test: TOut To<TOut>(this object value)
            sbyte srcMinBoxedToGenericResult = srcMinBoxed.To<sbyte>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMinGenericToBoxedResult = srcMin.To(typeof(sbyte));

            // Test: TOut To<TIn, TOut>(this TIn value)
             sbyte srcMinGenericToGenericResult = srcMin.To<ulong, sbyte>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (sbyte)srcMin can be used.
            //      2) If not can srcMin.Tosbyte(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMin, typeof(sbyte)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedMinBoxedToObjectResult = default(sbyte);
            var expectedMinBoxedToGenericResult = default(sbyte);
            var expectedMinGenericToBoxedResult = default(sbyte);
            var expectedMinGenericToGenericResult = default(sbyte);

            unsafe
            {
                ulong src = srcMin;
                ulong* srcPtr = &src;
                expectedMinBoxedToObjectResult = *(sbyte*)srcPtr;
                expectedMinBoxedToGenericResult = *(sbyte*)srcPtr;
                expectedMinGenericToBoxedResult = *(sbyte*)srcPtr;
                expectedMinGenericToGenericResult = *(sbyte*)srcPtr;
            }


            Assert.True((sbyte)srcMinBoxedToObjectResult == expectedMinBoxedToObjectResult);
            Assert.True(srcMinBoxedToGenericResult == expectedMinBoxedToGenericResult);
            Assert.True((sbyte)srcMinGenericToBoxedResult == expectedMinGenericToBoxedResult);
            Assert.True(srcMinGenericToGenericResult == expectedMinGenericToGenericResult);
        }



        [Fact]
        public void ConvertToSByteFromUInt64MaxTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMaxBoxedToObjectResult = srcMaxBoxed.To(typeof(sbyte));
                
            // Test: TOut To<TOut>(this object value)
            sbyte srcMaxBoxedToGenericResult = srcMaxBoxed.To<sbyte>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMaxGenericToBoxedResult = srcMax.To(typeof(sbyte));

            // Test: TOut To<TIn, TOut>(this TIn value)
             sbyte srcMaxGenericToGenericResult = srcMax.To<ulong, sbyte>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (sbyte)srcMax can be used.
            //      2) If not can srcMax.Tosbyte(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMax, typeof(sbyte)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedMaxBoxedToObjectResult = default(sbyte);
            var expectedMaxBoxedToGenericResult = default(sbyte);
            var expectedMaxGenericToBoxedResult = default(sbyte);
            var expectedMaxGenericToGenericResult = default(sbyte);

            unsafe
            {
                ulong src = srcMax;
                ulong* srcPtr = &src;
                expectedMaxBoxedToObjectResult = *(sbyte*)srcPtr;
                expectedMaxBoxedToGenericResult = *(sbyte*)srcPtr;
                expectedMaxGenericToBoxedResult = *(sbyte*)srcPtr;
                expectedMaxGenericToGenericResult = *(sbyte*)srcPtr;
            }


            Assert.True((sbyte)srcMaxBoxedToObjectResult == expectedMaxBoxedToObjectResult);
            Assert.True(srcMaxBoxedToGenericResult == expectedMaxBoxedToGenericResult);
            Assert.True((sbyte)srcMaxGenericToBoxedResult == expectedMaxGenericToBoxedResult);
            Assert.True(srcMaxGenericToGenericResult == expectedMaxGenericToGenericResult);
        }



        [Fact]
        public void ConvertToByteFromUInt64DefaultTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcDefaultBoxedToObjectResult = srcDefaultBoxed.To(typeof(byte));
                
            // Test: TOut To<TOut>(this object value)
            byte srcDefaultBoxedToGenericResult = srcDefaultBoxed.To<byte>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcDefaultGenericToBoxedResult = srcDefault.To(typeof(byte));

            // Test: TOut To<TIn, TOut>(this TIn value)
             byte srcDefaultGenericToGenericResult = srcDefault.To<ulong, byte>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (byte)srcDefault can be used.
            //      2) If not can srcDefault.Tobyte(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcDefault, typeof(byte)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedDefaultBoxedToObjectResult = default(byte);
            var expectedDefaultBoxedToGenericResult = default(byte);
            var expectedDefaultGenericToBoxedResult = default(byte);
            var expectedDefaultGenericToGenericResult = default(byte);

            unsafe
            {
                ulong src = srcDefault;
                ulong* srcPtr = &src;
                expectedDefaultBoxedToObjectResult = *(byte*)srcPtr;
                expectedDefaultBoxedToGenericResult = *(byte*)srcPtr;
                expectedDefaultGenericToBoxedResult = *(byte*)srcPtr;
                expectedDefaultGenericToGenericResult = *(byte*)srcPtr;
            }


            Assert.True((byte)srcDefaultBoxedToObjectResult == expectedDefaultBoxedToObjectResult);
            Assert.True(srcDefaultBoxedToGenericResult == expectedDefaultBoxedToGenericResult);
            Assert.True((byte)srcDefaultGenericToBoxedResult == expectedDefaultGenericToBoxedResult);
            Assert.True(srcDefaultGenericToGenericResult == expectedDefaultGenericToGenericResult);
        }



        [Fact]
        public void ConvertToByteFromUInt64MinTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMinBoxedToObjectResult = srcMinBoxed.To(typeof(byte));
                
            // Test: TOut To<TOut>(this object value)
            byte srcMinBoxedToGenericResult = srcMinBoxed.To<byte>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMinGenericToBoxedResult = srcMin.To(typeof(byte));

            // Test: TOut To<TIn, TOut>(this TIn value)
             byte srcMinGenericToGenericResult = srcMin.To<ulong, byte>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (byte)srcMin can be used.
            //      2) If not can srcMin.Tobyte(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMin, typeof(byte)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedMinBoxedToObjectResult = default(byte);
            var expectedMinBoxedToGenericResult = default(byte);
            var expectedMinGenericToBoxedResult = default(byte);
            var expectedMinGenericToGenericResult = default(byte);

            unsafe
            {
                ulong src = srcMin;
                ulong* srcPtr = &src;
                expectedMinBoxedToObjectResult = *(byte*)srcPtr;
                expectedMinBoxedToGenericResult = *(byte*)srcPtr;
                expectedMinGenericToBoxedResult = *(byte*)srcPtr;
                expectedMinGenericToGenericResult = *(byte*)srcPtr;
            }


            Assert.True((byte)srcMinBoxedToObjectResult == expectedMinBoxedToObjectResult);
            Assert.True(srcMinBoxedToGenericResult == expectedMinBoxedToGenericResult);
            Assert.True((byte)srcMinGenericToBoxedResult == expectedMinGenericToBoxedResult);
            Assert.True(srcMinGenericToGenericResult == expectedMinGenericToGenericResult);
        }



        [Fact]
        public void ConvertToByteFromUInt64MaxTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMaxBoxedToObjectResult = srcMaxBoxed.To(typeof(byte));
                
            // Test: TOut To<TOut>(this object value)
            byte srcMaxBoxedToGenericResult = srcMaxBoxed.To<byte>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMaxGenericToBoxedResult = srcMax.To(typeof(byte));

            // Test: TOut To<TIn, TOut>(this TIn value)
             byte srcMaxGenericToGenericResult = srcMax.To<ulong, byte>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (byte)srcMax can be used.
            //      2) If not can srcMax.Tobyte(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMax, typeof(byte)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedMaxBoxedToObjectResult = default(byte);
            var expectedMaxBoxedToGenericResult = default(byte);
            var expectedMaxGenericToBoxedResult = default(byte);
            var expectedMaxGenericToGenericResult = default(byte);

            unsafe
            {
                ulong src = srcMax;
                ulong* srcPtr = &src;
                expectedMaxBoxedToObjectResult = *(byte*)srcPtr;
                expectedMaxBoxedToGenericResult = *(byte*)srcPtr;
                expectedMaxGenericToBoxedResult = *(byte*)srcPtr;
                expectedMaxGenericToGenericResult = *(byte*)srcPtr;
            }


            Assert.True((byte)srcMaxBoxedToObjectResult == expectedMaxBoxedToObjectResult);
            Assert.True(srcMaxBoxedToGenericResult == expectedMaxBoxedToGenericResult);
            Assert.True((byte)srcMaxGenericToBoxedResult == expectedMaxGenericToBoxedResult);
            Assert.True(srcMaxGenericToGenericResult == expectedMaxGenericToGenericResult);
        }



        [Fact]
        public void ConvertToInt16FromUInt64DefaultTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcDefaultBoxedToObjectResult = srcDefaultBoxed.To(typeof(short));
                
            // Test: TOut To<TOut>(this object value)
            short srcDefaultBoxedToGenericResult = srcDefaultBoxed.To<short>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcDefaultGenericToBoxedResult = srcDefault.To(typeof(short));

            // Test: TOut To<TIn, TOut>(this TIn value)
             short srcDefaultGenericToGenericResult = srcDefault.To<ulong, short>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (short)srcDefault can be used.
            //      2) If not can srcDefault.Toshort(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcDefault, typeof(short)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedDefaultBoxedToObjectResult = default(short);
            var expectedDefaultBoxedToGenericResult = default(short);
            var expectedDefaultGenericToBoxedResult = default(short);
            var expectedDefaultGenericToGenericResult = default(short);

            unsafe
            {
                ulong src = srcDefault;
                ulong* srcPtr = &src;
                expectedDefaultBoxedToObjectResult = *(short*)srcPtr;
                expectedDefaultBoxedToGenericResult = *(short*)srcPtr;
                expectedDefaultGenericToBoxedResult = *(short*)srcPtr;
                expectedDefaultGenericToGenericResult = *(short*)srcPtr;
            }


            Assert.True((short)srcDefaultBoxedToObjectResult == expectedDefaultBoxedToObjectResult);
            Assert.True(srcDefaultBoxedToGenericResult == expectedDefaultBoxedToGenericResult);
            Assert.True((short)srcDefaultGenericToBoxedResult == expectedDefaultGenericToBoxedResult);
            Assert.True(srcDefaultGenericToGenericResult == expectedDefaultGenericToGenericResult);
        }



        [Fact]
        public void ConvertToInt16FromUInt64MinTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMinBoxedToObjectResult = srcMinBoxed.To(typeof(short));
                
            // Test: TOut To<TOut>(this object value)
            short srcMinBoxedToGenericResult = srcMinBoxed.To<short>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMinGenericToBoxedResult = srcMin.To(typeof(short));

            // Test: TOut To<TIn, TOut>(this TIn value)
             short srcMinGenericToGenericResult = srcMin.To<ulong, short>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (short)srcMin can be used.
            //      2) If not can srcMin.Toshort(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMin, typeof(short)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedMinBoxedToObjectResult = default(short);
            var expectedMinBoxedToGenericResult = default(short);
            var expectedMinGenericToBoxedResult = default(short);
            var expectedMinGenericToGenericResult = default(short);

            unsafe
            {
                ulong src = srcMin;
                ulong* srcPtr = &src;
                expectedMinBoxedToObjectResult = *(short*)srcPtr;
                expectedMinBoxedToGenericResult = *(short*)srcPtr;
                expectedMinGenericToBoxedResult = *(short*)srcPtr;
                expectedMinGenericToGenericResult = *(short*)srcPtr;
            }


            Assert.True((short)srcMinBoxedToObjectResult == expectedMinBoxedToObjectResult);
            Assert.True(srcMinBoxedToGenericResult == expectedMinBoxedToGenericResult);
            Assert.True((short)srcMinGenericToBoxedResult == expectedMinGenericToBoxedResult);
            Assert.True(srcMinGenericToGenericResult == expectedMinGenericToGenericResult);
        }



        [Fact]
        public void ConvertToInt16FromUInt64MaxTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMaxBoxedToObjectResult = srcMaxBoxed.To(typeof(short));
                
            // Test: TOut To<TOut>(this object value)
            short srcMaxBoxedToGenericResult = srcMaxBoxed.To<short>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMaxGenericToBoxedResult = srcMax.To(typeof(short));

            // Test: TOut To<TIn, TOut>(this TIn value)
             short srcMaxGenericToGenericResult = srcMax.To<ulong, short>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (short)srcMax can be used.
            //      2) If not can srcMax.Toshort(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMax, typeof(short)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedMaxBoxedToObjectResult = default(short);
            var expectedMaxBoxedToGenericResult = default(short);
            var expectedMaxGenericToBoxedResult = default(short);
            var expectedMaxGenericToGenericResult = default(short);

            unsafe
            {
                ulong src = srcMax;
                ulong* srcPtr = &src;
                expectedMaxBoxedToObjectResult = *(short*)srcPtr;
                expectedMaxBoxedToGenericResult = *(short*)srcPtr;
                expectedMaxGenericToBoxedResult = *(short*)srcPtr;
                expectedMaxGenericToGenericResult = *(short*)srcPtr;
            }


            Assert.True((short)srcMaxBoxedToObjectResult == expectedMaxBoxedToObjectResult);
            Assert.True(srcMaxBoxedToGenericResult == expectedMaxBoxedToGenericResult);
            Assert.True((short)srcMaxGenericToBoxedResult == expectedMaxGenericToBoxedResult);
            Assert.True(srcMaxGenericToGenericResult == expectedMaxGenericToGenericResult);
        }



        [Fact]
        public void ConvertToUInt16FromUInt64DefaultTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcDefaultBoxedToObjectResult = srcDefaultBoxed.To(typeof(ushort));
                
            // Test: TOut To<TOut>(this object value)
            ushort srcDefaultBoxedToGenericResult = srcDefaultBoxed.To<ushort>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcDefaultGenericToBoxedResult = srcDefault.To(typeof(ushort));

            // Test: TOut To<TIn, TOut>(this TIn value)
             ushort srcDefaultGenericToGenericResult = srcDefault.To<ulong, ushort>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (ushort)srcDefault can be used.
            //      2) If not can srcDefault.Toushort(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcDefault, typeof(ushort)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedDefaultBoxedToObjectResult = default(ushort);
            var expectedDefaultBoxedToGenericResult = default(ushort);
            var expectedDefaultGenericToBoxedResult = default(ushort);
            var expectedDefaultGenericToGenericResult = default(ushort);

            unsafe
            {
                ulong src = srcDefault;
                ulong* srcPtr = &src;
                expectedDefaultBoxedToObjectResult = *(ushort*)srcPtr;
                expectedDefaultBoxedToGenericResult = *(ushort*)srcPtr;
                expectedDefaultGenericToBoxedResult = *(ushort*)srcPtr;
                expectedDefaultGenericToGenericResult = *(ushort*)srcPtr;
            }


            Assert.True((ushort)srcDefaultBoxedToObjectResult == expectedDefaultBoxedToObjectResult);
            Assert.True(srcDefaultBoxedToGenericResult == expectedDefaultBoxedToGenericResult);
            Assert.True((ushort)srcDefaultGenericToBoxedResult == expectedDefaultGenericToBoxedResult);
            Assert.True(srcDefaultGenericToGenericResult == expectedDefaultGenericToGenericResult);
        }



        [Fact]
        public void ConvertToUInt16FromUInt64MinTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMinBoxedToObjectResult = srcMinBoxed.To(typeof(ushort));
                
            // Test: TOut To<TOut>(this object value)
            ushort srcMinBoxedToGenericResult = srcMinBoxed.To<ushort>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMinGenericToBoxedResult = srcMin.To(typeof(ushort));

            // Test: TOut To<TIn, TOut>(this TIn value)
             ushort srcMinGenericToGenericResult = srcMin.To<ulong, ushort>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (ushort)srcMin can be used.
            //      2) If not can srcMin.Toushort(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMin, typeof(ushort)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedMinBoxedToObjectResult = default(ushort);
            var expectedMinBoxedToGenericResult = default(ushort);
            var expectedMinGenericToBoxedResult = default(ushort);
            var expectedMinGenericToGenericResult = default(ushort);

            unsafe
            {
                ulong src = srcMin;
                ulong* srcPtr = &src;
                expectedMinBoxedToObjectResult = *(ushort*)srcPtr;
                expectedMinBoxedToGenericResult = *(ushort*)srcPtr;
                expectedMinGenericToBoxedResult = *(ushort*)srcPtr;
                expectedMinGenericToGenericResult = *(ushort*)srcPtr;
            }


            Assert.True((ushort)srcMinBoxedToObjectResult == expectedMinBoxedToObjectResult);
            Assert.True(srcMinBoxedToGenericResult == expectedMinBoxedToGenericResult);
            Assert.True((ushort)srcMinGenericToBoxedResult == expectedMinGenericToBoxedResult);
            Assert.True(srcMinGenericToGenericResult == expectedMinGenericToGenericResult);
        }



        [Fact]
        public void ConvertToUInt16FromUInt64MaxTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMaxBoxedToObjectResult = srcMaxBoxed.To(typeof(ushort));
                
            // Test: TOut To<TOut>(this object value)
            ushort srcMaxBoxedToGenericResult = srcMaxBoxed.To<ushort>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMaxGenericToBoxedResult = srcMax.To(typeof(ushort));

            // Test: TOut To<TIn, TOut>(this TIn value)
             ushort srcMaxGenericToGenericResult = srcMax.To<ulong, ushort>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (ushort)srcMax can be used.
            //      2) If not can srcMax.Toushort(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMax, typeof(ushort)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedMaxBoxedToObjectResult = default(ushort);
            var expectedMaxBoxedToGenericResult = default(ushort);
            var expectedMaxGenericToBoxedResult = default(ushort);
            var expectedMaxGenericToGenericResult = default(ushort);

            unsafe
            {
                ulong src = srcMax;
                ulong* srcPtr = &src;
                expectedMaxBoxedToObjectResult = *(ushort*)srcPtr;
                expectedMaxBoxedToGenericResult = *(ushort*)srcPtr;
                expectedMaxGenericToBoxedResult = *(ushort*)srcPtr;
                expectedMaxGenericToGenericResult = *(ushort*)srcPtr;
            }


            Assert.True((ushort)srcMaxBoxedToObjectResult == expectedMaxBoxedToObjectResult);
            Assert.True(srcMaxBoxedToGenericResult == expectedMaxBoxedToGenericResult);
            Assert.True((ushort)srcMaxGenericToBoxedResult == expectedMaxGenericToBoxedResult);
            Assert.True(srcMaxGenericToGenericResult == expectedMaxGenericToGenericResult);
        }



        [Fact]
        public void ConvertToInt32FromUInt64DefaultTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcDefaultBoxedToObjectResult = srcDefaultBoxed.To(typeof(int));
                
            // Test: TOut To<TOut>(this object value)
            int srcDefaultBoxedToGenericResult = srcDefaultBoxed.To<int>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcDefaultGenericToBoxedResult = srcDefault.To(typeof(int));

            // Test: TOut To<TIn, TOut>(this TIn value)
             int srcDefaultGenericToGenericResult = srcDefault.To<ulong, int>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (int)srcDefault can be used.
            //      2) If not can srcDefault.Toint(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcDefault, typeof(int)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedDefaultBoxedToObjectResult = default(int);
            var expectedDefaultBoxedToGenericResult = default(int);
            var expectedDefaultGenericToBoxedResult = default(int);
            var expectedDefaultGenericToGenericResult = default(int);

            unsafe
            {
                ulong src = srcDefault;
                ulong* srcPtr = &src;
                expectedDefaultBoxedToObjectResult = *(int*)srcPtr;
                expectedDefaultBoxedToGenericResult = *(int*)srcPtr;
                expectedDefaultGenericToBoxedResult = *(int*)srcPtr;
                expectedDefaultGenericToGenericResult = *(int*)srcPtr;
            }


            Assert.True((int)srcDefaultBoxedToObjectResult == expectedDefaultBoxedToObjectResult);
            Assert.True(srcDefaultBoxedToGenericResult == expectedDefaultBoxedToGenericResult);
            Assert.True((int)srcDefaultGenericToBoxedResult == expectedDefaultGenericToBoxedResult);
            Assert.True(srcDefaultGenericToGenericResult == expectedDefaultGenericToGenericResult);
        }



        [Fact]
        public void ConvertToInt32FromUInt64MinTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMinBoxedToObjectResult = srcMinBoxed.To(typeof(int));
                
            // Test: TOut To<TOut>(this object value)
            int srcMinBoxedToGenericResult = srcMinBoxed.To<int>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMinGenericToBoxedResult = srcMin.To(typeof(int));

            // Test: TOut To<TIn, TOut>(this TIn value)
             int srcMinGenericToGenericResult = srcMin.To<ulong, int>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (int)srcMin can be used.
            //      2) If not can srcMin.Toint(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMin, typeof(int)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedMinBoxedToObjectResult = default(int);
            var expectedMinBoxedToGenericResult = default(int);
            var expectedMinGenericToBoxedResult = default(int);
            var expectedMinGenericToGenericResult = default(int);

            unsafe
            {
                ulong src = srcMin;
                ulong* srcPtr = &src;
                expectedMinBoxedToObjectResult = *(int*)srcPtr;
                expectedMinBoxedToGenericResult = *(int*)srcPtr;
                expectedMinGenericToBoxedResult = *(int*)srcPtr;
                expectedMinGenericToGenericResult = *(int*)srcPtr;
            }


            Assert.True((int)srcMinBoxedToObjectResult == expectedMinBoxedToObjectResult);
            Assert.True(srcMinBoxedToGenericResult == expectedMinBoxedToGenericResult);
            Assert.True((int)srcMinGenericToBoxedResult == expectedMinGenericToBoxedResult);
            Assert.True(srcMinGenericToGenericResult == expectedMinGenericToGenericResult);
        }



        [Fact]
        public void ConvertToInt32FromUInt64MaxTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMaxBoxedToObjectResult = srcMaxBoxed.To(typeof(int));
                
            // Test: TOut To<TOut>(this object value)
            int srcMaxBoxedToGenericResult = srcMaxBoxed.To<int>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMaxGenericToBoxedResult = srcMax.To(typeof(int));

            // Test: TOut To<TIn, TOut>(this TIn value)
             int srcMaxGenericToGenericResult = srcMax.To<ulong, int>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (int)srcMax can be used.
            //      2) If not can srcMax.Toint(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMax, typeof(int)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedMaxBoxedToObjectResult = default(int);
            var expectedMaxBoxedToGenericResult = default(int);
            var expectedMaxGenericToBoxedResult = default(int);
            var expectedMaxGenericToGenericResult = default(int);

            unsafe
            {
                ulong src = srcMax;
                ulong* srcPtr = &src;
                expectedMaxBoxedToObjectResult = *(int*)srcPtr;
                expectedMaxBoxedToGenericResult = *(int*)srcPtr;
                expectedMaxGenericToBoxedResult = *(int*)srcPtr;
                expectedMaxGenericToGenericResult = *(int*)srcPtr;
            }


            Assert.True((int)srcMaxBoxedToObjectResult == expectedMaxBoxedToObjectResult);
            Assert.True(srcMaxBoxedToGenericResult == expectedMaxBoxedToGenericResult);
            Assert.True((int)srcMaxGenericToBoxedResult == expectedMaxGenericToBoxedResult);
            Assert.True(srcMaxGenericToGenericResult == expectedMaxGenericToGenericResult);
        }



        [Fact]
        public void ConvertToUInt32FromUInt64DefaultTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcDefaultBoxedToObjectResult = srcDefaultBoxed.To(typeof(uint));
                
            // Test: TOut To<TOut>(this object value)
            uint srcDefaultBoxedToGenericResult = srcDefaultBoxed.To<uint>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcDefaultGenericToBoxedResult = srcDefault.To(typeof(uint));

            // Test: TOut To<TIn, TOut>(this TIn value)
             uint srcDefaultGenericToGenericResult = srcDefault.To<ulong, uint>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (uint)srcDefault can be used.
            //      2) If not can srcDefault.Touint(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcDefault, typeof(uint)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedDefaultBoxedToObjectResult = default(uint);
            var expectedDefaultBoxedToGenericResult = default(uint);
            var expectedDefaultGenericToBoxedResult = default(uint);
            var expectedDefaultGenericToGenericResult = default(uint);

            unsafe
            {
                ulong src = srcDefault;
                ulong* srcPtr = &src;
                expectedDefaultBoxedToObjectResult = *(uint*)srcPtr;
                expectedDefaultBoxedToGenericResult = *(uint*)srcPtr;
                expectedDefaultGenericToBoxedResult = *(uint*)srcPtr;
                expectedDefaultGenericToGenericResult = *(uint*)srcPtr;
            }


            Assert.True((uint)srcDefaultBoxedToObjectResult == expectedDefaultBoxedToObjectResult);
            Assert.True(srcDefaultBoxedToGenericResult == expectedDefaultBoxedToGenericResult);
            Assert.True((uint)srcDefaultGenericToBoxedResult == expectedDefaultGenericToBoxedResult);
            Assert.True(srcDefaultGenericToGenericResult == expectedDefaultGenericToGenericResult);
        }



        [Fact]
        public void ConvertToUInt32FromUInt64MinTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMinBoxedToObjectResult = srcMinBoxed.To(typeof(uint));
                
            // Test: TOut To<TOut>(this object value)
            uint srcMinBoxedToGenericResult = srcMinBoxed.To<uint>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMinGenericToBoxedResult = srcMin.To(typeof(uint));

            // Test: TOut To<TIn, TOut>(this TIn value)
             uint srcMinGenericToGenericResult = srcMin.To<ulong, uint>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (uint)srcMin can be used.
            //      2) If not can srcMin.Touint(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMin, typeof(uint)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedMinBoxedToObjectResult = default(uint);
            var expectedMinBoxedToGenericResult = default(uint);
            var expectedMinGenericToBoxedResult = default(uint);
            var expectedMinGenericToGenericResult = default(uint);

            unsafe
            {
                ulong src = srcMin;
                ulong* srcPtr = &src;
                expectedMinBoxedToObjectResult = *(uint*)srcPtr;
                expectedMinBoxedToGenericResult = *(uint*)srcPtr;
                expectedMinGenericToBoxedResult = *(uint*)srcPtr;
                expectedMinGenericToGenericResult = *(uint*)srcPtr;
            }


            Assert.True((uint)srcMinBoxedToObjectResult == expectedMinBoxedToObjectResult);
            Assert.True(srcMinBoxedToGenericResult == expectedMinBoxedToGenericResult);
            Assert.True((uint)srcMinGenericToBoxedResult == expectedMinGenericToBoxedResult);
            Assert.True(srcMinGenericToGenericResult == expectedMinGenericToGenericResult);
        }



        [Fact]
        public void ConvertToUInt32FromUInt64MaxTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMaxBoxedToObjectResult = srcMaxBoxed.To(typeof(uint));
                
            // Test: TOut To<TOut>(this object value)
            uint srcMaxBoxedToGenericResult = srcMaxBoxed.To<uint>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMaxGenericToBoxedResult = srcMax.To(typeof(uint));

            // Test: TOut To<TIn, TOut>(this TIn value)
             uint srcMaxGenericToGenericResult = srcMax.To<ulong, uint>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (uint)srcMax can be used.
            //      2) If not can srcMax.Touint(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMax, typeof(uint)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedMaxBoxedToObjectResult = default(uint);
            var expectedMaxBoxedToGenericResult = default(uint);
            var expectedMaxGenericToBoxedResult = default(uint);
            var expectedMaxGenericToGenericResult = default(uint);

            unsafe
            {
                ulong src = srcMax;
                ulong* srcPtr = &src;
                expectedMaxBoxedToObjectResult = *(uint*)srcPtr;
                expectedMaxBoxedToGenericResult = *(uint*)srcPtr;
                expectedMaxGenericToBoxedResult = *(uint*)srcPtr;
                expectedMaxGenericToGenericResult = *(uint*)srcPtr;
            }


            Assert.True((uint)srcMaxBoxedToObjectResult == expectedMaxBoxedToObjectResult);
            Assert.True(srcMaxBoxedToGenericResult == expectedMaxBoxedToGenericResult);
            Assert.True((uint)srcMaxGenericToBoxedResult == expectedMaxGenericToBoxedResult);
            Assert.True(srcMaxGenericToGenericResult == expectedMaxGenericToGenericResult);
        }



        [Fact]
        public void ConvertToInt64FromUInt64DefaultTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcDefaultBoxedToObjectResult = srcDefaultBoxed.To(typeof(long));
                
            // Test: TOut To<TOut>(this object value)
            long srcDefaultBoxedToGenericResult = srcDefaultBoxed.To<long>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcDefaultGenericToBoxedResult = srcDefault.To(typeof(long));

            // Test: TOut To<TIn, TOut>(this TIn value)
             long srcDefaultGenericToGenericResult = srcDefault.To<ulong, long>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (long)srcDefault can be used.
            //      2) If not can srcDefault.Tolong(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcDefault, typeof(long)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedDefaultBoxedToObjectResult = default(long);
            var expectedDefaultBoxedToGenericResult = default(long);
            var expectedDefaultGenericToBoxedResult = default(long);
            var expectedDefaultGenericToGenericResult = default(long);

            unsafe
            {
                ulong src = srcDefault;
                ulong* srcPtr = &src;
                expectedDefaultBoxedToObjectResult = *(long*)srcPtr;
                expectedDefaultBoxedToGenericResult = *(long*)srcPtr;
                expectedDefaultGenericToBoxedResult = *(long*)srcPtr;
                expectedDefaultGenericToGenericResult = *(long*)srcPtr;
            }


            Assert.True((long)srcDefaultBoxedToObjectResult == expectedDefaultBoxedToObjectResult);
            Assert.True(srcDefaultBoxedToGenericResult == expectedDefaultBoxedToGenericResult);
            Assert.True((long)srcDefaultGenericToBoxedResult == expectedDefaultGenericToBoxedResult);
            Assert.True(srcDefaultGenericToGenericResult == expectedDefaultGenericToGenericResult);
        }



        [Fact]
        public void ConvertToInt64FromUInt64MinTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMinBoxedToObjectResult = srcMinBoxed.To(typeof(long));
                
            // Test: TOut To<TOut>(this object value)
            long srcMinBoxedToGenericResult = srcMinBoxed.To<long>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMinGenericToBoxedResult = srcMin.To(typeof(long));

            // Test: TOut To<TIn, TOut>(this TIn value)
             long srcMinGenericToGenericResult = srcMin.To<ulong, long>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (long)srcMin can be used.
            //      2) If not can srcMin.Tolong(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMin, typeof(long)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedMinBoxedToObjectResult = default(long);
            var expectedMinBoxedToGenericResult = default(long);
            var expectedMinGenericToBoxedResult = default(long);
            var expectedMinGenericToGenericResult = default(long);

            unsafe
            {
                ulong src = srcMin;
                ulong* srcPtr = &src;
                expectedMinBoxedToObjectResult = *(long*)srcPtr;
                expectedMinBoxedToGenericResult = *(long*)srcPtr;
                expectedMinGenericToBoxedResult = *(long*)srcPtr;
                expectedMinGenericToGenericResult = *(long*)srcPtr;
            }


            Assert.True((long)srcMinBoxedToObjectResult == expectedMinBoxedToObjectResult);
            Assert.True(srcMinBoxedToGenericResult == expectedMinBoxedToGenericResult);
            Assert.True((long)srcMinGenericToBoxedResult == expectedMinGenericToBoxedResult);
            Assert.True(srcMinGenericToGenericResult == expectedMinGenericToGenericResult);
        }



        [Fact]
        public void ConvertToInt64FromUInt64MaxTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMaxBoxedToObjectResult = srcMaxBoxed.To(typeof(long));
                
            // Test: TOut To<TOut>(this object value)
            long srcMaxBoxedToGenericResult = srcMaxBoxed.To<long>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMaxGenericToBoxedResult = srcMax.To(typeof(long));

            // Test: TOut To<TIn, TOut>(this TIn value)
             long srcMaxGenericToGenericResult = srcMax.To<ulong, long>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (long)srcMax can be used.
            //      2) If not can srcMax.Tolong(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMax, typeof(long)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedMaxBoxedToObjectResult = default(long);
            var expectedMaxBoxedToGenericResult = default(long);
            var expectedMaxGenericToBoxedResult = default(long);
            var expectedMaxGenericToGenericResult = default(long);

            unsafe
            {
                ulong src = srcMax;
                ulong* srcPtr = &src;
                expectedMaxBoxedToObjectResult = *(long*)srcPtr;
                expectedMaxBoxedToGenericResult = *(long*)srcPtr;
                expectedMaxGenericToBoxedResult = *(long*)srcPtr;
                expectedMaxGenericToGenericResult = *(long*)srcPtr;
            }


            Assert.True((long)srcMaxBoxedToObjectResult == expectedMaxBoxedToObjectResult);
            Assert.True(srcMaxBoxedToGenericResult == expectedMaxBoxedToGenericResult);
            Assert.True((long)srcMaxGenericToBoxedResult == expectedMaxGenericToBoxedResult);
            Assert.True(srcMaxGenericToGenericResult == expectedMaxGenericToGenericResult);
        }



        [Fact]
        public void ConvertToUInt64FromUInt64DefaultTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcDefaultBoxedToObjectResult = srcDefaultBoxed.To(typeof(ulong));
                
            // Test: TOut To<TOut>(this object value)
            ulong srcDefaultBoxedToGenericResult = srcDefaultBoxed.To<ulong>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcDefaultGenericToBoxedResult = srcDefault.To(typeof(ulong));

            // Test: TOut To<TIn, TOut>(this TIn value)
             ulong srcDefaultGenericToGenericResult = srcDefault.To<ulong, ulong>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (ulong)srcDefault can be used.
            //      2) If not can srcDefault.Toulong(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcDefault, typeof(ulong)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedDefaultBoxedToObjectResult = default(ulong);
            var expectedDefaultBoxedToGenericResult = default(ulong);
            var expectedDefaultGenericToBoxedResult = default(ulong);
            var expectedDefaultGenericToGenericResult = default(ulong);

            unsafe
            {
                ulong src = srcDefault;
                ulong* srcPtr = &src;
                expectedDefaultBoxedToObjectResult = *(ulong*)srcPtr;
                expectedDefaultBoxedToGenericResult = *(ulong*)srcPtr;
                expectedDefaultGenericToBoxedResult = *(ulong*)srcPtr;
                expectedDefaultGenericToGenericResult = *(ulong*)srcPtr;
            }


            Assert.True((ulong)srcDefaultBoxedToObjectResult == expectedDefaultBoxedToObjectResult);
            Assert.True(srcDefaultBoxedToGenericResult == expectedDefaultBoxedToGenericResult);
            Assert.True((ulong)srcDefaultGenericToBoxedResult == expectedDefaultGenericToBoxedResult);
            Assert.True(srcDefaultGenericToGenericResult == expectedDefaultGenericToGenericResult);
        }



        [Fact]
        public void ConvertToUInt64FromUInt64MinTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMinBoxedToObjectResult = srcMinBoxed.To(typeof(ulong));
                
            // Test: TOut To<TOut>(this object value)
            ulong srcMinBoxedToGenericResult = srcMinBoxed.To<ulong>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMinGenericToBoxedResult = srcMin.To(typeof(ulong));

            // Test: TOut To<TIn, TOut>(this TIn value)
             ulong srcMinGenericToGenericResult = srcMin.To<ulong, ulong>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (ulong)srcMin can be used.
            //      2) If not can srcMin.Toulong(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMin, typeof(ulong)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedMinBoxedToObjectResult = default(ulong);
            var expectedMinBoxedToGenericResult = default(ulong);
            var expectedMinGenericToBoxedResult = default(ulong);
            var expectedMinGenericToGenericResult = default(ulong);

            unsafe
            {
                ulong src = srcMin;
                ulong* srcPtr = &src;
                expectedMinBoxedToObjectResult = *(ulong*)srcPtr;
                expectedMinBoxedToGenericResult = *(ulong*)srcPtr;
                expectedMinGenericToBoxedResult = *(ulong*)srcPtr;
                expectedMinGenericToGenericResult = *(ulong*)srcPtr;
            }


            Assert.True((ulong)srcMinBoxedToObjectResult == expectedMinBoxedToObjectResult);
            Assert.True(srcMinBoxedToGenericResult == expectedMinBoxedToGenericResult);
            Assert.True((ulong)srcMinGenericToBoxedResult == expectedMinGenericToBoxedResult);
            Assert.True(srcMinGenericToGenericResult == expectedMinGenericToGenericResult);
        }



        [Fact]
        public void ConvertToUInt64FromUInt64MaxTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMaxBoxedToObjectResult = srcMaxBoxed.To(typeof(ulong));
                
            // Test: TOut To<TOut>(this object value)
            ulong srcMaxBoxedToGenericResult = srcMaxBoxed.To<ulong>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMaxGenericToBoxedResult = srcMax.To(typeof(ulong));

            // Test: TOut To<TIn, TOut>(this TIn value)
             ulong srcMaxGenericToGenericResult = srcMax.To<ulong, ulong>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (ulong)srcMax can be used.
            //      2) If not can srcMax.Toulong(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMax, typeof(ulong)) be used? (accounting for overflow)
            //      4) If not use unsafe

            

            var expectedMaxBoxedToObjectResult = default(ulong);
            var expectedMaxBoxedToGenericResult = default(ulong);
            var expectedMaxGenericToBoxedResult = default(ulong);
            var expectedMaxGenericToGenericResult = default(ulong);

            unsafe
            {
                ulong src = srcMax;
                ulong* srcPtr = &src;
                expectedMaxBoxedToObjectResult = *(ulong*)srcPtr;
                expectedMaxBoxedToGenericResult = *(ulong*)srcPtr;
                expectedMaxGenericToBoxedResult = *(ulong*)srcPtr;
                expectedMaxGenericToGenericResult = *(ulong*)srcPtr;
            }


            Assert.True((ulong)srcMaxBoxedToObjectResult == expectedMaxBoxedToObjectResult);
            Assert.True(srcMaxBoxedToGenericResult == expectedMaxBoxedToGenericResult);
            Assert.True((ulong)srcMaxGenericToBoxedResult == expectedMaxGenericToBoxedResult);
            Assert.True(srcMaxGenericToGenericResult == expectedMaxGenericToGenericResult);
        }



        [Fact]
        public void ConvertToSingleFromUInt64DefaultTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcDefaultBoxedToObjectResult = srcDefaultBoxed.To(typeof(float));
                
            // Test: TOut To<TOut>(this object value)
            float srcDefaultBoxedToGenericResult = srcDefaultBoxed.To<float>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcDefaultGenericToBoxedResult = srcDefault.To(typeof(float));

            // Test: TOut To<TIn, TOut>(this TIn value)
             float srcDefaultGenericToGenericResult = srcDefault.To<ulong, float>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (float)srcDefault can be used.
            //      2) If not can srcDefault.Tofloat(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcDefault, typeof(float)) be used? (accounting for overflow)
            //      4) If not use unsafe

            
            float expectedDefaultBoxedToObjectResult = float.Parse(srcDefaultFloatingPoint);
            float expectedDefaultBoxedToGenericResult = float.Parse(srcDefaultFloatingPoint);
            float expectedDefaultGenericToBoxedResult = float.Parse(srcDefaultFloatingPoint);
            float expectedDefaultGenericToGenericResult = float.Parse(srcDefaultFloatingPoint);


            Assert.True((float)srcDefaultBoxedToObjectResult == expectedDefaultBoxedToObjectResult);
            Assert.True(srcDefaultBoxedToGenericResult == expectedDefaultBoxedToGenericResult);
            Assert.True((float)srcDefaultGenericToBoxedResult == expectedDefaultGenericToBoxedResult);
            Assert.True(srcDefaultGenericToGenericResult == expectedDefaultGenericToGenericResult);
        }



        [Fact]
        public void ConvertToSingleFromUInt64MinTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMinBoxedToObjectResult = srcMinBoxed.To(typeof(float));
                
            // Test: TOut To<TOut>(this object value)
            float srcMinBoxedToGenericResult = srcMinBoxed.To<float>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMinGenericToBoxedResult = srcMin.To(typeof(float));

            // Test: TOut To<TIn, TOut>(this TIn value)
             float srcMinGenericToGenericResult = srcMin.To<ulong, float>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (float)srcMin can be used.
            //      2) If not can srcMin.Tofloat(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMin, typeof(float)) be used? (accounting for overflow)
            //      4) If not use unsafe

            
            float expectedMinBoxedToObjectResult = float.Parse(srcMinFloatingPoint);
            float expectedMinBoxedToGenericResult = float.Parse(srcMinFloatingPoint);
            float expectedMinGenericToBoxedResult = float.Parse(srcMinFloatingPoint);
            float expectedMinGenericToGenericResult = float.Parse(srcMinFloatingPoint);


            Assert.True((float)srcMinBoxedToObjectResult == expectedMinBoxedToObjectResult);
            Assert.True(srcMinBoxedToGenericResult == expectedMinBoxedToGenericResult);
            Assert.True((float)srcMinGenericToBoxedResult == expectedMinGenericToBoxedResult);
            Assert.True(srcMinGenericToGenericResult == expectedMinGenericToGenericResult);
        }



        [Fact]
        public void ConvertToSingleFromUInt64MaxTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMaxBoxedToObjectResult = srcMaxBoxed.To(typeof(float));
                
            // Test: TOut To<TOut>(this object value)
            float srcMaxBoxedToGenericResult = srcMaxBoxed.To<float>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMaxGenericToBoxedResult = srcMax.To(typeof(float));

            // Test: TOut To<TIn, TOut>(this TIn value)
             float srcMaxGenericToGenericResult = srcMax.To<ulong, float>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (float)srcMax can be used.
            //      2) If not can srcMax.Tofloat(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMax, typeof(float)) be used? (accounting for overflow)
            //      4) If not use unsafe

            
            float expectedMaxBoxedToObjectResult = float.Parse(srcMaxFloatingPoint);
            float expectedMaxBoxedToGenericResult = float.Parse(srcMaxFloatingPoint);
            float expectedMaxGenericToBoxedResult = float.Parse(srcMaxFloatingPoint);
            float expectedMaxGenericToGenericResult = float.Parse(srcMaxFloatingPoint);


            Assert.True((float)srcMaxBoxedToObjectResult == expectedMaxBoxedToObjectResult);
            Assert.True(srcMaxBoxedToGenericResult == expectedMaxBoxedToGenericResult);
            Assert.True((float)srcMaxGenericToBoxedResult == expectedMaxGenericToBoxedResult);
            Assert.True(srcMaxGenericToGenericResult == expectedMaxGenericToGenericResult);
        }



        [Fact]
        public void ConvertToDoubleFromUInt64DefaultTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcDefaultBoxedToObjectResult = srcDefaultBoxed.To(typeof(double));
                
            // Test: TOut To<TOut>(this object value)
            double srcDefaultBoxedToGenericResult = srcDefaultBoxed.To<double>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcDefaultGenericToBoxedResult = srcDefault.To(typeof(double));

            // Test: TOut To<TIn, TOut>(this TIn value)
             double srcDefaultGenericToGenericResult = srcDefault.To<ulong, double>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (double)srcDefault can be used.
            //      2) If not can srcDefault.Todouble(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcDefault, typeof(double)) be used? (accounting for overflow)
            //      4) If not use unsafe

            
            double expectedDefaultBoxedToObjectResult = double.Parse(srcDefaultFloatingPoint);
            double expectedDefaultBoxedToGenericResult = double.Parse(srcDefaultFloatingPoint);
            double expectedDefaultGenericToBoxedResult = double.Parse(srcDefaultFloatingPoint);
            double expectedDefaultGenericToGenericResult = double.Parse(srcDefaultFloatingPoint);


            Assert.True((double)srcDefaultBoxedToObjectResult == expectedDefaultBoxedToObjectResult);
            Assert.True(srcDefaultBoxedToGenericResult == expectedDefaultBoxedToGenericResult);
            Assert.True((double)srcDefaultGenericToBoxedResult == expectedDefaultGenericToBoxedResult);
            Assert.True(srcDefaultGenericToGenericResult == expectedDefaultGenericToGenericResult);
        }



        [Fact]
        public void ConvertToDoubleFromUInt64MinTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMinBoxedToObjectResult = srcMinBoxed.To(typeof(double));
                
            // Test: TOut To<TOut>(this object value)
            double srcMinBoxedToGenericResult = srcMinBoxed.To<double>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMinGenericToBoxedResult = srcMin.To(typeof(double));

            // Test: TOut To<TIn, TOut>(this TIn value)
             double srcMinGenericToGenericResult = srcMin.To<ulong, double>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (double)srcMin can be used.
            //      2) If not can srcMin.Todouble(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMin, typeof(double)) be used? (accounting for overflow)
            //      4) If not use unsafe

            
            double expectedMinBoxedToObjectResult = double.Parse(srcMinFloatingPoint);
            double expectedMinBoxedToGenericResult = double.Parse(srcMinFloatingPoint);
            double expectedMinGenericToBoxedResult = double.Parse(srcMinFloatingPoint);
            double expectedMinGenericToGenericResult = double.Parse(srcMinFloatingPoint);


            Assert.True((double)srcMinBoxedToObjectResult == expectedMinBoxedToObjectResult);
            Assert.True(srcMinBoxedToGenericResult == expectedMinBoxedToGenericResult);
            Assert.True((double)srcMinGenericToBoxedResult == expectedMinGenericToBoxedResult);
            Assert.True(srcMinGenericToGenericResult == expectedMinGenericToGenericResult);
        }



        [Fact]
        public void ConvertToDoubleFromUInt64MaxTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMaxBoxedToObjectResult = srcMaxBoxed.To(typeof(double));
                
            // Test: TOut To<TOut>(this object value)
            double srcMaxBoxedToGenericResult = srcMaxBoxed.To<double>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMaxGenericToBoxedResult = srcMax.To(typeof(double));

            // Test: TOut To<TIn, TOut>(this TIn value)
             double srcMaxGenericToGenericResult = srcMax.To<ulong, double>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (double)srcMax can be used.
            //      2) If not can srcMax.Todouble(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMax, typeof(double)) be used? (accounting for overflow)
            //      4) If not use unsafe

            
            double expectedMaxBoxedToObjectResult = double.Parse(srcMaxFloatingPoint);
            double expectedMaxBoxedToGenericResult = double.Parse(srcMaxFloatingPoint);
            double expectedMaxGenericToBoxedResult = double.Parse(srcMaxFloatingPoint);
            double expectedMaxGenericToGenericResult = double.Parse(srcMaxFloatingPoint);


            Assert.True((double)srcMaxBoxedToObjectResult == expectedMaxBoxedToObjectResult);
            Assert.True(srcMaxBoxedToGenericResult == expectedMaxBoxedToGenericResult);
            Assert.True((double)srcMaxGenericToBoxedResult == expectedMaxGenericToBoxedResult);
            Assert.True(srcMaxGenericToGenericResult == expectedMaxGenericToGenericResult);
        }



        [Fact]
        public void ConvertToDecimalFromUInt64DefaultTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcDefaultBoxedToObjectResult = srcDefaultBoxed.To(typeof(decimal));
                
            // Test: TOut To<TOut>(this object value)
            decimal srcDefaultBoxedToGenericResult = srcDefaultBoxed.To<decimal>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcDefaultGenericToBoxedResult = srcDefault.To(typeof(decimal));

            // Test: TOut To<TIn, TOut>(this TIn value)
             decimal srcDefaultGenericToGenericResult = srcDefault.To<ulong, decimal>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (decimal)srcDefault can be used.
            //      2) If not can srcDefault.Todecimal(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcDefault, typeof(decimal)) be used? (accounting for overflow)
            //      4) If not use unsafe

            
            decimal expectedDefaultBoxedToObjectResult = decimal.Parse(srcDefaultFloatingPoint);
            decimal expectedDefaultBoxedToGenericResult = decimal.Parse(srcDefaultFloatingPoint);
            decimal expectedDefaultGenericToBoxedResult = decimal.Parse(srcDefaultFloatingPoint);
            decimal expectedDefaultGenericToGenericResult = decimal.Parse(srcDefaultFloatingPoint);


            Assert.True((decimal)srcDefaultBoxedToObjectResult == expectedDefaultBoxedToObjectResult);
            Assert.True(srcDefaultBoxedToGenericResult == expectedDefaultBoxedToGenericResult);
            Assert.True((decimal)srcDefaultGenericToBoxedResult == expectedDefaultGenericToBoxedResult);
            Assert.True(srcDefaultGenericToGenericResult == expectedDefaultGenericToGenericResult);
        }



        [Fact]
        public void ConvertToDecimalFromUInt64MinTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMinBoxedToObjectResult = srcMinBoxed.To(typeof(decimal));
                
            // Test: TOut To<TOut>(this object value)
            decimal srcMinBoxedToGenericResult = srcMinBoxed.To<decimal>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMinGenericToBoxedResult = srcMin.To(typeof(decimal));

            // Test: TOut To<TIn, TOut>(this TIn value)
             decimal srcMinGenericToGenericResult = srcMin.To<ulong, decimal>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (decimal)srcMin can be used.
            //      2) If not can srcMin.Todecimal(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMin, typeof(decimal)) be used? (accounting for overflow)
            //      4) If not use unsafe

            
            decimal expectedMinBoxedToObjectResult = decimal.Parse(srcMinFloatingPoint);
            decimal expectedMinBoxedToGenericResult = decimal.Parse(srcMinFloatingPoint);
            decimal expectedMinGenericToBoxedResult = decimal.Parse(srcMinFloatingPoint);
            decimal expectedMinGenericToGenericResult = decimal.Parse(srcMinFloatingPoint);


            Assert.True((decimal)srcMinBoxedToObjectResult == expectedMinBoxedToObjectResult);
            Assert.True(srcMinBoxedToGenericResult == expectedMinBoxedToGenericResult);
            Assert.True((decimal)srcMinGenericToBoxedResult == expectedMinGenericToBoxedResult);
            Assert.True(srcMinGenericToGenericResult == expectedMinGenericToGenericResult);
        }



        [Fact]
        public void ConvertToDecimalFromUInt64MaxTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMaxBoxedToObjectResult = srcMaxBoxed.To(typeof(decimal));
                
            // Test: TOut To<TOut>(this object value)
            decimal srcMaxBoxedToGenericResult = srcMaxBoxed.To<decimal>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMaxGenericToBoxedResult = srcMax.To(typeof(decimal));

            // Test: TOut To<TIn, TOut>(this TIn value)
             decimal srcMaxGenericToGenericResult = srcMax.To<ulong, decimal>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (decimal)srcMax can be used.
            //      2) If not can srcMax.Todecimal(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMax, typeof(decimal)) be used? (accounting for overflow)
            //      4) If not use unsafe

            
            decimal expectedMaxBoxedToObjectResult = decimal.Parse(srcMaxFloatingPoint);
            decimal expectedMaxBoxedToGenericResult = decimal.Parse(srcMaxFloatingPoint);
            decimal expectedMaxGenericToBoxedResult = decimal.Parse(srcMaxFloatingPoint);
            decimal expectedMaxGenericToGenericResult = decimal.Parse(srcMaxFloatingPoint);


            Assert.True((decimal)srcMaxBoxedToObjectResult == expectedMaxBoxedToObjectResult);
            Assert.True(srcMaxBoxedToGenericResult == expectedMaxBoxedToGenericResult);
            Assert.True((decimal)srcMaxGenericToBoxedResult == expectedMaxGenericToBoxedResult);
            Assert.True(srcMaxGenericToGenericResult == expectedMaxGenericToGenericResult);
        }


        [Fact]
        public void ConvertToDateTimeFromUInt64DefaultTest() 
        {
            Assert.Throws<InvalidCastException>(() => srcDefaultBoxed.To(typeof(DateTime)));
            Assert.Throws<InvalidCastException>(() => srcDefaultBoxed.To<DateTime>());
            Assert.Throws<InvalidCastException>(() => srcDefault.To(typeof(DateTime)));
            Assert.Throws<InvalidCastException>(() => srcDefault.To<ulong, DateTime>());
        }


        [Fact]
        public void ConvertToDateTimeFromUInt64MinTest() 
        {
            Assert.Throws<InvalidCastException>(() => srcMinBoxed.To(typeof(DateTime)));
            Assert.Throws<InvalidCastException>(() => srcMinBoxed.To<DateTime>());
            Assert.Throws<InvalidCastException>(() => srcMin.To(typeof(DateTime)));
            Assert.Throws<InvalidCastException>(() => srcMin.To<ulong, DateTime>());
        }


        [Fact]
        public void ConvertToDateTimeFromUInt64MaxTest() 
        {
            Assert.Throws<InvalidCastException>(() => srcMaxBoxed.To(typeof(DateTime)));
            Assert.Throws<InvalidCastException>(() => srcMaxBoxed.To<DateTime>());
            Assert.Throws<InvalidCastException>(() => srcMax.To(typeof(DateTime)));
            Assert.Throws<InvalidCastException>(() => srcMax.To<ulong, DateTime>());
        }



        [Fact]
        public void ConvertToStringFromUInt64DefaultTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcDefaultBoxedToObjectResult = srcDefaultBoxed.To(typeof(string));
                
            // Test: TOut To<TOut>(this object value)
            string srcDefaultBoxedToGenericResult = srcDefaultBoxed.To<string>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcDefaultGenericToBoxedResult = srcDefault.To(typeof(string));

            // Test: TOut To<TIn, TOut>(this TIn value)
             string srcDefaultGenericToGenericResult = srcDefault.To<ulong, string>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (string)srcDefault can be used.
            //      2) If not can srcDefault.Tostring(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcDefault, typeof(string)) be used? (accounting for overflow)
            //      4) If not use unsafe

            
            string expectedDefaultBoxedToObjectResult = srcDefault.ToString();
            string expectedDefaultBoxedToGenericResult = srcDefault.ToString();
            string expectedDefaultGenericToBoxedResult = srcDefault.ToString();
            string expectedDefaultGenericToGenericResult = srcDefault.ToString();


            Assert.True((string)srcDefaultBoxedToObjectResult == expectedDefaultBoxedToObjectResult);
            Assert.True(srcDefaultBoxedToGenericResult == expectedDefaultBoxedToGenericResult);
            Assert.True((string)srcDefaultGenericToBoxedResult == expectedDefaultGenericToBoxedResult);
            Assert.True(srcDefaultGenericToGenericResult == expectedDefaultGenericToGenericResult);
        }



        [Fact]
        public void ConvertToStringFromUInt64MinTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMinBoxedToObjectResult = srcMinBoxed.To(typeof(string));
                
            // Test: TOut To<TOut>(this object value)
            string srcMinBoxedToGenericResult = srcMinBoxed.To<string>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMinGenericToBoxedResult = srcMin.To(typeof(string));

            // Test: TOut To<TIn, TOut>(this TIn value)
             string srcMinGenericToGenericResult = srcMin.To<ulong, string>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (string)srcMin can be used.
            //      2) If not can srcMin.Tostring(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMin, typeof(string)) be used? (accounting for overflow)
            //      4) If not use unsafe

            
            string expectedMinBoxedToObjectResult = srcMin.ToString();
            string expectedMinBoxedToGenericResult = srcMin.ToString();
            string expectedMinGenericToBoxedResult = srcMin.ToString();
            string expectedMinGenericToGenericResult = srcMin.ToString();


            Assert.True((string)srcMinBoxedToObjectResult == expectedMinBoxedToObjectResult);
            Assert.True(srcMinBoxedToGenericResult == expectedMinBoxedToGenericResult);
            Assert.True((string)srcMinGenericToBoxedResult == expectedMinGenericToBoxedResult);
            Assert.True(srcMinGenericToGenericResult == expectedMinGenericToGenericResult);
        }



        [Fact]
        public void ConvertToStringFromUInt64MaxTest() 
        {

            // Test: object To(this object value, Type type) 
            object srcMaxBoxedToObjectResult = srcMaxBoxed.To(typeof(string));
                
            // Test: TOut To<TOut>(this object value)
            string srcMaxBoxedToGenericResult = srcMaxBoxed.To<string>();

            // Test: object To<TIn>(this TIn value, Type type)
            object srcMaxGenericToBoxedResult = srcMax.To(typeof(string));

            // Test: TOut To<TIn, TOut>(this TIn value)
             string srcMaxGenericToGenericResult = srcMax.To<ulong, string>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct (string)srcMax can be used.
            //      2) If not can srcMax.Tostring(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(srcMax, typeof(string)) be used? (accounting for overflow)
            //      4) If not use unsafe

            
            string expectedMaxBoxedToObjectResult = srcMax.ToString();
            string expectedMaxBoxedToGenericResult = srcMax.ToString();
            string expectedMaxGenericToBoxedResult = srcMax.ToString();
            string expectedMaxGenericToGenericResult = srcMax.ToString();


            Assert.True((string)srcMaxBoxedToObjectResult == expectedMaxBoxedToObjectResult);
            Assert.True(srcMaxBoxedToGenericResult == expectedMaxBoxedToGenericResult);
            Assert.True((string)srcMaxGenericToBoxedResult == expectedMaxGenericToBoxedResult);
            Assert.True(srcMaxGenericToGenericResult == expectedMaxGenericToGenericResult);
        }


    }
}

