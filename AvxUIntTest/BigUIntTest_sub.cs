using AvxUInt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace AvxUIntTest {
    public static class SubTests<N> where N : struct, IConstant {
        public static void SubTest() {
            Random random = new(1234);

            List<(BigUInt<N> b, BigInteger n)> vs = new();

            int length = default(N).Value;
            for (int i = 1; i <= BigUInt<N>.Bits; i += 15) {
                UInt32[] bits = UIntUtil.Random(random, BigUInt<N>.Length, i);
                UInt32[] bits_swapbit = (UInt32[])bits.Clone();
                bits_swapbit[random.Next(length)] ^= 1u << random.Next(UIntUtil.UInt32Bits);

                BigUInt<N> b = new(bits, enable_clone: false), b_swapbit = new(bits_swapbit, enable_clone: false);
                vs.Add((b, (BigInteger)b));
                vs.Add((b_swapbit, (BigInteger)b_swapbit));
            }
            {
                BigUInt<N> v = BigUInt<N>.Full;
                while (v > 0) {
                    vs.Add((v, v));
                    v >>= 15;
                }
            }

            vs.Add((1, 1));

            Console.WriteLine($"length={BigUInt<N>.Length}");

            int normal_passes = 0, overflow_passes = 0;

            foreach ((BigUInt<N> v1, BigInteger n1) in vs) {
                foreach ((BigUInt<N> v2, BigInteger n2) in vs) {
                    BigInteger n = n1 - n2;

                    if (n >= 0) {
                        Assert.AreEqual(n, (BigInteger)(v1 - v2));

                        if (v1.Digits <= 2) { 
                            Assert.AreEqual(n, (BigInteger)(UIntUtil.Pack(v1.Value[1], v1.Value[0]) - v2));
                        }

                        if (v1.Digits <= 1) { 
                            Assert.AreEqual(n, (BigInteger)(v1.Value[0] - v2));
                        }

                        if (v2.Digits <= 2) { 
                            Assert.AreEqual(n, (BigInteger)(v1 - UIntUtil.Pack(v2.Value[1], v2.Value[0])));
                        }

                        if (v2.Digits <= 1) { 
                            Assert.AreEqual(n, (BigInteger)(v1 - v2.Value[0]));
                        }

                        normal_passes++;
                    }
                    else {
                        Assert.ThrowsException<OverflowException>(() => {
                            _ = v1 - v2;
                        });

                        overflow_passes++;
                    }
                }
            }

            Console.WriteLine($"{nameof(normal_passes)}: {normal_passes}");
            Console.WriteLine($"{nameof(overflow_passes)}: {overflow_passes}");
        }

        public static void SubFullTest() {
            List<(BigUInt<N> v1, BigUInt<N> v2, BigInteger n1, BigInteger n2)> vs = new();
            BigInteger maxn = BigUInt<N>.Full, v = maxn / 2;

            while (v > 0) {
                BigInteger u = maxn - v;

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

            int normal_passes = 0, overflow_passes = 0;

            foreach ((BigUInt<N> v1, BigUInt<N> v2, BigInteger n1, BigInteger n2) in vs) {
                BigInteger n = n1 - n2;

                if (n >= 0) {
                    Assert.AreEqual(n, (BigInteger)(v1 - v2));

                    if (v1.Digits <= 2) { 
                        Assert.AreEqual(n, (BigInteger)(UIntUtil.Pack(v1.Value[1], v1.Value[0]) - v2));
                    }

                    if (v1.Digits <= 1) { 
                        Assert.AreEqual(n, (BigInteger)(v1.Value[0] - v2));
                    }

                    if (v2.Digits <= 2) { 
                        Assert.AreEqual(n, (BigInteger)(v1 - UIntUtil.Pack(v2.Value[1], v2.Value[0])));
                    }

                    if (v2.Digits <= 1) { 
                        Assert.AreEqual(n, (BigInteger)(v1 - v2.Value[0]));
                    }

                    normal_passes++;
                }
                else {
                    Assert.ThrowsException<OverflowException>(() => {
                        _ = v1 - v2;
                    });

                    overflow_passes++;
                }
            }

            Console.WriteLine($"{nameof(normal_passes)}: {normal_passes}");
            Console.WriteLine($"{nameof(overflow_passes)}: {overflow_passes}");
        }
    }

    [TestClass]
    public class SubTests {
        [TestMethod]
        public void SubTest() {
            SubTests<N4>.SubTest();
            SubTests<N5>.SubTest();
            SubTests<N6>.SubTest();
            SubTests<N7>.SubTest();
            SubTests<N8>.SubTest();
            SubTests<N9>.SubTest();
            SubTests<N10>.SubTest();
            SubTests<N11>.SubTest();
            SubTests<N12>.SubTest();
            SubTests<N13>.SubTest();
            SubTests<N14>.SubTest();
            SubTests<N15>.SubTest();
            SubTests<N16>.SubTest();
            SubTests<N17>.SubTest();
            SubTests<N23>.SubTest();
            SubTests<N24>.SubTest();
            SubTests<N25>.SubTest();
            SubTests<N31>.SubTest();
            SubTests<N32>.SubTest();
            SubTests<N33>.SubTest();
            SubTests<N47>.SubTest();
            SubTests<N48>.SubTest();
            SubTests<N50>.SubTest();
            SubTests<N53>.SubTest();
            SubTests<N56>.SubTest();
            SubTests<N59>.SubTest();
            SubTests<N63>.SubTest();
            SubTests<N64>.SubTest();
            SubTests<N65>.SubTest();
        }

        [TestMethod]
        public void SubFullTest() {
            SubTests<N4>.SubFullTest();
            SubTests<N5>.SubFullTest();
            SubTests<N6>.SubFullTest();
            SubTests<N7>.SubFullTest();
            SubTests<N8>.SubFullTest();
            SubTests<N9>.SubFullTest();
            SubTests<N10>.SubFullTest();
            SubTests<N11>.SubFullTest();
            SubTests<N12>.SubFullTest();
            SubTests<N13>.SubFullTest();
            SubTests<N14>.SubFullTest();
            SubTests<N15>.SubFullTest();
            SubTests<N16>.SubFullTest();
            SubTests<N17>.SubFullTest();
            SubTests<N23>.SubFullTest();
            SubTests<N24>.SubFullTest();
            SubTests<N25>.SubFullTest();
            SubTests<N31>.SubFullTest();
            SubTests<N32>.SubFullTest();
            SubTests<N33>.SubFullTest();
            SubTests<N47>.SubFullTest();
            SubTests<N48>.SubFullTest();
            SubTests<N50>.SubFullTest();
            SubTests<N53>.SubFullTest();
            SubTests<N56>.SubFullTest();
            SubTests<N59>.SubFullTest();
            SubTests<N63>.SubFullTest();
            SubTests<N64>.SubFullTest();
            SubTests<N65>.SubFullTest();
        }
    }
}
