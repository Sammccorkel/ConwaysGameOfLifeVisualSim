///////////////////////////////////////////
/// Created By: Samuel McCorkel
/// Date: 10/02/2024
/// Summary: A visual simulation of Conways Game of Life created in C#. 
///  Conway's Game of Life is a zero-player game which simulates the behavior of cells on a grid. 
/// Each cell in the grid is either alive or dead, and its state evolves based on a simple set of rules determined by its neighbors.
/// A live cell remains alive if it has 2 or 3 living neighbors, but dies otherwise, while a dead cell becomes alive if it 
/// has exactly 3 neighbors. These straightforward rules lead to complex, fascinating patterns, demonstrating emergent
/// behavior and self-organization, making the game a famous example in computational and mathematical studies.
////////////////////////////////////////////
using System;

namespace GameOfLifeApp
{
    /// <summary>
    /// Class responsible for managing the game grid and Conway's Game of Life logic.
    /// </summary>
    public class GameGrid
    {
        public const int GridWidth = 40;  // Number of cells horizontally
        public const int GridHeight = 30; // Number of cells vertically
        private bool[,] grid = new bool[GridWidth, GridHeight];
        private bool[,] previousGrid = new bool[GridWidth, GridHeight];
        private bool[,] wasEverInfected = new bool[GridWidth, GridHeight];
        private Random rand = new Random();

        public int TotalInfected { get; private set; } = 0;
        public int TotalDied { get; private set; } = 0;
        public int TotalNeverInfected => GridWidth * GridHeight - TotalInfected;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameGrid"/> class.
        /// Populates the grid with random live and dead cells.
        /// </summary>
        public GameGrid()
        {
            InitializeGrid();
        }

        /// <summary>
        /// Initializes the grid with random values (live or dead cells).
        /// </summary>
        private void InitializeGrid()
        {
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    grid[x, y] = rand.Next(2) == 0;
                    if (grid[x, y])
                    {
                        wasEverInfected[x, y] = true;
                        TotalInfected++;
                    }
                }
            }
        }

        /// <summary>
        /// Determines if a cell is alive at the given coordinates.
        /// </summary>
        /// <param name="x">X coordinate of the cell.</param>
        /// <param name="y">Y coordinate of the cell.</param>
        /// <returns>True if the cell is alive, otherwise false.</returns>
        public bool IsCellAlive(int x, int y)
        {
            return grid[x, y];
        }

        /// <summary>
        /// Updates the grid based on the rules of Conway's Game of Life.
        /// </summary>
        public void UpdateGrid()
        {
            bool[,] newGrid = new bool[GridWidth, GridHeight];

            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    int liveNeighbors = CountLiveNeighbors(x, y);

                    // Apply Conway's Game of Life rules
                    if (grid[x, y])
                    {
                        // Live cell stays alive with 2 or 3 live neighbors, otherwise it dies
                        if (liveNeighbors == 2 || liveNeighbors == 3)
                        {
                            newGrid[x, y] = true;
                        }
                        else
                        {
                            newGrid[x, y] = false;
                            TotalDied++;
                        }
                    }
                    else
                    {
                        // Dead cell becomes alive with exactly 3 live neighbors
                        if (liveNeighbors == 3)
                        {
                            newGrid[x, y] = true;
                            if (!wasEverInfected[x, y])
                            {
                                wasEverInfected[x, y] = true;
                                TotalInfected++;
                            }
                        }
                        else
                        {
                            newGrid[x, y] = false;
                        }
                    }
                }
            }

            previousGrid = grid;
            grid = newGrid;
        }

        /// <summary>
        /// Counts the number of live neighbors around the specified cell.
        /// </summary>
        /// <param name="x">X coordinate of the cell.</param>
        /// <param name="y">Y coordinate of the cell.</param>
        /// <returns>The number of live neighbors.</returns>
        private int CountLiveNeighbors(int x, int y)
        {
            int liveNeighbors = 0;

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                        continue; // Skip the cell itself

                    int nx = x + dx;
                    int ny = y + dy;

                    if (nx >= 0 && nx < GridWidth && ny >= 0 && ny < GridHeight)
                    {
                        if (grid[nx, ny])
                            liveNeighbors++;
                    }
                }
            }

            return liveNeighbors;
        }
    }
}
