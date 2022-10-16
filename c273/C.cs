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

            var larges = GetLarges(a.OrderBy(v => v).Distinct().ToArray());

            var results = new Dictionary<int, int>();

            foreach (var value in larges.Values)
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

        static Dictionary<int, int> GetLarges(int[] orderedUniqueNumbers)
        {
            var result = new Dictionary<int, int>();
            
            for (var i = 0; i < orderedUniqueNumbers.Length; ++i)
            {
                result[orderedUniqueNumbers[i]] = orderedUniqueNumbers.Length - 1 - i;
            }

            return result;
        }
    }
}
