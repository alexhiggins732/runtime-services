using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Runtime.MsilEngine
{
    public class MsilEngineService : IMsilEngineService
    {
        private Func<IMsilReader> readerResolver;
        private Func<IMsilStack> stackResolver;
        private Func<IMsilLocals> localsResolver;
        private Func<IMsilArguments> argumentsResolver;
        private Func<IMsilTokenResolver> tokenResolver;

        public MsilEngineService(Func<IMsilReader> readerResolver, Func<IMsilStack> stackResolver, Func<IMsilLocals> localsResolver, Func<IMsilArguments> argumentsResolver, Func<IMsilTokenResolver> tokenResolver)
        {
            this.readerResolver = readerResolver;
            this.stackResolver = stackResolver;
            this.localsResolver = localsResolver;
            this.argumentsResolver = argumentsResolver;
            this.tokenResolver = tokenResolver;
            MsilReader = readerResolver();
            Arguments = argumentsResolver();
            TokenResolver = tokenResolver(); 

        }

        public IMsilReader MsilReader { get; }
        public IMsilArguments Arguments { get; }
        public IMsilTokenResolver TokenResolver { get; }
        public IMsilLocals NewLocals() => localsResolver();
        public IMsilStack NewStack() => stackResolver();




        public static void Execute(MethodInfo methodInfo)
        {
            var il = methodInfo.GetMethodBody().GetILAsByteArray();
            var body = methodInfo.GetMethodBody();

            var reader = new MsilByteCodeReader(il);
            var locals = new MsilLocals(body);
            Func<IMsilLocals> localsResolver = () => new MsilLocals(body);
            Func<IMsilArguments> argumentsResolver = () => MsilArguments.Empty;
            Func<IMsilTokenResolver> tokenResolver = () => new MsilModuleTokenResolver(methodInfo.Module);
            Func<IMsilReader> readerResolver = () => new MsilByteCodeReader(il);
            Func<IMsilStack> stackResolver = () => new MsilStack();
            var service = new MsilEngineService(readerResolver, stackResolver, localsResolver, argumentsResolver, tokenResolver);
            var engine = new MsilEngine(service);
            engine.Execute();
        }

    }
    public class MsilArguments : IMsilArguments
    {
        public static readonly MsilArguments Empty = new MsilArguments();
        private object[] arguments;
        public MsilArguments(params object[] arguments) => this.arguments = arguments.ToArray();
        public object this[int index] { get => arguments[index]; set => arguments[index] = value; }
    }

    public class MsilLocals : IMsilLocals
    {
        public static readonly MsilLocals Empty = new MsilLocals();
        private object[] locals;
        public MsilLocals() => this.locals = new object[] { };
        public MsilLocals(MethodBody body)
        {
            this.locals = new object[body.LocalVariables.Count];
        }
        public object this[int index] { get => locals[index]; set => locals[index] = value; }
    }
}
