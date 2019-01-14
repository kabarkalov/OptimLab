using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OptimLab
{
    public struct InternalProblem
    {
        public double Y0;
        public List<FunctionDelegate> Functions;
        public SortedList<double, Trial> Trials;
        public double BestValue;
        public bool IsStop;

        public double MaxR;
        public int T;
    }

    public class ModReductionMethod : ReductionMethod
    {
        protected SortedList<double, InternalProblem> problems;
        protected double[] Mv;

        private StreamWriter file;

        public override string GetMethodName()
        {
            return "Адаптивная многошаговая схема";
        }

        private void AddProblem(int dim, double y0)
        {
            InternalProblem internalProblem = new InternalProblem();
            internalProblem.Y0 = y0;

            internalProblem.Functions = new List<FunctionDelegate>();
            if (dim == 0)
            {
                internalProblem.Functions.Add(delegate(double[] args)
                {
                    AddProblem(1, args[0]);
                    return problems[args[0]].BestValue;
                });
            }
            else
            {
                if (problem.Constraints.Count >= 1)
                {
                    internalProblem.Functions.Add(delegate(double[] args)
                    {
                        return problem.Constraints[0](new double[] { y0, args[0] });
                    });
                }
                if (problem.Constraints.Count >= 2)
                {
                    internalProblem.Functions.Add(delegate(double[] args)
                    {
                        return problem.Constraints[1](new double[] { y0, args[0] });
                    });
                }
                if (problem.Constraints.Count >= 3)
                {
                    internalProblem.Functions.Add(delegate(double[] args)
                    {
                        return problem.Constraints[2](new double[] { y0, args[0] });
                    });
                }
                internalProblem.Functions.Add(delegate(double[] args)
                {
                    return problem.TargetFunction(new double[] { y0, args[0] });
                });
            }

            internalProblem.Trials = new SortedList<double, Trial>();
            Trial trial = new Trial();
            trial.Index = 0;
            trial.Y = new double[1];
            trial.Y[0] = 0.0;
            internalProblem.Trials.Add(0.0, trial);

            trial = new Trial();
            trial.Index = 0;
            trial.Y = new double[1];
            trial.Y[0] = 1.0;
            internalProblem.Trials.Add(1.0, trial);

            trial = MakeTrial(dim, y0, 0.5, internalProblem.Functions);
            internalProblem.Trials.Add(0.5, trial);

            internalProblem.BestValue = internalProblem.Trials[0.5].Evals[internalProblem.Trials[0.5].Index - 1];
            internalProblem.IsStop = false;
            internalProblem.MaxR = 1.0;
            problems.Add(y0, internalProblem);
        }

        private void CalculateMv()
        {
            for (int v = 1; v < Mv.Length; v++)
            {
                Mv[v] = 0.0;
            }
            foreach (InternalProblem internalProblem in problems.Values)
            {
                List<int>[] Iv = new List<int>[internalProblem.Functions.Count + 1];
                for (int i = 0; i < internalProblem.Functions.Count + 1; i++)
                {
                    Iv[i] = new List<int>();
                }
                int V = Int32.MinValue;
                for (int i = 0; i < internalProblem.Trials.Count; i++)
                {
                    Iv[internalProblem.Trials.Values[i].Index].Add(i);
                    if (internalProblem.Trials.Values[i].Index > V)
                        V = internalProblem.Trials.Values[i].Index;
                }

                for (int v = 1; v <= internalProblem.Functions.Count; v++)
                {
                    double MvProblem = 0.0;
                    for (int j = 0; j < Iv[v].Count; j++)
                        for (int i = j + 1; i < Iv[v].Count; i++)
                        {
                            Trial Ti = internalProblem.Trials.Values[Iv[v][i]];
                            Trial Tj = internalProblem.Trials.Values[Iv[v][j]];
                            double temp = Math.Abs(Ti.Evals[Ti.Index - 1] - Tj.Evals[Tj.Index - 1]) /
                                (Ti.Y[0] - Tj.Y[0]);
                            if (temp > MvProblem)
                                MvProblem = temp;
                        }
                    if (MvProblem > Mv[v])
                        Mv[v] = MvProblem;
                }
            }
            for (int v = 1; v < Mv.Length; v++)
            {
                if (Mv[v] <= 1e-8)
                    Mv[v] = 1.0;
            }
        }

        private void CalculateRv()
        {
            CalculateMv();

            for (int k = 0; k < problems.Count; k++)
            {
                InternalProblem internalProblem = problems.Values[k];

                List<int>[] Iv = new List<int>[internalProblem.Functions.Count + 1];
                for (int i = 0; i < internalProblem.Functions.Count + 1; i++)
                {
                    Iv[i] = new List<int>();
                }
                int V = Int32.MinValue;
                for (int i = 0; i < internalProblem.Trials.Count; i++)
                {
                    Iv[internalProblem.Trials.Values[i].Index].Add(i);
                    if (internalProblem.Trials.Values[i].Index > V)
                        V = internalProblem.Trials.Values[i].Index;
                }

                double[] Zv = new double[internalProblem.Functions.Count + 1];
                for (int v = 1; v <= internalProblem.Functions.Count; v++)
                {
                    if (v < V)
                        Zv[v] = 0.0;
                    else
                    {
                        Zv[v] = Double.MaxValue;
                        for (int i = 0; i < Iv[v].Count; i++)
                        {
                            Trial Ti = internalProblem.Trials.Values[Iv[v][i]];
                            double temp = Ti.Evals[Ti.Index - 1];
                            if (temp < Zv[v])
                                Zv[v] = temp;
                        }
                    }
                }

                double[] Rv = new double[internalProblem.Trials.Count - 1];
                for (int i = 1; i < internalProblem.Trials.Count; i++)
                {
                    Trial Ti = internalProblem.Trials.Values[i];
                    Trial Ti1 = internalProblem.Trials.Values[i - 1];

                    double Zi = (i == internalProblem.Trials.Count - 1) ? 0 : Ti.Evals[Ti.Index - 1];
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

                internalProblem.T = 1;
                internalProblem.MaxR = Rv[0];
                for (int i = 1; i < internalProblem.Trials.Count; i++)
                {
                    if (Rv[i - 1] > Rv[internalProblem.T - 1])
                    {
                        internalProblem.T = i;
                        internalProblem.MaxR = Rv[i - 1];
                    }
                }

                problems[internalProblem.Y0] = internalProblem;
            }
        }

        private int FindBestProblem()
        {
            CalculateRv();

            int result = 0;
            double maxR = Double.MinValue;
            for (int i = 0; i < problems.Values.Count; i++)
            {
                if (problems.Values[i].MaxR > maxR)
                {
                    result = i;
                    maxR = problems.Values[i].MaxR;
                }
            }
            return result;
        }

        protected new Trial MakeTrial(int dim, double y0, double y, List<FunctionDelegate> functions)
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

                file.WriteLine("#" + result.Iterations + " y0=" + y0 + " y1=" + trial.Y[0] +
                    " z=" + temp.Evals[0] + " M=" + Mv[1]);

                if (trial.Index == functions.Count)
                {
                    if (temp.Evals[functions.Count - 1] < result.Value)
                    {
                        result.Point = temp.Y;
                        result.Value = temp.Evals[functions.Count - 1];
                    }
                }
            }

            return trial;
        }

        private void MakeIteration(ref InternalProblem internalProblem)
        {
            double y = 0.0;
            {
                Trial Tt = internalProblem.Trials.Values[internalProblem.T];
                Trial Tt1 = internalProblem.Trials.Values[internalProblem.T - 1];

                if (Tt.Y[0] - Tt1.Y[0] <= (double)options.GetValue("Epsilon"))
                    internalProblem.IsStop = true;

                double Zt = (internalProblem.T == internalProblem.Trials.Count - 1) ? 0 : Tt.Evals[Tt.Index - 1];
                double Zt1 = (internalProblem.T == 1) ? 0 : Tt1.Evals[Tt1.Index - 1];

                double R = (double)options.GetValue("R");

                if (Tt.Index != Tt1.Index)
                    y = (Tt.Y[0] + Tt1.Y[0]) / 2;
                else
                    y = (Tt.Y[0] + Tt1.Y[0]) / 2 - (Zt - Zt1) / (2 * R * Mv[Tt.Index]);
            }

            Trial trial = MakeTrial(Double.IsInfinity(internalProblem.Y0) ? 0 : 1,
                internalProblem.Y0, y, internalProblem.Functions);
            if (trial.Index == internalProblem.Functions.Count)
            {
                if (trial.Evals[internalProblem.Functions.Count - 1] < internalProblem.BestValue)
                    internalProblem.BestValue = trial.Evals[internalProblem.Functions.Count - 1];
            }
            internalProblem.Trials.Add(y, trial);
        }

        public override void Solve()
        {
            file = new StreamWriter("Log.txt");

            result = new Result(problem);
            Mv = new double[problem.Constraints.Count + 1 + 1];

            problems = new SortedList<double, InternalProblem>();
            AddProblem(0, Double.NegativeInfinity);
            do
            {
                int bestProblemIndex = FindBestProblem();
                InternalProblem bestProblem = problems.Values[bestProblemIndex];

                MakeIteration(ref bestProblem);

                problems[problems.Values[bestProblemIndex].Y0] = bestProblem;
                foreach (Trial trial in problems.Values[0].Trials.Values)
                {
                    if (trial.Y[0] == bestProblem.Y0)
                        trial.Evals[0] = bestProblem.BestValue;
                }
            }
            while ((result.Iterations <= (int)options.GetValue("MaxIters")) && !problems.Values[0].IsStop);

            file.Close();
        }
    }
}