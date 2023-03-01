namespace AvxUInt {
    public sealed partial class BigUInt<N> {

        public static BigUInt<N> operator /(BigUInt<N> a, BigUInt<N> b) {
            return Div(a, b);
        }

        public static BigUInt<N> operator /(BigUInt<N> a, UInt32 b) {
            return Div(a, b);
        }

        public static BigUInt<N> operator /(UInt32 a, BigUInt<N> b) {
            return Div(a, b);
        }

        public static BigUInt<N> operator /(BigUInt<N> a, UInt64 b) {
            return Div(a, b);
        }

        public static BigUInt<N> operator /(UInt64 a, BigUInt<N> b) {
            return Div(a, b);
        }

        public static BigUInt<N> operator %(BigUInt<N> a, BigUInt<N> b) {
            return Rem(a, b);
        }

        public static UInt32 operator %(BigUInt<N> a, UInt32 b) {
            return Rem(a, b);
        }

        public static BigUInt<N> operator %(UInt32 a, BigUInt<N> b) {
            return Rem(a, b);
        }

        public static UInt64 operator %(BigUInt<N> a, UInt64 b) {
            return Rem(a, b);
        }

        public static BigUInt<N> operator %(UInt64 a, BigUInt<N> b) {
            return Rem(a, b);
        }

        public static (BigUInt<N> q, BigUInt<N> r) DivRem(BigUInt<N> a, BigUInt<N> b) {
            BigUInt<N> q = Zero.Copy(), r = a.Copy();

            UIntUtil.DivRem(q.value, r.value, b.value);

            return (q, r);
        }

        public static BigUInt<N> Div(BigUInt<N> a, BigUInt<N> b) {
            return DivRem(a, b).q;
        }

        public static BigUInt<N> Rem(BigUInt<N> a, BigUInt<N> b) {
            return DivRem(a, b).r;
        }

        public static (BigUInt<N> q, UInt32 r) DivRem(BigUInt<N> a, UInt32 b) {
            BigUInt<N> q = Zero.Copy(), r = a.Copy();

            UIntUtil.DivRem(q.value, r.value, b);

            return (q, r[0]);
        }

        public static BigUInt<N> Div(BigUInt<N> a, UInt32 b) {
            return DivRem(a, b).q;
        }

        public static UInt32 Rem(BigUInt<N> a, UInt32 b) {
            return DivRem(a, b).r;
        }

        public static (BigUInt<N> q, UInt64 r) DivRem(BigUInt<N> a, UInt64 b) {
            BigUInt<N> q = Zero.Copy(), r = a.Copy();

            UIntUtil.DivRem(q.value, r.value, b);

            return (q, UIntUtil.Pack(r[1], r[0]));
        }

        public static BigUInt<N> Div(BigUInt<N> a, UInt64 b) {
            return DivRem(a, b).q;
        }

        public static UInt64 Rem(BigUInt<N> a, UInt64 b) {
            return DivRem(a, b).r;
        }

        public static (UInt32 q, UInt32 r) DivRem(UInt32 a, BigUInt<N> b) {
            if (b.Digits > 1u) {
                return (0u, a);
            }

            UInt32 denom = b[0];

            return (a / denom, a % denom);
        }

        public static UInt32 Div(UInt32 a, BigUInt<N> b) {
            if (b.Digits > 1u) {
                return 0u;
            }

            UInt32 denom = b[0];

            return a / denom;
        }

        public static UInt32 Rem(UInt32 a, BigUInt<N> b) {
            if (b.Digits > 1u) {
                return a;
            }

            UInt32 denom = b[0];

            return a % denom;
        }

        public static (UInt64 q, UInt64 r) DivRem(UInt64 a, BigUInt<N> b) {
            if (b.Digits > 2u) {
                return (0uL, a);
            }

            UInt64 denom = UIntUtil.Pack(b[1], b[0]);

            return (a / denom, a % denom);
        }

        public static UInt64 Div(UInt64 a, BigUInt<N> b) {
            if (b.Digits > 2u) {
                return 0uL;
            }

            UInt64 denom = UIntUtil.Pack(b[1], b[0]);

            return a / denom;
        }

        public static UInt64 Rem(UInt64 a, BigUInt<N> b) {
            if (b.Digits > 2u) {
                return a;
            }

            UInt64 denom = UIntUtil.Pack(b[1], b[0]);

            return a % denom;
        }

        public static (BigUInt<M> q, BigUInt<N> r) DivRem<M>(BigUInt<M> a, BigUInt<N> b) where M : struct, IConstant {
            BigUInt<M> q = BigUInt<M>.Zero.Copy();
            BigUInt<M> r = a.Copy();

            UIntUtil.DivRem(q.value, r.value, b.value);

            return (q, new BigUInt<N>(r.value[..Length], enable_clone: false));
        }

        public static BigUInt<M> Div<M>(BigUInt<M> a, BigUInt<N> b) where M : struct, IConstant {
            return DivRem(a, b).q;
        }

        public static BigUInt<N> Rem<M>(BigUInt<M> a, BigUInt<N> b) where M : struct, IConstant {
            return DivRem(a, b).r;
        }

        public static BigUInt<N> RoundDiv(BigUInt<N> a, BigUInt<N> b) {
            (BigUInt<N> q, BigUInt<N> r) = DivRem(a, b);

            uint lzc_r = r.LeadingZeroCount;

            if (lzc_r == 0u) {
                // e.g. r: 0x8000... b: 0xFFFF...
                UIntUtil.Add(q.value, 1u);
            }
            else {
                uint lzc_b = b.LeadingZeroCount;

                if (lzc_r == lzc_b) {
                    // e.g. r: 0x1000... b: 0x1FFF...
                    UIntUtil.Add(q.value, 1u);
                }
                else if ((lzc_r - lzc_b) == 1u) {
                    // e.g. r: 0x0800... b: 0x1000...
                    UIntUtil.LeftShift(r.value, 1);

                    if (UIntUtil.GreaterThanOrEqual((uint)Length, r.value, b.value)) {
                        UIntUtil.Add(q.value, 1u);
                    }
                }
            }

            return q;
        }

        public static BigUInt<M> RoundDiv<M>(BigUInt<M> a, BigUInt<N> b) where M : struct, IConstant {
            (BigUInt<M> q, BigUInt<N> r) = DivRem(a, b);

            uint lzc_r = r.LeadingZeroCount;

            if (lzc_r == 0u) {
                // e.g. r: 0x8000... b: 0xFFFF...
                UIntUtil.Add(q.value, 1u);
            }
            else {
                uint lzc_b = b.LeadingZeroCount;

                if (lzc_r == lzc_b) {
                    // e.g. r: 0x1000... b: 0x1FFF...
                    UIntUtil.Add(q.value, 1u);
                }
                else if ((lzc_r - lzc_b) == 1u) {
                    // e.g. r: 0x0800... b: 0x1000...
                    UIntUtil.LeftShift(r.value, 1);

                    if (UIntUtil.GreaterThanOrEqual((uint)Length, r.value, b.value)) {
                        UIntUtil.Add(q.value, 1u);
                    }
                }
            }

            return q;
        }
    }
}
