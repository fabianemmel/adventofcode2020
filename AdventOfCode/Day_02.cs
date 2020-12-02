using AoCHelper;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_02 : BaseDay
    {
        private readonly string[] _input;
        private const int target = 2020;

        public Day_02()
        {
            _input = File.ReadAllLines(InputFilePath);
        }
        public override string Solve_1()
        {
            uint nValid = 0;
            string[] currentLine;
            char[] separators = new char[] { ' ', '.', '-', ':' };
            foreach (var line in _input)
            {
                currentLine = line.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);
                if (isValidPasswordOld(currentLine[3], int.Parse(currentLine[0]), int.Parse(currentLine[1]), currentLine[2])) nValid++;
            }
            return nValid.ToString();
        }

        private bool isValidPasswordOld(string password, int minLength, int maxLength, string character)
        {
            var n = 0;
            foreach (var letter in password)
            {
                if (letter == character[0]) n++;
                if (n > maxLength) return false;
            }
            if (n < minLength) return false;
            return true;
        }

        public override string Solve_2()
        {
            uint nValid = 0;
            string[] currentLine;
            char[] separators = new char[] { ' ', '.', '-', ':' };
            foreach (var line in _input)
            {
                currentLine = line.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);
                if (isValidPasswordNew(currentLine[3], int.Parse(currentLine[0]), int.Parse(currentLine[1]), currentLine[2])) nValid++;
            }
            return nValid.ToString();
        }

        private bool isValidPasswordNew(string password, int firstPos, int secondPos, string character)
        {
            if ((password[firstPos - 1] == character[0] && password[secondPos - 1] != character[0]) || (password[firstPos - 1] != character[0] && password[secondPos - 1] == character[0]))
                return true;
            return false;
        }
    }
}
