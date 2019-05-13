using System.Collections.Generic;
using System.Linq;
using Xunit;
using static System.Runtime.RuntimeServices.Tests.ConvertTests;

namespace System.Runtime.RuntimeServices.Tests
{

    public partial class ParameterizedConversionTests
    {


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
