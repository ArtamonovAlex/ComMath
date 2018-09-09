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

        public static bool CheckPrevalence(Matrix matrix)
        {
            for (int row=0; row<matrix.Size; row++)
            {
                if(!CheckRowPrevalence(matrix, row, row))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ReachPrevalence(Matrix matrix)
        {
            for (int row = 0; row < matrix.Size; row++)
            {
                if (!CheckRowPrevalence(matrix, row, row))
                {
                    double max = matrix.Elements[row, row];
                    int wantedCell = row;
                    for (int cell=0; cell <matrix.Size; cell++)
                    {
                        if (matrix.Elements[row,cell] > max)
                        {
                            max = matrix.Elements[row, cell];
                            wantedCell = cell;
                        }
                    }
                    if (!CheckRowPrevalence(matrix, row, wantedCell))
                    {
                        return false;
                    }
                    if (wantedCell > row)
                    {
                        SwopColumns(matrix, row, wantedCell);
                    }
                    else return false;
                }
            }
            return CheckPrevalence(matrix);
        }

        private static void SwopColumns(Matrix matrix, int column1, int column2)
        {
            double swopper;
            for (int row = 0; row < matrix.Size; row++)
            {
                swopper = matrix.Elements[row, column1];
                matrix.Elements[row, column1] = matrix.Elements[row, column2];
                matrix.Elements[row, column2] = swopper;
            }
        }

        private static bool CheckRowPrevalence(Matrix matrix, int row, int cell)
        {
            bool comparer = true;
            double sum = 0;
            for (int cells = 0; cells < matrix.Size; cells++)
            {
                sum += Math.Abs(matrix.Elements[row, cells]);
            }
            sum -= matrix.Elements[row, cell];
            if (Math.Abs(matrix.Elements[row, cell]) <= sum)
            {
                comparer = false;
            }
            return comparer;
        }
    }
}
