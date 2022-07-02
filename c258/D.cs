#nullable enable
using System;
using System.Linq;

namespace AtCoder.c258
{
    internal static class D
    {
        internal static void Run()
        {
            var nx = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var n = nx[0];
            var x = nx[1];
            var stages = Enumerable.Range(0, n)
                .Select(_ => Console.ReadLine()!.Split().Select(int.Parse).ToArray())
                .ToArray();

            var min = long.MaxValue;
            var previous = 0L;
            var remain = x;

            foreach (var stage in stages)
            {
                if (remain == 0)
                {
                    break;
                }
                var (story, game) = (stage[0], stage[1]);
                previous += story + game;
                remain--;
                var loop = previous + (long)game * remain;
                min = Math.Min(min, loop);
            }

            Console.WriteLine(min);
        }
    }
}
