namespace Advent2024 {

    public class AdventDay10 : AdventDay {

        private int[,] input;
        private int inputLength;
        Dictionary<Tuple<int, int>, List<Tuple<int, int>>> heights = new Dictionary<Tuple<int, int>, List<Tuple<int, int>>>();

        public void RunCodePart1(string[] input) {

            Solve(input);
        }

        public void RunCodePart2(string[] input) {

            Solve(input, true);
        }

        public void Solve(string[] input, bool distinctTrails = false) {

            // Initialise values
            inputLength = input.Length;
            this.input = new int[input.Length, input[0].Length];


            for (int i = 0; i < input.Length; i++) {
                for (int j = 0; j < input[0].Length; j++) {
                    this.input[i, j] = input[i][j] == '.' ? -1 : input[i][j] - '0'; // Convert char digit to integer
                }
            }


            int result = 0;
            for (int i = 0; i < input.Length; i++) {
                for (int j = 0; j < input[i].Length; j++) {
                    if (this.input[i, j] == 0) {
                        Tuple<int, int> start = Tuple.Create(i, j);
                        heights.Add(start, new List<Tuple<int, int>>());
                        result += CheckTrail(start, i, j);
                    }
                }
            }
            Console.WriteLine(distinctTrails ? result : heights.Sum(x => x.Value.Count));
        }

        private int CheckTrail(Tuple<int, int> start, int x, int y) {

            if (input[x, y] == 9) {
                Tuple<int, int> height = Tuple.Create(x, y);
                if (!heights[start].Contains(height)) heights[start].Add(height);
                return 1;
            }
            int result = 0;

            // Vertical            
            if (x < inputLength - 1 && input[x, y] + 1 == input[x + 1, y]) result += CheckTrail(start, x + 1, y);
            if (x > 0 && input[x, y] + 1 == input[x - 1, y]) result += CheckTrail(start, x - 1, y);

            // Horizontal
            if (y < inputLength - 1 && input[x, y] + 1 == input[x, y + 1]) result += CheckTrail(start, x, y + 1);
            if (y > 0 && input[x, y] + 1 == input[x, y - 1]) result += CheckTrail(start, x, y - 1);

            return result;
        }
    }
}
