#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c272
{
    internal static class D
    {
        internal static void Run()
        {
            var nm = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var (n, m) = (nm[0], nm[1]);
            var moves = GetMoves(n, m);

            var queue = new Queue<ValueTuple<int, int, int>>();
            var distances = Enumerable.Range(0, n).Select(_ => new int[n]).ToArray();

            for (var i = 0; i < n; ++i)
            {
                for (var j = 0; j < n; ++j)
                {
                    distances[i][j] = -1;
                }
            }

            queue.Enqueue(ValueTuple.Create(0, 0, 0));

            while (queue.Count > 0)
            {
                var item = queue.Dequeue();

                if (distances[item.Item1][item.Item2] != -1)
                {
                    continue;
                }

                distances[item.Item1][item.Item2] = item.Item3;

                foreach (var move in moves)
                {
                    var row = item.Item1 + move.Item1;
                    var col = item.Item2 + move.Item2;

                    if (row < 0 || row >= n || col < 0 || col >= n)
                    {
                        continue;
                    }

                    if (distances[row][col] != -1)
                    {
                        continue;
                    }

                    queue.Enqueue(ValueTuple.Create(row, col, item.Item3 + 1));
                }
            }

            for (var i = 0; i < n; ++i)
            {
                Console.WriteLine(string.Join(" ", distances[i]));
            }
        }

        static List<ValueTuple<int, int>> GetMoves(int n, int m)
        {
            var moves = new List<ValueTuple<int, int>>();

            for (var i = -n; i <= n; ++i)
            {
                for (var j = -n; j <= n; ++j)
                {
                    if (i * i + j * j == m)
                    {
                        moves.Add(ValueTuple.Create(i, j));
                    }
                }
            }

            return moves;
        }
    }
}
