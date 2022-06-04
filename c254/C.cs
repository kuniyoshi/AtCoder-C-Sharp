#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AtCoder.c254
{
    internal static class C
    {
        internal static void Run()
        {
            var nk = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var (n, k) = (nk[0], nk[1]);
            var a = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

            var queues = MakeQueues(k, a);

            while (queues[0].Any())
            {
                for (var i = 0; i < queues.Length - 1; ++i)
                {
                    if (!queues[i + 1].Any())
                    {
                        continue;
                    }

                    if (queues[i].Peek() > queues[i + 1].Peek())
                    {
                        Console.WriteLine("No");
                        return;
                    }
                }

                for (var i = 0; i < queues.Length; ++i)
                {
                    if (!queues[i].Any())
                    {
                        continue;
                    }

                    queues[i].Dequeue();
                }
            }
            
            Console.WriteLine("Yes");
        }

        static Queue<int>[] MakeQueues(int k, int[] a)
        {
            var intermediate = Enumerable.Range(0, k)
                .Select(_ => new List<int>())
                .ToArray();

            for (var i = 0; i < a.Length; ++i)
            {
                intermediate[i % k].Add(a[i]);
            }

            var queues = new Queue<int>[k];

            for (var i = 0; i < intermediate.Length; ++i)
            {
                queues[i] = new Queue<int>(intermediate[i].OrderBy(x => x));
            }

            return queues;
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
    }
}
