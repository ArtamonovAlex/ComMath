using System;

namespace CommathL1
{
    public static class IO
    {

        public static Matrix ReadMatrix()
        {
            Console.Write("Please, enter the size of the matrix: ");
            if (!int.TryParse(Console.ReadLine(), out int matrixSize))
            {
                throw new Exception("Not an integer value");
            }
            if (matrixSize<1 || matrixSize>20)
            {
                throw new Exception("Unacceptable value");
            }
            double[,] elements = new double[matrixSize, matrixSize];
            Console.WriteLine($"Enter {elements.Length} double-type elements:");
            for(int row=0; row < matrixSize; row++)
            {
                for(int cell=0; cell < matrixSize; cell++)
                {
                    if (!double.TryParse(Console.ReadLine(), out elements[row,cell]))
                    {
                        throw new Exception("Not an double value");
                    }
                }
            }
            Matrix matrix = new Matrix(matrixSize, elements);
            return matrix;
        }

        public static double[] ReadVector(int matrixSize)
        {
            Console.WriteLine($"Please, enter {matrixSize} free terms: ");
            double[] elements = new double[matrixSize];
            for (int counter=0;counter<matrixSize;counter++)
            {
                if (!double.TryParse(Console.ReadLine(), out elements[counter]))
                {
                    throw new Exception("Not an double value");
                }
            }
            return elements;
        }

        public static void PrintVector(Vector vector, string title)
        {
            Console.Write($"{title}:\n(");
            foreach (double element in vector.Elements)
            {
                Console.Write($"{Math.Round(element,4),7}");
            }
            Console.WriteLine(")");
        }

        public static void PrintMatrix(Matrix matrix, string title)
        {
            Console.WriteLine($"{title}:");
            for (int row = 0; row < matrix.Size; row++)
            {
                for (int cell = 0; cell < matrix.Size; cell++)
                {
                    Console.Write($"{Math.Round(matrix.Elements[row, cell],4),7} ");
                }
                Console.WriteLine();
            }
        }
    }
}
