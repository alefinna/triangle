using System;
using System.Text;

namespace Triangle
{
    class Program
    {
        static void Main(string[] args)
        {
            // Can be handeled using DI and be unit tested.
            // Intentionally left out the unit tests. Sorry about that!
            ITriangleCoordinateService triangeService = new TriangleCoordinateService();

            // use interface to produce triangle coordinates
            // Starting at the origin (0, 0), build squares of 10 pixels size horizontally and
            // vertically towards the positive x and y axes respectively with maximum size of 60 pixels.
            // Here the rows (A - F) are represented on Y-axis and columns (1-12) on X-axis
            /// So we will have 6 squares of 10 pixels size on each axis creating A1 - A6, B1 - B6,
            /// .. F1 - F6 squares ... and A1-A12 ... F1-F12 right triangels.
            var xAxisPixels = new[] {0, 10, 20, 30, 40, 50, 60};
            var yAxisPixels = new[] {0, 10, 20, 30, 40, 50, 60};
            var coordinates = triangeService.CalculateCoordinates(xAxisPixels, yAxisPixels);
            Program.PrintCoordinates(coordinates);

            // triangle F1 with vertices (0, 0), (0, 10), (10, 0) - must return row = 10, and column = 10
            // in fact, based on our diagram, every row and column will have row = column = 10
            var triangle = triangeService
                .CalculateRowAndColumn(new int[,] {{0, 0}, {0, 10}, {10, 0}});
            Console.WriteLine("Triangle with vertices (0, 0),(0, 10),(10, 0)" +
                $"has Row = {triangle.Row} and Column = {triangle.Column}");

            // lets test the Swap by passing the first two vertices to be vertices opposite
            // to the right angle (the vertices of the hypotenuse)
            triangle = triangeService
                .CalculateRowAndColumn(new int[,] {{10, 0}, {0, 10}, {0, 0}});
            Console.WriteLine("Triangle with vertices (10, 0),(0, 10),(0, 0)" +
                $"has Row = {triangle.Row} and Column = {triangle.Column}");
        }

        /// <summary>
        ///  Prints the coordinates in the console. This will reverse the array to display 
        ///  as presented in the diagram. I am making an assumption that the F1 right triangle vertex is
        ///  at the origin.
        /// </summary>
        /// <param name="coordinates">An array of (x,y) coordinates.</param>
        public static void PrintCoordinates(int[][,] coordinates)
        {
            for (int i = coordinates.Length - 1; i >= 0; i--)
            {
                var outputLine = new StringBuilder();
                for (int j = 0; j < coordinates[i].GetLength(0); j++)
                {
                    outputLine.Append($"({coordinates[i][j, 0]}, {coordinates[i][j, 1]}) ");
                }
                Console.WriteLine(outputLine);
            }
        }
    }
}
