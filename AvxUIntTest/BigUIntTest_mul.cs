using AvxUInt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace AvxUIntTest {
    public static class MulTests<N> where N : struct, IConstant {
        public static void MulTest() {
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
                    BigInteger n = n1 * n2;

                    if (n <= maxn) {
                        Assert.AreEqual(n, (BigInteger)(v1 * v2), $"{n1}*{n2}");

                        if (v1.Digits <= 2) { 
                            Assert.AreEqual(n, (BigInteger)(UIntUtil.Pack(v1.Value[1], v1.Value[0]) * v2), $"{n1}*{n2}");
                        }

                        if (v1.Digits <= 1) { 
                            Assert.AreEqual(n, (BigInteger)(v1.Value[0] * v2), $"{n1}*{n2}");
                        }

                        if (v2.Digits <= 2) { 
                            Assert.AreEqual(n, (BigInteger)(v1 * UIntUtil.Pack(v2.Value[1], v2.Value[0])), $"{n1}*{n2}");
                        }

                        if (v2.Digits <= 1) { 
                            Assert.AreEqual(n, (BigInteger)(v1 * v2.Value[0]), $"{n1}*{n2}");
                        }

                        normal_passes++;
                    }
                    else {
                        Assert.ThrowsException<OverflowException>(() => {
                            _ = v1 * v2;
                        });

                        overflow_passes++;
                    }
                }
            }

            Console.WriteLine($"{nameof(normal_passes)}: {normal_passes}");
            Console.WriteLine($"{nameof(overflow_passes)}: {overflow_passes}");
        }

        public static void MulFullTest() {
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

            int normal_passes = 0, overflow_passes = 0;

            foreach ((BigUInt<N> v1, BigUInt<N> v2, BigInteger n1, BigInteger n2) in vs) {
                BigInteger n = n1 * n2;

                if (n <= maxn) {
                    Assert.AreEqual(n, (BigInteger)(v1 * v2), $"{n1}*{n2}");

                    if (v1.Digits <= 2) { 
                        Assert.AreEqual(n, (BigInteger)(UIntUtil.Pack(v1.Value[1], v1.Value[0]) * v2), $"{n1}*{n2}");
                    }

                    if (v1.Digits <= 1) { 
                        Assert.AreEqual(n, (BigInteger)(v1.Value[0] * v2), $"{n1}*{n2}");
                    }

                    if (v2.Digits <= 2) { 
                        Assert.AreEqual(n, (BigInteger)(v1 * UIntUtil.Pack(v2.Value[1], v2.Value[0])), $"{n1}*{n2}");
                    }

                    if (v2.Digits <= 1) { 
                        Assert.AreEqual(n, (BigInteger)(v1 * v2.Value[0]), $"{n1}*{n2}");
                    }

                    normal_passes++;
                }
                else {
                    Assert.ThrowsException<OverflowException>(() => {
                        _ = v1 * v2;
                    });

                    overflow_passes++;
                }
            }

            Console.WriteLine($"{nameof(normal_passes)}: {normal_passes}");
            Console.WriteLine($"{nameof(overflow_passes)}: {overflow_passes}");
        }
    }

    [TestClass]
    public class MulTests {
        [TestMethod]
        public void MulTest() {
            MulTests<N4>.MulTest();
            MulTests<N5>.MulTest();
            MulTests<N6>.MulTest();
            MulTests<N7>.MulTest();
            MulTests<N8>.MulTest();
            MulTests<N9>.MulTest();
            MulTests<N10>.MulTest();
            MulTests<N11>.MulTest();
            MulTests<N12>.MulTest();
            MulTests<N13>.MulTest();
            MulTests<N14>.MulTest();
            MulTests<N15>.MulTest();
            MulTests<N16>.MulTest();
            MulTests<N17>.MulTest();
            MulTests<N23>.MulTest();
            MulTests<N24>.MulTest();
            MulTests<N25>.MulTest();
            MulTests<N31>.MulTest();
            MulTests<N32>.MulTest();
            MulTests<N33>.MulTest();
            MulTests<N47>.MulTest();
            MulTests<N48>.MulTest();
            MulTests<N50>.MulTest();
            MulTests<N53>.MulTest();
            MulTests<N56>.MulTest();
            MulTests<N59>.MulTest();
            MulTests<N63>.MulTest();
            MulTests<N64>.MulTest();
            MulTests<N65>.MulTest();
        }

        [TestMethod]
        public void MulFullTest() {
            MulTests<N4>.MulFullTest();
            MulTests<N5>.MulFullTest();
            MulTests<N6>.MulFullTest();
            MulTests<N7>.MulFullTest();
            MulTests<N8>.MulFullTest();
            MulTests<N9>.MulFullTest();
            MulTests<N10>.MulFullTest();
            MulTests<N11>.MulFullTest();
            MulTests<N12>.MulFullTest();
            MulTests<N13>.MulFullTest();
            MulTests<N14>.MulFullTest();
            MulTests<N15>.MulFullTest();
            MulTests<N16>.MulFullTest();
            MulTests<N17>.MulFullTest();
            MulTests<N23>.MulFullTest();
            MulTests<N24>.MulFullTest();
            MulTests<N25>.MulFullTest();
            MulTests<N31>.MulFullTest();
            MulTests<N32>.MulFullTest();
            MulTests<N33>.MulFullTest();
            MulTests<N47>.MulFullTest();
            MulTests<N48>.MulFullTest();
            MulTests<N50>.MulFullTest();
            MulTests<N53>.MulFullTest();
            MulTests<N56>.MulFullTest();
            MulTests<N59>.MulFullTest();
            MulTests<N63>.MulFullTest();
            MulTests<N64>.MulFullTest();
            MulTests<N65>.MulFullTest();
        }
    }
}
