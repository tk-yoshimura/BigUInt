using AvxUInt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using System.Numerics;

namespace AvxUIntTest {
    public static class CmpTests<N> where N: struct, IConstant {
        private static readonly int length = default(N).Value;

        private static readonly ReadOnlyCollection<(BigUInt<N> bits, BigInteger bint)> tests;
        static CmpTests() {
            List<(BigUInt<N> bits, BigInteger bint)> vs = new();
            for (uint v = ~0u; v > 0; v /= 3) {
                for (int s = 1; s <= length / 2; s++) {
                    UInt32[] bits = new UInt32[length];

                    for (int k = 0; k < length; k += s) {
                        bits[k] = v;
                    }

                    BigUInt<N> b = new(bits);

                    vs.Add((b, (BigInteger)b));
                }
            }

            Random random = new(1234);
            for (int i = 0; i < 10; i++) {
                UInt32[] bits = UIntUtil.Random(random, length, random.Next(length * UIntUtil.UInt32Bits + 1));

                BigUInt<N> b = new(bits);
                vs.Add((b, (BigInteger)b));
            }

            Console.WriteLine($"{nameof(length)}={length}: {vs.Count} testcases");

            tests = Array.AsReadOnly(vs.ToArray());
        }

        public static void EqualTest() { 
            foreach ((BigUInt<N> n1, BigInteger b1) in tests) { 
                foreach ((BigUInt<N> n2, BigInteger b2) in tests) {
                    Assert.AreEqual(b1 == b2, n1 == n2, $"\n{length}\n{n1.ToHexcode()}\n{n2.ToHexcode()}");
                }
            }
        }

        public static void LessThanOrEqualTest() { 
            foreach ((BigUInt<N> n1, BigInteger b1) in tests) { 
                foreach ((BigUInt<N> n2, BigInteger b2) in tests) {
                    Assert.AreEqual(b1 <= b2, n1 <= n2, $"\n{length}\n{n1.ToHexcode()}\n{n2.ToHexcode()}");
                }
            }
        }

        public static void GreaterThanOrEqualTest() { 
            foreach ((BigUInt<N> n1, BigInteger b1) in tests) { 
                foreach ((BigUInt<N> n2, BigInteger b2) in tests) {
                    Assert.AreEqual(b1 >= b2, n1 >= n2, $"\n{length}\n{n1.ToHexcode()}\n{n2.ToHexcode()}");
                }
            }
        }
    }

    [TestClass]
    public class CmpTests {
        [TestMethod]
        public void EqualN8Test() {
            CmpTests<N4>.EqualTest();
            CmpTests<N5>.EqualTest();
            CmpTests<N6>.EqualTest();
            CmpTests<N7>.EqualTest();
            CmpTests<N8>.EqualTest();
        }
        
        [TestMethod]
        public void EqualN16Test() {
            CmpTests<N9>.EqualTest();
            CmpTests<N10>.EqualTest();
            CmpTests<N11>.EqualTest();
            CmpTests<N12>.EqualTest();
            CmpTests<N13>.EqualTest();
            CmpTests<N14>.EqualTest();
            CmpTests<N15>.EqualTest();
            CmpTests<N16>.EqualTest();
        }

        [TestMethod]
        public void EqualN32Test() {
            CmpTests<N17>.EqualTest();
            CmpTests<N23>.EqualTest();
            CmpTests<N24>.EqualTest();
            CmpTests<N25>.EqualTest();
            CmpTests<N31>.EqualTest();
            CmpTests<N32>.EqualTest();
        }

        [TestMethod]
        public void EqualN64Test() {
            CmpTests<N33>.EqualTest();
            CmpTests<N47>.EqualTest();
            CmpTests<N48>.EqualTest();
            CmpTests<N50>.EqualTest();
            CmpTests<N53>.EqualTest();
            CmpTests<N56>.EqualTest();
            CmpTests<N59>.EqualTest();
            CmpTests<N63>.EqualTest();
            CmpTests<N64>.EqualTest();
            CmpTests<N65>.EqualTest();
        }

