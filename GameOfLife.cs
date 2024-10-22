///////////////////////////////////////////
/// Created By: Samuel McCorkel
/// Date: 10/02/2024
/// Summary: A visual simulation of Conways Game of Life created in C#. 
/// Conway's Game of Life is a zero-player game which simulates the behavior of cells on a grid. 
/// Each cell in the grid is either alive or dead, and its state evolves based on a simple set of rules determined by its neighbors.
/// A live cell remains alive if it has 2 or 3 living neighbors, but dies otherwise, while a dead cell becomes alive if it 
/// has exactly 3 neighbors. These straightforward rules lead to complex, fascinating patterns, demonstrating emergent
/// behavior and self-organization, making the game a famous example in computational and mathematical studies.
////////////////////////////////////////////
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameOfLifeApp
{
    /// <summary>
    /// GameOfLife form class to display the grid and manage the game state.
    /// </summary>
    public class GameOfLife : Form
    {
        private const int CellSize = 20;
        private readonly GameGrid gameGrid;
        private System.Windows.Forms.Timer timer;
        private DateTime startTime;
        private Label timeLabel;
        private Label statsLabel;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameOfLife"/> class.
        /// Sets up the form and initializes the game grid.
        /// </summary>
        public GameOfLife()
        {
            this.Text = "Conway's Game of Life";
            this.ClientSize = new Size(GameGrid.GridWidth * CellSize + 200, GameGrid.GridHeight * CellSize); // Extra space for labels
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.Black;

            gameGrid = new GameGrid();

            // Set up timer
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 100; // 100ms per generation
            timer.Tick += UpdateGame;
            timer.Start();

            // Track the start time
            startTime = DateTime.Now;

            // Set up the time label
            timeLabel = new Label
            {
                ForeColor = Color.White,
                BackColor = Color.Black,
                Location = new Point(GameGrid.GridWidth * CellSize + 10, 10),
                Size = new Size(180, 20),
                Text = "Time Elapsed: 0s"
            };
            this.Controls.Add(timeLabel);

            // Set up the statistics label
            statsLabel = new Label
            {
                ForeColor = Color.White,
                BackColor = Color.Black,
                Location = new Point(GameGrid.GridWidth * CellSize + 10, 40),
                Size = new Size(180, 60),
                Text = "Infected: 0\nDied: 0\nNever Infected: 0"
            };
            this.Controls.Add(statsLabel);

            // Enable double-buffering to reduce flicker
            this.DoubleBuffered = true;
        }

        /// <summary>
        /// Handles the OnPaint event to draw the game grid and the divider.
        /// </summary>
        /// <param name="e">Paint event arguments.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Draw the game grid
            for (int x = 0; x < GameGrid.GridWidth; x++)
            {
                for (int y = 0; y < GameGrid.GridHeight; y++)
                {
                    if (gameGrid.IsCellAlive(x, y))
                    {
                        g.FillRectangle(Brushes.Green, x * CellSize, y * CellSize, CellSize, CellSize);
                    }
                    else
                    {
                        g.FillRectangle(Brushes.DarkGray, x * CellSize, y * CellSize, CellSize, CellSize);
                    }
                }
            }

            // Draw the white line divider between the grid and the stats
            int dividerX = GameGrid.GridWidth * CellSize + 5; // Position of the divider
            g.DrawLine(Pens.White, dividerX, 0, dividerX, this.ClientSize.Height); // Vertical line
        }

        private void InitializeComponent()
        {

        }

        /// <summary>
        /// Updates the game state at each timer tick.
        /// </summary>
        private void UpdateGame(object sender, EventArgs e)
        {
            gameGrid.UpdateGrid();

            // Update time elapsed
            TimeSpan elapsedTime = DateTime.Now - startTime;
            timeLabel.Text = $"Time Elapsed: {elapsedTime.Seconds}s";

            // Update statistics
            statsLabel.Text = $"Infected: {gameGrid.TotalInfected}\nDied: {gameGrid.TotalDied}\nNever Infected: {gameGrid.TotalNeverInfected}";

            Invalidate(); // Redraw the grid
        }
    }
}