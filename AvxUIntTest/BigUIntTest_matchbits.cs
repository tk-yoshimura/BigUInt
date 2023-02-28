using AvxUInt;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AvxUIntTest {
    [TestClass]
    public class MatchBitsTests {
        [TestMethod]
        public void MatchBitsTest() {
            Random random = new(1234);

            for (int length = 1; length <= 36; length++) {
                for (int i = 0; i < 40; i++) {
                    UInt32[] bits = UIntUtil.Random(random, length, random.Next(length * UIntUtil.UInt32Bits + 1));
                    UInt32[] bits_swapbit = (UInt32[])bits.Clone();

                    int idx1 = random.Next(length), idx2 = random.Next(UIntUtil.UInt32Bits);
                    int swapbit = (length - 1 - idx1) * UIntUtil.UInt32Bits + (UIntUtil.UInt32Bits - 1 - idx2);

                    bits_swapbit[idx1] ^= 1u << idx2;

                    Assert.AreEqual((uint)swapbit, UIntUtil.MatchBits((uint)length, bits, bits_swapbit));
                }
            }
        }
    }
}
