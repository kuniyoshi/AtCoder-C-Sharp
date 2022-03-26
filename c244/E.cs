#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c244
{
    internal static class E
    {
        internal static void Run()
        {
            var line = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var (n, m, k, s, t, x) = (line[0], line[1], line[2], line[3], line[4], line[5]);

            var links = Enumerable.Range(0, n)
                .Select(_ => new List<int>())
                .ToArray();

            for (var i = 0; i < m; ++i)
            {
                var pair = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
                var (left, right) = (pair[0], pair[1]);
                left--;
                right--;
                links[left].Add(right);
                links[right].Add(left);
            }

            s--;
            t--;
            x--;

            var dp = Enumerable.Range(0, k + 1)
                .Select(_ => new EvenOdd[n])
                .ToArray();

            dp[0][s].Even = 1;

            for (var ki = 1; ki < k + 1; ++ki)
            {
                for (var v = 0; v < n; ++v)
                {
                    if (!links[v].Any()) continue;

                    for (var i = 0; i < 2; ++i)
                    {
                        foreach (var neighbor in links[v])
                        {
                            var evenOrOdd = neighbor == x
                                ? i ^ 1
                                : i;

                            dp[ki][v].Add(i, dp[ki - 1][neighbor].Get(evenOrOdd));
                        }
                    }
                }
            }

            Console.WriteLine(dp[k][t].Even);
        }

        struct EvenOdd
        {
            const int Mod = 998244353;
            public long Even;
            public long Odd;

            public long Get(int evenOrOdd)
            {
                return evenOrOdd == 0 ? Even : Odd;
            }

            public void Add(int evenOrOdd, long amount)
            {
                if (evenOrOdd == 0)
                {
                    Even += amount;
                    Even %= Mod;
                }
                else
                {
                    Odd += amount;
                    Odd %= Mod;
                }
            }
        }
    }
}
