using System.Diagnostics;

namespace BigUInt {
    [DebuggerDisplay("{ToString(),nq}")]
    public readonly struct UInt128 {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly UInt32 e3, e2, e1, e0;

        public UInt128(UInt64 hi, UInt64 lo) {
            (this.e3, this.e2) = UIntUtil.Unpack(hi);
            (this.e1, this.e0) = UIntUtil.Unpack(lo);
        }

        public UInt64 Hi => UIntUtil.Pack(e3, e2);
        public UInt64 Lo => UIntUtil.Pack(e1, e0);

        public UInt32 E3 => e3;
        public UInt32 E2 => e2;
        public UInt32 E1 => e1;
        public UInt32 E0 => e0;

        public override string ToString() {
            UInt32 carry, dec0, dec1, dec2, dec3, dec4;

            (dec1, dec0) = UIntUtil.DecimalUnpack(e3);

            (carry, dec0) = UIntUtil.DecimalUnpack(UIntUtil.Pack(dec0, e2));
            (dec2, dec1) = UIntUtil.DecimalUnpack(UIntUtil.Pack(dec1, carry));

            (carry, dec0) = UIntUtil.DecimalUnpack(UIntUtil.Pack(dec0, e1));
            (carry, dec1) = UIntUtil.DecimalUnpack(UIntUtil.Pack(dec1, carry));
            (dec3, dec2) = UIntUtil.DecimalUnpack(UIntUtil.Pack(dec2, carry));

            (carry, dec0) = UIntUtil.DecimalUnpack(UIntUtil.Pack(dec0, e0));
            (carry, dec1) = UIntUtil.DecimalUnpack(UIntUtil.Pack(dec1, carry));
            (carry, dec2) = UIntUtil.DecimalUnpack(UIntUtil.Pack(dec2, carry));
            (dec4, dec3) = UIntUtil.DecimalUnpack(UIntUtil.Pack(dec3, carry));

            string str = $"{dec4:D9}{dec3:D9}{dec2:D9}{dec1:D9}{dec0:D9}".TrimStart('0');
            str = (str != string.Empty) ? str : "0";

            return str;
        }
    }
}