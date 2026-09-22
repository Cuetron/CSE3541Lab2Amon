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

/*
 * 
 * 1: Make an empyty set to add MST into
 * 
 * 2: For every node, create a new set with just that element in it
 * 
 * 3: sort the edges on the graph in adcending order (smallest to largest)
 * 
 * 4: for every edge in the list of edges sorted, check if they are in the same set
 * 
 * 5: If they are in the same set, they are already connected so dont add them, move on
 * 
 * 6: If they are not in the same set already, add the edge to the empty MST we made at the start and then add node B ot node A's set.
 * 
 * 7: When initial MST reaches |e| we are done
 * 
 */