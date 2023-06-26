#nullable enable
using System;
using System.Linq;

namespace AtCoder.c307
{
    internal struct Matrix
    {
        int Height;
        int Width;
        int[] Numbers;

        public Matrix(int height, int width, int[] numbers)
        {
            Height = height;
            Width = width;
            Numbers = numbers;
        }
    }

    internal static class C
    {
        internal static void Run()
        {
            var a = ReadMatrix();
            var b = ReadMatrix();
            var x = ReadMatrix();
        }

        static Matrix ReadMatrix()
        {
            var (h, w) = ReadInput.ReadArrayInt2();
            var numbers = Enumerable.Range(0, h).Select(_ =>
                {
                    var line = Console.ReadLine()!;
                    var bits = Enumerable.Range(0, line.Length).Select(i => line[i])
                        .Select(c => c == '#' ? 1 : 0)
                        .ToArray();
                    var result = 0;

                    for (var i = 0; i < bits.Length; ++i)
                    {
                        if (bits[bits.Length - 1 - i] > 0)
                        {
                            result += 1 << i;
                        }
                    }

                    return result;
                }
            )
                .SkipWhile(n => n == 0)
                .Reverse()
                .SkipWhile(n => n == 0)
                .Reverse()
                .ToArray();

            while (numbers.Length > 0 && numbers.All(n => (n & 1) == 0))
            {
                for (var i = 0; i < numbers.Length; ++i)
                {
                    numbers[i] >>= 1;
                }
            }

            return new Matrix(numbers.Length, w, numbers);
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
