using System.Data;
using System.IO.IsolatedStorage;
using TMPro.EditorUtilities;
using UnityEditor.Search;
using UnityEngine;

namespace amonq20
{
    public class UnionFind
    {
        private readonly int[] parent;
        private readonly int numberOfColumns;

        public UnionFind(int numberOfRows, int numberOfColumns)
        {
            this.numberOfColumns = numberOfColumns;
            parent = new int[numberOfRows * numberOfColumns];

            for (int i = 0; i < parent.Length; i++)
            {
                parent[i] = i;
            }
        }

        private int ToIndex(int row, int column)
        { 
            return row * numberOfColumns + column;
        }

        public int Find(int row, int column)
        {
            int index = ToIndex(row, column);

            while (index != parent[index])
            {
                index = parent[index] ;
            }

            return index;
        }
        

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