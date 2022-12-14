using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day4
{
    public static class Solution
    {
        public static void Part1()
        {
            var result = ParseInput()
                .Count(pair =>
                    ContainsRange(pair.first, pair.second)
                    || ContainsRange(pair.second, pair.first));

            Console.WriteLine("Day 4 - Part 1 - " + result);
        }

        public static void Part2()
        {
            var result = ParseInput()
                .Count(pair => Overlaps(pair.first, pair.second));

            Console.WriteLine("Day 4 - Part 2 - " + result);
        }

        public static IEnumerable<((int start, int end) first, (int start, int end) second)> ParseInput()
        {
            return File.ReadAllLines(@"Day4\input.txt")
                .Select((line) =>
                {
                    var pair = line
                        .Split(',')
                        .Select(range => (start: int.Parse(range.Split("-")[0]), end: int.Parse(range.Split("-")[1])));

                    return (pair.ElementAt(0), pair.ElementAt(1));
                });
        }

        public static bool ContainsRange((int start, int end) first, (int start, int end) second)
        {
            return second.start >= first.start && second.end <= first.end;
        }

        public static bool Overlaps((int start, int end) first, (int start, int end) second)
        {
            return second.start <= first.end && second.end >= first.start;
        }
    }
}
