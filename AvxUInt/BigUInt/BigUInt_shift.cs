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

            UIntUtil.LeftShift(ret.value, sft);

            return ret;
        }

        public static BigUInt<N> RightShift(BigUInt<N> n, int sft) {
            BigUInt<N> ret = n.Copy();

            UIntUtil.RightShift(ret.value, sft);

            return ret;
        }

        public static BigUInt<N> LeftBlockShift(BigUInt<N> n, int sft) {
            BigUInt<N> ret = n.Copy();

            UIntUtil.LeftBlockShift(ret.value, sft);

            return ret;
        }

        public static BigUInt<N> RightBlockShift(BigUInt<N> n, int sft) {
            BigUInt<N> ret = n.Copy();

            UIntUtil.RightBlockShift(ret.value, sft);

            return ret;
        }

        public static BigUInt<N> RightRoundShift(BigUInt<N> n, int sft) {
            BigUInt<N> ret = n.Copy();

            UIntUtil.RightRoundShift(ret.value, sft);

            return ret;
        }
    }
}
