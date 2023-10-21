#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AtCoder.c325
{
    internal static class D
    {
        internal static void Run()
        {
            var n = ReadInput.ReadSingle();
            var items = ReadInput.ReadArrayArrayLong(n);
            var queue = new LowerPriorQueue();
            var count = 0;
            var current = 1L;

            foreach (var item in items.OrderBy(item => item[0]))
            {
                var (begin, duration) = (item[0], item[1]);
                var end = begin + duration;
                queue.Push(end);

                while (queue.Any() && queue.Peek() < begin)
                {
                    var candidate = queue.Pop();

                    if (candidate >= current)
                    {
                        count++;
                        current++;
                    }
                }

                current = begin;
            }

            while (queue.Any())
            {
                var candidate = queue.Pop();

                if (candidate >= current)
                {
                    count++;
                    current++;
                }
            }

            Console.WriteLine(count);
        }

        internal class LowerPriorQueue
        {
            List<long> Items { get; } = new List<long>();

            internal bool Any()
            {
                return Items.Count > 0;
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

        internal static class Heap
        {
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
            internal static int ReadSingle()
            {
                return int.Parse(Console.ReadLine()!);
            }

            internal static long[] ReadArrayLong()
            {
                return Console.ReadLine()!.Split().Select(long.Parse).ToArray();
            }

            internal static (int, int, int, int) ReadArrayInt4()
            {
                var array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
                return (array[0], array[1], array[2], array[3]);
            }

            internal static (int, int) ReadArrayInt2()
            {
                var array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
                return (array[0], array[1]);
            }

            internal static long[][] ReadArrayArrayLong(int n)
            {
                return Enumerable.Range(0, n)
                    .Select(_ => ReadArrayLong())
                    .ToArray();
            }
        }
    }
}
