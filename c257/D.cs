#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c257
{
    internal static class D
    {
        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine()!);
            var jumps = Enumerable.Range(0, n)
                .Select(_ => Console.ReadLine()!.Split().Select(int.Parse).ToArray())
                .ToArray();

            ulong wa = 0L;
            ulong ac = 5_000_000_000;

            while (ac - wa > 1)
            {
                var wj = (wa + ac) / 2;

                if (Can(wj, n, jumps))
                {
                    ac = wj;
                }
                else
                {
                    wa = wj;
                }
            }

            Console.WriteLine(ac);
        }

        static bool Can(ulong s, int n, int[][] jumps)
        {
            var connected = Enumerable.Range(0, n)
                .Select(_ => new bool[n])
                .ToArray();

            for (var from = 0; from < n; ++from)
            {
                for (var to = 0; to < n; ++to)
                {
                    var distance = (ulong)Math.Abs(jumps[from][0] - jumps[to][0]) + (ulong)Math.Abs(jumps[from][1] - jumps[to][1]);
                    connected[from][to] = distance <= s * (ulong)jumps[from][2];
                }
            }

            for (var i = 0; i < n; ++i)
            {
                var queue = new Queue<int>();
                queue.Enqueue(i);
                var visited = new bool[n];

                while (queue.Any())
                {
                    var current = queue.Dequeue();

                    if (visited[current])
                    {
                        continue;
                    }

                    visited[current] = true;

                    for (var j = 0; j < n; ++j)
                    {
                        if (!visited[j] && connected[current][j])
                        {
                            queue.Enqueue(j);
                        }
                    }
                }

                if (visited.All(v => v))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
