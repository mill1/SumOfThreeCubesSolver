﻿using SumOfThreeCubesSolver.Interfaces;
using SumOfThreeCubesSolver.Models;
using SumOfThreeCubesSolver.Services;

namespace SumOfThreeCubesSolver.Solvers
{
    public class BruteForceSolver : IRunnable
    {
        private Arguments _arguments;
        private readonly SolutionsPrinter _solutionsPrinter;

        public BruteForceSolver(SolutionsPrinter solutionsPrinter)
        {
            _solutionsPrinter = solutionsPrinter;
        }

        public void Run(Arguments arguments)
        {
            _arguments = arguments;

            var permutations = GetKCombsWithRept(Enumerable.Range(_arguments.StartValue, ResolveRangeSize()), 3);

            Dictionary<double, List<SumOfThreeCubesResult>> dictionary = new();

            foreach (IEnumerable<int> permutation in permutations)
                ProcessCombination(dictionary, permutation.ToList());

            string path = _solutionsPrinter.Print(dictionary, _arguments);

            Console.WriteLine($"Done. See\r\n{path}");
        }

        private int ResolveRangeSize()
        {
            if (_arguments.StartValue < 0)
                return _arguments.EndValue + Math.Abs(_arguments.StartValue) + 1;

            return _arguments.EndValue - _arguments.StartValue + 1;
        }

        private void ProcessCombination(Dictionary<double, List<SumOfThreeCubesResult>> dictionary, List<int> combination)
        {
            bool proceed = _arguments.HandleAnnullingSolutions ? true : !IsAnnullingSolution(combination);

            if (proceed)
            {
                if (IsPrimitiveCombination(combination))
                {
                    var result = new SumOfThreeCubesResult(combination[0], combination[1], combination[2]);

                    if (result.Sum >= _arguments.LowerBound && result.Sum <= _arguments.Upperbound)
                    {
                        List<SumOfThreeCubesResult> list;
                        if (dictionary.TryGetValue(result.Sum, out list))
                            list.Add(result);
                        else
                        {
                            list = new List<SumOfThreeCubesResult> { result };
                            dictionary.Add(result.Sum, list);
                        }
                    }
                }
            }
        }

        // Does the combination contain values where the sum of two of them is zero, thus cancelling on another out? F.i: -9³ + 2³ + 9³
        private bool IsAnnullingSolution(List<int> combination)
        {
            return combination[0] + combination[1] == 0 ||
                   combination[0] + combination[2] == 0 ||
                   combination[1] + combination[2] == 0;
        }

        private bool IsPrimitiveCombination(List<int> combination)
        {
            return Math.Abs(combination.Aggregate(GCD)) == 1;
        }

        private int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        // Cannot take credit myself: https://stackoverflow.com/questions/1952153/what-is-the-best-way-to-find-all-combinations-of-items-in-an-array
        private static IEnumerable<IEnumerable<T>> GetKCombsWithRept<T>(IEnumerable<T> list, int length) where T : IComparable
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetKCombsWithRept(list, length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) >= 0),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}