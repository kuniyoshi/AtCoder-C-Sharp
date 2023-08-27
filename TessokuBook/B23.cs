#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.TessokuBook
{
    internal static class B23
    {
        static List<List<double>> _distance = new List<List<double>>();
        static int _n = -1;

        internal static void Run()
        {
            var n = ReadInput.ReadSingle();
            _n = n;
            var xy = ReadInput.ReadArrayArrayInt(n);

            _distance = Enumerable.Range(0, n).Select(_ => Enumerable.Repeat(0.0, n).ToList()).ToList();

            for (var i = 0; i < n; ++i)
            {
                for (var j = 0; j < n; ++j)
                {
                    _distance[i][j] = Math.Sqrt(
                        Math.Pow(xy[i][0] - xy[j][0], 2)+
                        Math.Pow(xy[i][1] - xy[j][1], 2)
                    );
                }
            }

            var result =Enumerable.Range(0, n).Select(u => Dfs(0, u, u, 0)).Where(u => u.HasValue).Min();
            Console.WriteLine(result ?? -1);
        }

        static double? Dfs(int passed, int current, int first, double total)
        {
            if (passed == ((1 << _n)  - 1) && current == first)
            {
                return total;
            }

            if (Convert.ToBoolean(passed & 1 << current))
            {
                return null;
            }

            passed |= 1 << current;
            double? min = null;

            for (var i = 0; i < _n; ++i)
            {
                if (Convert.ToBoolean(passed & 1 << i) && i != first)
                {
                    continue;
                }

                var candidate = Dfs(passed, i, first, total + _distance[current][i]);

                if (!candidate.HasValue)
                {
                    continue;
                }

                min ??= candidate.Value;
                min = Math.Min(min.Value, candidate.Value);
            }

            return min;
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
