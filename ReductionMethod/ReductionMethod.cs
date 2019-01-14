using System;
using System.Collections.Generic;
using System.Text;

namespace OptimLab
{
    public class ReductionMethod : Method
    {
        protected List<Trial> trials;
        protected Result result;

        public ReductionMethod()
        {
            trials = new List<Trial>();
        }

        public override string GetMethodName()
        {
            return "Многошаговая схема";
        }

        public override string GetGroupName()
        {
            return "Редукция размерности";
        }

        public override MethodOptions GetDefaultOptions()
        {
            return new ReductionMethodOptions();
        }

        public override List<Trial> GetTrials()
        {
            return trials;
        }

        public override Result GetResult()
        {
            return result;
        }

        protected Trial MakeTrial(int dim, double y0, double y, List<FunctionDelegate> functions)
        {
            Trial trial = new Trial();
            trial.Y = new double[1];
            trial.Y[0] = y;

            for (int i = 0; i < functions.Count; i++)
            {
                double temp = functions[i](trial.Y);
                trial.Evals.Add(temp);
                if (dim == 1)
                {
                    if (i == functions.Count - 1)
                        result.AddTargetFunctionEval();
                    else
                        result.AddConstraintEval(i);
                }
                if ((i == functions.Count - 1) || (temp > 0))
                {
                    trial.Index = i + 1;
                    break;
                }
            }

            if (dim == 1)
            {
                Trial temp = new Trial();
                temp.Y = new double[2] { y0, trial.Y[0] };
                temp.Index = trial.Index - 1;
                temp.Evals = trial.Evals;
                trials.Add(temp);

                result.Iterations++;
                if (trial.Index == functions.Count)
                {
                    if (temp.Evals[functions.Count - 1] < result.Value)
                    {
                        result.Point = temp.Y;
                        result.Value = temp.Evals[functions.Count - 1];
                    }
                    if (IsOperationProperty &&
                        (Math.Abs(result.Point[0] - MaximumPoint[0]) <= (double)options.GetValue("Epsilon")) &&
                        (Math.Abs(result.Point[1] - MaximumPoint[1]) <= (double)options.GetValue("Epsilon")))
                    {
                        IsOperationStop = true;
                    }
                }
            }
            return trial;
        }

