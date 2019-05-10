using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Runtime.ConversionServices
{
    public static class StringExtensionMethods
    {
        public static IEnumerable<string> GetLines(this string str, bool removeEmptyLines = false)
        {
            return str.Split(new[] { "\r\n", "\r", "\n" },
                removeEmptyLines ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);
        }
    }
    public class InterfaceGenerator
    {
        public class InterfaceCollection
        {
            public string Namespace;
            public List<InterfaceDefinition> InterfaceDefinitions;
        }
        public class InterfaceDefinition
        {
            public string Name;
            public string Implements;
            public List<string> BodyDefs = new List<string>();
            public override string ToString()
            {
                string lf = (BodyDefs.Count == 0) ? "" : "\r\n";
                string impToken = string.IsNullOrEmpty(Implements) ? " " : " : ";
                string indent = BodyDefs.Count == 0 ? "" : "\t";
                return $@"public interface {Name}{impToken}{(Implements ?? string.Empty)}{lf}{{{lf}{indent}{(BodyDefs.Count == 0 ? string.Empty : string.Join("", BodyDefs.Select(x => $"{indent}{x}{lf}")))}{lf}}}";
            }
        }
        public static void GenerateEmitOpCodeInterfaces()
        {
            var opcodes = OpCodeHelper.GetOpCodes();
            var coll = new InterfaceCollection() { Namespace = "System.Runtime.ConversionServices.IMsilOpcode" };
            coll.InterfaceDefinitions = opcodes.Select(x =>
             {
                 return new InterfaceDefinition { Name = $"IOpCode_{(System.Reflection.Metadata.ILOpCode)x.Value.Value}", Implements = "IMsilOpCode" };
             }).ToList();
            var dest = Path.GetFullPath(@"..\..\..\IMsilOpCodes.cs");
            GenerateInterfaces(coll, true, dest);

        }
        public static void GenerateInterfaces(
            InterfaceCollection collection,
            bool SingleFile,
            string DestFolder)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"namespace {collection.Namespace}");
            sb.AppendLine("{");
            if (SingleFile)
            {

                foreach (InterfaceDefinition def in collection.InterfaceDefinitions)
                {
                    var defCode = def.ToString();
                    var lines = defCode.GetLines(true);
                    foreach (var line in lines)
                    {
                        sb.AppendLine($"\t{line}");
                    }
                }
                sb.AppendLine("}");
                var code = sb.ToString();
                File.WriteAllText(DestFolder, code);
                return;
            }
            else
            {
                sb.AppendLine("}");

                var code = sb.ToString();
            }


        }
    }
}
