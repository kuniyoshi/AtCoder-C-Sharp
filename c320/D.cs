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
            var deltas = Enumerable.Range(0, n).Select(_ => new Dictionary<int, Tuple<int, int>>()).ToArray();

            foreach (var edge in edges)
            {
                var (from, to, dx, dy) = (edge[0], edge[1], edge[2], edge[3]);
                from--;
                to--;

                links[from].Add(to);
                links[to].Add(from);

                deltas[from][to] = Tuple.Create(dx, dy);
                deltas[to][from] = Tuple.Create(-dx, -dy);
            }

            var positions = new Tuple<int, int>[n];
            positions[0] = Tuple.Create(0, 0);

            deltas[0][0] = new Tuple<int, int>(0, 0);

            var queue = new Queue<Tuple<int, int, int>>();
            queue.Enqueue(Tuple.Create<int, int, int>(0, 0, 0));

            var visited = new HashSet<int>();

            while (queue.Count > 0)
            {
                var cursor = queue.Dequeue();

                if (visited.Contains(cursor.Item1))
                {
                    continue;
                }

                visited.Add(cursor.Item1);

                foreach (var neighbor in links[cursor.Item1])
                {
                    if (visited.Contains(neighbor))
                    {
                        continue;
                    }

                    positions[neighbor] = Tuple.Create(
                        cursor.Item2 + deltas[cursor.Item1][neighbor].Item1,
                        cursor.Item3 + deltas[cursor.Item1][neighbor].Item2
                    );
                    visited.Add(neighbor);

                    queue.Enqueue(Tuple.Create<int, int, int>(neighbor, positions[cursor.Item1].Item1, positions[cursor.Item1].Item2));
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
                Console.WriteLine($"{position.Item1} {position.Item2}");
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
