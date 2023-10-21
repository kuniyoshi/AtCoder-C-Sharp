#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c325
{
    internal static class C
    {
        internal static void Run()
        {
            var (h, w) = ReadInput.ReadArrayInt2();
            var s = Enumerable.Range(0, h).Select(_ => Console.ReadLine()!).ToArray();
            var visited = new bool[h, w];
            var count = 0;

            for (var i = 0; i < h; ++i)
            {
                for (var j = 0; j < w; ++j)
                {
                    if (visited[i, j])
                    {
                        continue;
                    }

                    if (s[i][j] != '#')
                    {
                        continue;
                    }

                    count++;

                    var queue = new Queue<Tuple<int, int>>();
                    queue.Enqueue(Tuple.Create<int, int>(i, j));

                    while (queue.Count > 0)
                    {
                        var (row, col) = queue.Dequeue();

                        if (visited[row, col])
                        {
                            continue;
                        }

                        visited[row, col] = true;

                        for (var u = -1; u <= 1; ++u)
                        {
                            for (var v = -1; v <= 1; ++v)
                            {
                                if (u == 0 && v == 0)
                                {
                                    continue;
                                }

                                if (row + u < 0 || row + u >= h || col + v < 0 || col + v >= w)
                                {
                                    continue;
                                }

                                if (s[row + u][col + v] != '#')
                                {
                                    continue;
                                }

                                if (visited[row + u, col + v])
                                {
                                    continue;
                                }

                                queue.Enqueue(Tuple.Create<int, int>(row + u, col + v));
                            }
                        }
                    }
                }
            }

            Console.WriteLine(count);
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

            internal static (int, int) ReadArrayInt2()
            {
                var array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
                return (array[0], array[1]);
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
