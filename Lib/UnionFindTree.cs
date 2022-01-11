#nullable enable
using System.Linq;

namespace AtCoder.Lib
{
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
