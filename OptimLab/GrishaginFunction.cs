using System;
using System.Collections.Generic;
using System.Text;

namespace OptimLab
{
    /// <summary>
    /// Функция Гришагина.
    /// </summary>
    public class GrishaginFunction
    {
        private double[,] af = new double[7, 7];
        private double[,] bf = new double[7, 7];
        private double[,] cf = new double[7, 7];
        private double[,] df = new double[7, 7];
        private double[] snx = new double[7];
        private double[] csx = new double[7];
        private double[] sny = new double[7];
        private double[] csy = new double[7];
        private int[] icnf = new int[45];
        private int index;

        private static int[,] matCon = new int[10, 45]
            {
                {0,0,1,1,1,0,1,0,1,1,0,0,1,0,1,1,1,1,0,1,1,0,1,0,0,1,0,1,0,1,0,1,1,0,1,0,1,0,1,1,1,1,0,0,0},
                {0,1,1,1,0,1,0,0,1,0,0,1,1,0,0,0,0,1,0,0,1,1,1,1,1,0,1,0,1,1,0,0,0,0,1,0,1,1,0,0,1,0,1,1,1},
                {1,1,0,0,0,0,0,0,0,1,1,1,0,0,0,0,1,1,0,1,1,0,0,1,1,0,0,0,0,1,0,1,1,0,0,1,1,1,0,1,0,0,1,0,0},
                {0,0,1,0,1,1,0,0,0,0,0,1,0,1,0,1,0,0,0,1,1,1,0,0,1,1,1,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0},
                {1,0,1,0,0,1,1,0,1,0,1,0,1,1,0,1,1,0,0,0,0,1,1,0,1,1,0,1,1,1,0,0,0,1,1,0,0,0,1,0,0,0,0,1,1},
                {0,1,0,0,0,1,1,0,0,1,0,1,0,0,0,0,0,0,1,0,1,1,0,1,1,1,0,0,0,0,0,0,1,1,1,0,1,1,1,1,1,1,0,1,0},
                {1,0,0,1,1,1,0,1,1,0,1,0,1,1,0,0,0,0,0,1,1,1,1,1,0,1,1,1,1,0,0,1,0,1,0,0,0,0,0,0,0,0,1,0,0},
                {0,1,1,1,1,1,1,1,1,1,0,1,0,0,0,0,0,1,0,0,0,1,0,1,1,0,0,0,1,0,0,1,1,1,0,0,1,1,1,0,1,1,1,1,1},
                {1,1,1,0,0,1,1,1,0,1,0,0,0,1,1,1,1,1,1,0,0,1,1,1,1,1,0,0,1,1,1,1,0,1,0,1,0,1,0,0,0,1,1,0,1},
                {1,0,0,1,1,1,0,1,1,1,1,0,1,1,1,0,1,1,0,0,0,1,1,1,1,1,0,0,0,0,0,0,1,1,0,1,1,0,1,0,0,0,0,0,0},
            };

        private static double[,] maximumPoints = new double[100, 2]
            {
                {0.603052, 0.408337},
                {0.652988, 0.320592},
                {1.000000, 0.000000},
                {0.066182, 0.582587},
                {0.904308, 0.872639},
                {0.344375, 0.524932},
                {0.000000, 1.000000},
                {0.948275, 0.887031},
                {0.226047, 0.520153},
                {0.341732, 0.197620},
                {0.069264, 0.430955},
                {0.000000, 1.000000},
                {0.452210, 0.072920},
                {0.579769, 0.046396},
                {0.000000, 1.000000},
                {0.310179, 1.000000},
                {0.909758, 0.926195},
                {0.434562, 0.825608},
                {0.066860, 0.770510},
                {0.641337, 0.135186},
                {0.885029, 0.390289},
                {0.649650, 0.414282},
                {0.142623, 0.157327},
                {0.862953, 1.000000},
                {0.460360, 0.993140},
                {0.379189, 0.688051},
                {0.845292, 0.424546},
                {0.441160, 0.016803},
                {1.000000, 1.000000},
                {0.303295, 0.134722},
                {0.109520, 0.265486},
                {1.000000, 0.000000},
                {0.593726, 0.503014},
                {0.694905, 1.000000},
                {0.051975, 0.409344},
                {0.125664, 0.518969},
                {0.000000, 0.000000},
                {0.155081, 0.238663},
                {0.537070, 0.461810},
                {0.110985, 0.917791},
                {1.000000, 0.000000},
                {0.776095, 0.764724},
                {0.087367, 0.677632},
                {0.308037, 0.536113},
                {0.042100, 0.563607},
                {0.287025, 0.159219},
                {0.451926, 0.169839},
                {0.884761, 0.245341},
                {0.047782, 0.171633},
                {0.000000, 0.415960},
                {0.192108, 0.303789},
                {0.554153, 0.809821},
                {0.914750, 0.541490},
                {0.661592, 0.925902},
                {0.962924, 0.436680},
                {0.000000, 0.000000},
                {0.616058, 0.560244},
                {0.439890, 0.343722},
                {0.218146, 0.677192},
                {1.000000, 1.000000},
                {0.198145, 0.317876},
                {0.875874, 0.653336},
                {0.229990, 0.336240},
                {0.169351, 0.015656},
                {0.760073, 0.906035},
                {0.702941, 0.308403},
                {0.365371, 0.282325},
                {0.314012, 0.651377},
                {0.237687, 0.374368},
                {0.586334, 0.508672},
                {0.000000, 0.000000},
                {0.383319, 1.000000},
                {0.780103, 0.103783},
                {0.350265, 0.566946},
                {0.798535, 0.478706},
                {0.317590, 0.069670},
                {0.715929, 0.704778},
                {0.563040, 0.442557},
                {0.565078, 0.322618},
                {0.146731, 0.510509},
                {0.000000, 0.543167},
                {0.208533, 0.454252},
                {0.155111, 0.972329},
                {0.000000, 1.000000},
                {0.336467, 0.909056},
                {0.570010, 0.908470},
                {0.296290, 0.540579},
                {0.172262, 0.332732},
                {0.000000, 1.000000},
                {1.000000, 0.000000},
                {1.000000, 1.000000},
                {0.674061, 0.869954},
                {1.000000, 1.000000},
                {0.852506, 0.637278},
                {0.877491, 0.399780},
                {0.835605, 0.751888},
                {0.673378, 0.827427},
                {0.831754, 0.367117},
                {0.601971, 0.734465},
                {0.000000, 0.000000}
            };

