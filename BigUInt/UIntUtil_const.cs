namespace BigUInt {
    internal static partial class UIntUtil {
        public const int UInt32Bits = sizeof(UInt32) * 8;
        public const int UInt64Bits = sizeof(UInt64) * 8;
        public const int UInt32MaxDecimalDigits = UInt32Bits * 30103 / 100000;
        public const int UInt64MaxDecimalDigits = UInt64Bits * 30103 / 100000;
        public const UInt32 UInt32MaxDecimal = 1000000000u;
        public const UInt64 UInt64MaxDecimal = 10000000000000000000ul;

        public const UInt32 UInt32Round = UInt32.MaxValue >> 1;
    }
}
