#nullable enable
using System;
using System.Diagnostics;
using System.Linq;
using static System.Console;

namespace AtCoder.c219
{
    internal static class D
    {
        internal static void Run()
        {
            var n = int.Parse(ReadLine()!);
            var xy = ReadLine()!.Split();
            var x = int.Parse(xy[0]);
            var y = int.Parse(xy[1]);
            var bentos = Enumerable.Range(0, n)
                .Select(_ => ReadLine()!.Split().Select(int.Parse).ToArray())
                .ToArray();

            var dp = new int[n + 1, x + 1, y + 1];

            const int maxCost = 500;

            for (var i = 0; i <= n; ++i)
            {
                for (var j = 0; j <= x; ++j)
                {
                    for (var k = 0; k <= y; ++k)
                    {
                        dp[i, j, k] = maxCost;
                    }
                }
            }

            dp[0, 0, 0] = 0;

            for (var i = 1; i <= n; ++i)
            {
                var bento = bentos[i - 1];

                for (var j = 0; j <= x; ++j)
                {
                    for (var k = 0; k <= y; ++k)
                    {
                        dp[i, j, k] = Math.Min(dp[i, j, k], dp[i - 1, j, k]);
                        var xIndex = Math.Min(j + bento[0], x);
                        var yIndex = Math.Min(k + bento[1], y);
                        // Debug.Assert(xIndex <= x, $"{xIndex} <= {x}");
                        // Debug.Assert(yIndex <= y, $"{yIndex} <= {y}");
                        dp[i, xIndex, yIndex] = Math.Min(
                            dp[i, xIndex, yIndex],
                            dp[i - 1, j, k] + 1
                        );
                    }
                }
            }

            WriteLine(dp[n, x, y] < maxCost ? dp[n, x, y] : -1);
        }
    }
}
