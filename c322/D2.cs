#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c322
{
    internal static class D2
    {
        internal static void Run()
        {
            var aList = Rotations(ReadInput.ReadGrid()).Select(Bits).SelectMany(Moves).ToArray();
            var bList = Rotations(ReadInput.ReadGrid()).Select(Bits).SelectMany(Moves).ToArray();
            var cList = Rotations(ReadInput.ReadGrid()).Select(Bits).SelectMany(Moves).ToArray();

            foreach (var a in aList)
            {
                foreach (var b in bList)
                {
                    if (IsOverlap(a, b))
                    {
                        continue;
                    }

                    foreach (var c in cList)
                    {
                        if (IsOverlap(a, c) || IsOverlap(b, c))
                        {
                            continue;
                        }

                        if (IsFulfill(a, b, c))
                        {
                            Console.WriteLine("Yes");
                            return;
                        }
                    }
                }
            }

            Console.WriteLine("No");
        }

        static bool IsFulfill(int[] a, int[] b, int[] c)
        {
            var results = new int[4];

            for (var i = 0; i < 4; ++i)
            {
                results[i] |= a[i];
            }
            for (var i = 0; i < 4; ++i)
            {
                results[i] |= b[i];
            }
            for (var i = 0; i < 4; ++i)
            {
                results[i] |= c[i];
            }

            return results.All(r => r == 15);
        }

        static bool IsOverlap(int[] a, int[] b)
        {
            for (var i = 0; i < 4; ++i)
            {
                if ((a[i] & b[i]) > 0)
                {
                    return true;
                }
            }

            return false;
        }

        internal static int[]? Move(int[] bits, int di, int dj)
        {
            var results = bits.ToArray();

            var popped = results.Reverse().Take(di).ToArray();

            if (popped.Any(p => p > 0))
            {
                return null;
            }

            results = Enumerable.Repeat(0, di).Concat(results).ToArray();

            var shifted = results.Take(-di);

            if (shifted.Any(s => s > 0))
            {
                return null;
            }

            results = Enumerable.Repeat(0, -di).Concat(results).ToArray();

            for (var i = 0; i < 4; ++i)
            {
                for (var j = 0; j < dj; ++j)
                {
                    if ((results[i] & 1) == 1)
                    {
                        return null;
                    }

                    results[i] >>= 1;
                }
            }

            for (var i = 0; i < 4; ++i)
            {
                for (var j = 0; j < -dj; ++j)
                {
                    if ((results[i] & 1 << 3) > 0)
                    {
                        return null;
                    }

                    results[i] <<= 1;
                }
            }

            return results.ToArray();
        }

        internal static int[][] Moves(int[] bits)
        {
            var results = new List<int[]>();

            for (var i = -4; i < 4; ++i)
            {
                for (var j = -4; j < 4; ++j)
                {
                    var moved = Move(bits, i, j);

                    if (moved != null)
                    {
                        results.Add(moved);
                    }
                }
            }

            return results.ToArray();
        }

        internal static int[] Bits(string[] grid)
        {
            var results = Enumerable.Repeat(0, 4).ToArray();

            for (var i = 0; i < grid.Length; ++i)
            {
                for (var j = 0; j < grid[i].Length; ++j)
                {
                    if (grid[i][j] == '#')
                    {
                        results[i] |= 1 << j;
                    }
                }
            }

            return results;
        }

        internal static string[][] Rotations(string[] grid)
        {
            var results = new List<string[]> { grid };

            for (var i = 0; i < 3; ++i)
            {
                results.Add(Rotate(results.Last()));
            }

            return results.ToArray();
        }

        static string[] Rotate(string[] grid)
        {
            var results = Enumerable.Repeat("....", 4).Select(s => s.ToCharArray()).ToArray();

            for (var i = 0; i < grid.Length; ++i)
            {
                for (var j = 0; j < grid[0].Length; ++j)
                {
                    results[i][j] = grid[j][i];
                }
            }

            return results.Reverse().Select(chars => chars.ToString()).ToArray();
        }

        static class ReadInput
        {
            internal static string[] ReadGrid()
            {
                return Enumerable.Range(0, 4).Select(_ => Console.ReadLine()!).ToArray();
            }
        }
    }
}
