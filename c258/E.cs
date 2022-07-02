#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c258
{
    internal static class E
    {
        internal static void Run()
        {
            var nqx = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var (n, q, x) = (nqx[0], nqx[1], nqx[2]);
            var temporaryWeights = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var weights = temporaryWeights.Concat(temporaryWeights).ToArray();
            var k = Enumerable.Range(0, q).Select(_ => long.Parse(Console.ReadLine()!)).ToArray();

            var acc = new List<long> { 0 };

            for (var index = 0; index < n; index++)
            {
                var weight = weights[index];
                acc.Add(acc.Last() + weight);
            }

            var counts = k.ToDictionary(x => x, _ => (long?)null);

            var cursor = 0;
            var max = k.Max();

            for (var i = 0; i < max; ++i)
            {
                var loops = x / acc.Last();
                var remain = x - loops * acc.Last();
                var count = 0;
                
                if (acc.Last() - acc[cursor] <= remain)
                {
                    remain -= acc.Last() - acc[cursor];
                    count += n - cursor;
                    cursor = 0;
                }

                var ac = cursor;
                var wa = n;

                while (wa - ac > 1)
                {
                    var wj = (ac + wa) / 2;

                    if (acc[wj] - acc[cursor] > remain)
                    {
                        wa = wj;
                    }
                    else
                    {
                        ac = wj;
                    }
                }

                ac++;

                count += ac - cursor;

                if (counts.ContainsKey(i + 1))
                {
                    counts[i + 1] = count;
                }

                cursor = (cursor + ac) % n;
            }

            foreach (var number in k)
            {
                Console.WriteLine(counts[number]);
            }
        }
    }
}
