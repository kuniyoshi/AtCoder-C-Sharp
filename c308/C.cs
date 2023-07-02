#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c308
{
    internal static class C
    {

        struct MyStruct
        {
            internal int Index;
            internal long[] Ab;

            internal MyStruct(int index, long[] ab)
            {
                Index = index;
                Ab = ab;
            }
        }

        class MyClass: IComparer<MyStruct>
        {
            public int Compare(MyStruct a, MyStruct b)
            {
                var comparison =  b.Ab[0] * (a.Ab[0] + a.Ab[1]) - (a.Ab[0] * (b.Ab[0] + b.Ab[1]));
                return comparison < 0 ? -1 : comparison > 0 ? 1 : 0;
            }
        }

        internal static void Run()
        {
            var n = ReadInput.ReadSingle();
            var ab = ReadInput.ReadArrayArrayLong(n);

            var tuples = Enumerable.Range(0, n)
                .Select(index => new MyStruct(index, ab[index]))
                .OrderBy(s => s, new MyClass());

            Console.WriteLine(string.Join(" ", tuples.Select(s => (s.Index + 1).ToString())));
        }



        static int Compare(Tuple<int, long[]> a, Tuple<int, long[]> b)
        {
            return (b.Item2[0] * (a.Item2[0] + a.Item2[1]))
                .CompareTo(a.Item2[0] * (b.Item2[0] + b.Item2[1]));
        }

        static T[] Merge<T>(T[] left, T[] right, Func<T, T, int> comparison)
        {
            var result = new List<T>();
            var leftIndex = 0;
            var rightIndex = 0;

            while (leftIndex < left.Length && rightIndex < right.Length)
            {
                if (comparison(left[leftIndex], right[rightIndex]) <= 0)
                {
                    result.Add(left[leftIndex]);
                    leftIndex++;
                }
                else
                {
                    result.Add(right[rightIndex]);
                    rightIndex++;
                }
            }

            while (leftIndex < left.Length)
            {
                result.Add(left[leftIndex]);
                leftIndex++;
            }

            while (rightIndex < right.Length)
            {
                result.Add(right[rightIndex]);
                rightIndex++;
            }

            return result.ToArray();
        }

        static T[] MergeSort<T>(T[] array, Func<T, T, int> comparison)
        {
            if (array.Length <= 1)
            {
                return array;
            }

            var middle = array.Length / 2;
            var left = array.Take(middle).ToArray();
            var right = array.Skip(middle).ToArray();

            return Merge(MergeSort(left, comparison), MergeSort(right, comparison), comparison);
        }

        static class ReadInput
        {
            internal static long[][] ReadArrayArrayLong(int n)
            {
                return Enumerable.Range(0, n)
                    .Select(_ => ReadArrayLong())
                    .ToArray();
            }

            internal static int ReadSingle()
            {
                return int.Parse(Console.ReadLine()!);
            }

            static long[] ReadArrayLong()
            {
                return Console.ReadLine()!.Split().Select(long.Parse).ToArray();
            }
        }
    }
}
