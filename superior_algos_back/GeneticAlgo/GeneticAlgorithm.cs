namespace superior_algos_back.GeneticAlgo
{
    public class GeneticAlgorithm
    {
        private readonly int _sizeOfPopulation = 100;
        private readonly int _numberOfGenerations = 5000;
        private readonly double _percentOfMutations = 0.15;
        private double _currentBestLength = 0;
        private double _previousBestLength = 0;
        private double _stagnationCounter = 0;
        private double _maxStagnationGenerations = 20;
        private Random _random = new Random();

        public List<List<Vertex>> GetRoute(List<Vertex> source)
        {
            var population = new Population(source, _sizeOfPopulation);

            for (int i = 0; i < _numberOfGenerations; i++)
            {
                var (parent1, parent2) = population.SelectParents();

                var child1 = Cross(parent1, parent2);
                var child2 = Cross(parent1, parent2);

                if (_random.NextDouble() < _percentOfMutations)
                    Mutate(child1);

                if (_random.NextDouble() < _percentOfMutations)
                    Mutate(child2);

                population.Chromosomes.Add(child1);
                population.Chromosomes.Add(child2);
                population.Chromosomes = population.Chromosomes.OrderBy(c => c.Length)
                    .Take(_sizeOfPopulation)
                    .ToList();

                _currentBestLength = population.Chromosomes[0].Length;
                if (_currentBestLength == _previousBestLength)
                    _stagnationCounter++;
                else
                    _stagnationCounter = 0;

                if (_stagnationCounter >= _maxStagnationGenerations)
                    break;
            }

            population.Chromosomes[0].Vertices.Add(new Vertex(population.Chromosomes[0].Vertices[0].Id,
                population.Chromosomes[0].Vertices[0].X, population.Chromosomes[0].Vertices[0].Y));

            return population.Chromosomes.Select(c=> c.Vertices).Take(new Range(_sizeOfPopulation - 10, _sizeOfPopulation)).ToList();
        }

        private Chromosome Cross(Chromosome parent1, Chromosome parent2)
        {
            int size = parent1.Vertices.Count;
            var child = new Chromosome() { Vertices = new List<Vertex>(new Vertex[size]) }; 

            int start = _random.Next(size);
            int end = _random.Next(start, size);

            for (int i = start; i <= end; i++)
                child.Vertices[i] = parent1.Vertices[i];

            int childPos = (end + 1) % size;
            foreach (var city in parent2.Vertices)
            {
                if (!child.Vertices.Contains(city))
                {
                    child.Vertices[childPos] = city;
                    childPos = (childPos + 1) % size;
                }
            }
            child.CountLength();
            child.CountFitness();
            return child;
        }

        private void Mutate(Chromosome child)
        {
            int firstGen = _random.Next(child.Vertices.Count);
            int secondGen = _random.Next(child.Vertices.Count);
            while(firstGen == secondGen)
                secondGen = _random.Next(child.Vertices.Count);


            var tmpGen = child.Vertices[firstGen];
            child.Vertices[firstGen] = child.Vertices[secondGen];
            child.Vertices[secondGen] = tmpGen;
        }
    }
}
