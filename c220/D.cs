#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c220
{
    internal static class D
    {
        internal static void Run()
        {
            var _ = int.Parse(Console.ReadLine()!);
            var a = new Queue<int>(Console.ReadLine()!.Split().Select(int.Parse));

            var patterns = new int[10];
            patterns[a.Dequeue()]++;
            var nexts = new int[10];

            while (a.Any())
            {
                var y = a.Dequeue();

                for (var i = 0; i < nexts.Length; ++i)
                {
                    nexts[i] = 0;
                }

                for (var i = 0; i < patterns.Length; ++i)
                {
                    if (patterns[i] == 0)
                    {
                        continue;
                    }

                    var x = i;
                    var f = (x + y) % 10;
                    var g = (x * y) % 10;
                    nexts[f] = nexts[f] + patterns[i];
                    nexts[g] = nexts[g] + patterns[i];
                }

                for (var i = 0; i < 10; ++i)
                {
                    patterns[i] = nexts[i];
                }
            }

            foreach (var pattern in patterns)
            {
                Console.WriteLine(pattern % 998244353);
            }
        }
    }
}
