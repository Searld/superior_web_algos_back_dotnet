using superior_algos_back.AStarAlgo;

namespace superior_algos_back.AntAlgo
{
    public class AntAlgorithm
    {
        private Random _random = new Random();
        private Dictionary<(int from, int to), Edge> edgesList;
        private List<Ant> _ants;

        private double _p = 0.6;
        private double _q = 1;
        private int _vertexNumber = 0;

        private int _bestIteration = 0;
        private double _bestLength = double.MaxValue;
        private List<int> _bestRoute = null;
        private List<List<int>> _mostEffectiveRoutes = new List<List<int>>();


        private int _maxIterations = 10000;

        private bool BestRouteStableFor(int maxStagnationIterations, int currentIteration, List<Route> currentRoutes)
        {
            var shortestRoute = currentRoutes.OrderBy(r => r.Length).First();
            double currentBestLength = shortestRoute.Length;

            if (currentBestLength < _bestLength)
            {
                _bestLength = currentBestLength;
                _bestRoute = shortestRoute.Indices;
                _bestIteration = currentIteration;
                return false;
            }

            _mostEffectiveRoutes.Add(shortestRoute.Indices);

            return (currentIteration - _bestIteration) >= maxStagnationIterations;
        }

        private void InitializeCollection(List<List<EdgeDTO>> sourceEdges)
        {
            _vertexNumber = sourceEdges.Count;

            Dictionary<(int from, int to), Edge> edges = new Dictionary<(int from, int to), Edge> (); // словарь для списка ребер
            for (int i = 0; i < sourceEdges.Count; i++)
            {
                var currentEdgesList = sourceEdges[i];
                for (int j = 0; j < currentEdgesList.Count; j++)
                {
                    var edge = new Edge(currentEdgesList[j].Length); //ребро из i в edgesList[j].Vertex

                    edges[(i + 1, currentEdgesList[j].Vertex)] = edge; // делаю чтобы ребро из i в j было тем же что и из j в i, чтобы типо жоска обновлять феромоны
                    edges[(currentEdgesList[j].Vertex, i+1)] = edge;
                }
            }

            edgesList = edges;
        }

        private void EvaporatePheromones()
        {
            List<(int form, int to)> evaporatedEdges = new List<(int form, int to)>(); 
            for (int i = 0; i < _vertexNumber; i++)
            {
                for (int j = 0; j < _vertexNumber-1; j++)
                {
                    if(i!=j)
                    {
                        if(!evaporatedEdges.Contains((j+1,i+1)))
                        { 
                            edgesList[(i + 1, j + 1)].NumberOfPheromones *= (1 - _p); 
                            evaporatedEdges.Add((i+1,j+1));
                        }
                    }
                }
            }
        }

        private List<Vertex> GetVerticesList()
        {
            List<Vertex> vertices = new List<Vertex>();
            for (int i = 0; i < _vertexNumber; i++)
            {
                vertices.Add(new Vertex(i+1));
            }
            return vertices;
        }

        private void GenerateColony()
        {
            _ants = new List<Ant>();
            for (int i = 0; i < (int)Math.Round(Math.Sqrt(_vertexNumber)); i++)
            {
                _ants.Add(new Ant());
            }
        }

        private void UpdatePheromones(List<Route> allRoutes)
        {
            foreach (Route route in allRoutes)
            {
                double delta = _q / route.Length;
                for (int i = 1; i < route.Indices.Count; i++)
                {
                    int from = route.Indices[i - 1];
                    int to = route.Indices[i];
                    edgesList[(from,to)].NumberOfPheromones += delta;
                }
            }
        }

        public List<List<int>> GetRoutes(List<List<EdgeDTO>> sourceEdges)
        {
            InitializeCollection(sourceEdges);
            GenerateColony();


            for (int i = 0; i < _maxIterations; i++)
            {
                EvaporatePheromones();

                List<Route> allRoutes = new List<Route>();

                foreach (var ant in _ants)
                {
                    var route = ant.BuildRoute(edgesList, GetVerticesList(), _random);
                    allRoutes.Add(route);
                }

                UpdatePheromones(allRoutes);
                if (BestRouteStableFor(10, i, allRoutes))
                    break;
                else
                {

                }
            }
            _mostEffectiveRoutes.Add(_bestRoute);

            return _mostEffectiveRoutes;
        }
    }
}
