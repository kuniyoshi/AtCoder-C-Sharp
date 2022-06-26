#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.c257
{
    internal static class C
    {
        internal static void Run()
        {
            var n = int.Parse(Console.ReadLine()!);
            var s = Console.ReadLine()!;
            var w = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

            var people = Enumerable.Range(0, n)
                .Select(index => new Person(s[index] == '0', w[index]))
                .OrderBy(p => p.Weight)
                .ThenBy(p => !p.IsChild)
                .ToArray();

            if (people.All(person => person.IsChild))
            {
                Console.WriteLine(n);
                return;
            }

            var childCount = new List<int> { 0 };

            foreach (var person in people)
            {
                childCount.Add(childCount.Last() + Convert.ToInt32(person.IsChild));
            }

            var manCount = new List<int> { 0 };

            foreach (var person in people)
            {
                manCount.Add(manCount.Last() + Convert.ToInt32(!person.IsChild));
            }

            for (var i = 0; i < people.Length; ++i)
            {
                // Console.WriteLine($"[{i}]: {F(manCount, childCount, i)}");
            }

            var max = F(manCount, childCount, 0);
            var previous = people.First();

            for (var index = 0; index < people.Length; index++)
            {
                var person = people[index];

                if (person.Weight == previous.Weight)
                {
                    continue;
                }

                previous = person;
                max = Math.Max(max, F(manCount, childCount, index));
            }

            Console.WriteLine(max);
        }

        static int F(List<int> manCount, List<int> childCount, int index)
        {
            var c = childCount[index];
            var m = manCount.Last() - manCount[index];
            return c + m;
        }

        struct Person
        {
            public Person(bool isChild, int weight)
            {
                IsChild = isChild;
                Weight = weight;
            }

            public bool IsChild { get; }
            public int Weight { get; }
        }
    }
}
