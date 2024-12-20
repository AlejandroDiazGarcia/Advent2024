
namespace Advent2024 {

    public class AdventDay11 : AdventDay {

        private int BLINKS = 25;
        private Dictionary<long, long> map = new Dictionary<long, long>();

        public void RunCodePart1(string[] input) {

            // Get numbers
            int[] inputs = input[0].Split(' ').Select(int.Parse).ToArray();

            // Initialise stones
            Stone first, stone;
            first = new Stone() {
                Number = inputs[0]
            };
            stone = first;

            for (int i = 0; i < inputs.Length - 1; i++) {
                Stone next = new Stone() {
                    Number = inputs[i + 1]
                };

                stone.Next = next;

                stone = next;
            }


            for (int i = 0; i < BLINKS; i++) {

                // Blink
                stone = first;
                while (stone != null) {
                    stone = stone.Blink();
                }
            }

            // Count stones
            int total = 0;
            stone = first;
            while (stone != null) {
                total++;
                stone = stone.Next;
            }

            Console.WriteLine(total);
        }


        public void RunCodePart2(string[] input) {

            BLINKS = 75;

            // Get numbers
            int[] inputs = input[0].Split(' ').Select(int.Parse).ToArray();

            // Initialise map
            for (int i = 0; i < inputs.Length; i++) {
                if (!map.ContainsKey(inputs[i])) map.Add(inputs[i], 0);
                map[inputs[i]]++;
            }

            Dictionary<long, long> copy;
            for (int i = 0; i < BLINKS; i++) {

                // Shallow copy
                copy = new Dictionary<long, long>(map);

                // Go through each stone number
                foreach (var stone in copy) {

                    // Remove old stones number
                    map[stone.Key] -= copy[stone.Key];

                    if (stone.Key == 0) {

                        // Zero stones become 1's
                        if (!map.ContainsKey(1)) map.Add(1, 0);
                        map[1] += copy[stone.Key];

                    }
                    else if (stone.Key.ToString().Length % 2 == 0) {

                        // Split stone in two
                        string numberString = stone.Key.ToString();

                        // First
                        long extra = Convert.ToInt64(numberString.Substring(0, numberString.Length / 2));
                        if (!map.ContainsKey(extra)) map.Add(extra, 0);
                        map[extra] += copy[stone.Key];

                        // Second
                        extra = Convert.ToInt64(numberString.Substring(numberString.Length / 2));
                        if (!map.ContainsKey(extra)) map.Add(extra, 0);
                        map[extra] += copy[stone.Key];
                    }
                    else {

                        // Multiply by 2024
                        if (!map.ContainsKey(stone.Key * 2024)) map.Add(stone.Key * 2024, 0);
                        map[stone.Key * 2024] += copy[stone.Key];
                    }
                }
            }

            Console.WriteLine(map.Values.Sum());
        }
    }

    public class Stone {

        public long Number { get; set; }

        public Stone Next { get; set; }

        public Stone ShallowCopy() {
            return (Stone)this.MemberwiseClone();
        }

        public Stone Blink() {

            if (Number == 0) Number = 1;
            else if (Number.ToString().Length % 2 == 0) {
                Stone next = Next != null ? Next.ShallowCopy() : Next;
                string numberString = Number.ToString();
                Number = Convert.ToInt64(numberString.Substring(0, numberString.Length / 2));
                Next = new Stone {
                    Number = Convert.ToInt64(numberString.Substring(numberString.Length / 2)),
                    Next = next
                };

                // Return original next one
                return next;
            }
            else {
                Number *= 2024;
            }

            return Next;
        }
    }
}
