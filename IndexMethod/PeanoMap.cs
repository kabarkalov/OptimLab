using System;
using System.Collections.Generic;
using System.Text;

namespace OptimLab
{
    /// <summary>
    /// Развертка Пеано.
    /// </summary>
    /// <remarks>Переводит отрезок [0; 1] в квадрат [-0.5; -0.5] x [0.5; 0.5].</remarks>
    public static class PeanoMap
    {
        private static int[] iu = new int[100];
        private static int[] iv = new int[100];
        private static int nexp;
        private static int n1;
        private static int iq;
        private static int l;

        private static void Node(int ip)
        {
            int iff = 0;
            int n = n1 + 1;
            int i = 0;
            int j = 0;
            int k1 = 0;
            int k2 = 0;

            if (ip == 0)
            {
                l = n1;
                for (i = 0; i < n; i++)
                {
                    iu[i] = -1;
                    iv[i] = -1;
                }
            }
            else
                if (ip == nexp - 1)
                {
                    l = n1;
                    iu[0] = 1;
                    iv[0] = 1;
                    for (i = 1; i < n; i++)
                    {
                        iu[i] = -1;
                        iv[i] = -1;
                    }
                    iv[n1] = 1;
                }
                else
                {
                    iff = nexp;
                    k1 = -1;
                    for (i = 0; i < n; i++)
                    {
                        iff /= 2;
                        if (ip >= iff)
                        {
                            if ((ip == iff) && (ip != 1))
                            {
                                l = i;
                                iq = -1;
                            }
                            ip -= iff;
                            k2 = 1;
                        }
                        else
                        {
                            k2 = -1;
                            if ((ip == iff - 1) && (ip != 0))
                            {
                                l = i;
                                iq = 1;
                            }
                        }
                        j = -k1 * k2;
                        iv[i] = j;
                        iu[i] = j;
                        k1 = k2;
                    }
                    iv[l] *= iq;
                    iv[n1] = -iv[n1];
                }
        }

        /// <summary>
        /// Вычисление образа отображения Пеано.
        /// </summary>
        /// <param name="x">Прообраз.</param>
        /// <param name="n">Размерность.</param>
        /// <param name="m">Степень детализации развертки.</param>
        /// <returns>Образ (центр квадрата).</returns>
        /// <remarks>m * n должно быть меньше 52.</remarks>
        public static double[] CalculateImage(double x, int n, int m)
        {
            double[] y = new double[n];
            int[] iw = new int[101];
            double d = 0.0;
            double dr = 0.0;
            double mne = 0.0;
            double p = 0.0;
            double r = 0.0;
            int it = 0;
            int ip = 0;
            int i = 0;
            int j = 0;
            int k = 0;

            n1 = n - 1;
            nexp = 1;
            for (i = 0; i < n; i++)
                nexp *= 2;
            d = x;
            r = 0.5;
            dr = nexp;
            mne = 1.0;
            for (i = 0; i < m; i++)
                mne *= dr;
            for (i = 0; i < n; i++)
            {
                iw[i] = 1;
                y[i] = 0.0;
            }
            for (j = 0; j < m; j++)
            {
                iq = 0;
                if (x == 1.0)
                {
                    ip = nexp - 1;
                    d = 0.0;
                }
                else
                {
                    d *= nexp;
                    ip = (int)d;
                    d -= ip;
                }
                Node(ip);
                k = iu[0];
                iu[0] = iu[it];
                iu[it] = k;
                k = iv[0];
                iv[0] = iv[it];
                iv[it] = k;
                if (l == 0)
                    l = it;
                else
                    if (l == it)
                        l = 0;
                if ((iq > 0) || ((iq == 0) && (ip == 0)))
                    k = l;
                else
                    if (iq < 0)
                        k = (it == n1) ? 0 : n1;
                r *= 0.5;
                it = l;
                for (i = 0; i < n; i++)
                {
                    iu[i] *= iw[i];
                    iw[i] *= -iv[i];
                    p = r * iu[i];
                    p += y[i];
                    y[i] = p;
                }
            }
            return y;
        }

