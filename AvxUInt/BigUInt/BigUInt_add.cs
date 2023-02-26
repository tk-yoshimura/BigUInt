﻿namespace AvxUInt {
    public sealed partial class BigUInt<N> {

        public static BigUInt<N> operator +(BigUInt<N> a, BigUInt<N> b) {
            return Add(a, b);
        }

        public static BigUInt<N> operator +(BigUInt<N> a, UInt32 b) {
            return Add(a, b);
        }

        public static BigUInt<N> operator +(UInt32 a, BigUInt<N> b) {
            return Add(b, a);
        }

        public static BigUInt<N> operator +(BigUInt<N> a, UInt64 b) {
            return Add(a, b);
        }

        public static BigUInt<N> operator +(UInt64 a, BigUInt<N> b) {
            return Add(b, a);
        }

        private static BigUInt<N> Add(BigUInt<N> v1, BigUInt<N> v2) {
            int v1_digits = v1.Digits, v2_digits = v2.Digits;

            if (v1_digits >= v2_digits) {
                BigUInt<N> ret = v1.Copy();

                UIntUtil.Add(ret.value, v2.value);

                return ret;
            }
            else {
                BigUInt<N> ret = v2.Copy();

                UIntUtil.Add(ret.value, v1.value);

                return ret;
            }
        }

        private static BigUInt<N> Add(BigUInt<N> v1, UInt32 v2) {
            BigUInt<N> ret = v1.Copy();

            UIntUtil.Add(ret.value, v2);

            return ret;
        }

        private static BigUInt<N> Add(BigUInt<N> v1, UInt64 v2) {
            BigUInt<N> ret = v1.Copy();

            UInt32[] v2_arr = new UInt32[2];
            (v2_arr[1], v2_arr[0]) = UIntUtil.Unpack(v2);

            UIntUtil.Add(ret.value, v2_arr);

            return ret;
        }
    }
}