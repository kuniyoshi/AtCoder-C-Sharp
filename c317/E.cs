#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c317
{
    internal static class E
    {
        internal static void Run()
        {
            var (h, w) = ReadInput.ReadArrayInt2();
            var cells = Enumerable.Range(0, h).Select(_ => Console.ReadLine()!).ToArray();

            // 11 10 9 8 7 6 5 4 3 2 1 0
            // St Go W G G G G P P P P .
            // S  G  # ^ > v < ^ > v < .

            var map = new int[h, w];

            for (var i = 0; i < h; ++i)
            {
                for (var j = 0; j < w; ++j)
                {
                    switch (cells[i][j])
                    {
                        case '.':
                            if (i > 0 && j > 0)
                            {
                                map[i, j] = (map[i - 1, j] & 1 << 6) | (map[i, j - 1] & 1 << 7);
                            }
                            else if (i > 0)
                            {
                                map[i, j] = map[i - 1, j] & 1 << 6;
                            }
                            else if (j > 0)
                            {
                                map[i, j] = map[i, j - 1] & 1 << 7;
                            }
                            else
                            {
                                map[i, j] = 0;
                            }

                            break;

                        case '^':
                            map[i, j] = (1 << 4) | (1 << 8);
                            break;

                        case '>':
                            map[i, j] = (1 << 3) | (1 << 7);
                            break;

                        case 'v':
                            map[i, j] = (1 << 2) | (1 << 6);
                            break;

                        case '<':
                            map[i, j] = (1 << 1) | (1 << 5);
                            break;

                        case '#':
                            map[i, j] = 1 << 9;
                            break;

                        case 'G':
                            map[i, j] = 1 << 10;
                            break;

                        case 'S':
                            map[i, j] = 1 << 11;
                            break;

                        default:
                            throw new Exception($"Un expected {cells[i][j]}");
                    }
                }
            }

            for (var i = h - 1; i >= 0; --i)
            {
                for (var j = w - 1; j >= 0; --j)
                {
                    if (cells[i][j] != '.')
                    {
                        continue;
                    }

                    if (i + 1 == h && j + 1 == w)
                    {
                        // do nothing
                    }
                    else if (i + 1 == h)
                    {
                        map[i, j] |= map[i, j + 1] & 1 << 5;
                    }
                    else if (j + 1 == w)
                    {
                        map[i, j] |= map[i + 1, j] & 1 << 8;
                    }
                    else
                    {
                        map[i, j] |= (map[i + 1, j] & 1 << 8) | (map[i, j + 1] & 1 << 5);
                    }
                }
            }

            var start = FindStart(cells);

            var step = -1;
            var queue = new Queue<Tuple<Tuple<int, int>, int>>();
            queue.Enqueue(Tuple.Create(start, 0));
            var visited = new bool[h, w];
            var deltas = new[]
            {
                Tuple.Create(1, 0),
                Tuple.Create(0, 1),
                Tuple.Create(0, -1),
                Tuple.Create(-1, 0),
            };

            while (queue.Count > 0)
            {
                var ((ci, cj), distance) = queue.Dequeue();

                if (visited[ci, cj])
                {
                    continue;
                }

                visited[ci, cj] = true;

                if (map[ci, cj] == 1 << 10)
                {
                    step = distance;
                    break;
                }

                foreach (var (di, dj) in deltas)
                {
                    var (ni, nj) = (di + ci, dj + cj);

                    if (ni >= 0
                        && ni < h
                        && nj >= 0
                        && nj < w
                        && (map[ni, nj] == 0 || map[ni, nj] == 1 << 10)
                        && !visited[ni, nj])
                    {
                        queue.Enqueue(Tuple.Create(Tuple.Create(ni, nj), distance + 1));
                    }
                }
            }

            Console.WriteLine(step);
        }

        static Tuple<int, int> FindStart(string[] cells)
        {
            for (var i = 0; i < cells.Length; ++i)
            {
                for (var j = 0; j < cells[0].Length; ++j)
                {
                    if (cells[i][j] == 'S')
                    {
                        return Tuple.Create(i, j);
                    }
                }
            }

            throw new Exception("Could not find start point");
        }

        static class ReadInput
        {
            internal static (int, int) ReadArrayInt2()
            {
                var array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
                return (array[0], array[1]);
            }
        }
    }
}
