namespace System.Runtime.ConversionServices
{
    public interface IConstructorDefinition
    {
        IRuntimeTypedReference Invoke(params object[] args);
        Type[] ArgumentTypes { get; }
    }
}
