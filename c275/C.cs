#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AtCoder.c275
{
    internal static class C
    {
        internal static void Run()
        {
            var cells = Enumerable.Range(0, 9)
                .Select(_ => Console.ReadLine()!)
                .ToArray();

            var points = new List<Tuple<int, int>>();

            for (var i = 0; i < 9; ++i)
            {
                for (var j = 0; j < 9; ++j)
                {
                    if (cells[i][j] == '#')
                    {
                        points.Add(Tuple.Create(i, j));
                    }
                }
            }

            var distances = new int[points.Count, points.Count];

            for (var i = 0; i < points.Count; ++i)
            {
                for (var j = 0; j < points.Count; ++j)
                {
                    distances[i, j] = Pow(points[i].Item1 - points[j].Item1) + Pow(points[i].Item2 - points[j].Item2);
                }
            }

            for (var k = 0; k < points.Count; ++k)
            {
                for (var i = 0; i < points.Count; ++i)
                {
                    for (var j = 0; j < points.Count; ++j)
                    {
                        if (distances[i, j] > distances[i, k] + distances[k, j])
                        {
                            distances[i, j] = distances[i, k] + distances[k, j];
                        }
                    }
                }
            }

            var count = 0;

            for (var i = 0; i < points.Count; ++i)
            {
                for (var j = 0; j < points.Count; ++j)
                {
                    for (var k = 0; k < points.Count; ++k)
                    {
                        for (var l = 0; l < points.Count; ++l)
                        {
                            if (i == j || i == k || i == l || j == k || j == l || k == l)
                            {
                                continue;
                            }

                            if (!IsAngle(i, j, k, points)
                                || !IsAngle(j, k, l, points)
                                || !IsAngle(k, l, i, points)
                                || !IsAngle(l, i, j, points))
                            {
                                continue;
                            }

                            var d = new[]
                            {
                                distances[i, j],
                                distances[j, k],
                                distances[k, l],
                                distances[l, i],
                            };
                            var h = new Dictionary<int, int>();

                            foreach (var x in d)
                            {
                                if (!h.ContainsKey(x))
                                {
                                    h[x] = 0;
                                }

                                h[x]++;
                            }

                            var isSame = h.Values.Max() == 4;

                            if (isSame)
                            {
                                count++;
                            }
                        }
                    }
                }
            }
            
            Console.WriteLine(count/ 4);
        }

        static bool IsAngle(int a, int b, int c, List<Tuple<int,int>> points)
        {
            var ba = Tuple.Create(points[a].Item1 - points[b].Item1, points[a].Item2 - points[b].Item2);
            var bc = Tuple.Create(points[c].Item1 - points[b].Item1, points[c].Item2 -  points[b].Item2);
            var cross = ba.Item1 * bc.Item2 - ba.Item2 *bc.Item1;
            return cross > 0;
        }

        static int Pow(int x)
        {
            return x * x;
        }
    }
}
