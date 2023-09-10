#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c319
{
    internal static class C
    {

        static List<int> numbers = new List<int>();
        internal static void Run()
        {
            foreach (var _ in Enumerable.Range(0, 3))
            {
                foreach (var i in ReadInput.ReadArrayInt())
                {
                    numbers.Add(i);
                }

            }

            var count = Dfs(new List<int>(), new Dictionary<int, bool>());
            var whole = Kaijo(9);
            Console.WriteLine((double)(whole - count)/whole);
        }

        static int Dfs(List<int> history, Dictionary<int,bool> used)
        {
            if (history.Count == 9)
            {
                return Judge(history);
            }

            var total = 0;

            for (var i = 0; i < 9; ++i)
            {
                if (used.ContainsKey(i))
                {
                    continue;
                }

                history.Add(i);
                used[i] = true;
                total += Dfs(history, used);
                used.Remove(i);
                history.RemoveAt(history.Count - 1);
            }

            return total;
        }

        static int Judge(List<int> history)
        {
            var values = Enumerable.Range(0, 8).Select(_ => new List<int>()).ToArray();

            for (var i = 0; i < 9; ++i)
            {
                foreach (var index in IndexOf(history[i]))
                {
                    values[index].Add(numbers[history[i]]);
                }
            }

            foreach (var list in values)
            {
                if (list[0] == list[1] && list[2] != list[0])
                {
                    return 1;
                }
            }

            return 0;
        }

        static int Kaijo(int n)
        {
            var result = 1;

            while (n > 0)
            {
                result *= n--;
            }

            return result;
        }

        static int[] IndexOf(int i)
        {
            switch (i)
            {
                case 0:
                    return new int[] { 0, 3, 6 };

                case 1:
                    return new int[] { 0, 4 };

                case 2:
                    return new int[] { 0, 5, 7 };

                case 3:
                    return new int[] { 1, 3 };

                case 4:
                    return new int[] { 1, 4, 6, 7 };

                case 5:
                    return new int[] { 1, 5 };

                case 6:
                    return new int[] { 2, 3, 7 };

                case 7:
                    return new int[] { 2, 4 };

                case 8:
                    return new int[] { 2, 5, 6 };

                default:
                    throw new Exception("some");
            }
        }

        static class ReadInput
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
                var array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
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
