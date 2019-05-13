using System.Reflection;

namespace System.Runtime.MsilEngine
{
    public interface IMsilTokenResolver
    {
        Module TokenModule { get; }
        FieldInfo ResolveField(int metadataToken);
        MemberInfo ResolveMember(int metadataToken);
        MethodBase ResolveMethod(int metadataToken);
        byte[] ResolveSignature(int metadataToken);
        string ResolveString(int metadataToken);
        Type ResolveType(int metadataToken);
    }
}