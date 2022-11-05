#nullable enable
using System;
using System.Linq;

namespace AtCoder.c276
{
    internal static class D
    {
        internal static void Run()
        {
            var n = Console.ReadLine()!;
            var a = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

            var count = Answer(a);
            
            Console.WriteLine(count);
        }

        static int Answer(int[] values)
        {
            var total = 0;
            var x = (int?)null;

            var count = new[]
            {
                new int[values.Length],
                new int[values.Length],
            };

            for (var i = 0; i < values.Length; ++i)
            {
                var value = values[i];

                while ((value % 2) == 0)
                {
                    value /= 2;
                    total++;
                    count[0][i]++;
                }
                while ((value % 3) == 0)
                {
                    value /= 3;
                    total++;
                    count[0][i]++;
                }

                if (!x.HasValue)
                {
                    x = value;
                }
                else if (x.Value != value)
                {
                    return -1;
                }
            }

            total -= count[0].Min() * values.Length;
            total -= count[1].Min() * values.Length;

            return total;
        }
    }
}
