using AoCHelper;
using System.IO;

namespace AdventOfCode
{
    public class Day_08 : BaseDay
    {
        private string[] _input;
        private int pc;
        private bool[] visited;
        private long acc = 0;
        public Day_08()
        {
            if (!File.Exists(InputFilePath))
            {
                throw new SolvingException($"Path {InputFilePath} not found for {GetType().Name}");
            }
            _input = File.ReadAllLines(InputFilePath);
        }
        public override string Solve_1()
        {
            visited = new bool[_input.Length];
            pc = 0;
            while (visited[pc] == false && pc < _input.Length)
            {
                Parse(_input[pc]);
            }
            return acc.ToString();
        }

        private void Parse(string line)
        {
            visited[pc] = true;
            var operation = line.Substring(0, 3);
            var operand = int.Parse(line.Substring(4));

            switch (operation)
            {
                case "nop":
                    pc++;
                    break;
                case "acc":
                    acc += operand;
                    pc++;
                    break;
                case "jmp":
                    pc += operand;
                    break;
            }
            return;
        }

        private int changeOp = 0;

        public override string Solve_2()
        {
            for (changeOp = 0; changeOp < _input.Length; changeOp++)
            {
                visited = new bool[_input.Length];
                pc = 0;
                acc = 0;

                while (true)
                {
                    if (pc == _input.Length - 1)
                    {
                        return acc.ToString();
                    }
                    else if (pc > _input.Length && pc < 0) break;
                    if (visited[pc]) break;
                    Parse2(_input[pc]);
                }
            }
            return "";
        }

        private void Parse2(string line)
        {
            visited[pc] = true;
            var operation = line.Substring(0, 3);
            var operand = int.Parse(line.Substring(4));

            if (changeOp == pc)
            {
                if (operation == "jmp") operation = "nop";
                else if (operation == "nop") operation = "jmp";
            }

            switch (operation)
            {
                case "nop":
                    pc++;
                    break;
                case "acc":
                    acc += operand;
                    pc++;
                    break;
                case "jmp":
                    pc += operand;
                    break;
            }
            return;
        }
    }
}
