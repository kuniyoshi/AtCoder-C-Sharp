#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c288
{
    internal static class C
    {
        internal static void Run()
        {
            var (n, m) = ReadArrayInt2();
            var edges = ReadArrayArrayInt(m);

            var links = new Dictionary<int, List<int>>();

            foreach (var edge in edges)
            {
                var (u, v) = (edge[0], edge[1]);
                u--;
                v--;

                if (!links.ContainsKey(u))
                {
                    links.Add(u, new List<int>());
                }

                if (!links.ContainsKey(v))
                {
                    links.Add(v, new List<int>());
                }

                links[u].Add(v);
                links[v].Add(u);
            }

            var tree = new UnionFindTree(n);

            foreach (var edge in edges)
            {
                var (u, v) = (edge[0], edge[1]);
                u--;
                v--;

                tree.Unite(u, v);
            }

            var root = new Dictionary<int, bool>();

            var count = 0;

            for (var i = 0; i < n; ++i)
            {
                if (root.ContainsKey(tree.Root(i)))
                {
                    continue;
                }

                root.Add(tree.Root(i), true);

                count += Dfs(i, -1, new Dictionary<int, bool>(), links);
            }

            Console.WriteLine(count / 2);
        }

        static int Dfs(int v, int from, Dictionary<int, bool> visited, Dictionary<int, List<int>> links)
        {
            if (visited.ContainsKey(v))
            {
                return 1;
            }

            visited.Add(v, true);

            var count = 0;

            if (links.ContainsKey(v))
            {
                foreach (var w in links[v])
                {
                    if (w == from)
                    {
                        continue;
                    }
                    count += Dfs(w, v, visited, links);
                }
            }

            return count;
        }

        static int[][] ReadArrayArrayInt(int n)
        {
            return Enumerable.Range(0, n)
                .Select(_ => ReadArrayInt())
                .ToArray();
        }

        static int[] ReadArrayInt()
        {
            return Console.ReadLine()!.Split().Select(int.Parse).ToArray();
        }

        static (int, int) ReadArrayInt2()
        {
            var array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            return (array[0], array[1]);
        }
    }

     class UnionFindTree
    {
        int[] Parents { get; }
        int[] Sizes { get; }

        internal UnionFindTree(int n)
        {
            Sizes = Enumerable.Repeat(element: 1, n).ToArray();
            Parents = Enumerable.Range(start: 0, n).ToArray();
        }

      internal  int Root(int v)
        {
            var parent = Parents[v];

            if (parent == v)
            {
                return v;
            }

            Parents[v] = Root(parent);

            return Parents[v];
        }

        int Size(int u)
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
