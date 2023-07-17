#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c305
{
    internal static class E
    {
        internal static void Run()
        {
            var (n, m, k) = ReadInput.ReadArrayInt3();
            var edges = ReadInput.ReadArrayArrayInt(m);
            var guards = ReadInput.ReadArrayArrayInt(k).OrderByDescending(p => p[1]);

            var link = new Dictionary<int, List<int>>();

            foreach (var edge in edges)
            {
                var (u, v) = (edge[0], edge[1]);
                u--;
                v--;

                if (!link.ContainsKey(u))
                {
                    link.Add(u, new List<int>());
                }

                if (!link.ContainsKey(v))
                {
                    link.Add(v, new List<int>());
                }

                link[u].Add(v);
                link[v].Add(u);
            }

            var hp = new Dictionary<int, int>();

            foreach (var guard in guards)
            {
                var (p, h) = (guard[0], guard[1]);
                p--;

                if (hp.ContainsKey(p) && hp[p] >= h)
                {
                    continue;
                }

                var queue = new Queue<Tuple<int, int>>();
                queue.Enqueue(Tuple.Create(p, 0));

                while (queue.Count > 0)
                {
                    var pair = queue.Dequeue();

                    if (hp.ContainsKey(pair.Item1) && hp[pair.Item1] >= h - pair.Item2)
                    {
                        continue;
                    }

                    hp[pair.Item1] = h - pair.Item2;

                    if (h - pair.Item2 == 0)
                    {
                        continue;
                    }

                    if (!link.ContainsKey(pair.Item1))
                    {
                        continue;
                    }

                    foreach (var v in link[pair.Item1])
                    {
                        if (hp.ContainsKey(v) && hp[v] >= h - pair.Item2 - 1)
                        {
                            continue;
                        }

                        queue.Enqueue(Tuple.Create(v, pair.Item2 + 1));
                    }
                }
            }

            Console.WriteLine(hp.Count);
            Console.WriteLine(string.Join(" ", hp.Keys.OrderBy(h => h).Select(v => v + 1)));

            // var guarded = new List<int>();
            //
            // for (var i = 0; i < n; ++i)
            // {
            //     if (hp.ContainsKey(i))
            //     {
            //         guarded.Add(i + 1);
            //     }
            // }
            //
            // Console.WriteLine(guarded.Count);
            // Console.WriteLine(string.Join(" ", guarded.Select(v => v.ToString())));
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

            internal static (int, int, int) ReadArrayInt3()
            {
                var array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
                return (array[0], array[1], array[2]);
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
