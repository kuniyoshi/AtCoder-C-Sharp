#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c272
{
    internal static class C
    {
        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine()!);
            var numbers = Console.ReadLine()!.Split().Select(int.Parse).OrderByDescending(value => value).ToArray();
            var odds = new List<int>();
            var evens = new List<int>();

            foreach (var number in numbers)
            {
                if (number == 0)
                {
                    continue;
                }

                if (number % 2 == 1 && odds.Count < 2)
                {
                    odds.Add(number);
                }

                if (number % 2 == 0 && evens.Count < 2)
                {
                    evens.Add(number);
                }
            }

            var odd = odds.Count == 2 ? odds.Sum() : -1;
            var even = evens.Count == 2 ? evens.Sum() : -1;

            if (odd != -1 && even != -1)
            {
                Console.WriteLine(Math.Max(odd, even));
            }
            else if (odd != -1)
            {
                Console.WriteLine(odd);
            }
            else if (even != -1)
            {
                Console.WriteLine(even);
            }
            else
            {
                Console.WriteLine(-1);
            }
        }
    }
}
