using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace AvxUInt {
    internal static partial class UIntUtil {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Zeroset(UInt32[] arr, uint index, uint length) {
            if (length <= 0) {
                return;
            }

            if (checked(index + length) > arr.Length) {
                throw new ArgumentOutOfRangeException($"{nameof(index)},{nameof(length)}");
            }

            fixed (UInt32* v0 = arr) {
                UInt32* v = v0 + index;

                uint r = length;
                while (r >= MM256UInt32s * 4) {
                    Avx.Store(v, Vector256<UInt32>.Zero);
                    Avx.Store(v + MM256UInt32s, Vector256<UInt32>.Zero);
                    Avx.Store(v + MM256UInt32s * 2, Vector256<UInt32>.Zero);
                    Avx.Store(v + MM256UInt32s * 3, Vector256<UInt32>.Zero);
                    v += MM256UInt32s * 4;
                    r -= MM256UInt32s * 4;
                }
                if (r >= MM256UInt32s * 2) {
                    Avx.Store(v, Vector256<UInt32>.Zero);
                    Avx.Store(v + MM256UInt32s, Vector256<UInt32>.Zero);
                    v += MM256UInt32s * 2;
                    r -= MM256UInt32s * 2;
                }
                if (r >= MM256UInt32s) {
                    Avx.Store(v, Vector256<UInt32>.Zero);
                    v += MM256UInt32s;
                    r -= MM256UInt32s;
                }
                if (r > 0) {
                    Avx2.MaskStore(v, Mask256.LSV(r), Vector256<UInt32>.Zero);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Zeroset(UInt32[] arr) {
            uint length = (uint)arr.Length;

            fixed (UInt32* v0 = arr) {
                UInt32* v = v0;

                uint r = length;
                while (r >= MM256UInt32s * 4) {
                    Avx.Store(v, Vector256<UInt32>.Zero);
                    Avx.Store(v + MM256UInt32s, Vector256<UInt32>.Zero);
                    Avx.Store(v + MM256UInt32s * 2, Vector256<UInt32>.Zero);
                    Avx.Store(v + MM256UInt32s * 3, Vector256<UInt32>.Zero);
                    v += MM256UInt32s * 4;
                    r -= MM256UInt32s * 4;
                }
                if (r >= MM256UInt32s * 2) {
                    Avx.Store(v, Vector256<UInt32>.Zero);
                    Avx.Store(v + MM256UInt32s, Vector256<UInt32>.Zero);
                    v += MM256UInt32s * 2;
                    r -= MM256UInt32s * 2;
                }
                if (r >= MM256UInt32s) {
                    Avx.Store(v, Vector256<UInt32>.Zero);
                    v += MM256UInt32s;
                    r -= MM256UInt32s;
                }
                if (r > 0) {
                    Avx2.MaskStore(v, Mask256.LSV(r), Vector256<UInt32>.Zero);
                }
            }
        }
    }
}
