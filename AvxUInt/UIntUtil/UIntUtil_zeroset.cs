using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace AvxUInt {
    internal static partial class UIntUtil {

        /// <summary>Zeroset with range</summary>
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

                Vector256<uint> zero = Vector256<UInt32>.Zero;

                uint r = length;
                while (r >= MM256UInt32s * 4) {
                    StoreX4(v, zero, zero, zero, zero, v0, arr.Length);
                    v += MM256UInt32s * 4;
                    r -= MM256UInt32s * 4;
                }
                if (r >= MM256UInt32s * 2) {
                    StoreX2(v, zero, zero, v0, arr.Length);
                    v += MM256UInt32s * 2;
                    r -= MM256UInt32s * 2;
                }
                if (r >= MM256UInt32s) {
                    Store(v, zero, v0, arr.Length);
                    v += MM256UInt32s;
                    r -= MM256UInt32s;
                }
                if (r > 0) {
                    MaskStore(v, zero, Mask256.Lower(r), v0, arr.Length);
                }
            }
        }

        /// <summary>Zeroset all</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Zeroset(UInt32[] arr) {
            uint length = (uint)arr.Length;

            fixed (UInt32* v0 = arr) {
                UInt32* v = v0;

                Vector256<uint> zero = Vector256<UInt32>.Zero;

                uint r = length;
                while (r >= MM256UInt32s * 4) {
                    StoreX4(v, zero, zero, zero, zero, v0, arr.Length);
                    v += MM256UInt32s * 4;
                    r -= MM256UInt32s * 4;
                }
                if (r >= MM256UInt32s * 2) {
                    StoreX2(v, zero, zero, v0, arr.Length);
                    v += MM256UInt32s * 2;
                    r -= MM256UInt32s * 2;
                }
                if (r >= MM256UInt32s) {
                    Store(v, zero, v0, arr.Length);
                    v += MM256UInt32s;
                    r -= MM256UInt32s;
                }
                if (r > 0) {
                    MaskStore(v, zero, Mask256.Lower(r), v0, arr.Length);
                }
            }
        }
    }
}
