using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.ConversionServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.ConversionServices.Conversions;

namespace StaticConversionsGenerator
{
    partial class Program
    {
        static object GetExpressions()
        {
            var t = typeof(System.Linq.Expressions.Expression);
            var methods = t.GetMethods(BindingFlags.Public | BindingFlags.Static);
            var d = methods.ToLookup(x => x.ReturnType, x => x);
            var ms = methods.OrderBy(x => x.ReturnType.Name).ThenBy(x => x.Name)
                .Select(x => $"{x}")
                .ToList();
            var defs = string.Join("\r\n", ms);

            var codebuilder = new System.Text.StringBuilder();
            var methodList = methods.OrderBy(x => x.ReturnType.Name).ThenBy(x => x.Name).ToList();

            foreach (var method in methodList)
            {
                var sb = new System.Text.StringBuilder();
                var name = method.Name;
                var args = method.GetParameters();
                if (args.Any(x => (x.ParameterType.Name == "MethodInfo") || x.ParameterType != typeof(System.Linq.Expressions.Expression) && x.ParameterType.Name.Contains("Expression"))) continue;
                sb.Append($"public struct {name}");

                var genericParams = new List<string>();
                foreach (var arg in args)
                {
                    if (arg.ParameterType == t)
                    {
                        genericParams.Add($"T{genericParams.Count + 1}");
                    }
                    else
                    {
                        genericParams.Add(arg.ParameterType.Name);
                    }
                }
                if (genericParams.Count > 0)
                {
                    sb.Append($"<{string.Join(", ", genericParams)}>");
                }
                sb.AppendLine();
                sb.AppendLine("{");

                sb.AppendLine($"\tstatic Func<{string.Join(", ", genericParams)}> op;");
                sb.AppendLine($"\tpublic static Func<{string.Join(", ", genericParams)}> Op = (op ?? CreateFunc<{string.Join(", ", genericParams)}>(Operation.{name}));");
                sb.AppendLine("}");
                var structdef = sb.ToString();
                codebuilder.AppendLine(structdef);
            }
            var structCode = codebuilder.ToString();



            return defs;

        }
        public enum OpKey
        {
            Add,
            AddUnchecked,
            Subtract,
            SubtractUnchecked
        }

        static T1 OpKeySignatureStub<OpKey, T1>(OpKey a) => default(T1);

        static T2 OpKeySignatureStub<OpKey, T1, T2>(OpKey a, T1 b) => default(T2);

        static Func<OpKey, T1> OpKeySignature<OpKey, T1>(OpKey a, T1 b)
       => ((d) => OpKeySignatureStub<OpKey, T1>(d));

        static Func<OpKey, T1, T2> OpKeySignature<OpKey, T1, T2>(OpKey a, T1 b, T2 c)
            => ((d, e) => OpKeySignatureStub<OpKey, T1, T2>(d, e));


        //static Func<OpKey, T1, T2> GetKey<OpKey, T1, T2>(OpKey a, T1 b, T2 c)
        //{
        //    var fn = (Func<OpKey, T1, T2>)((d,e) => GetKey2<OpKey, T1, T2>(d,e));
        //    return fn;
        //}
        static void AnonTypeKey()
        {
            var r = OpKeySignature(OpKey.Add, 1, 1);
            var t = r.Method.ReturnType;

            var d = new Dictionary<Type, Delegate>();
            d.Add(r.GetType(), r);

            var rs = OpKeySignature(OpKey.Subtract, 1, 1);
            var ts = r.Method.ReturnType;
            d.Add(rs.GetType(), r);

            var r2 = OpKeySignature(OpKey.Add, 1ul, 1);
            d.Add(r2.GetType(), r);

            var r3 = OpKeySignature((dynamic)OpKey.Add, 1ul, 1);
            d.Add(r3.Method.ReturnType, r);

            var r4 = OpKeySignature(OpKey.Add, 1ul, 1);
            d.Add(r4.GetType(), r);

        }
        static void Main(string[] args)
        {
            GenericRuntimeTypeDefinitionFactory.TestGenericTypeDefinition();

            GenericCallFactory.ResolverFactoryTest.Test();

            //GenericBinder.GenericBinderTest.Run();
            ArithmeticFactory.TestInterfaceCall();
            var b = new SignatureBuilderTest();
            SignatureBuilderTest.TestBuilder();
            SignatureBuilderTest.TypeRefActionTest();

            AnonTypeKey();
            var expressions = GetExpressions();
       

            TypedReferenceDevelopHelper.RunOperatorTests();

            GenericArithmetic.Test();

            var boolMax = true;
            var boolMaxSingle = boolMax.To<float>();

            var boolMaxIsLessThanboolMaxSingle = boolMax.Compare().LessThan(boolMaxSingle);

            //Builtin:
            // sbyte.MaxValue.CompareTo(boolMax)
            // result: System.ArgumentException: 'Object must be of type SByte.'
            var sbyteMaxIsGreaterThanBoolMaxSingle = sbyte.MaxValue.Compare().GreaterThan(boolMax);
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


            GenerateIConveribleTests();

            Func<int[]> ilTestFunc = ILTest;

            var method = typeof(Program).GetMethod(nameof(ILTest), BindingFlags.NonPublic | BindingFlags.Static);
            var il = method.GetMethodBody().GetILAsByteArray();
            var expected = ILTest();

            var result = ExecuteIl(il, method, ilTestFunc.Method.Module).To<int[]>();
            bool equals = expected.SequenceEqual(result.To<int[]>());

        }