        [TestMethod]
        public void LessThanOrEqualN8Test() {
            CmpTests<N4>.LessThanOrEqualTest();
            CmpTests<N5>.LessThanOrEqualTest();
            CmpTests<N6>.LessThanOrEqualTest();
            CmpTests<N7>.LessThanOrEqualTest();
            CmpTests<N8>.LessThanOrEqualTest();
        }
        
        [TestMethod]
        public void LessThanOrEqualN16Test() {
            CmpTests<N9>.LessThanOrEqualTest();
            CmpTests<N10>.LessThanOrEqualTest();
            CmpTests<N11>.LessThanOrEqualTest();
            CmpTests<N12>.LessThanOrEqualTest();
            CmpTests<N13>.LessThanOrEqualTest();
            CmpTests<N14>.LessThanOrEqualTest();
            CmpTests<N15>.LessThanOrEqualTest();
            CmpTests<N16>.LessThanOrEqualTest();
        }

        [TestMethod]
        public void LessThanOrEqualN32Test() {
            CmpTests<N17>.LessThanOrEqualTest();
            CmpTests<N23>.LessThanOrEqualTest();
            CmpTests<N24>.LessThanOrEqualTest();
            CmpTests<N25>.LessThanOrEqualTest();
            CmpTests<N31>.LessThanOrEqualTest();
            CmpTests<N32>.LessThanOrEqualTest();
        }

        [TestMethod]
        public void LessThanOrEqualN64Test() {
            CmpTests<N33>.LessThanOrEqualTest();
            CmpTests<N47>.LessThanOrEqualTest();
            CmpTests<N48>.LessThanOrEqualTest();
            CmpTests<N50>.LessThanOrEqualTest();
            CmpTests<N53>.LessThanOrEqualTest();
            CmpTests<N56>.LessThanOrEqualTest();
            CmpTests<N59>.LessThanOrEqualTest();
            CmpTests<N63>.LessThanOrEqualTest();
            CmpTests<N64>.LessThanOrEqualTest();
            CmpTests<N65>.LessThanOrEqualTest();
        }

        
        [TestMethod]
        public void GreaterThanOrEqualN8Test() {
            CmpTests<N4>.GreaterThanOrEqualTest();
            CmpTests<N5>.GreaterThanOrEqualTest();
            CmpTests<N6>.GreaterThanOrEqualTest();
            CmpTests<N7>.GreaterThanOrEqualTest();
            CmpTests<N8>.GreaterThanOrEqualTest();
        }
        
        [TestMethod]
        public void GreaterThanOrEqualN16Test() {
            CmpTests<N9>.GreaterThanOrEqualTest();
            CmpTests<N10>.GreaterThanOrEqualTest();
            CmpTests<N11>.GreaterThanOrEqualTest();
            CmpTests<N12>.GreaterThanOrEqualTest();
            CmpTests<N13>.GreaterThanOrEqualTest();
            CmpTests<N14>.GreaterThanOrEqualTest();
            CmpTests<N15>.GreaterThanOrEqualTest();
            CmpTests<N16>.GreaterThanOrEqualTest();
        }

        [TestMethod]
        public void GreaterThanOrEqualN32Test() {
            CmpTests<N17>.GreaterThanOrEqualTest();
            CmpTests<N23>.GreaterThanOrEqualTest();
            CmpTests<N24>.GreaterThanOrEqualTest();
            CmpTests<N25>.GreaterThanOrEqualTest();
            CmpTests<N31>.GreaterThanOrEqualTest();
            CmpTests<N32>.GreaterThanOrEqualTest();
        }

        [TestMethod]
        public void GreaterThanOrEqualN64Test() {
            CmpTests<N33>.GreaterThanOrEqualTest();
            CmpTests<N47>.GreaterThanOrEqualTest();
            CmpTests<N48>.GreaterThanOrEqualTest();
            CmpTests<N50>.GreaterThanOrEqualTest();
            CmpTests<N53>.GreaterThanOrEqualTest();
            CmpTests<N56>.GreaterThanOrEqualTest();
            CmpTests<N59>.GreaterThanOrEqualTest();
            CmpTests<N63>.GreaterThanOrEqualTest();
            CmpTests<N64>.GreaterThanOrEqualTest();
            CmpTests<N65>.GreaterThanOrEqualTest();
        }
    }
}
