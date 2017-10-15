using System;

namespace Triangle {
    // I can possibly create an Interface and/or abstract class for this
    public class Triangle 
    {
        private int[,] _vertices;
        public Triangle(int[,] vertices)
        {
            // if length of the first dimension is not 3, it's not triangle
            // I am not going to try to verify every rule of triangle here... As it's possible
            // three (x,y) vertices may not form a triangle.
            if (vertices.GetLength(0) != 3)
                throw new Exception ("Not a triangle");

            _vertices = vertices;
        }

        public int Row { get; set; }
        public int Column { get; set; }
        public int Hypotenuse { get; set; }

        public int Length => _vertices.GetLength(0);
        public int[,] Vertices => _vertices;

        public bool HaveSameXCoordinates(int index1, int index2)
        {
            if (!HasValidIndexes(index1, index2)) return false;
            return _vertices[index1, 0] == _vertices[index2, 0];
        }

        public bool HaveSameYCoordinates(int index1, int index2)
        {
            if (!HasValidIndexes(index1, index2)) return false;
            return _vertices[index1, 1] == _vertices[index2, 1];
        }

        public void Swap(int index1, int index2)
        {
            if (!HasValidIndexes(index1, index2)) 
            {
                throw new Exception("Invalid index(s)");
            };

            var tempX = _vertices[index1, 0];
            var tempY = _vertices[index1, 1];
            _vertices[index1, 0] = _vertices[index2, 0];
            _vertices[index1, 1] = _vertices[index2, 1];
            _vertices[index2, 0] = tempX;
            _vertices[index2, 1] = tempY;
        }

        private bool HasValidIndexes(int index1, int index2)
        {
            return index1 < Length || index2 < Length;
        }
    }
}