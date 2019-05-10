using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StaticConversionsGenerator
{
    public class LinqExpressionStructBuilder
    {
        public static object GetExpressions()
        {
            var t = typeof(System.Linq.Expressions.Expression);
            var methods = t.GetMethods(BindingFlags.Public | BindingFlags.Static);
            var d = methods.ToLookup(x => x.ReturnType, x => x);
            var ms = methods.OrderBy(x => x.ReturnType.Name).ThenBy(x => x.Name)
                .Select(x => $"{x}")
                .ToList();
            var defs = string.Join("\r\n", ms);

            var codebuilder = new System.Text.StringBuilder();
            var methodList = methods.OrderBy(x => x.ReturnType.Name).ThenBy(x => x.Name).ToList();

            foreach (var method in methodList)
            {
                var sb = new System.Text.StringBuilder();
                var name = method.Name;
                var args = method.GetParameters();
                if (args.Any(x => (x.ParameterType.Name == "MethodInfo") || x.ParameterType != typeof(System.Linq.Expressions.Expression) && x.ParameterType.Name.Contains("Expression"))) continue;
                sb.Append($"public struct {name}");

                var genericParams = new List<string>();
                foreach (var arg in args)
                {
                    if (arg.ParameterType == t)
                    {
                        genericParams.Add($"T{genericParams.Count + 1}");
                    }
                    else
                    {
                        genericParams.Add(arg.ParameterType.Name);
                    }
                }
                if (genericParams.Count > 0)
                {
                    sb.Append($"<{string.Join(", ", genericParams)}>");
                }
                sb.AppendLine();
                sb.AppendLine("{");

                sb.AppendLine($"\tstatic Func<{string.Join(", ", genericParams)}> op;");
                sb.AppendLine($"\tpublic static Func<{string.Join(", ", genericParams)}> Op = (op ?? CreateFunc<{string.Join(", ", genericParams)}>(Operation.{name}));");
                sb.AppendLine("}");
                var structdef = sb.ToString();
                codebuilder.AppendLine(structdef);
            }
            var structCode = codebuilder.ToString();



            return defs;

        }
    }
}
