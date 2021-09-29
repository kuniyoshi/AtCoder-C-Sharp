#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c220
{
    internal static class C
    {
        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine()!);
            var a = new Queue<long>(Console.ReadLine()!.Split().Select(long.Parse));
            var x = long.Parse(Console.ReadLine()!);

            if (a.Count != n)
            {
                throw new Exception("invalid lenght a");
            }

            var seriesSum = a.Sum();
            var seriesCount = x / seriesSum;

            var sum = seriesCount * seriesSum;

            while (sum < x)
            {
                sum = sum + a.Dequeue();
            }

            Console.WriteLine(seriesCount * n + n - a.Count);
        }
    }
}
