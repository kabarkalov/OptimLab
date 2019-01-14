using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OptimLab
{
    public partial class FormProblem : Form
    {
        public FormProblem()
        {
            InitializeComponent();
        }

        public int TargetFunctionIndex
        {
            get
            {
                int result = 0;
                try { result = Int32.Parse(textBoxTargetFunction.Text); }
                catch { result = 1; }
                if (result < 1) result = 1;
                if (result > 100) result = 100;
                return result;
            }
            set
            {
                textBoxTargetFunction.Text = value.ToString();
            }
        }

        public int NumConstraints
        {
            get
            {
                int result = 0;
                try { result = Int32.Parse(textBoxNumConstraints.Text); }
                catch { result = 0; }
                if (result < 0) result = 0;
                if (result > 3) result = 3;
                return result;
            }
            set
            {
                textBoxNumConstraints.Text = value.ToString();
            }
        }

        public bool RegenerateConstraints
        {
            get
            {
                return checkBoxRegenerate.Checked;
            }
        }
    }
}