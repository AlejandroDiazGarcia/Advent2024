namespace Advent2024 {

    public class AdventDay8 : AdventDay {


        public void RunCodePart1(string[] input) {
            Solve(input);
        }

        public void RunCodePart2(string[] input) {
            Solve(input, true);
        }

        public void Solve(string[] input, bool harmonics = false) {

            int inputLength = input.Length;
            int inputHalfLength = input.Length / 2;
            char[,] antinodes = new char[inputLength, inputLength];
            Dictionary<char, List<Tuple<int, int>>> antennas = new Dictionary<char, List<Tuple<int, int>>>();

            // Parse input
            for (int i = 0; i < input.Length; i++) {

                for (int j = 0; j < input[i].Length; j++) {

                    if (input[i][j] != '.') {

                        if (!antennas.ContainsKey(input[i][j])) antennas.Add(input[i][j], new List<Tuple<int, int>>());
                        antennas[input[i][j]].Add(Tuple.Create(i, j));
                    }
                }
            }

            foreach (var antennaGroup in antennas) {

                while (antennaGroup.Value.Count > 0) {

                    // Get antenna
                    var antenna = antennaGroup.Value[0];
                    antennaGroup.Value.RemoveAt(0);

                    // If there are resonant harmonics, antennas are also antinodes
                    if (harmonics) {
                        antinodes[antenna.Item1, antenna.Item2] = '#';
                    }

                    foreach (var second in antennaGroup.Value) {

                        // Check whether the antennas aren't too far away
                        if (second.Item1 - antenna.Item1 < inputHalfLength || Math.Abs(antenna.Item2 - second.Item2) < inputHalfLength) {

                            // Upper antinodes
                            int xDif = (second.Item1 - antenna.Item1);
                            int resonance = 1, x;
                            while ((x = antenna.Item1 - (resonance * xDif)) >= 0) {

                                int y = antenna.Item2 + (resonance * (antenna.Item2 - second.Item2));
                                if (y >= 0 && y < inputLength) {    
                                    antinodes[x, y] = '#';
                                }
                                else {
                                    break;
                                }

                                resonance++;
                                if (!harmonics) break;
                            }


                            // Lower antinodes
                            resonance = 1;
                            while ((x = second.Item1 + (resonance * xDif)) < inputLength) {

                                int y = second.Item2 + (resonance * (second.Item2 - antenna.Item2));
                                if (y >= 0 && y < inputLength) {       
                                    antinodes[x, y] = '#';
                                }
                                else {
                                    break;
                                }

                                resonance++;
                                if (!harmonics) break;
                            }

                        }
                    }
                }
            }


            int count = 0;
            for (int i = 0; i < inputLength; i++) // Rows
            {
                for (int j = 0; j < inputLength; j++) // Columns
                {
                    if (antinodes[i, j] != '\0') count++;
                }
            }

            Console.WriteLine(count);
        }
    }
}
