using System;
using System.Collections.Generic;
using System.Text;

namespace OptimLabInternal
{
    /// <summary>
    /// Испытание.
    /// </summary>
    public class Trial
    {
        /// <summary>
        /// Наибольшая размерность.
        /// </summary>
        public const int MaxNumberOfDimensions = 50;

        /// <summary>
        /// Наибольшее количество функций.
        /// </summary>
        public const int MaxNumberOfFunctions = 30;

        /// <summary>
        /// Номер ядра, проводившего испытание.
        /// </summary>
        public int Core;

        /// <summary>
        /// Прообраз.
        /// </summary>
        public double X;

        /// <summary>
        /// Точка.
        /// </summary>
        public double[] Point;

        /// <summary>
        /// Значения функций.
        /// </summary>
        public double[] CalculatedValues;

        /// <summary>
        /// Индекс.
        /// </summary>
        public int Index;

        /// <summary>
        /// Характеристика.
        /// </summary>
        public double R;

        /// <summary>
        /// Изменение прообраза.
        /// </summary>
        public double DeltaX;

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public Trial()
        {
            R = Double.MinValue;
        }

        /// <summary>
        /// Конструктор копирования.
        /// </summary>
        /// <param name="trial">Испытание-образец.</param>
        public Trial(Trial trial)
        {
            Core = trial.Core;
            X = trial.X;
            Index = trial.Index;
            R = trial.R;
            DeltaX = trial.DeltaX;

            Point = new double[trial.Point.Length];
            for (int i = 0; i < trial.Point.Length; i++)
                Point[i] = trial.Point[i];

            CalculatedValues = new double[trial.CalculatedValues.Length];
            for (int i = 0; i < trial.CalculatedValues.Length; i++)
                CalculatedValues[i] = trial.CalculatedValues[i];
        }
    }

    /// <summary>
    /// Параллельный индексный метод оптимизации.
    /// </summary>
    public class IndexMethodInternal
    {
        /// <summary>
        /// Список функций.
        /// </summary>
        public List<OptimLab.FunctionDelegate> Functions;

        /// <summary>
        /// Левая граница области поиска.
        /// </summary>
        public double[] Left;

        /// <summary>
        /// Правая граница области поиска.
        /// </summary>
        public double[] Right;

        /// <summary>
        /// Размерность.
        /// </summary>
        public int Dimension;

        /// <summary>
        /// Текущая точность вычислений.
        /// </summary>
        public double CurrentEpsilon;

        /// <summary>
        /// Количество разверток.
        /// </summary>
        public int L;

        /// <summary>
        /// Плотность развертки.
        /// </summary>
        public int Density;

        /// <summary>
        /// Тип развертки Пеано.
        /// </summary>
        public int TypeOfCurve;

        /// <summary>
        /// Параметр метода.
        /// </summary>
        public double Rv;

        /// <summary>
        /// Резервирование.
        /// </summary>
        public double Reserve;

        /// <summary>
        /// Номер шага применения резервирования.
        /// </summary>
        public int Kr;

        /// <summary>
        /// Испытания.
        /// </summary>
        public SortedList<double, Trial> Trials;

        /// <summary>
        /// Лучшее испытание.
        /// </summary>
        public Trial BestTrial;

        /// <summary>
        /// Количество вычислений функций.
        /// </summary>
        public int[] NumberOfEvaluations;

        /// <summary>
        /// Количество испытаний.
        /// </summary>
        public int NumberOfTrials;

        private List<List<Trial>> Iv;
        private double[] Mv;
        private double[] Zv;
        private int[] Tv;

        protected double[] Point;
        private int MaxIndex;
        private int PreimageIndex;
        private bool Stop;

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public IndexMethodInternal()
        {
            Left = new double[Trial.MaxNumberOfDimensions];
            Right = new double[Trial.MaxNumberOfDimensions];
            Point = new double[Trial.MaxNumberOfDimensions];
            Functions = new List<OptimLab.FunctionDelegate>();

            Trials = new SortedList<double, Trial>();
            Iv = new List<List<Trial>>();

            BestTrial = new Trial();
            BestTrial.Index = Trial.MaxNumberOfFunctions;
            BestTrial.CalculatedValues = new double[Trial.MaxNumberOfFunctions];
            for (int i = 0; i < Trial.MaxNumberOfFunctions; i++)
                BestTrial.CalculatedValues[i] = Double.MaxValue;
        }

