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
            return Items.Count > 0;
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
    }
}
