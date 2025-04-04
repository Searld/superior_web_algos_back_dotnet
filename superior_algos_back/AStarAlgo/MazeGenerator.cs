using System.Drawing;

namespace superior_algos_back.AStarAlgo
{
    public class MazeGenerator
    {
        private Random _rand = new Random();
        private int _size = 0;
        private Tuple<int, int>[] _directions = new Tuple<int, int>[]
        {
            new Tuple<int,int>( -1, 0 ),
            new Tuple<int,int>( 1, 0 ),
            new Tuple<int,int>( 0, -1 ),
            new Tuple<int,int>( 0, 1 )
        };
        public List<List<bool>> Generate(int size)
        {
            _size = size;
            List<List<bool>> maze = new List<List<bool>>();
            for (int i = 0; i < size; i++)
            {
                maze.Add(new List<bool>());
                for (int j = 0; j < size; j++)
                {
                    maze[i].Add(false);
                }
            }

            int x = _rand.Next(0, _size / 2) * 2;
            int y = _rand.Next(0, _size / 2) * 2;

            HashSet<Tuple<int, int>> needConnectPoints = new HashSet<Tuple<int, int>>();
            needConnectPoints.Add(new Tuple<int, int>(x, y));

            while (needConnectPoints.Count > 0)
            {
                var point = GetAndRemoveRandomElement(needConnectPoints);

                x = point.Item1;
                y = point.Item2;

                maze[y][x] = true;

                Connect(maze, x, y);
                AddVisitPoints(maze, needConnectPoints, x, y);
            }

            return maze;
        }

        private void Connect(List<List<bool>> maze, int x, int y)
        {
            Random.Shared.Shuffle(_directions);

            for (int i = 0; i < _directions.GetLength(0); i++)
            {
                int neighborX = x + _directions[i].Item1 * 2;
                int neighborY = y + _directions[i].Item2 * 2;

                if (IsRoad(maze, neighborX, neighborY))
                {
                    int connectorX = x + _directions[i].Item1;
                    int connectorY = y + _directions[i].Item2;
                    maze[connectorY][connectorX] = true;

                    return;
                }
            }
        }

        private void AddVisitPoints(List<List<bool>> maze, HashSet<Tuple<int, int>> points, int x, int y)
        {
            if (IsPointInMaze(x - 2, y) && !IsRoad(maze, x - 2, y) )
                points.Add(new Tuple<int, int>(x - 2, y));

            if (IsPointInMaze( x + 2, y) && !IsRoad(maze, x + 2, y))
                points.Add(new Tuple<int, int>(x + 2, y));

            if (IsPointInMaze( x, y - 2) && !IsRoad(maze, x, y - 2))
                points.Add(new Tuple<int, int>(x, y - 2));

            if (IsPointInMaze( x, y + 2) && !IsRoad(maze, x, y + 2))
                points.Add(new Tuple<int, int>(x, y + 2));
        }

        private Tuple<int,int> GetAndRemoveRandomElement(HashSet<Tuple<int,int>> points)
        {
            var randomPoint = points.ElementAt(_rand.Next(points.Count));

            points.Remove(randomPoint);

            return randomPoint;
        }

        private bool IsRoad(List<List<bool>> maze, int x, int y)
        {
            return IsPointInMaze(x, y) && maze[y][x] == true;
        }

        private bool IsPointInMaze(int x, int y)
        {
            return x >= 0 && x < _size && y >= 0 && y < _size;
        }
    }
}
