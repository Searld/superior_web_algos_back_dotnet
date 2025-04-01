using System.Diagnostics.Metrics;

namespace superior_algos_back.AStarAlgo
{
    public enum CellType
    {
        Start,
        End,
        Unaviable,
        Aviable
    }
    public class Cell
    {
        public Index Index {  get; set; }
        public List<Cell> NearestCells { get; set; } = new List<Cell>() { }; // первая - верх, вторая - низ, третья - право, четвертая - лево
        public CellType Type { get; set; }
        public int PathLength { get; set; } = 0;
        public int Cost { get; set; } = 0;
        public bool IsVisited { get; set; } = false;
        public Cell Previous { get; set; }

        public Cell(CellType type, Index index)
        {
            Type = type;
            Index = index;
        }

        public static bool operator >(Cell cell1, Cell cell2)
        {
            return cell1.Cost > cell2.Cost;
        }
        public static bool operator <(Cell cell1, Cell cell2)
        {
            return cell1.Cost < cell2.Cost;
        }
    }
}
