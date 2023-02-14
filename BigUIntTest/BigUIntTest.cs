using BigUInt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace BigUIntTest {
    [TestClass]
    public class UInt128Test {

        [TestMethod]
        public void CreateTest() {
            UInt128 v1 = new(1u, 2u, 3u, 4u);
            UInt128 v2 = new(0x0000000500000006uL, 0x0000000700000008uL);

            Assert.AreEqual(1u, v1.E3);
            Assert.AreEqual(2u, v1.E2);
            Assert.AreEqual(3u, v1.E1);
            Assert.AreEqual(4u, v1.E0);
            Assert.AreEqual(5u, v2.E3);
            Assert.AreEqual(6u, v2.E2);
            Assert.AreEqual(7u, v2.E1);
            Assert.AreEqual(8u, v2.E0);

            Assert.AreEqual(0x0000000100000002uL, v1.Hi);
            Assert.AreEqual(0x0000000300000004uL, v1.Lo);
            Assert.AreEqual(0x0000000500000006uL, v2.Hi);
            Assert.AreEqual(0x0000000700000008uL, v2.Lo);
        }

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
            UInt128 v0 = "0";
            UInt128 v1 = "1";
            UInt128 v2 = "10";
            UInt128 v3 = "100";
            UInt128 v4 = "10000000000000";
            UInt128 v5 = "1000000000000000000000000000";
            UInt128 v6 = "10000000000000000000000000000000000";
            UInt128 v7 = "340282366920938463463374607431768211455";

            Assert.AreEqual("0", v0.ToString());
            Assert.AreEqual("1", v1.ToString());
            Assert.AreEqual("10", v2.ToString());
            Assert.AreEqual("100", v3.ToString());
            Assert.AreEqual("10000000000000", v4.ToString());
            Assert.AreEqual("1000000000000000000000000000", v5.ToString());
            Assert.AreEqual("10000000000000000000000000000000000", v6.ToString());
            Assert.AreEqual("340282366920938463463374607431768211455", v7.ToString());

            Assert.ThrowsException<OverflowException>(() => {
                UInt128 v7 = "340282366920938463463374607431768211456";
            });

            Assert.ThrowsException<OverflowException>(() => {
                UInt128 v7 = "3402823669209384634633746074317682114560";
            });
        }

        [TestMethod]
        public void CmpTest() {
            UInt128 v1 = new(1u, 2u, 3u, 4u);
            UInt128 v2 = new(2u, 2u, 3u, 4u);
            UInt128 v3 = new(1u, 3u, 3u, 4u);
            UInt128 v4 = new(1u, 2u, 4u, 4u);
            UInt128 v5 = new(1u, 2u, 3u, 5u);
            UInt128 v6 = new(1u, 2u, 3u, 4u);

            Assert.IsTrue(v1 < v2);
            Assert.IsTrue(v1 < v3);
            Assert.IsTrue(v1 < v4);
            Assert.IsTrue(v1 < v5);
            Assert.IsTrue(v2 > v3);
            Assert.IsTrue(v2 > v4);
            Assert.IsTrue(v2 > v5);
            Assert.IsTrue(v3 > v4);
            Assert.IsTrue(v3 > v5);
            Assert.IsTrue(v4 > v5);

            Assert.IsTrue(v1 <= v2);
            Assert.IsTrue(v1 <= v3);
            Assert.IsTrue(v1 <= v4);
            Assert.IsTrue(v1 <= v5);
            Assert.IsTrue(v2 >= v3);
            Assert.IsTrue(v2 >= v4);
            Assert.IsTrue(v2 >= v5);
            Assert.IsTrue(v3 >= v4);
            Assert.IsTrue(v3 >= v5);
            Assert.IsTrue(v4 >= v5);

            Assert.IsFalse(v1 > v2);
            Assert.IsFalse(v1 > v3);
            Assert.IsFalse(v1 > v4);
            Assert.IsFalse(v1 > v5);
            Assert.IsFalse(v2 < v3);
            Assert.IsFalse(v2 < v4);
            Assert.IsFalse(v2 < v5);
            Assert.IsFalse(v3 < v4);
            Assert.IsFalse(v3 < v5);
            Assert.IsFalse(v4 < v5);

            Assert.IsFalse(v1 >= v2);
            Assert.IsFalse(v1 >= v3);
            Assert.IsFalse(v1 >= v4);
            Assert.IsFalse(v1 >= v5);
            Assert.IsFalse(v2 <= v3);
            Assert.IsFalse(v2 <= v4);
            Assert.IsFalse(v2 <= v5);
            Assert.IsFalse(v3 <= v4);
            Assert.IsFalse(v3 <= v5);
            Assert.IsFalse(v4 <= v5);

            Assert.IsTrue(v1 == v6);
            Assert.IsFalse(v1 != v6);
            Assert.IsTrue(v1 <= v6);
            Assert.IsTrue(v1 >= v6);
        }

        [TestMethod]
        public void RightShiftTest() {
            BigInteger v = BigInteger.Parse("234567891234567891234567891234567891234");

            for (int sft = 0; sft <= 130; sft++) {
                BigInteger u = v >> sft;
                UInt128 w = (UInt128)v.ToString() >> sft;

                Assert.AreEqual(u.ToString(), w.ToString(), $"{sft}");
            }

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                UInt128 b = new(0u, 0u, 0u, 123456u);

                UInt128 c = b >> -1;
            });
        }

        [TestMethod]
        public void LeftShiftTest() {
            BigInteger v = BigInteger.Parse("234567891234567891234567891234567891234");

            for (int sft = 0; sft <= 130; sft++) {
                BigInteger u = (v >> sft) << sft;
                UInt128 w = (UInt128)(v >> sft).ToString() << sft;

                Assert.AreEqual(u.ToString(), w.ToString(), $"{sft}");
            }

            for (int sft = 1; sft <= 127; sft++) {
                Assert.ThrowsException<OverflowException>(() => {
                    UInt128 w = (UInt128)(v >> (sft - 1)).ToString() << sft;
                });
            }

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                UInt128 b = new(0u, 0u, 0u, 123456u);

                UInt128 c = b << -1;
            });
        }

        [TestMethod]
        public void AddTest() {
            UInt128 v1 = new(1u, 0u, 0u, 0u), v2 = new(0u, ~0u, ~0u, ~0u);

            Assert.AreEqual(new UInt128(1u, ~0u, ~0u, ~0u), v1 + v2);

            BigInteger maxvalue = (((BigInteger)UInt64.MaxValue) << 64) + UInt64.MaxValue;
            BigInteger v = maxvalue;
            List<BigInteger> vs = new();
            while (v > 0) {
                vs.Add(v);
                v /= 7;
            }
            vs.Add(0);

            for (int i = 0; i < vs.Count; i++) { 
                for (int j = 0; j < vs.Count; j++) {
                    UInt128 i0 = vs[i].ToString(), i1 = vs[j].ToString();

                    Console.WriteLine($"{i0} + {i1}");

                    if (vs[i] + vs[j] > maxvalue) {
                        Assert.ThrowsException<OverflowException>(() => {
                            UInt128 _ = i0 + i1;
                        }, $"{i0} + {i1}");
                    }
                    else {
                        Assert.AreEqual((vs[i] + vs[j]).ToString(), (i0 + i1).ToString(), $"{i0} + {i1}");
                    }
                }
            }
        }

        [TestMethod]
        public void SubTest() {
            UInt128 v1 = new(1u, 0u, 0u, 0u), v2 = new(0u, ~0u, ~0u, ~0u), v3 = new(1u, 0u, 0u, 1u);

            Assert.AreEqual((UInt128)1, v1 - v2);

            Assert.ThrowsException<OverflowException>(() => {
                UInt128 _ = v1 - v3;
            });

            BigInteger v = (((BigInteger)UInt64.MaxValue) << 64) + UInt64.MaxValue;
            List<BigInteger> vs = new();
            while (v > 0) {
                vs.Add(v);
                v /= 7;
            }
            vs.Add(0);

            for (int i = 0; i < vs.Count; i++) { 
                for (int j = 0; j < vs.Count; j++) {
                    UInt128 i0 = vs[i].ToString(), i1 = vs[j].ToString();

                    Console.WriteLine($"{i0} - {i1}");

                    if (i0 < i1) {
                        Assert.ThrowsException<OverflowException>(() => {
                            UInt128 _ = i0 - i1;
                        }, $"{i0} - {i1}");
                    }
                    else {
                        Assert.AreEqual((vs[i] - vs[j]).ToString(), (i0 - i1).ToString(), $"{i0} - {i1}");
                    }
                }
            }
        }

        [TestMethod]
        public void MulTest() {
            UInt128 v1 = new(0u, 1u, 0u, 0u), v2 = new(0u, 0u, ~0u, ~0u);
            
            Assert.AreEqual(new UInt128(~0u, ~0u, 0u, 0u), v1 * v2);

            BigInteger maxvalue = (((BigInteger)UInt64.MaxValue) << 64) + UInt64.MaxValue;
            BigInteger v = maxvalue;
            List<BigInteger> vs = new();
            while (v > 0) {
                vs.Add(v);
                v /= 3;
            }
            vs.Add(0);

            for (int i = 0; i < vs.Count; i++) { 
                for (int j = 0; j < vs.Count; j++) {
                    UInt128 i0 = vs[i].ToString(), i1 = vs[j].ToString();

                    Console.WriteLine($"{i0} * {i1}");

                    if (vs[i] * vs[j] > maxvalue) {
                        Assert.ThrowsException<OverflowException>(() => {
                            UInt128 _ = i0 * i1;
                        }, $"{i0} * {i1}");
                    }
                    else {
                        Assert.AreEqual((vs[i] * vs[j]).ToString(), (i0 * i1).ToString(), $"{i0} * {i1}");
                    }
                }
            }
        }
    }
}