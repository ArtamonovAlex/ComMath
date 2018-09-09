using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommathL1
{
    public class Matrix
    {
        public int Size;
        public double[,] Elements;

        public Matrix(int matrixSize, double[,] elements)
        {
            if (SizeCheck(matrixSize, elements))
            {
                Size = matrixSize;
                Elements = new double[matrixSize, matrixSize];
                Helper.Copy2DArray(elements, Elements);
            }
            else
            {
                throw new Exception("Size mismath");
            }
        }

        public Matrix()
        {

        }

        private bool SizeCheck(int matrixSize, double[,] elements)
        {
            if (elements.Length == matrixSize * matrixSize)
            {
                return true;
            }
            else return false;
        }

        public override bool Equals(object obj)
        {
            var matrix = obj as Matrix;
            bool comparer = true;
            for (int row = 0; row < matrix.Size; row++)
            {
                for (int cell = 0; cell < matrix.Size; cell++)
                {
                    if (Elements[row, cell] != matrix.Elements[row, cell])
                    {
                        comparer = false;
                    }
                }
            }
            return matrix != null &&
                   Size == matrix.Size &&
                   comparer;
        }

        public bool CopyTo(Matrix matrix)
        {
            if (matrix.Size == 0)
            {
                matrix.Size = Size;
                matrix.Elements = new double[matrix.Size, matrix.Size];
            }
            return Helper.Copy2DArray(Elements,matrix.Elements);
        }
    }
}
