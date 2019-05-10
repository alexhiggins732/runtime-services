using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace System.Runtime.ConversionServices.Tests
{
    class Program
    {
        public static void Main(string[] args)
        {

            var testClass = new OperandFactoryTests();
            OperandFactoryTests.RunAllTests();

            1.ToTypedReference();
            //TODO: Obselete. Remove interface
            //var intRef = 1.ToObjectReference().Cast<char>();
            //TODO: Obselete. Remove interface
            //var intRef2 = 2.ToObjectReference();
            //var int3 = intRef2.ToTypedReference();
            //var int4 = int3.Cast<char>();

            //var intRef2 = 2.ToObjectReference();
            //var int5 = 1.ToObjectReference().Cast<char>();

            // var int6 = 1.Cast<char>();

            //string c = '-'.Cast<string>();

            var tests = new DependencyInjectionOverrideTests();

            tests.TestDiInjectionFromEnumerable();

        }
    }

   
}
