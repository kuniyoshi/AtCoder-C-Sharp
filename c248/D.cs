#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c248
{
    internal static class D
    {
        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine()!);
            var a = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var q = int.Parse(Console.ReadLine()!);
            var queries = Enumerable.Range(start: 0, q)
                .Select(_ => Query.Create(Console.ReadLine()!.Split().Select(int.Parse).ToArray()))
                .ToArray();

            var appearance = new HashSet<int>();

            foreach (var query in queries)
            {
                appearance.Add(query.X);
            }

            var spansOf = new Dictionary<int, List<Span>>();

            for (var i = 0; i < a.Length; ++i)
            {
                if (!appearance.Contains(a[i]))
                {
                    continue;
                }

                if (!spansOf.ContainsKey(a[i]))
                {
                    spansOf.Add(a[i], new List<Span>());
                }

                spansOf[a[i]].Add(new Span(i, spansOf[a[i]].LastOrDefault().Value + 1));
            }

            foreach (var query in queries)
            {
                if (!spansOf.ContainsKey(query.X))
                {
                    Console.WriteLine(value: 0);
                    continue;
                }

                var spans = spansOf[query.X];

                if (query.Right < spans.First().Index)
                {
                    Console.WriteLine(value: 0);
                    continue;
                }

                if (query.Left > spans.Last().Index)
                {
                    Console.WriteLine(value: 0);
                    continue;
                }

                if (spans.Count == 1)
                {
                    Console.WriteLine(spans.First().Value);
                    continue;
                }

                if (query.Left <= spans.First().Index && query.Right >= spans.Last().Index)
                {
                    Console.WriteLine(spans.Last().Value - spans.First().Value + 1);
                    continue;
                }

                var left = GetLeftIndex(query, spans) ?? 0;
                var right = GetRightIndex(query, spans) ?? spans.Count - 1;

                if (left == right)
                {
                    Console.WriteLine(spans[left].Value);
                    continue;
                }

                Console.WriteLine(spans[right].Value - spans[left].Value + 1);
            }
        }

        static int? GetLeftIndex(Query query, List<Span> spans)
        {
            if (query.Left < spans.First().Index)
            {
                return null;
            }

            if (query.Left == spans.Last().Index)
            {
                return spans.Count - 1;
            }

            var ac = 0;
            var wa = spans.Count - 1;

            while (wa - ac > 1)
            {
                var wj = (ac + wa) / 2;

                if (spans[wj].Index <= query.Left)
                {
                    ac = wj;
                }
                else
                {
                    wa = wj;
                }
            }

            return ac;
        }

        static int? GetRightIndex(Query query, List<Span> spans)
        {
            if (query.Right > spans.Last().Index)
            {
                return null;
            }

            if (query.Right == spans.First().Index)
            {
                return 0;
            }

            var ac = spans.Count - 1;
            var wa = 0;

            while (ac - wa > 1)
            {
                var wj = (ac + wa) / 2;

                if (spans[wj].Index >= query.Right)
                {
                    ac = wj;
                }
                else
                {
                    wa = wj;
                }
            }

            return ac;
        }

        struct Query
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

            public static Query Create(int[] values)
            {
                return new Query(values[0] - 1, values[1] - 1, values[2]);
            }
        }

        struct Span
        {
            public Span(int index, int value)
            {
                Index = index;
                Value = value;
            }

            public int Value { get; }
            public int Index { get; }
        }
    }
}
