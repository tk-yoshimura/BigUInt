using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

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
    }
}