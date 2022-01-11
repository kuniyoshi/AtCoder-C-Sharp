#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AtCoder.c234
{
    internal static class D
    {
        internal static void Run()
        {
            var arguments = Console.ReadLine()!.Split()
                .Select(int.Parse)
                .ToArray();
            var n = arguments[0];
            var k = arguments[1];
            var p = Console.ReadLine()!.Split()
                .Select(int.Parse)
                .ToArray();

            var priorityQueue = new LowerPriorQueue();

            for (var i = 0; i < k; ++i)
            {
                priorityQueue.Push(p[i]);
            }

            var next = priorityQueue.Pop();
            Console.WriteLine(next);

            for (var i = k; i < p.Length; ++i)
            {
                var current = p[i];
                next = current > next ? current : next;
                priorityQueue.Push(next);
                Console.WriteLine(priorityQueue.Peek());
                next = priorityQueue.Peek();
                priorityQueue.Pop();
            }
        }

        class LowerPriorQueue
        {
            List<int> Items { get; } = new List<int>();

            public void Push(int value)
            {
                Heap.ReversePushTo(Items, value);
            }

            public int Peek()
            {
                return Items[0];
            }

            public int Pop()
            {
                Debug.Assert(Items.Any(), "Items.Any()");
                return Heap.ReversePopFrom(Items);
            }

            static class Heap
            {
                public static void ReversePushTo(List<int> buffer, int item)
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

                public static int ReversePopFrom(List<int> buffer)
                {
                    Debug.Assert(buffer.Any(), "buffer.Any()");
                    var lastRoot = buffer[0];
                    buffer[0] = buffer[buffer.Count - 1];
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
            }

            public bool Any()
            {
                return Items.Any();
            }
        }
    }
}
