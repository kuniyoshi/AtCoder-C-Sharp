#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c226
{
    internal static class D
    {
        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine()!);
            var cities = Enumerable.Range(0, n)
                .Select(_ => Console.ReadLine()!.Split().Select(int.Parse).ToArray())
                .ToArray();
            var spells = new List<(int, int)>();

            for (var i = 0; i < cities.Length - 1; ++i)
            {
                var from = cities[i];
                for (var j = i + 1; j < cities.Length; ++j)
                {
                    var to = cities[j];
                    var deltaX = to[0] - from[0];
                    var deltaY = to[1] - from[1];

                    var isRequired = false;

                    foreach (var spell in spells)
                    {
                        var multiply = deltaX / spell.Item1;

                        if (multiply * spell.Item1 == deltaX && multiply * spell.Item2 == deltaY)
                        {
                            continue;
                        }

                        isRequired = true;
                    }

                    if (isRequired)
                    {
                        spells.Add((deltaX, deltaY));
                    }
                }
            }

            Console.WriteLine(spells.Count);
        }
    }
}
