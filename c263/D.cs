#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c263
{
    internal static class D
    {
        internal static void Run()
        {
            var nlr = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var (n, l, r) = (nlr[0], nlr[1], nlr[2]);
            var a = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

            var sum = a.Aggregate(0L, (acc, value) => acc + value);

            var dp = new V[n];

            // dp[0].Original = a[0];
            //
            // for (var i = 0; i < n - 1; ++i)
            // {
            //     dp[i + 1].Original = dp[i].Original + a[i + 1];
            // }

            dp[0].Original = sum;
            dp[0].Left = sum + l - a[0];
            dp[0].Right = r * n;
            
            for (var i = 1; i < n; ++i)
            {
                dp[i].Left = dp[i - 1].Left + l - a[i];
                dp[i].Original = Math.Min(dp[i - 1].Left, dp[i - 1].Original);
                dp[i].Right = Math.Min(dp[i - 1].Original + r - a[i], dp[i - 1].Right);
            }

            Console.WriteLine(dp.Min(value => Min3(value.Left, value.Right, value.Original)));
        }

        static long Min3(long a, long b, long c)
        {
            return Math.Min(Math.Min(a, b), c);
        }

        struct V
        {
            public V(long original, long left, long right)
            {
                Original = original;
                Left = left;
                Right = right;
            }

            public long Original;
            public long Left;
            public long Right;
        }
    }
}
