using Microsoft.Extensions.DependencyInjection;
using SumOfThreeCubesSolver.Interfaces;
using SumOfThreeCubesSolver.Services;
using SumOfThreeCubesSolver.Solvers;

namespace SumOfThreeCubesSolver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var arguments = ResolveArguments(args);
            IServiceCollection services = BuildServiceCollection(arguments.UseBruteForce);

            using (ServiceProvider sp = services.BuildServiceProvider())
            {
                var r = sp.GetRequiredService<IRunnable>();
                r.Run(arguments);
            }
        }

        private static IServiceCollection BuildServiceCollection(bool useBruteForce)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddScoped<SolutionsPrinter>();

            if (useBruteForce)
                services.AddScoped<IRunnable, BruteForceSolver>();
            else
                services.AddScoped<IRunnable, NeedsWorkSolver>();

            return services;
        }

        private static Arguments ResolveArguments(string[] args)
        {
            var useBruteForce = ResolveArgument(args, "use brute force");
            var startValue = ResolveArgument(args, "start value");
            var endValue = ResolveArgument(args, "end value");
            var lowerBound = ResolveArgument(args, "lower bound");
            var upperBound = ResolveArgument(args, "upper bound");

            var handleAnnullingSolutions = ResolveArgument(args, "process annulling solutions");
            var printNoSolutions = ResolveArgument(args, "print no solutions");

            return new Arguments
            {
                UseBruteForce = useBruteForce == null ? true : bool.Parse(useBruteForce),
                StartValue = startValue == null ? -100 : int.Parse(startValue),
                EndValue = endValue == null ? 100 : int.Parse(endValue),
                LowerBound = lowerBound == null ? 0 : int.Parse(lowerBound),
                Upperbound = upperBound == null ? 1000 : int.Parse(upperBound),
                HandleAnnullingSolutions = handleAnnullingSolutions == null ? false : bool.Parse(handleAnnullingSolutions),
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
    }
}