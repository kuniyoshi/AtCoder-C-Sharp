#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AtCoder.c306
{
    internal static class E
    {
        internal static void Run()
        {
            var (n, k, q) = ReadInput.ReadArrayInt3();
            var queries = ReadInput.ReadArrayArrayInt(q);

            var candidateQueue = new HigherPriorQueue();
            var kQueue = new LowerPriorQueue();
            var a = Enumerable.Repeat(0, n).ToArray();
            var isIn = new HashSet<int>();
            var total = 0L;
            var generations = Enumerable.Repeat(0, n).ToArray();

            foreach (var i in Enumerable.Range(0, k))
            {
                kQueue.Push(Tuple.Create(a[i], i, generations[i]));
                isIn.Add(i);
            }

            foreach (var i in Enumerable.Range(k, n - k))
            {
                candidateQueue.Push(Tuple.Create(a[i], i, generations[i]));
            }

            foreach (var query in queries)
            {
                var (x, y) = (query[0], query[1]);
                x--;

                if (isIn.Contains(x))
                {
                    total -= a[x];
                    isIn.Remove(x);
                }

                while (kQueue.Any()
                       && (!isIn.Contains(kQueue.Peek().Item2)
                           || kQueue.Peek().Item3 < generations[kQueue.Peek().Item2]))
                {
                    if (kQueue.Peek().Item3 == generations[kQueue.Peek().Item2])
                    {
                        isIn.Remove(kQueue.Peek().Item2);
                    }
                    kQueue.Pop();
                }

                while (candidateQueue.Any()
                       && (isIn.Contains(candidateQueue.Peek().Item2)
                           || candidateQueue.Peek().Item3 < generations[candidateQueue.Peek().Item2]))
                {
                    candidateQueue.Pop();
                }

                generations[x]++;
                a[x] = y;

                if (!kQueue.Any())
                {
                    isIn.Add(x);
                    total += a[x];
                    kQueue.Push(Tuple.Create(y, x, generations[x]));
                }
                else if (y > kQueue.Peek().Item1)
                {
                    isIn.Add(x);
                    total += a[x];
                    kQueue.Push(Tuple.Create(y, x, generations[x]));
                }
                // else if (isIn.Count != k)
                // {
                //     isIn.Add(x);
                //     total += a[x];
                //     kQueue.Push(Tuple.Create(y, x, generations[x]));
                // }

                Console.WriteLine(total);
            }
        }

        class LowerPriorQueue
        {
            List<Tuple<int, int, int>> Items { get; } = new List<Tuple<int, int, int>>();

            internal bool Any()
            {
                return Items.Count > 0;
            }

            internal Tuple<int, int, int> Peek()
            {
                return Items[index: 0];
            }

            internal void Pop()
            {
                Debug.Assert(Items.Any(), "Items.Any()");
                Heap.ReversePopFrom(Items);
            }

            internal void Push(Tuple<int, int, int> value)
            {
                Heap.ReversePushTo(Items, value);
            }
        }

        class HigherPriorQueue
        {
            List<Tuple<int, int, int>> Items { get; } = new List<Tuple<int, int, int>>();

            internal bool Any()
            {
                return Items.Count > 0;
            }

            internal Tuple<int, int, int> Peek()
            {
                return Items[index: 0];
            }

            internal void Pop()
            {
                Debug.Assert(Items.Any(), "Items.Any()");
                Heap.PopFrom(Items);
            }

            internal void Push(Tuple<int, int, int> value)
            {
                Heap.PushTo(Items, value);
            }
        }

        static class Heap
        {
            internal static void PopFrom(List<Tuple<int, int, int>> buffer)
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

                    var child = right < buffer.Count && buffer[left].Item1 <= buffer[right].Item1
                        ? right
                        : left;

                    if (buffer[cursor].Item1 < buffer[child].Item1)
                    {
                        (buffer[cursor], buffer[child]) = (buffer[child], buffer[cursor]);
                    }

                    cursor = child;
                }
            }

            internal static void PushTo(List<Tuple<int, int, int>> buffer, Tuple<int, int, int> item)
            {
                buffer.Add(item);
                var cursor = buffer.Count - 1;

                while (cursor != 0)
                {
                    var parent = (cursor - 1) / 2;

                    if (buffer[parent].Item1 < buffer[cursor].Item1)
                    {
                        (buffer[parent], buffer[cursor]) = (buffer[cursor], buffer[parent]);
                    }

                    cursor = parent;
                }
            }

            internal static void ReversePopFrom(List<Tuple<int, int, int>> buffer)
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

                    if (buffer[cursor].Item1 > buffer[child].Item1)
                    {
                        (buffer[cursor], buffer[child]) = (buffer[child], buffer[cursor]);
                    }

                    cursor = child;
                }
            }

            internal static void ReversePushTo(List<Tuple<int, int, int>> buffer, Tuple<int, int, int> item)
            {
                buffer.Add(item);
                var cursor = buffer.Count - 1;

                while (cursor != 0)
                {
                    var parent = (cursor - 1) / 2;

                    if (buffer[parent].Item1 > buffer[cursor].Item1)
                    {
                        (buffer[parent], buffer[cursor]) = (buffer[cursor], buffer[parent]);
                    }

                    cursor = parent;
                }
            }
        }

        static class ReadInput
        {
            static int[] ReadArrayInt()
            {
                return Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            }

            internal static (int, int, int) ReadArrayInt3()
            {
                var array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
                return (array[0], array[1], array[2]);
            }

            internal static int[][] ReadArrayArrayInt(int n)
            {
                return Enumerable.Range(0, n)
                    .Select(_ => ReadArrayInt())
                    .ToArray();
            }
        }
    }
}
