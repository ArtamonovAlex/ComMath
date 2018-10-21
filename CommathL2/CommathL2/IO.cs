using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommathL2
{
    public static class IO
    {
        public static double ReadAccuracy()
        {
            Console.WriteLine("Please, enter the accuracy:");
            if (double.TryParse(Console.ReadLine(), out double accuracy))
            {
                return accuracy;
            } else
            {
                throw new Exception("Accuracy must be a number");
            }
        }

        public static double ReadBorder(string side)
        {
            Console.WriteLine($"Please, enter the {side} border:");
            if (double.TryParse(Console.ReadLine(), out double border))
            {
                return border;
            }
            else
            {
                throw new Exception("Border must be number");
            }
        }

        public static Func<double, double> ChooseFunction()
        {
            Console.WriteLine("Choose one of this functions to integrate:" +
                "\n1. x^2" +
                "\n2. ln(x)" +
                "\n3. sqrt(x^2 +3)");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    return x => x * x;
                case "2":
                    return x => Math.Log(x);
                case "3":
                    return x => Math.Sqrt(x * x + 3);
                default:
                    throw new Exception("You must choose the correct function");
            }
        }

        public static void PrintResults(double result, long parts, double error)
        {
            Console.WriteLine($"Result: {Math.Round(result,5)}\nParts: {parts}\nError: {Math.Round(error,7)}");
        }
    }
}
