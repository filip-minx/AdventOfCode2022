using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day8
{
    public static class Solution
    {
        public static List<(int x, int y)> DirectionVectors = new List<(int x, int y)>()
        {
            (0, -1),
            (-1, 0),
            (0, 1),
            (1, 0)
        };

        public static void Part1()
        {
            var map = ParseInput();

            var result = CountVisibleTrees(map);

            Console.WriteLine("Day 8 - Part 1 - " + result);
        }

        public static void Part2()
        {
            var map = ParseInput();
            
            var result = GetHighestScenicScore(map);

            Console.WriteLine("Day 8 - Part 2 - " + result);
        }

        public static int[,] ParseInput()
        {
            var lines = File.ReadAllLines(@"Day8\Input.txt");

            var map = new int[lines[0].Length, lines.Length];

            for (int y = 0; y < lines.Length; y++)
            {
                var line = lines[y];

                for (int x = 0; x < line.Length; x++)
                {
                    map[x, y] = int.Parse(line[x].ToString());
                }
            }

            return map;
        }

        public static int CountVisibleTrees(int[,] map)
        {
            return EnumerateMap(map)
                .Where(i => IsTreeVisible(i.x, i.y, map))
                .Count();
        }

        public static bool IsTreeVisible(int x, int y, int[,] map)
        {
            return DirectionVectors
                .Select(dir => IsDirectionVisible(x, y, map, dir))
                .Aggregate((a, b) => a || b);
        }

        public static bool IsDirectionVisible(int x, int y, int[,] map, (int x, int y) directionVector)
        {
            var dimension = map.GetLength(0);

            if (IsDirectionOutOfBounds(x, y, dimension, directionVector))
            {
                return true;
            }

            var currentHeight = map[x, y];

            var cx = x;
            var cy = y;

            while (cx > 0 && cx < dimension - 1 && cy > 0 && cy < dimension - 1)
            {
                cx = cx + directionVector.x;
                cy = cy + directionVector.y;

                var nextHeight = map[cx, cy];

                if (currentHeight <= nextHeight)
                {
                    return false;
                }
            }

            return true;
        }

        public static int GetHighestScenicScore(int[,] map)
        {
            return EnumerateMap(map)
                .Select(i => GetScenicScore(i.x, i.y, map))
                .OrderByDescending(score => score)
                .First();
        }

        public static int GetScenicScore(int x, int y, int[,] map)
        {
            return DirectionVectors
                .Select(dir => CountVisibleTreesInDirection(x, y, map, dir))
                .Aggregate((a, b) => a * b);
        }

        public static int CountVisibleTreesInDirection(int x, int y, int[,] map, (int x, int y) directionVector)
        {
            var dimension = map.GetLength(0);

            if (IsDirectionOutOfBounds(x, y, dimension, directionVector))
            {
                return 0;
            }

            var currentHeight = map[x, y];

            var cx = x;
            var cy = y;

            var visibleCount = 0;

            while (cx > 0 && cx < dimension - 1 && cy > 0 && cy < dimension - 1)
            {
                cx = cx + directionVector.x;
                cy = cy + directionVector.y;

                var nextHeight = map[cx, cy];

                visibleCount++;

                if (nextHeight >= currentHeight)
                {
                    break;
                }
            }

            return visibleCount;
        }

        public static bool IsDirectionOutOfBounds(int x, int y, int dimension, (int x, int y) directionVector)
        {
            var dx = x + directionVector.x;
            var dy = y + directionVector.y;

            return !(dx >= 0 && dx < dimension && dy >= 0 && dy < dimension);
        }

        public static IEnumerable<(int x, int y)> EnumerateMap(int[,] map)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    yield return (x, y);
                }
            }
        }
    }
}
