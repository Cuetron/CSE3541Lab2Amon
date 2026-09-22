using System.Collections.Generic;

namespace ShareefSoftware
{
    /// Utility class to traverse a grid.
    public class GridTraversal<T>
    {
        private bool[,] haveVisited;
        private readonly IGridGraph<T> grid;

        /// Constructor
        public GridTraversal(IGridGraph<T> grid)
        {
            this.grid = grid;
        }
        /// Kruskals enumeration
        public IEnumerable<((int Row, int Column) From, (int Row, int Column) To)> DepthFirst(int startRow, int startColumn)
        {
            haveVisited = new bool[grid.NumberOfRows, grid.NumberOfColumns];
            return KruskalsAlg((startRow, startColumn));
        }

        private IEnumerable<((int Row, int Column) From, (int Row, int Column) To)> KruskalsAlg((int Row, int Column) cell)
        {
            if (!haveVisited[cell.Row, cell.Column])
            {
                haveVisited[cell.Row, cell.Column] = true;
                foreach (var neighbor in grid.Neighbors(cell.Row, cell.Column))
                {
                    if (!haveVisited[neighbor.Row, neighbor.Column])
                    if (!haveVisited[neighbor.Row, neighbor.Column])
                    {
                        foreach (var edge in KruskalsAlg(neighbor))
                            yield return edge;
                        yield return ((cell.Row, cell.Column), neighbor);
                    }
                }
            }
        }
    }
}