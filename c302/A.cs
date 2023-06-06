#nullable enable
using System;
using System.Linq;

namespace AtCoder.c302
{
    internal static class A
    {
        internal static void Run()
        {
            var ab = Console.ReadLine()!.Split().Select(long.Parse).ToArray();
            var (a, b) = (ab[0], ab[1]);
            Console.WriteLine((a + b + 1) / b);
        }
    }
}
