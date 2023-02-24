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
        public static Vector256<UInt32> CompareGreaterThanOrEqual(Vector256<UInt32> x, Vector256<UInt32> y) {
            Vector256<UInt32> geq = CompareEqual(Max(x, y), x);

            return geq;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<UInt32> CompareLessThanOrEqual(Vector256<UInt32> x, Vector256<UInt32> y) {
            Vector256<UInt32> leq = CompareEqual(Max(y, x), y);

            return leq;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<UInt32> CompareNotEqual(Vector256<UInt32> x, Vector256<UInt32> y) {
            Vector256<UInt32> neq = Xor(CompareEqual(x, y), Vector256.Create(~0u));

            return neq;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<UInt32> Reverse(Vector256<UInt32> x) {
            Vector256<UInt32> y = PermuteVar8x32(x, Vector256.Create(7u, 6u, 5u, 4u, 3u, 2u, 1u, 0u));

            return y;
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
        public static unsafe Vector256<UInt32> ReverseLoadVector256(UInt32* ptr) {
            Vector256<UInt32> v0 = Reverse(LoadVector256(ptr));

            return v0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe (Vector256<UInt32> v0, Vector256<UInt32> v1) ReverseLoadVector256X2(UInt32* ptr) {
            Vector256<UInt32> v0 = Reverse(LoadVector256(ptr + MM256UInt32s));
            Vector256<UInt32> v1 = Reverse(LoadVector256(ptr));

            return (v0, v1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe (Vector256<UInt32> v0, Vector256<UInt32> v1, Vector256<UInt32> v2, Vector256<UInt32> v3) ReverseLoadVector256X4(UInt32* ptr) {
            Vector256<UInt32> v0 = Reverse(LoadVector256(ptr + MM256UInt32s * 3));
            Vector256<UInt32> v1 = Reverse(LoadVector256(ptr + MM256UInt32s * 2));
            Vector256<UInt32> v2 = Reverse(LoadVector256(ptr + MM256UInt32s));
            Vector256<UInt32> v3 = Reverse(LoadVector256(ptr));

            return (v0, v1, v2, v3);
        }
    }
}
