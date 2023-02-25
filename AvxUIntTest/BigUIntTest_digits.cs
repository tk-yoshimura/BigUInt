using AvxUInt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AvxUIntTest {
    [TestClass]
    public class DigitsTests {
        [TestMethod]
        public void DigitsTest() {
            BigUInt<Pow2.N4> n1 = new(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu }, enable_clone: false);
            BigUInt<Pow2.N4> n2 = new(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x7FFFFFFFu }, enable_clone: false);
            BigUInt<Pow2.N4> n3 = new(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x3FFFFFFFu }, enable_clone: false);
            BigUInt<Pow2.N4> n4 = new(new UInt32[] { 0xFFFFFFFFu, 0u, 0xFFFFFFFFu, 0x3FFFFFFFu }, enable_clone: false);
            BigUInt<Pow2.N4> n5 = new(new UInt32[] { 0x7FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu }, enable_clone: false);
            BigUInt<Pow2.N4> n6 = new();
            BigUInt<Pow2.N4> n7 = new(1u);
            BigUInt<Pow2.N4> n8 = new(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0x7FFFFFFFu, 0u }, enable_clone: false);

            BigUInt<N13> n9 = new(new UInt32[] { 
                0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 
                0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 
                0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 
                0xFFFFFFFFu 
            }, enable_clone: false);

            BigUInt<N13> n10 = new(new UInt32[] { 
                0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 
                0xFFFFFFFFu, 0xFFFFFFFFu, 0, 0, 
                0, 0, 0, 0, 
                0
            }, enable_clone: false);

            BigUInt<N13> n11 = new(new UInt32[] { 
                0xFFFFFFFFu, 0xFFFFFFFFu, 0, 0, 
                0, 0, 0, 0, 
                0, 0, 0, 0, 
                0
            }, enable_clone: false);

            Assert.AreEqual(4, n1.Digits);
            Assert.AreEqual(4, n2.Digits);
            Assert.AreEqual(4, n3.Digits);
            Assert.AreEqual(4, n4.Digits);
            Assert.AreEqual(4, n5.Digits);
            Assert.AreEqual(1, n6.Digits);
            Assert.AreEqual(1, n7.Digits);
            Assert.AreEqual(3, n8.Digits);
            Assert.AreEqual(13, n9.Digits);
            Assert.AreEqual(6, n10.Digits);
            Assert.AreEqual(2, n11.Digits);
        }
    }
}
