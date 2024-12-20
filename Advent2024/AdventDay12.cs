namespace Advent2024 {

    public class AdventDay12 : AdventDay {

        private string[] input;
        private int inputLength;
        Dictionary<char, Plot> plots = new Dictionary<char, Plot>();

        public void RunCodePart1(string[] input) {

            Solve(input);
        }

        public void RunCodePart2(string[] input) {

            Solve(input, true);
        }

        public void Solve(string[] input, bool discount = false) {

            // Initialise values
            inputLength = input.Length;
            this.input = input;

            int total = 0;
            for (int i = 0; i < input.Length; i++) {
                for (int j = 0; j < input[0].Length; j++) {

                    // Check if we've already processed that coordinate
                    Tuple<int, int> coord = Tuple.Create(i, j);
                    if (!plots.Values.Any(x => x.Coords.Contains(coord))) {

                        // Check garden
                        CheckGarden(i, j, discount);

                        // Work out total for that area and perimeter
                        total += plots[input[i][j]].Area * (discount ? plots[input[i][j]].GetCorners() : plots[input[i][j]].Perimeter);

                        // Reset values
                        plots[input[i][j]].Area = 0;
                        plots[input[i][j]].Perimeter = 0;
                    }
                }
            }

            Console.WriteLine(total);
        }

        private void CheckGarden(int x, int y, bool discount = false) {

            if (!plots.ContainsKey(input[x][y])) plots.Add(input[x][y], new Plot());

            // Check if this square has already been checked
            Tuple<int, int> coord = Tuple.Create(x, y);
            if (plots[input[x][y]].Coords.Contains(coord)) return;

            // Add coord
            plots[input[x][y]].Coords.Add(coord);

            // Update area
            plots[input[x][y]].Area++;

            // Update perimeter

            // Vertical            
            if (x < inputLength - 1 && input[x][y] == input[x + 1][y]) CheckGarden(x + 1, y, discount);
            else if (!discount) plots[input[x][y]].Perimeter++;

            if (x > 0 && input[x][y] == input[x - 1][y]) CheckGarden(x - 1, y, discount);
            else if (!discount) plots[input[x][y]].Perimeter++;

            // Horizontal
            if (y < inputLength - 1 && input[x][y] == input[x][y + 1]) CheckGarden(x, y + 1, discount);
            else if (!discount) plots[input[x][y]].Perimeter++;

            if (y > 0 && input[x][y] == input[x][y - 1]) CheckGarden(x, y - 1, discount);
            else if (!discount) plots[input[x][y]].Perimeter++;
        }
    }

    public class Plot {

        public int Area { get; set; }

        public int Perimeter { get; set; }

        public List<Tuple<int, int>> Coords { get; set; } = new List<Tuple<int, int>>();

        public int GetCorners() {

            int corners = 0;

            throw new NotImplementedException();

            return corners;
        }
    }
}
