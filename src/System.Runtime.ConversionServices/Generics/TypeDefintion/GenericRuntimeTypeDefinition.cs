using System;
using System.Collections.Generic;
using System.Reflection;


namespace System.Runtime.ConversionServices
{

    public class GenericRuntimeTypeDefinition<T> : IGenericRuntimeTypeDefinition
    {
        public RuntimeTypedReference<T> TypedReference = new RuntimeTypedReference<T>();
        public GenericRuntimeTypeInfo<T> TypeInfo => new GenericRuntimeTypeInfo<T>(this);

        IGenericRuntimeTypeInfo IGenericRuntimeTypeDefinition.IGenericRuntimeTypeInfo => TypeInfo;
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
                //TODO: NullReference check here? Swallow any exception and return null? 
                //TODO: Overload to return null if an exception occurs?
                var ctor = TypeInfo.DefaultConstuctor;
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

        //public InstanceFieldDefinition<T> Field(Func<GenericRuntimeTypeDefinition<T>, InstanceFieldDefinition<T>> fieldResolver)
        //{
        //    var field = fieldResolver(this);
        //    return field;
        //}
        public TOut Field<TOut>(T instance, Func<T, TOut> fieldResolver)
        {

            var def = fieldResolver(instance);
            return def;
            //return def.Get(instance.ToTypedReference());
        }
    }

}
