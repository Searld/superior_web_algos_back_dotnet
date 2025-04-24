using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace superior_algos_back.GeneticAlgo
{
    public class Population
    {
        public List<Chromosome> Chromosomes { get; set; } = new List<Chromosome>();
        private readonly int _sizeOfTournament = 3;

        public Population(List<Vertex> sourceVertices, int sizeOfPopulation)
        {
            GeneratePopulation(sourceVertices, sizeOfPopulation);
        }

        public void GeneratePopulation(List<Vertex> sourceVertices, int sizeOfPopulation)
        {
            var firstChromosome = new Chromosome(sourceVertices);
            firstChromosome.CountLength();
            firstChromosome.CountFitness();
            Chromosomes.Add(firstChromosome);

            var uniqueRoutes = new HashSet<string>(capacity: sizeOfPopulation) { GetRouteKey(firstChromosome.Vertices) };

            for (int i = 1; i < sizeOfPopulation; i++)
            {
                var chromosome = new Chromosome();
                chromosome.Vertices = GenerateUniqueChromosome(sourceVertices, uniqueRoutes);
                chromosome.CountLength();
                chromosome.CountFitness();
                Chromosomes.Add(chromosome);
            }
        }

        public (Chromosome, Chromosome) SelectParents()
        {
            var parent1 = Selection();
            var parent2 = Selection();

            while (parent1 == parent2)
                parent2 = Selection();

            return (parent1, parent2);
        }

        private Chromosome Selection()
        {
            var tournament = Chromosomes.OrderBy(_ => Random.Shared.Next()).Take(_sizeOfTournament);
            return tournament.OrderByDescending(c => c.Fitness).First();
        }

        private List<Vertex> GenerateUniqueChromosome(List<Vertex> sourceList, HashSet<string> uniqueRoutes)
        {
            List<Vertex> newRoute;
            string routeKey;
            int attempts = 0;
            const int maxAttempts = 100; 

            do
            {
                newRoute = new List<Vertex>(sourceList);
                Random.Shared.Shuffle(CollectionsMarshal.AsSpan(newRoute));

                routeKey = GetRouteKey(newRoute);
                attempts++;

                if (attempts >= maxAttempts)
                    break; 
            }
            while (uniqueRoutes.Contains(routeKey));

            uniqueRoutes.Add(routeKey);
            return newRoute;
        }

        private string GetRouteKey(List<Vertex> route)
        {
            return string.Join("-", route.Select(v => v.Id));
        }

    }
}