        public struct IConvertibleInfo
        {
            public Type RuntimeType;
            public TypeCode TypeCode;
            public IConvertible DefaultValue;
            public IConvertible MinValue;
            public IConvertible MaxValue;
            public int RuntimeBits;
            public int LogicalBits;
            public bool Signed;
            public string Alias;
            public string TypeName;
            public override string ToString() => $"{TypeCode}";

        }
        public class TestTypes
        {
            public const string Default = nameof(Default);
            public const string Min = nameof(Min);
            public const string Max = nameof(Max);
            public static readonly string[] AllTypes = new string[] { Default, Min, Max };
        }
        public static void GenerateIConveribleTests()
        {
            var IConvertibleType = typeof(IConvertible);
            var IConvertibleMethods = typeof(IConvertible).GetMethods().Where(x => x.GetParameters().Length == 1).ToList();
            var IConvertibleTypes = IConvertibleMethods.Select(x => x.ReturnType).ToList();

            var nullObject = (object)null;

            var IcDefaultValues = new Dictionary<Type, IConvertibleInfo>();


            foreach (var icType in IConvertibleTypes)
            {
                var info = new IConvertibleInfo();
                info.DefaultValue = (IConvertible)nullObject.To(icType);
                info.RuntimeType = icType;
                info.TypeCode = info.DefaultValue?.GetTypeCode() ?? TypeCode.Empty;
                switch (info.TypeCode)
                {
                    case TypeCode.Boolean:
                        info.DefaultValue = default(bool);
                        info.MinValue = false;
                        info.MaxValue = true;
                        info.LogicalBits = 1;
                        info.RuntimeBits = 4;
                        info.Signed = false;
                        info.Alias = "bool";
                        info.TypeName = nameof(TypeCode.Boolean);
                        break;
                    case TypeCode.Char:
                        info.DefaultValue = default(char);
                        info.MinValue = char.MinValue;
                        info.MaxValue = char.MaxValue;
                        info.LogicalBits = 16;
                        info.RuntimeBits = 2;
                        info.Signed = false;
                        info.Alias = "char";
                        info.TypeName = nameof(TypeCode.Char);
                        break;
                    case TypeCode.SByte:
                        info.DefaultValue = default(sbyte);
                        info.MinValue = sbyte.MinValue;
                        info.MaxValue = sbyte.MaxValue;
                        info.LogicalBits = 8;
                        info.RuntimeBits = 32;
                        info.Signed = true;
                        info.Alias = "sbyte";
                        info.TypeName = nameof(TypeCode.SByte);
                        break;
                    case TypeCode.Byte:
                        info.DefaultValue = default(byte);
                        info.MinValue = byte.MinValue;
                        info.MaxValue = byte.MaxValue;
                        info.LogicalBits = 8;
                        info.RuntimeBits = 32;
                        info.Signed = false;
                        info.Alias = "byte";
                        info.TypeName = nameof(TypeCode.Byte);
                        break;
                    case TypeCode.Int16:
                        info.DefaultValue = default(short);
                        info.MinValue = short.MinValue;
                        info.MaxValue = short.MaxValue;
                        info.LogicalBits = 16;
                        info.RuntimeBits = 32;
                        info.Signed = true;
                        info.Alias = "short";
                        info.TypeName = nameof(TypeCode.Int16);
                        break;
                    case TypeCode.UInt16:
                        info.DefaultValue = default(ushort);
                        info.MinValue = ushort.MinValue;
                        info.MaxValue = ushort.MaxValue;
                        info.LogicalBits = 16;
                        info.RuntimeBits = 32;
                        info.Signed = false;
                        info.Alias = "ushort";
                        info.TypeName = nameof(TypeCode.UInt16);
                        break;
                    case TypeCode.Int32:
                        info.DefaultValue = default(int);
                        info.MinValue = int.MinValue;
                        info.MaxValue = int.MaxValue;
                        info.LogicalBits = 32;
                        info.RuntimeBits = 32;
                        info.Signed = true;
                        info.Alias = "int";
                        info.TypeName = nameof(TypeCode.Int32);
                        break;
                    case TypeCode.UInt32:
                        info.DefaultValue = default(uint);
                        info.MinValue = uint.MinValue;
                        info.MaxValue = uint.MaxValue;
                        info.LogicalBits = 32;
                        info.RuntimeBits = 32;
                        info.Signed = false;
                        info.Alias = "uint";
                        info.TypeName = nameof(TypeCode.UInt32);
                        break;
                    case TypeCode.Int64:
                        info.DefaultValue = default(long);
                        info.MinValue = long.MinValue;
                        info.MaxValue = long.MaxValue;
                        info.LogicalBits = 64;
                        info.RuntimeBits = 64;
                        info.Signed = true;
                        info.Alias = "long";
                        info.TypeName = nameof(TypeCode.Int64);
                        break;
                    case TypeCode.UInt64:
                        info.DefaultValue = default(ulong);
                        info.MinValue = ulong.MinValue;
                        info.MaxValue = ulong.MaxValue;
                        info.LogicalBits = 64;
                        info.RuntimeBits = 64;
                        info.Signed = false;
                        info.Alias = "ulong";
                        info.TypeName = nameof(TypeCode.UInt64);
                        break;
                    case TypeCode.Single:
                        info.DefaultValue = default(float);
                        info.MinValue = float.MinValue;
                        info.MaxValue = float.MaxValue;
                        info.LogicalBits = 32;
                        info.RuntimeBits = 32;
                        info.Signed = true;
                        info.Alias = "float";
                        info.TypeName = nameof(TypeCode.Single);
                        break;
                    case TypeCode.Double:
                        info.DefaultValue = default(double);
                        info.MinValue = double.MinValue;
                        info.MaxValue = double.MaxValue;
                        info.LogicalBits = 64;
                        info.RuntimeBits = 64;
                        info.Signed = false;
                        info.Alias = "double";
                        info.TypeName = nameof(TypeCode.Double);
                        break;
                    case TypeCode.Decimal:
                        info.DefaultValue = default(decimal);
                        info.MinValue = decimal.MinValue;
                        info.MaxValue = decimal.MaxValue;
                        info.LogicalBits = 128;
                        info.RuntimeBits = 128;
                        info.Signed = false;
                        info.Alias = "decimal";
                        info.TypeName = nameof(TypeCode.Decimal);
                        break;
                    case TypeCode.DateTime:
                        info.DefaultValue = default(DateTime);
                        info.MinValue = DateTime.MinValue;
                        info.MaxValue = DateTime.MaxValue;
                        info.LogicalBits = 32;
                        info.RuntimeBits = 32;
                        info.Signed = false;
                        info.Alias = "DateTime";
                        info.TypeName = nameof(TypeCode.DateTime);
                        break;
                    case TypeCode.Object:
                    case TypeCode.Empty:
                    case TypeCode.String:
                        info.DefaultValue = default(string);
                        info.MinValue = null;
                        info.MaxValue = null;
                        info.LogicalBits = int.MaxValue;
                        info.RuntimeBits = int.MaxValue;
                        info.Signed = false;
                        var name = info.RuntimeType.Name;
                        switch (name)
                        {
                            case "String":
                                info.TypeCode = TypeCode.String;
                                info.Alias = "string";
                                info.TypeName = nameof(TypeCode.String);
                                break;
                            case "Object":
                                info.TypeCode = TypeCode.Object;
                                info.Alias = "object";
                                info.TypeName = nameof(TypeCode.Object);
                                break;
                            default:
                                info.TypeCode = TypeCode.Empty;
                                info.Alias = "null";
                                info.TypeName = "null";
                                break;
                        }
                        break;

                    default:

                        break;
                }




                IcDefaultValues.Add(icType, info);
            }


            var testTypes = TestTypes.AllTypes;

            foreach (var fromInfo in IcDefaultValues.Values.ToList())
            {
                //TODO: implement string tests
                if (fromInfo.TypeCode == TypeCode.String) continue;
                var srcDefault = fromInfo.DefaultValue;
                var srcDefaultBoxed = (object)srcDefault;
                var srcTypeAlias = fromInfo.Alias;
                var srcTypeName = fromInfo.TypeName;
                var quotedDefault = $"\"{fromInfo.DefaultValue}\"";
                var quotedMax = $"\"{fromInfo.MaxValue}\"";
                var quotedMin = $"\"{fromInfo.MinValue}\"";

                var quotedFloatingPointDefault = $"\"{(fromInfo.TypeCode == TypeCode.Boolean ? 0 : fromInfo.DefaultValue)}\"";
                var quotedFloatingPointMax = $"\"{(fromInfo.TypeCode == TypeCode.Boolean ? 1 : fromInfo.MaxValue)}\"";
                var quotedFloatingPointMin = $"\"{(fromInfo.TypeCode == TypeCode.Boolean ? 0 : fromInfo.MinValue)}\"";

                var testBodyInitializer = $@"
        //initalize default, min and maxed as well as boxed versions of each
        public {srcTypeAlias} srcDefault = {srcTypeAlias}.Parse({quotedDefault});
        public {srcTypeAlias} srcMin = {srcTypeAlias}.Parse({quotedMin});
        public {srcTypeAlias} srcMax = {srcTypeAlias}.Parse({quotedMax});
        public object srcDefaultBoxed => (object)srcDefault;
        public object srcMinBoxed =>   (object)srcMin;
        public object srcMaxBoxed =>  (object)srcMax;

        public string srcDefaultFloatingPoint = {(fromInfo.TypeCode == TypeCode.Char ? "0.ToString()" : quotedFloatingPointDefault)};
        public string srcMinFloatingPoint = {(fromInfo.TypeCode == TypeCode.Char ? "0.ToString()" : quotedFloatingPointMin)};
        public string srcMaxFloatingPoint = {(fromInfo.TypeCode == TypeCode.Char ? "ushort.MaxValue.ToString()" : quotedFloatingPointMax)};

        //perform boxed and unboxed conversions using the various cast operators.

";

                var tests = new List<String>();

                foreach (var toInfo in IcDefaultValues.Values.ToList())
                {
                    var destType = toInfo.RuntimeType;
                    var destTypeAlias = toInfo.Alias;
                    var destTypeName = toInfo.TypeName;
                    var destDefault = toInfo.DefaultValue;
                    var destDefaultBoxed = (object)destDefault;
                    var destMin = toInfo.MinValue;
                    var destMinBoxed = (object)destMin;
                    var destMax = toInfo.MaxValue;
                    var destMaxBoxed = (object)destMax;
                    var quotedDestDefault = $"\"{destDefault}\"";
                    var quotedDestMin = $"\"{destMin}\"";
                    var quotedDestMax = $"\"{destMax}\"";


                    foreach (var testType in testTypes)
                    {
                        string expectedCode = "";
                        switch (toInfo.TypeCode)
                        {
                            case TypeCode.DateTime:
                                switch (fromInfo.TypeCode)
                                {
                                    case TypeCode.Int32:
                                    case TypeCode.Int64:

                                        expectedCode = $@"
            {destTypeAlias} expected{testType}BoxedToObjectResult = new DateTime((long)src{testType});
            {destTypeAlias} expected{testType}BoxedToGenericResult = new DateTime((long)src{testType});
            {destTypeAlias} expected{testType}GenericToBoxedResult = new DateTime((long)src{testType});
            {destTypeAlias} expected{testType}GenericToGenericResult = new DateTime((long)src{testType});
";
                                        break;
                                    case TypeCode.String:
                                        break;
                                    default:
                                        //Assert.Throws<InvalidCastException>(() => srcDefaultBoxed.To(typeof(DateTime)));
                                        expectedCode = $@"
        [Fact]
        public void ConvertTo{destTypeName}From{srcTypeName}{testType}Test() 
        {{
            Assert.Throws<InvalidCastException>(() => src{testType}Boxed.To(typeof({destTypeAlias})));
            Assert.Throws<InvalidCastException>(() => src{testType}Boxed.To<{destTypeAlias}>());
            Assert.Throws<InvalidCastException>(() => src{testType}.To(typeof({destTypeAlias})));
            Assert.Throws<InvalidCastException>(() => src{testType}.To<{srcTypeAlias}, {destTypeAlias}>());
        }}
";
                                        tests.Add(expectedCode);
                                        continue;

                                }
                                break;
                            case TypeCode.String:
                                expectedCode = $@"
            {destTypeAlias} expected{testType}BoxedToObjectResult = src{testType}.ToString();
            {destTypeAlias} expected{testType}BoxedToGenericResult = src{testType}.ToString();
            {destTypeAlias} expected{testType}GenericToBoxedResult = src{testType}.ToString();
            {destTypeAlias} expected{testType}GenericToGenericResult = src{testType}.ToString();
";
                                break;
                            case TypeCode.Boolean:
                                switch (testType)
                                {
                                    case TestTypes.Max:
                                        expectedCode = $@"
            var expected{testType}BoxedToObjectResult = true;
            var expected{testType}BoxedToGenericResult = true;
            var expected{testType}GenericToBoxedResult = true;
            var expected{testType}GenericToGenericResult = true;
";
                                        break;
                                    case TestTypes.Min:
                                        var bExpected = fromInfo.Signed ? "true" : "false";
                                        expectedCode = $@"
            var expected{testType}BoxedToObjectResult = {bExpected};
            var expected{testType}BoxedToGenericResult = {bExpected};
            var expected{testType}GenericToBoxedResult = {bExpected};
            var expected{testType}GenericToGenericResult = {bExpected};
";

                                        break;
                                    case TestTypes.Default:
                                        expectedCode = $@"
            var expected{testType}BoxedToObjectResult = false;
            var expected{testType}BoxedToGenericResult = false;
            var expected{testType}GenericToBoxedResult = false;
            var expected{testType}GenericToGenericResult = false;
";

                                        break;
                                }
                                break;


                            case TypeCode.Double:
                            case TypeCode.Decimal:
                            case TypeCode.Single:

                                expectedCode = $@"
            {destTypeAlias} expected{testType}BoxedToObjectResult = {destTypeAlias}.Parse(src{testType}FloatingPoint);
            {destTypeAlias} expected{testType}BoxedToGenericResult = {destTypeAlias}.Parse(src{testType}FloatingPoint);
            {destTypeAlias} expected{testType}GenericToBoxedResult = {destTypeAlias}.Parse(src{testType}FloatingPoint);
            {destTypeAlias} expected{testType}GenericToGenericResult = {destTypeAlias}.Parse(src{testType}FloatingPoint);
";

                                break;

                            default:
                                var expectedValue = "";
                                switch (testType)
                                {
                                    case TestTypes.Default:
                                        expectedValue = quotedDefault;
                                        break;
                                    case TestTypes.Min:
                                        expectedValue = quotedDestMin;
                                        break;
                                    case TestTypes.Max:
                                        expectedValue = quotedDestMax;
                                        break;

                                }
                                expectedCode = $@"

            var expected{testType}BoxedToObjectResult = default({destTypeAlias});
            var expected{testType}BoxedToGenericResult = default({destTypeAlias});
            var expected{testType}GenericToBoxedResult = default({destTypeAlias});
            var expected{testType}GenericToGenericResult = default({destTypeAlias});

            unsafe
            {{
                {srcTypeAlias} src = src{testType};
                {srcTypeAlias}* srcPtr = &src;
                expected{testType}BoxedToObjectResult = *({destTypeAlias}*)srcPtr;
                expected{testType}BoxedToGenericResult = *({destTypeAlias}*)srcPtr;
                expected{testType}GenericToBoxedResult = *({destTypeAlias}*)srcPtr;
                expected{testType}GenericToGenericResult = *({destTypeAlias}*)srcPtr;
            }}
";
                                break;
                        }


                        var testMethodCode = $@"

        [Fact]
        public void ConvertTo{destTypeName}From{srcTypeName}{testType}Test() 
        {{

            // Test: object To(this object value, Type type) 
            object src{testType}BoxedToObjectResult = src{testType}Boxed.To(typeof({destTypeAlias}));
                
            // Test: TOut To<TOut>(this object value)
            {destTypeAlias} src{testType}BoxedToGenericResult = src{testType}Boxed.To<{destTypeAlias}>();

            // Test: object To<TIn>(this TIn value, Type type)
            object src{testType}GenericToBoxedResult = src{testType}.To(typeof({destTypeAlias}));

            // Test: TOut To<TIn, TOut>(this TIn value)
             {destTypeAlias} src{testType}GenericToGenericResult = src{testType}.To<{srcTypeAlias}, {destTypeAlias}>();

            // Assert results: 
            //TODO: Build conversion table to detect:
            //      1) If direct ({destTypeAlias})src{testType} can be used.
            //      2) If not can src{testType}.To{destTypeAlias}(iFormatProvider) be used? (accounting for overflow)
            //      3) If not can System.Convert.ChangeType(src{testType}, typeof({destTypeAlias})) be used? (accounting for overflow)
            //      4) If not use unsafe

            {expectedCode}

            Assert.True(({destTypeAlias})src{testType}BoxedToObjectResult == expected{testType}BoxedToObjectResult);
            Assert.True(src{testType}BoxedToGenericResult == expected{testType}BoxedToGenericResult);
            Assert.True(({destTypeAlias})src{testType}GenericToBoxedResult == expected{testType}GenericToBoxedResult);
            Assert.True(src{testType}GenericToGenericResult == expected{testType}GenericToGenericResult);
        }}
";
                        tests.Add(testMethodCode);

                    }



                }

                var testMethods = string.Join("\r\n", tests);
                var conversionName = $"Convert{srcTypeName}";
                var className = $"{conversionName}Tests";
                var testMethodName = $"{conversionName}Test";
                var testCode = $@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using static System.Runtime.ConversionServices.Conversions;

namespace System.Runtime.ConversionServices.Tests 
{{
    public class {className}
    {{
{testBodyInitializer}

{testMethods}

    }}
}}

";
                var testFileName = $"{className}.cs";
                var testsDirectory = Path.GetFullPath(@"..\..\..\System.Runtime.ConversionServices.Tests");


                var testDestination = Path.Combine(testsDirectory, testFileName);
                File.WriteAllText(testDestination, testCode);


            }

        }



