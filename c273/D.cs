#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c273
{
    internal static class D
    {
        internal static void Run()
        {
            var (h, w, rs, cs) = ReadInput.ReadArrayInt4();
            var n = ReadInput.ReadSingle();
            var walls = ReadInput.ReadArrayArrayInt(n);
            var q = ReadInput.ReadSingle();
            var queries = Enumerable.Range(start: 0, q)
                .Select(_ =>
                    {
                        var l = Console.ReadLine()!.Split().ToArray();
                        return Tuple.Create(l[0], int.Parse(l[1]));
                    }
                )
                .ToArray();

            var wallsOf = new Dictionary<Align, Dictionary<int, List<int>>>
            {
                [Align.Row] = new Dictionary<int, List<int>>(),
                [Align.Col] = new Dictionary<int, List<int>>(),
                [Align.ReverseRow] = new Dictionary<int, List<int>>(),
                [Align.ReverseCol] = new Dictionary<int, List<int>>(),
            };

            foreach (var wall in walls)
            {
                if (!wallsOf[Align.Row].ContainsKey(wall[0]))
                {
                    wallsOf[Align.Row][wall[0]] = new List<int>();
                }

                if (!wallsOf[Align.Col].ContainsKey(wall[1]))
                {
                    wallsOf[Align.Col][wall[1]] = new List<int>();
                }

                if (!wallsOf[Align.ReverseRow].ContainsKey(wall[0]))
                {
                    wallsOf[Align.ReverseRow][wall[0]] = new List<int>();
                }

                if (!wallsOf[Align.ReverseCol].ContainsKey(wall[1]))
                {
                    wallsOf[Align.ReverseCol][wall[1]] = new List<int>();
                }

                wallsOf[Align.Row][wall[0]].Add(wall[1]);
                wallsOf[Align.ReverseRow][wall[0]].Add(w - wall[1] + 1);
                wallsOf[Align.Col][wall[1]].Add(wall[0]);
                wallsOf[Align.ReverseCol][wall[1]].Add(h - wall[0] + 1);
            }

            foreach (var list in wallsOf.Values.SelectMany(v => v.Values))
            {
                list.Sort();
            }

            var current = new[]
            {
                rs,
                cs,
            };

            foreach (var query in queries)
            {
                int? wallPoint;

                switch (query.Item1)
                {
                    case "L":
                        wallPoint = BinarySearch(
                            wallsOf[Align.ReverseRow].ContainsKey(current[0])
                                ? wallsOf[Align.ReverseRow][current[0]]
                                : null,
                            current[1]
                        );
                        current[1] = w
                                     - Math.Min(
                                         w - current[1] + 1 + query.Item2,
                                         wallPoint.HasValue ? wallPoint.Value - 1 : w
                                     )
                                     + 1;
                        break;

                    case "R":
                        wallPoint = BinarySearch(
                            wallsOf[Align.Row].ContainsKey(current[0]) ? wallsOf[Align.Row][current[0]] : null,
                            current[1]
                        );
                        current[1] = Math.Min(current[1] + query.Item2, wallPoint.HasValue ? wallPoint.Value - 1 : w);
                        break;

                    case "U":
                        wallPoint = BinarySearch(
                            wallsOf[Align.ReverseCol].ContainsKey(current[1])
                                ? wallsOf[Align.ReverseCol][current[1]]
                                : null,
                            current[0]
                        );
                        current[0] = h
                                     - Math.Min(
                                         h - current[0] + 1 + query.Item2,
                                         wallPoint.HasValue ? wallPoint.Value - 1 : h
                                     )
                                     + 1;
                        break;

                    case "D":
                        wallPoint = BinarySearch(
                            wallsOf[Align.Col].ContainsKey(current[1]) ? wallsOf[Align.Col][current[1]] : null,
                            current[0]
                        );
                        current[0] = Math.Min(current[0] + query.Item2, wallPoint.HasValue ? wallPoint.Value - 1 : h);
                        break;
                }

                Console.WriteLine(string.Join(" ", current));
            }
        }

        static int? BinarySearch(List<int>? points, int current)
        {
            if (!(points?.Any() ?? false))
            {
                return null;
            }

            if (points[0] > current)
            {
                return null;
            }

            var wa = 0;
            var ac = points.Count - 1;

            while (ac - wa > 1)
            {
                var wj = (wa + ac) / 2;

                if (points[wj] < current)
                {
                    wa = wj;
                }
                else
                {
                    ac = wj;
                }
            }

            return points[ac];
        }

        enum Align
        {
            Row,
            Col,
            ReverseRow,
            ReverseCol,
        }

        static class ReadInput
        {
            internal static int[][] ReadArrayArrayInt(int n)
            {
                return Enumerable.Range(start: 0, n)
                    .Select(_ => ReadArrayInt())
                    .ToArray();
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

            internal static int ReadSingle()
            {
                return int.Parse(Console.ReadLine()!);
            }
        }
    }
}
