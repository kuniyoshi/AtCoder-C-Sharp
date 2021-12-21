#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c232
{
    internal static class D
    {
        internal static void Run()
        {
            var hw = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var h = hw[0];
            var w = hw[1];

            var c = Enumerable.Range(start: 0, h)
                .Select(_ => Console.ReadLine()!.ToCharArray())
                .ToArray();

            var max = Bfs(
                new Queue<Coord>(new[] { new Coord(h: 0, w: 0) }),
                steps: 0,
                c
            );

            Console.WriteLine(max);
        }

        static int Bfs(Queue<Coord> queue, int steps, char[][] c)
        {
            if (!queue.Any())
            {
                return steps;
            }

            var coord = queue.Dequeue();
            steps++;

            var max = steps;
            var nexts = new[] { coord.Down(), coord.Right() };

            foreach (var next in nexts)
            {
                if (!(next.IsIn(c) && next.CanBe(c)))
                {
                    continue;
                }

                var newQueue = new Queue<Coord>(queue);
                newQueue.Enqueue(next);

                max = Math.Max(Bfs(newQueue, steps, c), max);
            }

            return max;
        }

        readonly struct Coord
        {
            public Coord(int h, int w)
            {
                H = h;
                W = w;
            }

            int H { get; }
            int W { get; }

            public Coord Down()
            {
                return new Coord(H + 1, W);
            }

            public Coord Right()
            {
                return new Coord(H, W + 1);
            }

            public bool IsIn(char[][] c)
            {
                return H < c.Length
                       && W < c[0].Length;
            }

            public bool CanBe(char[][] c)
            {
                return c[H][W] == '.';
            }
        }
    }
}
