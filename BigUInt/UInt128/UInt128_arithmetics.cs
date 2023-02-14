﻿using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using static System.Formats.Asn1.AsnWriter;

namespace BigUInt {
    public readonly partial struct UInt128 {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 operator +(UInt128 a, UInt128 b) {
            UInt32 carry, e3, e2, e1, e0;

            (carry, e0) = UIntUtil.Unpack(unchecked((UInt64)a.e0 + (UInt64)b.e0));
            (carry, e1) = UIntUtil.Unpack(unchecked((UInt64)a.e1 + (UInt64)b.e1 + carry));
            (carry, e2) = UIntUtil.Unpack(unchecked((UInt64)a.e2 + (UInt64)b.e2 + carry));
            (carry, e3) = UIntUtil.Unpack(unchecked((UInt64)a.e3 + (UInt64)b.e3 + carry));

            if (carry > 0u) {
                throw new OverflowException();
            }

            return new(e3, e2, e1, e0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 operator -(UInt128 a, UInt128 b) {
            UInt64 hi = ~b.Hi, lo = ~b.Lo;
            (hi, lo) = (lo < ~0uL) ? (hi, lo + 1uL) : (hi < ~0uL) ? (hi + 1uL, 0uL) : (0uL, 0uL);

            UInt128 b_comp = new(hi, lo);

            UInt32 carry, e3, e2, e1, e0;

            (carry, e0) = UIntUtil.Unpack(unchecked((UInt64)a.e0 + (UInt64)b_comp.e0));
            (carry, e1) = UIntUtil.Unpack(unchecked((UInt64)a.e1 + (UInt64)b_comp.e1 + carry));
            (carry, e2) = UIntUtil.Unpack(unchecked((UInt64)a.e2 + (UInt64)b_comp.e2 + carry));
            (carry, e3) = UIntUtil.Unpack(unchecked((UInt64)a.e3 + (UInt64)b_comp.e3 + carry));

            if (carry < 1u && b != Zero) {
                throw new OverflowException();
            }

            return new(e3, e2, e1, e0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 operator *(UInt128 a, UInt128 b) {
            if ((a.e1 > 0u && b.e3 > 0u) || (a.e2 > 0u && b.e2 > 0u) || (a.e3 > 0u && b.e1 > 0u) ||
                (a.e2 > 0u && b.e3 > 0u) || (a.e3 > 0u && b.e2 > 0u) || (a.e3 > 0u && b.e3 > 0u)) {
                throw new OverflowException();
            }

            (UInt64 v00_hi, UInt32 e0) = UIntUtil.Unpack((UInt64)a.e0 * b.e0);

            (UInt64 v01_hi, UInt64 v01_lo) = UIntUtil.Unpack((UInt64)a.e0 * b.e1);
            (UInt64 v10_hi, UInt64 v10_lo) = UIntUtil.Unpack((UInt64)a.e1 * b.e0);

            (UInt64 v02_hi, UInt64 v02_lo) = UIntUtil.Unpack((UInt64)a.e0 * b.e2);
            (UInt64 v11_hi, UInt64 v11_lo) = UIntUtil.Unpack((UInt64)a.e1 * b.e1);
            (UInt64 v20_hi, UInt64 v20_lo) = UIntUtil.Unpack((UInt64)a.e2 * b.e0);

            (UInt32 v03_hi, UInt64 v03_lo) = UIntUtil.Unpack((UInt64)a.e0 * b.e3);
            (UInt32 v12_hi, UInt64 v12_lo) = UIntUtil.Unpack((UInt64)a.e1 * b.e2);
            (UInt32 v21_hi, UInt64 v21_lo) = UIntUtil.Unpack((UInt64)a.e2 * b.e1);
            (UInt32 v30_hi, UInt64 v30_lo) = UIntUtil.Unpack((UInt64)a.e3 * b.e0);

            (UInt32 carry1, UInt32 e1) = UIntUtil.Unpack(v00_hi + v01_lo + v10_lo);
            (UInt32 carry2, UInt32 e2) = UIntUtil.Unpack(v01_hi + v10_hi + v02_lo + v11_lo + v20_lo + carry1);
            (UInt32 carry3, UInt32 e3) = UIntUtil.Unpack(v02_hi + v11_hi + v20_hi + v03_lo + v12_lo + v21_lo + v30_lo + carry2);

            if (carry3 > 0u || v03_hi > 0u || v12_hi > 0u || v21_hi > 0u || v30_hi > 0u) {
                throw new OverflowException();
            }

            return new UInt128(e3, e2, e1, e0);
        }

        public static (UInt128 q, UInt128 r) DivRem(UInt128 a, UInt128 b) {
            if (a < b) {
                return (Zero, a);
            }
            if (b == Zero) {
                throw new DivideByZeroException();
            }
            if (a.Hi == 0uL) {
                return a.e1 > 0u ? (a.Lo / b.Lo, a.Lo % b.Lo) : (a.e0 / b.e0, a.e0 % b.e0);
            }
            if (b.e3 >= 0x80000000u) {
                return (a < b) ? (0u, a) : (1u, a - b);
            }

            int b_lzc = LeadingZeroCount(b);
            UInt128 q = Zero, r = a;
                        
            int loops = 0;

            for (;;) {
                if (r.e3 > 0u) {
                    if (b.Hi == 0uL) {
                        UInt64 n = r.Hi / b.Lo;

                        q += (UInt128)n << UIntUtil.UInt64Bits;
                        r -= (b * n) << UIntUtil.UInt64Bits;
                    }
                    else if (b.e3 == 0u && (b.e2 < ~0u || b.e1 < ~0u)) {
                        UInt64 n = r.Hi / (UIntUtil.Pack(b.e2, b.e1) + 1uL);

                        q += (UInt128)n << UIntUtil.UInt32Bits;
                        r -= (b * n) << UIntUtil.UInt32Bits;
                    }
                    else if(b.Hi > 0uL){
                        UInt64 n = r.Hi / (b.Hi + 1uL);

                        q += n;
                        r -= b * n;
                    }
                }
                if (r.e3 == 0u) {
                    if (b.Hi == 0uL) {
                        UInt64 n = UIntUtil.Pack(r.e2, r.e1) / b.Lo;

                        q += (UInt128)n << UIntUtil.UInt32Bits;
                        r -= (b * n) << UIntUtil.UInt32Bits;
                    }
                    else if (b.e3 == 0u && (b.e2 < ~0u || b.e1 < ~0u)) {
                        UInt64 n = UIntUtil.Pack(r.e2, r.e1) / (UIntUtil.Pack(b.e2, b.e1) + 1uL);

                        q += n;
                        r -= b * n;
                    }
                }
                if (r.Hi == 0uL) {
                    if (b.Hi == 0uL) {
                        UInt64 n = r.Lo / b.Lo;

                        q += n;
                        r -= b * n;

                        break;
                    }
                }
                                
                int r_lzc = LeadingZeroCount(r), scale = b_lzc - r_lzc - 1;

                if (scale <= 0) {
                    while (r >= b) {
                        q += 1u;
                        r -= b;

                        loops++;
                    }
                    break;
                }

                q += (UInt128)1u << scale;
                r -= b << scale;

                loops++;
            }

            Console.WriteLine(loops);

            return (q, r);
        }

        public static UInt128 operator /(UInt128 a, UInt128 b) => DivRem(a, b).q;
    }
}