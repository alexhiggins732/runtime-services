using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Runtime.MsilEngine
{
    public class MsilStackCall : IMsilStackCall
    {
        List<string> operations;
        public MsilStackCall()
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
}