        static object ExecuteIl(byte[] msil, object target, Module module, Dictionary<int, object> callArgs = null)
        {
            var opCodeLookup = typeof(OpCodes).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(x => (OpCode)x.GetValue(null))
                .ToDictionary(x => (int)x.Value, x => x);

            Stack<object> stack = new Stack<object>();
            Dictionary<int, object> locals = new Dictionary<int, object>();
            if (callArgs is null) callArgs = new Dictionary<int, object>();
            var stream = new MemoryStream(msil);
            var br = new BinaryReader(stream);
            int pos = -1;
            int opcode = 0;

            int arg;
            Type typeArg;
            MethodInfo method;
            ConstructorInfo constructor;
            OpCode current = OpCodes.Nop;
            object[] methodArgs = null;
            object methodResult = null;
            ParameterInfo[] methodParameters = null;
            object methodTarget = null;

        Next:
            opcode = stream.ReadByte();
            if (opcode == 254)
            {
                opcode <<= 8;
                opcode += stream.ReadByte();
            }
            current = opCodeLookup[opcode];
            switch (opcode)
            {
                case 0: //nop
                    goto Read;
                case 6: //ld.loc.0
                case 7: //ld.loc.1
                case 8: //ld.loc.2
                case 9: //ld.loc.3
                    stack.Push(locals[opcode - 6]); goto Read;
                case 10: //stloc.0
                case 11: //stloc.1
                case 12: //stloc.2
                case 13: //stloc.3
                    locals[opcode - 10] = stack.Pop(); goto Read;
                case 17: //ldloc.s
                    stack.Push(locals[stream.ReadByte()]); goto Read;
                case 19: //stloc.s
                    locals[stream.ReadByte()] = stack.Pop(); goto Read;
                case 22: // ldc.i4.0
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                    stack.Push(opcode - 22); goto Read;
                case 37: //dup
                    stack.Push(stack.Peek()); goto Read;
                case 38: //pop
                    stack.Pop(); goto Read;
                case 42: //42
                    goto Ret;
                case 43: //43
                    arg = stream.ReadByte();
                    stream.Seek(arg, SeekOrigin.Current);
                    goto Next;
                case 40://call
                case 111: //callvirt
                    arg = br.ReadInt32();
                    method = (MethodInfo)module.ResolveMethod(arg);
                    methodParameters = method.GetParameters();
                    methodArgs = new object[methodParameters.Length];
                    for (arg = methodArgs.Length - 1; arg > -1; arg--) methodArgs[arg] = stack.Pop();//.To(methodParameters[arg].ParameterType);
                    methodTarget = method.IsStatic ? null : stack.Pop();
                    methodResult = method.Invoke(methodTarget, methodArgs);
                    if (method.ReturnType != typeof(void))
                        stack.Push(methodResult.To(method.ReturnType));
                    goto Read;
                case 114: //ldstr
                    arg = br.ReadInt32();
                    methodResult = module.ResolveString(arg);
                    stack.Push(methodResult);
                    goto Read;
                case 115: //new obj
                    arg = br.ReadInt32();
                    constructor = (ConstructorInfo)module.ResolveMethod(arg);
                    methodParameters = constructor.GetParameters();
                    methodArgs = new object[methodParameters.Length];
                    for (arg = methodArgs.Length - 1; arg > -1; arg--) methodArgs[arg] = stack.Pop();//.To(methodParameters[arg].ParameterType);
                    //methodTarget = target; // constructor.IsStatic ? null : stack.Pop();
                    methodResult = constructor.Invoke(methodArgs);
                    stack.Push(methodResult);
                    goto Read;
                case 116: //cast class
                    arg = br.ReadInt32();
                    typeArg = module.ResolveType(arg);
                    methodResult = stack.Pop();
                    stack.Push(methodResult.To(typeArg));
                    goto Read;
                case 140: /*box*/  //-> nothing need, value on stack is already object arg = br.ReadInt32(); ; var top = stack.Peek();
                    arg = br.ReadInt32();
                    typeArg = module.ResolveType(arg);
                    stack.Push((object)stack.Pop().To(typeArg));
                    goto Read;
                case 141: //new array
                    arg = br.ReadInt32();
                    typeArg = module.ResolveType(arg);
                    methodResult = Array.CreateInstance(typeArg, (int)stack.Pop());
                    stack.Push(methodResult);
                    goto Read;
                case 154: //ldelem.ref
                    arg = (int)stack.Pop();
                    stack.Push(((Array)stack.Pop()).GetValue(arg));
                    goto Read;
                case 162: //stelem.ref
                    methodResult = stack.Pop();
                    arg = (int)stack.Pop();
                    ((Array)stack.Pop()).SetValue(methodResult, arg);
                    goto Read;
                case 165: //unbox any
                    arg = br.ReadInt32();
                    typeArg = module.ResolveType(arg);
                    stack.Push((object)stack.Pop().To(typeArg));
                    goto Read;
                case 208: //ldtoken
                    arg = br.ReadInt32();
                    typeArg = module.ResolveType(arg);
                    stack.Push(typeArg.TypeHandle);
                    goto Read;
                default:
                    var notImplemented = current;
                    throw new NotImplementedException();
            }

