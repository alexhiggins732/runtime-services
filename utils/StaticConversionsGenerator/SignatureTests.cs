using System;
using System.Collections.Generic;
using System.Runtime.ConversionServices;

namespace StaticConversionsGenerator
{

    public class SignatureTests
    {
        public enum OpKey
        {
            Add,
            AddUnchecked,
            Subtract,
            SubtractUnchecked
        }

        public static T1 OpKeySignatureStub<OpKey, T1>(OpKey a) => default(T1);

        public static T2 OpKeySignatureStub<OpKey, T1, T2>(OpKey a, T1 b) => default(T2);

        public static Func<OpKey, T1> OpKeySignature<OpKey, T1>(OpKey a, T1 b)
       => ((d) => OpKeySignatureStub<OpKey, T1>(d));

        public static Func<OpKey, T1, T2> OpKeySignature<OpKey, T1, T2>(OpKey a, T1 b, T2 c)
            => ((d, e) => OpKeySignatureStub<OpKey, T1, T2>(d, e));


        //static Func<OpKey, T1, T2> GetKey<OpKey, T1, T2>(OpKey a, T1 b, T2 c)
        //{
        //    var fn = (Func<OpKey, T1, T2>)((d,e) => GetKey2<OpKey, T1, T2>(d,e));
        //    return fn;
        //}
        public static void AnonTypeKey()
        {
            var r = OpKeySignature(OpKey.Add, 1, 1);
            var t = r.Method.ReturnType;

            var d = new Dictionary<Type, Delegate>();
            d.Add(r.GetType(), r);

            var rs = OpKeySignature(OpKey.Subtract, 1, 1);
            var ts = r.Method.ReturnType;
            d.Add(rs.GetType(), r);

            var r2 = OpKeySignature(OpKey.Add, 1ul, 1);
            d.Add(r2.GetType(), r);

            var r3 = OpKeySignature((dynamic)OpKey.Add, 1ul, 1);
            d.Add(r3.Method.ReturnType, r);

            var r4 = OpKeySignature(OpKey.Add, 1ul, 1);
            d.Add(r4.GetType(), r);

        }
    }

}
