#nullable enable
using System;

namespace AtCoder.c242
{
    internal static class C
    {
        const int Mod = 998244353;

        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine()!);

            var dp = new long[n, 9];

            for (var i = 0; i < 9; ++i)
            {
                dp[0, i] = 1;
            }

            for (var i = 1; i < n; ++i)
            {
                for (var j = 0; j < 9; ++j)
                {
                    dp[i, j] = j switch
                    {
                        0 => (dp[i - 1, 0] + dp[i - 1, 1]) % Mod,
                        8 => (dp[i - 1, 7] + dp[i - 1, 8]) % Mod,
                        _ => (dp[i - 1, j - 1] + dp[i - 1, j] + dp[i - 1, j + 1]) % Mod
                    };
                }
            }

            var answer = 0L;

            for (var i = 0; i < 9; ++i)
            {
                answer = (answer + dp[n - 1, i]) % Mod;
            }

            Console.WriteLine(answer);
        }
    }
}
