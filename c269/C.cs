#nullable enable
using System;
using System.Collections.Generic;

namespace AtCoder.c269
{
    internal static class C
    {
        internal static void Run()
        {
            var n = long.Parse(Console.ReadLine()!);
            var digits = GetRanks(n);

            var count = Pow2(digits.Count);

            for (var i = 0; i < count; ++i)
            {
                var ranks = GetRanks(i);
                var value = 0L;

                foreach (var rank in ranks)
                {
                    value += Pow2(digits[rank]);
                }

                Console.WriteLine(value);
            }
        }

        static List<int> GetRanks(long value)
        {
            var ranks = new List<int>();

            for (var i = 0; i < 64; ++i)
            {
                var x = Pow2(i);

                if ((x & value) == x)
                {
                    ranks.Add(i);
                }
            }

            return ranks;
        }

        static long Pow2(int n)
        {
            var value = 1L;

            for (var i = 0; i < n; ++i)
            {
                value *= 2;
            }

            return value;
        }
    }
}
