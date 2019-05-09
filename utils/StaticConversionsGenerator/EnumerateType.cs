using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StaticConversionsGenerator
{
    public class EnumeratedTypeTest
    {
        public static void Run()
        {
            var aFromEnum =(new[] { TypeCode.Boolean, TypeCode.Byte }).
                Select(x => new { x })
                .ToList();
            var firstType = aFromEnum.First().GetType();
            var secondType = aFromEnum.Last().GetType();
            var anonsAreEqual = firstType == secondType;
            var AreEqaul = aFromEnum.First() == aFromEnum.Last();

            var aFromObject= (new object[] { true, 1 }).
                Select(x => new { x })
                .ToList();
            var firstTypeO = aFromObject.First().GetType();
            var secondTypeO = aFromObject.Last().GetType();
            var anonsAreEqualO = firstTypeO == secondTypeO;
            var AreEqualO = aFromObject.First() == aFromObject.Last();

            var firstQueryEqSecondQuery = aFromObject.First().GetType() == aFromEnum.Last().GetType();

            var aFromObject1 = (new object[] { true, 1 }).
                Select(x => new { x })
                .ToList();
            var firstTypeO1 = aFromObject1.First().GetType();
            var secondTypeO1= aFromObject1.Last().GetType();
            var anonsAreEqualO1 = firstTypeO1 == secondTypeO1;
            var AreEqualO1 = aFromObject1.First() == aFromObject1.Last();

            var aFromObjectEqaFromObject1 = aFromObject1.First() == aFromObject1.First();

            var typeCodes = new[] { firstType.GUID, firstTypeO.GUID, firstTypeO1.GUID };
            var guidesEq = typeCodes.Distinct();
            var metaTokens = new[] { firstType.MetadataToken, firstTypeO.MetadataToken, firstTypeO1.MetadataToken };
            var metaDistinct = metaTokens.Distinct().ToList();
            var types = new[] { firstType, firstTypeO, firstTypeO1 };
            var typesDistinct = types.Distinct().ToList();
            types.DefaultIfEmpty().ToList();


            var t = new EnumerateType<EnumerateType>();
            var t2 = new EnumerateType("0");
            var t3 = new EnumerateType("1");
            var t2g = typeof(EnumerateType<>).MakeGenericType(t2);
            var t3g = typeof(EnumerateType<>).MakeGenericType(t3);
            var eq = t2g == t3g;
            var eq2 = t.GetType() == t2g;

            EnumerateType<EnumerateType>.DoSomething = () => Console.WriteLine("EnumerateType<EnumerateType>");
            EnumerateType<EnumerateType>.DoSomething();

            var f = t2g.GetField("DoSomething");
            var fieldAction = (Action)f.GetValue(null);
            fieldAction();

        }
    }

    public struct EnumerateType<T>
    {
        public static Action DoSomething = () => Console.WriteLine(typeof(T).Name);
    }
    public class EnumerateType : Type
    {
        private string name;
        public EnumerateType(string name)
        {
            this.name = name;
        }
        public override string Name => name;

        public override IEnumerable<CustomAttributeData> CustomAttributes => base.CustomAttributes;

        public override int MetadataToken => base.MetadataToken;

        //public override Module Module => base.Module;

        public override Module Module => throw new NotImplementedException();

        public override MemberTypes MemberType => base.MemberType;

        public override Type DeclaringType => base.DeclaringType;

        public override MethodBase DeclaringMethod => base.DeclaringMethod;

        public override Type ReflectedType => base.ReflectedType;

        public override StructLayoutAttribute StructLayoutAttribute => base.StructLayoutAttribute;

        public override Guid GUID => throw new NotImplementedException();

        public override Assembly Assembly => throw new NotImplementedException();

        public override RuntimeTypeHandle TypeHandle => base.TypeHandle;

        public override string FullName => throw new NotImplementedException();

        public override string Namespace => throw new NotImplementedException();

        public override string AssemblyQualifiedName => throw new NotImplementedException();

        public override Type BaseType => throw new NotImplementedException();

        public override GenericParameterAttributes GenericParameterAttributes => base.GenericParameterAttributes;

        public override bool IsEnum => base.IsEnum;

        public override bool IsSerializable => base.IsSerializable;

        public override bool IsGenericType => base.IsGenericType;

        public override bool IsGenericTypeDefinition => base.IsGenericTypeDefinition;

        public override bool IsConstructedGenericType => base.IsConstructedGenericType;

        public override bool IsGenericParameter => base.IsGenericParameter;

        public override int GenericParameterPosition => base.GenericParameterPosition;

        public override bool ContainsGenericParameters => base.ContainsGenericParameters;

        public override Type[] GenericTypeArguments => base.GenericTypeArguments;

        public override bool IsSecurityCritical => base.IsSecurityCritical;

        public override bool IsSecuritySafeCritical => base.IsSecuritySafeCritical;

        public override bool IsSecurityTransparent => base.IsSecurityTransparent;

        public override Type UnderlyingSystemType => typeof(Type);

        public override bool Equals(object o)
        {
            return base.Equals(o);
        }

        public override bool Equals(Type o)
        {
            return base.Equals(o);
        }

        public override Type[] FindInterfaces(TypeFilter filter, object filterCriteria)
        {
            return base.FindInterfaces(filter, filterCriteria);
        }

        public override MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria)
        {
            return base.FindMembers(memberType, bindingAttr, filter, filterCriteria);
        }

        public override int GetArrayRank()
        {
            return base.GetArrayRank();
        }

        public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        public override IList<CustomAttributeData> GetCustomAttributesData()
        {
            return base.GetCustomAttributesData();
        }

        public override MemberInfo[] GetDefaultMembers()
        {
            return base.GetDefaultMembers();
        }

        public override Type GetElementType()
        {
            throw new NotImplementedException();
        }

        public override string GetEnumName(object value)
        {
            return base.GetEnumName(value);
        }

        public override string[] GetEnumNames()
        {
            return base.GetEnumNames();
        }

        public override Type GetEnumUnderlyingType()
        {
            return base.GetEnumUnderlyingType();
        }

        public override Array GetEnumValues()
        {
            return base.GetEnumValues();
        }

        public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override EventInfo[] GetEvents()
        {
            return base.GetEvents();
        }

        public override EventInfo[] GetEvents(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override FieldInfo GetField(string name, BindingFlags bindingAttr)
        {
            var t = base.GetType();
            var field = t.GetField(name, bindingAttr);
            return field;
            //base.GetField(name, bindingAttr);
        }

        public override FieldInfo[] GetFields(BindingFlags bindingAttr)
        {
            var t = base.GetType();
            var fields = t.GetFields(bindingAttr);
            return fields;
        }

        public override Type[] GetGenericArguments()
        {
            return base.GetGenericArguments();
        }

        public override Type[] GetGenericParameterConstraints()
        {
            return base.GetGenericParameterConstraints();
        }

        public override Type GetGenericTypeDefinition()
        {
            return base.GetGenericTypeDefinition();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override Type GetInterface(string name, bool ignoreCase)
        {
            throw new NotImplementedException();
        }

        public override InterfaceMapping GetInterfaceMap(Type interfaceType)
        {
            return base.GetInterfaceMap(interfaceType);
        }

        public override Type[] GetInterfaces()
        {
            throw new NotImplementedException();
        }

        public override MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
        {
            return base.GetMember(name, bindingAttr);
        }

        public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
        {
            return base.GetMember(name, type, bindingAttr);
        }

        public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override Type GetNestedType(string name, BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override Type[] GetNestedTypes(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
        {
            throw new NotImplementedException();
        }

        public override bool IsAssignableFrom(Type c)
        {
            return base.IsAssignableFrom(c);
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        public override bool IsEnumDefined(object value)
        {
            return base.IsEnumDefined(value);
        }

        public override bool IsEquivalentTo(Type other)
        {
            return base.IsEquivalentTo(other);
        }

        public override bool IsInstanceOfType(object o)
        {
            return base.IsInstanceOfType(o);
        }

        public override bool IsSubclassOf(Type c)
        {
            return base.IsSubclassOf(c);
        }

        public override Type MakeArrayType()
        {
            return base.MakeArrayType();
        }

        public override Type MakeArrayType(int rank)
        {
            return base.MakeArrayType(rank);
        }

        public override Type MakeByRefType()
        {
            return base.MakeByRefType();
        }

        public override Type MakeGenericType(params Type[] typeArguments)
        {
            return base.MakeGenericType(typeArguments);
        }

        public override Type MakePointerType()
        {
            return base.MakePointerType();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override TypeAttributes GetAttributeFlagsImpl()
        {
            throw new NotImplementedException();
        }

        protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        protected override TypeCode GetTypeCodeImpl()
        {
            return base.GetTypeCodeImpl();
        }

        protected override bool HasElementTypeImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsArrayImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsByRefImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsCOMObjectImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsContextfulImpl()
        {
            return base.IsContextfulImpl();
        }

        protected override bool IsMarshalByRefImpl()
        {
            return base.IsMarshalByRefImpl();
        }

        protected override bool IsPointerImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsPrimitiveImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsValueTypeImpl()
        {
            return base.IsValueTypeImpl();
        }
    }
}
