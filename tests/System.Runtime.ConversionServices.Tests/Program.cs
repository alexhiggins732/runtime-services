using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace System.Runtime.ConversionServices.Tests
{
    class Program
    {

        public static void CompileTimeApiExtensionsCleanup()
        {


            //TODO: ToTypedReference() is very verbose.
            var typed = 1.ToTypedReference();
            typed.Arithmetic(); //TODO: typed exposes Arithmetic, while <T> exposes .ArithmeticTest<T>
            typed.Compare(1);//TODO: verify usage
            //typed.Convert<ulong>(); //TODO: what is usage
            typed.DirectCast<ulong>(); //TODO: yet another conversion interface. unify under sublevel api.
            var gen = typed.Generic; //TODO: property applicable here? If okay, solidify other functions here.
            var genTyped = typed.GenericArgumentType;// todo should this be under .Generic
            var typedSet = typed.SetValue(2);//TODO: solidfy api. This blows up easily. Also SetValue doesn't indicate functoin.

            var typedUnbox = typed.Unbox(); //TODO: confusion on apis returning native vs interface.
            var genericUnboxed = 1.Unbox<int>();
            // var genericUnboxedWontCompiled = 1.Unbox<sbyte>();//TODO: cleanup <T> parameter if not being used to convert.
            var genericExtensionUnboxed = 1.Unbox(); // returns runtime interface even for compile time types.


            //TODO: make explicit interface def
            var boxed = typed.BoxedValue;

            //TODO: Spike: Should this be a top-level api?
            //TODO: Unify Cast, Convert and To fluent apis.
            //TODO: boxed objects should have converter api (cast,convert or to);

            //TODO: this results in a compile time error: Requires reciever of int.    
            //var converted = boxed.Convert<int>();

            //TODO: has this been solidified into To(). If not expose convert() through top level api.
            var convertedToInt = boxed.Convert().To<int>();
            var casted = 1.Cast<sbyte>();
            var someother = 1.Convert<int>();// convert generic extension does nothing.
            var andAnother = ulong.MaxValue.ChangeType<sbyte>(); //this blows up probably remove it or make it explicit sub level.
            var andAnother1 = ulong.MaxValue.Compare(1);//where is the generic for this?

            var poof = int.MaxValue.ArithmeticTest();//todo: this has been refactored to Arithemtic() why is it showing here.
            var foo = int.MinValue.Add(5);//TODO: is there value for direct extensions on numberics know at compile time?
            var bar = int.MinValue.Subtract(-1); //TODO: verify <T> vs IGenericArithmetic extension.
            var wow = int.MinValue.Unbox<int>();//TODO: clean up unbox. It returns IRuntimeConvert.
            var wow2 = typed.Unbox();

            boxed.ToTypedReference();
            var typedRef = typed.To<sbyte>();

            //TODO: fix requires recieved of int for Generic call.
            //boxed.ToTypedReference<int>();
            var boxedTo = boxed.To(typeof(sbyte));
            var typedTo = boxed.To<sbyte>();

        }

        public static void Main(string[] args)
        {

            var testClass = new OperandFactoryTests();
            OperandFactoryTests.RunAllTests();



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
