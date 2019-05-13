using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Runtime.ConversionServices
{
    //TODO: Spike, naming convention. [GenericRuntime]TypeInfo, TypeDefintion and Type
    //      Some methods don't make sense or are not intuitive when being called against TypeInfo or TypeDefinition.
    //      It looks like Microsoft's default conventions are a quagmire of attempting to fix/mask previous
    //      defects and/or offer new functionality by giving existing types similar names 
    //      (Generic)(Runtime|RT)(Type|Type)(Info|Handle), EG Type,RuntimeType,RTInfo,TypeInfo,etc...
    //      there are differences but others are mostly reguritations of previos types with new functionality.
    //      Obviously, this design was probably by choice backward/forward compatibly, cross-platform, cross-framework
    //      compatibility but let's define conventions to unmuddy the waters.
    //GenericRuntimeTypeInfoFields
    public partial class GenericRuntimeTypeInfo<T> : IGenericRuntimeTypeInfo
    {
        public List<InstanceFieldDefinition<T>> InstanceFields { get; private set; }
        public List<StaticFieldDefinition<T>> StaticFields { get; private set; }
        List<IInstanceFieldDefinition> IGenericRuntimeTypeInfo.InstanceFields => InstanceFields.Cast<IInstanceFieldDefinition>().ToList();
        List<IStaticFieldDefinition> IGenericRuntimeTypeInfo.StaticFields => StaticFields.Cast<IStaticFieldDefinition>().ToList();
        private void LoadFields()
        {

            InstanceFields = new List<InstanceFieldDefinition<T>>();
            StaticFields = new List<StaticFieldDefinition<T>>();

            var instanceFields = GenericArgumentType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (instanceFields.Length > 0)
            {
                InstanceFields.AddRange(instanceFields.Select(x => new InstanceFieldDefinition<T>(x)));
            }

            var staticFields = GenericArgumentType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (staticFields.Length > 0)
                StaticFields.AddRange(staticFields.Select(x => new StaticFieldDefinition<T>(x)));
        }
    }
    public interface IFieldDefinition
    {
        FieldInfo FieldInfo { get; }
    }
    public interface IStaticFieldDefinition : IFieldDefinition
    {
        IRuntimeTypedReference Get { get; }
        void Set(IRuntimeTypedReference value);
    }
    public interface IInstanceFieldDefinition : IFieldDefinition
    {
        IRuntimeTypedReference Get(IRuntimeTypedReference instance);
        void Set(IRuntimeTypedReference instance, IRuntimeTypedReference value);
        void Set<T>(IRuntimeTypedReference instance, T value);
    }
    //TODO: should we only expose instance fields as an instance property to allow
    //      the same  {get;set} behavior as static fields?

    //TODO: should we have <TInstanceType, TFieldType>? 

    public class FieldDefinition<T> : IFieldDefinition
    {
        public FieldInfo FieldInfo { get; protected set; }
    }

    public class InstanceFieldDefinition<T> : FieldDefinition<T>, IInstanceFieldDefinition
    {

        public InstanceFieldDefinition(FieldInfo x)
        {
            this.FieldInfo = x;
        }


        public TOut Get<TOut>(IRuntimeTypedReference instance)
             //=> FieldInfo.GetValue(instance.BoxedValue).ToTypedReference(FieldInfo.FieldType);
             => (TOut)instance.Generic.GetField(this).To(FieldInfo.FieldType);

        //TODO: Eliminate reflection. We still have calls to old APIs generating generic types on the fly
        // implement Get<T> in IGenericStruct Generic
        public IRuntimeTypedReference Get(IRuntimeTypedReference instance)
                //=> FieldInfo.GetValue(instance.BoxedValue).ToTypedReference(FieldInfo.FieldType);
                => instance.Generic.GetField(this);

        public void Set(IRuntimeTypedReference instance, IRuntimeTypedReference value)
            => FieldInfo.SetValue((T)instance.BoxedValue, value.To(FieldInfo.FieldType));

        public void Set<TIn>(IRuntimeTypedReference instance, TIn value)
          => FieldInfo.SetValue((T)instance.BoxedValue, value.To(FieldInfo.FieldType));
        public void Set<TIn>(T instance, TIn value)
            => FieldInfo.SetValue(instance, value.To(FieldInfo.FieldType));
    }
    public class StaticFieldDefinition<T> : FieldDefinition<T>, IStaticFieldDefinition
    {
        public StaticFieldDefinition(FieldInfo x)
        {
            this.FieldInfo = x;
        }

        public IRuntimeTypedReference Get => FieldInfo.GetValue(null).ToTypedReference();

        public void Set(IRuntimeTypedReference value) => FieldInfo.SetValue(null, value.To(FieldInfo.FieldType));
    }

}
