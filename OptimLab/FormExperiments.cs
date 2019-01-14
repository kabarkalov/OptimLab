using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OptimLab
{
    public partial class FormExperiments : Form
    {
        public FormExperiments()
        {
            InitializeComponent();
        }

        public void Initialize(List<Experiment> experiments)
        {
            for (int i = 0; i < experiments.Count; i++)
            {
                string[] row = {
                                   experiments[i].MethodVisibleName,
                                   experiments[i].MethodOptions.GetValue("R").ToString(),
                                   experiments[i].MethodOptions.GetValue("Epsilon").ToString(),
                                   experiments[i].MethodOptions.GetValue("MaxIters").ToString()
                               };
                dataGridViewExperiments.Rows.Add(row);
            }
        }

        public List<int> GetIndices()
        {
            List<int> result = new List<int>();
            foreach (DataGridViewRow row in dataGridViewExperiments.SelectedRows)
            {
                result.Add(row.Index);
            }
            for (int i = 0; i < result.Count; i++)
                for (int j = i; j < result.Count; j++)
                {
                    if (result[i] > result[j])
                    {
                        int temp = result[i];
                        result[i] = result[j];
                        result[j] = temp;
                    }
                }
            return result;
        }
    }
}