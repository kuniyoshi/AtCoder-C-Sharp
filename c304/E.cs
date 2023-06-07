#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c304
{
    internal static class E
    {
        internal static void Run()
        {
            var (n, m) = ReadInput.ReadArrayInt2();
            var edges = ReadInput.ReadArrayArrayInt(m);
            var k = ReadInput.ReadInt();
            var conditions = ReadInput.ReadArrayArrayInt(k);
            var q = ReadInput.ReadInt();
            var queries = ReadInput.ReadArrayArrayInt(q);

            var tree = new UnionFindTree(n);

            foreach (var edge in edges)
            {
                var (u, v) = (edge[0], edge[1]);
                u--;
                v--;
                tree.Unite(u, v);
            }

            var condition = new HashSet<string>();

            foreach (var cond in conditions)
            {
                var (u, v) = (cond[0], cond[1]);
                u--;
                v--;
                condition.Add($"{tree.Root(u)},{tree.Root(v)}");
                condition.Add($"{tree.Root(v)},{tree.Root(u)}");
            }

            foreach (var query in queries)
            {
                var (u, v) = (query[0], query[1]);
                u--;
                v--;
                var bad = condition.Contains($"{tree.Root(u)},{tree.Root(v)}");
                Console.WriteLine(bad ? "No" : "Yes");
            }
        }

        static class ReadInput
        {
            static int[] ReadArrayInt()
            {
                return Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            }

            internal static (int, int) ReadArrayInt2()
            {
                var array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
                return (array[0], array[1]);
            }

            internal static int ReadInt()
            {
                return int.Parse(Console.ReadLine()!);
            }

            internal static int[][] ReadArrayArrayInt(int n)
            {
                return Enumerable.Range(0, n)
                    .Select(_ => ReadArrayInt())
                    .ToArray();
            }
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
