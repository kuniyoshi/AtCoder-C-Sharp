#nullable enable
using System;
using System.Collections.Generic;

namespace AtCoder.c324
{
    internal static class D
    {
        internal static void Run()
        {
            var n = ReadInput.ReadSingle();
            var s = ReadInput.ReadSingle2();
            var frequency = Frequency2(s);
            var count = 0L;
            var upper = (long)Math.Pow(10, n);

            for (var i = 0L; i * i < upper; ++i)
            {
                var candidate = Frequency(i * i);
                // var candidate = Frequency2((i * i).ToString());

                candidate[0] += Math.Max(n - (i * i).ToString().Length, 0);

                if (IsSame(candidate, frequency))
                {
                    count++;
                }
            }

            Console.WriteLine(count);
        }

        static bool IsSame(Dictionary<long, long> a, Dictionary<long, long> b)
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

        static Dictionary<long, long> Frequency2(string value)
        {
            var result = new Dictionary<long, long>();

            for (var i = 0; i < 10; ++i)
            {
                result[i] = 0;
            }

            foreach (var n in value)
            {
                result[long.Parse(n.ToString())]++;
            }

            return result;
        }

        static Dictionary<long, long> Frequency(long value)
        {
            var result = new Dictionary<long, long>();

            for (var i = 0; i < 10; ++i)
            {
                result[i] = 0;
            }

            if (value == 0)
            {
                result[0] = 1;
                return result;
            }

            while (value > 0)
            {
                result[value % 10]++;
                value /= 10;
            }

            return result;
        }

        static class ReadInput
        {
            internal static long ReadSingle()
            {
                return long.Parse(Console.ReadLine()!);
            }

            internal static string ReadSingle2()
            {
                return Console.ReadLine()!;
            }
        }
    }
}
