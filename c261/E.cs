#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c261
{
    internal static class E
    {
        internal static void Run()
        {
            var nc = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var (n, c) = (nc[0], nc[1]);
            var operations = Enumerable.Range(0, n)
                .Select(_ => Console.ReadLine()!.Split().Select(ulong.Parse).ToArray())
                .ToArray();

            var x = (ulong)c;
            var and = ulong.MaxValue;
            var or = (ulong)0;
            var xor = (ulong)0;

            for (var i = 0; i < n; ++i)
            {
                var (t, a) = (operations[i][0], operations[i][1]);

                switch (t)
                {
                    case 1:
                        and &= a;
                        break;
                    
                    case 2:
                        or |= a;
                        break;
                    
                    case 3:
                        xor ^= a;
                        break;
                    
                    default:
                        throw new Exception("error");
                }

                x = (x & and | or) ^ xor;

                Console.WriteLine(x);
            }
        }


    }
}
