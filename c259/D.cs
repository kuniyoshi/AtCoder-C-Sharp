#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c259
{
    internal static class D
    {
        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine()!);
            var line = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var (sx, sy, tx, ty) = (line[0], line[1], line[2], line[3]);
            var circles = Enumerable.Range(0, n)
                .Select(_ => Console.ReadLine()!.Split().Select(int.Parse).ToArray())
                .ToArray();

            var graph = new List<int>[n];

            for (var i = 0; i < n; ++i)
            {
                for (var j = 0; j < i; ++j)
                {
                    if (j == i)
                    {
                        continue;
                    }

                    var dx = circles[i][0] - circles[j][0];
                    var dy = circles[i][1] - circles[j][1];
                    var distance = Math.Sqrt((long)dx * dx + (long)dy * dy);

                    if (distance > circles[i][2] + circles[j][2])
                    {
                        continue;
                    }

                    var isISmall = circles[i][2] < circles[j][2];

                    if (isISmall)
                    {
                        if (circles[j][2] > distance + circles[i][2])
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (circles[i][2] > distance + circles[j][2])
                        {
                            continue;
                        }
                    }

                    if (graph[i] == null)
                    {
                        graph[i] = new List<int>();
                    }
                    
                    graph[i].Add(j);

                    if (graph[j] == null)
                    {
                        graph[j] = new List<int>();
                    }
                    
                    graph[j].Add(i);
                }
            }

            var from = GetIndex(circles, n, sx, sy);
            var to = GetIndex(circles, n, tx, ty);

            var queue = new Queue<int>();
            queue.Enqueue(from);
            var found = false;
            var visited = new HashSet<int>();

            while (queue.Any())
            {
                var v = queue.Dequeue();

                if (visited.Contains(v))
                {
                    continue;
                }

                visited.Add(v);

                if (v == to)
                {
                    found = true;
                    break;
                }

                if (graph[v] == null)
                {
                    continue;
                }

                foreach (var w in graph[v])
                {
                    if (visited.Contains(w))
                    {
                        continue;
                    }
                    
                    queue.Enqueue(w);
                }
            }
            
            Console.WriteLine(found ? "Yes" : "No");
        }

        static int GetIndex(int[][] circles, int n,int x, int y)
        {
            for (var i = 0; i < n; ++i)
            {
                var circle = circles[i];
                var (x1, y1, r) = (circle[0], circle[1], circle[2]);

                var isMatch = ((x1 + r) == x && y1 == y)
                            || ((x1 - r) == x && y1 == y)
                            || (x1 == x && (y1 + r) == y)
                            || (x1 == x && (y1 - r) == y);

                if (isMatch)
                {
                    return i;
                }
            }

            throw new Exception("no circle found");
        }
    }
}
