namespace System.Runtime.RuntimeServices
{
    public partial class GenericRuntimeTypeInfo<T> : IGenericRuntimeTypeInfo
    {
        private GenericRuntimeTypeDefinition<T> typeDefinition;
        private RuntimeTypedReference<T> typedReference => typeDefinition.TypedReference;
        private Type GenericArgumentType => typedReference.GenericArgumentType;
        public GenericRuntimeTypeInfo(GenericRuntimeTypeDefinition<T> typeDefinition)
        {
            this.typeDefinition = typeDefinition;
            loadConstructors();
            LoadFields();
        }
    }

}
