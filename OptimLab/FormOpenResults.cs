using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace OptimLab
{
    public enum ShowMode
    {
        All,
        Partial
    }

    public partial class FormOpenResults : Form
    {
        public FormOpenResults()
        {
            InitializeComponent();
        }

        private string fileName;

        public string FileName
        {
            get { return fileName; }
        }

        public ShowMode ShowMode
        {
            get
            {
                return radioButtonAll.Checked ? ShowMode.All : ShowMode.Partial;
            }
        }

        public int PartSize
        {
            get
            {
                int result = 0;
                if (radioButtonPartial.Checked)
                {
                    try { result = Int32.Parse(textBoxPartSize.Text); }
                    catch { result = 10; }
                    if (result < 1) result = 1;
                    if (result > 100) result = 100;
                    return result;
                }
                return result;
            }
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Application.StartupPath;
            dialog.Filter = "Файлы с результатами (*.opt) | *.opt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileName = dialog.FileName;
                textBoxFileName.Text = Path.GetFileName(dialog.FileName);
            }
        }

        private void radioButtonAll_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPartSize.Enabled = radioButtonPartial.Checked;
        }

        private void radioButtonPartial_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPartSize.Enabled = radioButtonPartial.Checked;
        }
    }
}