using AoCHelper;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_01 : BaseDay
    {
        private readonly List<int> _input;
        private const int target = 2020;

        public Day_01()
        {
            _input = File.ReadAllLines(InputFilePath).Select(int.Parse).ToList();
        }
        public override string Solve_1()
        {
            var data = _input;
            data.Sort();

            var lower = 0;
            var upper = data.Count - 1;

            while (lower < upper)
            {
                if (data.ElementAt(lower) + data.ElementAt(upper) == target)
                    return (data.ElementAt(lower) * data.ElementAt(upper)).ToString();
                if (data.ElementAt(lower) + data.ElementAt(upper) < target) lower++; else upper--;
            }
            return "No solution found!";
        }

        public override string Solve_2()
        {
            var data = _input;
            data.Sort();

            int upper, lower;

            for (int i = 0; i < data.Count - 1; i++)
            {
                lower = i + 1;
                upper = data.Count - 1;
                while (lower < upper)
                {
                    if (data.ElementAt(i) + data.ElementAt(lower) + data.ElementAt(upper) == target)
                        return (data.ElementAt(i) * data.ElementAt(lower) * data.ElementAt(upper)).ToString();
                    if (data.ElementAt(i) + data.ElementAt(lower) + data.ElementAt(upper) < target) lower++; else upper--;
                }
            }
            return "No solution found!";
        }
    }
}