        /// <summary>
        /// Индекс функции.
        /// </summary>
        public int Index
        {
            get
            {
                return index;
            }
        }

        /// <summary>
        /// Точка максимума функции.
        /// </summary>
        public double[] MaximumPoint
        {
            get
            {
                // Результат.
                double[] result = new double[2];

                result[0] = maximumPoints[index - 1, 0];
                result[1] = maximumPoints[index - 1, 1];
                return result;
            }
        }

        /// <summary>
        /// Наибольшее значение функции.
        /// </summary>
        public double MaximumValue
        {
            get
            {
                return CalculateFunction(MaximumPoint);
            }
        }

        /// <summary>
        /// Наименьшее значение функции.
        /// </summary>
        public double MinimumValue
        {
            get
            {
                double result = Double.MaxValue;
                for (int i = 0; i <= 100; i++)
                    for (int j = 0; j <= 100; j++)
                    {
                        double temp = CalculateFunction(new double[] { i / 100.0, j / 100.0 });
                        if (temp < result)
                            result = temp;
                    }
                return result;
            }
        }

        private void Generate(int[] k, int[] k1, int cap1, int cap2)
        {
            int jct = 0;
            int j = 0;

            for (int i = cap2; i >= cap1; i--)
            {
                j = (k[i] + k1[i] + jct) / 2;
                k[i] += k1[i] + jct - j * 2;
                jct = j;
            }
            if (jct != 0)
                for (int i = cap2; i >= cap1; i--)
                {
                    j = (k[i] + jct) / 2;
                    k[i] += jct - j * 2;
                    jct = j;
                }
        }

        private double Random(int[] k)
        {
            int[] k1 = new int[45];
            double de2 = 0.0;
            double result = 0.0;

            for (int i = 0; i < 38; i++)
                k1[i] = k[i + 7];
            for (int i = 38; i < 45; i++)
                k1[i] = 0;
            for (int i = 0; i < 45; i++)
                k[i] = Math.Abs(k[i] - k1[i]);
            for (int i = 27; i < 45; i++)
                k1[i] = k[i - 27];
            for (int i = 0; i < 27; i++)
                k1[i] = 0;

            Generate(k, k1, 9, 44);
            Generate(k, k1, 0, 8);

            result = 0.0;
            de2 = 1.0;
            for (int i = 0; i < 36; i++)
            {
                de2 = de2 / 2.0;
                result += k[i + 9] * de2;
            }
            return result;
        }

        /// <summary>
        /// Конструктор по индексу функции.
        /// </summary>
        /// <param name="index">Индекс функции (от 1 до 100).</param>
        public GrishaginFunction(int index)
        {
            if ((index < 1) || (index > 100))
                throw new ArgumentOutOfRangeException("index", index, "Неверный индекс функции Гришагина");
            this.index = index;

            int lst = 10;
            int i1 = (index - 1) / lst;
            int i2 = i1 * lst;
            int i3 = 0;

            for (int i = 0; i < 45; i++)
                icnf[i] = matCon[i1, i];
            if (i2 != index - 1)
            {
                i3 = index - 1 - i2;
                for (int i = 1; i <= i3; i++)
                    for (int j = 0; j < 196; j++)
                        Random(icnf);
            }
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                {
                    af[j, i] = 2.0 * Random(icnf) - 1.0;
                    cf[j, i] = 2.0 * Random(icnf) - 1.0;
                }
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                {
                    bf[j, i] = 2.0 * Random(icnf) - 1.0;
                    df[j, i] = 2.0 * Random(icnf) - 1.0;
                }
        }

