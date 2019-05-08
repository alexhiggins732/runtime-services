using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Runtime.ConversionServices.Conversions;
using static System.Runtime.ConversionServices.Tests.CompareExtensionTests;

namespace System.Runtime.ConversionServices.Tests
{
    class Program
    {
        public static void Main(string[] args)
        {

            var intRef = 1.ToObjectReference().Cast<char>();

            var intRef2 = 2.ToObjectReference();
            var int3 = intRef2.ToTypedReference();
            var int4 = int3.Cast<char>();

            var int5 = 1.ToObjectReference().Cast<char>();

            var int6 = 1.Cast<char>();

            string c = '-'.Cast<string>();

            var tests = new DependencyInjectionOverrideTests();

            tests.TestDiInjectionFromEnumerable();

        }
    }

   
}
