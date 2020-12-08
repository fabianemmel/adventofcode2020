using AoCHelper;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_04 : BaseDay
    {
        private readonly string _input;

        public Day_04()
        {
            if (!File.Exists(InputFilePath))
            {
                throw new SolvingException($"Path {InputFilePath} not found for {GetType().Name}");
            }
            _input = File.ReadAllText(InputFilePath);
        }
        public override string Solve_1()
        {
            var passports = _input.Split("\r\n\r\n", System.StringSplitOptions.RemoveEmptyEntries);
            var nValid = 0;
            foreach (var passport in passports)
            {
                if (isValidPassport(passport)) nValid++;
            }
            return nValid.ToString();
        }

        bool isValidPassport(string passport)
        {
            var nElements = 0;
            bool cid = false;

            cid = false;
            var passportFields = passport.Split(new char[] { ':', '\n', ' ' });
            nElements = passportFields.Length / 2;
            for (int i = 0; i < passportFields.Length; i = i + 2)
            {
                if (passportFields[i] == "cid") cid = true;
            }
            if (nElements == 8 || (nElements == 7 && cid == false)) return true;
            return false;
        }

        public override string Solve_2()
        {
            var passports = _input.Split("\r\n\r\n", System.StringSplitOptions.RemoveEmptyEntries);
            var nValid = 0;
            foreach (var passport in passports)
            {
                if (isValidPassportRegex(passport)) nValid++;
            }
            return nValid.ToString();
        }


        bool isValidPassportRegex(string passport)
        {
            var cid = false;
            var passportFields = passport.Split(new char[] { ':', '\n', ' ', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
            var nElements = passportFields.Length / 2;
            if (nElements < 7) return false;

            for (int i = 0; i < passportFields.Length; i = i + 2)
            {
                var value = passportFields[i + 1];
                switch (passportFields[i])
                {
                    case "byr":
                        if (!Regex.IsMatch(value, "^(19[2-9][0-9]|200[0-2])$")) return false;
                        break;
                    case "iyr":
                        if (!Regex.IsMatch(value, "^(201[0-9]|2020)$")) return false;
                        break;
                    case "eyr":
                        if (!Regex.IsMatch(value, "^(202[0-9]|2030)$")) return false;
                        break;
                    case "hgt":
                        if (!Regex.IsMatch(value, "^(1([5-8][0-9]|9[0-3])cm|(59|6[0-9]|7[0-6])in)$")) return false;
                        break;
                    case "hcl":
                        if (!Regex.IsMatch(value, "^(#[a-f0-9]{6})$")) return false;
                        break;
                    case "ecl":
                        if (!Regex.IsMatch(value, "^(amb|blu|brn|gry|grn|hzl|oth)$")) return false;
                        break;
                    case "pid":
                        if (!Regex.IsMatch(value, "^([0-9]{9})$")) return false;
                        break;
                    case "cid":
                        cid = true;
                        break;
                }
            }
            if (nElements == 8 || (nElements == 7 && cid == false))
            {
                return true;
            }
            return false;
        }
    }
}
