#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c220
{
    internal static class D
    {
        internal static void Run()
        {
            var _ = int.Parse(Console.ReadLine()!);
            var a = new Queue<int>(Console.ReadLine()!.Split().Select(int.Parse));

            var patterns = new int[10];
            patterns[a.Dequeue()]++;
            var nexts = new int[10];

            while (a.Any())
            {
                var y = a.Dequeue();

                for (var i = 0; i < nexts.Length; ++i)
                {
                    nexts[i] = 0;
                }

                for (var i = 0; i < patterns.Length; ++i)
                {
                    if (patterns[i] == 0)
                    {
                        continue;
                    }

                    var x = i;
                    var f = (x + y) % 10;
                    var g = (x * y) % 10;
                    nexts[f] = nexts[f] + patterns[i];
                    nexts[g] = nexts[g] + patterns[i];
                }

                for (var i = 0; i < 10; ++i)
                {
                    patterns[i] = nexts[i];
                }
            }

            foreach (var pattern in patterns)
            {
                Console.WriteLine(pattern % 998244353);
            }
        }

        internal static void Slow()
        {
            var n = int.Parse(Console.ReadLine()!);
            var a = new Queue<int>(Console.ReadLine()!.Split().Select(int.Parse));

            var operationsLists = ListOperations(n - 1);

            var results = new int[10];

            foreach (var operations in operationsLists)
            {
                var aa = new Queue<int>(a);
                var p = aa.Dequeue();

                foreach (var operation in operations)
                {
                    var q = operation switch
                    {
                        0 => (p + aa.Dequeue()) % 10,
                        1 => (p * aa.Dequeue()) % 10,
                        _ => throw new ArgumentOutOfRangeException(),
                    };

                    p = q;
                }

                results[p]++;
            }

            for (var i = 0; i < results.Length; ++i)
            {
                Console.WriteLine(results[i]);
            }
        }

        static List<List<int>> ListOperations(int n)
        {
            var results = new List<List<int>>();

            var queue = new Queue<int>();
            var operations = new List<int>();

            queue.Enqueue(0);
            queue.Enqueue(1);

            Impl(n, queue, operations, results);

            return results;
        }

        static void Impl(int n, Queue<int> queue, List<int> operations, List<List<int>> results)
        {
            if (n == 0)
            {
                results.Add(operations);
                return;
            }

            while (queue.Any())
            {
                var o = new List<int>(operations) { queue.Dequeue() };
                var q = new Queue<int>();
                q.Enqueue(0);
                q.Enqueue(1);

                Impl(n - 1, q, o.ToList(), results);
            }
        }
    }
}
