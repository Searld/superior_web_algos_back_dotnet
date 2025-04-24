using superior_algos_back.GeneticAlgo;

namespace superior_algos_back
{
    public static class VertexMapper
    {
        public static List<Vertex> ToVertex(List<VertexDTO> vertexDTO)
        {
            var vertexList = new List<Vertex>();
            foreach (var vertex in vertexDTO)
            {
                vertexList.Add(new Vertex(vertex.Id, vertex.X, vertex.Y));
            }
            return vertexList;
        }
    }
}
