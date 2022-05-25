#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AtCoder.c252
{
    internal static class E
    {
        internal static void Run()
        {
            var nm = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var (n, m) = (nm[0], nm[1]);
            var edges = Enumerable.Range(0, n).Select(_ => new List<Edge>()).ToArray();

            for (var i = 0; i < m; ++i)
            {
                var numbers = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
                var (a, b, cost) = (numbers[0], numbers[1], numbers[2]);
                a--;
                b--;
                edges[a].Add(new Edge(i + 1, a, b, cost));
                edges[b].Add(new Edge(i + 1, a, b, cost));
            }

            var costs = Enumerable.Repeat((long?)null, n).ToArray();
            var previous = Enumerable.Repeat((int?)null, n).ToArray();

            costs[0] = 0;

            var queue = new LowerPriorQueue();
            queue.Push(new Pair(0L, 0));

            var visited = new HashSet<int>();

            while (queue.Any())
            {
                var item = queue.Pop();
                var from = item.V;

                if (!costs[from].HasValue)
                {
                    throw new Exception($"{from} has no cost");
                }

                visited.Add(from);

                foreach (var edge in edges[from])
                {
                    var to = edge.OtherOf(from);

                    if (visited.Contains(to))
                    {
                        continue;
                    }
                    
                    var alternative = costs[from]!.Value + edge.Cost;

                    if (!costs[to].HasValue || alternative < costs[to]!.Value)
                    {
                        costs[to] = alternative;
                        queue.Push(new Pair(alternative, to));
                        previous[to] = edge.Number;
                    }
                }
            }

            Console.WriteLine(string.Join(" ", previous.Where(p => p != null)));
        }

        struct Edge
        {
            public Edge(int number, int a, int b, int cost)
            {
                Number = number;
                A = a;
                B = b;
                Cost = cost;
            }

            public int OtherOf(int side)
            {
                return side == A ? B : A;
            }

            public int Number { get; }
            public int A { get; }
            public int B { get; }
            public int Cost { get; }
        }

        struct Pair
        {
            public Pair(long cost, int v)
            {
                Cost = cost;
                V = v;
            }

            public long Cost { get; }
            public int V { get; }
        }

        class LowerPriorQueue
        {
            List<Pair> Items { get; } = new List<Pair>();

            internal bool Any()
            {
                return Items.Any();
            }

            internal Pair Pop()
            {
                Debug.Assert(Items.Any(), "Items.Any()");
                return Heap.ReversePopFrom(Items);
            }

            internal void Push(Pair value)
            {
                Heap.ReversePushTo(Items, value);
            }

            static class Heap
            {
                internal static Pair ReversePopFrom(List<Pair> buffer)
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

                internal static void ReversePushTo(List<Pair> buffer, Pair item)
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
        }
    }
}