        /// <summary>
        /// "Разверточное" ограничение.
        /// </summary>
        /// <param name="arguments">Аргументы.</param>
        /// <returns>Значение.</returns>
        private static double g0(double[] arguments)
        {
            // Результат.
            double result = double.MinValue;
            // Временная переменная.
            double temp;

            for (int i = 0; i < arguments.Length; i++)
            {
                temp = Math.Abs(arguments[i]) - 0.5;
                if (temp > result)
                    result = temp;
            }
            return result;
        }

        private void CalculateDeltaX(double x)
        {
            int j = Trials.IndexOfKey(x);
            int j1 = j - 1;
            Trials.Values[j].DeltaX = Math.Pow(Trials.Values[j].X - Trials.Values[j1].X, 1.0 / Dimension);
        }

        /// <summary>
        /// Вычисление характеристики.
        /// </summary>
        /// <param name="x">Прообраз.</param>
        /// <returns>Характеристика.</returns>
        private double CalculateR(double x)
        {
            Trial i = Trials.Values[Trials.IndexOfKey(x)];
            Trial i1 = Trials.Values[Trials.IndexOfKey(x) - 1];

            double result = 0.0;
            double deltax = i.DeltaX;
            int v = 0;

            if (i.Index < i1.Index)
            {
                v = i1.Index;
                result = 2.0 * deltax - 4.0 * (i1.CalculatedValues[v] - Zv[v]) / (Rv * Mv[v]);
            }
            if (i.Index > i1.Index)
            {
                v = i.Index;
                result = 2.0 * deltax - 4.0 * (i.CalculatedValues[v] - Zv[v]) / (Rv * Mv[v]);
            }
            if (i.Index == i1.Index)
            {
                v = i.Index;
                result = deltax + (i.CalculatedValues[v] - i1.CalculatedValues[v]) * (i.CalculatedValues[v] - i1.CalculatedValues[v]) /
                    (deltax * Mv[v] * Mv[v]) - 2 * (i.CalculatedValues[v] + i1.CalculatedValues[v] - 2.0 * Zv[v]) / (Rv * Mv[v]);
            }
            return result;
        }

        /// <summary>
        /// Заполнение множества Mv.
        /// </summary>
        /// <param name="x">Прообраз.</param>
        private void CalculateMv(double x)
        {
            Trial p = Trials[x];
            double deltax = 0.0;
            int tek_index = p.Index;

            if ((L > 1) && (tek_index == 0))
            {
                Mv[0] = 2.0 * Math.Sqrt(Dimension + 3.0);
                return;
            }

            Trial i;
            double rpr = 0.0;

            if (Trials.IndexOfKey(p.X) != 0)
            {
                deltax += p.DeltaX;
                i = Trials.Values[Trials.IndexOfKey(x) - 1];

                while (Trials.IndexOfKey(i.X) != 0)
                {
                    if (i.CalculatedValues[tek_index] < Double.MaxValue)
                        break;
                    deltax += i.DeltaX;
                    i = Trials.Values[Trials.IndexOfKey(i.X) - 1];
                }

                if (((Trials.IndexOfKey(i.X) != 0) ||
                    ((Trials.IndexOfKey(i.X) == 0) && (p.Index == i.Index))) &&
                    ((L == 1) ||
                    ((L > 1) && ((int)p.X == (int)i.X))))
                {
                    rpr = Math.Abs(i.CalculatedValues[tek_index] - p.CalculatedValues[tek_index]) / deltax;
                    if ((rpr > Mv[tek_index]) || ((Mv[tek_index] == 1.0) && (rpr > 0.0)))
                        Mv[tek_index] = rpr;
                }
            }

            deltax = 0.0;
            i = Trials.Values[Trials.IndexOfKey(x) + 1];

            while (Trials.IndexOfKey(i.X) != Trials.Count - 1)
            {
                deltax += i.DeltaX;
                if (i.CalculatedValues[tek_index] < Double.MaxValue)
                    break;
                i = Trials.Values[Trials.IndexOfKey(i.X) + 1];
            }

            if ((Trials.IndexOfKey(i.X) != Trials.Count - 1) &&
                ((L == 1) || ((L > 1) && (p.Index == i.Index))))
            {
                rpr = Math.Abs(i.CalculatedValues[tek_index] - p.CalculatedValues[tek_index]) / deltax;
                if ((rpr > Mv[tek_index]) || ((Mv[tek_index] == 1.0) && (rpr > 0.0)))
                    Mv[tek_index] = rpr;
            }
        }

