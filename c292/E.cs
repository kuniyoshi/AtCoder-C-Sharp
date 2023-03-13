#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c292
{
    internal static class E
    {
        internal static void Run()
        {
            var(n, m) = ReadInput.ReadArrayInt2();

            var links = Enumerable.Range(0, n).Select(_ => new List<int>()).ToArray();

            for (var i = 0; i < m; ++i)
            {
                var (u, v) = ReadInput.ReadArrayInt2();
                u--;
                v--;

                links[u].Add(v);
            }

            var total = 0;

            for (var i = 0; i < n; ++i)
            {
                var count = 0;
                var visited = new bool[n];
                var queue = new Queue<int>();
                queue.Enqueue(i);
                visited[i] = true;

                while (queue.Count > 0)
                {
                    var u = queue.Dequeue();

                    foreach (var v in links[u])
                    {
                        if (visited[v])
                        {
                            continue;
                        }
                        queue.Enqueue(v);
                        visited[v] = true;
                        count++;
                    }
                }

                total += count;
            }

            Console.WriteLine(total - m);
        }
    }

    internal static class ReadInput
    {
        static int[] ReadArrayInt()
        {
            return Console.ReadLine()!.Split().Select(int.Parse).ToArray();
        }

        internal static (int, int) ReadArrayInt2()
        {
            var array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            return (array[0], array[1]);
        }

        internal static int[][] ReadArrayArrayInt(int n)
        {
            return Enumerable.Range(0, n)
                .Select(_ => ReadArrayInt())
                .ToArray();
        }
    }
}
