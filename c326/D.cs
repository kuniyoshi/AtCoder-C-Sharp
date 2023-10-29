#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c326
{
    internal static class D
    {
        internal static void Run()
        {
            var n = ReadInput.ReadSingle();
            var r = Console.ReadLine()!.ToCharArray();
            var c = Console.ReadLine()!.ToCharArray();
            var matrix = Enumerable.Range(0, n).Select(_ => Enumerable.Repeat('.', n).ToArray()).ToArray();

            var can = Dfs(0, matrix, r, c);

            if (can)
            {
                YesNo.Write(can);

                foreach (var cols in matrix)
                {
                    Console.WriteLine(new string(cols));
                }

                return;
            }

            YesNo.Write(false);
        }

        static readonly char[] Chars = { 'A', 'B', 'C', '.' };

        static bool Dfs(int id, char[][] matrix, char[] rowConstraint, char[] colConstraint)
        {
            if (id == matrix.Length * matrix.Length)
            {
                return Test(matrix);
            }

            var row = id / matrix.Length;
            var col = id % matrix.Length;

            for (var i = row; i < matrix.Length; ++i)
            {
                var usedInRow = matrix[i].ToHashSet();
                var rowCandidates = Chars.ToHashSet();

                foreach (var c in usedInRow)
                {
                    if (c != '.')
                    {
                        rowCandidates.Remove(c);
                    }
                }

                if (usedInRow.Count == 1)
                {
                    rowCandidates = new HashSet<char> { rowConstraint[i] };
                }

                for (var j = col; j < matrix.Length; ++j)
                {
                    var usedInCol = matrix.Select(r => r[j]).ToHashSet();
                    var colCandidates = Chars.ToHashSet();

                    foreach (var c in usedInCol)
                    {
                        if (c != '.')
                        {
                            colCandidates.Remove(c);
                        }
                    }

                    if (usedInCol.Count == 1)
                    {
                        colCandidates = new HashSet<char> { colConstraint[j] };
                    }

                    for (var k = 0; k < Chars.Length; ++k)
                    {
                        if (!rowCandidates.Contains(Chars[k]))
                        {
                            continue;
                        }

                        if (!colCandidates.Contains(Chars[k]))
                        {
                            continue;
                        }

                        var backup = matrix[i][j];
                        matrix[i][j] = Chars[k];
                        var returnValue = Dfs(i * matrix.Length + j + 1, matrix, rowConstraint, colConstraint);

                        if (returnValue)
                        {
                            return true;
                        }

                        matrix[i][j] = backup;
                    }
                }
            }

            return false;
        }

        static bool Test(char[][] matrix)
        {
            foreach (var row in matrix)
            {
                if (!Test2(row))
                {
                    return false;
                }
            }

            var cols = Enumerable.Range(0, matrix.Length)
                .Select(j => Enumerable.Range(0, matrix.Length).Select(i => matrix[i][j]).ToArray());

            foreach (var col in cols)
            {
                if (!Test2(col))
                {
                    return false;
                }
            }

            return true;
        }

        static bool Test2(char[] lineChars)
        {
            var used = new HashSet<char>();

            foreach (var c in lineChars)
            {
                if (c == '.')
                {
                    continue;
                }

                if (used.Contains(c))
                {
                    return false;
                }

                used.Add(c);
            }

            return used.Count == 3;
        }

        static class YesNo
        {
            internal static void Write(bool isYes)
            {
                Console.WriteLine(isYes ? "Yes" : "No");
            }
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