        /// <summary>
        /// Вычисление функции.
        /// </summary>
        /// <param name="args">Точка.</param>
        /// <returns>Значение функции.</returns>
        public double CalculateFunction(double[] point)
        {
            double d1 = Math.PI * point[0];
            double d2 = Math.PI * point[1];
            double sx = Math.Sin(d1);
            double cx = Math.Cos(d1);
            double sy = Math.Sin(d2);
            double cy = Math.Cos(d2);

            snx[0] = sx;
            csx[0] = cx;
            sny[0] = sy;
            csy[0] = cy;
            for (int i = 0; i < 6; i++)
            {
                snx[i + 1] = snx[i] * cx + csx[i] * sx;
                csx[i + 1] = csx[i] * cx - snx[i] * sx;
                sny[i + 1] = sny[i] * cy + csy[i] * sy;
                csy[i + 1] = csy[i] * cy - sny[i] * sy;
            }
            d1 = 0.0;
            d2 = 0.0;
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                {
                    d1 += af[i, j] * snx[i] * sny[j] + bf[i, j] * csx[i] * csy[j];
                    d2 += cf[i, j] * snx[i] * sny[j] - df[i, j] * csx[i] * csy[j];
                }
            return Math.Sqrt(d1 * d1 + d2 * d2);
        }

        /// <summary>
        /// Вычисление частной производной по X.
        /// </summary>
        /// <param name="args">Точка.</param>
        /// <returns>Значение частной производной по X.</returns>
        public double CalculateDerivativeX(double[] point)
        {
            double dd = 0.0;
            double d1 = Math.PI * point[0];
            double d2 = Math.PI * point[1];
            double sx = Math.Sin(d1);
            double cx = Math.Cos(d1);
            double sy = Math.Sin(d2);
            double cy = Math.Cos(d2);
            double t1 = 0.0;
            double t2 = 0.0;

            snx[0] = sx;
            csx[0] = cx;
            sny[0] = sy;
            csy[0] = cy;
            for (int i = 0; i < 6; i++)
            {
                snx[i + 1] = snx[i] * cx + csx[i] * sx;
                csx[i + 1] = csx[i] * cx - snx[i] * sx;
                sny[i + 1] = sny[i] * cy + csy[i] * sy;
                csy[i + 1] = csy[i] * cy - sny[i] * sy;
            }
            d1 = 0.0;
            d2 = 0.0;
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                {
                    d1 += af[i, j] * snx[i] * sny[j] + bf[i, j] * csx[i] * csy[j];
                    d2 += cf[i, j] * snx[i] * sny[j] - df[i, j] * csx[i] * csy[j];
                }
            dd = Math.Sqrt(d1 * d1 + d2 * d2);
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                {
                    t1 += af[i, j] * Math.PI * (i + 1) * csx[i] * sny[j];
                    t1 -= bf[i, j] * Math.PI * (i + 1) * snx[i] * csy[j];
                    t2 += cf[i, j] * Math.PI * (i + 1) * csx[i] * sny[j];
                    t2 += df[i, j] * Math.PI * (i + 1) * snx[i] * csy[j];
                }
            return (t1 * d1 + t2 * d2) / dd;
        }

        /// <summary>
        /// Вычисление частной производной по Y.
        /// </summary>
        /// <param name="args">Точка.</param>
        /// <returns>Значение частной производной по Y.</returns>
        public double CalculateDerivativeY(double[] point)
        {
            double dd = 0.0;
            double d1 = Math.PI * point[0];
            double d2 = Math.PI * point[1];
            double sx = Math.Sin(d1);
            double cx = Math.Cos(d1);
            double sy = Math.Sin(d2);
            double cy = Math.Cos(d2);
            double t1 = 0.0;
            double t2 = 0.0;

            snx[0] = sx;
            csx[0] = cx;
            sny[0] = sy;
            csy[0] = cy;
            for (int i = 0; i < 6; i++)
            {
                snx[i + 1] = snx[i] * cx + csx[i] * sx;
                csx[i + 1] = csx[i] * cx - snx[i] * sx;
                sny[i + 1] = sny[i] * cy + csy[i] * sy;
                csy[i + 1] = csy[i] * cy - sny[i] * sy;
            }
            d1 = 0.0;
            d2 = 0.0;
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                {
                    d1 += af[i, j] * snx[i] * sny[j] + bf[i, j] * csx[i] * csy[j];
                    d2 += cf[i, j] * snx[i] * sny[j] - df[i, j] * csx[i] * csy[j];
                }
            dd = Math.Sqrt(d1 * d1 + d2 * d2);
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                {
                    t1 += af[i, j] * Math.PI * (j + 1) * snx[i] * csy[j];
                    t1 -= bf[i, j] * Math.PI * (j + 1) * csx[i] * sny[j];
                    t2 += cf[i, j] * Math.PI * (j + 1) * snx[i] * csy[j];
                    t2 += df[i, j] * Math.PI * (j + 1) * csx[i] * sny[j];
                }
            return (t1 * d1 + t2 * d2) / dd;
        }
    }
}