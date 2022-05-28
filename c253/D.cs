#nullable enable
using System;
using System.ComponentModel;
using System.Linq;

namespace AtCoder.c253
{
    internal static class D
    {
        internal static void Run()
        {
            var nab = Console.ReadLine()!.Split().Select(long.Parse).ToArray();
            var (n, a, b) = (nab[0], nab[1], nab[2]);

            var whole = Divide2(n, (1L + n));
            var sumA = Calc(n, a);
            var sumB = Calc(n, b);
            var sumAb = Calc(n, a * b);

            var answer = whole - sumA - sumB + sumAb;

            Console.WriteLine(answer);
        }

        static long Divide2(long a, long b)
        {
            if (b % 2L == 0)
            {
                var b2 = b / 2;
                return b2 * a;
            }

            if (a % 2L == 0)
            {
                var a2 = a / 2;
                return a2 * b;
            }

            throw new InvalidEnumArgumentException();
        }

        static long Calc(long n, long a)
        {
            var count = n / a;
            var sum = Divide22(a, count);
            return sum;
        }

        static long Divide22(long a, long x)
        {
            if (x % 2 == 0)
            {
                return x / 2 * a + x / 2 * x * a;
            }
            
            if (a % 2 == 0)
            {
                return (a / 2 + (a / 2) * x) * x;
            }

            throw new ArgumentException();
        }
    }
}
