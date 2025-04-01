using Microsoft.AspNetCore.Mvc.Formatters;
using System.Linq;
namespace superior_algos_back.AStarAlgo
{
    public class AStar
    {
        private static Dictionary<int, CellType> _convertToCellType = new Dictionary<int, CellType>()
        {
            { 0, CellType.Aviable },
            { 1, CellType.Start },
            { 2, CellType.End },
            { -1, CellType.Unaviable }
        };

        public static Cell Start { get; set; }
        public static Cell End { get; set; }
        public static List<Cell> OpenBank {  get; set; } = new List<Cell>();

        public static int CountManhattanDistance(Cell start, Cell end)
        {
            return Math.Abs(start.Index.X - end.Index.X) + Math.Abs(start.Index.Y - end.Index.Y);
        }

        public static void InitializeField(List<List<int>> sourceField,int n) 
        {
            List<List<Cell>> field = new List<List<Cell>>();

            for (int i = 0; i < sourceField.Count; i++)
            {
                field.Add(new List<Cell>());

                for (int j = 0; j < sourceField[i].Count; j++)
                    field[i].Add(new Cell(_convertToCellType[sourceField[i][j]], new Index(j, i)));
            }

            for (int i = 0; i < field.Count; i++)
            {
                for (int j = 0; j < field[i].Count; j++)
                {
                    field[i][j].NearestCells.Add(i - 1 >= 0 && field[i - 1][j].Type != CellType.Unaviable ? field[i - 1][j] : null);
                    field[i][j].NearestCells.Add(i + 1 < n && field[i + 1][j].Type != CellType.Unaviable ? field[i + 1][j] : null);
                    field[i][j].NearestCells.Add(j+1 < n && field[i][j+1].Type != CellType.Unaviable ? field[i][j+1] : null);
                    field[i][j].NearestCells.Add(j-1 >=0 && field[i][j-1].Type != CellType.Unaviable  ? field[i][j-1] : null);
                    if (field[i][j].Type == CellType.Start) Start = field[i][j];
                    if (field[i][j].Type == CellType.End) End = field[i][j];
                }
            }
        }

        public static Route FindRoute(List<List<int>> field)
        {
            InitializeField(field, field.Count);

            Cell currentCell = Start;
            currentCell.IsVisited = true;

            Route route = new Route();

            while(currentCell != End)
            {
                for (int i = 0; i < currentCell.NearestCells.Count; i++)
                {
                    if (currentCell.NearestCells[i]!= null && !currentCell.NearestCells[i].IsVisited)
                    {
                        currentCell.NearestCells[i].PathLength = currentCell.PathLength + 10;
                        currentCell.NearestCells[i].Cost = currentCell.NearestCells[i].PathLength +
                            CountManhattanDistance(currentCell.NearestCells[i], End);
                        currentCell.NearestCells[i].Previous = currentCell;
                        OpenBank.Add(currentCell.NearestCells[i]);
                    }
                }
                
                currentCell = currentCell.NearestCells.Where(c=> c != null).Any(c => c.Type == CellType.End) ?
                    currentCell.NearestCells.Where(c => c.Type == CellType.End).FirstOrDefault() :
                    OpenBank.Where(c => c != null).MinBy(c => c.Cost);

                OpenBank.Remove(currentCell);
                currentCell.IsVisited = true;
            }

            while (currentCell != Start)
            {
                currentCell = currentCell.Previous;
                route.Indexes.Add(currentCell.Index);
            }
            return route;
        }

    }
}
