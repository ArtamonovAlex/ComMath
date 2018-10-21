using System;

namespace CommathL2
{
    public static class Integrator
    {
        public static double Integrate(Func<double, double> function, double leftBorder, double rightBorder, double accuracy, out long parts, out double error)
        {
            double firstResult = 0;
            double result = 0;
            parts = 4;
            double step = (rightBorder - leftBorder) / parts;
            double point = leftBorder;
            for(int part = 0; part<=parts; part++)
            {
                double value = function(point);
                if (!(value is double.NaN))
                {
                    if (part == 0 || part == parts)
                    {
                        firstResult += value;
                    }
                    else if (part % 2 == 0)
                    {
                        firstResult += 2 * value;
                    }
                    else
                    {
                        firstResult += 4 * value;
                    }
                } else
                {
                    throw new Exception("Function is not computable");
                }
                point += step;
            }
            firstResult *= step/3;
            do
            {
                parts *= 2;
                step = (rightBorder - leftBorder) / parts;
                point = leftBorder;
                for (int part = 0; part <= parts; part++)
                {
                    if (part == 0 || part == parts)
                    {
                        result += function(point);
                    }
                    if (part % 2 == 0)
                    {
                        result += 2 * function(point);
                    }
                    else
                    {
                        result += 4 * function(point);
                    }
                    point += step;
                }
                result *= step / 3;
                error = Math.Abs(firstResult - result) / 15;
                firstResult = result;
            } while (error > accuracy);
            return result;
        }
    }
}
