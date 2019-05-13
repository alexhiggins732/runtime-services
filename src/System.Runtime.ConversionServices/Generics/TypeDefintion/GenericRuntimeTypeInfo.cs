namespace System.Runtime.ConversionServices
{
    public partial class GenericRuntimeTypeInfo<T> : IGenericRuntimeTypeInfo
    {
        private GenericRuntimeTypeDefinition<T> typeDefinition;
        private RuntimeTypedReference<T> TypedReference => typeDefinition.TypedReference;
        private Type GenericArgumentType => TypedReference.GenericArgumentType;
        public GenericRuntimeTypeInfo(GenericRuntimeTypeDefinition<T> typeDefinition)
        {
            this.typeDefinition = typeDefinition;
            LoadConstructors();
            LoadFields();
        }
    }

}
