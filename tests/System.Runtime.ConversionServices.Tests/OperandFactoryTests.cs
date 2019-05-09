using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using System.Runtime.ConversionServices.MsilOpcodes;
using static System.Runtime.ConversionServices.MsilOpcodes.MsilOpcodeInterfaces;
using static System.Runtime.ConversionServices.TypeConstants;
using System.Reflection;

namespace System.Runtime.ConversionServices.Tests
{
    public class OperandFactoryTests
    {
        [Fact]
        public static void RunAllTests()
        {
            var typesToTest = TypeConstantInfo.GetTypeConstants();
            Assert.True(typesToTest.Count == 14);

            foreach (var typeToTest in typesToTest)
            {

                var defaultValues = typeToTest.GetDefaultValues();
                if (typeToTest.Type != typeof(BigInt))
                {
                    Assert.True(defaultValues.Length == 3);
                }
                else
                {
                    Assert.True(defaultValues.Length == 1);
                }


                var typeUnaryOperators = GetUnaryOperatorsForType(typeToTest);
                foreach (var unaryOperator in typeUnaryOperators)
                {

                }
            }
        }

        public interface IMsilOpCodeInterface : IMsilOpCode
        {
            Type InterfaceType { get; }
            string Name { get; }
        }
        private struct MsilOpCodeInterface<T> : IMsilOpCodeInterface
        {
            public Type InterfaceType => typeof(T);
            public string Name => InterfaceType.Name;
            public MsilOpCodeInterface<TOut> To<TOut>() => new MsilOpCodeInterface<TOut>();
        }
        private struct MsilUnaryOpCodeInterface<TOpCode> : IMsilOpCodeInterface
        {
            public Type InterfaceType => typeof(TOpCode);
            public string Name => InterfaceType.Name;
            public Func<TIn, TOut> ResolveOperator<TIn, TOut>() 
                => OperandGenerator.CreateUnary<TIn, TOut, TOpCode>();
          
            public MsilOpCodeInterface<TOut> To<TOut>() => new MsilOpCodeInterface<TOut>();
        }

        // if we go generics: https://stackoverflow.com/questions/4963160/how-to-determine-if-a-type-implements-an-interface-with-c-sharp-reflection
        private static List<IMsilOpCodeInterface> GetUnaryOperatorsForType(TypeConstantInfo typeToTest)
        {
           
            var generic = typeof(MsilOpCodeInterface<>);
            var result = typeof(MsilOpcodeInterfaces)
                .GetNestedTypes()
                .Where(x => x.IsInterface && x.GetInterface(nameof(IOpCodeUnary)) != null)
                .Select(x => (IMsilOpCodeInterface)Activator.CreateInstance(generic.MakeGenericType(x)))
                .ToList();
            return result;
        }

        private static List<IMsilOpCodeInterface> GetBinaryOperatorsForType(TypeConstantInfo typeToTest)
        {
            var generic = typeof(MsilOpCodeInterface<>);
            var result = typeof(MsilOpcodeInterfaces)
                .GetNestedTypes()
                .Where(x => x.IsInterface && x.GetInterface(nameof(IOpCodeBinary)) != null)
               .Select(x => (IMsilOpCodeInterface)Activator.CreateInstance(generic.MakeGenericType(x)))
                .ToList();
            return result;
        }

        public static bool MyInterfaceFilter(Type typeObj, Object criteriaObj)
        {
            if (typeObj.ToString() == criteriaObj.ToString())
                return true;
            else
                return false;
        }
    }
}
