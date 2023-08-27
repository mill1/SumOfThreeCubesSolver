using SumOfThreeCubesSolver.Models;

namespace SumOfThreeCubesSolver.Services
{
    public class SolutionsPrinter
    {
        public string Print(Dictionary<double, List<SumOfThreeCubesResult>> dictionary, Arguments _arguments)
        {
            var path = Path.Combine(_arguments.Path, $"SumOfThreeCubesSolutions_{_arguments.PrintFrom}_-_{_arguments.PrintUpTo}.txt");

            using (StreamWriter outputFile = new StreamWriter(path))
            {
                outputFile.WriteLine($"# Primitive solutions for x³ + y³ + z³ = n (for {_arguments.PrintFrom} <= n <= {_arguments.PrintUpTo} and {_arguments.StartValue} <= x,y,z <= {_arguments.EndValue},");
                outputFile.WriteLine($"# print values without solutions: {_arguments.PrintNoSolutions}, process 'anulling solutions': {_arguments.ProcessAnnullingSolutions})");

                for (int value = _arguments.PrintFrom; value <= _arguments.PrintUpTo; value++)
                    PrintSolutions(dictionary, outputFile, value, _arguments.PrintNoSolutions);
            }
            return path;
        }

        private void PrintSolutions(Dictionary<double, List<SumOfThreeCubesResult>> dictionary, StreamWriter outputFile, int value, bool printNoSolutions)
        {
            List<SumOfThreeCubesResult> solutions;

            if (dictionary.TryGetValue(value, out solutions))
            {
                solutions = solutions.OrderBy(x => Math.Abs(x.A) + Math.Abs(x.B) + Math.Abs(x.C)).ToList();
                string text = ResolveText(solutions);
                outputFile.WriteLine($"{value} = {text}");
            }
            else
            {
                if (printNoSolutions)
                {
                    if (value % 9 == 4 || value % 9 == 5)
                        outputFile.WriteLine($"{value} = no solution possible");
                    else
                        outputFile.WriteLine($"{value} = no solution found");
                }
            }
        }

        private static string ResolveText(List<SumOfThreeCubesResult> solutions)
        {
            string text = string.Empty;
            foreach (var solution in solutions)
            {
                if (solution == solutions.First())
                    text += solution.ToString();
                else
                    text += ", " + solution.ToString();
            }
            return text;
        }

        public string Print(string directory, int value, IEnumerable<string> solutions)
        {
            var path = Path.Combine(directory, $"SumOfThreeCubesSolutions_for_{value}.txt");

            using (StreamWriter outputFile = new StreamWriter(path))
            {
                outputFile.WriteLine($"# Primitive solution(s) for x³ + y³ + z³ = {value}:");

                foreach (var solution in solutions)
                    outputFile.WriteLine(solution);
            }
            return path;
        }
    }
}
