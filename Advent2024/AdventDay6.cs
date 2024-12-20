namespace Advent2024 {

    public class AdventDay6 : AdventDay {

        public void RunCodePart1(string[] input) {

            Guard guard = new Guard(input);

            guard.Patrol();

            int count = 0;
            for (int i = 0; i < guard.Positions.GetLength(0); i++) // Rows
            {
                for (int j = 0; j < guard.Positions.GetLength(1); j++) // Columns
                {
                    if (guard.Positions[i, j] != '\0') count++;
                }
            }

            Console.WriteLine(count);
        }

        public void RunCodePart2(string[] input) {

            int total = 0;

            for (int i = 0; i < input.Length; i++) {

                for (int j = 0; j < input[i].Length; j++) {

                    if (input[i][j] == '.') {
                        string[] newInput = (string[])input.Clone();
                        char[] newRow = newInput[i].ToCharArray();
                        newRow[j] = '#';
                        newInput[i] = new string(newRow);

                        Guard guard = new Guard(newInput);
                        if (guard.Patrol()) total++;
                    }
                }
            }

            Console.WriteLine(total.ToString());
        }
    }

    public class Guard {

        private int PositionX = 0;

        private int PositionY = 0;

        private Direction direction;

        private int IncrementX;

        private int IncrementY;

        private string[] input;

        public int[,] Positions { get; set; }

        public Guard(string[] input) {

            this.input = input;
            Positions = new int[input.Length, input[0].Length];

            for (int i = 0; i < input.Length; i++) {

                if (input[i].Any(x => x == '>' || x == '<' || x == 'v' || x == '^')) {
                    PositionX = i;
                    if (input[i].Any(x => x == '>')) {
                        PositionY = input[i].IndexOf('>');
                        IncrementX = 0;
                        IncrementY = 1;
                        direction = Direction.Right;
                    }
                    else if (input[i].Any(x => x == '<')) {
                        PositionY = input[i].IndexOf('<');
                        IncrementX = 0;
                        IncrementY = -1;
                        direction = Direction.Left;
                    }
                    else if (input[i].Any(x => x == 'v')) {
                        PositionY = input[i].IndexOf('v');
                        IncrementX = 1;
                        IncrementY = 0;
                        direction = Direction.Down;
                    }
                    else if (input[i].Any(x => x == '^')) {
                        PositionY = input[i].IndexOf('^');
                        IncrementX = -1;
                        IncrementY = 0;
                        direction = Direction.Up;
                    }
                }
            }

            // Mark position
            Positions[PositionX, PositionY] |= (int)direction;
        }

        public bool Patrol() {

            bool loop = false;

            while (PositionX + IncrementX >= 0 && PositionX + IncrementX < input.Length &&
                PositionY + IncrementY >= 0 && PositionY + IncrementY < input[0].Length) {

                if (input[PositionX + IncrementX][PositionY + IncrementY] == '#') {
                    Turn();
                }
                else {

                    // Check if the guard is in a loop
                    if ((Positions[PositionX + IncrementX, PositionY + IncrementY] & (int)direction) != 0) {
                        loop = true;
                        break;
                    }

                    // Move
                    PositionX += IncrementX;
                    PositionY += IncrementY;

                    Positions[PositionX, PositionY] |= (int)direction;
                }
            }

            return loop;
        }

        public void Turn() {

            if (direction == Direction.Up) {
                IncrementX = 0;
                IncrementY = 1;
                direction = Direction.Right;
            }
            else if (direction == Direction.Right) {
                IncrementX = 1;
                IncrementY = 0;
                direction = Direction.Down;
            }
            else if (direction == Direction.Down) {
                IncrementX = 0;
                IncrementY = -1;
                direction = Direction.Left;
            }
            else if (direction == Direction.Left) {
                IncrementX = -1;
                IncrementY = 0;
                direction = Direction.Up;
            }
        }

    }
    public enum Direction {
        Up = 1,
        Down = 2,
        Left = 4,
        Right = 8,
    }
}
