namespace AvxUInt {
    public sealed partial class BigUInt<N> {

        public static BigUInt<N> operator /(BigUInt<N> a, BigUInt<N> b) {
            return DivRem(a, b).q;
        }

        public static BigUInt<N> operator /(BigUInt<N> a, UInt32 b) {
            return DivRem(a, b).q;
        }

        public static BigUInt<N> operator /(UInt32 a, BigUInt<N> b) {
            return DivRem(a, b).q;
        }

        public static BigUInt<N> operator /(BigUInt<N> a, UInt64 b) {
            return DivRem(a, b).q;
        }

        public static BigUInt<N> operator /(UInt64 a, BigUInt<N> b) {
            return DivRem(a, b).q;
        }

        public static BigUInt<N> operator %(BigUInt<N> a, BigUInt<N> b) {
            return DivRem(a, b).r;
        }

        public static BigUInt<N> operator %(BigUInt<N> a, UInt32 b) {
            return DivRem(a, b).r;
        }

        public static BigUInt<N> operator %(UInt32 a, BigUInt<N> b) {
            return DivRem(a, b).r;
        }

        public static BigUInt<N> operator %(BigUInt<N> a, UInt64 b) {
            return DivRem(a, b).r;
        }

        public static BigUInt<N> operator %(UInt64 a, BigUInt<N> b) {
            return DivRem(a, b).r;
        }

        public static (BigUInt<N> q, BigUInt<N> r) DivRem(BigUInt<N> a, BigUInt<N> b) {
            BigUInt<N> q = Zero.Copy(), r = a.Copy();

            UIntUtil.DivRem(q.value, r.value, b.value);

            return (q, r);
        }

        public static (BigUInt<N> q, BigUInt<N> r) DivRem(BigUInt<N> a, UInt32 b) {
            BigUInt<N> q = Zero.Copy(), r = a.Copy();

            UIntUtil.DivRem(q.value, r.value, b);

            return (q, r);
        }

        public static (BigUInt<N> q, BigUInt<N> r) DivRem(BigUInt<N> a, UInt64 b) {
            BigUInt<N> q = Zero.Copy(), r = a.Copy();

            UIntUtil.DivRem(q.value, r.value, b);

            return (q, r);
        }

        public static (BigUInt<N> q, BigUInt<N> r) DivRem(UInt32 a, BigUInt<N> b) {
            if (b.Digits > 1u) {
                return (Zero, a);
            }

            UInt32 denom = b[0];

            return (a / denom, a % denom);
        }

        public static (BigUInt<N> q, BigUInt<N> r) DivRem(UInt64 a, BigUInt<N> b) {
            if (b.Digits > 2u) {
                return (Zero, a);
            }

            UInt64 denom = UIntUtil.Pack(b[1], b[0]);

            return (a / denom, a % denom);
        }
    }
}
