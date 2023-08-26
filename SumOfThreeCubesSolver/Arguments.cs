namespace SumOfThreeCubesSolver
{
    public class Arguments
    {
        public bool UseBruteForce { get; set; }
        public int StartValue { get; set; }
        public int EndValue { get; set; }
        public int LowerBound { get; set; }
        public int Upperbound { get; set; }
        public bool HandleAnnullingSolutions { get; set; }
        public bool PrintNoSolutions { get; set; }
        public string Path { get; set; }
    }
}
