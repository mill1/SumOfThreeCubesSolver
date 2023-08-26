using SumOfThreeCubesSolver.Models;

namespace SumOfThreeCubesSolver.Services
{
    public class SolutionsPrinter
    {
        public string Print(Dictionary<double, List<SumOfThreeCubesResult>> dictionary, Arguments _arguments)
        {
            var path = Path.Combine(_arguments.Path, $"SumOfThreeCubesSolutions_{_arguments.LowerBound}-{_arguments.Upperbound}.txt");

            using (StreamWriter outputFile = new StreamWriter(path))
            {
                outputFile.WriteLine($"# Primitive solutions for x³ + y³ + z³ = n (for {_arguments.LowerBound} <= n <= {_arguments.Upperbound} and {_arguments.StartValue} <= x,y,z <= {_arguments.EndValue})");

                for (int value = _arguments.LowerBound; value <= _arguments.Upperbound; value++)
                    PrintSolutionsOfValue(dictionary, outputFile, value, _arguments.PrintNoSolutions);
            }
            return path;
        }

        private void PrintSolutionsOfValue(Dictionary<double, List<SumOfThreeCubesResult>> dictionary, StreamWriter outputFile, int value, bool printNoSolutions)
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
    }
}
