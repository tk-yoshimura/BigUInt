using AvxUInt;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AvxUIntTest {
    [TestClass]
    public class TopUInt64Tests {
        [TestMethod]
        public void TopUInt64Test() {
            Random random = new(1234);

            for (int length = 2; length <= 36; length++) {
                for (int i = 0; i <= length * UIntUtil.UInt32Bits; i++) {
                    for (int j = 0; j <= 8; j++) {
                        UInt32[] bits = UIntUtil.Random(random, length, i);
                        (UInt64 n_actual, uint lzc_actual) = UIntUtil.TopUInt64(bits);

                        uint lzc_expected = UIntUtil.LeadingZeroCount(bits);
                        UIntUtil.LeftShift(bits, (int)lzc_expected);
                        UInt64 n_expected = UIntUtil.Pack(bits[^1], bits[^2]);

                        Assert.AreEqual(n_expected, n_actual);
                        Assert.AreEqual(lzc_expected, lzc_actual);
                    }
                }
            }
        }
    }
}
