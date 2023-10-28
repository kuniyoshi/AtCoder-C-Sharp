#nullable enable
using System;
using System.Linq;

namespace AtCoder.c324
{
    internal static class E
    {
        internal static int[] Frequency(string s)
        {
            var result = new int[26];

            foreach (var c in s)
            {
                result[c - 'a']++;
            }

            return result;
        }

        internal static void Run()
        {
            var (n, t) = ReadInput.ReadFirst();
            var s = ReadInput.ReadS(n);

            var frequency = Frequency(t);
            var count = 0;

            foreach (var si in s)
            {

            }

            Console.WriteLine(count);
        }

        static class ReadInput
        {
            internal static (int, string) ReadFirst()
            {
                var line = Console.ReadLine()!.Split();
                return (int.Parse(line[0]), line[1]);
            }

            internal static string[] ReadS(int count)
            {
                return Enumerable.Range(0, count).Select(_ => Console.ReadLine()!).ToArray();
            }
        }
    }
}
