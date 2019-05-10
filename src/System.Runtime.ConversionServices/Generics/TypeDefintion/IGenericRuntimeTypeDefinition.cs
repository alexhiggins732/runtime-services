namespace System.Runtime.ConversionServices
{
    public interface IGenericRuntimeTypeDefinition
    {
        IRuntimeTypedReference New();
        IRuntimeTypedReference New(params object[] args);
        IGenericRuntimeTypeInfo IGenericRuntimeTypeInfo { get; }
    }

}
