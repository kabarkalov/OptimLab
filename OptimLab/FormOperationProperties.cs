using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OptimLab
{
    public partial class FormOperationProperties : Form
    {
        public FormOperationProperties()
        {
            InitializeComponent();
        }

        public void AddLegendRecord(Experiment experiment, Color color)
        {
            string[] row = {
                               "",
                               experiment.MethodVisibleName,
                               experiment.MethodOptions.GetValue("R").ToString(),
                               experiment.MethodOptions.GetValue("Epsilon").ToString(),
                               experiment.MethodOptions.GetValue("MaxIters").ToString()
                           };
            dataGridViewLegend.Rows.Add(row);
            dataGridViewLegend.Rows[dataGridViewLegend.Rows.Count - 1].Cells[0].Style.BackColor = color;
            dataGridViewLegend.Rows[dataGridViewLegend.Rows.Count - 1].Cells[0].Style.SelectionBackColor = color;
        }
    }
}