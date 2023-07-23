#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AtCoder.c260
{
    internal static class D
    {
        internal static void Run()
        {
            var nk = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var (n, k) = (nk[0], nk[1]);
            var p = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var answers = new int[n];

            for (var i = 0; i < n; ++i)
            {
                answers[i] = -1;
            }

            var lowerPriorQueue = new LowerPriorQueue();
            var higherPriorQueue = new HigherPriorQueue();

            for (var index = 0; index < p.Length; index++)
            {
                var value = p[index];

                while (lowerPriorQueue.Any() && value > lowerPriorQueue.Peek().Top)
                {
                    higherPriorQueue.Push(lowerPriorQueue.Pop());
                }

                while (higherPriorQueue.Any() && value < higherPriorQueue.Peek().Top)
                {
                    lowerPriorQueue.Push(higherPriorQueue.Pop());
                }

                if (lowerPriorQueue.Any() )
                {
                    if (value >= lowerPriorQueue.Peek().Top)
                    {
                        throw new Exception("somehow");
                    }
                    
                    var item = lowerPriorQueue.Pop();
                    item.Push(value);
                    lowerPriorQueue.Push(item);
                }
                else
                {
                    lowerPriorQueue.Push(new Mountain(value));
                }

                if (lowerPriorQueue.Peek().Size == k)
                {
                    var item = lowerPriorQueue.Pop();

                    foreach (var x in item.Items)
                    {
                        answers[x - 1] = index + 1;
                    }
                }
            }

            foreach (var answer in answers)
            {
                Console.WriteLine(answer);
            }
        }

         class Mountain
        {
            internal int Top => Items.Peek();
            internal Stack<int> Items { get; }

            internal int Size => Items.Count;

            internal Mountain(int a)
            {
                Items = new Stack<int>();
                Items.Push(a);
            }

            internal void Push(int value)
            {
                Items.Push(value);
            }
        }

         class HigherPriorQueue
        {
            List<Mountain> Items { get; } = new List<Mountain>();

            internal bool Any()
            {
                return Items.Any();
            }

            internal Mountain Peek()
            {
                return Items[index: 0];
            }

            internal Mountain Pop()
            {
                Debug.Assert(Items.Any(), "Items.Any()");
                return Heap.PopFrom(Items);
            }

            internal void Push(Mountain value)
            {
                Heap.PushTo(Items, value);
            }
        }

         class LowerPriorQueue
        {
            List<Mountain> Items { get; } = new List<Mountain>();

            internal bool Any()
            {
                return Items.Any();
            }

            internal Mountain Peek()
            {
                return Items[index: 0];
            }

            internal Mountain Pop()
            {
                Debug.Assert(Items.Any(), "Items.Any()");
                return Heap.ReversePopFrom(Items);
            }

            internal void Push(Mountain value)
            {
                Heap.ReversePushTo(Items, value);
            }
        }

         static class Heap
        {
            internal static Mountain PopFrom(List<Mountain> buffer)
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

                    var child = right < buffer.Count && buffer[left].Top <= buffer[right].Top
                        ? right
                        : left;

                    if (buffer[cursor].Top < buffer[child].Top)
                    {
                        (buffer[cursor], buffer[child]) = (buffer[child], buffer[cursor]);
                    }

                    cursor = child;
                }

                return lastRoot;
            }

            internal static void PushTo(List<Mountain> buffer, Mountain item)
            {
                buffer.Add(item);
                var cursor = buffer.Count - 1;

                while (cursor != 0)
                {
                    var parent = (cursor - 1) / 2;

                    if (buffer[parent].Top < buffer[cursor].Top)
                    {
                        (buffer[parent], buffer[cursor]) = (buffer[cursor], buffer[parent]);
                    }

                    cursor = parent;
                }
            }

            internal static Mountain ReversePopFrom(List<Mountain> buffer)
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

                    var child = right < buffer.Count && buffer[left].Top >= buffer[right].Top
                        ? right
                        : left;

                    if (buffer[cursor].Top > buffer[child].Top)
                    {
                        (buffer[cursor], buffer[child]) = (buffer[child], buffer[cursor]);
                    }

                    cursor = child;
                }

                return lastRoot;
            }

            internal static void ReversePushTo(List<Mountain> buffer, Mountain item)
            {
                buffer.Add(item);
                var cursor = buffer.Count - 1;

                while (cursor != 0)
                {
                    var parent = (cursor - 1) / 2;

                    if (buffer[parent].Top > buffer[cursor].Top)
                    {
                        (buffer[parent], buffer[cursor]) = (buffer[cursor], buffer[parent]);
                    }

                    cursor = parent;
                }
            }
        }
    }
}
