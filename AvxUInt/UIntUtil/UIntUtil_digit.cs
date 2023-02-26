using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using static System.Runtime.Intrinsics.X86.Avx;
using static System.Runtime.Intrinsics.X86.Avx2;

namespace AvxUInt {
    internal static partial class UIntUtil {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int Digits(UInt32[] value) {
            uint cnt = 0, r = (uint)value.Length;

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
                        cnt += (uint)LeadingZeroCount(flag);
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
                        cnt += (uint)LeadingZeroCount(flag);
                    }
                }
            }

            return value.Length - unchecked((int)cnt);
        }
    }
}
