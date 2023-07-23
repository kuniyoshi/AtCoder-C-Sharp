#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c309
{
    internal static class E
    {
        internal static void Run()
        {
            var (n, m) = ReadInput.ReadArrayInt2();
            var parents = ReadInput.ReadArrayInt().Select(number => number - 1).ToArray();
            var insurances = ReadInput.ReadArrayArrayInt(m).OrderByDescending(p => p[1]).ToArray();

            foreach (var pair in insurances)
            {
                pair[0]--;
            }

            var children = new Dictionary<int, List<int>>();

            for (var i = 0; i < parents.Length; ++i)
            {
                var child = i + 1;
                var parent = parents[i];

                if (!children.ContainsKey(parent))
                {
                    children.Add(parent, new List<int>());
                }

                children[parent].Add(child);
            }

            var powers = Enumerable.Range(0, n).Select(_ => -10).ToArray();

            foreach (var pair in insurances)
            {
                var (u, p) = (pair[0], pair[1]);

                if (powers[u] >= p)
                {
                    continue;
                }

                powers[u] = p;

                var queue = new Queue<int>();
                var distance = new Dictionary<int, int>
                {
                    [u] = 0,
                };
                queue.Enqueue(u);

                while (queue.Count > 0)
                {
                    var v = queue.Dequeue();

                    if (powers[v] > powers[u] - distance[v])
                    {
                        continue;
                    }

                    if (!children.ContainsKey(v))
                    {
                        continue;
                    }

                    foreach (var child in children[v])
                    {
                        if (powers[child] >= powers[u] - (distance[v] + 1))
                        {
                            continue;
                        }

                        distance[child] = distance[v] + 1;
                        powers[child] = powers[u] - (distance[v] + 1);
                        queue.Enqueue(child);
                    }
                }
            }

            Console.WriteLine(powers.Count(p => p >= 0));
        }

        static class ReadInput
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
}
