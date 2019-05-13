namespace System.Runtime.RuntimeServices
{
    public static partial class Conversions
    {
        /// <summary>
        /// Interface for to support conversion of statically boxed object references.
        /// </summary>
        public interface IRuntimeConvert
        {
            /// <summary>
            /// Convert to the target Runtime <see cref="Type"/>.
            /// </summary>
            /// <param name="type"></param>
            /// <returns></returns>
            object To(Type type);

            /// <summary>
            /// Convert to &lt;<typeparamref name="TOut"/>&gt;            
            /// /// </summary>
            /// <typeparam name="TOut"></typeparam>
            /// <returns></returns>
            TOut To<TOut>();

            /// <summary>
            /// Unbox an object reference and return it's <see cref="IRuntimeConvert"/> interface
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            IRuntimeConvert Unbox(object value);
            bool LessThan(object value);
            bool LessThanOrEqual(object value);
            bool Equals(object value);
            bool GreaterThan(object value);
            bool GreaterThanOrEqual(object value);
            int Compare(object value);
            int Compare(IRuntimeConvert value);

        }


    }
}
