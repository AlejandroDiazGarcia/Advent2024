namespace Advent2024 {

    public class AdventDay7 : AdventDay {

        public void RunCodePart1(string[] input) {

            Solve(input);
        }

        public void RunCodePart2(string[] input) {

            Solve(input, true);

        }

        public void Solve(string[] input, bool thirdOp = false) {

            long total = 0, result = 0;
            for (int i = 0; i < input.Length; i++) {

                result = Convert.ToInt64(input[i].Split(':')[0]);
                List<long> operators = input[i].Split(':')[1].Trim().Split(' ').Select(long.Parse).ToList();

                if (CheckEquation(result, operators, thirdOp)) total += result;

            }
            Console.WriteLine(total.ToString());
        }

        private bool CheckEquation(long result, List<long> operators, bool thirdOp) {

            // Check base case
            if (operators.Count == 2) {
                return operators[0] + operators[1] == result || operators[0] * operators[1] == result || (thirdOp && Convert.ToInt64($"{operators[0]}{operators[1]}") == result);
            }
            else {

                // Get first 2 values
                long first = operators[0];
                long second = operators[1];
                operators.RemoveRange(0, 2);

                // Add new options
                List<long> original = new List<long>(operators);
                List<long> copy = new List<long>(operators);
                operators.Insert(0, first + second);
                copy.Insert(0, first * second);
                original.Insert(0, Convert.ToInt64($"{first}{second}"));

                return CheckEquation(result, copy, thirdOp) || CheckEquation(result, operators, thirdOp) || (thirdOp && CheckEquation(result, original, thirdOp));
            }
        }
    }
}
