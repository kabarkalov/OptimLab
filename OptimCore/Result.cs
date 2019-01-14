using System;
using System.Collections.Generic;
using System.Text;

namespace OptimLab
{
    public class Result
    {
        public double[] Point;
        public double Value;

        public int Iterations;
        public int TargetFunctionEvals;
        public int[] ConstraintEvals;

        public Result(Problem problem)
        {
            Point = new double[problem.Left.Length];
            ConstraintEvals = new int[problem.Constraints.Count];
        }

        public void AddTargetFunctionEval()
        {
            TargetFunctionEvals++;
        }

        public void AddConstraintEval(int constraint)
        {
            ConstraintEvals[constraint]++;
        }
    }
}