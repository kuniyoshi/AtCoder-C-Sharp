#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c310
{
    internal static class D
    {
        static Dictionary<string, int> _cache = new Dictionary<string, int>();

        internal static void Run()
        {
            var (n, t, m) = ReadInput.ReadArrayInt3();
            var pairs = ReadInput.ReadArrayArrayInt(m);

            var bad = new Dictionary<int, Dictionary<int, bool>>();

            foreach (var pair in pairs)
            {
                if (!bad.ContainsKey(pair[0]))
                {
                    bad.Add(pair[0], new Dictionary<int, bool>());
                }
                if (!bad.ContainsKey(pair[1]))
                {
                    bad.Add(pair[1], new Dictionary<int, bool>());
                }

                bad[pair[0]][pair[1]] = true;
                bad[pair[1]][pair[0]] = true;
            }

            var teams = Enumerable.Range(0, t).Select(_ => new HashSet<int>()).ToArray();
            teams[0].Add(1);

            if (t > 1)
            {
                Console.WriteLine(Dfs(teams, 2, n, bad) / Permutation(t - 1 ));
            }
            else
            {
                Console.WriteLine(Dfs(teams, 1, n, bad) / Permutation(t));
            }

        }

        static string CreateKey(HashSet<int>[] teams)
        {
            return string.Join(":", teams.OrderBy(t => t.Count).Select(t => string.Join(",", t.OrderBy(v => v))));
        }

        static int Permutation(int t)
        {
            return Enumerable.Range(1, t).Aggregate((acc, n) => acc * n);
        }

        static int Dfs(HashSet<int>[] teams, int who, int n, Dictionary<int,Dictionary<int,bool>> bad)
        {
            if (who > n)
            {
                var v = teams.All(t => t.Count > 0) ? 1 : 0;

                return v;
            }

            var key = CreateKey(teams);

            if (_cache.ContainsKey(key))
            {
                return _cache[key];
            }

            var total = 0;

            for (var i = 0; i < teams.Length; ++i)
            {
                if (bad.ContainsKey(who) && teams[i].Any(t => bad[who].ContainsKey(t)))
                {
                    continue;
                }

                teams[i].Add(who);

                total += Dfs(teams, who + 1, n, bad);

                teams[i].Remove(who);
            }

            return _cache[key] = total;

            return total;
        }

        static class ReadInput
        {
            static int[] ReadArrayInt()
            {
                return Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            }

            internal static (int, int, int) ReadArrayInt3()
            {
                var array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
                return (array[0], array[1], array[2]);
            }

            internal static int[][] ReadArrayArrayInt(int n)
            {
                return Enumerable.Range(0, n)
                    .Select(_ => ReadArrayInt())
                    .ToArray();
            }
        }

    }
}
