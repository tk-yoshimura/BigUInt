using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.Intrinsics.X86.Avx;

namespace AvxUInt {
    internal static partial class UIntUtil {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int LeadingZeroCount(UInt32[] value) {
            uint cnt = 0, r = (uint)value.Length;

            fixed (UInt32* v0 = value) {
                UInt32* v = v0 + r;

                while (r >= MM256UInt32s) {
                    Vector256<UInt32> x = LoadVector256(v - MM256UInt32s);
                    if (TestZ(x, x)) {
                        cnt += UInt32Bits * MM256UInt32s;
                        v -= MM256UInt32s;
                        r -= MM256UInt32s;
                    }
                    else {
                        uint flag = ((uint)MoveMask(CompareNotEqual(x, Vector256<UInt32>.Zero).AsSingle())) << 24;
                        uint idx = (uint)LeadingZeroCount(flag);
                        cnt += UInt32Bits * idx;
                        v -= idx;
                        r -= idx;
                        break;
                    }
                }
                v--;

                while (r > 0) {
                    UInt32 n = v[0];

                    if (n == 0u) {
                        cnt += UInt32Bits;
                        v--;
                        r--;
                    }
                    else {
                        cnt += Lzcnt.LeadingZeroCount(n);
                        break;
                    }
                }
            }

            return checked((int)cnt);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int LeadingZeroCount(UInt32 value) {
            uint cnt = Lzcnt.LeadingZeroCount(value);

            return checked((int)cnt);
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
