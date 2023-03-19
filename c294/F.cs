#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c294
{
    internal static class F
    {
        internal static void Run()
        {
            var (n, m, k) = ReadInput.ReadArrayInt3();
            var takahashi = ReadInput.ReadArrayArrayInt(n);
            var aoki = ReadInput.ReadArrayArrayInt(m);

            var set = new SortedSet<float>();

            for (var i = 0; i < n; ++i)
            {
                for (var j = 0; j < m; ++j)
                {
                    var sugar = takahashi[i][0] + aoki[j][0];
                    var water = takahashi[i][1] + aoki[j][1];
                    var percent = 100f * sugar / (water + sugar);
                    set.Add(percent);
                }
            }

            // if (k < (n + m) / 2)
            // {
                var value = set.Reverse().Skip(k - 1).Take(1).ToArray();
                Console.WriteLine(value[0]);
            // }
            // else
            // {
            // var value = set.Skip(k - 1).ToArray();
            // Console.WriteLine(value[0]);
            // }
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

            internal static (int, int, int) ReadArrayInt3()
            {
                var array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
                return (array[0], array[1], array[2]);
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
