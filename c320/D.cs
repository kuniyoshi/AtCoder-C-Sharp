#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c320
{
    internal static class D
    {
        internal static void Run()
        {
            var (n, m) = ReadInput.ReadArrayInt2();
            var edges = Enumerable.Range(0, m).Select(_ => ReadInput.ReadArrayInt()).ToArray();

            var links = Enumerable.Range(0, n).Select(_ => new List<int>()).ToArray();
            var deltas = Enumerable.Range(0, n).Select(_ => new Dictionary<int, Coord>()).ToArray();

            foreach (var edge in edges)
            {
                var (from, to, dx, dy) = (edge[0], edge[1], edge[2], edge[3]);
                from--;
                to--;

                links[from].Add(to);
                links[to].Add(from);

                deltas[from][to] = new Coord(dx, dy);
                deltas[to][from] = new Coord(-dx, -dy);
            }

            var positions = new Coord[n];
            positions[0] = new Coord(0, 0);

            deltas[0][0] = new Coord(0, 0);

            var queue = new Queue<Tuple<int, Coord>>();
            queue.Enqueue(Tuple.Create(0, new Coord(0, 0)));

            var visited = new HashSet<int>();

            while (queue.Count > 0)
            {
                var (u, coord) = queue.Dequeue();

                if (visited.Contains(u))
                {
                    continue;
                }

                visited.Add(u);

                foreach (var neighbor in links[u].Where(v => !visited.Contains(v)))
                {
                    positions[neighbor] = new Coord(
                        coord.X + deltas[u][neighbor].X,
                        coord.Y + deltas[u][neighbor].Y
                    );

                    queue.Enqueue(Tuple.Create(neighbor, new Coord(positions[neighbor].X, positions[neighbor].Y)));
                }
            }

            for (var index = 0; index < positions.Length; index++)
            {
                if (!visited.Contains(index))
                {
                    Console.WriteLine("undecidable");
                    continue;
                }

                var position = positions[index];
                Console.WriteLine($"{position.X} {position.Y}");
            }
        }

        struct Coord
        {
            internal long X;
            internal long Y;

            internal Coord(long x, long y)
            {
                X = x;
                Y = y;
            }
        }

        static class ReadInput
        {
            internal static int[] ReadArrayInt()
            {
                return Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            }

            internal static (int, int) ReadArrayInt2()
            {
                var array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
                return (array[0], array[1]);
            }
        }
    }
}
