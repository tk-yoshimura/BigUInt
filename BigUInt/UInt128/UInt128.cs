using System.Diagnostics;

namespace BigUInt {
    [DebuggerDisplay("{ToString(),nq}")]
    public readonly partial struct UInt128 {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly UInt32 e3, e2, e1, e0;

        public UInt128(UInt64 hi, UInt64 lo) {
            (this.e3, this.e2) = UIntUtil.Unpack(hi);
            (this.e1, this.e0) = UIntUtil.Unpack(lo);
        }

        public UInt128(UInt32 e3, UInt32 e2, UInt32 e1, UInt32 e0) {
            this.e3 = e3;
            this.e2 = e2;
            this.e1 = e1;
            this.e0 = e0;
        }

        public UInt64 Hi => UIntUtil.Pack(e3, e2);
        public UInt64 Lo => UIntUtil.Pack(e1, e0);

        public UInt32 E3 => e3;
        public UInt32 E2 => e2;
        public UInt32 E1 => e1;
        public UInt32 E0 => e0;
    }
}