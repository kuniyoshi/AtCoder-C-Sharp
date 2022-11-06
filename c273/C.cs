#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c273
{
    internal static class C
    {
        internal static void Run()
        {
            var n = ReadInput.ReadSingle();
            var a = ReadInput.ReadArrayInt();

            var numbers = new List<Tuple<int, int>>();

            foreach (var value in a.OrderBy(v => v))
            {
                if (!numbers.Any() || numbers.Last().Item1 != value)
                {
                    numbers.Add(Tuple.Create(value, 1));
                }
                else
                {
                    numbers[numbers.Count - 1] = Tuple.Create(numbers.Last().Item1, numbers.Last().Item2 + 1);
                }
            }

            var count = new Dictionary<int, int>();

            for (var i = 0; i < numbers.Count; ++i)
            {
                var kinds = numbers.Count - 1 - i;
                count[kinds] = numbers[i].Item2;
            }

            for (var i = 0; i < n; ++i)
            {
                Console.WriteLine(count.ContainsKey(i) ? count[i] : 0);
            }
        }

        static class ReadInput
        {
            internal static int ReadSingle()
            {
                return int.Parse(Console.ReadLine()!);
            }

            internal static int[] ReadArrayInt()
            {
                return Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            }
        }

    }
}
