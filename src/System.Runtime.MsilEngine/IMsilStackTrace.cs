namespace System.Runtime.MsilEngine
{
    public interface IMsilStackTrace
    {
        void Pop(object result);
        void Push(Array value);
        void Push(object value);
        void Trace(string value);
    }
}