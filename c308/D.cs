#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c308
{
    internal static class D
    {
        struct Size
        {
            public int H;
            public int W;

            public Size(int h, int w)
            {
                H = h;
                W = w;
            }
        }

        internal static void Run()
        {
            var (h, w) = ReadInput.ReadArrayInt2();
            var s = Enumerable.Range(0, h).Select(_ => Console.ReadLine()!).ToArray();

            if (!s[0].StartsWith("s"))
            {
                Console.WriteLine("No");
                return;
            }

            var visited = new Dictionary<int, HashSet<int>>();

            foreach (var i in Enumerable.Range(0, h))
            {
                visited.Add(i, new HashSet<int>());
            }

            Console.WriteLine(Dfs(s, 0, 0, 0, new Size(h, w), visited) ? "Yes" : "No");
        }

        static bool Dfs(string[] s, int h, int w, int index, Size size,
                        Dictionary<int, HashSet<int>> visited)
        {
            if (h == (size.H - 1) && w == (size.W - 1))
            {
                return true;
            }

            var snuke = "snuke";

            visited[h].Add(w);

            if ( h + 1 < size.H && s[h + 1][w] == snuke[(index + 1) % 5] && !visited[h+1].Contains(w))
            {
                if (Dfs(s, h + 1, w, index + 1, size, visited))
                {
                    return true;
                }
            }

            if (h - 1 >= 0 && s[h - 1][w] == snuke[(index + 1) % 5]&&!visited[h-1].Contains(w))
            {
                if (Dfs(s, h - 1, w, index + 1, size, visited))
                {
                    return true;
                }
            }

            if (w + 1 < size.W && s[h][w + 1] == snuke[(index + 1) % 5]&&!visited[h].Contains(w+1))
            {
                if (Dfs(s, h, w + 1, index + 1, size, visited))
                {
                    return true;
                }
            }

            if (w - 1 >= 0 && s[h][w - 1] == snuke[(index + 1) % 5]&&!visited[h].Contains(w-1))
            {
                if (Dfs(s, h, w - 1, index + 1, size, visited))
                {
                    return true;
                }
            }

            visited[h].Remove(w);
            return false;
        }

        internal static class ReadInput
        {
            internal static int ReadSingle()
            {
                return int.Parse(Console.ReadLine()!);
            }

            internal static int[] ReadArrayInt()
            {
                return Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            }

            internal static (int, int, int, int) ReadArrayInt4()
            {
                var array = Console.ReadLine()!.Split((char)0).Select(int.Parse).ToArray();
                return (array[0], array[1], array[2], array[3]);
            }

            internal static (int, int) ReadArrayInt2()
            {
                var array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
                return (array[0], array[1]);
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
