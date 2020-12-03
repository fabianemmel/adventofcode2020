using AoCHelper;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_03 : BaseDay
    {
        private readonly string[] _input;
        private const int target = 2020;

        public Day_03()
        {
            if (!File.Exists(InputFilePath))
            {
                throw new SolvingException($"Path {InputFilePath} not found for {GetType().Name}");
            }            
            _input = File.ReadAllLines(InputFilePath);
        }
        public override string Solve_1()
        {            
            return getTreesOnSlope(3, 1).ToString();
        }

        private int getTreesOnSlope(int movX, int movY)
        {   
            int currentPosX = 0, currentPosY = 0;
            int width = _input[0].Length;
            int height = _input.Length;

            int nTrees = 0;

            while(currentPosY < height - 1)
            {
                currentPosX = (currentPosX + movX) % width;
                currentPosY += movY;
                if(_input.ElementAt(currentPosY).ElementAt(currentPosX) == '#') nTrees++;
            }            
            return nTrees;
        }

        public override string Solve_2()
        {
            long result = 1;
            result *= getTreesOnSlope(1, 1);
            result *= getTreesOnSlope(3, 1);
            result *= getTreesOnSlope(5, 1);
            result *= getTreesOnSlope(7, 1);
            result *= getTreesOnSlope(1, 2);
            return result.ToString();
        }
    }
}
