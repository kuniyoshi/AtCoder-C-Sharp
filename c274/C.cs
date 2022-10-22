#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c274
{
    internal static class C
    {
        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine()!);
            var a = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

            var index = new Dictionary<int, int>
            {
                [1] = 1,
            };

            for (var i = 0; i < a.Length; ++i)
            {
                var x = a[i];
                var ix = index[x];
                var nameLeft = (i + 1) * 2;
                var nameRight = (i + 1) * 2 + 1;

                var indexLeft = ix * 2;
                var indexRight = ix * 2 + 1;

                index[nameLeft] = indexLeft;
                index[nameRight] = indexRight;
            }

            for (var i = 1; i <= 2 * n + 1; ++i)
            {
                if (index.ContainsKey(i))
                {
                    var current = index[i];
                    var count = 0;

                    while (current > 1)
                    {
                        current = current >> 1;
                        count++;
                    }

                    Console.WriteLine(count);
                }
                else
                {
                    Console.WriteLine(0);
                }
            }
        }
    }
}
