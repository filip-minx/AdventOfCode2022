using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day6
{
    public static class Solution
    {
        public static void Part1()
        {
            var input = File.ReadAllText(@"Day6\Input.txt");

            var result = GetOrderOfFirstUniqSequence(input, 4);

            Console.WriteLine("Day 6 - Part 1 - " + result);
        }

        public static void Part2()
        {
            var input = File.ReadAllText(@"Day6\Input.txt");

            var result =  GetOrderOfFirstUniqSequence(input, 14);

            Console.WriteLine("Day 6 - Part 2 - " + result);
        }

        public static int GetOrderOfFirstUniqSequence(string sequence, int sequenceLength)
        {
            for (int i = 0; i < sequence.Length - 4; i++)
            {
                var set = sequence.Skip(i).Take(sequenceLength).ToHashSet();

                if (set.Count == sequenceLength)
                {
                    return i + sequenceLength;
                }
            }

            throw new InvalidOperationException();
        }
    }
}
