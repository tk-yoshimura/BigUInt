namespace BigUInt {
    public readonly partial struct UInt128 {

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