namespace System.Runtime.RuntimeServices
{
    public interface IConstructorDefinition
    {
        IRuntimeTypedReference Invoke(params object[] args);
        Type[] ArgumentTypes { get; }
    }
}
