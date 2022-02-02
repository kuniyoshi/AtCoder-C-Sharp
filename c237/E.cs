#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AtCoder.c237
{
    internal static class E
    {
        internal static void Run()
        {
            var nm = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var n = nm[0];
            var m = nm[1];
            var h = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var uvItems = Enumerable.Range(0, m)
                .Select(_ => Console.ReadLine()!.Split().Select(number => int.Parse(number) - 1).ToArray())
                .ToArray();

            var neighborsOf = Enumerable.Range(0, n).Select(_ => new List<int>()).ToArray();

            foreach (var uv in uvItems)
            {
                neighborsOf[uv[0]].Add(uv[1]);
                neighborsOf[uv[1]].Add(uv[0]);
            }

            var queue = new LowerPriorQueue();
            queue.Push(Tuple.Create(0, 0));

            var cost = new Dictionary<int, int>
            {
                [0] = 0,
            };

            while (queue.Any())
            {
                var (destinationCost, destination) = queue.Pop();

                if (!cost.ContainsKey(destination) || destinationCost != cost[destination])
                {
                    continue;
                }

                foreach (var neighbor in neighborsOf[destination])
                {
                    var candidate = destinationCost + Math.Max(h[neighbor] - h[destination], 0);

                    if (cost.ContainsKey(neighbor) && candidate >= cost[neighbor])
                    {
                        continue;
                    }

                    cost[neighbor] = candidate;
                    queue.Push(Tuple.Create(candidate, neighbor));
                }
            }

            var max = 0;

            for (var i = 0; i < n; ++i)
            {
                if (!cost.ContainsKey(i))
                {
                    continue;
                }

                var value = h[0] - h[i] - cost[i];
                max = Math.Max(max, value);
            }

            Console.WriteLine(max);
        }

        class LowerPriorQueue
        {
            List<Tuple<int, int>> Items { get; } = new List<Tuple<int, int>>();

            internal bool Any()
            {
                return Items.Any();
            }

            internal Tuple<int, int> Peek()
            {
                return Items[index: 0];
            }

            internal Tuple<int, int> Pop()
            {
                Debug.Assert(Items.Any(), "Items.Any()");
                return Heap.ReversePopFrom(Items);
            }

            internal void Push(Tuple<int, int> value)
            {
                Heap.ReversePushTo(Items, value);
            }

            static class Heap
            {
                internal static Tuple<int, int> ReversePopFrom(List<Tuple<int, int>> buffer)
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

                        var child = right < buffer.Count && buffer[left].Item1 >= buffer[right].Item1
                            ? right
                            : left;

                        if (buffer[cursor].Item1 <= buffer[child].Item1)
                        {
                            break;
                        }

                        (buffer[cursor], buffer[child]) = (buffer[child], buffer[cursor]);
                        cursor = child;
                    }

                    return lastRoot;
                }

                internal static void ReversePushTo(List<Tuple<int, int>> buffer, Tuple<int, int> item)
                {
                    buffer.Add(item);
                    var cursor = buffer.Count - 1;

                    while (cursor != 0)
                    {
                        var parent = (cursor - 1) / 2;

                        if (buffer[parent].Item1 <= buffer[cursor].Item1)
                        {
                            break;
                        }

                        (buffer[parent], buffer[cursor]) = (buffer[cursor], buffer[parent]);
                        cursor = parent;
                    }
                }
            }
        }
    }
}
