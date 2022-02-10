#nullable enable
using System;
using System.Linq;

namespace AtCoder.c238
{
    internal static class D
    {
        static bool[,,,,] Can { get; }

        internal static void Run()
        {
            var t = int.Parse(Console.ReadLine()!);
            var q = Enumerable.Range(start: 0, t)
                .Select(_ => Console.ReadLine()!.Split().Select(long.Parse).ToArray());

            foreach (var query in q)
            {
                var has = Test(query[0], query[1], increment: 0);
                Console.WriteLine(has ? "Yes" : "No");
            }
        }

        static bool Test(long and, long sum, int increment)
        {
            if (and == 0 && sum == 0)
            {
                return Can[and, sum, increment, 0, 0];
            }

            var a = and & 1;
            var s = sum & 1;
            var can = false;

            for (var x = 0; x < 2; ++x)
            {
                for (var y = 0; y < 2; ++y)
                {
                    if (!Can[a, s, increment, x, y])
                    {
                        continue;
                    }

                    if (Test(and >> 1, sum >> 1, (increment + x + y) >> 1))
                    {
                        can = true;
                    }
                }
            }

            return can;
        }

        static D()
        {
            Can = new bool[2, 2, 2, 2, 2];
            Can[0, 0, 0, 0, 0] = true;
            Can[0, 0, 1, 0, 1] = true;
            Can[0, 0, 1, 1, 0] = true;
            Can[0, 1, 0, 0, 1] = true;
            Can[0, 1, 0, 1, 0] = true;
            Can[0, 1, 1, 0, 0] = true;
            Can[1, 0, 0, 1, 1] = true;
            Can[1, 1, 1, 1, 1] = true;
        }
    }
}
