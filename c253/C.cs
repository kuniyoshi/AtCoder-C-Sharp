#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AtCoder.c253
{
    internal static class C
    {
        internal static void Run()
        {
            var q = int.Parse(Console.ReadLine()!);
            var queries = Enumerable.Range(0, q)
                .Select(_ => Console.ReadLine()!.Split().Select(int.Parse).ToArray())
                .ToArray();

            var set = new Dictionary<int, int>();
            var lowerPriorQueue = new LowerPriorQueue();
            var higherPriorQueue = new HigherPriorQueue();

            foreach (var query in queries)
            {
                var operation = query[0];

                switch (operation)
                {
                    case 1:
                        var x1 = query[1];

                        if (!set.ContainsKey(x1))
                        {
                            set[x1] = 0;
                        }

                        set[x1]++;

                        lowerPriorQueue.Push(x1);
                        higherPriorQueue.Push(x1);

                        break;

                    case 2:
                        var (x2, c) = (query[1], query[2]);

                        if (set.ContainsKey(x2))
                        {
                            set[x2] -= c;

                            if (set[x2] <= 0)
                            {
                                set.Remove(x2);
                            }

                            while (lowerPriorQueue.Any() && !set.ContainsKey(lowerPriorQueue.Peek()))
                            {
                                lowerPriorQueue.Pop();
                            }
                            while (higherPriorQueue.Any() && !set.ContainsKey(higherPriorQueue.Peek()))
                            {
                                higherPriorQueue.Pop();
                            }
                        }
                        
                        break;

                    case 3:
                        if (!lowerPriorQueue.Any() || !higherPriorQueue.Any())
                        {
                            throw new Exception("invalid operation 3 occured while queue is empty");
                        }

                        Console.WriteLine(higherPriorQueue.Peek() - lowerPriorQueue.Peek());
                        break;

                    default:
                        throw new Exception($"un expected operation: {operation}");
                }
            }
        }

        class LowerPriorQueue
        {
            List<int> Items { get; } = new List<int>();

            internal bool Any()
            {
                return Items.Any();
            }

            internal int Peek()
            {
                return Items[index: 0];
            }

            internal int Pop()
            {
                Debug.Assert(Items.Any(), "Items.Any()");
                return Heap.ReversePopFrom(Items);
            }

            internal void Push(int value)
            {
                Heap.ReversePushTo(Items, value);
            }
        }
        class HigherPriorQueue
        {
            List<int> Items { get; } = new List<int>();

            internal bool Any()
            {
                return Items.Any();
            }

            internal int Peek()
            {
                return Items[index: 0];
            }

            internal int Pop()
            {
                Debug.Assert(Items.Any(), "Items.Any()");
                return Heap.PopFrom(Items);
            }

            internal void Push(int value)
            {
                Heap.PushTo(Items, value);
            }
        }

        static class Heap
        {
            internal static int ReversePopFrom(List<int> buffer)
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

                    if (buffer[cursor] > buffer[child])
                    {
                        (buffer[cursor], buffer[child]) = (buffer[child], buffer[cursor]);
                    }

                    cursor = child;
                }

                return lastRoot;
            }
            internal static int PopFrom(List<int> buffer)
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

                    var child = right < buffer.Count && buffer[left] <= buffer[right]
                        ? right
                        : left;

                    if (buffer[cursor] < buffer[child])
                    {
                        (buffer[cursor], buffer[child]) = (buffer[child], buffer[cursor]);
                    }

                    cursor = child;
                }

                return lastRoot;
            }

            internal static void ReversePushTo(List<int> buffer, int item)
            {
                buffer.Add(item);
                var cursor = buffer.Count - 1;

                while (cursor != 0)
                {
                    var parent = (cursor - 1) / 2;

                    if (buffer[parent] > buffer[cursor])
                    {
                        (buffer[parent], buffer[cursor]) = (buffer[cursor], buffer[parent]);
                    }

                    cursor = parent;
                }
            }
            internal static void PushTo(List<int> buffer, int item)
            {
                buffer.Add(item);
                var cursor = buffer.Count - 1;

                while (cursor != 0)
                {
                    var parent = (cursor - 1) / 2;

                    if (buffer[parent] < buffer[cursor])
                    {
                        (buffer[parent], buffer[cursor]) = (buffer[cursor], buffer[parent]);
                    }

                    cursor = parent;
                }
            }
        }
    }
}
