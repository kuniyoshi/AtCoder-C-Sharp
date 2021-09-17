#nullable enable
using System;
using System.Linq;
using static System.Console;

namespace AtCoder
{
    internal static class E
    {
        static void Main()
        {
            var head = ReadLine()!.Split();
            var n = int.Parse(head[0]);
            var m = int.Parse(head[1]);

            var edges = new Tuple<int, int, int>[m];

            for (var i = 0; i < m; ++i)
            {
                var line = ReadLine()!.Split();
                edges[i] = new Tuple<int, int, int>(int.Parse(line[0]) - 1, int.Parse(line[1]) - 1, int.Parse(line[2]));
            }

            var totalCost = edges.Where(edge => edge.Item3 > 0)
                .Select(edge => (long)edge.Item3)
                .Sum();

            var tree = new UnionFindTree(n);

            foreach (var (u, v, cost) in edges.OrderBy(edge => edge.Item3))
            {
                if (tree.Root(u) == tree.Root(v))
                {
                    continue;
                }

                tree.Unite(u, v);

                if (cost > 0)
                {
                    totalCost = totalCost - cost;
                }
            }

            WriteLine(totalCost);
        }

        class UnionFindTree
        {
            int[] Parents { get; }

            public UnionFindTree(int n)
            {
                Parents = Enumerable.Range(start: 0, n).ToArray();
            }

            public int Root(int v)
            {
                var parent = Parents[v];

                if (parent == v)
                {
                    return v;
                }

                return Parents[v] = Root(parent);
            }

            public void Unite(int u, int v)
            {
                var (rootU, rootV) = (Root(u), Root(v));

                if (rootU == rootV)
                {
                    return;
                }

                Parents[rootV] = rootU;
            }
        }
    }
}
