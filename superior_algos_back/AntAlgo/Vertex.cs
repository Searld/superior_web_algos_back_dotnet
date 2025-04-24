namespace superior_algos_back.AntAlgo
{
    public class Vertex
    {
        public int Id {  get; set; }
        public bool IsVisited = false;
        public Vertex(int id)
        {
            Id = id;
        }
    }
}
