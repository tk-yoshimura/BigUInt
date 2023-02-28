﻿namespace AvxUInt {
    public sealed partial class BigUInt<N> {

        public static BigUInt<N> operator -(BigUInt<N> a, BigUInt<N> b) {
            return Sub(a, b);
        }

        public static BigUInt<N> operator -(BigUInt<N> a, UInt32 b) {
            return Sub(a, b);
        }

        public static BigUInt<N> operator -(UInt32 a, BigUInt<N> b) {
            return Sub(a, b);
        }

        public static BigUInt<N> operator -(BigUInt<N> a, UInt64 b) {
            return Sub(a, b);
        }

        public static BigUInt<N> operator -(UInt64 a, BigUInt<N> b) {
            return Sub(a, b);
        }

        public static BigUInt<N> Sub(BigUInt<N> a, BigUInt<N> b) {
            BigUInt<N> ret = a.Copy();

            UIntUtil.Sub(ret.value, b.value);

            return ret;
        }

        public static BigUInt<N> Sub(BigUInt<N> a, UInt32 b) {
            BigUInt<N> ret = a.Copy();

            UIntUtil.Sub(ret.value, b);

            return ret;
        }

        public static BigUInt<N> Sub(BigUInt<N> a, UInt64 b) {
            BigUInt<N> ret = a.Copy();

            UIntUtil.Sub(ret.value, b);

            return ret;
        }

        public static BigUInt<N> Sub(UInt32 a, BigUInt<N> b) {
            if (b.Digits > 1) {
                throw new OverflowException();
            }

            UInt32 n = b[0];
            if (a < n) {
                throw new OverflowException();
            }

            return a - n;
        }

        public static BigUInt<N> Sub(UInt64 a, BigUInt<N> b) {
            if (b.Digits > 2) {
                throw new OverflowException();
            }

            UInt64 n = UIntUtil.Pack(b[1], b[0]);
            if (a < n) {
                throw new OverflowException();
            }

            return a - n;
        }
    }
}
