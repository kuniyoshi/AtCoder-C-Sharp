#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AtCoder.c224
{
    internal static class C
    {
        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine()!);
            var v = Enumerable.Range(0, n)
                .Select(_ => Console.ReadLine()!.Split().Select(int.Parse).ToArray())
                .OrderBy(w => w[0] + w[1])
                .ToArray();

            var count = 0;

            for (var i = 0; i < n - 2; ++i)
            {
                for (var j = i + 1; j < n - 1; ++j)
                {
                    for (var k = j + 1; k < n; ++k)
                    {
                        var a1 = new Fraction(v[j][0] - v[i][0], v[j][1] - v[i][1]);
                        var a2 = new Fraction(v[k][0] - v[j][0], v[k][1] - v[j][1]);

                        if (a1.IsDegreeSame(a2))
                        {
                            continue;
                        }

                        var dxij = v[j][0] - v[i][0];
                        var dxjk = v[k][0] - v[j][0];
                        var dyij = v[j][1] - v[i][1];
                        var dyjk = v[k][1] - v[j][1];

                        if ((dxij == 0 && dxjk == 0) || (dyij == 0 && dyjk == 0))
                        {
                            throw new Exception("all zero");
                            continue;
                        }

                        var minDx = Math.Min(dxij, dxjk);
                        var maxDx = Math.Min(dxij, dxjk);

                        if (dxij != 0 && dyij != 0 && dxjk % dxij == 0 && dyjk % dyij == 0)
                        {
                            Console.WriteLine($"### skip: ({v[i][0]}, {v[i][1]}), ({v[j][0]}, {v[j][1]}), ({v[k][0]}, {v[k][1]})");
                            Console.WriteLine($"--- dx: ({dxij}, {dxjk}), dy: ({dyij}, {dyjk})");
                            continue;
                        }

                        // var x = v[j][0] - v[i][0];
                        // var y = v[j][1] - v[i][1];
                        //
                        // if (x == 0)
                        // {
                        //     x++;
                        // }
                        //
                        // if (y == 0)
                        // {
                        //     y++;
                        // }
                        //
                        // var mx = (v[k][0] - v[j][0]) % x;
                        // var my = (v[k][1] - v[j][0]) % y;
                        //
                        // if (mx == 0 && my == 0)
                        // {
                        //     continue;
                        // }

                        // if (((v[j][0] + x) == v[k][0]) && ((v[j][1] + y) == v[k][1]))
                        // {
                        //     continue;
                        // }
                        //
                        // Console.WriteLine($"### x: {x}, y: {y}");
                        // Console.WriteLine($"### ({v[i][0]}, {v[i][1]})");
                        // Console.WriteLine($"### ({v[j][0]}, {v[j][1]})");
                        // Console.WriteLine($"### ({v[k][0]}, {v[k][1]})");
                        count++;
                    }
                }
            }

            Console.WriteLine(count);
        }

        class Fraction
        {
            int X { get; }
            int Y { get; }

            public Fraction(int x, int y)
            {
                X = x;
                Y = y;
            }

            public bool IsDegreeSame(Fraction other)
            {
                throw new NotImplementedException();
            }
        }
    }
}
