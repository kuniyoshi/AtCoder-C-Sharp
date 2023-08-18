#nullable enable
using System;
using System.Linq;

namespace AtCoder.c312
{
    internal static class C
    {
        internal static void Run()
        {
            var (n, m) = ReadInput.ReadArrayInt2();
            var a = ReadInput.ReadArrayInt();
            var b = ReadInput.ReadArrayInt();

            var wa = 0;
            var ac = 1_000_000_000 + 1;

            while (ac - wa > 1)
            {
                var wj = (ac + wa) / 2;

                if (T(wj, a, b))
                {
                    ac = wj;
                }
                else
                {
                    wa = wj;
                }
            }

            Console.WriteLine(ac);
        }

        static bool T(int price, int[] sellers, int[] buyers)
        {
            var sellersCount = sellers.Count(v => v <= price);
            var buyersCount = buyers.Count(v => v >= price);
            return sellersCount >= buyersCount;
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
