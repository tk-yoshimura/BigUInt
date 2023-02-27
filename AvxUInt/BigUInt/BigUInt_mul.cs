namespace AvxUInt {
    public sealed partial class BigUInt<N> {

        public static BigUInt<N> operator *(BigUInt<N> a, BigUInt<N> b) {
            return Mul(a, b);
        }

        public static BigUInt<N> operator *(BigUInt<N> a, UInt32 b) {
            return Mul(a, b);
        }

        public static BigUInt<N> operator *(UInt32 a, BigUInt<N> b) {
            return Mul(b, a);
        }

        public static BigUInt<N> operator *(BigUInt<N> a, UInt64 b) {
            return Mul(a, b);
        }

        public static BigUInt<N> operator *(UInt64 a, BigUInt<N> b) {
            return Mul(b, a);
        }

        private static BigUInt<N> Mul(BigUInt<N> v1, BigUInt<N> v2) {
            BigUInt<N> ret = Zero.Copy();

            UIntUtil.Fma(ret.value, v1.value, v2.value);

            return ret;
        }

        private static BigUInt<N> Mul(BigUInt<N> v1, UInt32 v2) {
            BigUInt<N> ret = Zero.Copy();

            UIntUtil.Fma(ret.value, v1.value, v2);

            return ret;
        }

        private static BigUInt<N> Mul(BigUInt<N> v1, UInt64 v2) {
            BigUInt<N> ret = Zero.Copy();

            UIntUtil.Fma(ret.value, v1.value, v2);

            return ret;
        }

        public static BigUInt<Double<N>> ExpandMul(BigUInt<N> v1, BigUInt<N> v2) {
            BigUInt<Double<N>> ret = BigUInt<Double<N>>.Zero;

            UIntUtil.Fma(ret.value, v1.value, v2.value);

            return ret;
        }
    }
}