        Read:
            pos++;
            if (pos < msil.Length)
                goto Next;
            Ret:
            var result = stack.Count == 0 ? null : stack.Pop();
            return result;
        }
        static int[] ILTest()
        {
            Stack<object> stack = new Stack<object>();
            stack.Push(5);
            stack.Push(typeof(int));



            stack.Push(Array.CreateInstance((Type)stack.Pop(), (int)stack.Pop()));
            stack.Push(stack.Peek());//dup

            stack.Push(1);
            stack.Push(1);
            stack.Push(typeof(Array).GetMethod(nameof(Array.SetValue), new[] { typeof(object), typeof(int) }));

            var methodInfo = (MethodInfo)stack.Pop();
            var methodArgs = new[] { stack.Pop(), stack.Pop(), stack.Pop() };
            methodInfo.Invoke(methodArgs[2], new[] { methodArgs[1], methodArgs[0] });
            System.Convert.ChangeType(methodArgs[1], typeof(byte));
            var result = stack.Pop();
            return (int[])result;
        }
        public static void RunConvertTests()
        {
            Stack<object> stack = new Stack<object>();

            stack.Push(null);
            //Test null reference handling.
            var result = stack.Pop().Convert().To<bool>();

            stack.Push(1);
            var convertdx = stack.Pop().To<char>();
            stack.Push(1);
            var convertdt = stack.Pop().To(typeof(double));
            //var eq = convertdx == convertdt;// <-- fails since convertdt will be boxed; 
            //Test fallback from Lamba Convert to System.Convert.ChangeType for IConvertible.
            //"No coercion operator is defined between types 'System.Int32' and 'System.Boolean'.
            stack.Push(1.Convert().To<bool>());// 

            // (object)true
            var boxedIntAsBool = stack.Pop();
            var staticConvert = Converter<object>.To(boxedIntAsBool, typeof(int));
            var t = staticConvert.GetType();


            stack.Push(ulong.MaxValue);
            var boxedUlong = stack.Pop();
            var boxedBoolConvertFromUlong = Converter<object>.To(boxedUlong, typeof(bool));
            var boxedBoolFromUlong = boxedUlong.To(typeof(bool));
            var boxedBoolFromUlongType = boxedBoolFromUlong.GetType();
            var UlongFromBoxedBool = boxedBoolFromUlong.Convert().To<ulong>();

            // Lamba convert bool => int succeeds
            stack.Push(true.Convert().To<int>());

            // (object)1;
            var boxedBoolAsInt = stack.Pop();


            //Test boxed int conversion to int,
            var boxedIntToInt = boxedBoolAsInt.Convert().To<int>();

            //Test boxed bool conversion to bool,
            var boxedBoolToBool = boxedIntAsBool.Convert().To<bool>();



            //test boxed int conversion to bool
            // this works, as the first call to the converter throw a coercion exception and fellback to system.convert.
            var unboxedBoolAsIntToBool = boxedBoolAsInt.Convert().To<int>().Convert().To<bool>();
            // this won't even call the conversion function. Throws an exception trying to call the converter
            var boxedIntToBool = boxedBoolAsInt.Convert().To<bool>();

            //test boxed bool conversion to int
            var boxedBoolToInt = boxedIntAsBool.Convert().To<int>();
            var boxedBoolToInt2 = boxedIntAsBool.Convert().To<int>();

            stack.Push(true);
            var boxedBool = stack.Pop();
            var unboxed = boxedBool.Convert().To<int>();
            var unboxedFromType = boxedBool.Convert().To(typeof(int));
            var unboxedToIConvertible = unboxed.ChangeType<bool>();
            var ub2 = unboxed.Convert().To<bool>();
            var ub3 = unboxed.Convert().To<bool>();
            var v0 = 1;
            var sb = v0.Convert().To<bool>();
            var v1 = v0.Convert().To<byte>();
            var v2 = Conversions.Convert<byte>(v1).To<double>();
            var v3 = v2.Convert().To<decimal>();
            var v4 = v3.To<bool>();
            var v5 = v4.Convert().To<decimal?>();
            var v6 = v5.Convert().To<int>();
            var b9 = v6.Convert().To<sbyte>();
            var b8 = b9.Convert().To<char>();
            var b6 = b9.Convert().To<ushort>();

        }
    }
}
