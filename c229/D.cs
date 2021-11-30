#nullable enable
using System;
using System.Linq;

namespace AtCoder.c229
{
    internal static class D
    {
        internal static void Run()
        {
            var s = ReadS();
            var k = int.Parse(Console.ReadLine()!);

            var dotCounts = new int[s.Length];
            var acc = 0;

            for (var i = 0; i < s.Length; ++i)
            {
                if (s[i] == X.Dot)
                {
                    acc++;
                }

                dotCounts[i] = acc;
            }

            var max = 0;

            for (var i = 0; i < s.Length; ++i)
            {
                if (GetDotCounts(i, s.Length - 1, dotCounts) <= k)
                {
                    max = Math.Max(s.Length - i, max);
                    break;
                }

                var ac = i;
                var wa = s.Length - 1;

                while (wa - ac > 1)
                {
                    var wj = (ac + wa) / 2;

                    if (GetDotCounts(i, wj, dotCounts) <= k)
                    {
                        ac = wj;
                    }
                    else
                    {
                        wa = wj;
                    }
                }

                max = Math.Max(ac - i + 1, max);
            }

            Console.WriteLine(max);
        }

        static int GetDotCounts(int start, int end, int[] dotCounts)
        {
            if (start != 0)
            {
                return dotCounts[end] - dotCounts[start - 1];
            }

            return dotCounts[end];
        }

        static X[] ReadS()
        {
            var s = Console.ReadLine()!;
            return s.ToCharArray().Select(c => c switch
                    {
                        'X' => X.Mark,
                        '.' => X.Dot,
                        var unknown => throw new Exception($"unknown: [{unknown}]"),
                    }
                )
                .ToArray();
        }

        enum X
        {
            Mark,
            Dot,
        }
    }
}
