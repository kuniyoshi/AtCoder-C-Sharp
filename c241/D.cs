#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c241
{
    internal static class D
    {
        internal static void Run()
        {
            var q = int.Parse(Console.ReadLine()!);
            var queries = Enumerable.Range(start: 0, q)
                .Select(_ => Console.ReadLine()!.Split().Select(long.Parse).ToArray());
            var t = new BinaryTrieTree(Pow(x: 10, y: 18));

            foreach (var query in queries)
            {
                var (operation, x) = (query[0], query[1]);

                switch (operation)
                {
                    case 1:
                        t.Insert(x);
                        break;

                    case 2:
                        var k = (int)query[2];
                        Console.WriteLine(t.At(t.CountLte(x) - k + 1) ?? -1);
                        break;

                    case 3:
                        var k2 = (int)query[2];
                        Console.WriteLine(t.At(t.CountLte(x - 1) + k2) ?? -1);
                        break;

                    default:
                        throw new Exception($"un expected operation: {operation}");
                }
            }
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
            List<int> Counts { get; } = new List<int> { 0 };
            List<int?> Ones { get; } = new List<int?> { null };
            int ShiftSize { get; }
            List<int?> Zeros { get; } = new List<int?> { null };

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

            int Size => Counts[index: 0];

            public long? At(int at)
            {
                if (at <= 0 || at > Size)
                {
                    return null;
                }

                var remain = at;
                var index = 0;
                var result = 0L;

                for (var mask = 1L << ShiftSize; mask > 0; mask >>= 1)
                {
                    var hasZero = HasNext(index, isZero: true);
                    var hasOne = HasNext(index, isZero: false);

                    if (!hasZero && !hasOne)
                    {
                        throw new Exception("invalid node");
                    }

                    if (!hasOne)
                    {
                        index = GetNext(index, isZero: true);
                        continue;
                    }

                    if (!hasZero)
                    {
                        result = result | mask;
                        index = GetNext(index, isZero: false);
                        continue;
                    }

                    var less = Counts[GetNext(index, isZero: true)];
                    var more = Counts[GetNext(index, isZero: false)];

                    if (remain <= less)
                    {
                        index = GetNext(index, isZero: true);
                        continue;
                    }

                    result = result | mask;
                    remain = remain - less;
                    index = GetNext(index, isZero: false);
                }

                return result;
            }

            public int CountLte(long value)
            {
                var count = Counts[index: 0];
                var index = 0;

                for (var mask = 1L << ShiftSize; mask > 0; mask >>= 1)
                {
                    var isZero = (value & mask) == 0;

                    switch (isZero, HasNext(index, isZero))
                    {
                        case (true, true):
                            count = count
                                    - (HasNext(index, isZero: false)
                                        ? Counts[GetNext(index, isZero: false)]
                                        : 0);
                            index = GetNext(index, isZero);
                            break;

                        case (true, false):
                            count = count
                                    - (HasNext(index, isZero: false)
                                        ? Counts[GetNext(index, isZero: false)]
                                        : 0);
                            return count;

                        case (false, true):
                            index = GetNext(index, isZero);
                            break;

                        case (false, false):
                            return count;
                    }
                }

                return count;
            }

            public void Insert(long value)
            {
                var index = 0;

                Counts[index: 0]++;

                for (var mask = 1L << ShiftSize; mask > 0; mask >>= 1)
                {
                    index = GetNext(index, (value & mask) == 0);
                    Counts[index]++;
                }
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

            bool HasNext(int index, bool isZero)
            {
                var list = isZero ? Zeros : Ones;

                return list[index].HasValue;
            }

            enum Align
            {
                Zero,
                One,
            }
        }
    }
}
