#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c243
{
    internal static class D
    {
        internal static void Run()
        {
            var nx = Console.ReadLine()!.Split().Select(long.Parse).ToArray();
            var (n, x) = (nx[0], nx[1]);
            var s = Console.ReadLine()!;

            var operations =new List<char>();
            var uCount = 0;

            for (var i = 0; i < n; ++i)
            {
                var op = s[s.Length - i - 1];

                if (op == 'U')
                {
                    uCount++;
                    continue;
                }

                if (uCount == 0)
                {
                    operations.Add(op);
                    continue;
                }

                uCount--;
            }

            operations.AddRange(Enumerable.Repeat('U', uCount));

            operations.Reverse();

            var node = x;

            foreach (var operation in operations)
            {
                switch (operation)
                {
                    case 'U':
                        node = node / 2;
                        break;

                    case 'L':
                        node = node * 2;
                        break;

                    case 'R':
                        node = node * 2 + 1;
                        break;

                    default:
                        throw new Exception("invalid operation");
                }
            }

            Console.WriteLine(node);
        }
    }
}
