namespace Advent2024 {

    public class AdventDay5 : AdventDay {

        private List<Update> updates = new List<Update>();

        public void RunCodePart1(string[] input) {

            // Retrieve updates
            int i = GetUpdates(input);

            // Loop through rules
            int total = 0;
            for (i = i + 1; i < input.Length; i++) {

                int[] numbers = input[i].Split(',').Select(int.Parse).ToArray();
                if (CheckRules(numbers)) {
                    total += numbers[numbers.Length / 2];
                }
            }

            Console.WriteLine(total.ToString());
        }

        public void RunCodePart2(string[] input) {

            // Retrieve updates
            int i = GetUpdates(input);

            // Loop through rules
            int total = 0;
            for (i = i + 1; i < input.Length; i++) {

                // Check for wrong rules
                int[] numbers = input[i].Split(',').Select(int.Parse).ToArray();
                if (!CheckRules(numbers)) {

                    // Order list
                    List<Update> ordered = new List<Update>() { updates.First(x => x.Id == numbers[0]) };
                    for (int j = 1; j < numbers.Length; j++) {
                        Update update = updates.First(x => x.Id == numbers[j]);
                        int position = 0;
                        while (position < ordered.Count && ordered[position].Children.Any(x => x.Id == numbers[j])) {
                            position++;
                        }
                        ordered.Insert(position, update);
                    }

                    Console.WriteLine(string.Join(",", ordered.Select(x => x.Id)));
                    total += ordered[numbers.Length / 2].Id;
                }
            }

            Console.WriteLine(total.ToString());
        }

        /// <summary>
        /// Get all updates with parent/child association
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int GetUpdates(string[] input) {

            int i = 0;
            for (i = 0; i < input.Length; i++) {

                // Line break split input
                if (input[i] == "") {
                    break;
                }

                var numbers = input[i].Split('|').Select(int.Parse).ToArray();
                Update parent = updates.FirstOrDefault(x => x.Id == numbers[0]);
                if (parent == null) {
                    parent = new Update() { Id = numbers[0] };
                    updates.Add(parent);
                }

                Update child = updates.FirstOrDefault(x => x.Id == numbers[1]);
                if (child == null) {
                    child = new Update() { Id = numbers[1] };
                    updates.Add(child);
                }

                parent.Children.Add(child);
                child.Parents.Add(parent);
            }

            return i;
        }

        /// <summary>
        /// Check whether the number list is in the correct order
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        private bool CheckRules(int[] numbers) {

            bool valid = true;
            for (int i = 0; i < numbers.Length - 1; i++) {
                Update update = updates.FirstOrDefault(x => x.Id == numbers[i]);
                if (update == null || !update.Children.Any(x => x.Id == numbers[i + 1])) {
                    valid = false;
                    break;
                }
            }

            return valid;
        }
    }

    public class Update {

        public int Id { get; set; }

        public List<Update> Parents { get; set; } = new List<Update>();

        public List<Update> Children { get; set; } = new List<Update>();

    }
}
