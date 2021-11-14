#nullable enable
using System;
using System.Collections.Generic;

namespace AtCoder.c227
{
    internal static class C
    {
        internal static void Run()
        {
            var n = long.Parse(Console.ReadLine()!);
            var primes = new List<long>();
            var furui = new bool[((long)Math.Sqrt(n)) + 2];

            if (furui.Length < 2)
            {
                Console.WriteLine(1);
                return;
            }

            for (var i = 0; i < furui.Length; ++i)
            {
                furui[i] = true;
            }

            furui[1] = false;

            for (var i = 2; i < furui.Length; ++i)
            {
                if (furui[i])
                {
                    if ((n % i) == 0)
                    {
                        primes.Add(i);
                    }

                    for (var j = 2 * i; j < furui.Length; j += i)
                    {
                        furui[j] = false;
                    }
                }
            }

            Console.WriteLine(string.Join(", ", primes));
            Console.WriteLine(primes.Count);

            // var count = new long[primes.Count];
            var items = 0L;

            for (var i = 0; i < primes.Count; ++i)
            {
                var x = n;

                while ( x % primes[i] ==0)
                {
                    x /= primes[i];
                    items++;
                }
                // count[i] = n / primes[i];
                // items += n / primes[i];
            }

            var a = items;
            var b = items - 1;

            if (a % 2 == 0)
            {
                a /= 2;
            }
            else
            {
                b /= 2;
            }

            Console.WriteLine(a * b);
        }
    }
}
