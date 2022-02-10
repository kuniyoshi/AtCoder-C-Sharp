#nullable enable
using System;

namespace AtCoder.c238
{
    internal static class C
    {
        internal static void Run()
        {
            var n = long.Parse(Console.ReadLine()!);
            Console.WriteLine(Recursive(n));
        }

        static long Recursive(long x)
        {
            if (x.ToString().Length == 1)
            {
                return x * (1 + x) / 2;
            }

            var unit = Power(10, x.ToString().Length - 1);
            var a = 1;
            var l = x - unit + 1;
            var n = l - a + 1;

            var y = (n * (a + l) / 2) % 998244353;

            var z = unit - 1;

            return ((y + (Recursive(z) % 998244353)) % 998244353);
        }

        static long Power(int value, int count)
        {
            var acc = value;

            while (true)
            {
                if (count == 1)
                {
                    return acc;
                }

                acc = acc * acc;
                count = count - 1;
            }
        }
    }
}
