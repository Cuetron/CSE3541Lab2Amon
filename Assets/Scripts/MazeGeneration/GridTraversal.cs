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
        public IEnumerable<((int Row, int Column) From, (int Row, int Column) To)> GenerateMaze(int startRow, int startColumn)
        {
            haveVisited = new bool[grid.NumberOfRows, grid.NumberOfColumns];
            return KruskalsAlg((startRow, startColumn));
        }

        // Goes through every node in the duel graph and assigns and edge weight to it (need to randomize later)
        private List<((int Row, int Column) From, (int Row, int Column) To, int Weight)> BuildWeightedEdgeList()
        {
            var edges = new List<((int Row, int Column) From, (int Row, int Column) To, int Weight)>();
            int weightCounter = 0;

            for (int row = 0; row < grid.NumberOfRows; row++)
            {
                for (int column = 0; column < grid.NumberOfColumns; column++)
                {
                    foreach (var neighbor in grid.ForwardNeighbors(row, column))
                    {
                        edges.Add(((row, column), neighbor, weightCounter));
                        weightCounter++;
                    }
                }
            }

            return edges;
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
 * pre: Iterate over every cell and every grid neighbor, create the edges from that list of verticies.  Then assingn edge weights to those.
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