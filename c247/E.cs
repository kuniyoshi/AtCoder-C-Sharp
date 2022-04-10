using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace AtCoder.c247
{
    internal static class E
    {
        internal static void Run()
        {
            var topLine = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var (n, x, y) = (topLine[0], topLine[1], topLine[2]);
            var a = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

            var minTree = new MinTree(n);
            var maxTree = new MaxTree(n);

            for (var i = 0; i < a.Length; ++i)
            {
                minTree.Set(i, a[i]);
                maxTree.Set(i, a[i]);
            }

            var minIndexes = minTree.Count(y);
            var maxIndexes = minTree.Count(x);

            Console.WriteLine(minIndexes.Count * maxIndexes.Count);
            // var count = 0;
            //
            // for (var i = 0; i < minIndexes.Count; ++i)
            // {
            //     count += maxIndexes.Count(j => j >= i);
            // }

        }

        abstract class SegTree
        {
            int Size { get; }

            int?[] Items { get; }

            protected SegTree(int size)
            {
                Size = size;
                Items = new int?[2 * size - 1];
            }
            public List<int> Count(int value)
            {
                var items = new List<int>();

                for (var i = Size - 1; i < Items.Length; ++i)
                {
                    if (!Items[i].HasValue)
                    {
                        break;
                    }

                    if (Items[i] == value)
                    {
                        items.Add(i - (Size - 1));
                    }
                }

                return items;
            }

            protected abstract int? Select(int? a, int? b);
            public void Set(int at, int value)
            {
                var index = at + Size - 1;
                Items[index] = value;

                while (index > 0)
                {
                    index = (index - 1) / 2;
                    Items[index] = Select(Items[index * 2 + 1], Items[index * 2 + 2]);
                }
            }
        }

        class MaxTree : SegTree
        {
            public MaxTree(int size)
                : base(size)
            {
            }

            protected override int? Select(int? a, int? b)
            {
                return (a.HasValue, b.HasValue) switch
                {
                    (true, true) => Math.Max(a.Value, b.Value),
                    (true, false) => a.Value,
                    (false, true) => b.Value,
                    (false, false) => null,
                };
            }
        }
        class MinTree : SegTree
        {
            public MinTree(int size)
                : base(size)
            {
            }
            protected override int? Select(int? a, int? b)
            {
                return (a.HasValue, b.HasValue) switch
                {
                    (true, true) => Math.Min(a.Value, b.Value),
                    (true, false) => a.Value,
                    (false, true) => b.Value,
                    (false, false) => null,
                };
            }
        }
    }
}
