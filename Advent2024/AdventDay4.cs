
namespace Advent2024 {

    public class AdventDay4 : AdventDay {

        private string KEY = "";
        private int KeyLength;
        private string[] input;
        private string KEY_BACK = "";

        public void RunCodePart1(string[] input) {

            // Initialise values
            this.input = input;
            KEY = "XMAS";
            KeyLength = KEY.Length;

            int result = 0;
            for (int i = 0; i < input.Length; i++) {
                for (int j = 0; j < input[i].Length; j++) {
                    result += CheckWord(i, j);
                }
            }
            Console.WriteLine(result);
        }

        private int CheckWord(int x, int y) {

            if (input[x][y] != KEY[0]) return 0;

            int result = 0;

            // Upwards
            if (x > KeyLength - 2) {
                if (GetString(x, y, -1, 0) == KEY) result++;
                if (y > KeyLength - 2 && GetString(x, y, -1, -1) == KEY) result++;
                if (y < input[0].Length - KeyLength + 1 && GetString(x, y, -1, 1) == KEY) result++;
            }

            // Horizontal
            if (y > KeyLength - 2 && GetString(x, y, 0, -1) == KEY) result++;
            if (y < input[0].Length - KeyLength + 1 && GetString(x, y, 0, 1) == KEY) result++;

            // Downwards
            if (x < input[0].Length - KeyLength + 1) {
                if (GetString(x, y, 1, 0) == KEY) result++;
                if (y > KeyLength - 2 && GetString(x, y, 1, -1) == KEY) result++;
                if (y < input[0].Length - KeyLength + 1 && GetString(x, y, 1, 1) == KEY) result++;
            }

            return result;
        }

        /// <summary>
        /// Get string depending on the direction
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="xIncrement"></param>
        /// <param name="yIncrement"></param>
        /// <returns></returns>
        private string GetString(int x, int y, int xIncrement, int yIncrement) {

            string key = $"";
            for (int i = 0; i < KeyLength; i++) {
                key += $"{input[x + (xIncrement * i)][y + (yIncrement * i)]}";
            }

            return key;
        }

        public void RunCodePart2(string[] input) {

            // Initialise values
            this.input = input;
            KEY = "MAS";
            KeyLength = KEY.Length;
            KEY_BACK = new string(KEY.Reverse().ToArray());

            int result = 0;

            for (int i = 0; i < input.Length; i++) {
                for (int j = 0; j < input[i].Length; j++) {
                    result += CheckCross(i, j);
                }
            }
            Console.WriteLine(result);
        }

        private int CheckCross(int x, int y) {

            if (input[x][y] != KEY[0] && input[x][y] != KEY_BACK[0]) return 0;

            int result = 0;

            // Downwards
            if (x < input[0].Length - KeyLength + 1) {
                if (y < input[0].Length - KeyLength + 1 && (GetString(x, y, 1, 1) == KEY || GetString(x, y, 1, 1) == KEY_BACK) &&
                    (GetString(x, y + 2, 1, -1) == KEY || GetString(x + 2, y, -1, 1) == KEY)) result++;
            }

            return result;
        }

    }
}
