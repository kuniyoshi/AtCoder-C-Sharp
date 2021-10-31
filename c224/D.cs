#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AtCoder.c224
{
    internal static class D
    {
        // internal static void Run()
        // {
        //     var m = int.Parse(Console.ReadLine()!);
        //     var edges = Enumerable.Range(0, m)
        //         .Select(_ => Console.ReadLine()!.Split().Select(int.Parse).ToArray())
        //         .ToArray();
        //     var p = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
        //
        //     var link = CreateLink(edges);
        //     var pieces = GetPieces(p);
        //
        //     var t = new State(0, 1, 2, 3, 4, 5, 6, 7);
        //
        //     if (!link.ContainsKey(pieces))
        //     {
        //         Console.WriteLine(IsGoal(p) ? "0" : "-1");
        //
        //         return;
        //     }
        //
        //     var queue = new Queue<int>(link[pieces].Keys);
        //
        //     var states = new List<State>();
        //
        //
        //     Console.WriteLine(count);
        // }
        //
        // static bool IsGoal(int[] p)
        // {
        //     for (var i = 0; i < 8; ++i)
        //     {
        //         if (p[i] != i + 1)
        //         {
        //             return false;
        //         }
        //     }
        //
        //     return true;
        // }
        //
        // static int[,] CreateLink(int[][] edges)
        // {
        //     var link = new int[9, 9];
        //
        //     foreach (var edge in edges)
        //     {
        //         var (from, to) = (edge[0]-1, edge[1]-1);
        //         link[from, to]++;
        //         link[to, from]++;
        //     }
        //
        //     return link;
        // }
        //
        // static int[] GetPieces(int[] p)
        // {
        //     var pieces = new int[8];
        //
        //     for (var i = 0; i < 9; ++i)
        //     {
        //         pieces[]
        //     }
        //     var x = new HashSet<int>(p);
        //     var empty = Enumerable.Range(1, 9).First(y => !x.Contains(y));
        //     return empty;
        // }
        //
        // readonly struct State
        // {
        //     int P1 { get; }
        //     int P2 { get; }
        //     int P3 { get; }
        //     int P4 { get; }
        //     int P5 { get; }
        //     int P6 { get; }
        //     int P7 { get; }
        //     int P8 { get; }
        //
        //     public State(int p1,
        //                  int p2,
        //                  int p3,
        //                  int p4,
        //                  int p5,
        //                  int p6,
        //                  int p7,
        //                  int p8)
        //     {
        //         P1 = p1;
        //         P2 = p2;
        //         P3 = p3;
        //         P4 = p4;
        //         P5 = p5;
        //         P6 = p6;
        //         P7 = p7;
        //         P8 = p8;
        //     }
        // }
    }
}
