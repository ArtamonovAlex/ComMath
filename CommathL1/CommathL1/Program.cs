using System;

namespace CommathL1
{
    class Program
    {
        static void Main(string[] args)
        {
            double accuracy;
            int size;
            Matrix matrix = new Matrix();
            Vector freeTerms = new Vector();
            try
            {
                switch (args.Length)
                {
                    case 0:
                        size = IO.ReadSize();
                        if(!IO.Randomize(size , out matrix, out freeTerms))
                        {
                            matrix = IO.ReadMatrix(size);
                            freeTerms.Elements = IO.ReadVector(matrix.Size);
                        }
                        accuracy = IO.ReadAccuracy();
                        break;
                    case 1:
                        IO.ReadFromFile(args[0], out matrix, out freeTerms, out accuracy);
                        break;
                    default:
                        throw new Exception("Invalid arguments");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message} \nPress anything to exit");
                Console.ReadKey();
                return;
            }
            IO.PrintMatrix(matrix, "\nBase matrix");
            if (!Helper.CheckPrevalence(matrix) && !Helper.ReachPrevalence(matrix))
            {
                Console.WriteLine("There is no diagonal prevalence \nPress anything to exit");
                Console.ReadKey();
                return;
            }
            Helper.NormalizeSLAE(matrix, freeTerms, out Matrix normalMatrix, out freeTerms);
            IO.PrintMatrix(normalMatrix, "\nNormalized matrix");
            IO.PrintVector(freeTerms, "\nNormalized vector");
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
            } while (accuracy<maxDifference);
            Console.WriteLine($"Ammount of iterations: {iteration}");
            Console.ReadLine();
        }
    }
}
