using System.Reflection;

namespace System.Runtime.MsilEngine
{
    public class MsilModuleTokenResolver : IMsilTokenResolver
    {
        public Module TokenModule { get; }
        public FieldInfo ResolveField(int metadataToken) => TokenModule.ResolveField(metadataToken);
        public MemberInfo ResolveMember(int metadataToken) => TokenModule.ResolveMember(metadataToken);
        public MethodBase ResolveMethod(int metadataToken) => TokenModule.ResolveMethod(metadataToken);
        public byte[] ResolveSignature(int metadataToken) => TokenModule.ResolveSignature(metadataToken);
        public string ResolveString(int metadataToken) => TokenModule.ResolveString(metadataToken);
        public Type ResolveType(int metadataToken) => TokenModule.ResolveType(metadataToken);

        public MsilModuleTokenResolver(Module module) => this.TokenModule = module;
    }
}
