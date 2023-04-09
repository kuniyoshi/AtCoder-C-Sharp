#nullable enable
using System;
using System.Linq;

namespace AtCoder.c297
{
    internal static class D
    {
        internal static void Run()
        {
            var pair = Console.ReadLine()!.Split().Select(long.Parse).ToArray();
            var count = 0L;

            while (pair[0] != pair[1])
            {
                var large = pair[0] > pair[1] ? 0 : 1;
                var small = large == 1 ? 0 : 1;

                var ac = 1L;
                var wa = (long)Math.Ceiling((double)pair[large] / pair[small]) + 1;

                while (wa - ac > 1)
                {
                    var wj = (ac + wa) / 2;

                    if (pair[large] - wj * pair[small] >= pair[small])
                    {
                        ac = wj;
                    }
                    else
                    {
                        wa = wj;
                    }
                }

                pair[large] = pair[large] - ac * pair[small];
                count += ac;
            }

            Console.WriteLine(count);
        }
    }
}
