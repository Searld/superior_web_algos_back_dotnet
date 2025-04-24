namespace superior_algos_back.GeneticAlgo
{
    public class Vertex
    {
        public int Id {  get; set; }
        public double X {  get; set; }
        public double Y { get; set; }

        public double GetDistance(Vertex vertex)
        {
            return Math.Sqrt(Math.Pow(X - vertex.X,2) + Math.Pow(Y - vertex.Y,2));
        }
        public Vertex(int id, double x, double y)
        {
            Id = id;
            X = x;
            Y = y;
        }
    }
}
