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

            var next = priorityQueue.Peek();
            priorityQueue.Pop();
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

            public void Pop()
            {
                Debug.Assert(Items.Any(), "Items.Any()");
                Heap.ReversePopFrom(Items);
            }

            static class Heap
            {
                public static void ReversePushTo(List<int> buffer, int item)
                {
                    var n = buffer.Count;
                    buffer.Add(item);

                    while (n != 0)
                    {
                        var i = (n - 1) / 2;
                        if (buffer[n] < buffer[i])
                        {
                            (buffer[n], buffer[i]) = (buffer[i], buffer[n]);
                            // T tmp = array[n]; array[n] = array[i]; array[i] = tmp;
                        }
                        n = i;
                    }

                    // var cursor = buffer[buffer.Count - 1];
                    //
                    // while (cursor != 0)
                    // {
                    //     var parent = (cursor - 1) / 2;
                    //
                    //     if (buffer[parent] >= buffer[cursor])
                    //     {
                    //         (buffer[parent], buffer[cursor]) = (buffer[cursor], buffer[parent]);
                    //     }
                    //
                    //     cursor = parent;
                    // }
                }

                public static void ReversePopFrom(List<int> buffer)
                {
                    var n = buffer.Count - 1;
                    buffer[0] = buffer[n];
                    buffer.RemoveAt(buffer.Count - 1);

                    for (int i = 0, j; (j = 2 * i + 1) < n; )
                    {
                        if ((j != n - 1) && (buffer[j].CompareTo(buffer[j + 1])) >= 0)
                        {
                            j++;
                        }

                        if (buffer[i].CompareTo(buffer[j]) >= 0)
                        {
                            (buffer[j], buffer[i]) = (buffer[i], buffer[j]);
                        }

                        i = j;
                    }
                    // Debug.Assert(buffer.Any(), "buffer.Any()");
                    // var lastRoot = buffer[0];
                    // buffer[0] = buffer[buffer.Count - 1];
                    // buffer.RemoveAt(buffer.Count - 1);
                    //
                    // var cursor = 0;
                    // int left;
                    //
                    // while ((left = 2 * cursor + 1) < buffer.Count)
                    // {
                    //     var right = left + 1;
                    //
                    //     var child = right < buffer.Count && buffer[left] >= buffer[right]
                    //         ? right
                    //         : left;
                    //
                    //     if (buffer[cursor] >= buffer[child])
                    //     {
                    //         (buffer[cursor], buffer[child]) = (buffer[child], buffer[cursor]);
                    //     }
                    //
                    //     cursor = child;
                    // }
                    //
                    // return lastRoot;
                }
            }

            public bool Any()
            {
                return Items.Any();
            }
        }
    }

}
