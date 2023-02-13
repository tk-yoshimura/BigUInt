using BigUInt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace BigUIntTest {
    [TestClass]
    public class UInt128Test {
        [TestMethod]
        public void BigIntegarTest() {
            BigInteger v = (((BigInteger)UInt64.MaxValue) << 64) + UInt64.MaxValue;
            BigInteger mask = UInt64.MaxValue;

            while (v > 0) {
                UInt128 u = new((UInt64)((v >> 64) & mask), (UInt64)(v & mask));
                UInt128 w = new UInt128(u.ToString());

                Console.WriteLine(v);

                Assert.AreEqual(v.ToString(), u.ToString());
                Assert.AreEqual(w, u);

                v /= 3;
            }
        }

        [TestMethod]
        public void ToStringTest() {
            UInt128 v0 = new(0uL, 0uL);
            UInt128 v1 = new(0uL, 1uL);
            UInt128 v2 = new(0uL, 10uL);
            UInt128 v3 = new(0uL, 100uL);
            UInt128 v4 = new(0uL, 10000000000000uL);
            UInt128 v5 = new(0x00000000033b2e3cuL, 0x9fd0803ce8000000uL);
            UInt128 v6 = new(0x0001ed09bead87c0uL, 0x378d8e6400000000uL);
            UInt128 v7 = new(UInt64.MaxValue, UInt64.MaxValue);

            Assert.AreEqual("0", v0.ToString());
            Assert.AreEqual("1", v1.ToString());
            Assert.AreEqual("10", v2.ToString());
            Assert.AreEqual("100", v3.ToString());
            Assert.AreEqual("10000000000000", v4.ToString());
            Assert.AreEqual("1000000000000000000000000000", v5.ToString());
            Assert.AreEqual("10000000000000000000000000000000000", v6.ToString());
            Assert.AreEqual("340282366920938463463374607431768211455", v7.ToString());
        }

        [TestMethod]
        public void ParseTest() {
            UInt128 v0 = new("0");
            UInt128 v1 = new("1");
            UInt128 v2 = new("10");
            UInt128 v3 = new("100");
            UInt128 v4 = new("10000000000000");
            UInt128 v5 = new("1000000000000000000000000000");
            UInt128 v6 = new("10000000000000000000000000000000000");
            UInt128 v7 = new("340282366920938463463374607431768211455");

            Assert.AreEqual("0", v0.ToString());
            Assert.AreEqual("1", v1.ToString());
            Assert.AreEqual("10", v2.ToString());
            Assert.AreEqual("100", v3.ToString());
            Assert.AreEqual("10000000000000", v4.ToString());
            Assert.AreEqual("1000000000000000000000000000", v5.ToString());
            Assert.AreEqual("10000000000000000000000000000000000", v6.ToString());
            Assert.AreEqual("340282366920938463463374607431768211455", v7.ToString());

            Assert.ThrowsException<OverflowException>(() => {
                UInt128 v7 = new("340282366920938463463374607431768211456");
            });

            Assert.ThrowsException<OverflowException>(() => {
                UInt128 v7 = new("3402823669209384634633746074317682114560");
            });
        }
    }
}