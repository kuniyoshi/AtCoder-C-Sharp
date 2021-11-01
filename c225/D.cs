#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c225
{
    internal static class D
    {
        internal static void Run()
        {
            var (n, q) = Read2();
            var queries = Enumerable.Range(start: 0, q)
                .Select(_ => Console.ReadLine()!.Split().Select(int.Parse).ToArray());

            var trains = new int[n + 1, 2];

            foreach (var query in queries)
            {
                switch (query[0])
                {
                    case 1:
                        Op1(trains, query[1], query[2]);
                        break;

                    case 2:
                        Op2(trains, query[1], query[2]);
                        break;

                    case 3:
                        Op3(trains, query[1]);
                        break;
                }
            }
        }

        static void Op1(int[,] trains, int x, int y)
        {
            trains[x, 1] = y;
            trains[y, 0] = x;
        }

        static void Op2(int[,] trains, int x, int y)
        {
            trains[x, 1] = 0;
            trains[y, 0] = 0;
        }

        static void Op3(int[,] trains, int x)
        {
            var backs = new List<int>();
            var cursor = x;

            while (trains[cursor, 1] != 0)
            {
                backs.Add(trains[cursor, 1]);
                cursor = trains[cursor, 1];
            }

            var fronts = new List<int>();
            cursor = x;

            while (trains[cursor, 0] != 0)
            {
                fronts.Add(trains[cursor, 0]);
                cursor = trains[cursor, 0];
            }

            var result = new List<int>();
            result.AddRange(fronts.ToArray().Reverse());
            result.Add(x);
            result.AddRange(backs);

            Console.WriteLine(string.Join(" ", new[] { result.Count }.Concat(result)));
        }

        static (int, int) Read2()
        {
            var numbers = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            return (numbers[0], numbers[1]);
        }
    }
}
