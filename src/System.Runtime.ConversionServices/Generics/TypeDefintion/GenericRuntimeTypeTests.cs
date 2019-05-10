using System.Linq;


namespace System.Runtime.ConversionServices
{
    public class GenericRuntimeTypeTests
    {
        //TODO: Cleanup interface. Expose limited number of top level types
        //TODO: unify fluent api issues into single place and only put GenericTimeType specific tests/usage here.
        public static void ExplicitInterfaceExtenstionTests()
        {
            //TODO: Did we lose interface cleanup?
            var newFromType = typeof(TestClass).New();
            1.ToTypedReference();
            //TODO: Remove need to cast arge to runtime typed reference
            //TODO: Expose only via Arithmetic interface.
            //newFromType.Add(1.ToTypedReference());
            // newFromType.Add<int>(1.ToTypedReference());
            newFromType.Arithmetic();// <- this is okay I think.
            newFromType.Arithmetic().Add(1);
            newFromType.Arithmetic().Add<long>(2);
            //TODO: Nested and renamed (maybe op binary)
            //newFromType.BinaryCall(1.ToTypedReference(), OperatorType.Add);
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
            var infNewDefault = defInterface.IGenericRuntimeTypeInfo.DefaultConstructor.Invoke();

            var newFromType = testClassType.New();
            var newFromTypeWithArgs = testClassType.New("hello", 1);

            //a bit wonky drilling to deep.
            var fields =
                infNew
                .Generic
                .IGenericRuntimeTypeDefinition
                .IGenericRuntimeTypeInfo
                .InstanceFields;

            infNew
               .Generic
               .IGenericRuntimeTypeDefinition
               .IGenericRuntimeTypeInfo
               .InstanceFields.First().Set(infNew, 1.ToTypedReference());

            var firstField =
                fields
                .First();
            var firstFieldvalue
                = firstField
                .Get(infNew);//TODO: should the instance be passed to Fields/Methods/Properties constructors?
            firstField.Set(infNew, 1);//Need to fix this.

            int a = ~1;

            //TODO: Implement. Decide on nest.

            //Two considerations. 1) compile time API. 2 Runtime api.


            //Direct api is much cleaner, but to be fair it goes directly to 
            // infNew
            //   .Generic
            //   .IGenericRuntimeTypeDefinition
            var def = new GenericRuntimeTypeDefinition<TestClass>();

            var newTestDefault = def.New();

            def.TypeInfo.InstanceFields.First().Set(newTestDefault, 1);

            //vs 


            //TODO: Do we need a Generic.TypeDef.TypeInfo walk?
            //      TypeDef and TypeInfo can be merged.
            //      Fill out TypeDef/TypeInfo interfaces and 
            //      evaluate option of merge the interface api directly into Generic.
            //      or as even as a direct sibling to generic.

            // VS offers direct access to members(properties, fields, methods) with a single.
            //  and even this can be painful.
            var example = (new object[] {
                newTestDefault.ToTypedReference(),
                 newTestDefault.SomeIntMethod(),
                 newTestDefault.IntProperty,
                 newTestDefault.IntField
            });
            //This would be suberb!
            /*
            With<TestClass>.New()
               .select(x => new[] { ToTypedReference(), SomeIntMethod(), IntProperty, IntField});
           
            With(newTestDefault)
                .Do( () => { IntProperty =1, IntField =2, Call(SomeMethod()) } );     

            With(newTestDefault)
                .select(x => new[] { ToTypedReference(), SomeIntMethod(), IntProperty, IntField });
              
             */
            /*
                infNew
                    .Generic
                    .IGenericRuntimeTypeDefinition
                    .IGenericRuntimeTypeInfo
                    .InstanceFields.First().Set(infNew, 1.ToTypedReference());
                 // can we expost the same api?
                infNew
                    .Generic
                    .TypeDef
                    .TypeInfo
                    .Fields.First().Set(infNew, 1.ToTypedReference());
             */

            var defaultCtor = def.TypeInfo.DefaultConstuctor;
            //TODO: verify interface exposes needed methods IGenericRuntimeTypeDefinition
            var typedRef = def.TypedReference;






            //newTestDefault.

            //TODO: Cleanup Extensions on strongly typed refrerences.
            //newTestDefault.Compare

            var newTestPublicFirst = def.New(x => x.TypeInfo.PublicConstructors.First());
            var newTestPublicFirstFromTypeInfo = def.New(x => x.TypeInfo.PublicConstructors.First());


            var newTestPublicSecond = def.New(x => x.TypeInfo.PublicConstructors[1], "hello");
            var newTestPrivateFirst = def.New(x => x.TypeInfo.PrivateConstructors.First(), "hello", 1);
            var newTestPrivateSecond = def.New(x => x.TypeInfo.PrivateConstructors[1], 1);

            var newTestResolved = def.New("hello", 1);

            RuntimeTypedReference<TestClass>.GenericType.New();

            var newFieldTest = def.Field(newTestDefault, (x) => x.IntField);

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



        //TODO: Compile the constructor and invoke via
        //  ConstructorCall<Type,ConstructorArgs>.Invoke
        //public struct ConstructorCall<TType, TConstructorArgs>
        //{
        //    public static Func<TConstructorArgs, TType> Ctor;
        //}
        //public struct MethodArgs<T>
        //{

        //}

    }

}
