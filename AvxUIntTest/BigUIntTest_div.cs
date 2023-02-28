using AvxUInt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace AvxUIntTest {
    public static class DivTests<N> where N : struct, IConstant {
        public static void DivTest() {
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

            vs.Add((1, 1));

            BigInteger maxn = BigUInt<N>.Full;

            Console.WriteLine($"length={BigUInt<N>.Length}");

            int normal_passes = 0, divzero_passes = 0;

            foreach ((BigUInt<N> v1, BigInteger n1) in vs) {
                foreach ((BigUInt<N> v2, BigInteger n2) in vs) {
                    Console.WriteLine($"{n1}/{n2}");

                    if (n2 > 0) {
                        BigInteger q = n1 / n2, r = n1 % n2;
    
                        Assert.AreEqual(q, (BigInteger)(v1 / v2));
                        Assert.AreEqual(r, (BigInteger)(v1 % v2));

                        normal_passes++;
                    }
                    else {
                        Assert.ThrowsException<DivideByZeroException>(() => {
                            _ = v1 / v2;
                        });
                        Assert.ThrowsException<DivideByZeroException>(() => {
                            _ = v1 % v2;
                        });

                        divzero_passes++;
                    }
                }
            }

            Console.WriteLine($"{nameof(normal_passes)}: {normal_passes}");
            Console.WriteLine($"{nameof(divzero_passes)}: {divzero_passes}");
        }

        public static void DivFullTest() {
            List<(BigUInt<N> v1, BigUInt<N> v2, BigInteger n1, BigInteger n2)> vs = new();
            BigInteger maxn = BigUInt<N>.Full, v = maxn / 2;

            while (v > 1) {
                BigInteger u = maxn / v;

                BigInteger v0 = v - 1, v1 = v, v2 = v + 1;
                BigInteger u0 = u - 1, u1 = u, u2 = u + 1;

                vs.Add((BigUInt<N>.Parse(v0.ToString()), BigUInt<N>.Parse(u0.ToString()), v0, u0));
                vs.Add((BigUInt<N>.Parse(v0.ToString()), BigUInt<N>.Parse(u1.ToString()), v0, u1));
                vs.Add((BigUInt<N>.Parse(v0.ToString()), BigUInt<N>.Parse(u2.ToString()), v0, u2));

                vs.Add((BigUInt<N>.Parse(v1.ToString()), BigUInt<N>.Parse(u0.ToString()), v1, u0));
                vs.Add((BigUInt<N>.Parse(v1.ToString()), BigUInt<N>.Parse(u1.ToString()), v1, u1));
                vs.Add((BigUInt<N>.Parse(v1.ToString()), BigUInt<N>.Parse(u2.ToString()), v1, u2));

                vs.Add((BigUInt<N>.Parse(v2.ToString()), BigUInt<N>.Parse(u0.ToString()), v2, u0));
                vs.Add((BigUInt<N>.Parse(v2.ToString()), BigUInt<N>.Parse(u1.ToString()), v2, u1));
                vs.Add((BigUInt<N>.Parse(v2.ToString()), BigUInt<N>.Parse(u2.ToString()), v2, u2));

                v >>= 1;
            }

            Console.WriteLine($"length={BigUInt<N>.Length}");

            int normal_passes = 0, divzero_passes = 0;

            foreach ((BigUInt<N> v1, BigUInt<N> v2, BigInteger n1, BigInteger n2) in vs) {
                if (n2 > 0) {
                    BigInteger q = n1 / n2, r = n1 % n2;
    
                    Assert.AreEqual(q, (BigInteger)(v1 / v2));
                    Assert.AreEqual(r, (BigInteger)(v1 % v2));

                    normal_passes++;
                }
                else {
                    Assert.ThrowsException<DivideByZeroException>(() => {
                        _ = v1 / v2;
                    });
                    Assert.ThrowsException<DivideByZeroException>(() => {
                        _ = v1 % v2;
                    });

                    divzero_passes++;
                }
            }

            Console.WriteLine($"{nameof(normal_passes)}: {normal_passes}");
            Console.WriteLine($"{nameof(divzero_passes)}: {divzero_passes}");
        }
    }

    [TestClass]
    public class DivTests {
        [TestMethod]
        public void DivTest() {
            DivTests<N4>.DivTest();
            DivTests<N5>.DivTest();
            DivTests<N6>.DivTest();
            DivTests<N7>.DivTest();
            DivTests<N8>.DivTest();
            DivTests<N9>.DivTest();
            DivTests<N10>.DivTest();
            DivTests<N11>.DivTest();
            DivTests<N12>.DivTest();
            DivTests<N13>.DivTest();
            DivTests<N14>.DivTest();
            DivTests<N15>.DivTest();
            DivTests<N16>.DivTest();
            DivTests<N17>.DivTest();
            DivTests<N23>.DivTest();
            DivTests<N24>.DivTest();
            DivTests<N25>.DivTest();
            DivTests<N31>.DivTest();
            DivTests<N32>.DivTest();
            DivTests<N33>.DivTest();
            DivTests<N47>.DivTest();
            DivTests<N48>.DivTest();
            DivTests<N50>.DivTest();
            DivTests<N53>.DivTest();
            DivTests<N56>.DivTest();
            DivTests<N59>.DivTest();
            DivTests<N63>.DivTest();
            DivTests<N64>.DivTest();
            DivTests<N65>.DivTest();
        }

        [TestMethod]
        public void DivFullTest() {
            DivTests<N4>.DivFullTest();
            DivTests<N5>.DivFullTest();
            DivTests<N6>.DivFullTest();
            DivTests<N7>.DivFullTest();
            DivTests<N8>.DivFullTest();
            DivTests<N9>.DivFullTest();
            DivTests<N10>.DivFullTest();
            DivTests<N11>.DivFullTest();
            DivTests<N12>.DivFullTest();
            DivTests<N13>.DivFullTest();
            DivTests<N14>.DivFullTest();
            DivTests<N15>.DivFullTest();
            DivTests<N16>.DivFullTest();
            DivTests<N17>.DivFullTest();
            DivTests<N23>.DivFullTest();
            DivTests<N24>.DivFullTest();
            DivTests<N25>.DivFullTest();
            DivTests<N31>.DivFullTest();
            DivTests<N32>.DivFullTest();
            DivTests<N33>.DivFullTest();
            DivTests<N47>.DivFullTest();
            DivTests<N48>.DivFullTest();
            DivTests<N50>.DivFullTest();
            DivTests<N53>.DivFullTest();
            DivTests<N56>.DivFullTest();
            DivTests<N59>.DivFullTest();
            DivTests<N63>.DivFullTest();
            DivTests<N64>.DivFullTest();
            DivTests<N65>.DivFullTest();
        }
    }
}
