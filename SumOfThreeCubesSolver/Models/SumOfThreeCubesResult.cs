using SumOfThreeCubesSolver.Extensions;

namespace SumOfThreeCubesSolver.Models
{
    public record SumOfThreeCubesResult
    {
        public SumOfThreeCubesResult(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
            Sum = a.Pow(3) + b.Pow(3) + c.Pow(3);
        }
        public double Sum { get; }
        public double A { get; }
        public double B { get; }
        public double C { get; }

        public override string? ToString()
        {
            return $"{A}³ + {B}³ + {C}³";
        }
    }
}
