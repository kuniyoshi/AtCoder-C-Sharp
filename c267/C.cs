#nullable enable
using System;
using System.Linq;

namespace AtCoder.c267
{
    internal static class C
    {
        internal static void Run()
        {
            var nm = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var m = nm[1];
            var numbers = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var max = long.MinValue;

            for (var i = 0; i < numbers.Length; ++i)
            {
                if (i + m - 1 >= numbers.Length)
                {
                    continue;
                }

                var value = 0L;
                
                for (var j = 0; j < m; ++j)
                {
                    value += numbers[i + j] * (j + 1);
                }

                max = Math.Max(max, value);
            }

            Console.WriteLine(max);
        }
    }
}
