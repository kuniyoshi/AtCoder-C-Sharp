#nullable enable
using System;
using System.Linq;

namespace AtCoder.c238
{
    internal static class D
    {
        internal static void Run()
        {
            var t = int.Parse(Console.ReadLine()!);
            var q = Enumerable.Range(start: 0, t)
                .Select(_ => Console.ReadLine()!.Split().Select(long.Parse).ToArray());

            foreach (var query in q)
            {
                var has = Test(query[0], query[1]);
                Console.WriteLine(has ? "Yes" : "No");
            }
        }

        static bool Test(long and, long sum)
        {
            if (sum == 0)
            {
                return and == 0;
            }

            var a = and & 1;

            for (var x = 0; x < 2; ++x)
            {
                for (var y = 0; y < 2; ++y)
                {
                    if ((x & y) != a)
                    {
                        continue;
                    }

                    if ((sum - x - y) < 0)
                    {
                        continue;
                    }

                    if ((sum - x - y) % 2 != 0)
                    {
                        continue;
                    }

                    if (Test(and >> 1, (sum - x - y) >> 1))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
