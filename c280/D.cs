#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c280
{
    internal static class D
    {
        internal static void Run()
        {
            var k = ReadSingle();
            var factors = Factors(k);
            var candidates = Candidates();

            var answer = Solve(factors, candidates);

            Console.WriteLine(answer);
        }

        static int Solve(List<int> factors, List<List<int>> candidates)
        {
            for (var i = 0; i < candidates.Count; ++i)
            {
                var candidate = candidates[i];

                if (IsSatisfy(factors, candidate))
                {
                    return i + 1;
                }
            }

            throw new Exception("Could not found answer");
        }

        static bool IsSatisfy(List<int> factors, List<int> candidate)
        {
            for (var i = 0; i < factors.Count; ++i)
            {
                var index = candidate.FindIndex(x => x == factors[i]);

                if (!(index >= 0))
                {
                    return false;
                }

                candidate[index] = 0;
            }

            return false;
        }

        static List<List<int>> Candidates()
        {
            var result = new List<List<int>>();
            
            for (var i = 2; i <= 15; ++i)
            {
                var factors = Factors(i);
                var last = result.LastOrDefault() ?? new List<int>();
                result.Add(last.Concat(factors).ToList());
            }

            return result;
        }

        static List<int> Factors(int k)
        {
            var max = (int) Math.Sqrt(k);
            var factors = new List<int>();
            var used = new bool[max + 1];

            var acc = k;

            for (var i = 2; i <= max; ++i)
            {
                if (used[i])
                {
                    continue;
                }

                used[i] = true;

                while ((acc % i) == 0)
                {
                    acc /= i;
                    factors.Add(i);
                }

                for (var j = i + i; j <= max; j += i)
                {
                    used[j] = true;
                }
            }

            return factors;
        }

        static int ReadSingle()
        {
            return int.Parse(Console.ReadLine()!);
        }
    }
}
