#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AtCoder.c223
{
    internal static class D
    {
        internal static void Run()
        {
            var heads = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var n = heads[0];
            var m = heads[1];
            var edges = Enumerable.Range(start: 0, m)
                .Select(_ => Console.ReadLine()!.Split().Select(int.Parse).ToArray())
                .ToArray();

            var fromTo = new Dictionary<int, Dictionary<int, bool>>();
            var toFrom = new Dictionary<int, Dictionary<int, bool>>();

            foreach (var edge in edges)
            {
                var from = edge[0];
                var to = edge[1];

                if (!fromTo.ContainsKey(from))
                {
                    fromTo.Add(from, new Dictionary<int, bool>());
                }

                fromTo[from][to] = true;

                if (!toFrom.ContainsKey(to))
                {
                    toFrom.Add(to, new Dictionary<int, bool>());
                }

                toFrom[to][from] = true;
            }

            var queue = new LowerPriorQueue();

            for (var i = 1; i <= n; ++i)
            {
                if (!toFrom.ContainsKey(i))
                {
                    queue.Push(i);
                }
            }

            var s = new List<int>();

            while (queue.Any())
            {
                var u = queue.Pop();
                s.Add(u);

                if (fromTo.ContainsKey(u))
                {
                    foreach (var to in fromTo[u].Keys)
                    {
                        toFrom[to].Remove(u);

                        if (!toFrom[to].Any())
                        {
                            toFrom.Remove(to);
                            queue.Push(to);
                        }
                    }
                }

                toFrom.Remove(u);
            }

            if (toFrom.Any())
            {
                Console.WriteLine(value: -1);
                return;
            }

            Console.WriteLine(string.Join(" ", s.Select(v => v.ToString())));
        }

        class LowerPriorQueue
        {
            List<int> Items { get; } = new List<int>();

            public bool Any()
            {
                return Items.Any();
            }

            public int Pop()
            {
                Debug.Assert(Items.Any(), "Items.Any()");
                return Heap.ReversePopFrom(Items);
            }

            public void Push(int value)
            {
                Heap.ReversePushTo(Items, value);
            }

            static class Heap
            {
                public static int ReversePopFrom(List<int> buffer)
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

                        var child = right < buffer.Count && buffer[left] >= buffer[right]
                            ? right
                            : left;

                        if (buffer[cursor] >= buffer[child])
                        {
                            (buffer[cursor], buffer[child]) = (buffer[child], buffer[cursor]);
                        }

                        cursor = child;
                    }

                    return lastRoot;
                }

                public static void ReversePushTo(List<int> buffer, int item)
                {
                    buffer.Add(item);
                    var cursor = buffer.Count - 1;

                    while (cursor != 0)
                    {
                        var parent = (cursor - 1) / 2;

                        if (buffer[parent] >= buffer[cursor])
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
