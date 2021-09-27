#nullable enable
using System;
using System.Linq;

namespace AtCoder.c220
{
    internal static class C
    {
        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine()!);
            var a = Console.ReadLine()!.Split().Select(long.Parse).ToArray();
            var x = long.Parse(Console.ReadLine()!);

            var seriesSum = a.Sum();
            var seriesCount = x / seriesSum;

            var h = 0;
            var sum = seriesCount * seriesSum;

            foreach (var item in a)
            {
                sum = sum + item;
                h++;

                if (sum > x)
                {
                    break;
                }
            }

            Console.WriteLine($"series count: {seriesCount * a.Length}");
            Console.WriteLine($"h: {h}");
            Console.WriteLine(seriesCount * a.Length + h);
        }
    }
}
