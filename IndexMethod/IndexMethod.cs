using System;
using System.Collections.Generic;
using System.Text;

namespace OptimLab
{
    public class IndexMethod : Method
    {
        protected OptimLabInternal.IndexMethodInternal internalMethod;
        protected int iteration;

        public IndexMethod()
        {
            internalMethod = new OptimLabInternal.IndexMethodInternal();
        }

        public override string GetMethodName()
        {
            return "Развертка";
        }

        public override string GetGroupName()
        {
            return "Редукция размерности";
        }

        protected override void Reset()
        {
            base.Reset();
            iteration = 0;
        }

        public override void SetProblem(Problem problem)
        {
            base.SetProblem(problem);

            internalMethod.Dimension = 2;
            internalMethod.Left = problem.Left;
            internalMethod.Right = problem.Right;

            internalMethod.Functions = new List<FunctionDelegate>();
            for (int i = 0; i < problem.Constraints.Count; i++)
                internalMethod.Functions.Add(problem.Constraints[i]);
            internalMethod.Functions.Add(problem.TargetFunction);
        }

        public override MethodOptions GetDefaultOptions()
        {
            return new IndexMethodOptions();
        }

        public override void SetOptions(MethodOptions options)
        {
            base.SetOptions(options);

            internalMethod.L = 1;
            internalMethod.Density = (int)options.GetValue("Density");
            internalMethod.Rv = (double)options.GetValue("R");
            internalMethod.Reserve = 0.0;
            internalMethod.Kr = 2000;
        }

        public override List<Trial> GetTrials()
        {
            SortedList<double, OptimLabInternal.Trial> trials = internalMethod.Trials;
            List<Trial> result = new List<Trial>();
            foreach (OptimLabInternal.Trial trial in trials.Values)
            {
                Trial temp = new Trial();
                temp.X = trial.X;
                temp.Y = trial.Point;
                temp.Index = trial.Index;
                for (int j = 0; j < problem.Constraints.Count + 1; j++)
                    temp.Evals.Add(trial.CalculatedValues[j]);
                result.Add(temp);
            }
            return result;
        }

        public override Result GetResult()
        {
            Result result = new Result(problem);
            result.Point = internalMethod.BestTrial.Point;
            result.Value = internalMethod.BestTrial.CalculatedValues[
                internalMethod.BestTrial.CalculatedValues.Length - 1];

            IList<OptimLabInternal.Trial> trials = internalMethod.Trials.Values;
            for (int i = 0; i < trials.Count; i++)
            {
                result.Iterations++;
                if (trials[i].Index == problem.Constraints.Count)
                {
                    for (int j = 0; j < problem.Constraints.Count; j++)
                        result.AddConstraintEval(j);
                    result.AddTargetFunctionEval();
                }
                else if (trials[i].Index >= 0)
                {
                    for (int j = 0; j < trials[i].Index + 1; j++)
                        result.AddConstraintEval(j);
                }
            }
            if (problem.Constraints.Count == 0)
                result.TargetFunctionEvals += 2;
            else
                result.ConstraintEvals[0] += 2;
            return result;
        }

        public void MakeIteration()
        {
            if (iteration == 0)
                internalMethod.Prepare();

            internalMethod.MakeIteration();
            iteration++;

            isStop = (iteration > (int)options.GetValue("MaxIters")) ||
                (internalMethod.CurrentEpsilon <=
                (double)options.GetValue("Epsilon") * (double)options.GetValue("Epsilon"));
        }

        public override void Solve()
        {
            while (!isStop)
                MakeIteration();
        }
    }
}