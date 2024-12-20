using System.Text.RegularExpressions;

namespace Advent2024 {

    public class AdventDay13 : AdventDay {

        public void RunCodePart1(string[] input) {

            Solve(input);
        }

        public void RunCodePart2(string[] input) {

            Solve(input, true);
        }

        public void Solve(string[] input, bool higherPrice = false) {

            long total = 0, a1 = 0, a2 = 0, b1 = 0, b2 = 0;
            long c1 = 0, c2 = 0;
            string pattern = @"Button [A-Z]: X\+(\d+), Y\+(\d+)";
            Match match;

            for (int i = 0; i < input.Length; i++) {

                if (i % 4 < 2) {
                    // Equations
                    match = Regex.Match(input[i], pattern);

                    if (i % 4 == 0) {
                        a1 = Convert.ToInt32(match.Groups[1].Value);
                        a2 = Convert.ToInt32(match.Groups[2].Value);
                    }
                    else {
                        b1 = Convert.ToInt32(match.Groups[1].Value);
                        b2 = Convert.ToInt32(match.Groups[2].Value);
                    }
                }
                else if (i % 4 == 2) {

                    // Result
                    match = Regex.Match(input[i], @"Prize: X=(\d+), Y=(\d+)");

                    c1 = Convert.ToInt32(match.Groups[1].Value);
                    c2 = Convert.ToInt32(match.Groups[2].Value);

                    if (higherPrice) {
                        c1 += 10000000000000;
                        c2 += 10000000000000;
                    }
                }
                else {

                    // Calculate the determinant
                    long determinant = a1 * b2 - a2 * b1;

                    if (determinant == 0) {
                        continue;
                    }

                    // Cramer's Rule
                    long determinantA = c1 * b2 - c2 * b1;
                    long determinantB = a1 * c2 - a2 * c1;

                    if (determinantA % determinant != 0 || determinantB % determinant != 0) {
                        continue;
                    }

                    long a = determinantA / determinant;
                    long b = determinantB / determinant;

                    if (!higherPrice && (a > 100 || b > 100)) {
                        continue;
                    }

                    total += 3 * a + b;
                }
            }

            Console.WriteLine(total);
        }

    }
}
