#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c226
{
    internal static class D
    {
        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine()!);
            var cities = Enumerable.Range(0, n)
                .Select(_ => Console.ReadLine()!.Split().Select(int.Parse).ToArray())
                .ToArray();
            var count = new HashSet<(int, int)>();

            for (var i = 0; i < cities.Length - 1; ++i)
            {
                var from = cities[i];

                for (var j = i + 1; j < cities.Length; ++j)
                {
                    var to = cities[j];
                    var deltaX = to[0] - from[0];
                    var deltaY = to[1] - from[1];

                    var (x, y) = Normalize(deltaX, deltaY);
                    count.Add((x, y));
                }
            }

            Console.WriteLine(2 * count.Count);
        }

        static (int, int) Normalize(int x, int y)
        {
            if (x == 0)
            {
                return (0, 1);
            }

            if (y == 0)
            {
                return (1, 0);
            }

            var (snX, snY) = (Math.Sign(x), Math.Sign(y)) switch
            {
                (1, 1) => (x, y),
                (1, -1) => (x, y),
                (-1, 1) => (-x, -y),
                (-1, -1) => (-x, -y),
                _ => throw new ArgumentOutOfRangeException()
            };

            var gcd = Gcd(snX, snY);

            return (snX / gcd, snY / gcd);
        }

        static int Gcd(int a, int b)
        {
            var (absA, absB) = (Math.Abs(a), Math.Abs(b));
            var (large, small) = absB > absA ? (absB, absA) : (absA, absB);

            while ((large % small) != 0)
            {
                (large, small) = (small, large % small);
            }

            return small;
        }
    }
}
