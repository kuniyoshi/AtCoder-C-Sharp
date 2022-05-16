#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c251
{
    internal static class B
    {
        internal static void Run()
        {
            var nw = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var (n, w) = (nw[0], nw[1]);
            var a = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var total = new HashSet<int>();

            for (var i = 0; i < a.Length; ++i)
            {
                var ai = a[i];

                if (ai <= w)
                {
                    total.Add(ai);
                }
                
                for (var j = 0; j < i; ++j)
                {
                    var aij = ai + a[j];

                    if (aij <= w)
                    {
                        total.Add(aij);
                    }
                    
                    for (var k = 0; k < j; ++k)
                    {
                        var aijk = aij + a[k];

                        if (aijk <= w)
                        {
                            total.Add(aijk);
                        }
                    }
                }
            }
            
            Console.WriteLine(total.Count);
        }
    }
}
