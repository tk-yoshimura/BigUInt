namespace AvxUInt {
    public sealed partial class BigUInt<N> {
        public static BigUInt<N> Fma(BigUInt<N> c, BigUInt<N> a, BigUInt<N> b) {
            BigUInt<N> ret = c.Copy();

            UIntUtil.Fma(ret.value, a.value, b.value);

            return ret;
        }

        public static BigUInt<N> Fma(BigUInt<N> c, BigUInt<N> a, UInt32 b) {
            BigUInt<N> ret = c.Copy();

            UIntUtil.Fma(ret.value, a.value, b);

            return ret;
        }

        public static BigUInt<N> Fma(BigUInt<N> c, BigUInt<N> a, UInt64 b) {
            BigUInt<N> ret = c.Copy();

            UIntUtil.Fma(ret.value, a.value, b);

            return ret;
        }

        public static BigUInt<Double<N>> Fma(BigUInt<Double<N>> c, BigUInt<N> a, BigUInt<N> b) {
            BigUInt<Double<N>> ret = c.Copy();

            UIntUtil.Fma(ret.value, a.value, b.value);

            return ret;
        }

        public static BigUInt<Double<N>> Fma(BigUInt<Double<N>> c, BigUInt<N> a, UInt32 b) {
            BigUInt<Double<N>> ret = c.Copy();

            UIntUtil.Fma(ret.value, a.value, b);

            return ret;
        }

        public static BigUInt<Double<N>> Fma(BigUInt<Double<N>> c, BigUInt<N> a, UInt64 b) {
            BigUInt<Double<N>> ret = c.Copy();

            UIntUtil.Fma(ret.value, a.value, b);

            return ret;
        }

        public static BigUInt<N> Fms(BigUInt<N> c, BigUInt<N> a, BigUInt<N> b) {
            BigUInt<N> ret = c.Copy();

            UIntUtil.Fms(ret.value, a.value, b.value);

            return ret;
        }

        public static BigUInt<N> Fms(BigUInt<N> c, BigUInt<N> a, UInt32 b) {
            BigUInt<N> ret = c.Copy();

            UIntUtil.Fms(ret.value, a.value, b);

            return ret;
        }

        public static BigUInt<N> Fms(BigUInt<N> c, BigUInt<N> a, UInt64 b) {
            BigUInt<N> ret = c.Copy();

            UIntUtil.Fms(ret.value, a.value, b);

            return ret;
        }

        public static BigUInt<Double<N>> Fms(BigUInt<Double<N>> c, BigUInt<N> a, BigUInt<N> b) {
            BigUInt<Double<N>> ret = c.Copy();

            UIntUtil.Fms(ret.value, a.value, b.value);

            return ret;
        }

        public static BigUInt<Double<N>> Fms(BigUInt<Double<N>> c, BigUInt<N> a, UInt32 b) {
            BigUInt<Double<N>> ret = c.Copy();

            UIntUtil.Fms(ret.value, a.value, b);

            return ret;
        }

        public static BigUInt<Double<N>> Fms(BigUInt<Double<N>> c, BigUInt<N> a, UInt64 b) {
            BigUInt<Double<N>> ret = c.Copy();

            UIntUtil.Fms(ret.value, a.value, b);

            return ret;
        }
    }
}
