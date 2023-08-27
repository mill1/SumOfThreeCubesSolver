using SumOfThreeCubesSolver.Interfaces;
using SumOfThreeCubesSolver.Services;

namespace SumOfThreeCubesSolver.Solvers
{
    public class NeedsWorkSolver : IRunnable
    {
        private readonly SolutionsPrinter _solutionsPrinter;
        private Arguments _arguments;

        public NeedsWorkSolver(SolutionsPrinter solutionsPrinter)
        {
            _solutionsPrinter = solutionsPrinter;
        }

        public void Run(Arguments arguments)
        {
            _arguments = arguments;

            while(true)
            {
                ConsoleWriteLine("Enter a value you'd like the solution(s) for (e.g. 3, 33 or 42):");

                var value = int.Parse(Console.ReadLine());

                if (new int[] { 3, 33, 42 }.Any(v => v == value))
                {
                    CalculateSolutions(value);
                    return;
                }

                ConsoleWriteLine("Wouldn't you rather have the solution(s) for 3, 33 or 42? (yes/no)");
                var answer = Console.ReadLine().Trim().ToLower();

                if (answer == "yes" || answer == "y")
                {
                    ConsoleWriteLine("Great. Make a choice:\r\n3 (1)\r\n33 (2)\r\n42 (3)");
                    value = int.Parse(Console.ReadLine());

                    if (new int[] { 1, 2, 3 }.Any(v => v == value))
                    {
                        ConsoleWriteLine("Are you sure? I wouldn't want you to have seconds thoughts or anything. (yes/no)");
                        answer = Console.ReadLine().Trim().ToLower();
                        if (answer == "yes" || answer == "y")
                        {
                            ConsoleWriteLine("That wasn't so hard now, was it?");
                            CalculateSolutions(ResolveValue(value));
                            return;
                        }
                        continue;
                    }
                }
            }
        }

        private static void ConsoleWriteLine(string value)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        private static int ResolveValue(int value)
        {
            switch (value)
            {
                case 1: return 3;
                case 2: return 33;
                case 3: return 42;
                default:
                    throw new ArgumentException();
            }
        }

        private void CalculateSolutions(int value)
        {
            var solutions = GetSolutions(value);
            var path = _solutionsPrinter.Print(_arguments.Path, value, solutions);
            ConsoleWriteLine($"Done. See\r\n{path}");
        }

        private IEnumerable<string> GetSolutions(int value)
        {
            for (int i = 1; i <= 3; i++)
            {
                ConsoleWriteLine($"calculating.{new string('.', i)}");
                Thread.Sleep(2000);
            }
            
            switch (value)
            {
                case 3:
                    return new List<string>
                    {
                        "1³ + 1³ + 1³",
                        "-5³ + 4³ + 4³",
                        "569936821221962380720³ + (−569936821113563493509)³ + (−472715493453327032)³"
                    };
                case 33:
                    return new List<string>
                    {
                        "88661289752875283³ + (−87784054428622393)³ + (−27361114688070403)³"
                    };
                case 42:
                    return new List<string>
                    {
                        "(-80538738812075974)³ + 80435758145817515³ + 1260212329733563³"
                    };
                default:
                    throw new ArgumentException();
            }
        }
    }
}
