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
            var tests = new DependencyInjectionOverrideTests();

            tests.TestDiInjectionFromEnumerable();
         
        }
    }
}
