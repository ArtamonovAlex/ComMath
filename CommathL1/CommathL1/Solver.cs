using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommathL1
{
    public static class Solver
    {
        public static void MakeIteration(Matrix matrix, Vector freeTerms, Vector vector, out Vector nextVector, out double maxDifference)
        {
            maxDifference = 0;
            nextVector = new Vector(vector.Elements);
            for (int j = 0; j < matrix.Size; j++)
            {
                maxDifference = 0;
                nextVector.Elements[j] = 0;
                for (int k = 0; k < matrix.Size; k++)
                {
                    if (k != j)
                    {
                        nextVector.Elements[j] -= matrix.Elements[j, k] * vector.Elements[k];
                    }
                }
                nextVector.Elements[j] += freeTerms.Elements[j];
                if (maxDifference < Math.Abs(nextVector.Elements[j] - vector.Elements[j]))
                {
                    maxDifference = Math.Abs(nextVector.Elements[j] - vector.Elements[j]);
                }
            }
        }
    }
}
