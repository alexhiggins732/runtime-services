using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace System.Runtime.ConversionServices
{

    //GenericRuntimeTypeInfoConstructors
    public partial class GenericRuntimeTypeInfo<T> : IGenericRuntimeTypeInfo
    {
        //TODO: Lazy load
        //TODO:  Unify these default CTOR resolution. Cache if checked and don't try second resolve attempt.

        /// <summary>
        /// The default parameterless constructor for the referenced <see cref="IGenericRuntimeTypeInfo"/>
        /// </summary>
        IConstructorDefinition IGenericRuntimeTypeInfo.DefaultConstructor => DefaultConstuctor;

        /// <summary>
        /// The default parameterless constructor for the referenced <see cref="IGenericRuntimeTypeInfo"/>
        /// </summary>
        public ConstructorDefinition<T> DefaultConstuctor => ResolveConstructor();

        /// <summary>
        /// Internal method to load constructors declared on the the specified type.
        /// </summary>
        private void LoadConstructors()
        {
            //TODO: Check for default constructor here once.
            PublicConstructors = new List<PublicConstructorDefinition<T>>();
            PrivateConstructors = new List<PrivateConstructorDefinition<T>>();

            var publicCtors = GenericArgumentType.GetConstructors();
            if (publicCtors.Length > 0)
            {
                PublicConstructors.AddRange(publicCtors.Select(x => new PublicConstructorDefinition<T>(x)));
            }

            var privateCtors = GenericArgumentType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            if (privateCtors.Length > 0)
                PrivateConstructors.AddRange(privateCtors.Select(x => new PrivateConstructorDefinition<T>(x)));
        }

        /// <summary>
        /// A list of public constructors for the referenced typed <typeparamref name="T"/>
        /// </summary>
        public List<PublicConstructorDefinition<T>> PublicConstructors;

        /// <summary>
        /// A list of private constructors for the referenced typed.
        /// </summary>
        public List<PrivateConstructorDefinition<T>> PrivateConstructors;

        List<IPublicConstructorDefinition> IGenericRuntimeTypeInfo.PublicConstructors 
            => PublicConstructors.Cast<IPublicConstructorDefinition>().ToList();

        List<IPrivateConstructorDefinition> IGenericRuntimeTypeInfo.PrivateConstructors
                => PublicConstructors.Cast<IPrivateConstructorDefinition>().ToList();

        /// <summary>
        /// Finds the <typeparamref name="T"/> constructor with the parameter types matching the types in the supplied array of <paramref name="constructorParameters"/>
        /// </summary>
        /// <param name="constructorParameters">The parameters to pass to the <see cref="ConstructorInfo"/></param>
        /// <returns></returns>
        public ConstructorDefinition<T> ResolveConstructor(params object[] constructorParameters)
        {
            //TODO: Cache resolution?
            var argTypes = (constructorParameters ?? new Type[] { })
                .Select(x => x?.GetType() ?? typeof(object)).ToArray();
            var constructor = (ConstructorDefinition<T>)PublicConstructors.FirstOrDefault(x => x.ArgumentTypes.SequenceEqual(argTypes))
             ?? (ConstructorDefinition<T>)PrivateConstructors.FirstOrDefault(x => x.ArgumentTypes.SequenceEqual(argTypes));
            return constructor;
        }
    }

    //TODO: Spike: Do we need Public vs Private?
    //TODO: Determine feasibility of static cache vs introducing garbage collection at the cost of performance.
    public class PublicConstructorDefinition<T> : ConstructorDefinition<T>
    {
        public PublicConstructorDefinition(ConstructorInfo x) : base(x) { }
    }

    public class PrivateConstructorDefinition<T> : ConstructorDefinition<T>
    {
        public PrivateConstructorDefinition(ConstructorInfo x) : base(x) { }
    }

    /// <summary>
    /// A <see cref="ConstructorInfo"/> wrapper for the <see cref="IRuntimeTypedReference"/> to provide
    ///     fluent API services to create new instances of <typeparamref name="T"/> at Runtime.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConstructorDefinition<T> : IConstructorDefinition
    {

        public ConstructorInfo Constructor { get; }
        public ConstructorDefinition(ConstructorInfo constructorInfo) => this.Constructor = constructorInfo;
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

            return (T)Constructor.Invoke(args);
        }


        /// <summary>
        /// Invokes the <see cref="ConstructorInfo"/> with the signature matching the supplied <paramref name="constructorParameters"/>. 
        /// </summary>
        /// <param name="constructorParameters"></param>
        /// <returns></returns>
        IRuntimeTypedReference IConstructorDefinition.Invoke(params object[] constructorParameters)
            => new RuntimeTypedReference<T>(Invoke(constructorParameters));

        /// <summary>
        /// A <see cref="Type"/> array for the parameters passed to <see cref="ConstructorInfo.Invoke(object[])"/>
        /// </summary>
        public Type[] ArgumentTypes => Constructor.GetParameters().Select(x => x.ParameterType).ToArray();
    }
}
