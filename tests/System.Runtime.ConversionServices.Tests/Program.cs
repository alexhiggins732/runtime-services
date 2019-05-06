using System;
using System.Collections.Generic;
using System.Text;

namespace System.Runtime.ConversionServices.Tests
{
    class Program
    {
        public static void Main(string[] args)
        {
            var tests = new ConversionExtensionsTests();
            tests.TestConvertArray();
        }
    }
}
