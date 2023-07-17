#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.TessokuBook
{
    internal static class A10
    {
        internal static void Run()
        {
            var n = ReadInput.ReadSingle();
            var a = ReadInput.ReadArrayInt();
            var d = ReadInput.ReadSingle();
            var ranges = ReadInput.ReadArrayArrayInt(d);

            var accs = Enumerable.Range(0, 100).Select(_ => new int[n + 1]).ToArray();

            for (var size = 0; size < accs.Length; ++size)
            {
                for (var i = 1; i <= a.Length; ++i)
                {
                    accs[size][i] = accs[size][i - 1] + Convert.ToInt32(a[i - 1] == (100 - size));
                }
            }

            foreach (var range in ranges)
            {
                var (l, r) = (range[0], range[1]);

                for (var rSize = 0; rSize < 100; ++rSize)
                {
                    if ( accs[rSize][l - 1] > 0|| accs[rSize][r] < accs[rSize][n])
                    {
                        Console.WriteLine(100 - rSize);
                        break;
                    }
                }
            }
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
