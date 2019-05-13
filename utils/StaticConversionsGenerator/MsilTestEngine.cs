using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.RuntimeServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.RuntimeServices.Conversions;
namespace StaticConversionsGenerator
{
    public class MsilTestEngine
    {
        public interface IMsilStack
        {
            void Push(object obj);
            object Pop();
            object Peek();
            int Count { get; }

            void TraceEnter(OpCode current);
            void TraceExit();
            void Trace(int arg);
            void Trace(string value);
        }
        public class StackCall
        {
            List<string> operations;
            public StackCall()
            {
                operations = new List<string>();
            }
            public void Add(string expression)
            {
                operations.Add(expression);
            }
            public override string ToString()
            {
                return string.Join(" ", operations);
            }
            public void Clear() => operations.Clear();
        }
        public class MsilStackTrace
        {
            //private Stack<object> stack;
            private StackCall current = null;
            public List<string> Calls;
            public MsilStackTrace()
            {
                current = new StackCall();
                Calls = new List<string>();
            }
            public void Pop(object result)
            {
                if (result is Array array)
                    current.Add($"\r\n Stack.Pop() = ({array.GetType().GetElementType().FullName}[{array.Length}])");
                else if (result is System.RuntimeFieldHandle fieldhandle)
                    current.Add($"\r\n Stack.Pop() = {fieldhandle}({fieldhandle.Value})");
                else if (result is IConvertible convertible)
                    current.Add($"\r\n Stack.Pop() = {result}");
                else
                    current.Add($"\r\n Stack.Pop() = {result}");
            }
            public void Push(Array value)
            {
                var type = value.GetType().GetElementType();
                current.Add($"\r\n Stack.Push({type.FullName}[{value.Length}])");
                //stack.Push(value);
            }
            public void Push(object value)
            {
                if (value is Array array)
                    Push(array);
                else if (value is System.RuntimeFieldHandle fieldhandle)
                    current.Add($"\r\n Stack.Push({fieldhandle}({fieldhandle.Value})");
                else if (value is IConvertible convertible)
                    current.Add($"\r\n Stack.Push({value})");
                else
                    current.Add($"\r\n Stack.Push({value})");
                //stack.Push(value);
            }
            public void Trace(string value)
            {
                current.Add(value);
                //stack.Push(value);
            }

            internal void Enter(OpCode opCode)
            {
                current.Add($"{opCode}");
            }

            internal void Exit()
            {
                Calls.Add(current.ToString());
                current.Clear();
            }
        }
        public class MsilStack : IMsilStack
        {
            private Stack<object> stack;
            private MsilStackTrace stackTrace;
            public MsilStack()
            {
                stack = new Stack<object>();
                stackTrace = new MsilStackTrace();
            }
            public int Count => stack.Count;

            public object Peek() => stack.Peek();


            public object Pop()
            {
                var result = stack.Pop();
                stackTrace.Pop(result);
                return result;
            }

            public void Push(object obj)
            {
                stackTrace.Push(obj);
                stack.Push(obj);
            }

            public void Trace(int arg)
            {
                stackTrace.Trace($" {arg}");
            }

            public void Trace(string value)
            {
                stackTrace.Trace(value);
            }

            public void TraceEnter(OpCode current)
            {
                stackTrace.Enter(current);
            }
            public void TraceExit()
            {
                stackTrace.Exit();
            }
        }

        public int[] MsilTestMethod()
        {
            var l = new[] { 1, 2, 3, 4 };
            var ag = l.Length;
            ag -= 1;
            for (; ag > -1; l[ag--] = 0) ;
            return l;
        }

        public static void TestMethodInfoIlBytes()
        {
            var meth = typeof(MsilTestEngine).GetMethod(nameof(MsilTestMethod));
            var il = meth.GetMethodBody().GetILAsByteArray();

            ExecuteIl(il, meth, meth.DeclaringType.Module);
        }

