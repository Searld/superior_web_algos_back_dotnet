namespace superior_algos_back.GeneticAlgo
{
    public class Chromosome
    {
        public double Length { get; set; }
        public List<Vertex> Vertices { get; set; } = new List<Vertex>();
        public double Fitness { get; set; }
        public Chromosome(List<Vertex> source)
        {
             Vertices = source.OrderBy(v=>v.Id).ToList();
        }
        public Chromosome()
        {
            
        }

        public void CountLength()
        {
            for (int i = 1; i < Vertices.Count-1; i++)
            {
                Length += Vertices[i].GetDistance(Vertices[i - 1]);
            }
        }
        public void CountFitness()
        {
            Fitness = 1 / Length;
        }
    }
}
