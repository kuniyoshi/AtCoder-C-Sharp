#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c259
{
    internal static class D
    {
        internal static void Run()
        {
            var n = ReadInt();
            var (sx, sy, tx, ty) = Read4Ints();
            var circles = ReadIntsLines(n);

            // var found = SolveByBfs(n, circles, sx, sy, tx, ty);
            var found = SolveByUnionFindTree(n, circles, sx, sy, tx, ty);

            Console.WriteLine(found ? "Yes" : "No");
        }

        static (int, int, int) ExtractInts(int[] items)
        {
            return (items[0], items[1], items[2]);
        }

        static int GetIndex(int[][] circles, int n, int x, int y)
        {
            for (var i = 0; i < n; ++i)
            {
                var (x1, y1, r) = ExtractInts(circles[i]);

                // sqrt( (x - x1)^2 + (y - y1)^2 ) == r 
                // (x - x1)^2 + (y - y1)^2 == r^2 
                if (Pow2(x - x1) + Pow2(y - y1) == Pow2(r))
                {
                    return i;
                }
            }

            throw new Exception("no circle found");
        }

        static long Pow2(long x)
        {
            return x * x;
        }

        static (int, int, int, int) Read4Ints()
        {
            var line = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            return (line[0], line[1], line[2], line[3]);
        }

        static int ReadInt()
        {
            return int.Parse(Console.ReadLine()!);
        }

        static int[][] ReadIntsLines(int n)
        {
            return Enumerable.Range(start: 0, n)
                .Select(_ => Console.ReadLine()!.Split().Select(int.Parse).ToArray())
                .ToArray();
        }

        static bool SolveByBfs(int n,
                               int[][] circles,
                               int sx,
                               int sy,
                               int tx,
                               int ty)
        {
            var graph = new List<int>?[n];

            for (var i = 0; i < n; ++i)
            {
                for (var j = 0; j < i; ++j)
                {
                    var (xi, yi, ri) = ExtractInts(circles[i]);
                    var (xj, yj, rj) = ExtractInts(circles[j]);
                    var d2 = Pow2(xi - xj) + Pow2(yi - yj);

                    if (d2 > (ri + rj) * (ri + rj))
                    {
                        continue;
                    }

                    // d + rs < rl
                    // d < rl - rs
                    // d2 < (rl - rs)^2
                    if (d2 < (ri - rj) * (ri - rj))
                    {
                        continue;
                    }

                    graph[i] ??= new List<int>();
                    graph[i]!.Add(j);

                    graph[j] ??= new List<int>();
                    graph[j]!.Add(i);
                }
            }

            var from = GetIndex(circles, n, sx, sy);
            var to = GetIndex(circles, n, tx, ty);

            var queue = new Queue<int>();
            queue.Enqueue(from);
            var found = false;
            var visited = new HashSet<int>();

            while (queue.Any())
            {
                var v = queue.Dequeue();

                if (visited.Contains(v))
                {
                    continue;
                }

                visited.Add(v);

                if (v == to)
                {
                    found = true;
                    break;
                }

                if (graph[v] == null)
                {
                    continue;
                }

                foreach (var w in graph[v]!.Where(w => !visited.Contains(w)))
                {
                    queue.Enqueue(w);
                }
            }

            return found;
        }

        static bool SolveByUnionFindTree(int n,
                                         int[][] circles,
                                         int sx,
                                         int sy,
                                         int tx,
                                         int ty)
        {
            var tree = new UnionFindTree(n);

            for (var i = 0; i < n; ++i)
            {
                for (var j = 0; j < i; ++j)
                {
                    var (xi, yi, ri) = ExtractInts(circles[i]);
                    var (xj, yj, rj) = ExtractInts(circles[j]);

                    var d2 = Pow2(xi - xj) + Pow2(yi - yj);

                    if (d2 > Pow2(ri + rj))
                    {
                        continue;
                    }

                    // d + rs < rl
                    // d < rl - rs
                    // d^2 < (rl - rs)^2
                    if (d2 < Pow2(ri - rj))
                    {
                        continue;
                    }

                    tree.Unite(i, j);
                }
            }

            int? from = null;
            int? to = null;

            for (var i = 0; i < n; ++i)
            {
                var (x, y, r) = ExtractInts(circles[i]);

                if (!from.HasValue && Pow2(sx - x) + Pow2(sy - y) == Pow2(r))
                {
                    from = i;
                    continue;
                }

                if (!to.HasValue && Pow2(tx - x) + Pow2(ty - y) == Pow2(r))
                {
                    to = i;
                    continue;
                }

                if (from.HasValue && to.HasValue)
                {
                    break;
                }
            }

            if (!from.HasValue || !to.HasValue)
            {
                throw new Exception($"Could not found from or to: ({from}, {to})");
            }

            return tree.Root(from.Value) == tree.Root(to.Value);
        }

        internal class UnionFindTree
        {
            int[] Parents { get; }
            int[] Sizes { get; }

            internal UnionFindTree(int n)
            {
                Sizes = Enumerable.Repeat(element: 1, n).ToArray();
                Parents = Enumerable.Range(start: 0, n).ToArray();
            }

            internal int Root(int v)
            {
                var parent = Parents[v];

                if (parent == v)
                {
                    return v;
                }

                Parents[v] = Root(parent);

                return Parents[v];
            }

            internal int Size(int u)
            {
                return Sizes[Root(u)];
            }

            internal void Unite(int u, int v)
            {
                var (rootU, rootV) = (Root(u), Root(v));

                if (rootU == rootV)
                {
                    return;
                }

                var (sizeU, sizeV) = (Size(u), Size(v));
                Sizes[rootV] = sizeU + sizeV;
                Sizes[rootU] = sizeU + sizeV;
                Parents[rootU] = rootV;
            }
        }
    }
}
