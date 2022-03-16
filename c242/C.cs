#nullable enable
using System;
using System.Collections.Generic;

namespace AtCoder.c242
{
    internal static class C
    {
        const int Mod = 998244353;

        static Dictionary<Argument, long> Cache { get; } = new Dictionary<Argument, long>();
        // static Dictionary<Tuple<int, int>, long> Cache { get; } = new Dictionary<Tuple<int, int>, long>();

        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine()!);

            var answer = 0L;

            for (var digit = 1; digit < 10; ++digit)
            {
                answer = answer + (Recursive(new Argument(n - 1, digit)) % Mod);
            }

            Console.WriteLine(answer % Mod);
        }

        class Argument: IEqualityComparer<Argument>
        {
            public bool Equals(Argument x, Argument y)
            {
                return x.N == y.N && x.Digit == y.Digit;
            }

            public int GetHashCode(Argument obj)
            {
                return obj.N ^ obj.Digit;
            }

            public int N { get; }
            public int Digit { get; }

            public Argument(int n, int digit)
            {
                N = n;
                Digit = digit;
            }

            public Argument Next(int digit)
            {
                return new Argument(N - 1, digit);
            }
        }

        static long Recursive(Argument argument)
        {
            if (Cache.ContainsKey(argument))
            {
                return Cache[argument];
            }

            if (argument.N == 1)
            {
                return Cache[argument] = (argument.Digit == 1 || argument.Digit == 9) ? 2 : 3;
            }

            if (argument.Digit == 1)
            {
                return Cache[argument] = (Recursive(argument.Next(1)) % Mod)
                                         + (Recursive(argument.Next(2)) % Mod);
            }

            if (argument.Digit == 9)
            {
                return Cache[argument] = (Recursive(argument.Next(9)) % Mod)
                                         + (Recursive(argument.Next(8)) % Mod);
            }

            return Cache[argument] = (Recursive(argument.Next(argument.Digit - 1)) % Mod)
                                     + (Recursive(argument.Next(argument.Digit)) % Mod)
                                     + (Recursive(argument.Next(argument.Digit + 1)) % Mod);
        }
    }
}
