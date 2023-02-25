using AvxUInt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace AvxUIntTest {
    public static class ShiftTests<N> where N : struct, IConstant {
        public static void LeftShiftTest() {
            Random random = new(1234);

            for (int sft = 0; sft < BigUInt<N>.Bits; sft++) {
                UInt32[] mantissa = UIntUtil.Random(random, BigUInt<N>.Length, BigUInt<N>.Bits - sft);

                BigUInt<N> v = new(mantissa, enable_clone: false);
                BigInteger bi = v;

                BigUInt<N> v_sft = v << sft;
                BigInteger bi_sft = bi << sft;

                Console.WriteLine(sft);
                Console.WriteLine(v.ToHexcode());
                Console.WriteLine(v_sft.ToHexcode());
                Assert.AreEqual(bi_sft, v_sft);

                Console.Write("\n");
            }

            Assert.ThrowsException<OverflowException>(() => {
                BigUInt<N> v = new(1u);

                BigUInt<N> v_sft = BigUInt<N>.LeftBlockShift(v, BigUInt<N>.Length);
            });
        }

        public static void RightShiftTest() {
            Random random = new(1234);

            UInt32[] mantissa = UIntUtil.Random(random, BigUInt<N>.Length, BigUInt<N>.Bits);

            BigUInt<N> v = new(mantissa, enable_clone: false);
            BigInteger bi = v;

            for (int sft = 0; sft <= BigUInt<N>.Bits + 4; sft++) {
                BigUInt<N> v_sft = v >> sft;
                BigInteger bi_sft = bi >> sft;

                Console.WriteLine(sft);
                Console.WriteLine(v.ToHexcode());
                Console.WriteLine(v_sft.ToHexcode());
                Assert.AreEqual(bi_sft, v_sft);

                Console.Write("\n");
            }
        }

        public static void LeftBlockShiftTest() {
            Random random = new(1234);

            for (int sft = 0; sft < BigUInt<N>.Length; sft++) {
                UInt32[] mantissa = UIntUtil.Random(random, BigUInt<N>.Length, BigUInt<N>.Bits - sft * UIntUtil.UInt32Bits);

                BigUInt<N> v = new(mantissa, enable_clone: false);
                BigInteger bi = v;

                BigUInt<N> v_sft = BigUInt<N>.LeftBlockShift(v, sft);
                BigInteger bi_sft = bi << (sft * UIntUtil.UInt32Bits);

                Console.WriteLine(sft);
                Console.WriteLine(v.ToHexcode());
                Console.WriteLine(v_sft.ToHexcode());
                Assert.AreEqual(bi_sft, v_sft);

                Console.Write("\n");
            }

            Assert.ThrowsException<OverflowException>(() => {
                BigUInt<N> v = new(0x12345678u);

                BigUInt<N> v_sft = BigUInt<N>.LeftBlockShift(v, BigUInt<N>.Length);
            });
        }

        public static void RightBlockShiftTest() {
            Random random = new(1234);

            UInt32[] mantissa = UIntUtil.Random(random, BigUInt<N>.Length, BigUInt<N>.Bits);

            BigUInt<N> v = new(mantissa, enable_clone: false);
            BigInteger bi = v;

            for (int sft = 0; sft <= BigUInt<N>.Length + 4; sft++) {
                BigUInt<N> v_sft = BigUInt<N>.RightBlockShift(v, sft);
                BigInteger bi_sft = bi >> (sft * UIntUtil.UInt32Bits);

                Console.WriteLine(sft);
                Console.WriteLine(v.ToHexcode());
                Console.WriteLine(v_sft.ToHexcode());
                Assert.AreEqual(bi_sft, v_sft);

                Console.Write("\n");
            }
        }
    }

    [TestClass]
    public class ShiftTests {
        [TestMethod]
        public void LeftShiftTest() {
            ShiftTests<N4>.LeftShiftTest();
            ShiftTests<N5>.LeftShiftTest();
            ShiftTests<N6>.LeftShiftTest();
            ShiftTests<N7>.LeftShiftTest();
            ShiftTests<N8>.LeftShiftTest();
            ShiftTests<N9>.LeftShiftTest();
            ShiftTests<N10>.LeftShiftTest();
            ShiftTests<N11>.LeftShiftTest();
            ShiftTests<N12>.LeftShiftTest();
            ShiftTests<N13>.LeftShiftTest();
            ShiftTests<N14>.LeftShiftTest();
            ShiftTests<N15>.LeftShiftTest();
            ShiftTests<N16>.LeftShiftTest();
            ShiftTests<N17>.LeftShiftTest();
            ShiftTests<N23>.LeftShiftTest();
            ShiftTests<N24>.LeftShiftTest();
            ShiftTests<N25>.LeftShiftTest();
            ShiftTests<N31>.LeftShiftTest();
            ShiftTests<N32>.LeftShiftTest();
            ShiftTests<N33>.LeftShiftTest();
            ShiftTests<N47>.LeftShiftTest();
            ShiftTests<N48>.LeftShiftTest();
            ShiftTests<N50>.LeftShiftTest();
            ShiftTests<N53>.LeftShiftTest();
            ShiftTests<N56>.LeftShiftTest();
            ShiftTests<N59>.LeftShiftTest();
            ShiftTests<N63>.LeftShiftTest();
            ShiftTests<N64>.LeftShiftTest();
            ShiftTests<N65>.LeftShiftTest();
        }

        [TestMethod]
        public void RightShiftTest() {
            ShiftTests<N4>.RightShiftTest();
            ShiftTests<N5>.RightShiftTest();
            ShiftTests<N6>.RightShiftTest();
            ShiftTests<N7>.RightShiftTest();
            ShiftTests<N8>.RightShiftTest();
            ShiftTests<N9>.RightShiftTest();
            ShiftTests<N10>.RightShiftTest();
            ShiftTests<N11>.RightShiftTest();
            ShiftTests<N12>.RightShiftTest();
            ShiftTests<N13>.RightShiftTest();
            ShiftTests<N14>.RightShiftTest();
            ShiftTests<N15>.RightShiftTest();
            ShiftTests<N16>.RightShiftTest();
            ShiftTests<N17>.RightShiftTest();
            ShiftTests<N23>.RightShiftTest();
            ShiftTests<N24>.RightShiftTest();
            ShiftTests<N25>.RightShiftTest();
            ShiftTests<N31>.RightShiftTest();
            ShiftTests<N32>.RightShiftTest();
            ShiftTests<N33>.RightShiftTest();
            ShiftTests<N47>.RightShiftTest();
            ShiftTests<N48>.RightShiftTest();
            ShiftTests<N50>.RightShiftTest();
            ShiftTests<N53>.RightShiftTest();
            ShiftTests<N56>.RightShiftTest();
            ShiftTests<N59>.RightShiftTest();
            ShiftTests<N63>.RightShiftTest();
            ShiftTests<N64>.RightShiftTest();
            ShiftTests<N65>.RightShiftTest();
        }

        [TestMethod]
        public void LeftBlockShiftTest() {
            ShiftTests<N4>.LeftBlockShiftTest();
            ShiftTests<N5>.LeftBlockShiftTest();
            ShiftTests<N6>.LeftBlockShiftTest();
            ShiftTests<N7>.LeftBlockShiftTest();
            ShiftTests<N8>.LeftBlockShiftTest();
            ShiftTests<N9>.LeftBlockShiftTest();
            ShiftTests<N10>.LeftBlockShiftTest();
            ShiftTests<N11>.LeftBlockShiftTest();
            ShiftTests<N12>.LeftBlockShiftTest();
            ShiftTests<N13>.LeftBlockShiftTest();
            ShiftTests<N14>.LeftBlockShiftTest();
            ShiftTests<N15>.LeftBlockShiftTest();
            ShiftTests<N16>.LeftBlockShiftTest();
            ShiftTests<N17>.LeftBlockShiftTest();
            ShiftTests<N23>.LeftBlockShiftTest();
            ShiftTests<N24>.LeftBlockShiftTest();
            ShiftTests<N25>.LeftBlockShiftTest();
            ShiftTests<N31>.LeftBlockShiftTest();
            ShiftTests<N32>.LeftBlockShiftTest();
            ShiftTests<N33>.LeftBlockShiftTest();
            ShiftTests<N47>.LeftBlockShiftTest();
            ShiftTests<N48>.LeftBlockShiftTest();
            ShiftTests<N50>.LeftBlockShiftTest();
            ShiftTests<N53>.LeftBlockShiftTest();
            ShiftTests<N56>.LeftBlockShiftTest();
            ShiftTests<N59>.LeftBlockShiftTest();
            ShiftTests<N63>.LeftBlockShiftTest();
            ShiftTests<N64>.LeftBlockShiftTest();
            ShiftTests<N65>.LeftBlockShiftTest();
        }

        [TestMethod]
        public void RightBlockShiftTest() {
            ShiftTests<N4>.RightBlockShiftTest();
            ShiftTests<N5>.RightBlockShiftTest();
            ShiftTests<N6>.RightBlockShiftTest();
            ShiftTests<N7>.RightBlockShiftTest();
            ShiftTests<N8>.RightBlockShiftTest();
            ShiftTests<N9>.RightBlockShiftTest();
            ShiftTests<N10>.RightBlockShiftTest();
            ShiftTests<N11>.RightBlockShiftTest();
            ShiftTests<N12>.RightBlockShiftTest();
            ShiftTests<N13>.RightBlockShiftTest();
            ShiftTests<N14>.RightBlockShiftTest();
            ShiftTests<N15>.RightBlockShiftTest();
            ShiftTests<N16>.RightBlockShiftTest();
            ShiftTests<N17>.RightBlockShiftTest();
            ShiftTests<N23>.RightBlockShiftTest();
            ShiftTests<N24>.RightBlockShiftTest();
            ShiftTests<N25>.RightBlockShiftTest();
            ShiftTests<N31>.RightBlockShiftTest();
            ShiftTests<N32>.RightBlockShiftTest();
            ShiftTests<N33>.RightBlockShiftTest();
            ShiftTests<N47>.RightBlockShiftTest();
            ShiftTests<N48>.RightBlockShiftTest();
            ShiftTests<N50>.RightBlockShiftTest();
            ShiftTests<N53>.RightBlockShiftTest();
            ShiftTests<N56>.RightBlockShiftTest();
            ShiftTests<N59>.RightBlockShiftTest();
            ShiftTests<N63>.RightBlockShiftTest();
            ShiftTests<N64>.RightBlockShiftTest();
            ShiftTests<N65>.RightBlockShiftTest();
        }
    }
}