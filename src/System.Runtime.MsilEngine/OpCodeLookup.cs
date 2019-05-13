using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace System.Runtime.MsilEngine
{

    /// <summary>
    /// Utility class for performing <see cref="OpCode"/> lookups by their <see cref="OpCode.Value"/>.
    /// </summary>
    public class OpCodeLookup
    {

        private static Dictionary<int, OpCode> opCodeLookup;
        private static Dictionary<int, string> opCodeNameLookup;
        /// <summary>
        /// A dictionary of <see cref="OpCode"/> indexed by <see cref="OpCode.Value"/>
        /// </summary>
        public static Dictionary<int, OpCode> OpCodes => opCodeLookup ??
            (opCodeLookup = (
            typeof(OpCodes).GetFields(BindingFlags.Public | BindingFlags.Static)
               .Select(x => (OpCode)x.GetValue(null))
               .ToDictionary(x => (int)x.Value, x => x)));

        /// <summary>
        /// A dictionary of names defined in <see cref="System.Reflection.Emit.OpCodes"/> indexed by <see cref="OpCode.Value"/>
        /// </summary>
        public static Dictionary<int, string> OpCodeNames => (opCodeNameLookup = (
            typeof(OpCodes).GetFields(BindingFlags.Public | BindingFlags.Static)
               .ToDictionary(x => (int)(((OpCode)x.GetValue(null)).Value), x => x.Name)));
    }
}
