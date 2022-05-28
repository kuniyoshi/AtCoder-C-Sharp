#nullable enable
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AtCoder.Lib.PriorityQueue
{
    internal static class Heap
    {
        internal static int PopFrom(List<int> buffer)
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

                var child = right < buffer.Count && buffer[left] <= buffer[right]
                    ? right
                    : left;

                if (buffer[cursor] < buffer[child])
                {
                    (buffer[cursor], buffer[child]) = (buffer[child], buffer[cursor]);
                }

                cursor = child;
            }

            return lastRoot;
        }

        internal static void PushTo(List<int> buffer, int item)
        {
            buffer.Add(item);
            var cursor = buffer.Count - 1;

            while (cursor != 0)
            {
                var parent = (cursor - 1) / 2;

                if (buffer[parent] < buffer[cursor])
                {
                    (buffer[parent], buffer[cursor]) = (buffer[cursor], buffer[parent]);
                }

                cursor = parent;
            }
        }

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
