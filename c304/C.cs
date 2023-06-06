#nullable enable
using System;
using System.Linq;

namespace AtCoder.c304
{
    internal static class C
    {
        internal static void Run()
        {
            var (n, d) = ReadInput.ReadArrayInt2();
            var people = ReadInput.ReadArrayArrayInt(n);

            var tree = new UnionFindTree(n);

            for (var i = 0; i < n; ++i)
            {
                for (var j = 0; j < i; ++j)
                {
                    var dx = people[j][0] - people[i][0];
                    var dy = people[j][1] - people[i][1];

                    if (dx * dx + dy * dy <= d * d)
                    {
                        tree.Unite(i, j);
                    }
                }
            }

            for (var i = 0; i < n; ++i)
            {
                Console.WriteLine(tree.Root(0) == tree.Root(i) ? "Yes" : "No");
            }
        }

        static class ReadInput
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


        internal class UnionFindTree
        {
            int[] Parents { get; }
            int[] Sizes { get; }

            internal UnionFindTree(int n)
            {
                Sizes = Enumerable.Repeat(element: 1, n).ToArray();
                Parents = Enumerable.Range(start: 0, n).ToArray();
            }

            internal int Root(int v)
            {
                var parent = Parents[v];

                if (parent == v)
                {
                    return v;
                }

                Parents[v] = Root(parent);

                return Parents[v];
            }

            internal int Size(int u)
            {
                return Sizes[Root(u)];
            }

            internal void Unite(int u, int v)
            {
                var (rootU, rootV) = (Root(u), Root(v));

                if (rootU == rootV)
                {
                    return;
                }

                var (sizeU, sizeV) = (Size(u), Size(v));
                Sizes[rootV] = sizeU + sizeV;
                Sizes[rootU] = sizeU + sizeV;
                Parents[rootU] = rootV;
            }
        }

    }
}
