using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day3
{
    public static class Solution
    {
        public static void Part1()
        {
            var result = File.ReadAllLines(@"Day3\input.txt")
                .Select(r =>
                {
                    var c1 = r.Substring(0, r.Length / 2);
                    var c2 = r.Substring(r.Length / 2);

                    var set = c1.ToHashSet();
                    var item = c2.Intersect(c1).Single();
                    var priority = GetPriority(item);

                    return priority;
                })
                .Sum();


            Console.WriteLine("Day 3 - Part 1 - " + result);
        }

        public static void Part2()
        {
            var lines = File.ReadAllLines(@"Day3\input.txt");
            var input = new List<List<string>>();

            for (int i = 0; i < lines.Length / 3; i++)
            {
                var group = new List<string>();
                input.Add(group);

                for (int j = i * 3; j < i * 3 + 3; j++)
                {
                    group.Add(lines[j]);
                }
            }

            var result = input.Select(i =>
                i.Aggregate((a, b) =>
                    new string(a.Intersect(b).ToArray())))
                .Select(i => GetPriority(i.Single()))
                .Sum();

            Console.WriteLine("Day 3 - Part 2 - " + result);
        }

        public static int GetPriority(char item) => char.IsLower(item) ? item - 96 : item - 64 + 26;
    }
}
