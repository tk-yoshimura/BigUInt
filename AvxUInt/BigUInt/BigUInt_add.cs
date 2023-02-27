namespace AvxUInt {
    public sealed partial class BigUInt<N> {

        public static BigUInt<N> operator +(BigUInt<N> a, BigUInt<N> b) {
            return Add(a, b);
        }

        public static BigUInt<N> operator +(BigUInt<N> a, UInt32 b) {
            return Add(a, b);
        }

        public static BigUInt<N> operator +(UInt32 a, BigUInt<N> b) {
            return Add(b, a);
        }

        public static BigUInt<N> operator +(BigUInt<N> a, UInt64 b) {
            return Add(a, b);
        }

        public static BigUInt<N> operator +(UInt64 a, BigUInt<N> b) {
            return Add(b, a);
        }

        private static BigUInt<N> Add(BigUInt<N> v1, BigUInt<N> v2) {
            BigUInt<N> ret = v1.Copy();

            UIntUtil.Add(ret.value, v2.value);

            return ret;
        }

        private static BigUInt<N> Add(BigUInt<N> v1, UInt32 v2) {
            BigUInt<N> ret = v1.Copy();

            UIntUtil.Add(ret.value, v2);

            return ret;
        }

        private static BigUInt<N> Add(BigUInt<N> v1, UInt64 v2) {
            BigUInt<N> ret = v1.Copy();

            UIntUtil.Add(ret.value, v2);

            return ret;
        }
    }
}