        public static string getOpCodeName(OpCode op)
        {
            var name = $"{op}".Replace(".", "_").Trim('_');
            var parts = name.Split("_".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return string.Join("_", parts.Select(x => x.Length > 1 ? x.Substring(0, 1).ToUpper() + x.Substring(1) : x));
        }
        public static object ExecuteIl(byte[] msil, object target, Module module, Dictionary<int, object> callArgs = null)
        {
            var opCodeLookup = OpCodeHelper.GetOpCodes();
            //Stack<object> stack = new Stack<object>();
            IMsilStack stack = new MsilStack();
            Dictionary<int, object> locals = new Dictionary<int, object>();

            bool test = callArgs is null;
            if (test) callArgs = new Dictionary<int, object>();
            var stream = new MemoryStream(msil);
            var br = new BinaryReader(stream);

            int pos = -1;
            int opcode = 0;
            Array array = null;
            int arg;
            int arg2;
            Type typeArg;
            MethodInfo method;
            ConstructorInfo constructor;
            OpCode current = OpCodes.Nop;
            object[] methodArgs = null;
            object obj = null;
            object obj2 = null;
            ParameterInfo[] methodParameters = null;
            object methodTarget = null;
            goto Next;

        Jump:
            stack.TraceExit();
            pos = (int)stream.Position;
        Next:

            opcode = stream.ReadByte();
            if (opcode == 254)
            {
                opcode <<= 8;
                opcode += stream.ReadByte();
                opcode = unchecked((short)opcode);
            }
            current = opCodeLookup[opcode];
            stack.TraceEnter(current);

            switch (opcode)
            {
                case 0: //nop
                    goto Read;
                case 6: //ld.loc.0
                case 7: //ld.loc.1
                case 8: //ld.loc.2
                case 9: //ld.loc.3
                    arg = opcode - 6;
                    obj = locals[arg];
                    stack.Trace(arg);
                    stack.Push(obj); goto Read;
                case 10: //stloc.0
                case 11: //stloc.1
                case 12: //stloc.2
                case 13: //stloc.3
                    arg = arg = opcode - 10;
                    stack.Trace(arg);
                    obj = stack.Pop();
                    locals[arg] = obj; goto Read;
                case 17: //ldloc.s
                    arg = arg = stream.ReadByte();
                    stack.Trace(arg);
                    obj = locals[arg];
                    stack.Push(obj); goto Read;
                case 19: //stloc.s
                    arg = stream.ReadByte();
                    stack.Trace(arg);
                    obj = stack.Pop();
                    locals[arg] = obj; goto Read;
                //case 21:// ldc.i4.m1
                case 21: stack.Push(-1); goto Read;
                case 22: // ldc.i4.0
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                    arg = opcode - 22;
                    stack.Trace(arg);
                    stack.Push(arg); goto Read;
                case 37: //dup
                    stack.Push(obj = stack.Peek()); goto Read;
                case 38: //pop
                    stack.Pop(); goto Read;
                case 42: //42 ret
                    goto Ret;
                case 43: //43 br.s
                    arg = (sbyte)stream.ReadByte();
                    stack.Trace(arg);
                    stream.Seek(arg, SeekOrigin.Current);
                    goto Jump;
                case 45://brtrue.s
                    arg = (sbyte)br.ReadByte();
                    stack.Trace(arg);
                    test = (bool)stack.Pop();
                    if (test) { stream.Seek(arg, SeekOrigin.Current); goto Jump; }
                    goto Read;
                case 89: //Value = 89
                    obj = stack.Pop();
                    obj2 = stack.Pop();
                    obj = ((dynamic)obj2) - ((dynamic)obj);
                    stack.Push(obj);
                    goto Read;
                case 105://conv.i4
                    obj = stack.Pop();
                    obj = obj.To<int>();
                    stack.Push(obj);
                    goto Read;
                case 40://call
                case 111: //callvirt
                    arg = br.ReadInt32();
                    stack.Trace(arg);
                    method = (MethodInfo)module.ResolveMethod(arg);
                    methodParameters = method.GetParameters();
                    methodArgs = new object[methodParameters.Length];
                    //for (arg = methodArgs.Length - 1; arg > -1; arg--) methodArgs[arg] = stack.Pop();//.To(methodParameters[arg].ParameterType);   

                    //cleanup MSIL generated locals;
                    arg = methodArgs.Length;
                    arg -= 1;
                    for (; test = arg > -1; methodArgs[arg--] = stack.Pop()) ;

                    methodTarget = (test = method.IsStatic) ? null : stack.Pop();
                    stack.Trace(method.ToString() + "\r\n");
                    obj = method.Invoke(methodTarget, methodArgs);
                    if (test = method.ReturnType != typeof(void))
                        stack.Push(obj.To(method.ReturnType));
                    goto Read;
                case 114: //ldstr
                    arg = br.ReadInt32();
                    stack.Trace(arg);
                    obj = module.ResolveString(arg);
                    stack.Push(obj);
                    goto Read;
                case 115: //new obj
                    arg = br.ReadInt32();
                    stack.Trace(arg);
                    obj = module.ResolveMethod(arg);
                    constructor = (ConstructorInfo)obj;
                    methodParameters = constructor.GetParameters();
                    arg = methodParameters.Length;
                    methodArgs = new object[arg];

                    // loop is creating to many additional locals in compiled MSIL code.
                    //for (arg = methodArgs.Length - 1; arg > -1; arg--) methodArgs[arg] = stack.Pop();//.To(methodParameters[arg].ParameterType);               
                    arg = methodArgs.Length;
                    arg -= 1;
                    arg = methodArgs.Length - 1;
                    for (; test = arg > 0; methodArgs[arg--] = stack.Pop()) ;
                    arg2 = arg - 1;
                    while (arg > arg2)
                    {
                        obj = stack.Pop();
                        methodArgs[arg] = obj;
                        arg -= 1;
                    }

                    obj = constructor.Invoke(methodArgs);
                    stack.Push(obj);
                    goto Read;
                case 116: //cast class
                    arg = br.ReadInt32();
                    stack.Trace(arg);
                    typeArg = module.ResolveType(arg);
                    obj = stack.Pop();
                    obj = obj.To(typeArg);
                    stack.Push(obj);
                    goto Read;
                case 140: /*box*/  //-> nothing need, value on stack is already object arg = br.ReadInt32(); ; var top = stack.Peek();
                    arg = br.ReadInt32();
                    stack.Trace(arg);
                    typeArg = module.ResolveType(arg);
                    obj = (object)stack.Pop();
                    obj = obj.To(typeArg);
                    stack.Push(obj);
                    goto Read;
                case 141: //new array
                    arg = br.ReadInt32();
                    stack.Trace(arg);
                    typeArg = module.ResolveType(arg);
                    arg = (int)stack.Pop();
                    obj = Array.CreateInstance(typeArg, arg);
                    stack.Push(obj);
                    goto Read;
                case 142: //ldlen:
                    array = (Array)stack.Pop();
                    arg = array.Length;
                    stack.Push(arg);
                    goto Read;
                case 154: //ldelem.ref
                    arg = (int)stack.Pop();
                    obj = stack.Pop();
                    array = (Array)obj;
                    obj = array.GetValue(arg);
                    stack.Push(obj);
                    goto Read;
                case 158: // stelem.i4
                    obj = stack.Pop();
                    arg = (int)stack.Pop();
                    array = (Array)stack.Pop();
                    array.SetValue(obj.To<int>(), arg);
                    goto Read;
                case 162: //stelem.ref
                    obj = stack.Pop();
                    arg = (int)stack.Pop();
                    obj = stack.Pop();
                    array = (Array)obj;
                    stack.Trace($"[{arg}] = {obj}");
                    array.SetValue(obj, arg);
                    goto Read;
                case 165: //unbox any
                    arg = br.ReadInt32();
                    stack.Trace(arg);
                    typeArg = module.ResolveType(arg);
                    obj = stack.Pop();
                    obj = obj.To(typeArg);
                    stack.Push(obj);
                    goto Read;
                case 208: //ldtoken
                    arg = br.ReadInt32();
                    stack.Trace(arg);
                    var member = module.ResolveMember(arg);
                    //typeArg = (Type)module.ResolveMember(arg);
                    if (member is FieldInfo fieldInfo)
                        stack.Push(fieldInfo.FieldHandle);
                    else if (member is Type typeInfo)
                        stack.Push(typeInfo.TypeHandle);
                    else if (member is MethodInfo methodInfo)
                        stack.Push(methodInfo.MethodHandle);
                    else throw new NotImplementedException();
                    goto Read;
                case -510: //cgt
                    obj2 = stack.Pop();
                    obj = stack.Pop();
                    test = ((dynamic)obj) > ((dynamic)obj2);
                    stack.Push(test);
                    goto Read;
                default:
                    var notImplemented = current;
                    throw new NotImplementedException();
            }

        Read:
            stack.TraceExit();
            pos++;
            if (test = pos < msil.Length)
                goto Next;
            Ret:
            stack.TraceExit();
            var result = stack.Count == 0 ? null : stack.Pop();
            return result;
        }
    }
}
