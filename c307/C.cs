#nullable enable
using System;
using System.Linq;

namespace AtCoder.c307
{
    internal struct Matrix
    {
        public readonly int Height;
        public readonly int Width;
        public readonly int[] Numbers;

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

            for (var ah = 0; ah < x.Height; ++ah)
            {
                for (var aw = 0; aw < x.Width; ++aw)
                {
                    for (var bh = 0; bh < x.Height; ++bh)
                    {
                        for (var bw = 0; bw < x.Width; ++bw)
                        {
                            if (Can(ah, aw, bh, bw, a, b, x))
                            {
                                Console.WriteLine("Yes");
                                return;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("No");
        }

        static bool Can(int ah, int aw, int bh, int bw, Matrix a,
                        Matrix b, Matrix x)
        {
            if (ah + a.Height > x.Height || aw + a.Width > x.Width)
            {
                return false;
            }

            if (bh + b.Height > x.Height || bw + b.Width > x.Width)
            {
                return false;
            }

            var numbersA = Enumerable.Repeat(0, ah)
                .Concat(a.Numbers.Select(n => n << (x.Width - (aw + a.Width))))
                .Concat(Enumerable.Repeat(0, x.Height - (ah + a.Height)))
                .ToArray();
            var numbersB = Enumerable.Repeat(0, bh)
                .Concat(b.Numbers.Select(n => n << (x.Width - (bw + b.Width))))
                .Concat(Enumerable.Repeat(0, x.Height - (bh + b.Height))).ToArray();
            var numbersX = Enumerable.Range(0, x.Height)
                .Select(i => numbersA[i] | numbersB[i]).ToArray();

            return Enumerable.Range(0, x.Height).All(i => x.Numbers[i] == numbersX[i]);
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

            while (numbers.Any(n => n > 0) && numbers.All(n => (n & 1) == 0))
            {
                for (var i = 0; i < numbers.Length; ++i)
                {
                    numbers[i] >>= 1;
                }
            }

            return new Matrix(numbers.Length, numbers.Max(n => Convert.ToString(n, 2).Length), numbers);
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
