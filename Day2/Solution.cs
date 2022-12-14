using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day2
{
    public enum HandShape
    {
        Rock = 1,
        Paper = 2,
        Scisors = 3
    }
    public static class Solution
    {

        public static Dictionary<HandShape, Dictionary<HandShape, int>> Rules = new Dictionary<HandShape, Dictionary<HandShape, int>>()
        {
            {
                HandShape.Rock, new Dictionary<HandShape, int>()
                {
                    { HandShape.Rock, 3 },
                    { HandShape.Paper, 0 },
                    { HandShape.Scisors, 6 }
                }
            },
            {
                HandShape.Paper, new Dictionary<HandShape, int>()
                {
                    { HandShape.Rock, 6 },
                    { HandShape.Paper, 3 },
                    { HandShape.Scisors, 0 }
                }
            },
            {
                HandShape.Scisors, new Dictionary<HandShape, int>()
                {
                    { HandShape.Rock, 0 },
                    { HandShape.Paper, 6 },
                    { HandShape.Scisors, 3 }
                }
            }
        };

        public static Dictionary<string, HandShape> charToShapeMap = new Dictionary<string, HandShape>()
        {
            { "A", HandShape.Rock },
            { "X", HandShape.Rock },
            { "B", HandShape.Paper },
            { "Y", HandShape.Paper },
            { "C", HandShape.Scisors },
            { "Z", HandShape.Scisors }
        };

        public static void Part1()
        {
            var input = ParseInputP1();

            var score = input
                .Select(round => GetRoundScore(round.other, round.my))
                .Sum();

            Console.WriteLine("Day 2 - Part 1 - " + score);
        }

        public static void Part2()
        {
            var score =
                ParseInputP2()
                .Select(round => GetRoundScore(round.other, round.my))
                .Sum();

            Console.WriteLine("Day 2 - Part 2 - " + score);
        }

        public static IEnumerable<(HandShape other, HandShape my)> ParseInputP1()
        {
            return File.ReadLines(@"Day2\Input.txt")
                .Select((line) =>
                {
                    var tokens = line.Split(' ');
                    return (charToShapeMap[tokens[0]], charToShapeMap[tokens[1]]);
                });
        }

        public static IEnumerable<(HandShape other, HandShape my)> ParseInputP2()
        {
            return File.ReadLines(@"Day2\input.txt")
                .Select((line) =>
                {
                    var tokens = line.Split(' ');

                    HandShape myShape = default;
                    HandShape otherShape = charToShapeMap[tokens[0]];

                    var targetScore = tokens[1] == "X" ? 0 : (tokens[1] == "Y" ? 3 : 6);

                    myShape = Rules.First(r => r.Value[otherShape] == targetScore).Key;

                    return (otherShape, myShape);
                });
        }

        public static int GetRoundScore(HandShape other, HandShape my)
        {
            return (int)my + Rules[my][other];
        }
    }
}
