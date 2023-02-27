namespace AvxUInt {
    internal static partial class UIntUtil {
        
        /// <summary>Operate uint32 array q += a / b, a = a % b</summary>
        public static void DivRem(UInt32[] arr_q, UInt32[] arr_a, UInt32[] arr_b) {
            uint digits_b = (uint)Digits(arr_b);

            UInt64 div = RoundDenom(digits_b, arr_b);

            if (div == 0uL) {
                throw new DivideByZeroException();
            }

            int length = arr_a.Length;
            int lzc = LeadingZeroCount(arr_a);
            int r_offset = lzc, b_offset = LeadingZeroCount(arr_b) + (arr_a.Length - arr_b.Length) * UInt32Bits;

            while (r_offset < b_offset) {
                int sft = b_offset - r_offset - UInt32Bits;
                LeftShift(arr_a, lzc);

                UInt64 n = Pack(arr_a[length - 1], arr_a[length - 2]) / div;
                if (sft < 0) {
                    n >>= -sft;
                    sft = 0;
                }

                uint sft_block = (uint)sft / UInt32Bits;
                int sft_rem = sft % UInt32Bits;
                
                (UInt32 n_hi, UInt32 n_lo) = Unpack(n << sft_rem);

                Add(sft_block, arr_q, n_lo);
                Add(sft_block + 1, arr_q, n_hi);

                Fms(sft_block, digits_b, arr_a, arr_b, n_lo);
                Fms(sft_block + 1, digits_b, arr_a, arr_b, n_hi);

                lzc = LeadingZeroCount(arr_a);

                if (lzc >= arr_a.Length * UInt32Bits) {
                    break;
                }

                r_offset += lzc;
            }

            RightShift(arr_a, r_offset);

            uint digits_a = (uint)Digits(arr_a);

            if (digits_a > digits_b || ((digits_a == digits_b) && GreaterThanOrEqual(digits_a, arr_a[..(int)digits_a], arr_b[..(int)digits_a]))) {
                Add(arr_q, 1u);
                Sub(arr_a, arr_b);
            }
        }

        /// <summary>Operate uint32 array q += a / b, a = a % b</summary>
        public static void DivRem(UInt32[] arr_q, UInt32[] arr_a, UInt32 b) {
            DivRem(arr_q, arr_a, new UInt32[] { b });
        }

        /// <summary>Operate uint32 array q += a / b, a = a % b</summary>
        public static void DivRem(UInt32[] arr_q, UInt32[] arr_a, UInt64 b) {
            (UInt32 b_hi, UInt32 b_lo) = Unpack(b);

            DivRem(arr_q, arr_a, new UInt32[] { b_lo, b_hi });
        }

        private static UInt64 RoundDenom(uint digits_b, UInt32[] arr_b) {
#if DEBUG
            if (digits_b != Digits(arr_b)) {
                throw new ArgumentException($"mismatch digits of {nameof(arr_b)}", nameof(digits_b));
            }
#endif

            if (digits_b == 0u) {
                return 0u;
            }

            UInt32 n0 = arr_b[digits_b - 1];
            int lzc = LeadingZeroCount(n0);
            
            if (digits_b == 1u) {
                n0 <<= lzc;

                return n0;
            }
            else {
                UInt32 n1 = arr_b[digits_b - 2];
                (UInt64 n_hi, UInt32 n_lo) = Unpack(Pack(n0, n1) << lzc);

                if ((n_lo > 0u) || !IsZero(arr_b[..(int)(digits_b - 2)])){
                    return n_hi + 1uL;
                }

                return n_hi;
            }
        }
    }
}
