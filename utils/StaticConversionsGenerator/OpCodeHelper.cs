using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace StaticConversionsGenerator
{
    public class OpCodeHelper
    {
        public static Dictionary<int, OpCode> GetOpCodes()
        {
            var opCodeLookup = typeof(OpCodes).GetFields(BindingFlags.Public | BindingFlags.Static)
               .Select(x => (OpCode)x.GetValue(null))
               .ToDictionary(x => (int)x.Value, x => x);
            return opCodeLookup;
        }
    }
}
