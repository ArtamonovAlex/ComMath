using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommathLab3
{
    public class LagrangePolynomial
    {
        private double[] variables;
        private double[] y;

        public LagrangePolynomial( double[] variables, double [] y)
        {
            this.variables = variables;
            this.y = y;
        }

        public double Compute(double xValue)
        {
            
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
