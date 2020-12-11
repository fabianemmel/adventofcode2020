using AoCHelper;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day_11 : BaseDay
    {
        private readonly string[] _input;
        private char[,] currentGeneration;
        private int[,] neighbours;
        bool changed;

        public Day_11()
        {
            if (!File.Exists(InputFilePath))
            {
                throw new SolvingException($"Path {InputFilePath} not found for {GetType().Name}");
            }
            _input = File.ReadAllLines(InputFilePath);
            currentGeneration = new char[_input.Length, _input[0].Length];
            neighbours = new int[_input.Length, _input[0].Length];
        }

        private void parseInput()
        {
            for (int i = 0; i < currentGeneration.GetLength(0); i++)
            {
                for (int j = 0; j < currentGeneration.GetLength(1); j++)
                {
                    currentGeneration[i, j] = _input.ElementAt(i).ElementAt(j);
                }
            }
        }

        public override string Solve_1()
        {
            parseInput();
            changed = true;
            while (changed)
            {                
                Evolve(0, 4, false);
            }
            return CountOccupiedSeats().ToString();
        }

        private void CountAllNeighbours(bool method)
        {
            for (int i = 0; i < currentGeneration.GetLength(0); i++)
            {           
                for (int j = 0; j < currentGeneration.GetLength(1); j++)
                {
                    neighbours[i, j] = method? CountVisibleNeighbours(i, j) : CountDirectNeighbours(i, j);
                }
            }
        }

        private void Evolve(int minNeighbours, int maxNeighbours, bool method)
        {
            CountAllNeighbours(method);

            changed = false;

            for (int i = 0; i < currentGeneration.GetLength(0); i++)
            {
                for (int j = 0; j < currentGeneration.GetLength(1); j++)
                {
                    if (currentGeneration[i, j] == 'L' && neighbours[i, j] == minNeighbours)
                    {
                        currentGeneration[i, j] = '#';
                        changed = true;
                    }
                    else if (currentGeneration[i, j] == '#' && neighbours[i, j] >= maxNeighbours)
                    {
                        currentGeneration[i, j] = 'L';
                        changed = true;
                    }
                }
            }
        }

        private int CountOccupiedSeats()
        {
            var seats = from char seat in currentGeneration where seat == '#' select seat;
            return seats.Count();

        }

        private int CountDirectNeighbours(int i, int j)
        {
            if (currentGeneration[i, j] == '.') return 0;
            var sum = 0;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (i + x < 0 || i + x >= currentGeneration.GetLength(0) || j + y < 0 || j + y >= currentGeneration.GetLength(1) || (x == 0 && y == 0)) ;
                    else
                    {
                        if (currentGeneration[i + x, j + y] == '#') sum++;
                    }
                }
            }
            return sum;
        }

        private int CountVisibleNeighbours(int i, int j)
        {
            if (currentGeneration[i, j] == '.') return 0;

            var sum = 0;

            sum += CheckDirection(i, j, -1, 0);
            sum += CheckDirection(i, j, 1, 0);
            sum += CheckDirection(i, j, 0, -1);
            sum += CheckDirection(i, j, 0, 1);
            sum += CheckDirection(i, j, 1, 1);
            sum += CheckDirection(i, j, 1, -1);
            sum += CheckDirection(i, j, -1, 1);
            sum += CheckDirection(i, j, -1, -1);

            return sum;
        }

        private int CheckDirection(int x, int y, int dx, int dy)
        {
            while(true)
            {
                x += dx;
                y += dy;                
                if(x < 0 || x >= currentGeneration.GetLength(0) || y < 0 || y >= currentGeneration.GetLength(1)) return 0;
                switch(currentGeneration[x, y])
                {
                    case 'L': return 0;
                    case '#': return 1;
                }                
            }
        }

        public override string Solve_2()
        {
            parseInput();

            changed = true;
            while (changed)
            {
                Evolve(0, 5, true);
            }
            return CountOccupiedSeats().ToString();
        }
    }
}
