using AoCHelper;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_06 : BaseDay
    {
        private readonly string _input;
        public Day_06()
        {
            if (!File.Exists(InputFilePath))
            {
                throw new SolvingException($"Path {InputFilePath} not found for {GetType().Name}");
            }
            _input = File.ReadAllText(InputFilePath);
        }
        public override string Solve_1()
        {
            //Split into groups => Replace "\n" by empty string => Convert to char arrays => Group by characters => Sum number of distinc characters => convert to string
            return _input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries).Select(x => x.Replace("\n", "").ToCharArray().GroupBy(c => c)).Sum(c => c.Count()).ToString();
        }


        public override string Solve_2()
        {
            //Split into groups => Split each group into persons => Convert each persons answer to a char arrray => Find chars in common with previous ones => Sum the number of common letters
            return _input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries).Select(group => group.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(person => person.ToCharArray()).Aggregate<IEnumerable<char>>((last, next) => last.Intersect(next)).ToList()).Sum(x => x.Count).ToString();
        }

    }
}
