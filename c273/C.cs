#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;

namespace AtCoder.c273
{
    internal static class C
    {
        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine()!);
            var a = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

            var larges = GetLarges(a, a.OrderBy(v => v).Distinct().ToArray());
            var counts = GetCounts(a, larges);

            var results = new Dictionary<int, int>();

            foreach (var value in counts.Values)
            {
                if (!results.ContainsKey(value))
                {
                    results.Add(value, 0);
                }

                results[value]++;
            }

            for (var i = 0; i < n; ++i)
            {
                Console.WriteLine(results.ContainsKey(a[i]) ? results[a[i]] : 0);
            }
        }

        static Dictionary<int, int> GetCounts(int[] values, int[] larges)
        {
            var counts = new Dictionary<int, int>();

            for (var i = 0; i < values.Length; ++i)
            {
                counts[values[i]] = larges[i];
            }

            return counts;
        }

        static int[] GetLarges(int[] a, int[] orderedUniqueNumbers)
        {
            var larges = new int[a.Length];

            for (var i = 0; i < larges.Length; ++i)
            {
                if (a[i] == orderedUniqueNumbers.Last())
                {
                    larges[i] = 0;
                }
                
                var ac = 0;
                var wa = larges.Length - 1;

                while (wa - ac > 1)
                {
                    var wj = (ac + wa) / 2;

                    if (orderedUniqueNumbers[wj] > a[ac])
                    {
                        wa = wj;
                    }
                    else
                    {
                        ac = wj;
                    }
                }

                larges[i] = orderedUniqueNumbers.Length - 1 - ac;
            }

            return larges;
        }
    }
}
