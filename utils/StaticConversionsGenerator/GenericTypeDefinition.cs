using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.ConversionServices;
using System.Reflection;


namespace StaticConversionsGenerator
{
    public static class GenericRuntimeTypeDefinitionExtensions
    {
        public static GenericRuntimeTypeDefinitionFactory.IGenericRuntimeTypeDefinition RuntimeTypeDefintion(this Type t)
        {
            var genericType = typeof(GenericRuntimeTypeDefinitionFactory.GenericRuntimeTypeDefinition<>).MakeGenericType(t);
            var result = (GenericRuntimeTypeDefinitionFactory.IGenericRuntimeTypeDefinition)Activator.CreateInstance(genericType);
            return result;
        }
        public static IRuntimeTypedReference New(this Type t) => t.RuntimeTypeDefintion().New();
        public static IRuntimeTypedReference New(this Type t, params object[] args) => t.RuntimeTypeDefintion().New(args);

    }
    public class GenericRuntimeTypeDefinitionFactory
    {
        //TODO: Cleanup interface. Expose limited number of top level types
        public static void ExplicitInterfaceExtenstionTests()
        {
            //TODO: Did we lose interface cleanup?
            var newFromType = typeof(TestClass).New();

            //TODO: Remove need to cast arge to runtime typed reference
            //TODO: Expose only via Arithmetic interface.
            newFromType.Add(1.ToTypedReference());
            newFromType.Add<int>(1.ToTypedReference());
            newFromType.Arithmetic();// <- this is okay I think.
            //TODO: Nested and renamed (maybe op binary)
            newFromType.BinaryCall(1.ToTypedReference(), OperatorType.Add);
            //TODO: Should this be a method call?
            //TODO: Make explicit interface method and point to T Value;
            var boxed = newFromType.BoxedValue;
        }
        //TODO: Decide on API implementation definition;
        public static void CompileInterfaceUsageTests()
        {
            //newFromType.Props(TestClass.IntField).Get();
            //newFromType.Props("name").Get;
            //newFromType.Props("name").Set(1);

            //newFromType.Fields(TestClass.IntField).Get();
            //newFromType.Fields("name").Get;
            //newFromType.Fields("name").Set(1);

            ////VS:
            //GenericRuntimeTypeDefinition<TestClass>.Instance(newFromType).Properties(name) = "1";
            //GenericRuntimeTypeDefinition<TestClass>.Static.Properties("name") = "1";
            ////VS:

            ////Also-> methods vs 1) dictionary for all?
            //newFromType.Members["Name"] = "some value";
            ////vs single method.
            //newFromType.Members("Name") = "some value";
            ////vs 
            //newFromType.Members("Name").Set("some value");
            //newFromType.Members("Name").Get;
            //newFromType.Members("Name").Get();
            //newFromType.Members("Name").Invoke();
            //// vs:

            //newFromType.SetField(nameof(TestClass.IntField), 1);
            //newFromType.SetField(nameof(TestClass.StringField), "1");//TODO: need to setup implicit string conversions.
            //newFromType.GetField(nameof(TestClass.IntField), 1);
            //newFromType.GetField(nameof(TestClass.StringField), "1");//TODO: need to setup implicit string conversions.
            //newFromType.SetProp(nameof(TestClass.IntProperty), 1);
            //newFromType.GetProp(nameof(TestClass.IntProperty), 1);
            //newFromType.Call(nameof(TestClass.SomeVoid));
            //newFromType.Call(nameof(TestClass.SomeIntMethod);
            //newFromType.Call(nameof(TestClass.SomeIntMethod), 1);
            //newFromType.Call(nameof(TestClass.SomeIntMethod), 1, "s");
            //newFromType.Call(nameof(TestClass.SomeIntMethod), new[] { 1, "s" });
        }
        public static void TestGenericTypeDefinition()
        {

            var testClassType = typeof(TestClass);
            var defInterface = testClassType.RuntimeTypeDefintion();

            var infNew = defInterface.New();
            var infNewWithParams = defInterface.New("hello", 1);
            var infNewDefault = defInterface.GenericRuntimeTypeInfo.DefaultConstructor.Invoke();

            var newFromType = testClassType.New();
            var newFromTypeWithArgs = testClassType.New("hello", 1);




            //TODO: Implement. Decide on nest.

            //Two considerations. 1) compile time API. 2 Runtime api.



            var def = new GenericRuntimeTypeDefinition<TestClass>();
            var typedRef = def.TypedReference;



            var newTestDefault = def.New();

            //TODO: Cleanup Extensions on strongly typed refrerences.
            //newTestDefault.Compare

            var newTestPublicFirst = def.New(x => x.TypeInfo.PublicConstructors.First());
            var newTestPublicFirstFromTypeInfo = def.New(x => x.TypeInfo.PublicConstructors.First());


            var newTestPublicSecond = def.New(x => x.TypeInfo.PublicConstructors[1], "hello");
            var newTestPrivateFirst = def.New(x => x.TypeInfo.PrivateConstructors.First(), "hello", 1);
            var newTestPrivateSecond = def.New(x => x.TypeInfo.PrivateConstructors[1], 1);

            var newTestResolved = def.New("hello", 1);


        }

