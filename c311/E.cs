#nullable enable
using System;
using System.Linq;

namespace AtCoder.c311
{
    internal static class E
    {
        internal static void Run()
        {
            var (h, w, n) = ReadInput.ReadArrayInt3();
            var holes = ReadInput.ReadArrayArrayInt(n);

            var isHole = Enumerable.Range(0, h + 1)
                .Select(_ => Enumerable.Range(0, w + 1).Select(_ => false).ToArray())
                .ToArray();

            foreach (var hole in holes)
            {
                isHole[hole[0]][hole[1]] = true;
            }

            var acc = Enumerable.Range(0, h + 1)
                .Select(_ => Enumerable.Range(0, w + 1).Select(_ => 0).ToArray())
                .ToArray();

            foreach (var hole in holes)
            {
                acc[hole[0]][hole[1]] = 1;
            }

            for (var i = 1; i < h + 1; ++i)
            {
                for (var j = 1; j < w + 1; ++j)
                {
                    acc[i][j] += acc[i][j - 1];
                }
            }

            for (var j = 1; j < w + 1; ++j)
            {
                for (var i = 1; i < h + 1; ++i)
                {
                    acc[i][j] += acc[i - 1][j];
                }
            }

            var count = 0L;
            var maxSize = Math.Min(h, w);

            for (var i = 1; i < h + 1; ++i)
            {
                for (var j = 1; j < w + 1; ++j)
                {
                    if (isHole[i][j])
                    {
                        continue;
                    }

                    var ac = 0;
                    var wa = maxSize;

                    while (wa - ac > 1)
                    {
                        var wj = (wa + ac) / 2;

                        if (!IsIn(i, h, j, w, wj))
                        {
                            wa = wj;
                            continue;
                        }

                        var holeCount = acc[i + wj][j + wj]
                                        + acc[i - 1][j - 1]
                                        - acc[i - 1][j + wj]
                                        - acc[i + wj][j - 1];

                        if (holeCount > 0)
                        {
                            wa = wj;
                        }
                        else
                        {
                            ac = wj;
                        }
                    }

                    count += ac + 1;
                }
            }

            Console.WriteLine(count);
        }

        static bool IsIn(int i, int height, int j, int width, int size)
        {
            return i + size >= 0 && i + size <= height && j + size >= 0 && j + size <= width;
        }

        static class ReadInput
        {
            internal static int ReadSingle()
            {
                return int.Parse(Console.ReadLine()!);
            }

            internal static int[] ReadArrayInt()
            {
                return Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            }

            internal static (int, int, int, int) ReadArrayInt4()
            {
                var array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
                return (array[0], array[1], array[2], array[3]);
            }

            internal static (int, int, int) ReadArrayInt3()
            {
                var array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
                return (array[0], array[1], array[2]);
            }

            internal static int[][] ReadArrayArrayInt(int n)
            {
                return Enumerable.Range(0, n)
                    .Select(_ => ReadArrayInt())
                    .ToArray();
            }
        }
    }
}
