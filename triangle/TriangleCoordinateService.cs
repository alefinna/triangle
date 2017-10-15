using System;
using System.Collections.Generic;
using System.Linq;

namespace Triangle {
    public interface ITriangleCoordinateService
    {
        int[][,] CalculateCoordinates(int[] x, int[] y);
        Triangle CalculateRowAndColumn(int[,] vertices);
    }

    public class TriangleCoordinateService : ITriangleCoordinateService
    {
        /// <summary>
        ///  Creates an array of two-dimensional points (x, y) assuming the initial point is the origin (0,0).
        /// </summary>
        /// <remarks>
        ///  It is possible to extend this to accept the initial points for x and y.
        /// </remarks>
        /// <param name="x">An integer array of pixels on the x-axis.</param>
        /// <param name="y">An integer array of pixels on the y-axis.</param>
        /// <returns>
        ///  Returns an array of (x, y) coordinates of each rectangular unit formed by intersecting
        ///  the x's and the y's.
        ///</returns>
        public int[][,] CalculateCoordinates(int[] x, int[] y)
        {
            // this creates the new instance of the jagged array
            int[][,] coordinates = new int[y.Length][,];
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < y.Length; j++)
                {
                    if (coordinates[j] == null) {
                        // this creates a new instance of the two-dimensional array
                        coordinates[j] = new int[x.Length, 2];
                    }
                    coordinates[j][i, 0] = x[i];
                    coordinates[j][i, 1] = y[j];
                }
            }
            return coordinates;
        }

        /// <summary>
        ///  Calculates the row and column of a triangle with the specified vertices.
        /// </summary>
        /// <remarks>
        ///  This is done based on the diagram, that among the three distinct pairs of a triangle vertices,
        ///  the two pairs will either have the same x or y coordinate.
        /// </remarks>
        /// <param name="vertices">The vertices of the triangle.</param>
        /// <returns>
        ///  Returns a Triangle object with row and column.
        ///</returns>
        public Triangle CalculateRowAndColumn(int[,] vertices)
        {
            int i = 0; 
            var triangle = new Triangle(vertices);

            // if the first two vertices have different x and y coordinates, swap the first 
            // with the last coordinate. That will make the initial vertex the right angle vertex.
            if (!triangle.HaveSameXCoordinates(i, i + 1) && 
                !triangle.HaveSameYCoordinates(i, i + 1))
                triangle.Swap(i, i + 2);

            // check the x and y coordinates again, if it doesn't validate throw an exception
            // because based on the diagram, the row must be parallel to the y-axis and the column
            // must be parallel to the x-axis. 
            if (!triangle.HaveSameXCoordinates(i, i + 1) && 
                !triangle.HaveSameYCoordinates(i, i + 1))
                throw new Exception ("Vertices are not in the right format.");
            
            for (int j = 1; j < triangle.Length; j++)
            {
                if (triangle.HaveSameXCoordinates(i, j))
                    triangle.Row = Math.Abs(
                        triangle.Vertices[j, 1] - triangle.Vertices[i, 1]);
                else
                    triangle.Column = Math.Abs(
                        triangle.Vertices[j, 0] - triangle.Vertices[i, 0]);
            }

            return triangle;
        }
    }
}