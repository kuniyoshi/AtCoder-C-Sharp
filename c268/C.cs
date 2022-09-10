#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AtCoder.c268
{
    internal static class C
    {
        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine()!);
            var values = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

            var dish = new Dictionary<int, int>();

            for (var i = 0; i < values.Length; ++i)
            {
                dish[values[i]] = i;
            }
            
            var max = Math.Max(Left(values, dish), Right(values, dish));
            Console.WriteLine(max);
        }

        static int Left(int[] values, Dictionary<int, int> dishes)
        {
            var ranges = GetLeftRanges(values, dishes);
            return SelectMax(ranges);
        }
        static int Right(int[] values, Dictionary<int, int> dishes)
        {
            var ranges = GetRightRanges(values, dishes);
            return SelectMax(ranges);
        }

        static int SelectMax(List<Tuple<int,int>> ranges)
        {
            var queue = new LowerPriorQueue();
            var max = -1;

            foreach (var range in ranges)
            {
                while (queue.Any() && queue.Peek() < range.Item1)
                {
                    queue.Pop();
                }

                queue.Push(ranges.First().Item2);
                max = Math.Max(max, queue.Size);
            }

            return max;
        }

        static List<Tuple<int, int>> GetLeftRanges(int[] values, Dictionary<int, int> dish)
        {
            var ranges = new List<Tuple<int, int>>();

            for (var i = 0; i < values.Length; ++i)
            {
                ranges.AddRange(GetLeftRange(i, dish[i], values.Length));
            }

            return ranges.OrderBy(i => i.Item1)
                .ToList();
        }
        static List<Tuple<int, int>> GetRightRanges(int[] values, Dictionary<int, int> dish)
        {
            var ranges = new List<Tuple<int, int>>();

            for (var i = 0; i < values.Length; ++i)
            {
                ranges.AddRange(GetRightRange(i, dish[i], values.Length));
            }

            return ranges.OrderBy(i => i.Item1)
                .ToList();
        }

        static IEnumerable<Tuple<int, int>> GetLeftRange(int person, int dishPosition, int dishSize)
        {
            var distance = person > dishPosition ? dishPosition - person : person + dishSize - dishPosition;

            switch (distance)
            {
                case -1:
                    return new[] { Tuple.Create(0, 2) };

                case 0:
                    return new[]
                    {
                        Tuple.Create(0, 1),
                        Tuple.Create(dishSize - 1, dishSize + 1),
                    };

                case 1:
                    return new[]
                    {
                        Tuple.Create(0, 0),
                        Tuple.Create(dishSize - 2, dishSize),
                    };

                default:
                    var d = distance < 0 ? -distance : distance - 1;
                    // var d = distance < 0 ? -distance : (dishPosition + dishSize - distance - 1) % dishSize;
                    return new[] { Tuple.Create(d - 1, d + 1) };
            }
        }
        static IEnumerable<Tuple<int, int>> GetRightRange(int person, int dishPosition, int dishSize)
        {
            var distance = dishPosition < person ? dishPosition + dishSize - person : dishPosition - person;

            switch (distance)
            {
                case -1:
                    return new[]
                    {
                        Tuple.Create(0, 0),
                        Tuple.Create(dishSize - 2, dishSize),
                    };

                case 0:
                    return new[]
                    {
                        Tuple.Create(0, 1),
                        Tuple.Create(dishSize - 1, dishSize + 1),
                    };

                case 1:
                    return new[] { Tuple.Create(0, 2) };

                default:
                    var d = distance > 0 ? distance : (dishPosition + dishSize - distance - 1) % dishSize;
                    return new[] { Tuple.Create(d - 1, d + 1) };
            }
        }

        class LowerPriorQueue
        {
            List<int> Items { get; } = new List<int>();

            internal int Size => Items.Count;

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

        static class Heap
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
