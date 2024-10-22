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
using System.Windows.Forms;

namespace GameOfLifeApp
{
    /// <summary>
    /// Entry point for the application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main method which starts the Game of Life application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameOfLife());
        }
    }
}