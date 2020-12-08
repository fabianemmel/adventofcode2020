using AoCHelper;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_07 : BaseDay
    {
        private readonly string[] _input;
        private readonly Dictionary<string, List<BagContent>> dict = new Dictionary<string, List<BagContent>>();
        public Day_07()
        {
            if (!File.Exists(InputFilePath))
            {
                throw new SolvingException($"Path {InputFilePath} not found for {GetType().Name}");
            }
            _input = File.ReadAllLines(InputFilePath);
        }
        public override string Solve_1()
        {
            _input.Select(x => x.Split(" bags contain ")).ToList().ForEach(x => dict.Add(x[0], ParseBags(x[1])));

            //Bags containing "shiny gold"
            var bags = dict.Where(x => x.Value.Select(x => x.color).Contains("shiny gold")).Select(x => x.Key).ToList();
            var totalBags = bags;

            while (bags.Any())
            {
                var nextBags = dict.Where(x => x.Value.Select(x => x.color).Intersect(bags).Count() > 0).ToList();
                bags = nextBags.Select(x => x.Key).ToList();
                totalBags = totalBags.Concat(bags).ToList();
            }

            return totalBags.Distinct().Count().ToString();
        }

        private List<BagContent> ParseBags(string bags)
        {
            var list = new List<BagContent>();
            bags.Replace(".", "").Replace(" bags", "").Replace(" bag", "").Split(", ").Where(x => x != "no other").ToList().ForEach(x => list.Add(new BagContent(x)));
            return list;
        }

        public override string Solve_2()
        {
            return GetContent(dict["shiny gold"]).ToString();
        }

        public int GetContent(List<BagContent> content)
        {
            return content.Sum(x => x.n) + content.Sum(x => GetContent(dict[x.color]) * x.n);
        }
    }

    public class BagContent
    {
        public BagContent(string bag)
        {
            var firstSpace = bag.IndexOf(" ");
            n = int.Parse(bag.Substring(0, firstSpace));
            color = bag.Substring(firstSpace + 1);
        }
        public int n;
        public string color;
    }
}
