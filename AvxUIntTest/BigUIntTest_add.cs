using AvxUInt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace AvxUIntTest {
    public static class AddTests<N> where N : struct, IConstant {
        public static void AddTest() {
            Random random = new(1234);

            List<(BigUInt<N> b, BigInteger n)> vs = new();

            for (int i = 1; i <= BigUInt<N>.Bits; i += 15) { 
                UInt32[] bits = UIntUtil.Random(random, BigUInt<N>.Length, i);
                BigUInt<N> v = new(bits, enable_clone: false);
                vs.Add((v, v));
            }
            {
                BigUInt<N> v = BigUInt<N>.Full;
                while (v > 0) {
                    vs.Add((v, v));
                    v >>= 15;
                }
            }

            BigInteger maxn = BigUInt<N>.Full;

            Console.WriteLine($"length={BigUInt<N>.Length}");

            foreach((BigUInt<N> v1, BigInteger n1) in vs) {
                foreach ((BigUInt<N> v2, BigInteger n2) in vs) {
                    BigInteger n = n1 + n2;

                    Console.WriteLine($"{n1} + {n2}");

                    if (n <= maxn) {
                        Assert.AreEqual(n, (BigInteger)(v1 + v2));
                    }
                    else {
                        Assert.ThrowsException<OverflowException>(() => {
                            _ = v1 + v2;
                        });
                    }
                }
            }

            Assert.AreEqual(BigUInt<N>.Zero, new BigUInt<N>("0"));
        }

        public static void AddFullTest() {
            BigUInt<N> v = BigUInt<N>.Full;
            BigUInt<N> v2 = new(v.ToString());

            Assert.AreEqual(v, v2);
        }
    }


    [TestClass]
    public class AddTests {
        [TestMethod]
        public void AddTest() {
            AddTests<N4>.AddTest();
            AddTests<N5>.AddTest();
            AddTests<N6>.AddTest();
            AddTests<N7>.AddTest();
            AddTests<N8>.AddTest();
            AddTests<N9>.AddTest();
            AddTests<N10>.AddTest();
            AddTests<N11>.AddTest();
            AddTests<N12>.AddTest();
            AddTests<N13>.AddTest();
            AddTests<N14>.AddTest();
            AddTests<N15>.AddTest();
            AddTests<N16>.AddTest();
            AddTests<N17>.AddTest();
            AddTests<N23>.AddTest();
            AddTests<N24>.AddTest();
            AddTests<N25>.AddTest();
            AddTests<N31>.AddTest();
            AddTests<N32>.AddTest();
            AddTests<N33>.AddTest();
            AddTests<N47>.AddTest();
            AddTests<N48>.AddTest();
            AddTests<N50>.AddTest();
            AddTests<N53>.AddTest();
            AddTests<N56>.AddTest();
            AddTests<N59>.AddTest();
            AddTests<N63>.AddTest();
            AddTests<N64>.AddTest();
            AddTests<N65>.AddTest();
        }
    }
}
