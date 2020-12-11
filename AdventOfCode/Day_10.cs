using AoCHelper;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_10 : BaseDay
    {
        private readonly List<int> _input;
        private List<int> _output;
        private const int target = 2020;

        public Day_10()
        {
            if (!File.Exists(InputFilePath))
            {
                throw new SolvingException($"Path {InputFilePath} not found for {GetType().Name}");
            }                   
            _input = File.ReadAllLines(InputFilePath).Select(int.Parse).ToList();
        }
        public override string Solve_1()
        {
            _input.Sort();
            _output = new();
            for(int i = 1; i < _input.Count; i++)
            {
                _output.Add(_input[i] - _input[i - 1]);
            }
            return ((_output.Where(x => x == 1).Count() + 1) * (_output.Where(x => x == 3).Count() + 1)).ToString();            
        }

        public override string Solve_2()
        {
            var indexIs3 = _output.Select((v, i) => new { v, i }).Where(x => x.v == 3).Select(x => x.i).ToList();
            var diff3 = new List<int>();

            for (int i = 1; i < indexIs3.Count; i++)
            {
                diff3.Add(indexIs3[i] - indexIs3[i - 1]);
            }
            diff3 = diff3.Where(x => x > 1).ToList();
            diff3 = diff3.Select(x => x - 1).ToList();
            var result = 2L;
            foreach(var value in diff3)
            {
                switch(value)
                {
                    case 2:
                        result *= 2;
                        break;
                    case 3:
                        result *= 4;
                        break;
                    case 4:
                        result *= 7;
                        break;
                }
            }
            return result.ToString();
        }
    }
}
