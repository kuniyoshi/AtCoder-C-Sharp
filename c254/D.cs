#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace AtCoder.c254
{
    internal static class D
    {
        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine()!);
            var partList = Enumerable.Range(0, n).Select(_ => new List<int>()).ToArray();

            for (var i = 1; i <= n; ++i)
            {
                for (var j = i; j <= n; j += i)
                {
                    partList[i - 1].Add(j);
                }
            }

            var isDouble = new HashSet<long>();

            for (var i = 0; i < n; ++i)
            {
                isDouble.Add((long)(i + 1) * (i + 1));
            }

            var wholeFactors = new List<int>[n];
            wholeFactors[0] = new List<int>{1};
            wholeFactors[1] = new List<int>{2};
            wholeFactors[2] = new List<int>{3};
            wholeFactors[3] = new List<int>{2, 2};

            var count = 0L;

            for (var i = 0; i < partList.Length; ++i)
            {
                var factors = GetFactors(i, wholeFactors, partList[i]);
                var values = factors.Aggregate(1L, (x, y) => x * y);
                if (isDouble.Contains(values))
                    count += (factors.Count + 1) * (factors.Count+1);
            }

            Console.WriteLine(count);
        }

        static List<int> GetFactors(int x, List<int>[] wholeFactors, List<int> pairs)
        {
            if (wholeFactors[x] != null)
            {
                return wholeFactors[x];
            }

            var factors = new List<int>();

            for (var i = pairs.Count - 1; i >= 0; --i)
            {
                if (wholeFactors[i] != null)
                {
                    factors.AddRange(wholeFactors[i]);
                    break;
                }
                
                factors.Add(pairs[i]);
            }

            wholeFactors[x] = factors;

            return factors;
        }
    }
}
