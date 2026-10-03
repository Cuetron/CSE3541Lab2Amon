using System.Data;
using System.IO.IsolatedStorage;
using TMPro.EditorUtilities;
using UnityEditor.Search;
using UnityEngine;

namespace amonq20
{

    // Helper class for my kruskals algorithm.  Helps make the algorithm cleaner.
    public class UnionFind
    {
        private readonly int[] parent;
        private readonly int numberOfColumns;
        
        // Initializes each item to be its own parent
        public UnionFind(int numberOfRows, int numberOfColumns)
        {
            this.numberOfColumns = numberOfColumns;
            parent = new int[numberOfRows * numberOfColumns];

            for (int i = 0; i < parent.Length; i++)
            {
                parent[i] = i;
            }
        }

        // converts the row and column of an item to a sinlge list index
        private int ToIndex(int row, int column)
        { 
            return row * numberOfColumns + column;
        }

        // Finds the parent index of a node in the duel graph
        public int Find(int row, int column)
        {
            int index = ToIndex(row, column);

            while (index != parent[index])
            {
                index = parent[index] ;
            }

            return index;
        }
        
        // Combines 2 nodes into the same set by setting the parent of one to the other.
        public void Union(int rowA, int columnA, int rowB, int columnB)
        {
            int rootA = Find(rowA, columnA);
            int rootB = Find(rowB, columnB);
            if (rootA != rootB)
            {
                parent[rootB] = rootA;
            }
        }
    }
}