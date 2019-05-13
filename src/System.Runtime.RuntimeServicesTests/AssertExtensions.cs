using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace System.Runtime.RuntimeServices.Tests
{

    public class TheoryAttribute1 : TestMethodAttribute
    {

    }
    /// <summary>
    ///     Provides a data source for a data theory, with the data coming from one of the
    ///     following sources: 1. A static property 2. A static field 3. A static method
    ///     (with parameters) The member must return something compatible with IEnumerable<object[]>
    ///     with the test data. Caution: the property is completely enumerated by .ToList()
    ///    before any test is run. Hence it should return independent object sets.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class MemberDataAttribute1 : Attribute
    {
        //
        // Summary:
        //     Initializes a new instance of the Xunit.MemberDataAttribute class.
        //
        // Parameters:
        //   memberName:
        //     The name of the public static member on the test class that will provide the
        //     test data
        //
        //   parameters:
        //     The parameters for the member (only supported for methods; ignored for everything
        //     else)
        string attMemberName;
        object[] attParameters;
        public MemberDataAttribute(string memberName, params object[] parameters)
        {
            attMemberName = memberName;
            attParameters = parameters;
        }

        //
        public object[] ConvertDataItem(MethodInfo testMethod, object item)
        {
            return new object[] { };
        }
        public object[] DataItems()
        {
            return new object[] { };
        }
        public static object[] GetData()
        {
            var frame = new StackFrame(1);
            var method = frame.GetMethod();
            var att = (MemberDataAttribute)method.GetCustomAttributes().First(x => x is MemberDataAttribute);
            return att.DataItems();
        }

        internal static void Run(Func<object> test)
        {
            foreach (var dataItem in GetData())
            {
                test();
            }

            //p2(p1);
        }

        internal static void Run2(Func<object, object> act)
        {

            foreach (var dataItem in GetData())
            {
                act(dataItem);
            }
        }

        internal static void Run3<T>(T Instance, Func<T, Action> action)
        {
            GetData().All(x => { action((T)x)(); return true; });
        }
        internal static void Run5<T>(Func<T, Action> action)
        {
            GetData().All(x => { action((T)x); return true; });
        }

        internal static void Run6(Func<object, Action> action)
        {
            // get T parameter from the calling method?
            GetData().All(x => { action(x); return true; });
        }
        internal static void Run4<T>(Func<T, Action<T>> action)
        {
            GetData().All(x => { action((T)x); return true; });
        }
    }


    ////
    //// Summary:
    ////     Provides a base class for attributes that will provide member data. The member
    ////     data must return something compatible with System.Collections.IEnumerable. Caution:
    ////     the property is completely enumerated by .ToList() before any test is run. Hence
    ////     it should return independent object sets.
    //[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]

    //public abstract class MemberDataAttributeBase : DataAttribute
    //{
    //    //
    //    // Summary:
    //    //     Initializes a new instance of the Xunit.MemberDataAttributeBase class.
    //    //
    //    // Parameters:
    //    //   memberName:
    //    //     The name of the public static member on the test class that will provide the
    //    //     test data
    //    //
    //    //   parameters:
    //    //     The parameters for the member (only supported for methods; ignored for everything
    //    //     else)
    //    protected MemberDataAttributeBase(string memberName, object[] parameters);

    //    //
    //    // Summary:
    //    //     Returns true if the data attribute wants to skip enumerating data during discovery.
    //    //     This will cause the theory to yield a single test case for all data, and the
    //    //     data discovery will be during test execution instead of discovery.
    //    public bool DisableDiscoveryEnumeration { get; set; }
    //    //
    //    // Summary:
    //    //     Gets the member name.
    //    public string MemberName { get; }
    //    //
    //    // Summary:
    //    //     Gets or sets the type to retrieve the member from. If not set, then the property
    //    //     will be retrieved from the unit test class.
    //    public Type MemberType { get; set; }
    //    //
    //    // Summary:
    //    //     Gets or sets the parameters passed to the member. Only supported for static methods.
    //    public object[] Parameters { get; }

    //    //
    //    public override IEnumerable<object[]> GetData(MethodInfo testMethod);
    //    //
    //    // Summary:
    //    //     Converts an item yielded by the data member to an object array, for return from
    //    //     Xunit.MemberDataAttributeBase.GetData(System.Reflection.MethodInfo).
    //    //
    //    // Parameters:
    //    //   testMethod:
    //    //     The method that is being tested.
    //    //
    //    //   item:
    //    //     An item yielded from the data member.
    //    //
    //    // Returns:
    //    //     An object[] suitable for return from Xunit.MemberDataAttributeBase.GetData(System.Reflection.MethodInfo).
    //    protected abstract object[] ConvertDataItem(MethodInfo testMethod, object item);
    //}


    ////
    //// Summary:
    ////     Abstract attribute which represents a data source for a data theory. Data source
    ////     providers derive from this attribute and implement GetData to return the data
    ////     for the theory. Caution: the property is completely enumerated by .ToList() before
    ////     any test is run. Hence it should return independent object sets.
    //[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    ////[DataDiscoverer("Xunit.Sdk.DataDiscoverer", "xunit.core")]
    //public abstract class DataAttribute : Attribute
    //{
    //    protected DataAttribute();

    //    //
    //    // Summary:
    //    //     Marks all test cases generated by this data source as skipped.
    //    public virtual string Skip { get; set; }

    //    //
    //    // Summary:
    //    //     Returns the data to be used to test the theory.
    //    //
    //    // Parameters:
    //    //   testMethod:
    //    //     The method that is being tested
    //    //
    //    // Returns:
    //    //     One or more sets of theory data. Each invocation of the test method is represented
    //    //     by a single object array.
    //    public abstract IEnumerable<object[]> GetData(MethodInfo testMethod);
    //}


    public static class AssertExtensions
    {

        //
        // Summary:
        //     Tests whether the specified values are equal and throws an exception if the two
        //     values are not equal. Different numeric types are treated as unequal even if
        //     the logical values are equal. 42L is not equal to 42.
        //
        // Parameters:
        //   expected:
        //     The first value to compare. This is the value the tests expects.
        //
        //   actual:
        //     The second value to compare. This is the value produced by the code under test.
        //
        //   message:
        //     The message to include in the exception when actual is not equal to expected.
        //     The message is shown in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Type parameters:
        //   T:
        //     The type of values to compare.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if expected is not equal to actual.
        public static void Equal<T>(this Assert assert, T expected, T actual, string message, params object[] parameters)
            => Assert.AreEqual(expected, actual, message, parameters);

        //
        // Summary:
        //     Tests whether the specified objects are equal and throws an exception if the
        //     two objects are not equal. Different numeric types are treated as unequal even
        //     if the logical values are equal. 42L is not equal to 42.
        //
        // Parameters:
        //   expected:
        //     The first object to compare. This is the object the tests expects.
        //
        //   actual:
        //     The second object to compare. This is the object produced by the code under test.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if expected is not equal to actual.
        public static void Equal(this Assert assert, object expected, object actual)
            => Assert.AreEqual(expected, actual);

        //
        // Summary:
        //     Tests whether the specified objects are equal and throws an exception if the
        //     two objects are not equal. Different numeric types are treated as unequal even
        //     if the logical values are equal. 42L is not equal to 42.
        //
        // Parameters:
        //   expected:
        //     The first object to compare. This is the object the tests expects.
        //
        //   actual:
        //     The second object to compare. This is the object produced by the code under test.
        //
        //   message:
        //     The message to include in the exception when actual is not equal to expected.
        //     The message is shown in test results.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if expected is not equal to actual.
        public static void Equal(this Assert assert, object expected, object actual, string message)
                        => Assert.AreEqual(expected, actual, message);

        //
        // Summary:
        //     Tests whether the specified objects are equal and throws an exception if the
        //     two objects are not equal. Different numeric types are treated as unequal even
        //     if the logical values are equal. 42L is not equal to 42.
        //
        // Parameters:
        //   expected:
        //     The first object to compare. This is the object the tests expects.
        //
        //   actual:
        //     The second object to compare. This is the object produced by the code under test.
        //
        //   message:
        //     The message to include in the exception when actual is not equal to expected.
        //     The message is shown in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if expected is not equal to actual.
        public static void Equal(this Assert assert, object expected, object actual, string message, params object[] parameters)
                        => Assert.AreEqual(expected, actual, message, parameters);

        //
        // Summary:
        //     Tests whether the specified floats are equal and throws an exception if they
        //     are not equal.
        //
        // Parameters:
        //   expected:
        //     The first float to compare. This is the float the tests expects.
        //
        //   actual:
        //     The second float to compare. This is the float produced by the code under test.
        //
        //   delta:
        //     The required accuracy. An exception will be thrown only if actual is different
        //     than expected by more than delta.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if expected is not equal to actual.
        public static void Equal(this Assert assert, float expected, float actual, float delta)
                        => Assert.AreEqual(expected, actual, delta);

        //
        // Summary:
        //     Tests whether the specified floats are equal and throws an exception if they
        //     are not equal.
        //
        // Parameters:
        //   expected:
        //     The first float to compare. This is the float the tests expects.
        //
        //   actual:
        //     The second float to compare. This is the float produced by the code under test.
        //
        //   delta:
        //     The required accuracy. An exception will be thrown only if actual is different
        //     than expected by more than delta.
        //
        //   message:
        //     The message to include in the exception when actual is different than expected
        //     by more than delta. The message is shown in test results.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if expected is not equal to actual.
        public static void Equal(this Assert assert, float expected, float actual, float delta, string message)
                        => Assert.AreEqual(expected, actual, delta, message);

        //
        // Summary:
        //     Tests whether the specified floats are equal and throws an exception if they
        //     are not equal.
        //
        // Parameters:
        //   expected:
        //     The first float to compare. This is the float the tests expects.
        //
        //   actual:
        //     The second float to compare. This is the float produced by the code under test.
        //
        //   delta:
        //     The required accuracy. An exception will be thrown only if actual is different
        //     than expected by more than delta.
        //
        //   message:
        //     The message to include in the exception when actual is different than expected
        //     by more than delta. The message is shown in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if expected is not equal to actual.
        public static void Equal(this Assert assert, float expected, float actual, float delta, string message, params object[] parameters)
                        => Assert.AreEqual(expected, actual, delta, message, parameters);

        //
        // Summary:
        //     Tests whether the specified doubles are equal and throws an exception if they
        //     are not equal.
        //
        // Parameters:
        //   expected:
        //     The first double to compare. This is the double the tests expects.
        //
        //   actual:
        //     The second double to compare. This is the double produced by the code under test.
        //
        //   delta:
        //     The required accuracy. An exception will be thrown only if actual is different
        //     than expected by more than delta.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if expected is not equal to actual.
        public static void Equal(this Assert assert, double expected, double actual, double delta)
                        => Assert.AreEqual(expected, actual, delta);

        //
        // Summary:
        //     Tests whether the specified doubles are equal and throws an exception if they
        //     are not equal.
        //
        // Parameters:
        //   expected:
        //     The first double to compare. This is the double the tests expects.
        //
        //   actual:
        //     The second double to compare. This is the double produced by the code under test.
        //
        //   delta:
        //     The required accuracy. An exception will be thrown only if actual is different
        //     than expected by more than delta.
        //
        //   message:
        //     The message to include in the exception when actual is different than expected
        //     by more than delta. The message is shown in test results.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if expected is not equal to actual.
        public static void Equal(this Assert assert, double expected, double actual, double delta, string message)
                        => Assert.AreEqual(expected, actual, delta, message);

        //
        // Summary:
        //     Tests whether the specified strings are equal and throws an exception if they
        //     are not equal. The invariant culture is used for the comparison.
        //
        // Parameters:
        //   expected:
        //     The first string to compare. This is the string the tests expects.
        //
        //   actual:
        //     The second string to compare. This is the string produced by the code under test.
        //
        //   ignoreCase:
        //     A Boolean indicating a case-sensitive or insensitive comparison. (this Assert assert,true indicates
        //     a case-insensitive comparison.)
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if expected is not equal to actual.
        public static void Equal(this Assert assert, string expected, string actual, bool ignoreCase)
                        => Assert.AreEqual(expected, actual, ignoreCase);

        //
        // Summary:
        //     Tests whether the specified strings are equal and throws an exception if they
        //     are not equal. The invariant culture is used for the comparison.
        //
        // Parameters:
        //   expected:
        //     The first string to compare. This is the string the tests expects.
        //
        //   actual:
        //     The second string to compare. This is the string produced by the code under test.
        //
        //   ignoreCase:
        //     A Boolean indicating a case-sensitive or insensitive comparison. (this Assert assert,true indicates
        //     a case-insensitive comparison.)
        //
        //   message:
        //     The message to include in the exception when actual is not equal to expected.
        //     The message is shown in test results.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if expected is not equal to actual.
        public static void Equal(this Assert assert, string expected, string actual, bool ignoreCase, string message)
                        => Assert.AreEqual(expected, actual, ignoreCase, message);

        //
        // Summary:
        //     Tests whether the specified strings are equal and throws an exception if they
        //     are not equal. The invariant culture is used for the comparison.
        //
        // Parameters:
        //   expected:
        //     The first string to compare. This is the string the tests expects.
        //
        //   actual:
        //     The second string to compare. This is the string produced by the code under test.
        //
        //   ignoreCase:
        //     A Boolean indicating a case-sensitive or insensitive comparison. (this Assert assert,true indicates
        //     a case-insensitive comparison.)
        //
        //   message:
        //     The message to include in the exception when actual is not equal to expected.
        //     The message is shown in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if expected is not equal to actual.
        public static void Equal(this Assert assert, string expected, string actual, bool ignoreCase, string message, params object[] parameters)
                        => Assert.AreEqual(expected, actual, ignoreCase, message, parameters);
        //
        // Summary:
        //     Tests whether the specified strings are equal and throws an exception if they
        //     are not equal.
        //
        // Parameters:
        //   expected:
        //     The first string to compare. This is the string the tests expects.
        //
        //   actual:
        //     The second string to compare. This is the string produced by the code under test.
        //
        //   ignoreCase:
        //     A Boolean indicating a case-sensitive or insensitive comparison. (this Assert assert,true indicates
        //     a case-insensitive comparison.)
        //
        //   culture:
        //     A CultureInfo object that supplies culture-specific comparison information.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if expected is not equal to actual.
        public static void Equal(this Assert assert, string expected, string actual, bool ignoreCase, CultureInfo culture)
                        => Assert.AreEqual(expected, actual, ignoreCase, culture);

        //
        // Summary:
        //     Tests whether the specified strings are equal and throws an exception if they
        //     are not equal.
        //
        // Parameters:
        //   expected:
        //     The first string to compare. This is the string the tests expects.
        //
        //   actual:
        //     The second string to compare. This is the string produced by the code under test.
        //
        //   ignoreCase:
        //     A Boolean indicating a case-sensitive or insensitive comparison. (this Assert assert,true indicates
        //     a case-insensitive comparison.)
        //
        //   culture:
        //     A CultureInfo object that supplies culture-specific comparison information.
        //
        //   message:
        //     The message to include in the exception when actual is not equal to expected.
        //     The message is shown in test results.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if expected is not equal to actual.
        public static void Equal(this Assert assert, string expected, string actual, bool ignoreCase, CultureInfo culture, string message)
                        => Assert.AreEqual(expected, actual, ignoreCase, culture, message);

        //
        // Summary:
        //     Tests whether the specified strings are equal and throws an exception if they
        //     are not equal.
        //
        // Parameters:
        //   expected:
        //     The first string to compare. This is the string the tests expects.
        //
        //   actual:
        //     The second string to compare. This is the string produced by the code under test.
        //
        //   ignoreCase:
        //     A Boolean indicating a case-sensitive or insensitive comparison. (this Assert assert,true indicates
        //     a case-insensitive comparison.)
        //
        //   culture:
        //     A CultureInfo object that supplies culture-specific comparison information.
        //
        //   message:
        //     The message to include in the exception when actual is not equal to expected.
        //     The message is shown in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if expected is not equal to actual.
        public static void Equal(this Assert assert, string expected, string actual, bool ignoreCase, CultureInfo culture, string message, params object[] parameters)
                        => Assert.AreEqual(expected, actual, ignoreCase, culture, message, parameters);

        //
        // Summary:
        //     Tests whether the specified values are equal and throws an exception if the two
        //     values are not equal. Different numeric types are treated as unequal even if
        //     the logical values are equal. 42L is not equal to 42.
        //
        // Parameters:
        //   expected:
        //     The first value to compare. This is the value the tests expects.
        //
        //   actual:
        //     The second value to compare. This is the value produced by the code under test.
        //
        //   message:
        //     The message to include in the exception when actual is not equal to expected.
        //     The message is shown in test results.
        //
        // Type parameters:
        //   T:
        //     The type of values to compare.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if expected is not equal to actual.
        public static void Equal<T>(this Assert assert, T expected, T actual, string message)
                        => Assert.AreEqual(expected, actual, message);

        //
        // Summary:
        //     Tests whether the specified values are equal and throws an exception if the two
        //     values are not equal. Different numeric types are treated as unequal even if
        //     the logical values are equal. 42L is not equal to 42.
        //
        // Parameters:
        //   expected:
        //     The first value to compare. This is the value the tests expects.
        //
        //   actual:
        //     The second value to compare. This is the value produced by the code under test.
        //
        // Type parameters:
        //   T:
        //     The type of values to compare.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if expected is not equal to actual.
        public static void Equal<T>(this Assert assert, T expected, T actual)
                        => Assert.AreEqual(expected, actual);

        //
        // Summary:
        //     Tests whether the specified doubles are equal and throws an exception if they
        //     are not equal.
        //
        // Parameters:
        //   expected:
        //     The first double to compare. This is the double the tests expects.
        //
        //   actual:
        //     The second double to compare. This is the double produced by the code under test.
        //
        //   delta:
        //     The required accuracy. An exception will be thrown only if actual is different
        //     than expected by more than delta.
        //
        //   message:
        //     The message to include in the exception when actual is different than expected
        //     by more than delta. The message is shown in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if expected is not equal to actual.
        public static void Equal(this Assert assert, double expected, double actual, double delta, string message, params object[] parameters)
                        => Assert.AreEqual(expected, actual, delta, message, parameters);

        //
        // Summary:
        //     Tests whether the specified strings are unequal and throws an exception if they
        //     are equal. The invariant culture is used for the comparison.
        //
        // Parameters:
        //   notExpected:
        //     The first string to compare. This is the string the test expects not to match
        //     actual.
        //
        //   actual:
        //     The second string to compare. This is the string produced by the code under test.
        //
        //   ignoreCase:
        //     A Boolean indicating a case-sensitive or insensitive comparison. (this Assert assert,true indicates
        //     a case-insensitive comparison.)
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if notExpected is equal to actual.
        public static void NotEqual(this Assert assert, string notExpected, string actual, bool ignoreCase)
                        => Assert.AreNotEqual(notExpected, actual, ignoreCase);

        //
        // Summary:
        //     Tests whether the specified floats are unequal and throws an exception if they
        //     are equal.
        //
        // Parameters:
        //   notExpected:
        //     The first float to compare. This is the float the test expects not to match actual.
        //
        //   actual:
        //     The second float to compare. This is the float produced by the code under test.
        //
        //   delta:
        //     The required accuracy. An exception will be thrown only if actual is different
        //     than notExpected by at most delta.
        //
        //   message:
        //     The message to include in the exception when actual is equal to notExpected or
        //     different by less than delta. The message is shown in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if notExpected is equal to actual.
        public static void NotEqual(this Assert assert, float notExpected, float actual, float delta, string message, params object[] parameters)
                        => Assert.AreNotEqual(notExpected, actual, delta, message, parameters);

        //
        // Summary:
        //     Tests whether the specified floats are unequal and throws an exception if they
        //     are equal.
        //
        // Parameters:
        //   notExpected:
        //     The first float to compare. This is the float the test expects not to match actual.
        //
        //   actual:
        //     The second float to compare. This is the float produced by the code under test.
        //
        //   delta:
        //     The required accuracy. An exception will be thrown only if actual is different
        //     than notExpected by at most delta.
        //
        //   message:
        //     The message to include in the exception when actual is equal to notExpected or
        //     different by less than delta. The message is shown in test results.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if notExpected is equal to actual.
        public static void NotEqual(this Assert assert, float notExpected, float actual, float delta, string message)
                        => Assert.AreNotEqual(notExpected, actual, delta, message);

        //
        // Summary:
        //     Tests whether the specified floats are unequal and throws an exception if they
        //     are equal.
        //
        // Parameters:
        //   notExpected:
        //     The first float to compare. This is the float the test expects not to match actual.
        //
        //   actual:
        //     The second float to compare. This is the float produced by the code under test.
        //
        //   delta:
        //     The required accuracy. An exception will be thrown only if actual is different
        //     than notExpected by at most delta.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if notExpected is equal to actual.
        public static void NotEqual(this Assert assert, float notExpected, float actual, float delta)
                        => Assert.AreNotEqual(notExpected, actual, delta);

        //
        // Summary:
        //     Tests whether the specified doubles are unequal and throws an exception if they
        //     are equal.
        //
        // Parameters:
        //   notExpected:
        //     The first double to compare. This is the double the test expects not to match
        //     actual.
        //
        //   actual:
        //     The second double to compare. This is the double produced by the code under test.
        //
        //   delta:
        //     The required accuracy. An exception will be thrown only if actual is different
        //     than notExpected by at most delta.
        //
        //   message:
        //     The message to include in the exception when actual is equal to notExpected or
        //     different by less than delta. The message is shown in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if notExpected is equal to actual.
        public static void NotEqual(this Assert assert, double notExpected, double actual, double delta, string message, params object[] parameters)
                        => Assert.AreNotEqual(notExpected, actual, delta, message, parameters);

        //
        // Summary:
        //     Tests whether the specified strings are unequal and throws an exception if they
        //     are equal. The invariant culture is used for the comparison.
        //
        // Parameters:
        //   notExpected:
        //     The first string to compare. This is the string the test expects not to match
        //     actual.
        //
        //   actual:
        //     The second string to compare. This is the string produced by the code under test.
        //
        //   ignoreCase:
        //     A Boolean indicating a case-sensitive or insensitive comparison. (this Assert assert,true indicates
        //     a case-insensitive comparison.)
        //
        //   message:
        //     The message to include in the exception when actual is equal to notExpected.
        //     The message is shown in test results.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if notExpected is equal to actual.
        public static void NotEqual(this Assert assert, string notExpected, string actual, bool ignoreCase, string message)
                        => Assert.AreNotEqual(notExpected, actual, ignoreCase, message);

        //
        // Summary:
        //     Tests whether the specified objects are unequal and throws an exception if the
        //     two objects are equal. Different numeric types are treated as unequal even if
        //     the logical values are equal. 42L is not equal to 42.
        //
        // Parameters:
        //   notExpected:
        //     The first object to compare. This is the value the test expects not to match
        //     actual.
        //
        //   actual:
        //     The second object to compare. This is the object produced by the code under test.
        //
        //   message:
        //     The message to include in the exception when actual is equal to notExpected.
        //     The message is shown in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if notExpected is equal to actual.
        public static void NotEqual(this Assert assert, object notExpected, object actual, string message, params object[] parameters)
                        => Assert.AreNotEqual(notExpected, actual, message, parameters);

        //
        // Summary:
        //     Tests whether the specified doubles are unequal and throws an exception if they
        //     are equal.
        //
        // Parameters:
        //   notExpected:
        //     The first double to compare. This is the double the test expects not to match
        //     actual.
        //
        //   actual:
        //     The second double to compare. This is the double produced by the code under test.
        //
        //   delta:
        //     The required accuracy. An exception will be thrown only if actual is different
        //     than notExpected by at most delta.
        //
        //   message:
        //     The message to include in the exception when actual is equal to notExpected or
        //     different by less than delta. The message is shown in test results.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if notExpected is equal to actual.
        public static void NotEqual(this Assert assert, double notExpected, double actual, double delta, string message)
                        => Assert.AreNotEqual(notExpected, actual, delta, message);

        //
        // Summary:
        //     Tests whether the specified objects are unequal and throws an exception if the
        //     two objects are equal. Different numeric types are treated as unequal even if
        //     the logical values are equal. 42L is not equal to 42.
        //
        // Parameters:
        //   notExpected:
        //     The first object to compare. This is the value the test expects not to match
        //     actual.
        //
        //   actual:
        //     The second object to compare. This is the object produced by the code under test.
        //
        //   message:
        //     The message to include in the exception when actual is equal to notExpected.
        //     The message is shown in test results.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if notExpected is equal to actual.
        public static void NotEqual(this Assert assert, object notExpected, object actual, string message)
                        => Assert.AreNotEqual(notExpected, actual, message);

        //
        // Summary:
        //     Tests whether the specified strings are unequal and throws an exception if they
        //     are equal. The invariant culture is used for the comparison.
        //
        // Parameters:
        //   notExpected:
        //     The first string to compare. This is the string the test expects not to match
        //     actual.
        //
        //   actual:
        //     The second string to compare. This is the string produced by the code under test.
        //
        //   ignoreCase:
        //     A Boolean indicating a case-sensitive or insensitive comparison. (this Assert assert,true indicates
        //     a case-insensitive comparison.)
        //
        //   message:
        //     The message to include in the exception when actual is equal to notExpected.
        //     The message is shown in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if notExpected is equal to actual.
        public static void NotEqual(this Assert assert, string notExpected, string actual, bool ignoreCase, string message, params object[] parameters)
                        => Assert.AreNotEqual(notExpected, actual, ignoreCase, message, parameters);

        //
        // Summary:
        //     Tests whether the specified strings are unequal and throws an exception if they
        //     are equal.
        //
        // Parameters:
        //   notExpected:
        //     The first string to compare. This is the string the test expects not to match
        //     actual.
        //
        //   actual:
        //     The second string to compare. This is the string produced by the code under test.
        //
        //   ignoreCase:
        //     A Boolean indicating a case-sensitive or insensitive comparison. (this Assert assert,true indicates
        //     a case-insensitive comparison.)
        //
        //   culture:
        //     A CultureInfo object that supplies culture-specific comparison information.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if notExpected is equal to actual.
        public static void NotEqual(this Assert assert, string notExpected, string actual, bool ignoreCase, CultureInfo culture)
                        => Assert.AreNotEqual(notExpected, actual, ignoreCase, culture);

        //
        // Summary:
        //     Tests whether the specified strings are unequal and throws an exception if they
        //     are equal.
        //
        // Parameters:
        //   notExpected:
        //     The first string to compare. This is the string the test expects not to match
        //     actual.
        //
        //   actual:
        //     The second string to compare. This is the string produced by the code under test.
        //
        //   ignoreCase:
        //     A Boolean indicating a case-sensitive or insensitive comparison. (this Assert assert,true indicates
        //     a case-insensitive comparison.)
        //
        //   culture:
        //     A CultureInfo object that supplies culture-specific comparison information.
        //
        //   message:
        //     The message to include in the exception when actual is equal to notExpected.
        //     The message is shown in test results.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if notExpected is equal to actual.
        public static void NotEqual(this Assert assert, string notExpected, string actual, bool ignoreCase, CultureInfo culture, string message)
                        => Assert.AreNotEqual(notExpected, actual, ignoreCase, culture, message);

        //
        // Summary:
        //     Tests whether the specified values are unequal and throws an exception if the
        //     two values are equal. Different numeric types are treated as unequal even if
        //     the logical values are equal. 42L is not equal to 42.
        //
        // Parameters:
        //   notExpected:
        //     The first value to compare. This is the value the test expects not to match actual.
        //
        //   actual:
        //     The second value to compare. This is the value produced by the code under test.
        //
        //   message:
        //     The message to include in the exception when actual is equal to notExpected.
        //     The message is shown in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Type parameters:
        //   T:
        //     The type of values to compare.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if notExpected is equal to actual.
        public static void NotEqual<T>(this Assert assert, T notExpected, T actual, string message, params object[] parameters)
                        => Assert.AreNotEqual(notExpected, actual, message, parameters);

        //
        // Summary:
        //     Tests whether the specified values are unequal and throws an exception if the
        //     two values are equal. Different numeric types are treated as unequal even if
        //     the logical values are equal. 42L is not equal to 42.
        //
        // Parameters:
        //   notExpected:
        //     The first value to compare. This is the value the test expects not to match actual.
        //
        //   actual:
        //     The second value to compare. This is the value produced by the code under test.
        //
        //   message:
        //     The message to include in the exception when actual is equal to notExpected.
        //     The message is shown in test results.
        //
        // Type parameters:
        //   T:
        //     The type of values to compare.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if notExpected is equal to actual.
        public static void NotEqual<T>(this Assert assert, T notExpected, T actual, string message)
                        => Assert.AreNotEqual(notExpected, actual, message);

        //
        // Summary:
        //     Tests whether the specified values are unequal and throws an exception if the
        //     two values are equal. Different numeric types are treated as unequal even if
        //     the logical values are equal. 42L is not equal to 42.
        //
        // Parameters:
        //   notExpected:
        //     The first value to compare. This is the value the test expects not to match actual.
        //
        //   actual:
        //     The second value to compare. This is the value produced by the code under test.
        //
        // Type parameters:
        //   T:
        //     The type of values to compare.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if notExpected is equal to actual.
        public static void NotEqual<T>(this Assert assert, T notExpected, T actual)
                        => Assert.AreNotEqual(notExpected, actual);

        //
        // Summary:
        //     Tests whether the specified strings are unequal and throws an exception if they
        //     are equal.
        //
        // Parameters:
        //   notExpected:
        //     The first string to compare. This is the string the test expects not to match
        //     actual.
        //
        //   actual:
        //     The second string to compare. This is the string produced by the code under test.
        //
        //   ignoreCase:
        //     A Boolean indicating a case-sensitive or insensitive comparison. (this Assert assert,true indicates
        //     a case-insensitive comparison.)
        //
        //   culture:
        //     A CultureInfo object that supplies culture-specific comparison information.
        //
        //   message:
        //     The message to include in the exception when actual is equal to notExpected.
        //     The message is shown in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if notExpected is equal to actual.
        public static void NotEqual(this Assert assert, string notExpected, string actual, bool ignoreCase, CultureInfo culture, string message, params object[] parameters)
                        => Assert.AreNotEqual(notExpected, actual, ignoreCase, message, parameters);

        //
        // Summary:
        //     Tests whether the specified objects are unequal and throws an exception if the
        //     two objects are equal. Different numeric types are treated as unequal even if
        //     the logical values are equal. 42L is not equal to 42.
        //
        // Parameters:
        //   notExpected:
        //     The first object to compare. This is the value the test expects not to match
        //     actual.
        //
        //   actual:
        //     The second object to compare. This is the object produced by the code under test.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if notExpected is equal to actual.
        public static void NotEqual(this Assert assert, object notExpected, object actual)
                        => Assert.AreNotEqual(notExpected, actual);

        //
        // Summary:
        //     Tests whether the specified doubles are unequal and throws an exception if they
        //     are equal.
        //
        // Parameters:
        //   notExpected:
        //     The first double to compare. This is the double the test expects not to match
        //     actual.
        //
        //   actual:
        //     The second double to compare. This is the double produced by the code under test.
        //
        //   delta:
        //     The required accuracy. An exception will be thrown only if actual is different
        //     than notExpected by at most delta.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if notExpected is equal to actual.
        public static void NotEqual(this Assert assert, double notExpected, double actual, double delta)
                        => Assert.AreNotEqual(notExpected, actual, delta);

        //
        // Summary:
        //     Tests whether the specified objects refer to different objects and throws an
        //     exception if the two inputs refer to the same object.
        //
        // Parameters:
        //   notExpected:
        //     The first object to compare. This is the value the test expects not to match
        //     actual.
        //
        //   actual:
        //     The second object to compare. This is the value produced by the code under test.
        //
        //   message:
        //     The message to include in the exception when actual is the same as notExpected.
        //     The message is shown in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if notExpected refers to the same object as actual.
        public static void AreNotSame(this Assert assert, object notExpected, object actual, string message, params object[] parameters)
                        => Assert.AreNotSame(notExpected, actual, message, parameters);

        //
        // Summary:
        //     Tests whether the specified objects refer to different objects and throws an
        //     exception if the two inputs refer to the same object.
        //
        // Parameters:
        //   notExpected:
        //     The first object to compare. This is the value the test expects not to match
        //     actual.
        //
        //   actual:
        //     The second object to compare. This is the value produced by the code under test.
        //
        //   message:
        //     The message to include in the exception when actual is the same as notExpected.
        //     The message is shown in test results.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if notExpected refers to the same object as actual.
        public static void AreNotSame(this Assert assert, object notExpected, object actual, string message)
                        => Assert.AreNotSame(notExpected, actual, message);

        //
        // Summary:
        //     Tests whether the specified objects refer to different objects and throws an
        //     exception if the two inputs refer to the same object.
        //
        // Parameters:
        //   notExpected:
        //     The first object to compare. This is the value the test expects not to match
        //     actual.
        //
        //   actual:
        //     The second object to compare. This is the value produced by the code under test.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if notExpected refers to the same object as actual.
        public static void AreNotSame(this Assert assert, object notExpected, object actual)
                        => Assert.AreNotSame(notExpected, actual);

        //
        // Summary:
        //     Tests whether the specified objects both refer to the same object and throws
        //     an exception if the two inputs do not refer to the same object.
        //
        // Parameters:
        //   expected:
        //     The first object to compare. This is the value the test expects.
        //
        //   actual:
        //     The second object to compare. This is the value produced by the code under test.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if expected does not refer to the same object as actual.
        public static void AreSame(this Assert assert, object expected, object actual)
                        => Assert.AreSame(expected, actual);

        //
        // Summary:
        //     Tests whether the specified objects both refer to the same object and throws
        //     an exception if the two inputs do not refer to the same object.
        //
        // Parameters:
        //   expected:
        //     The first object to compare. This is the value the test expects.
        //
        //   actual:
        //     The second object to compare. This is the value produced by the code under test.
        //
        //   message:
        //     The message to include in the exception when actual is not the same as expected.
        //     The message is shown in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if expected does not refer to the same object as actual.
        public static void AreSame(this Assert assert, object expected, object actual, string message, params object[] parameters)
                        => Assert.AreSame(expected, actual, message, parameters);

        //
        // Summary:
        //     Tests whether the specified objects both refer to the same object and throws
        //     an exception if the two inputs do not refer to the same object.
        //
        // Parameters:
        //   expected:
        //     The first object to compare. This is the value the test expects.
        //
        //   actual:
        //     The second object to compare. This is the value produced by the code under test.
        //
        //   message:
        //     The message to include in the exception when actual is not the same as expected.
        //     The message is shown in test results.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if expected does not refer to the same object as actual.
        public static void AreSame(this Assert assert, object expected, object actual, string message)
                        => Assert.AreSame(expected, actual, message);

        //
        // Summary:
        //     Static equals overloads are used for comparing instances of two types for reference
        //     equality. This method should not be used for comparison of two instances for
        //     equality. This object will always throw with Assert.Fail. Please use Assert.AreEqual
        //     and associated overloads in your unit tests.
        //
        // Parameters:
        //   objA:
        //     Object A
        //
        //   objB:
        //     Object B
        //
        // Returns:
        //     False, always.
        public static bool Equals(this Assert assert, object objA, object objB)
                        => Assert.Equals(objA, objB);

        //
        // Summary:
        //     Throws an AssertFailedException.
        //
        // Parameters:
        //   message:
        //     The message to include in the exception. The message is shown in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Always thrown.
        public static void Fail(this Assert assert, string message, params object[] parameters)
                        => Assert.Fail(message, parameters);

        //
        // Summary:
        //     Throws an AssertFailedException.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Always thrown.
        public static void Fail(this Assert assert)
                        => Assert.Fail();

        //
        // Summary:
        //     Throws an AssertFailedException.
        //
        // Parameters:
        //   message:
        //     The message to include in the exception. The message is shown in test results.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Always thrown.
        public static void Fail(this Assert assert, string message)
                        => Assert.Fail(message);

        //
        // Summary:
        //     Throws an AssertInconclusiveException.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertInconclusiveException:
        //     Always thrown.
        public static void Inconclusive(this Assert assert)
                        => Assert.Inconclusive();

        //
        // Summary:
        //     Throws an AssertInconclusiveException.
        //
        // Parameters:
        //   message:
        //     The message to include in the exception. The message is shown in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertInconclusiveException:
        //     Always thrown.
        public static void Inconclusive(this Assert assert, string message, params object[] parameters)
                        => Assert.Inconclusive(message, parameters);

        //
        // Summary:
        //     Throws an AssertInconclusiveException.
        //
        // Parameters:
        //   message:
        //     The message to include in the exception. The message is shown in test results.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertInconclusiveException:
        //     Always thrown.
        public static void Inconclusive(this Assert assert, string message)
                        => Assert.Inconclusive(message);

        //
        // Summary:
        //     Tests whether the specified condition is false and throws an exception if the
        //     condition is true.
        //
        // Parameters:
        //   condition:
        //     The condition the test expects to be false.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if condition is true.
        public static void False(this Assert assert, bool condition)
                        => Assert.IsFalse(condition);

        //
        // Summary:
        //     Tests whether the specified condition is false and throws an exception if the
        //     condition is true.
        //
        // Parameters:
        //   condition:
        //     The condition the test expects to be false.
        //
        //   message:
        //     The message to include in the exception when condition is true. The message is
        //     shown in test results.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if condition is true.
        public static void False(this Assert assert, bool condition, string message)
                        => Assert.IsFalse(condition, message);

        //
        // Summary:
        //     Tests whether the specified condition is false and throws an exception if the
        //     condition is true.
        //
        // Parameters:
        //   condition:
        //     The condition the test expects to be false.
        //
        //   message:
        //     The message to include in the exception when condition is true. The message is
        //     shown in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if condition is true.
        public static void False(this Assert assert, bool condition, string message, params object[] parameters)
                        => Assert.IsFalse(condition, message, parameters);

        //
        // Summary:
        //     Tests whether the specified object is an instance of the expected type and throws
        //     an exception if the expected type is not in the inheritance hierarchy of the
        //     object.
        //
        // Parameters:
        //   value:
        //     The object the test expects to be of the specified type.
        //
        //   expectedType:
        //     The expected type of value.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if value is null or expectedType is not in the inheritance hierarchy of
        //     value.
        public static void IsInstanceOfType(this Assert assert, object value, Type expectedType)
                        => Assert.IsInstanceOfType(value, expectedType);

        //
        // Summary:
        //     Tests whether the specified object is an instance of the expected type and throws
        //     an exception if the expected type is not in the inheritance hierarchy of the
        //     object.
        //
        // Parameters:
        //   value:
        //     The object the test expects to be of the specified type.
        //
        //   expectedType:
        //     The expected type of value.
        //
        //   message:
        //     The message to include in the exception when value is not an instance of expectedType.
        //     The message is shown in test results.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if value is null or expectedType is not in the inheritance hierarchy of
        //     value.
        public static void IsInstanceOfType(this Assert assert, object value, Type expectedType, string message)
                        => Assert.IsInstanceOfType(value, expectedType, message);

        //
        // Summary:
        //     Tests whether the specified object is an instance of the expected type and throws
        //     an exception if the expected type is not in the inheritance hierarchy of the
        //     object.
        //
        // Parameters:
        //   value:
        //     The object the test expects to be of the specified type.
        //
        //   expectedType:
        //     The expected type of value.
        //
        //   message:
        //     The message to include in the exception when value is not an instance of expectedType.
        //     The message is shown in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if value is null or expectedType is not in the inheritance hierarchy of
        //     value.
        public static void IsInstanceOfType(this Assert assert, object value, Type expectedType, string message, params object[] parameters)
                        => Assert.IsInstanceOfType(value, expectedType, message, parameters);

        //
        // Summary:
        //     Tests whether the specified object is not an instance of the wrong type and throws
        //     an exception if the specified type is in the inheritance hierarchy of the object.
        //
        // Parameters:
        //   value:
        //     The object the test expects not to be of the specified type.
        //
        //   wrongType:
        //     The type that value should not be.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if value is not null and wrongType is in the inheritance hierarchy of
        //     value.
        public static void IsNotInstanceOfType(this Assert assert, object value, Type wrongType)
                        => Assert.IsNotInstanceOfType(value, wrongType);

        //
        // Summary:
        //     Tests whether the specified object is not an instance of the wrong type and throws
        //     an exception if the specified type is in the inheritance hierarchy of the object.
        //
        // Parameters:
        //   value:
        //     The object the test expects not to be of the specified type.
        //
        //   wrongType:
        //     The type that value should not be.
        //
        //   message:
        //     The message to include in the exception when value is an instance of wrongType.
        //     The message is shown in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if value is not null and wrongType is in the inheritance hierarchy of
        //     value.
        public static void IsNotInstanceOfType(this Assert assert, object value, Type wrongType, string message, params object[] parameters)
                        => Assert.IsNotInstanceOfType(value, wrongType, message, parameters);

        //
        // Summary:
        //     Tests whether the specified object is not an instance of the wrong type and throws
        //     an exception if the specified type is in the inheritance hierarchy of the object.
        //
        // Parameters:
        //   value:
        //     The object the test expects not to be of the specified type.
        //
        //   wrongType:
        //     The type that value should not be.
        //
        //   message:
        //     The message to include in the exception when value is an instance of wrongType.
        //     The message is shown in test results.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if value is not null and wrongType is in the inheritance hierarchy of
        //     value.
        public static void IsNotInstanceOfType(this Assert assert, object value, Type wrongType, string message)
                        => Assert.IsNotInstanceOfType(value, wrongType, message);

        //
        // Summary:
        //     Tests whether the specified object is non-null and throws an exception if it
        //     is null.
        //
        // Parameters:
        //   value:
        //     The object the test expects not to be null.
        //
        //   message:
        //     The message to include in the exception when value is null. The message is shown
        //     in test results.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if value is null.
        public static void IsNotNull(this Assert assert, object value, string message)
                        => Assert.IsNotNull(value, message);

        //
        // Summary:
        //     Tests whether the specified object is non-null and throws an exception if it
        //     is null.
        //
        // Parameters:
        //   value:
        //     The object the test expects not to be null.
        //
        //   message:
        //     The message to include in the exception when value is null. The message is shown
        //     in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if value is null.
        public static void IsNotNull(this Assert assert, object value, string message, params object[] parameters)
                        => Assert.IsNotNull(value, message, parameters);

        //
        // Summary:
        //     Tests whether the specified object is non-null and throws an exception if it
        //     is null.
        //
        // Parameters:
        //   value:
        //     The object the test expects not to be null.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if value is null.
        public static void IsNotNull(this Assert assert, object value)
                        => Assert.IsNotNull(value);

        //
        // Summary:
        //     Tests whether the specified object is null and throws an exception if it is not.
        //
        // Parameters:
        //   value:
        //     The object the test expects to be null.
        //
        //   message:
        //     The message to include in the exception when value is not null. The message is
        //     shown in test results.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if value is not null.
        public static void Null(this Assert assert, object value, string message)
                        => Assert.IsNull(value, message);

        //
        // Summary:
        //     Tests whether the specified object is null and throws an exception if it is not.
        //
        // Parameters:
        //   value:
        //     The object the test expects to be null.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if value is not null.
        public static void Null(this Assert assert, object value)
                        => Assert.IsNull(value);

        //
        // Summary:
        //     Tests whether the specified object is null and throws an exception if it is not.
        //
        // Parameters:
        //   value:
        //     The object the test expects to be null.
        //
        //   message:
        //     The message to include in the exception when value is not null. The message is
        //     shown in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if value is not null.
        public static void Null(this Assert assert, object value, string message, params object[] parameters)
                        => Assert.IsNull(value, message, parameters);

        //
        // Summary:
        //     Tests whether the specified condition is true and throws an exception if the
        //     condition is false.
        //
        // Parameters:
        //   condition:
        //     The condition the test expects to be true.
        //
        //   message:
        //     The message to include in the exception when condition is false. The message
        //     is shown in test results.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if condition is false.
        public static void True(this Assert assert, bool condition, string message)
                        => Assert.IsTrue(condition, message);

        //
        // Summary:
        //     Tests whether the specified condition is true and throws an exception if the
        //     condition is false.
        //
        // Parameters:
        //   condition:
        //     The condition the test expects to be true.
        //
        //   message:
        //     The message to include in the exception when condition is false. The message
        //     is shown in test results.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if condition is false.
        public static void True(this Assert assert, bool condition, string message, params object[] parameters)
                        => Assert.IsTrue(condition, message);

        //
        // Summary:
        //     Tests whether the specified condition is true and throws an exception if the
        //     condition is false.
        //
        // Parameters:
        //   condition:
        //     The condition the test expects to be true.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if condition is false.
        public static void True(this Assert assert, bool condition)
                        => Assert.IsTrue(condition);

        //
        // Summary:
        //     Replaces null characters (this Assert assert,'\0') with "\\0".
        //
        // Parameters:
        //   input:
        //     The string to search.
        //
        // Returns:
        //     The converted string with null characters replaced by "\\0".
        //
        // Remarks:
        //     This is only public and still present to preserve compatibility with the V1 framework.
        public static string ReplaceNullChars(this Assert assert, string input)
                        => Assert.ReplaceNullChars(input);

        //
        // Summary:
        //     Tests whether the code specified by delegate action throws exact given exception
        //     of type T (this Assert assert,and not of derived type) and throws AssertFailedException if code
        //     does not throws exception or throws exception of type other than T.
        //
        // Parameters:
        //   action:
        //     Delegate to code to be tested and which is expected to throw exception.
        //
        // Type parameters:
        //   T:
        //     Type of exception expected to be thrown.
        //
        // Returns:
        //     The exception that was thrown.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if action does not throws exception of type T.
        public static T ThrowsException<T>(this Assert assert, Action action) where T : Exception
             => Assert.ThrowsException<T>(action);
        //
        // Summary:
        //     Tests whether the code specified by delegate action throws exact given exception
        //     of type T (this Assert assert,and not of derived type) and throws AssertFailedException if code
        //     does not throws exception or throws exception of type other than T.
        //
        // Parameters:
        //   action:
        //     Delegate to code to be tested and which is expected to throw exception.
        //
        //   message:
        //     The message to include in the exception when action does not throws exception
        //     of type T.
        //
        // Type parameters:
        //   T:
        //     Type of exception expected to be thrown.
        //
        // Returns:
        //     The exception that was thrown.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if action does not throws exception of type T.
        public static T ThrowsException<T>(this Assert assert, Action action, string message) where T : Exception
                => Assert.ThrowsException<T>(action, message);
        //
        // Summary:
        //     Tests whether the code specified by delegate action throws exact given exception
        //     of type T (this Assert assert,and not of derived type) and throws AssertFailedException if code
        //     does not throws exception or throws exception of type other than T.
        //
        // Parameters:
        //   action:
        //     Delegate to code to be tested and which is expected to throw exception.
        //
        // Type parameters:
        //   T:
        //     Type of exception expected to be thrown.
        //
        // Returns:
        //     The exception that was thrown.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if action does not throws exception of type T.
        public static T ThrowsException<T>(this Assert assert, Func<object> action) where T : Exception
                    => Assert.ThrowsException<T>(action);
        //
        // Summary:
        //     Tests whether the code specified by delegate action throws exact given exception
        //     of type T (this Assert assert,and not of derived type) and throws AssertFailedException if code
        //     does not throws exception or throws exception of type other than T.
        //
        // Parameters:
        //   action:
        //     Delegate to code to be tested and which is expected to throw exception.
        //
        //   message:
        //     The message to include in the exception when action does not throws exception
        //     of type T.
        //
        // Type parameters:
        //   T:
        //     Type of exception expected to be thrown.
        //
        // Returns:
        //     The exception that was thrown.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if action does not throws exception of type T.
        public static T ThrowsException<T>(this Assert assert, Func<object> action, string message) where T : Exception
                    => Assert.ThrowsException<T>(action, message);
        //
        // Summary:
        //     Tests whether the code specified by delegate action throws exact given exception
        //     of type T (this Assert assert,and not of derived type) and throws AssertFailedException if code
        //     does not throws exception or throws exception of type other than T.
        //
        // Parameters:
        //   action:
        //     Delegate to code to be tested and which is expected to throw exception.
        //
        //   message:
        //     The message to include in the exception when action does not throws exception
        //     of type T.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Type parameters:
        //   T:
        //     Type of exception expected to be thrown.
        //
        // Returns:
        //     The exception that was thrown.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if action does not throw exception of type T.
        public static T ThrowsException<T>(this Assert assert, Func<object> action, string message, params object[] parameters) where T : Exception
                      => Assert.ThrowsException<T>(action, message, parameters);
        //
        // Summary:
        //     Tests whether the code specified by delegate action throws exact given exception
        //     of type T (this Assert assert,and not of derived type) and throws AssertFailedException if code
        //     does not throws exception or throws exception of type other than T.
        //
        // Parameters:
        //   action:
        //     Delegate to code to be tested and which is expected to throw exception.
        //
        //   message:
        //     The message to include in the exception when action does not throws exception
        //     of type T.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Type parameters:
        //   T:
        //     Type of exception expected to be thrown.
        //
        // Returns:
        //     The exception that was thrown.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if action does not throws exception of type T.
        public static T ThrowsException<T>(this Assert assert, Action action, string message, params object[] parameters) where T : Exception
                  => Assert.ThrowsException<T>(action, message, parameters);
        //
        // Summary:
        //     Tests whether the code specified by delegate action throws exact given exception
        //     of type T (this Assert assert,and not of derived type) and throws AssertFailedException if code
        //     does not throws exception or throws exception of type other than T.
        //
        // Parameters:
        //   action:
        //     Delegate to code to be tested and which is expected to throw exception.
        //
        // Type parameters:
        //   T:
        //     Type of exception expected to be thrown.
        //
        // Returns:
        //     The System.Threading.Tasks.Task executing the delegate.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if action does not throws exception of type T.

        public static Task<T> ThrowsExceptionAsync<T>(this Assert assert, Func<Task> action) where T : Exception
                    => Assert.ThrowsExceptionAsync<T>(action);
        //
        // Summary:
        //     Tests whether the code specified by delegate action throws exact given exception
        //     of type T (this Assert assert,and not of derived type) and throws AssertFailedException if code
        //     does not throws exception or throws exception of type other than T.
        //
        // Parameters:
        //   action:
        //     Delegate to code to be tested and which is expected to throw exception.
        //
        //   message:
        //     The message to include in the exception when action does not throws exception
        //     of type T.
        //
        // Type parameters:
        //   T:
        //     Type of exception expected to be thrown.
        //
        // Returns:
        //     The System.Threading.Tasks.Task executing the delegate.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if action does not throws exception of type T.
        public static Task<T> ThrowsExceptionAsync<T>(this Assert assert, Func<Task> action, string message) where T : Exception
                    => Assert.ThrowsExceptionAsync<T>(action, message);
        //
        // Summary:
        //     Tests whether the code specified by delegate action throws exact given exception
        //     of type T (this Assert assert,and not of derived type) and throws AssertFailedException if code
        //     does not throws exception or throws exception of type other than T.
        //
        // Parameters:
        //   action:
        //     Delegate to code to be tested and which is expected to throw exception.
        //
        //   message:
        //     The message to include in the exception when action does not throws exception
        //     of type T.
        //
        //   parameters:
        //     An array of parameters to use when formatting message.
        //
        // Type parameters:
        //   T:
        //     Type of exception expected to be thrown.
        //
        // Returns:
        //     The System.Threading.Tasks.Task executing the delegate.
        //
        // Exceptions:
        //   T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:
        //     Thrown if action does not throws exception of type T.

        public static Task<T> ThrowsExceptionAsync<T>(this Assert assert, Func<Task> action, string message, params object[] parameters) where T : Exception
                       => Assert.ThrowsExceptionAsync<T>(action, message);


    }
}
