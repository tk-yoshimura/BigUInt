namespace AvxUInt {
    public sealed partial class BigUInt<N> {

        public static BigUInt<N> operator /(BigUInt<N> a, BigUInt<N> b) {
            return DivRem(a, b).q;
        }

        public static BigUInt<N> operator /(BigUInt<N> a, UInt32 b) {
            return DivRem(a, b).q;
        }

        public static BigUInt<N> operator /(UInt32 a, BigUInt<N> b) {
            if (b.Digits > 1) {
                throw new OverflowException();
            }

            UInt32 n = b[0];
            if (a < n) {
                throw new OverflowException();
            }

            return a / n;
        }

        public static BigUInt<N> operator /(BigUInt<N> a, UInt64 b) {
            return DivRem(a, b).q;
        }

        public static BigUInt<N> operator /(UInt64 a, BigUInt<N> b) {
            if (b.Digits > 2) {
                throw new OverflowException();
            }

            UInt64 n = UIntUtil.Pack(b[1], b[0]);
            if (a < n) {
                throw new OverflowException();
            }

            return a / n;
        }

        public static BigUInt<N> operator %(BigUInt<N> a, BigUInt<N> b) {
            return DivRem(a, b).r;
        }

        public static BigUInt<N> operator %(BigUInt<N> a, UInt32 b) {
            return DivRem(a, b).r;
        }

        public static BigUInt<N> operator %(UInt32 a, BigUInt<N> b) {
            if (b.Digits > 1) {
                throw new OverflowException();
            }

            UInt32 n = b[0];
            if (a < n) {
                throw new OverflowException();
            }

            return a % n;
        }

        public static BigUInt<N> operator %(BigUInt<N> a, UInt64 b) {
            return DivRem(a, b).r;
        }

        public static BigUInt<N> operator %(UInt64 a, BigUInt<N> b) {
            if (b.Digits > 2) {
                throw new OverflowException();
            }

            UInt64 n = UIntUtil.Pack(b[1], b[0]);
            if (a < n) {
                throw new OverflowException();
            }

            return a % n;
        }

        private static (BigUInt<N> q, BigUInt<N> r) DivRem(BigUInt<N> v1, BigUInt<N> v2) {
            BigUInt<N> q = Zero.Copy(), r = v1.Copy();

            UIntUtil.DivRem(q.value, r.value, v2.value);

            return (q, r);
        }

        private static (BigUInt<N> q, BigUInt<N> r) DivRem(BigUInt<N> v1, UInt32 v2) {
            BigUInt<N> q = Zero.Copy(), r = v1.Copy();

            UIntUtil.DivRem(q.value, r.value, v2);

            return (q, r);
        }

        private static (BigUInt<N> q, BigUInt<N> r) DivRem(BigUInt<N> v1, UInt64 v2) {
            BigUInt<N> q = Zero.Copy(), r = v1.Copy();

            UIntUtil.DivRem(q.value, r.value, v2);

            return (q, r);
        }
    }
}
