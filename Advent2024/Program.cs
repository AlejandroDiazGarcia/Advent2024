using System.Reflection;

namespace Advent2024 {
    internal class Program {
        static void Main(string[] args) {

            int day = Convert.ToInt32(args.Length == 0 ? DateTime.Today.Day : args[0]);
            int part = Convert.ToInt32(args.Length == 0 ? 1 : args[1]);

            // Load the current assembly
            Assembly assembly = Assembly.GetExecutingAssembly();

            // Get all types
            var implementingTypes = assembly.GetTypes().Where(t => typeof(AdventDay).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract).ToArray();
            AdventDay advendDay = (AdventDay)Activator.CreateInstance(assembly.GetType($"Advent2024.AdventDay{day}"));

            string[] input = File.ReadAllLines($@"C:\Users\alejandro\Desktop\Advent 2024\Advent2024\Inputs\Day_{day}.txt");

            if (part == 1) advendDay.RunCodePart1(input);
            else if (part == 2) advendDay.RunCodePart2(input);

            Console.ReadLine();
        }
    }
}
