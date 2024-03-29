﻿#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoder.c248
{
    internal static class D
    {
        internal static void Run()
        {
            var results = Work(Input.FromConsole());

            foreach (var value in results)
            {
                Console.WriteLine(value);
            }
        }

        static int GetLowerBound(int index, List<int> indexes)
        {
            if (index < indexes.First())
            {
                return 0;
            }

            if (index > indexes.Last())
            {
                return indexes.Count;
            }

            if (index == indexes.First())
            {
                return 1;
            }

            if (index == indexes.Last())
            {
                return indexes.Count;
            }

            var ac = 0;
            var wa = indexes.Count - 1;

            while (wa - ac > 1)
            {
                var wj = (ac + wa) / 2;

                if (indexes[wj] <= index)
                {
                    ac = wj;
                }
                else
                {
                    wa = wj;
                }
            }

            return ac + Convert.ToInt32(indexes[ac] == index);
        }

        static List<int> Work(Input input)
        {
            var queries = input.Queries;
            var a = input.A;

            var indexesOf = new List<int>?[input.N + 1];

            for (var i = 0; i < a.Length; ++i)
            {
                (indexesOf[a[i]] ??= new List<int>()).Add(i);
            }

            var results = new List<int>();

            foreach (var query in queries)
            {
                if (indexesOf[query.X] is null)
                {
                    results.Add(item: 0);
                    continue;
                }

                var indexes = indexesOf[query.X]!;

                var left = GetLowerBound(query.Left - 1, indexes);
                var right = GetLowerBound(query.Right, indexes);

                results.Add(right - left);
            }

            return results;
        }

        class Input
        {
            public static Input FromConsole()
            {
                var n = int.Parse(Console.ReadLine()!);
                var a = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
                var q = int.Parse(Console.ReadLine()!);
                var queries = Enumerable.Range(start: 0, q)
                    .Select(_ => Query.Create(Console.ReadLine()!.Split().Select(int.Parse).ToArray()))
                    .ToArray();
                return new Input(n, a, q, queries);
            }

            public int[] A { get; }

            public int N { get; }
            public int Q { get; }
            public Query[] Queries { get; }

            Input(int n, int[] a, int q, Query[] queries)
            {
                N = n;
                A = a;
                Q = q;
                Queries = queries;
            }

            public override string ToString()
            {
                var buffer = new StringBuilder();
                buffer.Append($"{N}\n");
                buffer.Append(string.Join(" ", A)).Append("\n");
                buffer.Append($"{Q}\n");

                foreach (var query in Queries)
                {
                    buffer.Append(query.ToString()).Append("\n");
                }

                return buffer.ToString();
            }
        }

        readonly struct Query
        {
            Query(int left, int right, int x)
            {
                Left = left;
                Right = right;
                X = x;
            }

            public int Left { get; }
            public int Right { get; }
            public int X { get; }

            public override string ToString()
            {
                return $"{Left} {Right} {X}";
            }

            public static Query Create(params int[] values)
            {
                return new Query(values[0] - 1, values[1] - 1, values[2]);
            }
        }
    }
}
