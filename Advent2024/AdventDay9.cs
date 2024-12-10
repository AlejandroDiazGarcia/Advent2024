namespace Advent2024 {

    public class AdventDay9 : AdventDay {


        public void RunCodePart1(string[] input) {

            // Parse input
            // Parse input
            List<int> inputs = new List<int>();
            int id = 0, length;
            for (int i = 0; i < input[0].Length; i++) {

                length = Convert.ToInt32($"{input[0][i]}");
                for (int j = 0; j < length; j++) {
                    inputs.Add(i % 2 == 0 ? id : -1);
                }
                if (i % 2 == 0) id++;
            }

            int forward = 0, backward = inputs.Count - 1;
            while (forward < backward) {

                // Check for next free block
                while (inputs[forward] != -1) forward++;

                // Check for next memory block
                while (inputs[backward] == -1) backward--;

                if (forward < backward) {
                    inputs[forward] = inputs[backward];
                    inputs[backward] = -1;
                }
            }

            long count = 0;
            id = 0;
            while (inputs[id] != -1) {
                count += inputs[id] * id;
                id++;
            }

            Console.WriteLine(count);
        }

        public void RunCodePart2(string[] input) {

            // Parse input
            List<int> inputs = new List<int>();
            int id = 0, length;
            for (int i = 0; i < input[0].Length; i++) {

                length = Convert.ToInt32($"{input[0][i]}");
                for (int j = 0; j < length; j++) {
                    inputs.Add(i % 2 == 0 ? id : -1);
                }
                if (i % 2 == 0) {
                    id++;
                }
            }

            int forward = 0, backward = inputs.Count - 1;
            length = 0;
            while (forward < backward) {

                // Check for next free block
                while (inputs[forward] != -1) forward++;

                // Check for next memory block
                while (inputs[backward] == -1) backward--;

                if (inputs[backward] > id) {
                    backward--;
                    continue;
                }

                // Retrieve the whole block
                id = inputs[backward];
                length = 0;
                while (inputs[backward] == id) {
                    length++;
                    backward--;
                }
                backward++;

                if (forward < backward) {

                    var freeSpace = inputs
            .Select((n, index) => (Value: n, Index: index)) // Pair each number with its index
            .Where(pair => pair.Value == -1)           // Filter only the target number
            .Select((pair, groupIndex) => new { pair.Value, pair.Index, GroupIndex = groupIndex })
            .GroupBy(x => x.Index - x.GroupIndex)          // Group consecutive indices
            .Where(g => g.Count() >= length)               // Filter groups with the required count
            .FirstOrDefault();


                    if (freeSpace != null && freeSpace.First().Index < backward) {

                        int startFreeSpace = freeSpace.First().Index;
                        // Swap numbers
                        for (int i = 0; i < length; i++) {
                            inputs[startFreeSpace + i] = id;
                            inputs[backward + i] = -1;
                        }
                    }
                    else {
                        // Move to the next block
                        backward--;
                    }
                }
            }

            long count = 0;
            for (int i = 0; i < inputs.Count; i++) {
                if (inputs[i] != -1) count += inputs[i] * i;
            }

            Console.WriteLine(count);
        }
    }
}
