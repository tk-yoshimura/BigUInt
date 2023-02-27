using System;
using System.Runtime.Intrinsics;
using static System.Runtime.Intrinsics.X86.Avx;
using static System.Runtime.Intrinsics.X86.Avx2;

Vector256<UInt32> zero = Vector256<UInt32>.Zero;
Vector256<UInt32> x0 = Vector256.Create(1u, 2u, 3u, 4u, 5u, 6u, 7u, 8u);
Vector256<UInt32> x1 = Vector256.Create(9u, 10u, 11u, 12u, 13u, 14u, 15u, 16u);

Vector256<UInt32> l = Permute4x64(Shuffle(x0.AsSingle(), x1.AsSingle(), 0b10001000).AsDouble(), 0b11011000).AsUInt32();
Vector256<UInt32> h = Permute4x64(Shuffle(x0.AsSingle(), x1.AsSingle(), 0b11011101).AsDouble(), 0b11011000).AsUInt32();

Console.WriteLine("END");
