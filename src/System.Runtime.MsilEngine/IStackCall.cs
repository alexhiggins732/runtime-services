namespace System.Runtime.MsilEngine
{
    public interface IMsilStackCall
    {
        void Add(string expression);
        void Clear();
        string ToString();
    }
}