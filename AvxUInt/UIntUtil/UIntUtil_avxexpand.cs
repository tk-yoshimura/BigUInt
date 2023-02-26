using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using static System.Runtime.Intrinsics.X86.Avx;
using static System.Runtime.Intrinsics.X86.Avx2;

namespace AvxUInt {
    internal static partial class UIntUtil {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<UInt32> CompareGreaterThan(Vector256<UInt32> x, Vector256<UInt32> y) {
            Vector256<UInt32> gt = Xor(CompareEqual(Max(x, y), y), Vector256.Create(~0u));

            return gt;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<UInt32> CompareLessThan(Vector256<UInt32> x, Vector256<UInt32> y) {
            Vector256<UInt32> lt = Xor(CompareEqual(Max(y, x), x), Vector256.Create(~0u));

            return lt;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<UInt32> CompareNotEqual(Vector256<UInt32> x, Vector256<UInt32> y) {
            Vector256<UInt32> neq = Xor(CompareEqual(x, y), Vector256.Create(~0u));

            return neq;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe (Vector256<UInt32> v0, Vector256<UInt32> v1) LoadVector256X2(UInt32* ptr) {
            Vector256<UInt32> v0 = LoadVector256(ptr);
            Vector256<UInt32> v1 = LoadVector256(ptr + MM256UInt32s);

            return (v0, v1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe (Vector256<UInt32> v0, Vector256<UInt32> v1, Vector256<UInt32> v2, Vector256<UInt32> v3) LoadVector256X4(UInt32* ptr) {
            Vector256<UInt32> v0 = LoadVector256(ptr);
            Vector256<UInt32> v1 = LoadVector256(ptr + MM256UInt32s);
            Vector256<UInt32> v2 = LoadVector256(ptr + MM256UInt32s * 2);
            Vector256<UInt32> v3 = LoadVector256(ptr + MM256UInt32s * 3);

            return (v0, v1, v2, v3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void StoreX2(UInt32* ptr, Vector256<UInt32> v0, Vector256<UInt32> v1) {
            Store(ptr, v0);
            Store(ptr + MM256UInt32s, v1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void StoreX4(UInt32* ptr, Vector256<UInt32> v0, Vector256<UInt32> v1, Vector256<UInt32> v2, Vector256<UInt32> v3) {
            Store(ptr, v0);
            Store(ptr + MM256UInt32s, v1);
            Store(ptr + MM256UInt32s * 2, v2);
            Store(ptr + MM256UInt32s * 3, v3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAllZero(Vector256<UInt32> x) => TestZ(x, x);
    }
}
