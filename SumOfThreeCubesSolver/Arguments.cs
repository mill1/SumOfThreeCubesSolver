namespace SumOfThreeCubesSolver
{
    public class Arguments
    {
        public string Solver { get; set; }
        public int StartValue { get; set; }
        public int EndValue { get; set; }
        public int PrintFrom { get; set; }
        public int PrintUntil { get; set; }
        public bool ProcessAnnullingSolutions { get; set; }
        public bool PrintNoSolutions { get; set; }
        public int TextWarningThreshold { get; set; }
        public string Path { get; set; }
    }
}
