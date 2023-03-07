#nullable enable
using System;
using System.Collections.Generic;

namespace AtCoder.c292
{
    internal static class C
    {
        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine());

            var divisors = new Dictionary<int, int>();

            for (var i = 1; i <= n; ++i)
            {
                var sqrtI = Math.Floor(Math.Sqrt(i));
                var count = 0;

                for (var j = 1; j <= sqrtI; ++j)
                {
                    if (i % j == 0)
                    {
                        count++;

                        if (i / j != j)
                        {
                            count++;
                        }
                    }
                }

                divisors[i] = count;
            }

            var total = 0;

            for (var i = 1; i < n; ++i)
            {
                var ab = i;
                var cd = n - i;
                total += divisors[ab] * divisors[cd];
            }

            Console.WriteLine(total);
        }
    }
}
