﻿using System;

namespace CommathL1
{
    class Program
    {
        static void Main(string[] args)
        {
            int matrixSize = 3;
            double accuracy = 0.0001;
            double[,] input = new double[,] 
            {
                {10,1,-1,11},
                {1,10,-1,10},
                {-1,1,10,10}

            };
            Matrix matrix = new Matrix();
            Vector freeTerms = new Vector();
            try
            {
                matrix = IO.ReadMatrix();
                freeTerms.Elements = IO.ReadVector(matrix.Size);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message} \nPress anything to exit");
                Console.ReadKey();
                return;
            }
            Helper.NormalizeSLAE(matrix, freeTerms, out Matrix normalMatrix, out freeTerms);
            IO.PrintMatrix(matrix, "Base matrix");
            IO.PrintMatrix(normalMatrix, "Normalized matrix");
            IO.PrintVector(freeTerms, "Normalized vector");
            Vector nextVector = new Vector(freeTerms.Elements);
            Vector previousVector = new Vector(freeTerms.Elements);
            double maxDifference=0;
            int iteration = 0;
            do
            {
                iteration++;
                Solver.MakeIteration(normalMatrix, freeTerms, previousVector, out nextVector, out maxDifference);
                IO.PrintVector(nextVector,$"{iteration} iteration");
                nextVector.Elements.CopyTo(previousVector.Elements, 0);
                Console.WriteLine($"Error: {maxDifference}");
                Console.ReadLine();
            } while (accuracy<maxDifference);
            Console.WriteLine($"Ammount of iterations: {iteration}");
            Console.ReadLine();
        }
    }
}
