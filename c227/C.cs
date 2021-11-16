#nullable enable
using System;

namespace AtCoder.c227
{
    internal static class C
    {
        internal static void Run()
        {
            var n = long.Parse(Console.ReadLine()!);

            var count = 0L;

            for (var a = 1L; a * a * a <= n; ++a)
            {
                for (var b = a; a * b * b <= n; ++b)
                {
                    var maxC = n / (a * b);
                    var patterns = maxC - b + 1;
                    count = count + patterns;
                }
            }

            Console.WriteLine(count);
        }
    }
}
