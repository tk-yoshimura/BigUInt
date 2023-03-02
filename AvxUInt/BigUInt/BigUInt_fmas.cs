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

        public static BigUInt<M> Fma<M>(BigUInt<M> c, BigUInt<N> a, BigUInt<N> b) where M : struct, IConstant {
            BigUInt<M> ret = c.Copy();

            UIntUtil.Fma(ret.value, a.value, b.value);

            return ret;
        }

        public static BigUInt<M> Fma<M>(BigUInt<M> c, BigUInt<N> a, UInt32 b) where M : struct, IConstant {
            BigUInt<M> ret = c.Copy();

            UIntUtil.Fma(ret.value, a.value, b);

            return ret;
        }

        public static BigUInt<M> Fma<M>(BigUInt<M> c, BigUInt<N> a, UInt64 b) where M : struct, IConstant {
            BigUInt<M> ret = c.Copy();

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

        public static BigUInt<M> Fms<M>(BigUInt<M> c, BigUInt<N> a, BigUInt<N> b) where M : struct, IConstant {
            BigUInt<M> ret = c.Copy();

            UIntUtil.Fms(ret.value, a.value, b.value);

            return ret;
        }

        public static BigUInt<M> Fms<M>(BigUInt<M> c, BigUInt<N> a, UInt32 b) where M : struct, IConstant {
            BigUInt<M> ret = c.Copy();

            UIntUtil.Fms(ret.value, a.value, b);

            return ret;
        }

        public static BigUInt<M> Fms<M>(BigUInt<M> c, BigUInt<N> a, UInt64 b) where M : struct, IConstant {
            BigUInt<M> ret = c.Copy();

            UIntUtil.Fms(ret.value, a.value, b);

            return ret;
        }
    }
}
