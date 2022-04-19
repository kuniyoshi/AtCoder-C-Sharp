#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AtCoder.c248
{
    internal static class D
    {
        class Input
        {
            Input(int n, int[] a, int q, Query[] queries)
            {
                N = n;
                A = a;
                Q = q;
                Queries = queries;
            }

            public static Input GetRandom()
            {
                var nRange = 100;
                var aRange = 100;
                var qRange = 100;

                var random = new Random();

                var n = random.Next(1, nRange);
                var a = Enumerable.Range(0, n)
                    .Select(_ => random.Next(1, aRange))
                    .ToArray();
                var q = random.Next(1, qRange);
                var queries = Enumerable.Range(0, q)
                    .Select(_ =>
                        {
                            var l = random.Next(1, nRange);
                            var r = random.Next(1, nRange);
                            var x = random.Next(1, aRange);

                            if (r < l)
                            {
                                (l, r) = (r, l);
                            }

                            return Query.Create(l, r, x);
                        }
                    )
                    .ToArray();

                return new Input(n, a, q, queries);
            }

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

            public int N { get; }
            public int[] A { get; }
            public int Q { get; }
            public Query[] Queries { get; }

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

        internal static void Run()
        {
            var input = Input.FromConsole();
            var normal = Work(input);
            var honest = HonestWay(input);

            var message = input.ToString();
            Debug.Assert(honest.Count == normal.Count, message);

            for (var i = 0; i < honest.Count; ++i)
            {
                Console.WriteLine($"{i}/{honest.Count}");
                Debug.Assert(honest[i] == normal[i], message);
            }
            // RandomTest();
        }

        static void RandomTest()
        {
            for (var i = 0; i < 10000; ++i)
            {
                var input = Input.GetRandom();
                var normal = Work(input);
                var honest = HonestWay(input);

                Debug.Assert(honest.Count == normal.Count, input.ToString());

                for (var j = 0; j < honest.Count; ++j)
                {
                    Debug.Assert(honest[j] == normal[j], input.ToString());
                }
            }
        }

        static List<int> HonestWay(Input input)
        {
            var results = new List<int>();

            foreach (var query in input.Queries)
            {
                var a = input.A;
                var count = 0;

                var index = query.Left;

                while (index < query.Right + 1)
                {
                    Debug.Assert(index >= 0 && index < a.Length, $"index: {index}, left: {query.Left}, right: {query.Right}");
                    if (a[index] == query.X)
                    {
                        count++;
                    }
                }

                results.Add(count);
            }

            return results;
        }

        static List<int> Work(Input input)
        {
            var queries = input.Queries;
            var a = input.A;
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

            var results = new List<int>();

            foreach (var query in queries)
            {
                if (!spansOf.ContainsKey(query.X))
                {
                    results.Add(0);
                    continue;
                }

                var spans = spansOf[query.X];

                if (query.Right < spans.First().Index)
                {
                    results.Add(0);
                    continue;
                }

                if (query.Left > spans.Last().Index)
                {
                    results.Add(0);
                    continue;
                }

                if (spans.Count == 1)
                {
                    results.Add(spans.First().Value);
                    continue;
                }

                if (query.Left <= spans.First().Index && query.Right >= spans.Last().Index)
                {
                    results.Add(spans.Last().Value - spans.First().Value + 1);
                    continue;
                }

                var left = GetLeftIndex(query, spans) ?? 0;
                var right = GetRightIndex(query, spans) ?? spans.Count - 1;

                if (left == right)
                {
                    results.Add(spans[left].Value);
                    continue;
                }

                results.Add(spans[right].Value - spans[left].Value + 1);
            }

            return results;
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

            public override string ToString()
            {
                return $"{Left} {Right} {X}";
            }

            public static Query Create(params int[] values)
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
