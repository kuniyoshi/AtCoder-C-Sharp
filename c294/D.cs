#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AtCoder.c294
{
    internal static class D
    {
        internal static void Run()
        {
            var (n, q) = ReadInput.ReadArrayInt2();
            var queries = ReadInput.ReadArrayArrayInt(q);
            var queue = new LowerPriorQueue();
            var notGo = new HashSet<int>();
            var next = 1;

            foreach (var query in queries)
            {
                switch (query[0])
                {
                    case 1:
                        queue.Push(next);
                        notGo.Add(next);
                        next++;
                        break;

                    case 2:
                        notGo.Remove(query[1]);
                        break;

                    case 3:
                        while (!notGo.Contains(queue.Peek()))
                        {
                            queue.Pop();
                        }
                        Console.WriteLine(queue.Peek());
                        break;

                    default:
                        throw new Exception($"unknown operation: {query[0]}");
                }
            }
        }
    }

    internal class LowerPriorQueue
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
    }

    internal static class ReadInput
    {
        internal static int ReadSingle()
        {
            return int.Parse(Console.ReadLine()!);
        }

        internal static int[] ReadArrayInt()
        {
            return Console.ReadLine()!.Split().Select(int.Parse).ToArray();
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

        internal static int[][] ReadArrayArrayInt(int n)
        {
            return Enumerable.Range(0, n)
                .Select(_ => ReadArrayInt())
                .ToArray();
        }
    }
}
