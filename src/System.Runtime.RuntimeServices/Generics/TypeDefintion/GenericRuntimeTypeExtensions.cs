using System.Reflection;

namespace System.Runtime.RuntimeServices
{
    public static class GenericRuntimeTypeExtensions
    {
        //TODO: Creating a generic defintion semi-conflicts with 
        //      TypedReferenceExtensions.FromType. That method also cache IRuntimeTypedReference<T>
        //      where this method creates a generic type and then invokes the constructor
        //      on each call.
        public static IGenericRuntimeTypeDefinition RuntimeTypeDefintion(this Type t)
        {
            var genericType = typeof(GenericRuntimeTypeDefinition<>).MakeGenericType(t);
            var result = (IGenericRuntimeTypeDefinition)Activator.CreateInstance(genericType);
            return result;
        }
        public static IRuntimeTypedReference New(this Type t) => t.RuntimeTypeDefintion().New();
        public static IRuntimeTypedReference New(this Type t, params object[] args) => t.RuntimeTypeDefintion().New(args);

        public static IRuntimeTypedReference New(this FieldInfo field)
                => field.FromFieldInfo();

    }

}
