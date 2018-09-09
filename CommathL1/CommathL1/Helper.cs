using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommathL1
{
    public static class Helper
    {
        public static bool Copy2DArray<T>(T[,] array1, T[,] array2)
        {
            if (array1.Length == array2.Length)
            {
                for (int row = 0; row < Math.Sqrt(array1.Length); row++)
                {
                    for (int cell = 0; cell < Math.Sqrt(array1.Length); cell++)
                    {
                        array2[row, cell] = array1[row, cell];
                    }
                }
            }
            return array1.Length == array2.Length;
        }

        public static void NormalizeSLAE(Matrix matrix, Vector vector, out Matrix normalMatrix, out Vector normalVector)
        {
            normalMatrix = new Matrix();
            matrix.CopyTo(normalMatrix);
            normalVector = new Vector(vector.Elements);
            for (int row = 0; row < matrix.Size; row++)
            {
                for (int cell = 0; cell < matrix.Size; cell++)
                {
                    normalMatrix.Elements[row, cell] = matrix.Elements[row, cell] / matrix.Elements[row, row];
                }
                normalVector.Elements[row] = normalVector.Elements[row] / matrix.Elements[row, row];
            }
        }
    }
}
