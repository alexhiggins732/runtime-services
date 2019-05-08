using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Runtime.ConversionServices
{
    public static class ActionExtensions
    {
        public static Func<T> ResolveInstance<T>(this T instance)
             => () => instance;

        //public static Func<Action>
        //    ResolveAction<T, Action>(this T instance, Func<T, Action> action) =>
        //        x => action(instance);


        //public static void ResolveTest()
        //{
        //    var sb = new SignatureBuilderTest();
        //    var ac = sb.ResolveAction(x=> x.SomeMethod);

        //}
    }
    public struct ActionCall<T, TMethod>
    {
        //static Action<T, TMethod> Call;
        public static Action Call;

        public static Action<T, Func<T, Action>> Call2;
    }

    public struct ActionCall<T>
    {
        //static Action<T, TMethod> Call;
        public static Action Call;
        public static Action<T> CallGeneric;
        public static Action<T, Func<T, Action>> Call2;
    }

    public interface ISignature
    {

    }
    public struct Signature : ISignature
    {

    }
    public class SignatureBuilder
    {

        public static ISignature GetMethodSignature(MethodInfo method)
        {
            var result = new Signature();
            var typeParams = new List<Type>();
            var allTypeParams = new List<Type>();

            var metaDataToken = method.MetadataToken;
            var declaringType = method.DeclaringType;
            var declaringTypeToken = declaringType.MetadataToken;
            var module = declaringType.Module;
            var moduleToken = module.MetadataToken;
            var assembly = module.Assembly;
            var assemblyFullName = assembly.FullName;
            // assembly pointer, CTOR Token, Guid
            var atts = assembly.GetCustomAttribute(typeof(System.Runtime.InteropServices.GuidAttribute));
            var assemblyGuid = ((System.Runtime.InteropServices.GuidAttribute)atts).Value;

            var isGeneric = method.IsGenericMethod;
            var isGenericMethodDefintion = method.IsGenericMethodDefinition;

            if (isGeneric)
            {
                var genericArgs = method.GetGenericArguments();
                var genericMethodDef = method.GetGenericMethodDefinition();
            }

            if(method.ReturnType==typeof(void))
            {
                method.To<Action>();
            }
            allTypeParams.Add(method.DeclaringType);

            // [returntype] [methodname]<GenericParameters[]>(Arguments)
            //void(), void<...>, void([]), void<...>([]);
            //add return type last, then walk





            //Given method token, arguments:
            //  1) resolve method info from token.
            //  2) use method info to get list of arguments.
            //  3) create array of args from stack.
            //  4) call method using reflection
            //  5) push result onto stack if method result is not void.

            // Using generics.
            //  ITypedReference.Call(string Name, int MethodToken, ) -> instance methods only.
            //      will there be TypedReference for Static Classes, EG System.Console?
            //  IStaticReference.Call(MethodInfo, int MethodToken)
            var g = 1.ToTypedReference();
            //  1) Method to be called passed to ICaller
            //          A few overloads.
            //  2) Method signature will be globally unique(consider assemblies).
            //          Assembly.Module.Type.MethodType.ReturnType.Arguments.


            bool isStatic = method.IsStatic;
            if (!isStatic)
            {
                typeParams.Add(method.DeclaringType);
            }
            var name = method.ToString();
            typeParams.AddRange(method.GetParameters().Select(x => x.ParameterType));
            var isAction = method.ReturnType == typeof(void);
            if (!isAction)
                typeParams.Add(method.ReturnType);

            var test = new[] { 1 }.Select(x => (Action<string>)Console.Write)
                .First();
            //var equals = test(typeof(bool));

            Type SignatureType = null;
            if (isAction)
            {
                if (isStatic)
                {
                    // Action => public static void();
                    if (typeParams.Count() == 0)
                        SignatureType = typeof(Action);
                    else
                    {
                        // Action => public static void(params[]);
                        SignatureType = typeof(Action<>).MakeGenericType(typeParams.ToArray());

                    }
                }
                else
                {
                    //need to pass instance.
                    //Does the signature need func to resolve the instance?
                }

            }

            return result;
        }

        internal static ISignature GetMethodSignature(Action action)
        {
            return GetMethodSignature(action.Method);
        }
    }



    public class SignatureBuilderTest
    {
        public static void TestBuilder()
        {
          
            //var signature1 = SignatureBuilder.GetMethodSignature(StaticVoid);
            var t = typeof(SignatureBuilderTest);
            //need to check fields
            var methods = t.GetMethods(BindingFlags.Public |
                BindingFlags.Instance | BindingFlags.Static).ToList();
            var fields = t.GetFields(BindingFlags.Public |
                BindingFlags.Instance | BindingFlags.Static).ToList();
            var StaticVoidTout = methods.Where(x => x ==
             (new[] { 1 }).Select(c => (Action)StaticVoid).First().Method);

            var signature1 = SignatureBuilder.GetMethodSignature(methods[2]);

        }

        public static void StaticVoid()
        {
            Console.WriteLine("Called");
        }

        public static void StaticVoid<Tout>()
        {
            Console.WriteLine("Called");
        }

        public static void StaticVoid(string a)
        {
            Console.WriteLine("Called");
        }
        public static void StaticVoid(int a)
        {
            Console.WriteLine("Called");
        }

        public static void StaticVoid<T1>(string a)
        {
            Console.WriteLine("Called");
        }
        public static void StaticVoid<T1>(int a)
        {
            Console.WriteLine("Called");
        }

        public static void StaticVoid<T1, T2>(string a)
        {
            Console.WriteLine("Called");
        }
        public static void StaticVoid<T1, T2>(int a)
        {
            Console.WriteLine("Called");
        }
        public static Action StaticAction = () => Console.WriteLine("Called");
        public static Action<int> StaticAction2 = (a) => Console.WriteLine("Called");
        public static Action<int, bool> StaticAction3 = (a, b) => Console.WriteLine("Called");
        public static Action<bool, int> StaticAction4 = (a, b) => Console.WriteLine("Called");
        public static bool StaticMethod() => false;
        public static bool StaticMethod(string a) => false;
        public static bool StaticMethod(int a) => false;
        public static bool StaticMethod<t1>() => false;
        public static bool StaticMethod<t1>(string a) => false;
        public static bool StaticMethod<t1>(int a) => false;
        public static bool StaticMethod<t1, t2>() => false;
        public static bool StaticMethod<t1, t2>(string a) => false;
        public static bool StaticMethod<t1, t2>(int a) => false;
        public static Func<bool> SomeBool => () => true;
        public static Func<bool, bool> SomeBool2 => (a) => true;
        public static Func<int> SomeBool3 => () => 1;
        public static Func<int, bool> SomeBool4 => (a) => true;
        public static Func<bool, int> SomeBool5 => (a) => 1;
        public void InstanceVoid()
        {
            Console.WriteLine("Called");
        }
        public void InstanceVoid(string a)
        {
            Console.WriteLine("Called");
        }
        public void InstanceVoid(int a)
        {
            Console.WriteLine("Called");
        }
        public bool InstanceMethod() { return true; }
        public bool InstanceMethod<T>() { return true; }
        public bool InstanceMethod<T, T2>() { return true; }
        public bool InstanceMethod(string arg) { return true; }
        public bool InstanceMethod<T>(string arg, int arg1) { return true; }
        public bool InstanceMethod<T, T2>(string arg, int arg1) { return true; }
        public void CallAction<T>(T instance, Func<T, Action> method)
        {
            var action = method(instance);
            action();
        }
        public void CallAction2(Action action)
        {
            action();
        }
        public void StaticActionTest()
        {
        }
        public void InlineFuncImplicit()
        {
            ActionCall<SignatureBuilderTest,
                Func<SignatureBuilderTest, Action>>.Call2 = CallAction;
        }

        public void SomeMethod()
        {

        }
        public static void TypeRefActionTest()
        {
            var sigBuilder = new SignatureBuilderTest();
            var c = typeof(ActionCall<>).MakeGenericType(typeof(SignatureBuilderTest));
            var field = c.GetField("Call", Reflection.BindingFlags.Public | Reflection.BindingFlags.Static);
            field.SetValue(null, (Action)sigBuilder.SomeMethod);
            ActionCall<SignatureBuilderTest>.Call();

            var sigBuilderBoxed = (object)sigBuilder;
            var sigRef = sigBuilder.ToTypedReference();
            sigBuilderBoxed.ToTypedReference();

            ActionCall<SignatureBuilderTest,
               Func<SignatureBuilderTest, Func<SignatureBuilderTest, Action>>
               >.Call2
                = sigBuilder.CallAction;

            ActionCall<
                Func
                    <
                        //instance to call the action on
                        SignatureBuilderTest,
                        //generic arguments of the method to be called
                        Func<SignatureBuilderTest, Action> //generic arguments of the
                    >
                >.Call2 = sigBuilder.CallAction;



            ActionCall<Action>.Call = sigBuilder.SomeMethod;
            ActionCall<Func<SignatureBuilderTest, Action>>
                .CallGeneric = (methodResolver) =>
                {
                    var action = methodResolver(sigBuilder);
                    action();

                };

            //ActionCall<Func<SignatureBuilderTest, Action>>.CallGeneric(sigBuilder);

            //ActionCall<SignatureBuilder,
            //    Func<SignatureBuilder, Action>>
            //        .Call2(sigBuilder, sigBuilder.SomeMethod);
        }


    }
}
