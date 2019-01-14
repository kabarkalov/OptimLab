using System;
using System.Collections.Generic;
using System.Text;

namespace OptimLab
{
    public class Trial
    {
        public double X;
        public double[] Y;

        public int Index;
        public List<double> Evals;

        public Trial()
        {
            Evals = new List<double>();
        }
    }
}