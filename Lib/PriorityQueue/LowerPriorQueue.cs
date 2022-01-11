#nullable enable
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AtCoder.Lib.PriorityQueue
{
    internal class LowerPriorQueue
    {
        List<int> Items { get; } = new List<int>();

        internal bool Any()
        {
            return Items.Any();
        }

        internal int Peek()
        {
            return Items[index: 0];
        }

        internal int Pop()
        {
            Debug.Assert(Items.Any(), "Items.Any()");
            return Heap.ReversePopFrom(Items);
        }

        internal void Push(int value)
        {
            Heap.ReversePushTo(Items, value);
        }

        static class Heap
        {
            internal static int ReversePopFrom(List<int> buffer)
            {
                Debug.Assert(buffer.Any(), "buffer.Any()");
                var lastRoot = buffer[index: 0];
                buffer[index: 0] = buffer[buffer.Count - 1];
                buffer.RemoveAt(buffer.Count - 1);

                var cursor = 0;
                int left;

                while ((left = 2 * cursor + 1) < buffer.Count)
                {
                    var right = left + 1;

                    var child = right < buffer.Count && buffer[left] >= buffer[right]
                        ? right
                        : left;

                    if (buffer[cursor] > buffer[child])
                    {
                        (buffer[cursor], buffer[child]) = (buffer[child], buffer[cursor]);
                    }

                    cursor = child;
                }

                return lastRoot;
            }

            internal static void ReversePushTo(List<int> buffer, int item)
            {
                buffer.Add(item);
                var cursor = buffer.Count - 1;

                while (cursor != 0)
                {
                    var parent = (cursor - 1) / 2;

                    if (buffer[parent] > buffer[cursor])
                    {
                        (buffer[parent], buffer[cursor]) = (buffer[cursor], buffer[parent]);
                    }

                    cursor = parent;
                }
            }
        }
    }
}
