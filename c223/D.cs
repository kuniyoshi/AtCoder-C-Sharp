#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c223
{
    internal static class D
    {
        internal static void Run()
        {
            var heads= Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var n = heads[0];
            var m = heads[1];
            var bodies = Enumerable.Range(0, m)
                .Select(_ => Console.ReadLine()!.Split().Select(int.Parse).ToArray())
                .ToArray();
            var a = bodies.Select(body => body[0]).ToArray();
            var b = bodies.Select(body => body[1]).ToArray();

            var rules = bodies.Select(body => new Rule(body))
                .ToArray();
            var book = new Book(rules);

            var result = Enumerable.Range(1, m + 1)
                .ToList();
            result.Sort(book.GetOrder);

            if (book.IsInvalid(result))
            {
                Console.WriteLine(-1);
                return;
            }

            Console.WriteLine(string.Join(" ", result));
        }

        class Book
        {
            Rule[] Rules { get; }

            public Book(Rule[] rules)
            {
                Rules = rules;
            }

            public bool IsInvalid(List<int> result)
            {
                foreach (var rule in Rules)
                {
                    var indexA = result.IndexOf(rule._min);
                    var indexB = result.IndexOf(rule._max);

                    if (indexB < indexA)
                    {
                        return true;
                    }
                }

                return false;
            }

            public int GetOrder(int a, int b)
            {
                foreach (var rule in Rules)
                {
                    if (rule.IsMatch(a, b))
                    {
                        return rule.Compare(a, b);
                    }
                }

                return 0;
            }
        }

        class Rule
        {
            public int _min;
            public int _max;

            public bool IsMatch(int a, int b)
            {
                return (_min == a && _max == b)
                       || (_max == a && _min == b);
            }

            public Rule(int[] ab)
            {
                _min = ab[0];
                _max = ab[1];
            }

            public int Compare(int a, int b)
            {
                return a == _min ? -1 : 1;
            }
        }
    }
}
