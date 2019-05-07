using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace System.Runtime.ConversionServices.Tests
{
    public class DependencyInjectionOverrideTests
    {
        [Fact] 
        public void TestDiInjectedFromCharToStringFuncReturnsFoo()
        {
            Func<char, string> func = (value) => "bar";

            CompileTimeConverters.StaticConverters.RegisterConverter(func);

            char foo = 'f';
            var result = foo.To<string>();

            Assert.True(result == "bar");

        }

        [Fact]
        public void TestDiInjectionFromEnumerable()
        {
            Func<char, string> func1 = (value) => "foo";
            Func<char, char[]> func2 = (value) => "bar".ToCharArray();
            var converters = new Delegate [] { func1, func2 };



            CompileTimeConverters
                .StaticConverters
                .RegisterConverters(converters);

            char foo = 'f';
            var stringFoo = foo.To<string>();
            var stringBar = foo.To<char[]>().To<string>();

            Assert.True(stringFoo == "foo");
            Assert.True(stringBar == "bar");

        }

        [Fact]
        public void TestDiInjectionFromPublicStaticMethod()
        {

            CompileTimeConverters
                .StaticConverters
                .RegisterConverter((Func<char, string>)PublicStaticMethod);

            char foo = 'f';
            var result = foo.To<string>();
            Assert.True(result == nameof(PublicStaticMethod));
        }

        [Fact]
        public void TestDiInjectionFromPrivateStaticMethod()
        {

            CompileTimeConverters
                .StaticConverters
                .RegisterConverter((Func<char, string>)PrivateStaticMethod);

            char foo = 'f';
            var result = foo.To<string>();
            Assert.True(result == nameof(PrivateStaticMethod));
        }

        [Fact]
        public void TestDiInjectionFromPublicMethod()
        {

            CompileTimeConverters
                .StaticConverters
                .RegisterConverter((Func<char, string>)PublicMethod);

            char foo = 'f';
            var result = foo.To<string>();
            Assert.True(result == nameof(PublicMethod));
        }
        [Fact]
        public void TestDiInjectionFromPrivateMethod()
        {

            CompileTimeConverters
                .StaticConverters
                .RegisterConverter((Func<char, string>)PrivateMethod);

            char foo = 'f';
            var result = foo.To<string>();
            Assert.True(result == nameof(PrivateMethod));
        }


        public static string PublicStaticMethod(char c)
        {
            return nameof(PublicStaticMethod);
        }
        private static string PrivateStaticMethod(char c)
        {
            return nameof(PrivateStaticMethod);
        }
        public string PublicMethod(char c)
        {
            return nameof(PublicMethod);
        }
        private static string PrivateMethod(char c)
        {
            return nameof(PrivateMethod);
        }

    }
}
