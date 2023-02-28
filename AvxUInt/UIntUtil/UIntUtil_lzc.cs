using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.Intrinsics.X86.Avx;
using static System.Runtime.Intrinsics.X86.Avx2;

namespace AvxUInt {
    internal static partial class UIntUtil {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe uint LeadingZeroCount(UInt32[] value) {
            uint cnt = 0, lzc = 0, r = (uint)value.Length;

            fixed (UInt32* v0 = value) {
                UInt32* v = v0 + r - MM256UInt32s;

                while (r >= MM256UInt32s) {
                    Vector256<UInt32> x = Load(v, v0, value.Length);
                    if (TestZ(x, x)) {
                        cnt += MM256UInt32s;
                        v -= MM256UInt32s;
                        r -= MM256UInt32s;
                    }
                    else {
                        uint flag = ((uint)MoveMask(CompareNotEqual(x, Vector256<UInt32>.Zero).AsSingle())) << ShiftIDX3;
                        uint idx = LeadingZeroCount(flag);
                        cnt += idx;
                        lzc = LeadingZeroCount(v[MM256UInt32s - 1 - idx]);
                        r = 0;
                        break;
                    }
                }
                if (r > 0) {
                    Vector256<UInt32> mask = Mask256.Lower(r);

                    Vector256<UInt32> x = MaskLoad(v0, mask, v0, value.Length);
                    if (TestZ(x, x)) {
                        cnt += r;
                    }
                    else {
                        uint flag = ((uint)MoveMask(CompareNotEqual(x, Vector256<UInt32>.Zero).AsSingle())) << (int)(ShiftIDX4 - r);
                        uint idx = LeadingZeroCount(flag);
                        cnt += idx;
                        lzc = LeadingZeroCount(v0[r - 1 - idx]);
                    }
                }
            }

            return cnt * UInt32Bits + lzc;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe uint LeadingZeroCount(UInt32 value) {
            uint cnt = Lzcnt.LeadingZeroCount(value);

            return cnt;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe uint LeadingZeroCount(UInt64 value) {
            (UInt32 hi, UInt32 lo) = Unpack(value);

            if (hi == 0) {
                return LeadingZeroCount(lo) + UInt32Bits;
            }

            return LeadingZeroCount(hi);
        }
    }
}
