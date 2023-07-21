#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.TessokuBook
{
    internal static class B14
    {
        internal static void Run()
        {
            var (n, k) = ReadInput.ReadArrayInt2();
            var a = ReadInput.ReadArrayInt();

            var m = n / 2;

            var u = new HashSet<int>();
            var v = new HashSet<int>();

            for (var i = 0; i < (1 << m); ++i)
            {
                var sum = 0;
                var bits = i;
                var j = 0;

                while (bits > 0)
                {
                    if ((bits & 1) > 0)
                    {
                        sum += a[j];
                    }

                    bits >>= 1;
                    j++;
                }

                u.Add(sum);
            }

            for (var i = (1 << m); i < (1 << n); ++i)
            {
                var sum = 0;
                var bits = i;
                var j = 0;

                while (bits > 0)
                {
                    if ((bits & 1) > 0)
                    {
                        sum += a[j];
                    }

                    bits >>= 1;
                    j++;
                }

                v.Add(sum);
            }

            foreach (var key in u)
            {
                if (v.Contains(key))
                {
                    YesNo.Write(true);
                    return;
                }
            }

            YesNo.Write(false);
        }

        static class YesNo
        {
            internal static void Write(bool isYes)
            {
                Console.WriteLine(isYes ? "Yes" : "No");
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
