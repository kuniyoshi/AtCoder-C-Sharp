#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c222
{
    internal static class D
    {
        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine()!);
            var a = new List<int>(new[] { 0 }.Concat(Console.ReadLine()!.Split().Select(int.Parse)));
            var b = new List<int>(new[] { 0 }.Concat(Console.ReadLine()!.Split().Select(int.Parse)));

            var m = b.Max();

            var dp = new long[n + 1, m + 1];

            for (var i = 0; i <= m; ++i)
            {
                dp[0, i] = 1;
            }

            for (var i = 1; i <= n; ++i)
            {
                if (IsIn(0, a[i], b[i]))
                {
                    dp[i, 0] = dp[i - 1, 0];
                }
                else
                {
                    dp[i, 0] = 0;
                }

                for (var j = 1; j <= m; ++j)
                {
                    if (IsIn(j, a[i], b[i]))
                    {
                        dp[i, j] = (dp[i - 1, j] + dp[i, j - 1]) % 998244353;
                    }
                    else
                    {
                        dp[i, j] = dp[i, j - 1];
                    }
                }
            }

            Console.WriteLine(dp[n, m] % 998244353);
        }

        static bool IsIn(int target, int min, int max)
        {
            return target >= min && target <= max;
        }
    }
}
