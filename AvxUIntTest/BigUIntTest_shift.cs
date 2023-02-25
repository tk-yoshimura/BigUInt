using AvxUInt;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Numerics;

namespace AvxUIntTest {
    [TestClass]
    public class ShiftTests {

        [TestMethod]
        public void LeftShiftTest() {
            Random random = new(1234);

            for (int sft = 0; sft < BigUInt<Pow2.N32>.Bits; sft++) {
                UInt32[] mantissa = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, BigUInt<Pow2.N32>.Bits - sft);

                BigUInt<Pow2.N32> v = new(mantissa, enable_clone: false);
                BigInteger bi = v;

                BigUInt<Pow2.N32> v_sft = v << sft;
                BigInteger bi_sft = bi << sft;

                Console.WriteLine(sft);
                Console.WriteLine(v.ToHexcode());
                Console.WriteLine(v_sft.ToHexcode());
                Assert.AreEqual(bi_sft, v_sft);

                Console.Write("\n");
            }

            Assert.ThrowsException<OverflowException>(() => {
                BigUInt<Pow2.N32> v = new(1u);

                BigUInt<Pow2.N32> v_sft = BigUInt<Pow2.N32>.LeftBlockShift(v, BigUInt<Pow2.N32>.Length);
            });
        }

        [TestMethod]
        public void RightShiftTest() {
            Random random = new(1234);

            UInt32[] mantissa = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, BigUInt<Pow2.N32>.Bits);

            BigUInt<Pow2.N32> v = new(mantissa, enable_clone: false);
            BigInteger bi = v;

            for (int sft = 0; sft <= BigUInt<Pow2.N32>.Bits + 4; sft++) {
                BigUInt<Pow2.N32> v_sft = v >> sft;
                BigInteger bi_sft = bi >> sft;

                Console.WriteLine(sft);
                Console.WriteLine(v.ToHexcode());
                Console.WriteLine(v_sft.ToHexcode());
                Assert.AreEqual(bi_sft, v_sft);

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void LeftBlockShiftTest() {
            Random random = new(1234);

            for (int sft = 0; sft < BigUInt<Pow2.N32>.Length; sft++) {
                UInt32[] mantissa = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, BigUInt<Pow2.N32>.Bits - sft * UIntUtil.UInt32Bits);

                BigUInt<Pow2.N32> v = new(mantissa, enable_clone: false);
                BigInteger bi = v;

                BigUInt<Pow2.N32> v_sft = BigUInt<Pow2.N32>.LeftBlockShift(v, sft);
                BigInteger bi_sft = bi << (sft * UIntUtil.UInt32Bits);

                Console.WriteLine(sft);
                Console.WriteLine(v.ToHexcode());
                Console.WriteLine(v_sft.ToHexcode());
                Assert.AreEqual(bi_sft, v_sft);

                Console.Write("\n");
            }

            Assert.ThrowsException<OverflowException>(() => {
                BigUInt<Pow2.N32> v = new(0x12345678u);

                BigUInt<Pow2.N32> v_sft = BigUInt<Pow2.N32>.LeftBlockShift(v, BigUInt<Pow2.N32>.Length);
            });
        }

        [TestMethod]
        public void RightBlockShiftTest() {
            Random random = new(1234);

            UInt32[] mantissa = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, BigUInt<Pow2.N32>.Bits);

            BigUInt<Pow2.N32> v = new(mantissa, enable_clone: false);
            BigInteger bi = v;

            for (int sft = 0; sft <= BigUInt<Pow2.N32>.Length + 4; sft++) {
                BigUInt<Pow2.N32> v_sft = BigUInt<Pow2.N32>.RightBlockShift(v, sft);
                BigInteger bi_sft = bi >> (sft * UIntUtil.UInt32Bits);

                Console.WriteLine(sft);
                Console.WriteLine(v.ToHexcode());
                Console.WriteLine(v_sft.ToHexcode());
                Assert.AreEqual(bi_sft, v_sft);

                Console.Write("\n");
            }
        }
    }
}
