﻿using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using static System.Runtime.Intrinsics.X86.Avx;
using static System.Runtime.Intrinsics.X86.Avx2;

namespace AvxUInt {
    internal static partial class UIntUtil {

        /// <summary>Shift uint32 array v &lt;&lt;= sft</summary>
        public static unsafe void LeftShift(UInt32[] value, int sft) {
            if (sft < 0) {
                throw new ArgumentOutOfRangeException(nameof(sft));
            }

            if (sft > LeadingZeroCount(value)) {
                throw new OverflowException();
            }

            int sft_block = sft / UInt32Bits, sft_rem = sft % UInt32Bits;

            if (sft_rem == 0) {
                LeftBlockShift(value, sft_block);
                return;
            }

            byte lsft = (byte)sft_rem, rsft = (byte)(UInt32Bits - sft_rem);

            uint r = (uint)(value.Length - sft_block - 1), rem = r % MM256UInt32s;

            fixed (UInt32* v0 = value) {
                UInt32* v = v0 + r;

                Vector256<UInt32> x0, x1, x2, x3, y0, y1, y2, y3, z0, z1, z2, z3;

                if (rem > 0) {
                    Vector256<UInt32> mask = Mask256.Lower(rem);
                    x0 = MaskLoad(v - rem, mask);
                    y0 = MaskLoad(v - rem + 1, mask);

                    z0 = Or(ShiftRightLogical(x0, rsft), ShiftLeftLogical(y0, lsft));

                    MaskStore(v - rem + sft_block + 1, mask, z0);
                    v -= rem;
                    r -= rem;
                }
                while (r >= MM256UInt32s * 4) {
                    (x0, x1, x2, x3) = LoadVector256X4(v - MM256UInt32s * 4);
                    (y0, y1, y2, y3) = LoadVector256X4(v - MM256UInt32s * 4 + 1);

                    z0 = Or(ShiftRightLogical(x0, rsft), ShiftLeftLogical(y0, lsft));
                    z1 = Or(ShiftRightLogical(x1, rsft), ShiftLeftLogical(y1, lsft));
                    z2 = Or(ShiftRightLogical(x2, rsft), ShiftLeftLogical(y2, lsft));
                    z3 = Or(ShiftRightLogical(x3, rsft), ShiftLeftLogical(y3, lsft));

                    StoreX4(v - MM256UInt32s * 4 + sft_block + 1, z0, z1, z2, z3);
                    v -= MM256UInt32s * 4;
                    r -= MM256UInt32s * 4;
                }
                if (r >= MM256UInt32s * 2) {
                    (x0, x1) = LoadVector256X2(v - MM256UInt32s * 2);
                    (y0, y1) = LoadVector256X2(v - MM256UInt32s * 2 + 1);

                    z0 = Or(ShiftRightLogical(x0, rsft), ShiftLeftLogical(y0, lsft));
                    z1 = Or(ShiftRightLogical(x1, rsft), ShiftLeftLogical(y1, lsft));

                    StoreX2(v - MM256UInt32s * 2 + sft_block + 1, z0, z1);
                    v -= MM256UInt32s * 2;
                    r -= MM256UInt32s * 2;
                }
                if (r >= MM256UInt32s) {
                    x0 = LoadVector256(v - MM256UInt32s);
                    y0 = LoadVector256(v - MM256UInt32s + 1);

                    z0 = Or(ShiftRightLogical(x0, rsft), ShiftLeftLogical(y0, lsft));

                    Store(v - MM256UInt32s + sft_block + 1, z0);

                    v -= MM256UInt32s;
                }

                v[sft_block] = v[0] << sft_rem;
            }

            Zeroset(value, 0, (uint)sft_block);
        }

