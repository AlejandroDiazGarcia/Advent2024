namespace Advent2024 {

    public class AdventDay2 : AdventDay {

        public void RunCodePart1(string[] input) {

            int total = 0;
            for (int i = 0; i < input.Length; i++) {
                int[] level = input[i].Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
                bool increase = false, valid = true;
                for (int j = 0; j < level.Length - 1; j++) {

                    if (j == 0) increase = level[j + 1] > level[j];

                    if (increase) {
                        if (level[j + 1] <= level[j]) {
                            valid = false;
                            break;
                        }
                        if (level[j + 1] - level[j] > 3) {
                            valid = false;
                            break;
                        }
                    }
                    else {
                        if (level[j + 1] >= level[j]) {
                            valid = false;
                            break;
                        }
                        if (level[j] - level[j + 1] > 3) {
                            valid = false;
                            break;
                        }
                    }
                }
                if (valid) total++;
            }
            Console.WriteLine(total.ToString());
        }

        public void RunCodePart2(string[] input) {

            int total = 0;
            for (int i = 0; i < input.Length; i++) {
                if (CheckList(input[i])) total++;
            }
            Console.WriteLine(total.ToString());
        }

        private bool CheckList(string input, bool sublist = false) {

            int[] level = input.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
            bool increase = false;
            int j = 0;
            while (j < level.Length - 1) {

                if (j == 0) increase = level[j + 1] > level[j];

                if ((increase && (level[j + 1] <= level[j] || level[j + 1] - level[j] > 3)) ||
                    (!increase && (level[j + 1] >= level[j] || level[j] - level[j + 1] > 3))) {

                    if (sublist) return false;
                    if (j > 0) {
                        List<int> copiedList = new List<int>(level);
                        copiedList.RemoveAt(j - 1);
                        if (CheckList(string.Join(' ', copiedList), true)) return true;
                    }
                    if (j < level.Length - 1) {
                        List<int> copiedList = new List<int>(level);
                        copiedList.RemoveAt(j + 1);
                        if (CheckList(string.Join(' ', copiedList), true)) return true;
                    }
                    List<int> list = new List<int>(level);
                    list.RemoveAt(j);
                    if (CheckList(string.Join(' ', list), true)) return true;
                    return false;
                }

                j++;
            }

            return true;
        }
    }
}