        /// <summary>
        /// Проведение испытания.
        /// </summary>
        /// <param name="x">Прообраз.</param>
        private void MakeNewTrial(double x)
        {
            Trial trial = new Trial();

            trial.Point = new double[Dimension];
            trial.CalculatedValues = new double[Trial.MaxNumberOfFunctions];

            for (int i = 0; i < Functions.Count; i++)
                trial.CalculatedValues[i] = Double.MaxValue;

            // Прообраз на отрезке [0, L].
            trial.X = x;

            // Номер ядра, проводившего испытание.
            trial.Core = (int)Math.Floor(x);

            // Вычисление образа в стандартном гиперкубе.
            if (L == 1)
                Point = OptimLab.PeanoMap.CalculateImage(x, Dimension, Density);
            else
                Point = OptimLab.PeanoMap.CalculateImageMultiMap(x, Dimension, Density);

            // Вычисление всех прообразов.
            double[] xp = new double[L];
            int kp = 1;
            xp[0] = trial.X;

            double funcvalue;
            if (L == 1)
            {
                // Линейное преобразование - перевод координат из единичного гиперкуба на реальный.
                for (int i = 0; i < Dimension; i++)
                    Point[i] = Left[i] + (Right[i] - Left[i]) * (Point[i] + 0.5);

                for (int i = 0; i < Functions.Count; i++)
                {
                    NumberOfEvaluations[i]++;
                    funcvalue = Functions[i](Point);
                    trial.CalculatedValues[i] = funcvalue;
                    if (i == Functions.Count - 1)
                    {
                        trial.Index = Functions.Count - 1;
                        break;
                    }
                    if (funcvalue > 0.0)
                    {
                        trial.Index = i;
                        break;
                    }
                }
            }

            // Проверка "разверточного" (нулевого) ограничения.
            if (L > 1)
            {
                funcvalue = g0(Point);
                trial.CalculatedValues[0] = funcvalue;
                NumberOfEvaluations[0]++;

                if (funcvalue > 0.0)
                {
                    trial.Index = 0;
                    trial.CalculatedValues[0] = funcvalue;

                    // Линейное преобразование - перевод координат из единичного гиперкуба на реальный.
                    for (int i = 0; i < Dimension; i++)
                        Point[i] = Left[i] + (Right[i] - Left[i]) * (Point[i] + 0.5);
                }
                else
                {
                    xp = OptimLab.PeanoMap.CalculateAllPreimages(Point, Dimension, Density, L - 1);
                    kp = L;

                    // Линейное преобразование - перевод координат из единичного гиперкуба на реальный.
                    for (int i = 0; i < Dimension; i++)
                        Point[i] = Left[i] + (Right[i] - Left[i]) * (Point[i] + 0.5);

                    for (int i = 1; i < Functions.Count; i++)
                    {
                        NumberOfEvaluations[i]++;
                        funcvalue = Functions[i](Point);
                        trial.CalculatedValues[i] = funcvalue;
                        if (i == Functions.Count - 1)
                        {
                            trial.Index = Functions.Count - 1;
                            break;
                        }
                        if (funcvalue > 0.0)
                        {
                            trial.Index = i;
                            break;
                        }
                    }
                }
            }

            for (int d = 0; d < Dimension; d++)
                trial.Point[d] = Point[d];

            if (trial.Index > MaxIndex)
                MaxIndex = trial.Index;

            // Вставка прообразов.
            if (Trials.Count <= L)
            {
                trial.DeltaX = 1.0;
                trial.Index = -1;
                Trials.Add(trial.X, trial);
                PreimageIndex = trial.Index;
            }
            else
            {
                for (int i = 0; i < kp; i++)
                {
                    Trial ins = new Trial(trial);
                    ins.X = xp[i];

                    if (Trials.ContainsKey(ins.X))
                        Stop = true;
                    else
                        Trials.Add(ins.X, ins);

                    Trial j = Trials.Values[Trials.IndexOfKey(ins.X) + 1];
                    Trial j1 = Trials.Values[Trials.IndexOfKey(ins.X) - 1];

                    if (j.X - j1.X < CurrentEpsilon)
                        CurrentEpsilon = j.X - j1.X;

                    CalculateDeltaX(ins.X);
                    CalculateDeltaX(j.X);
                    CalculateMv(ins.X);
                    PreimageIndex = ins.Index;
                }
            }

            // Оценка значений.
            if ((trial.Index == MaxIndex) &&
                (trial.CalculatedValues[trial.Index] < BestTrial.CalculatedValues[trial.Index]))
            {
                BestTrial = trial;
                BestTrial.Index = Functions.Count - 1;
            }
        }

