#nullable enable
using System;
using System.Diagnostics;
using System.Linq;

namespace AtCoder.c241
{
    internal static class C
    {
        const int Two = 2;
        const int One = 1;
        const int Zero = 0;
        const int Rd = 2;
        const int Right = 0;
        const int Down = 1;
        const int Ld = 3;

        internal static void Run()
        {
            // Console.WriteLine(Can() ? "Yes" : "No");
            Console.WriteLine(Can2() ? "Yes" : "No");
        }

        static bool Can2()
        {
            var n = int.Parse(Console.ReadLine()!);
            var s = Enumerable.Repeat(0, n)
                .Select(_ => Console.ReadLine()!)
                .ToArray();

            var left = Enumerable.Range(-5, 6)
                .ToArray();
            var up = Enumerable.Range(-5, 6)
                .ToArray();
            var rightUp = Enumerable.Range(-5, 6)
                .ToArray();
            var leftUp = Enumerable.Range(-5, 6)
                .ToArray();

            static bool IsBetween(int x, int a, int b)
            {
                return x >= a && x <= b;
            }

            bool IsIn(int x)
            {
                return IsBetween(x, 0, n - 1);
            }

            for (var i = 0; i < n; ++i)
            {
                for (var j = 0; j < n; ++j)
                {
                    if (IsIn(j + left[0]))
                    {
                        var rightBlacks = left
                            .Select(dj => Tuple.Create(i, j + dj))
                            .Count(ij => s[ij.Item1][ij.Item2] == '#');

                        if (rightBlacks >= 4)
                        {
                            return true;
                        }
                    }

                    if (IsIn(i + up[0]))
                    {
                        var downBlacks = up
                            .Select(di => Tuple.Create(i + di, j))
                            .Count(ij => s[ij.Item1][ij.Item2] == '#');

                        if (downBlacks >= 4)
                        {
                            return true;
                        }
                    }

                    if (IsIn(i + rightUp[0]) && IsIn(j - rightUp[0]))
                    {
                        var leftDownBlacks = rightUp
                            .Select(x => Tuple.Create(i + x, j - x))
                            .Count(ij => s[ij.Item1][ij.Item2] == '#');

                        if (leftDownBlacks >= 4)
                        {
                            return true;
                        }
                    }

                    if (IsIn(i + leftUp[0]) && IsIn(j + leftUp[0]))
                    {
                        var rightDownBlacks = leftUp
                            .Select(x => Tuple.Create(i + x, j + x))
                            .Count(ij => s[ij.Item1][ij.Item2] == '#');

                        if (rightDownBlacks >= 4)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        static bool Can()
        {
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
                    Debug.Assert(dp[i, j, Right, Two] >= dp[i, j, Right, One]);
                    Debug.Assert(dp[i, j, Right, One] >= dp[i, j, Right, Zero]);
                    Debug.Assert(dp[i, j, Down, Two] >= dp[i, j, Down, One]);
                    Debug.Assert(dp[i, j, Down, One] >= dp[i, j, Down, Zero]);
                    Debug.Assert(dp[i, j, Rd, Two] >= dp[i, j, Rd, One]);
                    Debug.Assert(dp[i, j, Rd, One] >= dp[i, j, Rd, Zero]);
                    Debug.Assert(dp[i, j, Ld, Two] >= dp[i, j, Ld, One]);
                    Debug.Assert(dp[i, j, Ld, One] >= dp[i, j, Ld, Zero]);

                    if (j > 0)
                    {
                        Update(dp, isWall > 0, Right, i, j, i, j - 1);
                    }
                    else
                    {
                        UpdateOutOfAlign(dp, i, j, isWall, Right);
                    }

                    if (i > 0)
                    {
                        Update(dp, isWall > 0, Down, i, j, i - 1, j);
                    }
                    else
                    {
                        UpdateOutOfAlign(dp, i, j, isWall, Down);
                    }

                    if (i > 0 && j > 0)
                    {
                        Update(dp, isWall > 0, Rd, i, j, i - 1, j - 1);
                    }
                    else
                    {
                        UpdateOutOfAlign(dp, i, j, isWall, Rd);
                    }

                    if (i > 0 && j != n - 1)
                    {
                        Update(dp, isWall > 0, Ld, i, j, i - 1, j + 1);
                    }
                    else
                    {
                        UpdateOutOfAlign(dp, i, j, isWall, Ld);
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

        static void UpdateOutOfAlign(int[,,,] dp, int i, int j, int isWall, int align)
        {
            dp[i, j, align, Zero] = isWall;
            dp[i, j, align, One] = 1;
            dp[i, j, align, Two] = 1;
        }

        static void Update(int[,,,] dp,
                           bool isWall,
                           int align,
                           int i,
                           int j,
                           int previousI,
                           int previousJ)
        {
            dp[i, j, align, Two] = isWall
                ? dp[previousI, previousJ, align, Two] + 1
                : dp[previousI, previousJ, align, One] + 1;
            dp[i, j, align, One] = isWall
                ? dp[previousI, previousJ, align, One] + 1
                : dp[previousI, previousJ, align, Zero] + 1;
            dp[i, j, align, Zero] = isWall
                ? dp[previousI, previousJ, align, Zero] + 1
                : 0;
        }
    }
}
