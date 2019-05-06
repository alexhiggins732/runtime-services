using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Runtime.ConversionServices.Tests.CompareExtensionTests;

namespace System.Runtime.ConversionServices.Tests
{
    class Program
    {
        public static void Main(string[] args)
        {
            var tests = new CompareExtensionTests();

            var boolTests = ParameterizedConversionTests.BoolTestData;
            var boolTestArray = boolTests.ToArray();
            var data = CompareExtensionTests.AllTests;
            foreach(var array in data)
            {
                tests.TestOrderedCompareLessThan((IArrayWrapper)array[0]);
            }
         
        }
    }
}
