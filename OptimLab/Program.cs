using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OptimLab
{
    /// <summary>
    /// Основное приложение.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Запуск приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}