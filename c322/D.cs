#nullable enable
using System;
using System.Linq;

namespace AtCoder.c322
{
    internal static class D
    {

        struct Coord
        {
            internal  int Row;
            internal  int Col;

            internal Coord(int i, int j)
            {
                Row = i;
                Col = j;
            }
        }
        struct Grid
        {
            internal readonly bool[,] Cells;
            internal Coord Coord;

            internal Grid(string[] input)
            {
                Cells = new bool[4, 4];

                for (var i = 0; i < input.Length; ++i)
                {
                    for (var j = 0; j < input[i].Length; ++j)
                    {
                        Cells[i, j] = input[i][j] == '#';
                    }
                }

                Coord = new Coord(0, 0);
            }

            Grid(bool[,] cells, Coord coord)
            {
                Cells = cells;
                Coord = coord;
            }

            public bool IsCenter()
            {
                var min = new Coord(7, 7);
                var max = new Coord(0, 0);

                for (var i = 0; i < 4; ++i)
                {
                    for (var j = 0; j < 4; ++j)
                    {
                        if (Cells[i, j])
                        {
                            min.Row = Math.Min(min.Row, i);
                            min.Col = Math.Min(min.Col, j);

                            max.Row = Math.Max(max.Row, i);
                            max.Col = Math.Max(max.Col, j);
                        }
                    }
                }

                var isInRange = IsInRange(min.Row + Coord.Row, min.Col + Coord.Col)
                                && IsInRange(max.Row + Coord.Row, max.Col + Coord.Col);
                return isInRange;
            }

            internal Grid Rotate()
            {
                var buffer = new bool[4, 8];
                var sin90 = 1;
                var cos90 = 0;

                for (var i = 0; i < 4; ++i)
                {
                    for (var j = 0; j < 4; ++j)
                    {
                        if (!Cells[i, j])
                        {
                            continue;
                        }

                        buffer[sin90 * j + cos90 * i, 4 + cos90 * j - sin90 * i] = true;
                    }
                }

                var min = 7;
                var max = 0;

                for (var i = 0; i < 4; ++i)
                {
                    for (var j = 0; j < 8; ++j)
                    {
                        if (buffer[i, j])
                        {
                            min = Math.Min(min, j);
                            max = Math.Max(max, j);
                        }
                    }
                }

                var cells = new bool[4, 4];

                for (var i = 0;  i< 4; ++i)
                {
                    for (var j = 0; j < 4; ++j)
                    {
                        cells[i, j] = buffer[i, min + j];
                    }
                }

                return new Grid(cells, Coord);
            }
        }

        internal static void Run()
        {
            var a = new Grid(Enumerable.Range(0, 4).Select(_ => Console.ReadLine()!).ToArray());
            var b = new Grid(Enumerable.Range(0, 4).Select(_ => Console.ReadLine()!).ToArray());
            var c = new Grid(Enumerable.Range(0, 4).Select(_ => Console.ReadLine()!).ToArray());

            var isYes = Impl(a, b, c);
            YesNo.Write(isYes);
        }

        static Grid[] MakeRotatePatterns(Grid a)
        {
            var patterns = new Grid[4];
            patterns[0] = a;
            patterns[1] = patterns[0].Rotate();
            patterns[2] = patterns[1].Rotate();
            patterns[3] = patterns[2].Rotate();
            return patterns;
        }

        static bool Impl(Grid a, Grid b, Grid c)
        {
            var aList = MakeRotatePatterns(a);
            var bList = MakeRotatePatterns(b);
            var cList = MakeRotatePatterns(c);

            foreach (var aItem in aList)
            {
                foreach (var bItem in bList)
                {
                    foreach (var cItem in cList)
                    {
                        // 4 * 4 * 4
                        if (Solve(aItem, bItem, cItem))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        static bool Solve(Grid a, Grid b, Grid c)
        {
            for (var i = 0; i < 16 * 9; ++i)
            {
                a.Coord.Row = i / 16;
                a.Coord.Col = i % 16;

                if (!a.IsCenter())
                {
                    continue;
                }

                for (var j = 0; j < 16 * 9; ++j)
                {
                    b.Coord.Row = j / 16;
                    b.Coord.Col = j % 16;

                    if (!b.IsCenter())
                    {
                        continue;
                    }

                    for (var k = 0; k < 16 * 9; ++k)
                    {
                        c.Coord.Row = k / 16;
                        c.Coord.Col = k % 16;

                        if (!c.IsCenter())
                        {
                            continue;
                        }

                        if (Test(a, b, c))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        static bool IsInRange(int i, int j)
        {
            return i >= 4 && i <= 7 && j >= 4 && j <= 7;
        }

        static bool Test(Grid a, Grid b, Grid c)
        {
            var cells = new bool[4, 4];

            if (!FillCells(a, cells))
            {
                return false;
            }

            if (!FillCells(b, cells))
            {
                return false;
            }

            if (!FillCells(c, cells))
            {
                return false;
            }

            for (var i = 0; i < 4; ++i)
            {
                for (var j = 0; j < 4; ++j)
                {
                    if (!cells[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        static bool FillCells(Grid a, bool[,] cells)
        {
            for (var i = 0; i < 4; ++i)
            {
                for (var j = 0; j < 4; ++j)
                {
                    if (!a.Cells[i, j])
                    {
                        continue;
                    }

                    if (!IsInRange(a.Coord.Row + i, a.Coord.Col + j))
                    {
                        return false;
                    }

                    var u = a.Coord.Row + i - 4;
                    var v = a.Coord.Col + j - 4;

                    if (cells[u, v])
                    {
                        return false;
                    }

                    cells[u, v] = true;
                }
            }

            return true;
        }

        static class YesNo
        {
            internal static void Write(bool isYes)
            {
                Console.WriteLine(isYes ? "Yes" : "No");
            }
        }
    }
}
