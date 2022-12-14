using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day5
{
    public static class Solution
    {
        public static void Part1()
        {
            (var stacks, var instructions) = ParseInput();
            ProcessInstructionsP1(stacks, instructions);

            var result = stacks
                .Select(stack => stack.Peek())
                .Aggregate(string.Concat);

            Console.WriteLine("Day 5 - Part 1 - " + result);
        }
        public static void Part2()
        {
            (var stacks, var instructions) = ParseInput();
            ProcessInstructionsP2(stacks, instructions);

            var result = stacks
                .Select(stack => stack.Peek())
                .Aggregate(string.Concat);

            Console.WriteLine("Day 5 - Part 2 - " + result);
        }

        public static (List<Stack<string>> stacks, List<(int count, int from, int to)> instructions) ParseInput()
        {
            var lines = File.ReadAllLines(@"Day5\Input.txt");

            return (CreateStacks(lines), CreateInstructions(lines).ToList());
        }

        private static IEnumerable<(int count, int from, int to)> CreateInstructions(string[] lines)
        {
            var regex = new Regex(@"move (?<count>\d+) from (?<from>\d+) to (?<to>\d+)");
            var instructionsSection = lines.Skip(Array.IndexOf(lines, string.Empty) + 1).ToArray();

            return instructionsSection.Select((line) =>
            {
                var groups = regex.Match(line).Groups;
                
                var count = int.Parse(groups["count"].Value);
                var from = int.Parse(groups["from"].Value);
                var to = int.Parse(groups["to"].Value);

                return (count, from, to);
            });
        }

        public static List<Stack<string>> CreateStacks(string[] lines)
        {
            var stacksSection = lines.Take(Array.IndexOf(lines, string.Empty)).ToArray();

            var stackCount = int.Parse(stacksSection.Last()[stacksSection.Last().Length - 2].ToString());

            var stacks = new List<Stack<string>>();

            for (int i = 0; i < stackCount; i++)
            {
                stacks.Add(new Stack<string>());
            }

            var stacksData = stacksSection.Reverse().Skip(1).ToArray();

            for (int i = 0; i < stacksData.Length; i++)
            {
                var line = stacksData[i];

                for (int j = 0; j < stacks.Count; j++)
                {
                    var item = line[1 + j * 4];

                    if (item != ' ')
                    {
                        stacks[j].Push(item.ToString());
                    }
                }
            }

            return stacks;
        }

        public static void ProcessInstructionsP1(
            List<Stack<string>> stacks,
            List<(int count, int from, int to)> instructions)
        {
            foreach ((var count, var from, var to) in instructions)
            {
                stacks[to - 1].PushRange(stacks[from - 1].PopRange(count));
            }
        }

        public static void ProcessInstructionsP2(
            List<Stack<string>> stacks,
            List<(int count, int from, int to)> instructions)
        {
            foreach ((var count, var from, var to) in instructions)
            {
                stacks[to - 1].PushRange(stacks[from - 1].PopRange(count).Reverse());
            }
        }
    }

    public static class Extensions
    {
        public static IEnumerable<T> PopRange<T>(this Stack<T> stack, int amount)
        {
            while (amount-- > 0)
            {
                yield return stack.Pop();
            }
        }

        public static void PushRange<T>(this Stack<T> stack, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                stack.Push(item);
            }
        }
    }
}
