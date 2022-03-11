#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AtCoder.c241
{
    internal static class D
    {
        internal static void Run()
        {
            var t = new BinaryTrieTree(Pow(10, 18));
        }

        static long Pow(long x, long y)
        {
            var result = 1L;

            while (y-- > 0)
            {
                result = result * x;
            }

            return result;
        }

        class BinaryTrieTree
        {
            List<int?> Ones { get; } = new List<int?> { null };
            List<int?> Zeros { get; } = new List<int?> { null };
            List<int> Counts { get; } = new List<int> { 0 };
            int ShiftSize { get; }

            public BinaryTrieTree(long maxValue)
            {
                var value = maxValue;
                var width = 0;

                while (value != 0)
                {
                    width++;
                    value >>= 1;
                }

                ShiftSize = width - 1;
            }

            public int CountLte(long value)
            {
                var count = Counts[0];
                var index = 0;

                for (var mask = 1L << ShiftSize; mask > 0; mask <<= 1)
                {
                    var isZero = (value & mask) == 0;

                    switch (HasNext(index, isZero))
                    {
                        case true:
                            break;

                        case false:
                            break;
                    }
                        index = GetNext(index, isZero);
                }

            }

            public void Insert(long value)
            {
                var index = 0;

                Counts[0]++;

                for (var mask = 1L << ShiftSize; mask > 0; mask <<= 1)
                {
                    index = GetNext(index, isZero: (value & mask) == 0);
                    Counts[index]++;
                }
            }

            int ListSize => Counts.Count;

            bool HasNext(int index, bool isZero)
            {
                var list = isZero ? Zeros : Ones;

                return list[index].HasValue;
            }

            int GetNext(int index, bool isZero)
            {
                var list = isZero ? Zeros : Ones;

                if (!list[index].HasValue)
                {
                    Zeros.Add(item: null);
                    Ones.Add(item: null);
                    Counts.Add(item: 0);
                    list[index] = list.Count - 1;
                }

                if (!list[index].HasValue)
                {
                    throw new Exception("list[index].HasValue == false");
                }

                return list[index]!.Value;
            }
        }
    }
}
