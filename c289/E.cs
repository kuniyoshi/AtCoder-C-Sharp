#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c289
{
    internal static class E
    {
        internal static void Run()
        {
            var t = int.Parse(Console.ReadLine());

            while (t-- > 0)
            {
                var cost = Solve();
                Console.WriteLine(cost);
            }
        }

        static int Solve()
        {
            var nm = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var (n, m) = (nm[0], nm[1]);
            var c = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var edges = Enumerable.Range(0, m).Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

            var links = new Dictionary<int, List<int>>();

            foreach (var edge in edges)
            {
                var (u, v) = (edge[0], edge[1]);
                u--;
                v--;

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

            var queue = new Queue<Tuple<int, int, int>>();
            var visited = new Dictionary<int, Dictionary<int, bool>>();

            queue.Enqueue(Tuple.Create(0, n - 1, 0));

            while (queue.Any())
            {
                var tuple = queue.Dequeue();
                var (t, a, cost) = (tuple.Item1, tuple.Item2, tuple.Item3);

                if (t == n - 1 && a == 0)
                {
                    return cost;
                }

                if (!visited.ContainsKey(t))
                {
                    visited[t] = new Dictionary<int, bool>();
                }

                if (visited[t].ContainsKey(a))
                {
                    continue;
                }

                visited[t][a] = true;

                if (!links.ContainsKey(t) || !links.ContainsKey(a))
                {
                    continue;
                }

                foreach (var tn in links[t])
                {
                    if (!visited.ContainsKey(tn))
                    {
                        visited[tn] = new Dictionary<int, bool>();
                    }

                    foreach (var an in links[a])
                    {
                        if (visited[tn].ContainsKey(an))
                        {
                            continue;
                        }

                        if (c[tn] == c[an])
                        {
                            continue;
                        }

                        queue.Enqueue(Tuple.Create(tn, an, cost + 1));
                    }
                }
            }

            return -1;
        }
    }
}
