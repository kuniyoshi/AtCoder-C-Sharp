#nullable enable
using System;

namespace AtCoder.c165
{
    internal static class B
    {
        internal static void Run()
        {
            var x = long.Parse(Console.ReadLine()!);
            var count = 0;
            var acc = 100L;

            while (acc < x)
            {
                acc += acc / 100;
                count++;
            }

            Console.WriteLine(count);
        }
    }
}
