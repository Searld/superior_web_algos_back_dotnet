namespace superior_algos_back.AStarAlgo
{
    public class AStarRequest
    {
        public Route Route { get; set; }
        public List<AStarStep> Steps { get; set; } = new List<AStarStep>();
    }
}
