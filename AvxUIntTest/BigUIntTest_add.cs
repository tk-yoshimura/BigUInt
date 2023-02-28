using AvxUInt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace AvxUIntTest {
    public static class AddTests<N> where N : struct, IConstant {
        public static void AddTest() {
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

            BigInteger maxn = BigUInt<N>.Full;

            Console.WriteLine($"length={BigUInt<N>.Length}");

            int normal_passes = 0, overflow_passes = 0;

            foreach ((BigUInt<N> v1, BigInteger n1) in vs) {
                foreach ((BigUInt<N> v2, BigInteger n2) in vs) {
                    BigInteger n = n1 + n2;

                    if (n <= maxn) {
                        Assert.AreEqual(n, (BigInteger)(v1 + v2));

                        if (v1.Digits <= 2) { 
                            Assert.AreEqual(n, (BigInteger)(UIntUtil.Pack(v1.Value[1], v1.Value[0]) + v2));
                        }

                        if (v1.Digits <= 1) { 
                            Assert.AreEqual(n, (BigInteger)(v1.Value[0] + v2));
                        }

                        if (v2.Digits <= 2) { 
                            Assert.AreEqual(n, (BigInteger)(v1 + UIntUtil.Pack(v2.Value[1], v2.Value[0])));
                        }

                        if (v2.Digits <= 1) { 
                            Assert.AreEqual(n, (BigInteger)(v1 + v2.Value[0]));
                        }

                        normal_passes++;
                    }
                    else {
                        Assert.ThrowsException<OverflowException>(() => {
                            _ = v1 + v2;
                        });

                        overflow_passes++;
                    }
                }
            }

            Console.WriteLine($"{nameof(normal_passes)}: {normal_passes}");
            Console.WriteLine($"{nameof(overflow_passes)}: {overflow_passes}");
        }

        public static void AddFullTest() {
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
                BigInteger n = n1 + n2;

                if (n <= maxn) {
                    Assert.AreEqual(n, (BigInteger)(v1 + v2));

                    if (v1.Digits <= 2) { 
                        Assert.AreEqual(n, (BigInteger)(UIntUtil.Pack(v1.Value[1], v1.Value[0]) + v2));
                    }

                    if (v1.Digits <= 1) { 
                        Assert.AreEqual(n, (BigInteger)(v1.Value[0] + v2));
                    }

                    if (v2.Digits <= 2) { 
                        Assert.AreEqual(n, (BigInteger)(v1 + UIntUtil.Pack(v2.Value[1], v2.Value[0])));
                    }

                    if (v2.Digits <= 1) { 
                        Assert.AreEqual(n, (BigInteger)(v1 + v2.Value[0]));
                    }

                    normal_passes++;
                }
                else {
                    Assert.ThrowsException<OverflowException>(() => {
                        _ = v1 + v2;
                    });

                    overflow_passes++;
                }
            }

            Console.WriteLine($"{nameof(normal_passes)}: {normal_passes}");
            Console.WriteLine($"{nameof(overflow_passes)}: {overflow_passes}");
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

        [TestMethod]
        public void AddFullTest() {
            AddTests<N4>.AddFullTest();
            AddTests<N5>.AddFullTest();
            AddTests<N6>.AddFullTest();
            AddTests<N7>.AddFullTest();
            AddTests<N8>.AddFullTest();
            AddTests<N9>.AddFullTest();
            AddTests<N10>.AddFullTest();
            AddTests<N11>.AddFullTest();
            AddTests<N12>.AddFullTest();
            AddTests<N13>.AddFullTest();
            AddTests<N14>.AddFullTest();
            AddTests<N15>.AddFullTest();
            AddTests<N16>.AddFullTest();
            AddTests<N17>.AddFullTest();
            AddTests<N23>.AddFullTest();
            AddTests<N24>.AddFullTest();
            AddTests<N25>.AddFullTest();
            AddTests<N31>.AddFullTest();
            AddTests<N32>.AddFullTest();
            AddTests<N33>.AddFullTest();
            AddTests<N47>.AddFullTest();
            AddTests<N48>.AddFullTest();
            AddTests<N50>.AddFullTest();
            AddTests<N53>.AddFullTest();
            AddTests<N56>.AddFullTest();
            AddTests<N59>.AddFullTest();
            AddTests<N63>.AddFullTest();
            AddTests<N64>.AddFullTest();
            AddTests<N65>.AddFullTest();
        }
    }
}
