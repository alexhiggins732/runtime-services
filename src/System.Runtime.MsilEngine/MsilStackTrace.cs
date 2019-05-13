using System.Collections.Generic;
using System.Reflection.Emit;

namespace System.Runtime.MsilEngine
{
    public class MsilStackTrace : IMsilStackTrace
    {
       
        private MsilStackCall current = null;
        public List<string> Calls;
        public MsilStackTrace()
        {
            current = new MsilStackCall();
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
}
