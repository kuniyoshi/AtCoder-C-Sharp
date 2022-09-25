#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c270
{
    internal static class C
    {
        internal static void Run()
        {
            var first = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var (n, x, y) = (first[0], first[1], first[2]);
            var edges = Enumerable.Range(0, n - 1)
                .Select(_ => Console.ReadLine()!.Split().Select(int.Parse).ToArray())
                .ToArray();
            x--;
            y--;
            var vertexes = Bfs(x, y, Links(edges));
            Console.WriteLine(string.Join(" ", vertexes.Select(v => v + 1)));
        }

        static List<int> Bfs(int from, int to, Dictionary<int, List<int>> links)
        {
            if (links[from].Contains(to))
            {
                return new List<int> { from, to };
            }

            var visited = new Dictionary<int, bool>();
            var queue = new Queue<List<int>>();
            queue.Enqueue(new List<int> { from });

            while (queue.Any())
            {
                var items = queue.Dequeue();
                var v = items.Last();

                if (visited.ContainsKey(v))
                {
                    continue;
                }

                visited[v] = true;

                if (links[v].Contains(to))
                {
                    var result = new List<int>(items) { to };
                    return result;
                }

                foreach (var u in links[v].Where(u => !visited.ContainsKey(u)))
                {
                    queue.Enqueue(new List<int>(items) { u });
                }
            }

            throw new Exception("Could not found passes");
        }

        static Dictionary<int, List<int>> Links(int[][] edges)
        {
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

            return links;
        }
    }
}
