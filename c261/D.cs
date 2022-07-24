#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c261
{
    internal static class D
    {
        internal static void Run()
        {
            var nm = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var (n, m) = (nm[0], nm[1]);
            var x = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var bonus = ReadBonus(m);

            var dp = new long[n+1, n+1, 2];

            for (var i = 0; i < n; ++i)
            {
                for (var j = 0; j < i + 1; ++j)
                {
                    for (var k = 0; k < 2; ++k)
                    {
                        if (k == 1)
                        {
                            var current = dp[i, j, 1];
                            var addition = x[i] + (bonus.ContainsKey(j + 1) ? bonus[j + 1] : 0);
                            var total = current + addition;
                            dp[i + 1, j + 1, 0] = Math.Max(dp[i + 1, j + 1, 0], total);
                            dp[i + 1, j + 1, 1] = Math.Max(dp[i + 1, j + 1, 1], total);
                        }
                        else
                        {
                            var current = dp[i, j, 0];
                            dp[i + 1, 0, 0] = Math.Max(dp[i + 1, 0, 0], current);
                            dp[i + 1, 0, 1] = Math.Max(dp[i + 1, 0, 1], current);
                        }
                    }
                }
            }

            var max = 0L;

            for (var i = 0; i < n + 1; ++i)
            {
                for (var j = 0; j < n + 1; ++j)
                {
                    for (var k = 0; k < 2; ++k)
                    {
                        max = Math.Max(max, dp[i, j, k]);
                    }
                }
            }
            
            Console.WriteLine(max);
        }

        static Dictionary<int, int> ReadBonus(int m)
        {
            var items = Enumerable.Range(0, m)
                .Select(_ => Console.ReadLine()!.Split().Select(int.Parse).ToArray())
                .Select(pair => new Bonus(pair[0], pair[1]));
            var bonus = new Dictionary<int, int>();

            foreach (var item in items)
            {
                bonus[item.Count] = item.Value;
            }

            return bonus;
        }

        struct Bonus
        {
            internal Bonus(int count, int value)
            {
                Count = count;
                Value = value;
            }

            internal int Count { get; }
            internal int Value { get; }
        }
    }
}
