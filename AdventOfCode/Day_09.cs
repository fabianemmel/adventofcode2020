using AoCHelper;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_09 : BaseDay
    {
        private readonly List<long> _input;
        long invalidNumber = 0;

        Queue<long> preamble = new();

        public Day_09()
        {
            if (!File.Exists(InputFilePath))
            {
                throw new SolvingException($"Path {InputFilePath} not found for {GetType().Name}");
            }                   
            _input = File.ReadAllLines(InputFilePath).Select(long.Parse).ToList();
        }
        public override string Solve_1()
        {
            
            for (int i = 0; i < 25; i++) preamble.Enqueue(_input[i]);
            for (int i = 25; i < _input.Count; i++)
            {
                if (!Evaluate(_input[i]))
                {
                    invalidNumber = _input[i];
                    return invalidNumber.ToString();
                }
            }
            
            return "No solution found!";
        }

        private bool Evaluate(long value)
        {
            var data = preamble.ToList();
            data.Sort();

            var lower = 0;
            var upper = data.Count - 1;

            while (lower < upper)
            {
                if (data.ElementAt(lower) + data.ElementAt(upper) == value)
                {
                    preamble.Dequeue();
                    preamble.Enqueue(value);
                    return true;
                }
                if (data.ElementAt(lower) + data.ElementAt(upper) < value) lower++; else upper--;
            }
            preamble.Dequeue();
            preamble.Enqueue(value);
            return false;
        }

        public override string Solve_2()
        {
            for(int i = 0; i < _input.Count; i++)
            {
                var sum = 0L;
                var j = i;
                while(sum < invalidNumber)
                {
                    sum += _input[j];
                    if (sum == invalidNumber)
                    {                        
                        return (_input.GetRange(i, j-i).Min() + _input.GetRange(i, j - i).Max()).ToString();
                    }
                    j++;
                }
            }
            return "No solution found!";
        }
    }
}
