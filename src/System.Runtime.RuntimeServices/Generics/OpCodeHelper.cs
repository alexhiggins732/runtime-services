using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace System.Runtime.RuntimeServices
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
        public static void GenerateOpCodeInterfaces()
        {
            var opCodeLookup = typeof(OpCodes).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(x => (OpCode)x.GetValue(null))
                .ToDictionary(x => (int)x.Value, x => x);

            var opCodeNameLookup = typeof(OpCodes).GetFields(BindingFlags.Public | BindingFlags.Static)
                .ToDictionary(x => x.Name, x => unchecked((ushort)(((OpCode)x.GetValue(null)).Value)));

            var constDefs = opCodeLookup
                .Select(x => $"public Const {$"{$"{x.Value}".Substring(0, 1).ToUpper()}"}{$"{x.Value}".Substring(1).Replace(".", "_").Trim('_')} = {unchecked((ushort)x.Key)};").ToList();
            var constcode = string.Join("\r\n\t", constDefs);


            var constDefs2 = opCodeNameLookup
                .Select(x => $"public const int {x.Key} = {unchecked((ushort)x.Value)}; // 0x{x.Value.ToString("X2")} {(x.Value > 255 ? unchecked((ushort)x.Value).ToString() : "")}").ToList();
            var constcode2 = string.Join("\r\n\t", constDefs2);

            var switchDefs = opCodeNameLookup
                .Select(x => $"case MsilOpCodes.{x.Key}:// 0x{x.Value.ToString("X2")} ({(x.Value > 255 ? $"{x.Value} - {unchecked((short)x.Value)}" : x.Value.ToString())})\r\n\tOpCodeHandler<MsilOpCode.{x.Key}>.Handle(this);\r\n\tbreak;");
            var switchcode = string.Join("\r\n", switchDefs);

        }
    }
}
