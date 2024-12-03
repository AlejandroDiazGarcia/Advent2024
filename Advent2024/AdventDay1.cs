namespace Advent2024 {

    public class AdventDay1 : AdventDay {

        public void RunCodePart1(string[] input) {

            var list1 = input.Select(x => Convert.ToInt32(x.Split(' ')[0])).OrderBy(x => x).ToArray();
            var list2 = input.Select(x => Convert.ToInt32(x.Split(' ').Last())).OrderBy(x => x).ToArray();

            int total = 0;
            for (int i = 0; i < list1.Length; i++) {
                total += Math.Abs(list2[i] - list1[i]);
            }
            Console.WriteLine(total.ToString());
        }
        public void RunCodePart2(string[] input) {

            var list1 = input.Select(x => Convert.ToInt32(x.Split(' ')[0])).ToArray();
            var list2 = input.Select(x => Convert.ToInt32(x.Split(' ').Last())).ToArray();

            int total = 0;
            for (int i = 0; i < list1.Length; i++) {
                total += list1[i] * list2.Count(x => x == list1[i]);
            }
            Console.WriteLine(total.ToString());

        }
    }
}
