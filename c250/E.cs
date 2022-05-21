#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c250
{
    internal static class E
    {
        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine()!);
            var a = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var b = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var q = int.Parse(Console.ReadLine()!);
            var queries = Enumerable.Range(0, q)
                .Select(_ => Console.ReadLine()!.Split().Select(int.Parse).ToArray())
                .ToArray();
            
            
        }
    }
}
