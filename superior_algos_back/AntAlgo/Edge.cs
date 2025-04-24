namespace superior_algos_back.AntAlgo
{
    public class Edge
    {
        public double Length { get; set; }
        public double NumberOfPheromones { get; set; } = 1.0;
        public Edge(double length)
        {
            Length = length;
        }
    }
}
