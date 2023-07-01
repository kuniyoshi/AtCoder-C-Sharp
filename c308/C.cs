#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c308
{
    internal static class C
    {
        internal static void Run()
        {
            var n = ReadInput.ReadSingle();
            var ab = ReadInput.ReadArrayArrayLong(n);

            var tuples = Enumerable.Range(0, n)
                .Select(index => Tuple.Create(index, ab[index])).ToArray();

            Array.Sort(
                tuples,
                (a, b) => (long)b.Item2[0] * (long)(a.Item2[0] + a.Item2[1])
                          >= (long)a.Item2[0] * (long)(b.Item2[0] + b.Item2[1])
                    ? 1
                    : -1
            );

            Console.WriteLine(string.Join(" ", tuples.Select(s => (s.Item1 + 1).ToString())));
        }

        static class ReadInput
        {
            internal static int ReadSingle()
            {
                return int.Parse(Console.ReadLine()!);
            }

            static long[] ReadArrayLong()
            {
                return Console.ReadLine()!.Split().Select(long.Parse).ToArray();
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
