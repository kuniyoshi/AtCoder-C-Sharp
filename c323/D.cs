#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c323
{
    internal static class D
    {
        internal static void Run()
        {
            var n = ReadInput.ReadSingle();
            var slimes = ReadInput.ReadArrayArrayInt(n);

            var slimeCount = new Dictionary<long, long>();

            foreach (var slime in slimes)
            {
                var (size, c) = (slime[0], slime[1]);
                slimeCount[size] = c;
            }

            var sizes = slimeCount.Keys.OrderBy(value => value).ToArray();

            foreach (var size in sizes)
            {
                var accCount = slimeCount[size];
                var accSize = size;

                while (accCount > 1)
                {
                    var nextSizeCount = accCount / 2;
                    slimeCount[accSize] = accCount % 2;

                    accSize *= 2;
                    accCount = nextSizeCount + (slimeCount.ContainsKey(accSize) ? slimeCount[accSize] : 0L);
                    slimeCount[accSize] = accCount;
                }
            }

            Console.WriteLine(slimeCount.Values.Sum());
        }
        static class ReadInput
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
}
