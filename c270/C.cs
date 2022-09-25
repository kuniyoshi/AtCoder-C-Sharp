#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            var linksOf = GetLinks(edges);
            var passes = GetPasses(n, x, y, linksOf);
            Console.WriteLine(string.Join(" ", passes.Select(v => v + 1)));
        }

        static List<int> GetPasses(int n, int from, int to, Dictionary<int, List<int>> linksOf)
        {
            var d = Enumerable.Range(0, n).Select(v => v == from ? 0 : 2 * n).ToArray();
            var previous = new Dictionary<int, int>();

            var queue = new LowerPriorQueue();
            queue.Push(new Item(0, from));

            while (queue.Any())
            {
                var item = queue.Pop();
                var u = item.Vertex;

                foreach (var v in linksOf[item.Vertex])
                {
                    var alternative = d[u] + 1;

                    if (alternative < d[v])
                    {
                        d[v] = alternative;
                        queue.Push(new Item(alternative, v));
                        previous[v] = u;
                    }
                }
            }

            var passes = new List<int>();
            var cursor = to;

            while (previous.ContainsKey(cursor))
            {
                passes.Add(cursor);
                cursor = previous[cursor];
            }

            passes.Add(from);

            passes.Reverse();
            
            return passes;
        }

        struct Item
        {
            public Item(int cost, int vertex)
            {
                Cost = cost;
                Vertex = vertex;
            }

            public int Cost { get; }
            public int Vertex { get; }
        }

        static Dictionary<int, List<int>> GetLinks(int[][] edges)
        {
            var result = new Dictionary<int, List<int>>();

            foreach (var edge in edges)
            {
                var (u, v) = (edge[0], edge[1]);
                u--;
                v--;

                if (!result.ContainsKey(u))
                {
                    result[u] = new List<int>();
                }

                if (!result.ContainsKey(v))
                {
                    result[v] = new List<int>();
                }

                result[u].Add(v);
                result[v].Add(u);
            }

            return result;
        }

        static class Heap
        {
            internal static Item ReversePopFrom(List<Item> buffer)
            {
                Debug.Assert(buffer.Any(), "buffer.Any()");
                var lastRoot = buffer[index: 0];
                buffer[index: 0] = buffer[buffer.Count - 1];
                buffer.RemoveAt(buffer.Count - 1);

                var cursor = 0;
                int left;

                while ((left = 2 * cursor + 1) < buffer.Count)
                {
                    var right = left + 1;

                    var child = right < buffer.Count && buffer[left].Cost >= buffer[right].Cost
                        ? right
                        : left;

                    if (buffer[cursor].Cost > buffer[child].Cost)
                    {
                        (buffer[cursor], buffer[child]) = (buffer[child], buffer[cursor]);
                    }

                    cursor = child;
                }

                return lastRoot;
            }

            public static void ReversePushTo(List<Item> buffer, Item item)
            {
                buffer.Add(item);
                var cursor = buffer.Count - 1;

                while (cursor != 0)
                {
                    var parent = (cursor - 1) / 2;

                    if (buffer[parent].Cost > buffer[cursor].Cost)
                    {
                        (buffer[parent], buffer[cursor]) = (buffer[cursor], buffer[parent]);
                    }

                    cursor = parent;
                }
            }
        }

        class LowerPriorQueue
        {
            List<Item> Items { get; } = new List<Item>();

            internal bool Any()
            {
                return Items.Any();
            }

            internal Item Peek()
            {
                return Items[index: 0];
            }

            internal Item Pop()
            {
                Debug.Assert(Items.Any(), "Items.Any()");
                return Heap.ReversePopFrom(Items);
            }

            internal void Push(Item value)
            {
                Heap.ReversePushTo(Items, value);
            }
        }
    }
}
