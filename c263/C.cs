#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c263
{
    internal static class C
    {
        internal static void Run()
        {
            var nm = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var (n, m) = (nm[0], nm[1]);

            R(n, 1, m, new List<int>());
        }

        static void R(int remain, int from, int to, List<int> buffer)
        {
            if (remain == 0)
            {
                Console.WriteLine(string.Join(" ", buffer));
                return;
            }

            for (var i = from; i <= to; ++i)
            {
                var newBuffer = buffer.ToList();
                newBuffer.Add(i);
                R(remain - 1, i + 1, to, newBuffer);
            }
        }
    }
}