        private double Solve(int dim, double y0)
        {
            List<FunctionDelegate> functions = new List<FunctionDelegate>();
            if (dim == 0)
            {
                functions.Add(delegate(double[] args)
                {
                    return Solve(1, args[0]);
                });
            }
            else
            {
                if (problem.Constraints.Count >= 1)
                {
                    functions.Add(delegate(double[] args)
                    {
                        return problem.Constraints[0](new double[] { y0, args[0] });
                    });
                }
                if (problem.Constraints.Count >= 2)
                {
                    functions.Add(delegate(double[] args)
                    {
                        return problem.Constraints[1](new double[] { y0, args[0] });
                    });
                }
                if (problem.Constraints.Count >= 3)
                {
                    functions.Add(delegate(double[] args)
                    {
                        return problem.Constraints[2](new double[] { y0, args[0] });
                    });
                }
                functions.Add(delegate(double[] args)
                {
                    return problem.TargetFunction(new double[] { y0, args[0] });
                });
            }

            SortedList<double, Trial> trials = new SortedList<double, Trial>();

            Trial trial = new Trial();
            trial.Index = 0;
            trial.Y = new double[1];
            trial.Y[0] = 0.0;
            trials.Add(0.0, trial);

            trial = new Trial();
            trial.Index = 0;
            trial.Y = new double[1];
            trial.Y[0] = 1.0;
            trials.Add(1.0, trial);

            trial = MakeTrial(dim, y0, 0.5, functions);
            trials.Add(0.5, trial);

            double bestValue = Double.MaxValue;

            do
            {
                List<int>[] Iv = new List<int>[functions.Count + 1];
                for (int i = 0; i < functions.Count + 1; i++)
                {
                    Iv[i] = new List<int>();
                }
                int V = Int32.MinValue;
                for (int i = 0; i < trials.Count; i++)
                {
                    Iv[trials.Values[i].Index].Add(i);
                    if (trials.Values[i].Index > V)
                        V = trials.Values[i].Index;
                }

                double[] Mv = new double[functions.Count + 1];
                for (int v = 1; v <= functions.Count; v++)
                {
                    Mv[v] = 0.0;
                    for (int j = 0; j < Iv[v].Count; j++)
                        for (int i = j + 1; i < Iv[v].Count; i++)
                        {
                            Trial Ti = trials.Values[Iv[v][i]];
                            Trial Tj = trials.Values[Iv[v][j]];
                            double temp = Math.Abs(Ti.Evals[Ti.Index - 1] - Tj.Evals[Tj.Index - 1]) /
                                (Ti.Y[0] - Tj.Y[0]);
                            if (temp > Mv[v])
                                Mv[v] = temp;
                        }
                    if (Mv[v] <= 1e-8)
                        Mv[v] = 1.0;
                }

                double[] Zv = new double[functions.Count + 1];
                for (int v = 1; v <= functions.Count; v++)
                {
                    if (v < V)
                        Zv[v] = 0.0;
                    else
                    {
                        Zv[v] = Double.MaxValue;
                        for (int i = 0; i < Iv[v].Count; i++)
                        {
                            Trial Ti = trials.Values[Iv[v][i]];
                            double temp = Ti.Evals[Ti.Index - 1];
                            if (temp < Zv[v])
                                Zv[v] = temp;
                        }
                    }
                }

                double[] Rv = new double[trials.Count - 1];
                for (int i = 1; i < trials.Count; i++)
                {
                    Trial Ti = trials.Values[i];
                    Trial Ti1 = trials.Values[i - 1];

                    double Zi = (i == trials.Count - 1) ? 0 : Ti.Evals[Ti.Index - 1];
                    double Zi1 = (i == 1) ? 0 : Ti1.Evals[Ti1.Index - 1];

                    double D = Ti.Y[0] - Ti1.Y[0];
                    double R = (double)options.GetValue("R");

                    if (Ti.Index < Ti1.Index)
                        Rv[i - 1] = 2 * D - 4 * (Zi1 - Zv[Ti1.Index]) / (Mv[Ti1.Index] * R);
                    if (Ti.Index > Ti1.Index)
                        Rv[i - 1] = 2 * D - 4 * (Zi - Zv[Ti.Index]) / (Mv[Ti.Index] * R);
                    if (Ti.Index == Ti1.Index)
                        Rv[i - 1] = D + (Zi - Zi1) * (Zi - Zi1) / (D * R * R * Mv[Ti.Index] * Mv[Ti.Index]) -
                            2 * (Zi + Zi1 - 2 * Zv[Ti.Index]) / (R * Mv[Ti.Index]);
                }

                int t = 1;
                for (int i = 1; i < trials.Count; i++)
                {
                    if (Rv[i - 1] > Rv[t - 1])
                        t = i;
                }

                double y = 0.0;
                {
                    Trial Tt = trials.Values[t];
                    Trial Tt1 = trials.Values[t - 1];

                    //if ((dim == 1) || !IsOperationProperty)
                    //{
                        if (Tt.Y[0] - Tt1.Y[0] <= (double)options.GetValue("Epsilon"))
                            break;
                    //}

                    double Zt = (t == trials.Count - 1) ? 0 : Tt.Evals[Tt.Index - 1];
                    double Zt1 = (t == 1) ? 0 : Tt1.Evals[Tt1.Index - 1];

                    double R = (double)options.GetValue("R");

                    if (Tt.Index != Tt1.Index)
                        y = (Tt.Y[0] + Tt1.Y[0]) / 2;
                    else
                        y = (Tt.Y[0] + Tt1.Y[0]) / 2 - (Zt - Zt1) / (2 * R * Mv[Tt.Index]);
                }

                trial = MakeTrial(dim, y0, y, functions);
                if (trial.Index == functions.Count)
                {
                    if (trial.Evals[functions.Count - 1] < bestValue)
                        bestValue = trial.Evals[functions.Count - 1];
                }
                trials.Add(y, trial);

                /*if (IsOperationProperty && IsOperationStop)
                    break;*/
            }
            while (result.Iterations <= (int)options.GetValue("MaxIters"));
            return bestValue;
        }

        public override void Solve()
        {
            result = new Result(problem);
            Solve(0, Double.NegativeInfinity);
        }
    }
}