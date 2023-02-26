using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.Intrinsics.X86.Avx;
using static System.Runtime.Intrinsics.X86.Avx2;

namespace AvxUInt {
    internal static partial class UIntUtil {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int LeadingZeroCount(UInt32[] value) {
            uint cnt = 0, lzc = 0, r = (uint)value.Length;

            fixed (UInt32* v0 = value) {
                UInt32* v = v0 + r;

                while (r >= MM256UInt32s) {
                    Vector256<UInt32> x = LoadVector256(v - MM256UInt32s);
                    if (TestZ(x, x)) {
                        cnt += MM256UInt32s;
                        v -= MM256UInt32s;
                        r -= MM256UInt32s;
                    }
                    else {
                        uint flag = ((uint)MoveMask(CompareNotEqual(x, Vector256<UInt32>.Zero).AsSingle())) << 24;
                        uint idx = (uint)LeadingZeroCount(flag);
                        cnt += idx;
                        lzc = (uint)LeadingZeroCount(v0[value.Length - cnt - 1]);
                        r = 0;
                        break;
                    }
                }
                if (r > 0) {
                    Vector256<UInt32> mask = Mask256.Lower(r);
                    Vector256<UInt32> x = MaskLoad(v - r, mask);
                    if (TestZ(x, x)) {
                        cnt += r;
                    }
                    else {
                        uint flag = ((uint)MoveMask(CompareNotEqual(x, Vector256<UInt32>.Zero).AsSingle())) << (int)(24 + (MM256UInt32s - r));
                        uint idx = (uint)LeadingZeroCount(flag);
                        cnt += idx;
                        lzc = (uint)LeadingZeroCount(v0[value.Length - cnt - 1]);
                    }
                }
            }

            return unchecked((int)(cnt * UInt32Bits + lzc));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int LeadingZeroCount(UInt32 value) {
            uint cnt = Lzcnt.LeadingZeroCount(value);

            return unchecked((int)cnt);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int LeadingZeroCount(UInt64 value) {
            (UInt32 hi, UInt32 lo) = Unpack(value);

            if (hi == 0) {
                return LeadingZeroCount(lo) + UInt32Bits;
            }

            return LeadingZeroCount(hi);
        }
    }
}
