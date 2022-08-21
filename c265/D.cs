#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c265
{
    internal static class D
    {
        internal static void Run()
        {
            var parameters = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var (n, p, q, r) = (parameters[0], parameters[1], parameters[2], parameters[3]);
            var a = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

            var accs = new List<long> { 0 };

            for (var i = 0; i < n; ++i)
            {
                accs.Add(accs.Last() + a[i]);
            }

            var queries = new[] { p, q, r };

            for (var i = 0; i < n; ++i)
            {
                var pos = new Pos(i, i);
                var found = true;

                for (var j = 0; j < queries.Length; j++)
                {
                    pos = BinarySearch(accs, pos.Right, n, queries[j]);

                    if (pos == null)
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    YesNo.Write(true);
                    return;
                }
            }

            YesNo.Write(false);
        }

        static Pos? BinarySearch(List<long> accs, int from, int wa, long value)
        {
            if (from >= accs.Count - 1)
            {
                return null;
            }

            if (accs[wa] - accs[from] == value)
            {
                return new Pos(from, wa);
            }

            var ac = from;

            while (wa - ac > 1)
            {
                var wj = (ac + wa) / 2;

                if (accs[wj] - accs[from] == value)
                {
                    return new Pos(from, wj);
                }

                if (accs[wj] - accs[from] < value)
                {
                    ac = wj;
                }
                else
                {
                    wa = wj;
                }
            }

            if (accs[wa] - accs[from] == value)
            {
                return new Pos(from, wa);
            }

            return null;
        }

        internal static class YesNo
        {
            internal static void Write(bool isYes)
            {
                Console.WriteLine(isYes ? "Yes" : "No");
            }
        }

        class Pos
        {
            public Pos(int left, int right)
            {
                Left = left;
                Right = right;
            }

            public int Left { get; }
            public int Right { get; }
        }
    }
}
