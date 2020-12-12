using AoCHelper;
using System.IO;

namespace AdventOfCode
{
    enum Direction
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
    }
    public class Day_12 : BaseDay
    {
        private readonly string[] _input;
        private Direction direction = Direction.East;
        private int posX = 0;
        private int posY = 0;
        private int waypointX = -1;
        private int waypointY = 10;

        public Day_12()
        {
            if (!File.Exists(InputFilePath))
            {
                throw new SolvingException($"Path {InputFilePath} not found for {GetType().Name}");
            }
            _input = File.ReadAllLines(InputFilePath);
        }
        public override string Solve_1()
        {
            foreach (var instruction in _input)
            {
                ParseInstruction(instruction);
            }
            return (System.Math.Abs(posX) + System.Math.Abs(posY)).ToString();            
        }

        private void ParseInstruction(string instruction)
        {
            char action = instruction[0];
            int value = int.Parse(instruction.Substring(1));
            if( action == 'F')
            {
                action = direction switch
                {
                    Direction.North => 'N',
                    Direction.East => 'E',
                    Direction.South => 'S',
                    Direction.West => 'W',
                    _ => 'I'
                };                
            }

            switch (action)
            {
                case 'N':
                    posX -= value;
                    break;
                case 'S':
                    posX += value;
                    break;
                case 'E':
                    posY += value;
                    break;
                case 'W':
                    posY -= value;
                    break;
                case 'L':                    
                    direction = (Direction)(((int)direction - value / 90 + 4) % 4);
                    break;
                case 'R':
                    direction = (Direction)(((int)direction + value / 90 + 4) % 4);
                    break;
            }
        }
        
        public override string Solve_2()
        {
            posX = 0;
            posY = 0;
            foreach (var instruction in _input)
            {
                ParseInstructionWaypoint(instruction);
            }
            return (System.Math.Abs(posX) + System.Math.Abs(posY)).ToString();            
        }

        private void ParseInstructionWaypoint(string instruction)
        {
            char action = instruction[0];
            int value = int.Parse(instruction.Substring(1));

            switch (action)
            {
                case 'N':
                    waypointX -= value;
                    break;
                case 'S':
                    waypointX += value;
                    break;
                case 'E':
                    waypointY += value;
                    break;
                case 'W':
                    waypointY -= value;
                    break;
                case 'L':
                    RotateWaypoint(action, value);
                    break;
                case 'R':
                    RotateWaypoint(action, value);
                    break;
                case 'F':
                    posX += value * waypointX;
                    posY += value * waypointY;
                    break;
            }
        }

        private void RotateWaypoint(char action, int value)
        {
            value = value / 90;
            while (value > 0)
            {
                if (action == 'R')
                {
                    var temp = waypointX;
                    waypointX = waypointY;
                    waypointY = -temp;
                }
                else if (action == 'L')
                {
                    var temp = waypointX;
                    waypointX = -waypointY;
                    waypointY = temp;
                }
                value--;
            }
        }

    }
}
