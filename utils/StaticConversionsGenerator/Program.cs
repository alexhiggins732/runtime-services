using System.Runtime.ConversionServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.ConversionServices.Conversions;

namespace StaticConversionsGenerator
{
    partial class Program
    {
        static void Main(string[] args)
        {
            GenericRuntimeTypeTests.TestGenericTypeDefinition();

            GenericCallFactory.ResolverFactoryTest.Test();

            //GenericBinder.GenericBinderTest.Run();
            ArithmeticFactory.TestInterfaceCall();
            var b = new SignatureBuilderTest();
            SignatureBuilderTest.TestBuilder();
            SignatureBuilderTest.TypeRefActionTest();

            SignatureTests.AnonTypeKey();
            var expressions = LinqExpressionStructBuilder.GetExpressions();


            TypedReferenceDevelopHelper.RunOperatorTests();



            var boolMax = true;
            var boolMaxSingle = boolMax.To<float>();
            System.Console.WriteLine(boolMaxSingle);
           

            var boolMaxIsLessThanboolMaxSingle = boolMax.Comparer().LessThan(boolMaxSingle);

            //Builtin:
            // sbyte.MaxValue.CompareTo(boolMax)
            // result: System.ArgumentException: 'Object must be of type SByte.'
            var sbyteMaxIsGreaterThanBoolMaxSingle = sbyte.MaxValue.Comparer().GreaterThan(boolMax);
            //result: true

            int compareResult = sbyte.MaxValue.Compare(boolMax);
            // result: 126

            var sbyteCompareResult = sbyte.MaxValue.CompareTo(1);
            // result: 126

            // var dateFromBool = (DateTime)false;
            // result: won't compile. -> Cannot convert type 'bool' to 'System.DateTime' 

            //var boolMaxToData = boolMax.To<DateTime>();
            // result: invalid cast exception

            var helloWorldCharArray = "Hello World".ToCharArray();
            var helloWorldFromCharArray = helloWorldCharArray.ToString();
            // result: helloWorldFromCharArray = "System.Char[]"
            var hellWorld = helloWorldCharArray.To<string>();
            // result: hellWorld = "Hello World";
            var fromMaxValue = ushort.MaxValue;

            //Cannot even compile: Cannot convert type 'ushort' to 'string'   
            // var fromMaxValueString = (string)fromMaxValue;
            var fromMaxValueString = fromMaxValue.To<string>();
            // result: fromMaxValueString = "65535"
            var charArrayFromMaxValue = fromMaxValue.To<char[]>();
            // result: charArrayFromMaxValue = char[5] { '6', '5', '5', '3', '5' }



            //This works:
            var toSbyteResult = (sbyte)fromMaxValue;
            // result: toSbyteResult = -1

            //However, it fromMaxValue is an object  it Throws and exception!
            object boxedFromMaxValue = fromMaxValue;
            //var toSbyteResultFromBoxed = (sbyte)boxedFromMaxValue;
            // result: System.InvalidCastException: 'Specified cast is not valid.'
            var toSbyteResultFromRuntimeBoxed = boxedFromMaxValue.To<sbyte>();
            // result: toSbyteResultFromRuntimeBoxed = -1

            var sbyteCastsAreEqual = toSbyteResult == toSbyteResultFromRuntimeBoxed;
            //result: sbyteCastsAreEqual = true

            //TODO: While the libary current unboxes objects to their correct type
            // before converting as required there is still no support for 
            // usage of runtime comparison operators (eg, ==, <, <=, >, >=)

            byte a = 1;
            object aBoxedByte = a;
            //var eq = a == aBoxedByte;
            //result:  Operator '==' cannot be applied to operands of type 'byte' and 'object' 

            object anotherBoxedByte = a;
            var boxedBytesAreEqual = aBoxedByte == anotherBoxedByte;
            // result: false; => the compiler is comparing the address of the object references instead of the values

            var anUnboxedByte = aBoxedByte.Unbox();
            var anotherUnboxedByte = anotherBoxedByte.Unbox();

            var boxedValueEqualsUnboxedValue = anUnboxedByte.Equals(a);
            //resul: equal = true

            var anUnboxedByteEqualsanotherUnboxedbyte = anUnboxedByte.Equals(anotherBoxedByte);

         

        }
    }
}
