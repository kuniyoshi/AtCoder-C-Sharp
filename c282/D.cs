#nullable enable
using System;
using System.Linq;

namespace AtCoder.c282
{
    internal static class D
    {
        internal static void Run()
        {
            var (n, m) = ReadArrayInt2();
            var edges = ReadArrayArrayInt(m);
            
            
        }

        static (int, int) ReadArrayInt2()
        {
            var array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            return (array[0], array[1]);
        }

        static int[][] ReadArrayArrayInt(int n)
        {
            return Enumerable.Range(0, n)
                .Select(_ => ReadArrayInt())
                .ToArray();
        }

        static int[] ReadArrayInt()
        {
            return Console.ReadLine()!.Split().Select(int.Parse).ToArray();
        }
    }
}
