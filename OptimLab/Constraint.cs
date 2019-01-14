using System;
using System.Collections.Generic;
using System.Text;

namespace OptimLab
{
    public struct Constraint
    {
        public double A;
        public double B;
        public GrishaginFunction F;

        public Constraint(double A, double B, int I)
        {
            this.A = A;
            this.B = B;
            this.F = new GrishaginFunction(I);
        }
    }
}