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
    }
}
