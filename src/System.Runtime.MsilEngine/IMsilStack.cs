using System.Reflection.Emit;

namespace System.Runtime.MsilEngine
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
}
