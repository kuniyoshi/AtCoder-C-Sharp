#nullable enable
using System;
using System.Linq;

namespace AtCoder.c225
{
    internal static class C
    {
        internal static void Run()
        {
            var nm = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var (n, m) = (nm[0], nm[1]);
            var b = Enumerable.Range(0, n)
                .Select(_ => Console.ReadLine()!.Split().Select(int.Parse).Select(v => v - 1).ToArray())
                .ToArray();

            var isYes = IsYes(b, n, m);

            Console.WriteLine(isYes ? "Yes" : "No");
        }

        static bool IsYes(int[][] b, int n, int m)
        {
            var (minI, minJ) = GetLeftTop(b[0][0]);
            var maxExclusiveJ = minJ + m;

            if (maxExclusiveJ > 7)
            {
                return false;
            }

            for (var i = minI; i < minI + n; ++i)
            {
                for (var j = minJ; j < maxExclusiveJ; ++j)
                {
                    var got = b[i - minI][j - minJ];
                    var expected = i * 7 + j;

                    if (got != expected)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        static (int, int) GetLeftTop(int b)
        {
            var i = b / 7;
            var j = b % 7;
            return (i, j);
        }
    }
}
