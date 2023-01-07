#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c284
{
    internal static class E
    {
        internal static void Run()
        {
            var (n, m) = ReadArrayInt2();
            var edges = ReadArrayArrayInt(m);

            var links = new Dictionary<int, List<int>>();

            foreach (var edge in edges)
            {
                var (u, v) = (edge[0], edge[1]);

                if (!links.ContainsKey(u))
                {
                    links[u] = new List<int>();
                }

                if (!links.ContainsKey(v))
                {
                    links[v] = new List<int>();
                }

                links[u].Add(v);
                links[v].Add(u);
            }

            var passed = new Dictionary<int, bool>
            {
                [1] = true,
            };

            Console.WriteLine(Dfs2(0, 1, 1, links));
            // Console.WriteLine(Dfs(1, 1, passed, links));
        }

        static int Dfs2(int parent, int v, int depth, Dictionary<int, List<int>> links)
        {
            if (!links.ContainsKey(v))
            {
                return depth;
            }

            var sum = 0;
            var count = 0;
            
            foreach (var w in links[v])
            {
                if (w == parent)
                {
                    continue;
                }

                count++;
                sum += Dfs2(v, w, depth + 1, links);
            }

            if (count == 0)
            {
                return depth;
            }

            return sum - depth * (count - 1);
        }

        static int Dfs(int v, int count, Dictionary<int, bool> passed, Dictionary<int, List<int>> links)
        {
            if (count >= 1_000_000)
            {
                return 1_000_000;
            }

            if (!links.ContainsKey(v))
            {
                return count;
            }

            var sum = 0;

            foreach (var w in links[v])
            {
                if (passed.ContainsKey(w))
                {
                    continue;
                }

                passed[w] = true;

                sum += Dfs(w, 1, passed, links);

                passed.Remove(w);
            }

            return Math.Min(count + sum, 1_000_000);
        }

        static (int, int) ReadArrayInt2()
        {
            var array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            return (array[0], array[1]);
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
    }
}
