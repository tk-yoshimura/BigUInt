using System.Runtime.CompilerServices;

namespace BigUInt {
    public readonly partial struct UInt128 {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 operator +(UInt128 v1, UInt128 v2) {
            UInt32 carry, e3, e2, e1, e0;

            (carry, e0) = UIntUtil.Unpack(unchecked((UInt64)v1.e0 + (UInt64)v2.e0));
            (carry, e1) = UIntUtil.Unpack(unchecked((UInt64)v1.e1 + (UInt64)v2.e1 + carry));
            (carry, e2) = UIntUtil.Unpack(unchecked((UInt64)v1.e2 + (UInt64)v2.e2 + carry));
            (carry, e3) = UIntUtil.Unpack(unchecked((UInt64)v1.e3 + (UInt64)v2.e3 + carry));

            if (carry > 0u) {
                throw new OverflowException();
            }

            return new(e3, e2, e1, e0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 operator -(UInt128 v1, UInt128 v2) {
            UInt64 hi = ~v2.Hi, lo = ~v2.Lo;
            (hi, lo) = (lo < ~0uL) ? (hi, lo + 1uL) : (hi < ~0uL) ? (hi + 1uL, 0uL) : (0uL, 0uL);

            UInt128 v2_comp = new(hi, lo);

            UInt32 carry, e3, e2, e1, e0;

            (carry, e0) = UIntUtil.Unpack(unchecked((UInt64)v1.e0 + (UInt64)v2_comp.e0));
            (carry, e1) = UIntUtil.Unpack(unchecked((UInt64)v1.e1 + (UInt64)v2_comp.e1 + carry));
            (carry, e2) = UIntUtil.Unpack(unchecked((UInt64)v1.e2 + (UInt64)v2_comp.e2 + carry));
            (carry, e3) = UIntUtil.Unpack(unchecked((UInt64)v1.e3 + (UInt64)v2_comp.e3 + carry));

            if (carry < 1u && v2 != Zero) {
                throw new OverflowException();
            }

            return new(e3, e2, e1, e0);
        }
    }
}