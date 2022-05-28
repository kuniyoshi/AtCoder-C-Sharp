#nullable enable
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AtCoder.Lib.PriorityQueue
{
    internal class HigherPriorQueue
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
            return Heap.PopFrom(Items);
        }

        internal void Push(int value)
        {
            Heap.PushTo(Items, value);
        }
    }
}
