namespace Advent2024 {

    public class AdventDay15 : AdventDay {

        /// <summary>
        /// Input map
        /// </summary>
        private char[][] input;

        /// <summary>
        /// Robot position
        /// </summary>
        private Tuple<int, int> robot;

        /// <summary>
        /// Possible directions
        /// </summary>
        private Dictionary<char, Tuple<int, int>> directions = new Dictionary<char, Tuple<int, int>> {
            {'^', Tuple.Create(-1, 0) },
            {'v', Tuple.Create(1, 0) },
            {'>', Tuple.Create(0, 1) },
            {'<', Tuple.Create(0, -1) },
        };

        public void RunCodePart1(string[] input) {

            // Initialise values
            this.input = input.Select(s => s.ToCharArray()).ToArray();
            int i = 0;

            for (i = 0; i < input.Length; i++) {

                if (input[i] == string.Empty) {
                    break;
                }

                if (robot == null) {
                    // Get robot coords
                    if (input[i].Contains("@")) robot = Tuple.Create(i, input[i].IndexOf("@"));
                    else continue;

                }
            }

            int total = FollowInstructions(i);

            Console.WriteLine(total);
        }


        public void RunCodePart2(string[] input) {

            // Initialise values
            int i = 0;


            // Duplicate values
            List<char[]> lines = new List<char[]>();

            for (i = 0; i < input.Length; i++) {

                if (input[i] == string.Empty) {
                    break;
                }

                List<char> line = new List<char>();
                for (int j = 0; j < input[i].Length; j++) {

                    if (input[i][j] == 'O') {
                        line.Add('[');
                        line.Add(']');
                    }
                    else {

                        line.Add(input[i][j]);

                        if (input[i][j] == '@') {
                            line.Add('.');
                        }
                        else {
                            line.Add(input[i][j]);
                        }
                    }
                }
                if (line.Contains('@')) robot = Tuple.Create(i, line.IndexOf('@'));
                lines.Add(line.ToArray());

            }

            // Add instructions with break to match part 1
            lines.Add(null);
            for (int j = i + 1; j < input.Length; j++) {

                // Directions
                lines.Add(input[j].ToCharArray());
            }
            this.input = lines.ToArray();


            int total = FollowInstructions(i, true);

            Console.WriteLine(total);
        }

        private int FollowInstructions(int i, bool big = false) {

            for (i = i + 1; i < input.Length; i++) {

                // Directions
                for (int j = 0; j < input[i].Length; j++) {

                    // Check whether the robot can move his position
                    if (CanMove(directions[input[i][j]], big)) {

                        // Update old and new position
                        this.input[robot.Item1][robot.Item2] = '.';
                        robot = Tuple.Create(robot.Item1 + directions[input[i][j]].Item1, robot.Item2 + directions[input[i][j]].Item2);
                        this.input[robot.Item1][robot.Item2] = '@';

                        // Print map for debugging
                        Console.WriteLine($"Movement {i - 11} {j}: {input[i][j]}");
                        for (int k = 0; k < input.Length; k++) {

                            if (input[k] == null || input[k].Length == 0) break;
                            Console.WriteLine(input[k]);
                        }
                        Console.WriteLine(Environment.NewLine);
                    }
                }
            }

            // Work out total
            int total = 0;
            for (i = 0; i < input.Length; i++) {

                if (input[i] == null || input[i].Length == 0) break;

                for (int j = 0; j < this.input[0].Length; j++) {
                    if (this.input[i][j] == 'O' || this.input[i][j] == '[') {
                        total += 100 * i + j;
                    }
                }
            }

            return total;
        }

        private bool CanMove(Tuple<int, int> direction, bool big) {

            // Base case
            int boxX = robot.Item1 + direction.Item1, boxY = robot.Item2 + direction.Item2;
            if (input[boxX][boxY] == '.') return true;
            if (input[boxX][boxY] == '#') return false;

            // Boxes
            if (!big) {

                while (input[boxX][boxY] == 'O') {
                    boxX += direction.Item1;
                    boxY += direction.Item2;
                }

                // Check if there is a wall at the end of the boxes
                if (input[boxX][boxY] == '#') return false;

                // Move box
                input[boxX][boxY] = 'O';
            }
            else {

                if (direction.Item1 == 0) {

                    // Moving horizontally
                    int startY = boxY;
                    while (input[boxX][boxY] == '[' || input[boxX][boxY] == ']') {
                        boxY += direction.Item2;
                    }

                    // Check if there is a wall at the end of the boxes
                    if (input[boxX][boxY] == '#') return false;

                    // Move boxes
                    while(boxY != startY) {

                        input[boxX][boxY] = input[boxX][boxY - direction.Item2];
                        boxY -= direction.Item2;
                    }
                }
                else {

                    // Moving vertically - check where the boxes starts and end on the Y axis     
                    List<int> boxCoords = new List<int>() { boxY - (input[boxX][boxY] == ']' ? 1 : 0), boxY + (input[boxX][boxY] == '[' ? 1 : 0) };
                    Dictionary<int, List<int>> boxLevels = new Dictionary<int, List<int>>();

                    // Check until there is no new layer of boxes
                    while (boxCoords.Count > 0) {

                        // Store box positions on each level
                        List<int> existingLevel = new List<int>(boxCoords);
                        boxCoords = new List<int>();

                        foreach (int y in existingLevel) {

                            if (input[boxX][y] == '#') return false;

                            if (input[boxX][y] == ']') {
                                if(!boxCoords.Contains(y - 1)) boxCoords.Add(y - 1);
                                if (!boxCoords.Contains(y)) boxCoords.Add(y);
                            }
                            else if (input[boxX][y] == '[') {
                                if (!boxCoords.Contains(y)) boxCoords.Add(y);
                                if (!boxCoords.Contains(y + 1)) boxCoords.Add(y + 1);
                            }
                        }

                        if (boxCoords.Count > 0) {

                            boxLevels.Add(boxX, boxCoords);
                            boxX += direction.Item1;
                        }
                    }

                    // Move boxes
                    while (boxX - direction.Item1 != robot.Item1) {

                        List<int> previousLevel = boxLevels.First(x => x.Key == boxX - direction.Item1).Value;
                        List<int> newLevel = boxLevels.FirstOrDefault(x => x.Key == boxX).Value ?? new List<int>();

                        // Set the previous level in the new one
                        foreach (int y in previousLevel) {
                            input[boxX][y] = input[boxX - direction.Item1][y];
                        }

                        // Check if we need to remove boxes on the new level
                        foreach (int y in newLevel.Except(previousLevel)) {
                            input[boxX][y] = '.';
                        }

                        boxX -= direction.Item1;
                    }

                    // For the square next to the robot, on the new level, replace with a dot
                    input[robot.Item1 + direction.Item1][boxY + (input[robot.Item1 + direction.Item1][boxY] == '[' ? 1 : -1)] = '.';

                }
            }

            return true;
        }
    }
}