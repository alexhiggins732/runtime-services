using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
namespace System.Runtime.MsilEngine
{
    public class ILOpCodeReader
    {
        public static string getFieldDeclarations(int value, OpCode opCode)
        {
            return "";
        }
        public static void CreateIlOpCodeConstructors()
        {
            var opCodes = OpCodeLookup.OpCodes;
            int opCodeValue;
            OpCode opCode;
            IMsilReader msilReader;
            var opCodeNames = OpCodeLookup.OpCodeNames;

            var ctorStatements = opCodeNames.Select(x => $@"
                case MsilOpCodes.{x.Value}: // 0x{x.Key.ToString("X2")} ({x.Key})
                    return new IlOpCode({nameof(opCodeValue)}, {nameof(opCode)}, {nameof(msilReader)});
                    break;");
            var ctorSwitch = string.Join("\r\n", ctorStatements);

            var csStatements = opCodeNames.Select(x => $@"
    public struct IlOpCode{x.Value}
    {{
        public static readonly OpCode OpCode => OpCodes.{x.Value};

        {getFieldDeclarations((opCodeValue = x.Key), (opCode = opCodes[x.Key]))}

        public IlOpCode{x.Value}(OpCode opCode)
        {{
            this.OpCode = opCode;
        }}
        public IlOpCode{x.Value}(OpCode opCode, IMsilReader msilReader)
        {{
        }}
    }}
");

        }
        public static IlOpCode Create(int opCodeValue, OpCode opCode, IMsilReader msilReader)
        {


            switch (opCodeValue)
            {
                default: break;
            }
            return null;
        }
    }
    public class IlOpCode
    {
    }
}
