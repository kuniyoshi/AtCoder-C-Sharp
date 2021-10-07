#nullable enable
using System;
using System.Diagnostics;
using System.Linq;

namespace AtCoder.c221
{
    internal static class D
    {
        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine()!);
            var days = Enumerable.Range(0, n)
                .SelectMany(_ =>
                    {
                        var startAndContinuous = Console.ReadLine()!
                            .Split()
                            .Select(int.Parse)
                            .ToArray();
                        Debug.Assert(startAndContinuous.Length == 2, $"length: {startAndContinuous.Length}");
                        return new[]
                        {
                            new Tuple<int, int>(startAndContinuous[0], 1),
                            new Tuple<int, int>(startAndContinuous[0] + startAndContinuous[1], -1),
                        };
                    }
                )
                .OrderBy(t => t.Item1)
                .ToArray();

            var counter = 0;
            var logins = new int[n];

            for (var i = 0; i < days.Length - 1; ++i)
            {
                var (day, increment) = days[i];
                counter += increment;
                Debug.Assert(counter <= n && counter >= 0, $"counter: {counter}");

                if (counter == 0)
                {
                    continue;
                }

                logins[counter - 1] += days[i + 1].Item1 - day;
            }

            Console.WriteLine(string.Join(" ", logins));
        }
    }
}