        public class TestClass
        {
            public TestClass() { }
            public TestClass(string arg) { }
            private TestClass(string arg, int a) { }
            protected TestClass(int a) { }

            public int IntField;
            public string StringField;
            public int IntProperty { get; set; }
            public int IntReadOnlyProperty { get; }
            public int IntReadPrivateSetProperty { get; private set; }
            public int IntReadProtectedSetProperty { get; protected set; }
            public int IntWriteOnlyProperty { set { } }
            int IntPrivateWriteOnlyProperty { set { } }
            protected int IntProtectedSetProperty { set { } }
            public void SomeVoid() { }
            public void SomeVoid(string arg) { }
            public int SomeIntMethod() => 1;
            public int SomeIntMethod(string arg) => 1;
            public int SomeIntMethod<T>() => 1;
            public int SomeIntMethod<T>(string arg) => 1;
        }

        public interface IConstructorDefinition
        {
            IRuntimeTypedReference Invoke(params object[] args);
            Type[] ArgumentTypes { get; }
        }
        public interface IPublicConstructorDefinition : IConstructorDefinition { }
        public interface IPrivateConstructorDefinition : IConstructorDefinition { }

        //TODO: Compile the constructor and invoke via
        //  ConstructorCall<Type,ConstructorArgs>.Invoke



        public struct ConstructorCall<TType, TConstructorArgs>
        {
            public static Func<TConstructorArgs, TType> Ctor;
        }
        public struct MethodArgs<T>
        {

        }

        public class ConstructorDefinition<T> : IConstructorDefinition
        {
            public ConstructorInfo x;
            public ConstructorDefinition(ConstructorInfo constructorInfo) => this.x = constructorInfo;
            public T Invoke(params object[] args)// hook in constructor compiler here? or leave it decoupled?
            {
                //TODO: Need to check the number of arguments provided matches the arity of the generic type definition
                //               will be used to hold the arguments used to invoke the instantiation.
                //      Provide up to 8 generic overrides after which use object[] or anonymous as the generic type.
                //
                //var anonymousArgs = args.Select(x => new { x }).ToArray();
                //var tr = anonymousArgs.ToTypedReference();
                //var trGeneric = tr.MakeGeneric(typeof(MethodArgs<>)).New();

                ////TODO: Solidify SetValue. It is only meant for the typedReference itself.

                //trGeneric.SetValue(anonymousArgs);// can't use set value 
                //do we even need to do this? ToTypedReference has T to Generic<T> baked in.
                // be we want to bind a construct call to MethodArgs<T>.CallFunc
                // and RuntimeTypedReference<T> doesn't have this baked int.

                //var constructorArgs = typeof(MethodArgs<>).MakeGenericType(anonymousArgs.GetType()).RuntimeTypeDefintion();
                //var instance = constructorArgs.New();
                //instance.SetValue(anonymousArgs);

                return (T)x.Invoke(args);
            }
            IRuntimeTypedReference IConstructorDefinition.Invoke(params object[] args) => new RuntimeTypedReference<T>(Invoke(args));
            private Type[] argumentTypes;
            public Type[] ArgumentTypes => (argumentTypes ?? (argumentTypes = x.GetParameters().Select(x => x.ParameterType).ToArray()));
        }

        //TODO: Determine feasibility of static cache vs introducing garbage collection at the cost of performance.
        public class PublicConstructorDefinition<T> : ConstructorDefinition<T>
        {
            public PublicConstructorDefinition(ConstructorInfo x) : base(x) { }
        }
        public class PrivateConstructorDefinition<T> : ConstructorDefinition<T>
        {
            public PrivateConstructorDefinition(ConstructorInfo x) : base(x) { }
        }

        public interface IRuntimeTypeInfo
        {

            IConstructorDefinition DefaultConstructor { get; }
        }

