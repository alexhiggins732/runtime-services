using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Runtime.RuntimeServices
{
   
    public interface IDefault { object Box(object value); }
    public struct Generic<T> : IDefault
    {
        public static Func<T> Default => OperandFactory.CreateNullary<T>(OperandType.Default);
        public static object Box(object value) => Default();
        object IDefault.Box(object value) => Box(value);
    }
    public struct FuncFactory
    {
        public static IFunc CreateDefaultFunc(Type type) => TypedReferenceFactory.GetDefaultFunc(type);
        public static List<IFunc> GetDefaultFuncs(IEnumerable<Type> Types)
            => Types.Select(x => CreateDefaultFunc(x)).ToList();
    }




}
