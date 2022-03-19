#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c243
{
    internal static class E
    {
        internal static void Run()
        {
            var nm = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var (n, m) = (nm[0], nm[1]);
            var costs = Enumerable.Range(0, m)
                .Select(_ => Console.ReadLine()!.Split().Select(long.Parse).ToArray())
                .ToArray();

            var dp = new long[n, n];
            const long inf = 500_000_000_000;

            for (var i = 0; i < n; ++i)
            {
                for (var j = 0; j < n; ++j)
                {
                    dp[i, j] = j == i ? 0L : inf;
                }
            }

            foreach (var cost in costs)
            {
                var (from, to, c) = (cost[0], cost[1], cost[2]);
                from--;
                to--;
                dp[from, to] = c;
                dp[to, from] = c;
            }

            for (var k = 0; k < n; ++k)
            {
                for (var i = 0; i < n; ++i)
                {
                    for (var j = 0; j < n; ++j)
                    {
                        dp[i, j] = Math.Min(dp[i, j], dp[i, k] + dp[k, j]);
                    }
                }
            }

            var count = 0;

            foreach (var cost in costs)
            {
                var (from, to, c) = (cost[0], cost[1], cost[2]);
                from--;
                to--;
                var can = false;

                for (var k = 0; k < n; ++k)
                {
                    if (k == from || k == to)
                    {
                        continue;
                    }

                    if (dp[from, k] == inf || dp[k, to] == inf)
                    {
                        continue;
                    }

                    if (c >= (dp[from, k] + dp[k, to]))
                    {
                        can = true;
                        continue;
                    }
                }

                if (can)
                {
                    count++;
                }
            }

            Console.WriteLine(count);
        }
    }
}
