#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c236
{
    internal static class D
    {
        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine()!);
            var a = Enumerable.Range(0, 2 * n - 1)
                .Select(_ => Console.ReadLine()!.Split().Select(long.Parse).ToArray())
                .ToArray();

            Console.WriteLine(Dfs(new List<int>(), Enumerable.Range(0, 2 * n).ToList(), n, a));
        }

        static long Dfs(List<int> selectedIndexes, List<int> bufferIndexes, int n, long[][] a)
        {
            if (!bufferIndexes.Any())
            {
                var acc = 0L;

                for (var i = 0; i < n; ++i)
                {
                    var x = selectedIndexes[2 * i];
                    var y = selectedIndexes[2 * i + 1];
                    acc = acc ^ a[x][y - x - 1];
                }

                return acc;
            }

            var max = 0L;

            selectedIndexes.Add(bufferIndexes.First());
            bufferIndexes.RemoveAt(0);

            for (var i = 0; i < bufferIndexes.Count; ++i)
            {
                var indexes = new List<int>(selectedIndexes) { bufferIndexes[i] };
                var buffers = new List<int>(bufferIndexes);
                buffers.RemoveAt(i);
                var candidate = Dfs(
                    indexes,
                    buffers,
                    n,
                    a
                );

                max = candidate > max ? candidate : max;
            }

            return max;
        }
    }
}
