using System.Collections.Generic;
using System.Reflection.Emit;

namespace System.Runtime.MsilEngine
{
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
}
