namespace System.Runtime.ConversionServices
{
    public static partial class Conversions
    {
        public struct RuntimeConverter<TIn, TOut>
        {
            public static Func<TIn, TOut> Convert = CreateConverter<TIn, TOut>();
            public static object BoxedConvert(object value) => Convert((TIn)value);
        }
    }
}
