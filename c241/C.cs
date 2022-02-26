#nullable enable
using System;
using System.Linq;

namespace AtCoder.c241
{
    internal static class C
    {
        internal static void Run()
        {
            Console.WriteLine(Can() ? "Yes" : "NO");
        }

        static bool Can()
        {
            const int right = 0;
            const int down = 1;
            const int rd = 2;
            const int ld = 3;

            var n = int.Parse(Console.ReadLine()!);
            var s = Enumerable.Repeat(0, n)
                .Select(_ => Console.ReadLine()!)
                .ToArray();

            var dp = new int[n, n, 4, 3];

            for (var i = 0; i < n; ++i)
            {
                for (var j = 0; j < n; ++j)
                {
                    var isWall = Convert.ToInt32(s[i][j] == '#');

                    for (var k = 0; k < 4; ++k)
                    {
                        for (var l = 0; l < 3; ++l)
                        {
                            dp[i, j, k, l] = Math.Max(isWall, Convert.ToInt32(l > 0));
                        }
                    }
                }
            }

            for (var i = 0; i < n; ++i)
            {
                for (var j = 0; j < n; ++j)
                {
                    var isWall = Convert.ToInt32(s[i][j] == '#');

                    if (j > 0)
                    {
                        dp[i, j, right, 2] = isWall > 0
                            ? Math.Max(
                                dp[i, j - 1, right, 2] + 1,
                                dp[i, j - 1, right, 1] + 1
                            )
                            : dp[i, j - 1, right, 1] + 1;
                        dp[i, j, right, 1] = isWall > 0
                            ? Math.Max(
                                dp[i, j - 1, right, 1] + isWall,
                                dp[i, j - 1, right, 0] + 1
                            )
                            : dp[i, j - 1, right, 0] + 1;
                        dp[i, j, right, 0] = dp[i, j - 1, right, 0] + isWall;

                        if (isWall == 0)
                        {
                            dp[i, j, right, 0] = 0;
                        }
                    }
                    else
                    {
                        dp[i, j, right, 0] = isWall;
                        dp[i, j, right, 1] = 1;
                        dp[i, j, right, 2] = 1;
                    }

                    if (i > 0)
                    {
                        dp[i, j, down, 2] = isWall > 0
                            ? Math.Max(
                                dp[i - 1, j, down, 2] + isWall,
                                dp[i - 1, j, down, 1] + 1
                            )
                            : dp[i - 1, j, down, 1] + 1;
                        dp[i, j, down, 1] = isWall > 0
                            ? Math.Max(
                                dp[i - 1, j, down, 1] + isWall,
                                dp[i - 1, j, down, 0] + 1
                            )
                            : dp[i - 1, j, down, 0] + 1;
                        dp[i, j, down, 0] = dp[i - 1, j, down, 0] + isWall;

                        if (isWall == 0)
                        {
                            dp[i, j, down, 0] = 0;
                        }
                    }
                    else
                    {
                        dp[i, j, down, 0] = isWall;
                        dp[i, j, down, 1] = 1;
                        dp[i, j, down, 2] = 1;
                    }

                    if (i > 0 && j > 0)
                    {
                        dp[i, j, rd, 2] = isWall > 0
                            ? Math.Max(
                                dp[i - 1, j - 1, rd, 2] + isWall,
                                dp[i - 1, j - 1, rd, 1] + 1
                            )
                            : dp[i - 1, j - 1, rd, 1] + 1;
                        dp[i, j, rd, 1] = isWall > 0
                            ? Math.Max(
                                dp[i - 1, j - 1, rd, 1] + isWall,
                                dp[i - 1, j - 1, rd, 0] + 1
                            )
                            : dp[i - 1, j - 1, rd, 0] + 1;
                        dp[i, j, rd, 0] = dp[i - 1, j - 1, rd, 0] + isWall;

                        if (isWall == 0)
                        {
                            dp[i, j, rd, 0] = 0;
                        }
                    }
                    else
                    {
                        dp[i, j, rd, 0] = isWall;
                        dp[i, j, rd, 1] = 1;
                        dp[i, j, rd, 2] = 1;
                    }

                    if (i > 0 && j != n - 1)
                    {
                        dp[i, j, ld, 2] = isWall > 0
                            ? Math.Max(
                                dp[i - 1, j + 1, ld, 2] + isWall,
                                dp[i - 1, j + 1, ld, 1] + 1
                            )
                            : dp[i - 1, j + 1, ld, 1] + 1;
                        dp[i, j, ld, 1] = isWall > 0
                            ? Math.Max(
                                dp[i - 1, j + 1, ld, 1] + isWall,
                                dp[i - 1, j + 1, ld, 0] + 1
                            )
                            : dp[i - 1, j + 1, ld, 0] + 1;
                        dp[i, j, ld, 0] = dp[i - 1, j + 1, ld, 0] + isWall;

                        if (isWall == 0)
                        {
                            dp[i, j, ld, 0] = 0;
                        }
                    }
                    else
                    {
                        dp[i, j, ld, 0] = isWall;
                        dp[i, j, ld, 1] = 1;
                        dp[i, j, ld, 2] = 2;
                    }

                    for (var k = 0; k < 4; ++k)
                    {
                        for (var l = 0; l < 3; ++l)
                        {
                            if (dp[i, j, k, l] >= 6)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}