        /// <summary>Shift uint32 array v &gt;&gt;= sft</summary>
        public static unsafe void RightShift(UInt32[] value, int sft) {
            if (sft < 0) {
                throw new ArgumentOutOfRangeException(nameof(sft));
            }

            int sft_block = sft / UInt32Bits, sft_rem = sft % UInt32Bits;

            if (sft_rem == 0 || sft_block >= value.Length) {
                RightBlockShift(value, sft_block);
                return;
            }

            byte rsft = (byte)sft_rem, lsft = (byte)(UInt32Bits - sft_rem);

            uint count = (uint)(value.Length - sft_block - 1), r = count;

            fixed (UInt32* v0 = value) {
                UInt32* v = v0;

                Vector256<UInt32> x0, x1, x2, x3, y0, y1, y2, y3, z0, z1, z2, z3;

                while (r >= MM256UInt32s * 4) {
                    (x0, x1, x2, x3) = LoadVector256X4(v + sft_block);
                    (y0, y1, y2, y3) = LoadVector256X4(v + sft_block + 1);

                    z0 = Or(ShiftRightLogical(x0, rsft), ShiftLeftLogical(y0, lsft));
                    z1 = Or(ShiftRightLogical(x1, rsft), ShiftLeftLogical(y1, lsft));
                    z2 = Or(ShiftRightLogical(x2, rsft), ShiftLeftLogical(y2, lsft));
                    z3 = Or(ShiftRightLogical(x3, rsft), ShiftLeftLogical(y3, lsft));

                    StoreX4(v, z0, z1, z2, z3);
                    r -= MM256UInt32s * 4;
                    v += MM256UInt32s * 4;
                }
                if (r >= MM256UInt32s * 2) {
                    (x0, x1) = LoadVector256X2(v + sft_block);
                    (y0, y1) = LoadVector256X2(v + sft_block + 1);

                    z0 = Or(ShiftRightLogical(x0, rsft), ShiftLeftLogical(y0, lsft));
                    z1 = Or(ShiftRightLogical(x1, rsft), ShiftLeftLogical(y1, lsft));

                    StoreX2(v, z0, z1);
                    r -= MM256UInt32s * 2;
                    v += MM256UInt32s * 2;
                }
                if (r >= MM256UInt32s) {
                    x0 = LoadVector256(v + sft_block);
                    y0 = LoadVector256(v + sft_block + 1);

                    z0 = Or(ShiftRightLogical(x0, rsft), ShiftLeftLogical(y0, lsft));

                    Store(v, z0);
                    r -= MM256UInt32s;
                    v += MM256UInt32s;
                }
                if (r > 0) {
                    Vector256<UInt32> mask = Mask256.Lower(r);

                    x0 = MaskLoad(v + sft_block, mask);
                    y0 = MaskLoad(v + sft_block + 1, mask);

                    z0 = Or(ShiftRightLogical(x0, rsft), ShiftLeftLogical(y0, lsft));

                    MaskStore(v, mask, z0);

                    v += r;
                }

                v[0] = v[sft_block] >> sft_rem;
            }

            Zeroset(value, count + 1, (uint)sft_block);
        }

        /// <summary>Shift uint32 array v &lt;&lt;= sft * UInt32Bits</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void LeftBlockShift(UInt32[] value, int sft) {
            if (sft < 0) {
                throw new ArgumentOutOfRangeException(nameof(sft));
            }

            if (checked(sft + Digits(value)) > value.Length) {
                throw new OverflowException();
            }

            uint r = (uint)(value.Length - sft), rem = r % MM256UInt32s;

            fixed (UInt32* v0 = value) {
                UInt32* v = v0 + r;

                Vector256<UInt32> x0, x1, x2, x3;

                if (rem > 0) {
                    Vector256<UInt32> mask = Mask256.Lower(rem);
                    MaskStore(v + sft - rem, mask, MaskLoad(v - rem, mask));
                    r -= rem;
                    v -= rem;
                }
                while (r >= MM256UInt32s * 4) {
                    (x0, x1, x2, x3) = LoadVector256X4(v - MM256UInt32s * 4);
                    StoreX4(v + sft - MM256UInt32s * 4, x0, x1, x2, x3);
                    r -= MM256UInt32s * 4;
                    v -= MM256UInt32s * 4;
                }
                if (r >= MM256UInt32s * 2) {
                    (x0, x1) = LoadVector256X2(v - MM256UInt32s * 2);
                    StoreX2(v + sft - MM256UInt32s * 2, x0, x1);
                    r -= MM256UInt32s * 2;
                    v -= MM256UInt32s * 2;
                }
                if (r >= MM256UInt32s) {
                    x0 = LoadVector256(v - MM256UInt32s);
                    Store(v + sft - MM256UInt32s, x0);
                }
            }

            Zeroset(value, 0, (uint)sft);
        }

        /// <summary>Shift uint32 array v &gt;&gt;= sft * UInt32Bits</summary>
        public static unsafe void RightBlockShift(UInt32[] value, int sft) {
            if (sft < 0) {
                throw new ArgumentOutOfRangeException(nameof(sft));
            }

            if (sft >= value.Length) {
                Zeroset(value);
                return;
            }

            uint count = (uint)(value.Length - sft), r = count;

            fixed (UInt32* v0 = value) {
                UInt32* v = v0;

                Vector256<UInt32> x0, x1, x2, x3;

                while (r >= MM256UInt32s * 4) {
                    (x0, x1, x2, x3) = LoadVector256X4(v + sft);
                    StoreX4(v, x0, x1, x2, x3);
                    r -= MM256UInt32s * 4;
                    v += MM256UInt32s * 4;
                }
                if (r >= MM256UInt32s * 2) {
                    (x0, x1) = LoadVector256X2(v + sft);
                    StoreX2(v, x0, x1);
                    r -= MM256UInt32s * 2;
                    v += MM256UInt32s * 2;
                }
                if (r >= MM256UInt32s) {
                    x0 = LoadVector256(v + sft);
                    Store(v, x0);
                    r -= MM256UInt32s;
                    v += MM256UInt32s;
                }
                if (r > 0) {
                    Vector256<UInt32> mask = Mask256.Lower(r);
                    MaskStore(v, mask, MaskLoad(v + sft, mask));
                }
            }

            Zeroset(value, count, (uint)sft);
        }
    }
}