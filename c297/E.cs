#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AtCoder.c297
{
    internal static class E
    {
        internal static void Run()
        {
            var (n, k) = ReadInput.ReadArrayInt2();
            var a = ReadInput.ReadArrayLong();
            Array.Sort(a);

            var values = a.Distinct().ToArray();

            var used = new HashSet<long>();
            var queue = new LowerPriorQueue();

            queue.Push(0);
            used.Add(0);

            for (var i = 0; i < k; ++i)
            {
                var min = queue.Pop();

                foreach (var value in values)
                {
                    if (used.Contains(min + value))
                    {
                        continue;
                    }
                    queue.Push(min + value);
                    used.Add(min + value);
                }
            }

            Console.WriteLine(queue.Peek());
        }

        class LowerPriorQueue
        {
            List<long> Items { get; } = new List<long>();

            internal bool Any()
            {
                return Items.Any();
            }

            internal long Peek()
            {
                return Items[index: 0];
            }

            internal long Pop()
            {
                Debug.Assert(Items.Any(), "Items.Any()");
                return Heap.ReversePopFrom(Items);
            }

            internal void Push(long value)
            {
                Heap.ReversePushTo(Items, value);
            }
        }

        static class Heap
        {
            internal static long PopFrom(List<long> buffer)
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

            internal static void PushTo(List<long> buffer, long item)
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

            internal static long ReversePopFrom(List<long> buffer)
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

            internal static void ReversePushTo(List<long> buffer, long item)
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
        }

        static class ReadInput
        {
            internal static long[] ReadArrayLong()
            {
                return Console.ReadLine()!.Split().Select(long.Parse).ToArray();
            }

            internal static (int, int) ReadArrayInt2()
            {
                var array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
                return (array[0], array[1]);
            }
        }
    }
}
