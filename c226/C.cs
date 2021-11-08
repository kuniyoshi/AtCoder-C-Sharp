#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c226
{
    internal static class C
    {
        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine()!);
            var arts = Enumerable.Range(0, n)
                .Select(_ =>
                    {
                        var array = Console.ReadLine()!.Split().Select(long.Parse).ToArray();
                        var time = array[0];
                        var count = array[1];

                        return new Arts(
                            time,
                            (int)count,
                            array.Skip(2).Select(dependency => (int)dependency - 1).ToArray()
                        );
                    }
                )
                .ToArray();

            var dp = new long[n];

            for (var i = 0; i < dp.Length; ++i)
            {
                dp[i] = long.MaxValue;
            }

            for (var i = 0; i < n; ++i)
            {
                var dependencyTime = arts[i].Dependencies
                    .Sum(cursor => arts[cursor].Time);
                var time = dependencyTime + arts[i].Time;

                dp[i] = time < dp[i] ? time : dp[i];
            }

            Console.WriteLine(dp[n - 1]);
        }

        readonly struct Arts
        {
            public Arts(long time, int count, int[] dependencies)
            {
                Time = time;
                Count = count;
                Dependencies = dependencies;
            }

            internal long Time { get; }
            internal int Count { get; }
            internal int[] Dependencies { get; }
        }
    }
}
