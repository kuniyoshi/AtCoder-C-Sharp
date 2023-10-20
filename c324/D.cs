#nullable enable
using System;
using System.Linq;

namespace AtCoder.c324
{
    internal static class D
    {
        internal static bool IsSame(int[] a, int[]b)
        {
            for (var i = 0; i < 10; ++i)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }

            return true;
        }
        internal static int[] Frequency(string s)
        {
            var result = new int[10];

            foreach (var c in s)
            {
                result[c - '0']++;
            }

            return result;
        }

        internal static void Run()
        {
            var n = ReadInput.ReadSingle();
            var frequency = Frequency(ReadInput.ReadString());
            var length = frequency.Sum();
            var count = 0L;
            var upper = (long)Math.Pow(10, n);

            for (var i = 0L; i * i < upper; ++i)
            {
                var candidate = Frequency((i * i).ToString());
                candidate[0] += Math.Max(0, length - (i * i).ToString().Length);

                if (IsSame(candidate, frequency))
                {
                    count++;
                }
            }

            Console.WriteLine(count);
        }

        static class ReadInput
        {
            internal static long ReadSingle()
            {
                return long.Parse(Console.ReadLine()!);
            }

            internal static string ReadString()
            {
                return Console.ReadLine()!;
            }
        }
    }
}
