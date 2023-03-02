using AvxUInt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace AvxUIntTest {
    [TestClass]
    public class DecimalTests {
        [TestMethod]
        public void DecimalTest() {
            for (int i = 0; i <= BigUInt<N4>.MaxDecimalDigits; i++) {
                Assert.AreEqual("1" + new string('0', i), BigUInt<N4>.Decimal(i).ToString());
            }
            for (int i = 0; i <= BigUInt<N8>.MaxDecimalDigits; i++) {
                Assert.AreEqual("1" + new string('0', i), BigUInt<N8>.Decimal(i).ToString());
            }
        }
    }
}
