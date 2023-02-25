using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
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
                        break;
                    }
                }
                v--;

                while (r > 0) {
                    UInt32 n = v[0];

                    if (n == 0u) {
                        cnt += 1;
                        v--;
                        r--;
                    }
                    else {
                        break;
                    }
                }
            }

            return Math.Max(1, value.Length - checked((int)cnt));
        }
    }
}
