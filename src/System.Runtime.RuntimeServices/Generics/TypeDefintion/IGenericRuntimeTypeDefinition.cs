namespace System.Runtime.RuntimeServices
{
    public interface IGenericRuntimeTypeDefinition
    {
        IRuntimeTypedReference New();
        IRuntimeTypedReference New(params object[] args);
        IGenericRuntimeTypeInfo IGenericRuntimeTypeInfo { get; }
    }

}
