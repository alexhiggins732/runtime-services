using System.Collections.Generic;

namespace System.Runtime.ConversionServices
{
    public interface IGenericRuntimeTypeInfo
    {
        IConstructorDefinition DefaultConstructor { get; }
        List<IPrivateConstructorDefinition> PrivateConstructors { get; }
        List<IPublicConstructorDefinition> PublicConstructors { get; }
        List<IInstanceFieldDefinition> InstanceFields { get; }
        List<IStaticFieldDefinition> StaticFields { get; }
    }
}
