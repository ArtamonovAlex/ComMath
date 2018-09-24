using System;
using System.IO;

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
            Console.WriteLine($"Please, enter {matrixSize} free terms:");
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

        public static double ReadAccuracy()
        {
            Console.WriteLine($"Please, enter the accuracy:");
            if (!double.TryParse(Console.ReadLine(), out double accuracy))
            {
                throw new Exception("Not an double value");
            }
            return accuracy;
        }

        public static void ReadFromFile(string path, out Matrix matrix, out Vector vector, out double accuracy)
        {
            
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            using (StreamReader reader = new StreamReader(file))
            {
                char[] separator = { ' ' };
                string inputString = reader.ReadLine();
                string[] elements = inputString.Split(separator);
                if (!int.TryParse(elements[0], out int size) || size<1 || size>20) {
                    throw new Exception("Invalid size");
                }
                if (elements.Length != 2 + size*(size+1)) //matrix size + matrix [size*size] + vector[size] + accuracy
                {
                    throw new Exception("Invalid structure");
                }
                double[,] matrixElements = new double[size, size];
                for (int row = 0; row < size; row++)
                {
                    for (int cell = 0; cell < size; cell++)
                    {
                        if(!double.TryParse(elements[1 + row * size + cell], out matrixElements[row, cell])) //matrix size + number of matrix element
                        {
                            throw new Exception($"Invalid matrix element {elements[1 + row * size + cell]}");
                        }
                    }
                }
                Matrix inputMatrix = new Matrix(size, matrixElements);
                matrix = inputMatrix;
                double[] vectorElements = new double[size];
                for (int counter = 0; counter<size; counter++)
                {
                    if(!double.TryParse(elements[1 + size*size + counter], out vectorElements[counter])) //matrix size + matrix [size*size] + number of vector element
                    {
                        throw new Exception($"Invalid vector element {elements[1 + size * size + counter]}"); 
                    }
                }
                Vector inputVector = new Vector(vectorElements);
                vector = inputVector;
                if (!double.TryParse(elements[1 + size*(size+1)], out accuracy)) //matrix size + matrix[size*size] + vector[size]
                {
                    throw new Exception("Invalid accuracy");
                }
            }
            file.Close();
        }
    }
}
