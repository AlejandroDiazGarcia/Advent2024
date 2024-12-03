using System.Text.RegularExpressions;

namespace Advent2024 {

    public class AdventDay3 : AdventDay {

        public void RunCodePart1(string[] input) {

            string corrupt = string.Join("", input);

            int total = 0;

            string pattern = @"mul\((-?\d+),(-?\d+)\)";
            Match match;
            while ((match = Regex.Match(corrupt, pattern)).Success) {
                total += Convert.ToInt32(match.Groups[1].Value) * Convert.ToInt32(match.Groups[2].Value);
                corrupt = corrupt.Substring(corrupt.IndexOf(match.Value) + match.Value.Length);
            }
            Console.WriteLine(total.ToString());
        }

        public void RunCodePart2(string[] input) {

            string corrupt = string.Join("", input);

            int total = 0;
            bool active = true;

            string pattern = @"mul\((-?\d+),(-?\d+)\)";
            Match match;
            while ((match = Regex.Match(corrupt, pattern)).Success) {

                active = CheckActive(corrupt.Substring(0, match.Index), active);
                if (active) total += Convert.ToInt32(match.Groups[1].Value) * Convert.ToInt32(match.Groups[2].Value);
                corrupt = corrupt.Substring(corrupt.IndexOf(match.Value) + match.Value.Length);
            }
            Console.WriteLine(total.ToString());
        }

        private bool CheckActive(string input, bool active) {
            if (input.IndexOf("do()") > -1 || input.IndexOf("don't()") > -1) {
                int indexActive = input.IndexOf("do()");
                int indexDisable = input.IndexOf("don't()");
                active = indexActive != -1 && (indexDisable == -1 || indexActive < indexDisable);
                active = CheckActive(input.Substring(active ? indexActive + 4 : (indexDisable + 7)), active);
            }
            return active;
        }
    }
}
