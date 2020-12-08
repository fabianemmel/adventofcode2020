using AoCHelper;
using System.IO;
using System;
using System.Linq;

namespace AdventOfCode
{
    public class Day_05 : BaseDay
    {
        private readonly string[] _input;
        public Day_05()
        {
            if (!File.Exists(InputFilePath))
            {
                throw new SolvingException($"Path {InputFilePath} not found for {GetType().Name}");
            }
            _input = File.ReadAllLines(InputFilePath);
        }
        public override string Solve_1()
        {
            return _input.Select(seatString => this.getSeatId(seatString)).Max().ToString();
        }

        public int getSeatId(string seatstring)
        {
            return Convert.ToInt32(seatstring.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1'), 2);
        }

        public override string Solve_2()
        {
            var seats = _input.Select(seatString => this.getSeatId(seatString)).ToList();
            seats.Sort();
            var minValue = seats[0];
            var currentIndex = seats.Count >> 1;
            var indexShift = currentIndex >> 1;
            while (indexShift > 0)
            {
                if (seats[currentIndex] - minValue == currentIndex) currentIndex += indexShift;
                else currentIndex -= indexShift;
                indexShift = indexShift >> 1;
            }
            return (seats[currentIndex] - 1).ToString();
        }

    }
}
