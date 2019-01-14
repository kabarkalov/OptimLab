using System;
using System.Collections.Generic;
using System.Text;

namespace OptimLab
{
    public struct Problem
    {
        public double[] Left;
        public double[] Right;

        public FunctionDelegate TargetFunction;
        public List<FunctionDelegate> Constraints;
    }
}