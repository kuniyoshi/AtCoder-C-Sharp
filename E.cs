#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace AtCoder
{
    internal static class E
    {
        // // static void Main()
        // // {
        // //     var head = ReadLine()!.Split();
        // //     var n = int.Parse(head[0]);
        // //     var m = int.Parse(head[1]);
        // //
        // //     var edges = new Tuple<int, int, int>[m];
        // //
        // //     for (var i = 0; i < m; ++i)
        // //     {
        // //         var line = ReadLine()!.Split();
        // //         edges[i] = new Tuple<int, int, int>(int.Parse(line[0]) - 1, int.Parse(line[1]) - 1, int.Parse(line[2]));
        // //     }
        // //
        // //     var totalCost = SolveByPriorityQueue(edges, n);
        // //     // var totalCost = SolveByUnionFindTree(edges, n);
        // //
        // //     WriteLine(totalCost);
        // // }
        //
        // static long SolveByPriorityQueue(Tuple<int, int, int>[] edges, int n)
        // {
        //     var result = 0L;
        //
        //     var neighbors = new Neighbors();
        //
        //     foreach (var (u, v, cost) in edges)
        //     {
        //         neighbors.Add(u, v, cost);
        //         neighbors.Add(v, u, cost);
        //
        //         if (cost > 0)
        //         {
        //             result = result + cost;
        //         }
        //     }
        //
        //     var queue = new PriorityQueue();
        //     queue.Push(-1, 0);
        //     neighbors.Add(-1, 0, 0);
        //     var united = new HashSet<int>();
        //
        //     while (queue.HasItem())
        //     {
        //         var (u, currentCost) = queue.Pop();
        //
        //         if (united.Contains(u))
        //         {
        //             continue;
        //         }
        //
        //         united.Add(u);
        //
        //         if (currentCost > 0)
        //         {
        //             result = result - currentCost;
        //         }
        //
        //         foreach (var (neighbor, nextCosts) in ((Dictionary<int, List<int>>?)neighbors.List(u))!)
        //         {
        //             if (united.Contains(neighbor))
        //             {
        //                 continue;
        //             }
        //
        //             foreach (var nextCost in nextCosts)
        //             {
        //                 queue.Push(neighbor, nextCost);
        //             }
        //         }
        //     }
        //
        //     return result;
        // }
        //
        // class Neighbors
        // {
        //
        //     Dictionary<int, Dictionary<int, List<int>>> Items { get; } = new Dictionary<int, Dictionary<int, List<int>>>();
        //
        //     public void Add(int u, int v, int cost)
        //     {
        //         if (!Items.ContainsKey(u))
        //         {
        //             Items.Add(u, new Dictionary<int, List<int>>());
        //         }
        //
        //         if (!Items[u].ContainsKey(v))
        //         {
        //             Items[u].Add(v, new List<int>());
        //         }
        //
        //         Items[u][v].Add(cost);
        //     }
        //
        //     public Dictionary<int, List<int>> List(int u)
        //     {
        //         return Items[u];
        //     }
        // }
        //
        // class PriorityQueue
        // {
        //     List<(int, int)> Items { get; } = new List<(int, int)>();
        //
        //     public bool HasItem()
        //     {
        //         return Items.Any();
        //     }
        //
        //     public void Push(int v, int cost)
        //     {
        //         Heap.ReversePushTo(Items, v, cost);
        //     }
        //
        //     static class Heap
        //     {
        //         public static void ReversePushTo(List<(int, int)> to, int v, int cost)
        //         {
        //             to.Add((v, cost));
        //             var cursor = to.Count - 1;
        //
        //             while (cursor != 0)
        //             {
        //                 var parent = (cursor - 1) / 2;
        //
        //                 if (to[parent].Item2 > to[cursor].Item2)
        //                 {
        //                     (to[parent], to[cursor]) = (to[cursor], to[parent]);
        //                 }
        //
        //                 cursor = parent;
        //             }
        //         }
        //
        //         public static (int, int) ReversePopFrom(List<(int, int)> from)
        //         {
        //             var lastRoot = from[0];
        //
        //             from[0] = from[^1];
        //             from.RemoveAt(from.Count - 1);
        //
        //             var cursor = 0;
        //             int left;
        //
        //             while ((left = 2 * cursor + 1) < from.Count)
        //             {
        //                 var right = left + 1;
        //                 var child = right < from.Count && from[left].Item2 >= from[ right ].Item2
        //                     ? right
        //                     : left;
        //
        //                 if (from[ cursor ].Item2 >= from[ child ].Item2)
        //                 {
        //                     (from[cursor], from[child]) = (from[child], from[cursor]);
        //                 }
        //
        //                 cursor = child;
        //             }
        //
        //
        //             return lastRoot;
        //         }
        //     }
        //
        //     public (int, int) Pop()
        //     {
        //         return Heap.ReversePopFrom(Items);
        //     }
        // }
        //
        // static long SolveByUnionFindTree(Tuple<int, int, int>[] edges, int n)
        // {
        //     var totalCost = edges.Where(edge => edge.Item3 > 0)
        //         .Select(edge => (long)edge.Item3)
        //         .Sum();
        //
        //     var tree = new UnionFindTree(n);
        //
        //     foreach (var (u, v, cost) in edges.OrderBy(edge => edge.Item3))
        //     {
        //         if (tree.Root(u) == tree.Root(v))
        //         {
        //             continue;
        //         }
        //
        //         tree.Unite(u, v);
        //
        //         if (cost > 0)
        //         {
        //             totalCost = totalCost - cost;
        //         }
        //     }
        //
        //     return totalCost;
        // }
        //
        // class UnionFindTree
        // {
        //     int[] Parents { get; }
        //
        //     public UnionFindTree(int n)
        //     {
        //         Parents = Enumerable.Range(start: 0, n).ToArray();
        //     }
        //
        //     public int Root(int v)
        //     {
        //         var parent = Parents[v];
        //
        //         if (parent == v)
        //         {
        //             return v;
        //         }
        //
        //         return Parents[v] = Root(parent);
        //     }
        //
        //     public void Unite(int u, int v)
        //     {
        //         var (rootU, rootV) = (Root(u), Root(v));
        //
        //         if (rootU == rootV)
        //         {
        //             return;
        //         }
        //
        //         Parents[rootV] = rootU;
        //     }
        // }
    }
}