        /// <summary>
        /// Подготовка к запуску алгоритма.
        /// </summary>
        public void Prepare()
        {
            Trials.Clear();

            // Первоначальный индекс - не определен.
            MaxIndex = -2;

            // Добавление "разверточного" ограничения.
            if (L > 1)
                Functions.Insert(0, g0);

            // Инициализация информационных множеств.
            Tv = new int[Functions.Count];
            Mv = new double[Functions.Count];
            Zv = new double[Functions.Count];
            NumberOfEvaluations = new int[Functions.Count];

            for (int v = 0; v < Functions.Count; v++)
            {
                Iv.Add(new List<Trial>());
                Mv[v] = 1.0;
            }

            BestTrial.Index = Functions.Count - 1;
            CurrentEpsilon = Double.MaxValue;

            for (int l = 0; l <= L; l++)
                MakeNewTrial(l);
            MakeNewTrial(Math.Pow(0.5, Dimension + 2.0));

            NumberOfTrials = L + 2;
            Stop = false;
        }

        private void RecalculateRs()
        {
            int size = PreimageIndex + 1;

            // Предварительная очистка множества Iv.
            for (int v = 0; v < size; v++)
                Iv[v].Clear();

            // Заполнение множества Iv.
            for (int v = 0; v < size; v++)
            {
                for (int i = 0; i < Trials.Count; i++)
                {
                    if (Trials.Values[i].Index == v)
                    {
                        Iv[v].Add(new Trial(Trials.Values[i]));
                        break;
                    }
                }
            }

            // Заполнение множества Tv.
            for (int v = 0; v < Functions.Count - 1; v++)
            {
                if (v < MaxIndex)
                    Tv[v] = 1;
                else
                    Tv[v] = 0;
            }
            Tv[Functions.Count - 1] = 0;

            // Вычисление значений Zv.
            for (int v = 0; v < size; v++)
            {
                if (Iv[v].Count == 0)
                    continue;
                if (Tv[v] == 1)
                {
                    if (NumberOfTrials > Kr)
                    {
                        if (BestTrial.CalculatedValues[Functions.Count - 1] == Double.MaxValue)
                            Zv[v] = -Mv[v] * Reserve;
                        else
                            Zv[v] = (Mv[v] > Math.Abs(BestTrial.CalculatedValues[v])) ?
                                -Mv[v] * Reserve : -Math.Abs(BestTrial.CalculatedValues[0]) * Reserve;
                    }
                    else
                        Zv[v] = 0.0;
                    continue;
                }
                Zv[v] = BestTrial.CalculatedValues[v];
            }

            for (int i = 1; i < Trials.Count; i++)
                Trials.Values[i].R = CalculateR(Trials.Values[i].X);
        }

        /// <summary>
        /// Проведение итерации алгоритма.
        /// </summary>
        public void MakeIteration()
        {
            if (!Stop)
            {
                NumberOfTrials++;
                RecalculateRs();

                // Вычисление максимальной характеристики.
                double max = Double.MinValue;
                double value;
                int interval = 0;
                for (int i = 1; i < Trials.Count; i++)
                {
                    value = Trials.Values[i].R;
                    if (value > max)
                    {
                        max = value;
                        interval = i;
                    }
                }

                // Проведение испытания в интервале с максимальной характеристикой.
                Trial t = Trials.Values[interval];
                Trial t1 = Trials.Values[interval - 1];

                if ((t.Index != t1.Index) || ((t.Index == t1.Index) && (t.Index == -1)))
                    MakeNewTrial((t.X + t1.X) / 2.0);
                else
                    MakeNewTrial((t.X + t1.X) / 2.0 -
                        (((t.CalculatedValues[t.Index] - t1.CalculatedValues[t1.Index]) > 0) ? 1 : -1) *
                        Math.Pow(Math.Abs(t.CalculatedValues[t.Index] - t1.CalculatedValues[t1.Index]) / Mv[t.Index],
                        Dimension) / 2.0 / Rv);
            }
        }
    }
}