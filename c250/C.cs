#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c250
{
    internal static class C
    {
        internal static void Run()
        {
            var nq = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
            var (n, q) = (nq[0], nq[1]);
            var queries = Enumerable.Range(0, q)
                .Select(_ => int.Parse(Console.ReadLine()!))
                .ToArray();

            var nodes = Enumerable.Range(1, n)
                .Select(value => new Node(value))
                .ToArray();

            for (var i = 0; i < nodes.Length; ++i)
            {
                var node = nodes[i];
                node.Left = i == 0 ? null : nodes[i - 1];
                node.Right = i == nodes.Length - 1 ? null : nodes[i + 1];
            }

            foreach (var x in queries)
            {
                var node = nodes[x - 1];

                if (node.Right != null)
                {
                    var right = node.Right;
                    var left = node.Left;
                    
                    if (right.Right != null)
                    {
                        right.Right.Left = node;
                    }

                    if (left != null)
                    {
                        left.Right = right;
                    }
                    
                    node.Right = right.Right;
                    node.Left = right;
                    right.Right = node;
                    right.Left = left;
                }
                else
                {
                    var left = node.Left;
                    var right = node.Right;

                    if (left == null)
                    {
                        throw new Exception("no left found");
                    }

                    if (left.Left != null)
                    {
                        left.Left.Right = node;
                    }
                    
                    node.Right = left;
                    node.Left = left.Left;
                    left.Left = node;
                    left.Right = right;
                }
            }

            var leftEdge = nodes.FirstOrDefault(node => node.Left == null);

            if (leftEdge == null)
            {
                throw new Exception("no left edge found");
            }

            var cursor = leftEdge;

            var values = new List<int>();

            while (cursor != null)
            {
                values.Add(cursor.Value);
                cursor = cursor.Right;
            }
            
            Console.WriteLine(string.Join(" ", values));
        }

        class Node
        {
            public Node? Left;
            public Node? Right;

            public Node(int value)
            {
                Value = value;
            }

            public int Value { get; }
        }
    }
}
