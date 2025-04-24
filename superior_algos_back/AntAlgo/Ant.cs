using System;

namespace superior_algos_back.AntAlgo
{
    public class Ant
    {
        private int _alpha = 5;
        private int _beta = 2;

        public Route BuildRoute(Dictionary<(int from, int to), Edge> edgesList, List<Vertex> vertices, Random random)
        {
            Vertex currentVertex = vertices[random.Next(0,vertices.Count)];
            currentVertex.IsVisited = true;
            Route route = new Route();
            route.Indices.Add(currentVertex.Id);

            while (!vertices.All(v=> v.IsVisited))
            {
                List<double> probabilities = new List<double>();
                var notVisitedVertices = vertices.Where(v=> !v.IsVisited && v.Id != currentVertex.Id).ToList();

                double totalSum = 0.0;
                for (int i = 0; i < notVisitedVertices.Count; i++)
                {
                    var edge = edgesList[(currentVertex.Id, notVisitedVertices[i].Id)];
                    double pheromones = Math.Pow(edge.NumberOfPheromones,_alpha);
                    double heuristic = Math.Pow(1.0 / edge.Length, _beta);
                    double wish = pheromones * heuristic;
                    probabilities.Add(wish);
                    totalSum += wish;
                }

                for (int i = 0; i < probabilities.Count; i++)
                    probabilities[i] /= totalSum;

                double choice = random.NextDouble();
                double cumulative = 0.0;

                for (int i = 0; i < probabilities.Count; i++)
                {
                    cumulative += probabilities[i];
                    if (cumulative > choice)
                    {
                        route.Length += edgesList[(currentVertex.Id, notVisitedVertices[i].Id)].Length;
                        currentVertex = notVisitedVertices[i];
                        currentVertex.IsVisited = true;
                        route.Indices.Add(currentVertex.Id);
                        break;
                    }
                }
            }

            route.Indices.Add(route.Indices[0]);
            route.Length += edgesList[(route.Indices[route.Indices.Count - 2], route.Indices[0])].Length;
            return route;
        }
    }
}
