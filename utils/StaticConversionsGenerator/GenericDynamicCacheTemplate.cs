using System;
using System.Reflection.Emit;

namespace StaticConversionsGenerator
{
    partial class Program
    {
        public static class GenericDynamicCacheTemplate
        {
            public static int GenericMethod<T>(T obj)
            {
                return CachedCompiledMethod<T>.CompiledResult;
            }

            private static class CachedCompiledMethod<T>
            {
                public static readonly int CompiledResult;

                static CachedCompiledMethod()
                {
                    var dm = new DynamicMethod("func", typeof(int),
                                               Type.EmptyTypes, typeof(GenericDynamicCacheTemplate));

                    ILGenerator il = dm.GetILGenerator();
                    il.Emit(OpCodes.Sizeof, typeof(T));
                    il.Emit(OpCodes.Ret);

                    var func = (Func<int>)dm.CreateDelegate(typeof(Func<int>));
                    CompiledResult = func();
                }
            }
        }
    }
}
