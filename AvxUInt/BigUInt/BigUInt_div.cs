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

        public static UInt32 operator %(BigUInt<N> a, UInt32 b) {
            return DivRem(a, b).r;
        }

        public static BigUInt<N> operator %(UInt32 a, BigUInt<N> b) {
            return DivRem(a, b).r;
        }

        public static UInt64 operator %(BigUInt<N> a, UInt64 b) {
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

        public static (BigUInt<N> q, UInt32 r) DivRem(BigUInt<N> a, UInt32 b) {
            BigUInt<N> q = Zero.Copy(), r = a.Copy();

            UIntUtil.DivRem(q.value, r.value, b);

            return (q, r[0]);
        }

        public static (BigUInt<N> q, UInt64 r) DivRem(BigUInt<N> a, UInt64 b) {
            BigUInt<N> q = Zero.Copy(), r = a.Copy();

            UIntUtil.DivRem(q.value, r.value, b);

            return (q, UIntUtil.Pack(r[1], r[0]));
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

        public static (BigUInt<M> q, BigUInt<N> r) DivRem<M>(BigUInt<M> a, BigUInt<N> b) where M: struct, IConstant {
            BigUInt<M> q = BigUInt<M>.Zero.Copy();
            BigUInt<M> r = a.Copy();

            UIntUtil.DivRem(q.value, r.value, b.value);

            return (q, new BigUInt<N>(r.value[..Length], enable_clone: false));
        }

        public static BigUInt<N> RoundDiv(BigUInt<N> a, BigUInt<N> b) {
            (BigUInt<N> q, BigUInt<N> r) = DivRem(a, b);

            uint digits_b = b.Digits;
            
            if (digits_b == 1u) { 
                UInt64 nb = b.value[0], nr = r.value[0];

                if ((nr << 1) >= nb) {
                    UIntUtil.Add(q.value, 1u);
                }
            }
            if (digits_b >= 2u) {
                UInt64 nb = UIntUtil.Pack(b.value[digits_b - 1u], b.value[digits_b - 2u]);
                UInt64 nr = UIntUtil.Pack(r.value[digits_b - 1u], r.value[digits_b - 2u]);

                if ((nr > UInt64.MaxValue / 2) || ((nr << 1) >= nb)) {
                    UIntUtil.Add(q.value, 1u);
                }
            }

            return q;
        }

        public static BigUInt<M> RoundDiv<M>(BigUInt<M> a, BigUInt<N> b) where M: struct, IConstant {
            (BigUInt<M> q, BigUInt<N> r) = DivRem(a, b);

            uint digits_b = b.Digits;
            
            if (digits_b == 1u) { 
                UInt64 nb = b.value[0], nr = r.value[0];

                if ((nr << 1) >= nb) {
                    UIntUtil.Add(q.value, 1u);
                }
            }
            if (digits_b >= 2u) {
                UInt64 nb = UIntUtil.Pack(b.value[digits_b - 1u], b.value[digits_b - 2u]);
                UInt64 nr = UIntUtil.Pack(r.value[digits_b - 1u], r.value[digits_b - 2u]);

                if ((nr > UInt64.MaxValue / 2) || ((nr << 1) >= nb)) {
                    UIntUtil.Add(q.value, 1u);
                }
            }

            return q;
        }
    }
}
