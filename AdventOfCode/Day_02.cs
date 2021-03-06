﻿using AoCHelper;
using System.IO;

namespace AdventOfCode
{
    public class Day_02 : BaseDay
    {
        private readonly string[] _input;
        public Day_02()
        {
            if (!File.Exists(InputFilePath))
            {
                throw new SolvingException($"Path {InputFilePath} not found for {GetType().Name}");
            }
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
                if (IsValidPasswordOld(currentLine[3], int.Parse(currentLine[0]), int.Parse(currentLine[1]), currentLine[2])) nValid++;
            }
            return nValid.ToString();
        }

        private bool IsValidPasswordOld(string password, int minLength, int maxLength, string character)
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
                if (IsValidPasswordNew(currentLine[3], int.Parse(currentLine[0]), int.Parse(currentLine[1]), currentLine[2])) nValid++;
            }
            return nValid.ToString();
        }

        private bool IsValidPasswordNew(string password, int firstPos, int secondPos, string character)
        {
            if (password[firstPos - 1] == character[0] ^ password[secondPos - 1] == character[0])
                return true;
            return false;
        }
    }
}
