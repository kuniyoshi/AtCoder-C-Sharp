#nullable enable
using System;
using System.Linq;

namespace AtCoder.c255
{
    internal static class C
    {
        internal static void Run()
        {
            var arguments = Console.ReadLine()!.Split().Select(long.Parse).ToArray();
            var (x, a, d, n) = (arguments[0], arguments[1], arguments[2], arguments[3]);
            var first = a;
            var last = a + d * (n - 1);
            var max = Math.Max(first, last);
            var min = Math.Min(first, last);

            if (x <= min)
            {
                Console.WriteLine(min - x);
                return;
            }

            if (x >= max)
            {
                Console.WriteLine(x - max);
                return;
            }

            if (d == 0)
            {
                throw new Exception("d == 0 should be determined above");
            }

            var newX = Math.Abs(x - a);
            var newD = Math.Abs(d);

            var mod = newX % newD;
            Console.WriteLine(Math.Min(mod, newD - mod));
        }
    }
}
