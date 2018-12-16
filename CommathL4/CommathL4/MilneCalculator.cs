using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommathL4
{
    public static class MilneCalculator
    {

        public static double RungeKuttaMethod(Func<double,double,double> diffFunc, double x0, double y0, double step)
        {
            double k1 = diffFunc(x0, y0);
            double k2 = diffFunc(x0 + step / 2, y0 + k1 * step / 2);
            double k3 = diffFunc(x0 + step / 2, y0 + k2 * step / 2);
            double k4 = diffFunc(x0 + step, y0 + k3 * step);

            double nextY = y0 + step * (k1 + 2 * k2 + 2 * k3 + k4) / 6;

            return nextY;
        }

        public static double[] Calculate(Func<double, double, double> diffFunc, double x0, double y0, double step, double n, double accuracy, out double[] xArray)
        {
            List<double> y = new List<double> { y0 };
            List<double> x = new List<double> { x0 };

            for(int counter = 0; counter < 3; counter++)
            {
                y.Add(RungeKuttaMethod(diffFunc, x[counter], y[counter], step));
                x.Add(x[counter] + step);
            }

            for(int counter = 3; counter < n; counter++)
            {
                y.Add(MilneMethod(diffFunc, x.ToArray(), y.ToArray(), step, accuracy));
                x.Add(x[counter] + step);
            }

            xArray = x.ToArray();
            return y.ToArray();
        }

        public static double MilneMethod(Func<double, double, double> diffFunc, double[] x, double[] y, double step, double accuracy)
        {
            double error = 0;

            double yProg = y[y.Length - 4] + 
                4 * step * (
                    2 * diffFunc(x[x.Length - 3], y[y.Length - 3])
                    - diffFunc(x[x.Length - 2], y[y.Length - 2]) 
                    + 2 * diffFunc(x[x.Length - 1], y[y.Length - 1])
                    ) / 3;

            double yCorr;

            do
            {
                yCorr = y[y.Length - 2] + 
                    step * (
                        diffFunc(x[x.Length - 2], y[y.Length -2])
                        + 4 * diffFunc(x[x.Length - 1], y[y.Length - 1])
                        + diffFunc(x[x.Length - 1] + step, yProg)
                    ) / 3;
                error = Math.Abs(yCorr - yProg) / 29;
                yProg = yCorr;
            } while (error > accuracy);

            return yCorr;
        }
    }
}
