using System.Diagnostics;

namespace BigUInt {
    [DebuggerDisplay("{ToString(),nq}")]
    public readonly partial struct UInt128 {
        //[DebuggerBrowsable(DebuggerBrowsableState.Never)]
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

        public static implicit operator UInt128(string s) {
            return new UInt128(s);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public UInt64 Hi => UIntUtil.Pack(e3, e2);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public UInt64 Lo => UIntUtil.Pack(e1, e0);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public UInt32 E3 => e3;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public UInt32 E2 => e2;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public UInt32 E1 => e1;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public UInt32 E0 => e0;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static UInt128 Zero { get; } = new(0u, 0u, 0u, 0u);

        public static bool operator ==(UInt128 a, UInt128 b) {
            return a.E3 == b.E3 && a.E2 == b.E2 && a.E1 == b.E1 && a.E0 == b.E0;
        }

        public static bool operator !=(UInt128 a, UInt128 b) {
            return !(a == b);
        }

        public static bool operator <(UInt128 a, UInt128 b) {
            return
                a.E3 < b.E3 ? true : a.E3 > b.E3 ? false :
                a.E2 < b.E2 ? true : a.E2 > b.E2 ? false :
                a.E1 < b.E1 ? true : a.E1 > b.E1 ? false :
                a.E0 < b.E0;
        }

        public static bool operator <=(UInt128 a, UInt128 b) {
            return
                a.E3 < b.E3 ? true : a.E3 > b.E3 ? false :
                a.E2 < b.E2 ? true : a.E2 > b.E2 ? false :
                a.E1 < b.E1 ? true : a.E1 > b.E1 ? false :
                a.E0 <= b.E0;
        }

        public static bool operator >(UInt128 a, UInt128 b) {
            return
                a.E3 > b.E3 ? true : a.E3 < b.E3 ? false :
                a.E2 > b.E2 ? true : a.E2 < b.E2 ? false :
                a.E1 > b.E1 ? true : a.E1 < b.E1 ? false :
                a.E0 > b.E0;
        }

        public static bool operator >=(UInt128 a, UInt128 b) {
            return
                a.E3 > b.E3 ? true : a.E3 < b.E3 ? false :
                a.E2 > b.E2 ? true : a.E2 < b.E2 ? false :
                a.E1 > b.E1 ? true : a.E1 < b.E1 ? false :
                a.E0 >= b.E0;
        }

        public override bool Equals(object? obj) {
            return obj is not null && obj is UInt128 v && v == this;
        }

        public override int GetHashCode() {
            return (int)unchecked(E3 ^ E2 ^ E1 ^ E0);
        }
    }
}