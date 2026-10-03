using System.Collections;
using System.Collections.Generic;

namespace ShareefSoftware
{
    class GridGraphRandomizedNeighborsDecorator<T> : IGridGraph<T>
    {
        private readonly IGridGraph<T> realInstance;
        private readonly System.Random random;

        public GridGraphRandomizedNeighborsDecorator(IGridGraph<T> grid, System.Random random)
        {
            realInstance = grid;
            this.random = random;
        }

        public int NumberOfRows { get { return realInstance.NumberOfRows; } }
        public int NumberOfColumns { get { return realInstance.NumberOfColumns; } }

        public T GetCellValue(int row, int column)
        {
            return realInstance.GetCellValue(row,column);
        }

        public IEnumerable<(int Row, int Column)> Neighbors(int row, int column)
        {
            var neighbors = new List<(int row, int column)>(4);
            foreach (var neighbor in realInstance.Neighbors(row, column))
                neighbors.Add(neighbor);
            return EnumerableHelpers.Shuffle(neighbors, random);
        }
        // Need to add for my implementation of the Neighbors function
        public IEnumerable<(int Row, int Column)> ForwardNeighbors(int row, int column)
        {
            return realInstance.ForwardNeighbors(row, column);
        }

        public IEnumerator<(int Row, int Column, T NodeValue)> GetEnumerator()
        {
            return realInstance.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}