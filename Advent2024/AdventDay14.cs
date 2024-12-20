using System.Text.RegularExpressions;

namespace Advent2024 {

    public class AdventDay14 : AdventDay {

        int xLength = 103, yLength = 101;

        public void RunCodePart1(string[] input) {

            int[] quarters = new int[4];
            int xMiddle = xLength / 2, yMiddle = yLength / 2;
            List<Robot> robots = GetRobots(input);
            int seconds = 100;

            for (int i = 0; i < robots.Count; i++) {

                Robot robot = robots[i];

                Move(ref robot, seconds);

                if (robot.x < xMiddle && robot.y < yMiddle) quarters[0]++;
                else if (robot.x < xMiddle && robot.y > yMiddle) quarters[1]++;
                else if (robot.x > xMiddle && robot.y < yMiddle) quarters[2]++;
                else if (robot.x > xMiddle && robot.y > yMiddle) quarters[3]++;
            }

            Console.WriteLine(quarters[0] * quarters[1] * quarters[2] * quarters[3]);
        }

        public void RunCodePart2(string[] input) {

            List<Robot> robots = GetRobots(input);
            int[,] map = new int[xLength, yLength];

            // Pattern starts on second 76, every 103 secs
            for (int i = 0; i < robots.Count; i++) {

                Robot robot = robots[i];

                Move(ref robot, 76);
            }

            int seconds = 76, j = 103;
            while (true) {

                seconds += j;

                Console.WriteLine("Second: " + seconds);
                map = new int[xLength, yLength];

                for (int i = 0; i < robots.Count; i++) {

                    Robot robot = robots[i];

                    Move(ref robot, j);

                    map[robot.x, robot.y] = 1;
                }

                // Plot
                for (int x = 0; x < xLength; x++) {
                    for (int y = 0; y < yLength; y++) {
                        Console.Write(map[x, y] == 0 ? " " : "x");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(Environment.NewLine);
            }
        }

        private List<Robot> GetRobots(string[] input) {

            List<Robot> robots = new List<Robot>();

            string pattern = @"p=(\d+),(\d+) v=(-?\d+),(-?\d+)";
            Match match;

            for (int i = 0; i < input.Length; i++) {

                match = Regex.Match(input[i], pattern);

                Robot robot = new Robot {
                    y = Convert.ToInt32(match.Groups[1].Value),
                    x = Convert.ToInt32(match.Groups[2].Value),
                    vY = Convert.ToInt32(match.Groups[3].Value),
                    vX = Convert.ToInt32(match.Groups[4].Value),
                };

                robots.Add(robot);
            }

            return robots;
        }


        public void Move(ref Robot robot, int seconds = 1) {

            robot.x = (robot.x + (robot.vX * seconds)) % xLength;
            robot.y = (robot.y + (robot.vY * seconds)) % yLength;

            if (robot.x < 0) robot.x = xLength + robot.x;
            if (robot.y < 0) robot.y = yLength + robot.y;
        }

    }

    public class Robot {

        public int x { get; set; }
        public int y { get; set; }

        public int vX { get; set; }
        public int vY { get; set; }
    }
}