using amonq20;
using System.Collections.Generic;
using UnityEngine;

namespace ShareefSoftware
{
    public class LevelGeneration : MonoBehaviour
    {
        [SerializeField] TileStyle tileStyle;
        [SerializeField] int numberOfRows = 10;
        [SerializeField] int numberOfColumns = 10;
        [SerializeField] List<GameObject> barrierPrefabs;
        [SerializeField] List<GameObject> pathPrefabs;
        [SerializeField] private float cellWidth;
        [SerializeField] private float cellHeight;
        [SerializeField] private Transform parentForNewObjects;
        [SerializeField] int randomSeed = 0;
        [SerializeField] Vector3 mazeCenter;
        [SerializeField] private MazeRotationController mazeRotationController;
        [SerializeField] private Camera mainCamera;
        private void Awake()
        {
            System.Random random = CreateRandom();
            var maze = new Maze(numberOfRows, numberOfColumns, random);
            IGridGraph<bool> occupancyGrid = ConvertMazeToOccupancyGraph(maze);
            CreatePrefabs(random, occupancyGrid);

            mazeCenter = ComputeMazeCenter(occupancyGrid);
            mazeRotationController.SetPivot(mazeCenter);
            PositionCameraAboveMaze(mazeCenter, occupancyGrid);
        }

        private void CreatePrefabs(System.Random random, IGridGraph<bool> occupancyGrid)
        {
            var pathFactory = new GameObjectFactoryRandomFromList(pathPrefabs, random) { Parent = parentForNewObjects };
            var wallFactory = new GameObjectFactoryRandomFromList(barrierPrefabs, random) { Parent = parentForNewObjects };
            var gridFactory = new GridGameObjectFactory(cellWidth, cellHeight)
            {
                PrefabFactoryIfTrue = pathFactory,
                PrefabFactoryIfFalse = wallFactory
            };
            gridFactory.CreatePrefabs(occupancyGrid);
        }

        private System.Random CreateRandom()
        {
            if (randomSeed == 0)
            {
                randomSeed = (int)System.DateTime.Now.Ticks & 0x0000FFFF;
            }
            Debug.Log("Random Seed: " + randomSeed);
            System.Random random = new System.Random(randomSeed);
            return random;
        }

        private IGridGraph<bool> ConvertMazeToOccupancyGraph(Maze maze)
        {
            IGridGraph<bool> occupancyGrid;
            if (tileStyle == TileStyle.Small2x2)
                occupancyGrid = MazeUtility.Create2x2OccupancyGridFromMaze(maze);
            else
                occupancyGrid = MazeUtility.Create3x3OccupancyGridFromMaze(maze, tileStyle);
            PrintOccupancyGrid(occupancyGrid);
            return occupancyGrid;
        }

        private static void PrintOccupancyGrid(IGridGraph<bool> occupancyGrid)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            for (int row = occupancyGrid.NumberOfRows - 1; row >= 0; row--)
            {
                for (int column = 0; column < occupancyGrid.NumberOfColumns; column++)
                {
                    char symbol = (occupancyGrid.GetCellValue(row, column) == true) ? ' ' : '█';
                    stringBuilder.Append(symbol);
                }
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendLine();
            Debug.Log(stringBuilder.ToString());
        }
        private Vector3 ComputeMazeCenter(IGridGraph<bool> occupancyGrid)
        {
            float centerX = cellWidth * (occupancyGrid.NumberOfColumns - 1) / 2f;
            float centerZ = cellHeight * (occupancyGrid.NumberOfRows - 1) / 2f;
            return new Vector3(centerX, 0, centerZ);
        }

        // Sets the camera at an angle you can see the entire maze, it will move the camera further back based on how large the base of the maze is
        private void PositionCameraAboveMaze(Vector3 mazeCenter, IGridGraph<bool> occupancyGrid)
        {
            float mazeWidth = cellWidth * (occupancyGrid.NumberOfColumns - 1);
            float mazeDepth = cellHeight * (occupancyGrid.NumberOfRows - 1);
            float largestDimension = Mathf.Max(mazeWidth, mazeDepth);

            float fovRadians = mainCamera.fieldOfView * Mathf.Deg2Rad;
            float margin = 2.5f;
            float height = (largestDimension / 2f) / Mathf.Tan(fovRadians / 2f) * margin;

            mainCamera.transform.position = new Vector3(mazeCenter.x, height, mazeCenter.z);
            mainCamera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
    }
}