using Microsoft.Extensions.DependencyInjection;
using SumOfThreeCubesSolver.Interfaces;
using SumOfThreeCubesSolver.Models;
using SumOfThreeCubesSolver.Services;
using SumOfThreeCubesSolver.Solvers;

namespace SumOfThreeCubesSolver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var arguments = ResolveArguments(args);
                IServiceCollection services = BuildServiceCollection(arguments.Solver);

                using (ServiceProvider sp = services.BuildServiceProvider())
                {
                    var r = sp.GetRequiredService<IRunnable>();
                    r.Run(arguments);
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
        }

        private static IServiceCollection BuildServiceCollection(string solver)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddScoped<SolutionsPrinter>();
            
            switch (solver)
            {
                case "brute force solver":
                    services.AddScoped<IRunnable, BruteForceSolver>();
                    break;
                case "needs work solver":
                    services.AddScoped<IRunnable, NeedsWorkSolver>();
                    break;
                default:
                    throw new NotImplementedException($"Solver is not implemented: '{solver}'");
            }

            return services;
        }

        private static Arguments ResolveArguments(string[] args)
        {            
            int startValue = ResolveValue(args, "start value", -10);
            var endValue = ResolveValue(args, "end value", 10);
            var printFrom = ResolveArgument(args, "print from");
            var printUntil = ResolveArgument(args, "print until");
            var processAnnullingSolutions = ResolveArgument(args, "process annulling solutions");
            var printNoSolutions = ResolveArgument(args, "print no solutions");

            return new Arguments
            {
                Solver = ResolveArgument(args, "solver") ?? "brute force solver",
                StartValue = startValue,
                EndValue = endValue,
                PrintFrom = printFrom == null ? ResolveLimit(startValue) : int.Parse(printFrom),
                PrintUntil = printUntil == null ? ResolveLimit(endValue) : int.Parse(printUntil),
                ProcessAnnullingSolutions = processAnnullingSolutions == null ? false : bool.Parse(processAnnullingSolutions),
                PrintNoSolutions = printNoSolutions == null ? true : bool.Parse(printNoSolutions),
                Path = ResolveArgument(args, "path") ?? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
        }

        private static string ResolveArgument(string[] args, string value)
        {
            var arg = args.Where(x => x.StartsWith(value, StringComparison.OrdinalIgnoreCase));

            if (arg.Any())
            {
                int startIndex = arg.First().IndexOf(value, StringComparison.OrdinalIgnoreCase) + value.Length + 1;
                return arg.First().Substring(startIndex);
            }
            return null;
        }

        private static int ResolveValue(string[] args, string value, int defaultValue)
        {
            string stringValue = ResolveArgument(args, value);
            return stringValue == null ? defaultValue : int.Parse(stringValue);
        }

        private static int ResolveLimit(int startValue)
        {
            return (int)new SumOfThreeCubesResult(startValue, startValue, startValue).Sum;
        }
    }
}