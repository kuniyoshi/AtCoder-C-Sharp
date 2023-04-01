#nullable enable
using System;
using System.Linq;

namespace AtCoder.c296
{
    internal static class D
    {
        internal static void Run()
        {
            var (n, m) = ReadInput.ReadArrayLong2();

            var min = (long?)null;

            var sqrt = Math.Min((long)Math.Sqrt(m) + 1, n);

            for (var a = 1L; a <= sqrt; ++a)
            {
                if (a * n < m)
                {
                    continue;
                }

                if (a >= m)
                {
                    if (min == null || a < min)
                    {
                        min = a;
                    }
                    continue;
                }

                var ac = n;
                var wa = 1L;

                while (ac - wa > 1)
                {
                    var wj = (ac + wa) / 2;

                    if (wj * a >= m)
                    {
                        ac = wj;
                    }
                    else
                    {
                        wa = wj;
                    }
                }

                if (min == null && a * ac >= m)
                {
                    min = a * ac;
                }

                if (a * ac >= m && a * ac < min)
                {
                    min = a * ac;
                }
            }

            if (min == null)
            {
                Console.WriteLine(-1);
            }
            else
            {
                Console.WriteLine(min);
            }
        }

        static class ReadInput
        {
            internal static (long, long) ReadArrayLong2()
            {
                var array = Console.ReadLine()!.Split().Select(long.Parse).ToArray();
                return (array[0], array[1]);
            }
        }
    }
}
