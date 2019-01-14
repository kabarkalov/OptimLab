using System;
using System.Collections.Generic;
using System.Text;

namespace OptimLab
{
    public abstract class Method
    {
        protected Problem problem;
        protected MethodOptions options;
        protected bool isStop;

        public double[] MaximumPoint;
        public bool IsOperationProperty;
        protected bool IsOperationStop;

        public abstract string GetMethodName();

        public abstract string GetGroupName();

        protected virtual void Reset()
        {
            isStop = false;
        }

        public virtual void SetProblem(Problem problem)
        {
            this.problem = problem;
            Reset();
        }

        public abstract MethodOptions GetDefaultOptions();

        public virtual void SetOptions(MethodOptions options)
        {
            this.options = options;
            Reset();
        }

        public abstract List<Trial> GetTrials();

        public abstract Result GetResult();

        public abstract void Solve();
    }
}