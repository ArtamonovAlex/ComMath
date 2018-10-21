using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommathL2
{
    class Program
    {
        static void Main(string[] args)
        {
            double accuracy;
            double leftBorder;
            double rightBorder;

            try
            {
                Func<double, double> function = IO.ChooseFunction();
                leftBorder = IO.ReadBorder("left");
                rightBorder = IO.ReadBorder("right");
                accuracy = IO.ReadAccuracy();

                double result = Integrator.Integrate(function, leftBorder, rightBorder, accuracy, out long parts, out double error);
                IO.PrintResults(result, parts, error);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
