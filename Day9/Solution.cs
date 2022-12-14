using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day9
{
    public static class Solution
    {
        private static Dictionary<string, (int x, int y)> directionToVectorMap = new Dictionary<string, (int x, int y)>()
        {
            { "up", (x: 0, y: -1) },
            { "left", (x: -1, y: 0) },
            { "down", (x: 0, y: 1) },
            { "right", (x: 1, y: 0) },
        };

        public static void Part1()
        {

        }

        public static void Part2()
        {

        }

        public static IEnumerable<(int x, int y)> ParseInput()
        {
            File.ReadAllLines(@"Day9\Input.txt")
                .SelectMany(line =>
                {
                    var tokens = line.Split(' ');
                    var direction = tokens[0];
                    var count = tokens[1];

                })
        }
    }

    public class Instruction
    {
        public (int x, int y) DirectionVector { get; init; }
    }
}
