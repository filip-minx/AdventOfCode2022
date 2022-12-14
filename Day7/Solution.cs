using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day7
{
    public static class Solution
    {
        public static void Part1()
        {
            var tokens = GetInputTokens();
            var graph = CreateGraph(tokens);
            var dirNodes = GetDirectoryNodes(graph);

            var result = dirNodes
                .Select(GetTotalSizeOfNode)
                .Where(size => size <= 100000)
                .Sum();

            Console.WriteLine("Day 7 - Part 1 - " + result);
        }

        public static void Part2()
        {
            var totalSpace = 70000000;
            var requiredSpace = 30000000;

            var tokens = GetInputTokens();
            var graph = CreateGraph(tokens);
            var dirNodes = GetDirectoryNodes(graph);

            var currentSpace = totalSpace - GetTotalSizeOfNode(graph);

            var nodeToDelete = dirNodes
                .Select(node => (node: node, size: GetTotalSizeOfNode(node)))
                .OrderBy(node => node.size)
                .First(node => currentSpace + node.size >= requiredSpace)
                .node;

            var result = GetTotalSizeOfNode(nodeToDelete);

            Console.WriteLine("Day 7 - Part 2 - " + result);
        }

        public static Node CreateGraph(List<InputToken> tokens)
        {
            var rootDirectory = new Node() { Type = NodeType.Directory, Name = "/" };
            var currentDirectory = rootDirectory;

            foreach (var token in tokens)
            {
                switch (token.Type)
                {
                    case TokenType.ChangeDirectory:
                        if (token.Parameters["name"] == "..")
                        {
                            currentDirectory = currentDirectory.Parent;
                        }
                        else if (token.Parameters["name"] == "/")
                        {
                            currentDirectory = rootDirectory;
                        }
                        else
                        {
                            currentDirectory = currentDirectory.Children.Single(child => child.Name == token.Parameters["name"]);
                        }
                        break;

                    case TokenType.PrintDirectory:
                        currentDirectory.Children.Add(new Node() { Type = NodeType.Directory, Name = token.Parameters["name"], Parent = currentDirectory });
                        break;

                    case TokenType.PrintFile:
                        currentDirectory.Children.Add(new Node() { Type = NodeType.File, Name = token.Parameters["name"], Size = int.Parse(token.Parameters["size"]) });
                        break;

                    case TokenType.List:
                        break;
                }
            }

            return rootDirectory;
        }

        public static List<InputToken> GetInputTokens()
        {
            var lines = File.ReadAllLines(@"Day7\Input.txt");

            var cdRegex = new Regex(@"\$ cd (?<param>.+)");
            var lsRegex = new Regex(@"\$ ls");
            var dirRegex = new Regex(@"dir (?<param>.+)");
            var fileRegex = new Regex(@"(?<size>\d+) (?<name>.+)");

            return lines.Select(line =>
            {
                Match match;

                if ((match = cdRegex.Match(line)).Success)
                {
                    return new InputToken
                    {
                        Type = TokenType.ChangeDirectory,
                        Parameters = new Dictionary<string, string>
                        {
                            { "name", match.Groups["param"].Value }
                        }
                    };
                }

                if ((match = lsRegex.Match(line)).Success)
                {
                    return new InputToken
                    {
                        Type = TokenType.List
                    };
                }

                if ((match = dirRegex.Match(line)).Success)
                {
                    return new InputToken
                    {
                        Type = TokenType.PrintDirectory,
                        Parameters = new Dictionary<string, string>
                        {
                            { "name", match.Groups["param"].Value }
                        }
                        
                    };
                }

                if ((match = fileRegex.Match(line)).Success)
                {
                    return new InputToken
                    {
                        Type = TokenType.PrintFile,
                        Parameters = new Dictionary<string, string>
                        {
                            { "size", match.Groups["size"].Value },
                            { "name", match.Groups["name"].Value }
                        }
                    };
                }

                throw new InvalidOperationException();
            }).ToList();
        }

        public static int GetTotalSizeOfNode(Node node)
        {
            return node.Size + node.Children.Sum(GetTotalSizeOfNode);
        }

        public static IEnumerable<Node> GetDirectoryNodes(Node node)
        {
            if (node.Type == NodeType.Directory)
            {
                yield return node;
            }

            foreach (var i in node.Children.Select(GetDirectoryNodes).SelectMany(x => x)) yield return i;
        }
    }
}
