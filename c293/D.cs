#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c293
{
    internal static class D
    {
        internal static void Run()
        {
            var (n, m) = ReadInput.ReadArrayInt2();
            var edges = Enumerable.Range(0, m).Select(_ =>
                {
                    var line = Console.ReadLine().Split().ToArray();
                    var a = int.Parse(line[0]);
                    var c = int.Parse(line[2]);
                    a--;
                    c--;
                    var b = line[1];
                    var d = line[3];

                    if (b == "B")
                    {
                        a += n;
                    }

                    if (d == "B")
                    {
                        c += n;
                    }

                    return Tuple.Create(a, c);
                }
            ).ToArray();

            var links = Enumerable.Range(0,2 * n).Select(_ => new List<int>()).ToArray();

            foreach (var (u, v) in edges)
            {
                links[u].Add(v);
                links[v].Add(u);
            }

            for (var i = 0; i < n; ++i)
            {
                links[i].Add(i + n);
                links[i+n].Add(i );
            }

            var tree = new UnionFindTree(2 * n);

            for (var u = 0; u < 2 * n; ++u)
            {
                foreach (var v in links[u])
                {
                    tree.Unite(u, v);
                }
            }

            var vertexCount = new Dictionary<int, int>();
            var edgeCount = new Dictionary<int, int>();

            for (var u = 0; u < 2 *n; ++u)
            {
                var root = tree.Root(u);
                if (!vertexCount.ContainsKey(root))
                {
                    vertexCount[root] = 0;
                }

                if (!edgeCount.ContainsKey(root))
                {
                    edgeCount[root] = 0;
                }

                vertexCount[root]++;
                edgeCount[root] += links[u].Count;
            }

            var totalRootCount = vertexCount.Count;
            var loopCount = 0;

            foreach (var root in vertexCount.Keys)
            {
                if (vertexCount[root] == edgeCount[root] / 2)
                {
                    loopCount++;
                }
            }

            Console.WriteLine($"{loopCount} {totalRootCount - loopCount}");
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


    internal static class ReadInput
    {
        internal static (int, int) ReadArrayInt2()
        {
            var array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            return (array[0], array[1]);
        }
    }

}
