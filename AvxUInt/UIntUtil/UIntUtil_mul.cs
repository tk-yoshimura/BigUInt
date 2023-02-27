using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.Intrinsics.X86.Avx;
using static System.Runtime.Intrinsics.X86.Avx2;

namespace AvxUInt {
    internal static partial class UIntUtil {
        /// <summary>Operate uint32 array arr_dst += a * b &lt;&lt; offset</summary>
        public static unsafe void Mul(uint offset, UInt32[] arr_dst, UInt32[] arr_a, UInt32 b) {
            if (b == 0u) {
                return;
            }

            fixed (UInt32* va0 = arr_a) {
                UInt32* va = va0;

                Vector256<UInt32> y = Vector256.Create((UInt64)b).AsUInt32();
                uint r = (uint)arr_a.Length;

                Vector256<UInt32> a0, a1, a2, a3, c0, c1, c2, c3;

                while (r >= MM256UInt32s * 2) {
                    (a0, a1) = LoadVector256X2(va);
                    (x0, x1) = ToUInt64X2(a0);
                    (x2, x3) = ToUInt64X2(a1);

                    z0 = Avx2.Multiply(x0, y);
                }
            }

            if (b > 0u) {
                throw new OverflowException();
            }
        }
    }
}
