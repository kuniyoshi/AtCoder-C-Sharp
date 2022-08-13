#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c263
{
    internal static class Dold
    {
        internal static void Run()
        {
            var nlr = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var (n, l, r) = (nlr[0], nlr[1], nlr[2]);
            var a = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

            var original = a.Select(value => (long)value).Sum();
            var min = original;

            var lSums = new long[n];

            var acc = original;

            for (var i = 0; i < n; ++i)
            {
                var diff = l - a[i];
                lSums[i] = acc + diff;
                acc = lSums[i];
            }

            min = Math.Min(min, lSums.Min());

            var rSums = new long[n];
            acc = lSums.Last();

            for (var i = n - 1; i >= 0; --i)
            {
                var toOriginal = l - a[i];
                var diff = r - a[i];
                rSums[i] = acc + toOriginal + diff;
                acc = rSums[i];
            }

            min = Math.Min(min, rSums.Min());

            Console.WriteLine(min);
        }
    }
}
