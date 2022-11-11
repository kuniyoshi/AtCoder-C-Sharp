#nullable enable
using System;
using System.Linq;

namespace AtCoder.Lib
{
    internal static class ReadInput
    {
        internal static int ReadSingle()
        {
            return int.Parse(Console.ReadLine()!);
        }

        internal static int[] ReadArrayInt()
        {
            return Console.ReadLine()!.Split().Select(int.Parse).ToArray();
        }

        internal static (int, int, int, int) ReadArrayInt4()
        {
            var array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            return (array[0], array[1], array[2], array[3]);
        }

        static int[][] ReadArrayArrayInt(int n)
        {
            return Enumerable.Range(0, n)
                .Select(_ => ReadInput.ReadArrayInt())
                .ToArray();
        }
    }
}
