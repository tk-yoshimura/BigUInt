using System.Runtime.CompilerServices;

namespace AvxUInt {
    public sealed partial class BigUInt<N> {

        public static BigUInt<N> operator <<(BigUInt<N> n, int sft) {
            if (sft < 0) {
                throw new ArgumentOutOfRangeException(nameof(sft));
            }

            return LeftShift(n, sft);
        }

        public static BigUInt<N> operator >>(BigUInt<N> n, int sft) {
            if (sft < 0) {
                throw new ArgumentOutOfRangeException(nameof(sft));
            }

            return RightShift(n, sft);
        }

        public static BigUInt<N> LeftShift(BigUInt<N> n, int sft) {
            BigUInt<N> ret = n.Copy();

            ret.LeftShift(sft);

            return ret;
        }

        public static BigUInt<N> RightShift(BigUInt<N> n, int sft) {
            BigUInt<N> ret = n.Copy();

            ret.RightShift(sft);

            return ret;
        }

        public static BigUInt<N> LeftBlockShift(BigUInt<N> n, int sft) {
            BigUInt<N> ret = n.Copy();

            ret.LeftBlockShift(sft);

            return ret;
        }

        public static BigUInt<N> RightBlockShift(BigUInt<N> n, int sft) {
            BigUInt<N> ret = n.Copy();

            ret.RightBlockShift(sft);

            return ret;
        }

        /// <summary>Shift uint32 array v &lt;&lt;= sft</summary>
        private unsafe void LeftShift(int sft) {
            UIntUtil.LeftShift(value, sft);
        }

        /// <summary>Shift uint32 array v &gt;&gt;= sft</summary>
        private unsafe void RightShift(int sft) {
            UIntUtil.RightShift(value, sft);
        }

        /// <summary>Shift uint32 array v &lt;&lt;= sft * UInt32Bits</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe void LeftBlockShift(int sft) {
            UIntUtil.LeftBlockShift(value, sft);
        }

        /// <summary>Shift uint32 array v &gt;&gt;= sft * UInt32Bits</summary>
        private void RightBlockShift(int sft) {
            UIntUtil.RightBlockShift(value, sft);
        }
    }
}
