#nullable enable
using System;
using System.Linq;

namespace AtCoder.c314
{
    internal static class E
    {
        internal static void Run()
        {
            var (n, m) = ReadInput.ReadArrayInt2();
            var parameters = ReadInput.ReadArrayArrayInt(n);

            var dp = new double[10000 * 100 + 1];

            for (var i = 0; i < dp.Length; ++i)
            {
                dp[i] = double.NaN;
            }

            dp[0] = 0D;

            var cases = new int[10000 * 100 + 1];

            foreach (var parameter in parameters)
            {
                var cost = parameter[0];
                var count = parameter[1];
                var e = (double)(parameter.Skip(2).Sum()) / count;

                for (var i = dp.Length - 1; i >= 0; --i)
                {
                    if (i - cost < 0)
                    {
                        continue;
                    }

                    if (double.IsNaN(dp[i - cost]))
                    {
                        continue;
                    }

                    dp[i] = Math.Max(dp[i], dp[i-cost] + e);
                }
            }

            var result = Enumerable.Range(0, dp.Length).Where(point => point >= m).Min();
        }

        static class ReadInput
        {
            static int[] ReadArrayInt()
            {
                return Console.ReadLine()!.Split().Select(int.Parse).ToArray();
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
