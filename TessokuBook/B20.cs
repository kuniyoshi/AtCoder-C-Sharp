#nullable enable
using System;
using System.Linq;

namespace AtCoder.TessokuBook
{
    internal static class B20
    {
        static int Min(int a, int b, int c)
        {
            return Math.Min(Math.Min(a, b), c);
        }

        internal static void Run()
        {
            var s = Console.ReadLine()!;
            var t = Console.ReadLine()!;

            var dp = Enumerable.Range(0, s.Length + 1).Select(_ => new int[t.Length + 1]).ToArray();

            for (var i = 0; i < s.Length + 1; ++i)
            {
                for (var j = 0; j < t.Length + 1; ++j)
                {
                    dp[i][j] = s.Length + t.Length;
                }
            }

            for (var i = 0; i < s.Length + 1; ++i)
            {
                for (var j = 0; j < t.Length + 1; ++j)
                {
                    if (i == 0 && j == 0)
                    {
                        dp[i][j] = 0;
                        continue;
                    }

                    if (j == 0)
                    {
                        dp[i][j] = dp[i - 1][j] + 1;
                        continue;
                    }

                    if (i == 0)
                    {
                        dp[i][j] = dp[i][j - 1] + 1;
                        continue;
                    }

                    dp[i][j] = Min(
                        dp[i - 1][j] + 1,
                        dp[i][j - 1] + 1,
                        t[j - 1] == s[i - 1] ? dp[i - 1][j - 1] : dp[i - 1][j - 1] + 1
                    );
                }
            }

            Console.WriteLine(dp[s.Length][t.Length]);
        }
    }
}
