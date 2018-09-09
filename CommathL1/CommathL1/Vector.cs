using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommathL1
{
    public class Vector
    {
        public double[] Elements;

        public Vector(double[] elements)
        {
            Elements = new double[elements.Length];
            elements.CopyTo(Elements, 0);
        }
        public Vector()
        {

        }
    }
}