        private static int Number()
        {
            int iff = nexp;
            int iss = 0;
            int n = n1 + 1;
            int l1 = 0;
            int k1 = -1;
            int k2 = 0;

            for (int i = 0; i < n; i++)
            {
                iff /= 2;
                k2 = -k1 * iu[i];
                iv[i] = iu[i];
                k1 = k2;
                if (k2 < 0)
                    l1 = i;
                else
                {
                    iss += iff;
                    l = i;
                }
            }
            if (iss == 0)
                l = n1;
            else
            {
                iv[n1] = -iv[n1];
                if (iss == nexp - 1)
                    l = n1;
                else
                    if (l1 == n1)
                        iv[l] = -iv[l];
                    else
                        l = l1;
            }
            return iss;
        }

        /// <summary>
        /// Вычисление прообраза отображения Пеано.
        /// </summary>
        /// <param name="y">Образ.</param>
        /// <param name="n">Размерность.</param>
        /// <param name="m">Степень детализации развертки.</param>
        /// <returns>Прообраз (левая граница отрезка).</returns>
        public static double CalculatePreimage(double[] y, int n, int m)
        {
            int[] iw = new int[101];
            double x = 0.0;
            double r = 0.5;
            double r1 = 1.0;
            int it = 0;
            int ip = 0;
            int t = 0;

            n1 = n - 1;
            nexp = 1;
            for (int i = 0; i < n; i++)
            {
                nexp *= 2;
                iw[i] = 1;
            }
            for (int j = 0; j < m; j++)
            {
                r *= 0.5;
                for (int i = 0; i < n; i++)
                {
                    iu[i] = (y[i] < 0) ? -1 : 1;
                    y[i] -= r * iu[i];
                    iu[i] *= iw[i];
                }
                t = iu[0];
                iu[0] = iu[it];
                iu[it] = t;
                ip = Number();
                t = iv[0];
                iv[0] = iv[it];
                iv[it] = t;
                for (int i = 0; i < n; i++)
                    iw[i] *= -iv[i];
                if (l == 0)
                    l = it;
                else
                    if (l == it)
                        l = 0;
                it = l;
                r1 /= nexp;
                x += r1 * ip;
            }
            return x;
        }

        /// <summary>
        /// Вычисление образа отображения Пеано для множественной развертки.
        /// </summary>
        /// <param name="x">Прообраз.</param>
        /// <param name="n">Размерность.</param>
        /// <param name="m">Степень детализации развертки.</param>
        /// <returns>Образ.</returns>
        public static double[] CalculateImageMultiMap(double x, int n, int m)
        {
            double[] y = new double[n];
            double del = 0.0;
            int intx = 0;

            intx = (int)x;
            x -= intx;
            if (intx != 0)
            {
                del = 1.0;
                for (int i = 0; i < intx; i++)
                    del /= 2.0;
            }
            y = CalculateImage(x, n, m + 1);
            for (int i = 0; i < n; i++)
                y[i] = 2 * y[i] + 0.5 - del;
            return y;
        }

        /// <summary>
        /// Вычисление всех прообразов отображения Пеано для множественной развертки.
        /// </summary>
        /// <param name="y">Образ.</param>
        /// <param name="n">Размерность.</param>
        /// <param name="m">Степень детализации развертки.</param>
        /// <param name="l">Наибольший индекс развертки.</param>
        /// <returns>Прообразы.</returns>
        public static double[] CalculateAllPreimages(double[] y, int n, int m, int l)
        {
            double[] xp = new double[l + 1];
            double[] p2 = new double[n];
            double del = 0.5;
            double xx = 0.0;

            for (int i = 0; i < l; i++)
            {
                for (int j = 0; j < n; j++)
                    p2[j] = (y[j] + del - 0.5) * 0.5;
                xx = CalculatePreimage(p2, n, m + 1);
                xp[i] = xx + i + 1;
                del *= 0.5;
            }
            del = 0.0;
            for (int j = 0; j < n; j++)
                p2[j] = (y[j] + del - 0.5) * 0.5;
            xx = CalculatePreimage(p2, n, m + 1);
            xp[0] = xx;
            return xp;
        }
    }
}