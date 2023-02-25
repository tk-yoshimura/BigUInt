using AvxUInt;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AvxUIntTest {
    [TestClass]
    public class LeadingZeroCountTests {
        [TestMethod]
        public void LeadingZeroCountTest() {
            BigUInt<N65> n = BigUInt<N65>.Full;

            int lzc = 0;
            while (n > 0) {
                Assert.AreEqual(lzc, n.LeadingZeroCount);
                n >>= 1;
                lzc++;
            }
            Assert.AreEqual(lzc, n.LeadingZeroCount);
        }
    }
}
