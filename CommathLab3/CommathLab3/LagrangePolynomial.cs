using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommathLab3
{
    public class LagrangePolynomial
    {
        private Func<double, double> Function;
        private double[] variables;

        public LagrangePolynomial(Func<double, double> function, double[] variables)
        {
            this.Function = function;
            this.variables = variables;
        }

        public double Compute(double xValue)
        {
            double[] y = new double[variables.Length];
            for (int counter = 0; counter < y.Length; counter++)
            {
                y[counter] = Function(variables[counter]);
            }
            double result = 0;
            double basicPolynom;
            for (int j = 0; j < variables.Length; j++)
            {
                basicPolynom = 1;
                for (int i = 0; i < variables.Length; i++)
                {
                    if (j != i)
                    {
                        basicPolynom *= (xValue - variables[i]) / (variables[j] - variables[i]);
                    }
                }
                result += y[j] * basicPolynom;
            }
            return result;
        }
    }
}