        public class GenericRuntimeTypeInfo<T> : IRuntimeTypeInfo
        {
            private GenericRuntimeTypeDefinition<T> typeDefinition;
            private RuntimeTypedReference<T> typedReference => typeDefinition.TypedReference;
            public GenericRuntimeTypeInfo(GenericRuntimeTypeDefinition<T> typeDefinition)
            {
                this.typeDefinition = typeDefinition;

                var type = typedReference.GenericArgumentType;
                var publicCtors = type.GetConstructors();
                PublicConstructors = new List<PublicConstructorDefinition<T>>();
                PrivateConstructors = new List<PrivateConstructorDefinition<T>>();
                if (publicCtors.Length > 0)
                {
                    PublicConstructors.AddRange(publicCtors.Select(x => new PublicConstructorDefinition<T>(x)));
                }

                var privateCtors = type.GetConstructors(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (privateCtors.Length > 0)
                    PrivateConstructors.AddRange(privateCtors.Select(x => new PrivateConstructorDefinition<T>(x)));
            }
            private ConstructorDefinition<T> defaultConstructor;
            public ConstructorDefinition<T> DefaultConstuctor() => defaultConstructor ?? (defaultConstructor = getDefaultConstuctor());

            IConstructorDefinition IRuntimeTypeInfo.DefaultConstructor => DefaultConstuctor();

            private ConstructorDefinition<T> getDefaultConstuctor()
            {
                var result = ResolveConstructor();
                return result;
            }

            public List<PublicConstructorDefinition<T>> PublicConstructors;
            public List<PrivateConstructorDefinition<T>> PrivateConstructors;
            public ConstructorDefinition<T> ResolveConstructor(params object[] args)
            {
                var argTypes = (args ?? new object[] { })
                    .Select(x => x?.GetType() ?? typeof(object)).ToArray();
                var constructor = (ConstructorDefinition<T>)PublicConstructors.FirstOrDefault(x => x.ArgumentTypes.SequenceEqual(argTypes))
                 ?? (ConstructorDefinition<T>)PrivateConstructors.FirstOrDefault(x => x.ArgumentTypes.SequenceEqual(argTypes));
                return constructor;
            }
        }


        public interface IGenericRuntimeTypeDefinition
        {
            IRuntimeTypedReference New();
            IRuntimeTypedReference New(params object[] args);
            IRuntimeTypeInfo GenericRuntimeTypeInfo { get; }
        }

        public class GenericRuntimeTypeDefinition<T> : IGenericRuntimeTypeDefinition
        {
            public RuntimeTypedReference<T> TypedReference = new RuntimeTypedReference<T>();
            public GenericRuntimeTypeInfo<T> TypeInfo => new GenericRuntimeTypeInfo<T>(this);

            IRuntimeTypeInfo IGenericRuntimeTypeDefinition.GenericRuntimeTypeInfo => TypeInfo;
            public GenericRuntimeTypeDefinition()
            {
                TypedReference = new RuntimeTypedReference<T>();
            }


            //TODO: move constructor (and other methods, fields, etc) into TypeInfo class exposed as single property.
            //TODO: Expose New(), Call(), Get(), Set() on GenericTypeDefinition
            //TODO: Convert methods,getters and setters into delegates (action or func)
            //TODO: Auto bind delegates using generics 


            //Boxed Fluent API
            IRuntimeTypedReference IGenericRuntimeTypeDefinition.New() => new RuntimeTypedReference<T>(New());
            IRuntimeTypedReference IGenericRuntimeTypeDefinition.New(params object[] args) => new RuntimeTypedReference<T>(New(args));

            //Typed Fluent API
            public T New()
            {
                var result = default(T);
                if (result == null)
                {
                    var ctor = TypeInfo.DefaultConstuctor();
                    return ctor.Invoke();
                }
                return result;
            }

            public T New(Func<GenericRuntimeTypeDefinition<T>, ConstructorDefinition<T>> constructorResolver)
            {
                var constructor = constructorResolver(this);
                return constructor.Invoke();
            }

            public T New(Func<GenericRuntimeTypeDefinition<T>, ConstructorDefinition<T>> constructorResolver, params object[] args)
            {
                var constructor = constructorResolver(this);
                return constructor.Invoke(args);
            }
            public T New(params object[] args)
            {

                if (args == null || args.Length == 0)
                {
                    return New();
                }
                var constructor = TypeInfo.ResolveConstructor(args);
                return (constructor is null) ? throw new InvalidOperationException() : constructor.Invoke(args);
            }
        }
    }
}
