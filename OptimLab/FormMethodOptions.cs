using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace OptimLab
{
    public partial class FormMethodOptions : Form
    {
        private List<Label> labels;
        private List<TextBox> textBoxes;

        public FormMethodOptions()
        {
            InitializeComponent();

            labels = new List<Label>();
            textBoxes = new List<TextBox>();
        }

        public void GetMethodOptions(ref MethodOptions methodOptions)
        {
            for (int i = 0; i < textBoxes.Count; i++)
            {
                methodOptions.SetValue(textBoxes[i].Name.Substring(7), textBoxes[i].Text);
            }
        }

        public void SetMethodOptions(MethodOptions methodOptions)
        {
            List<string> names = methodOptions.GetNames();
            for (int i = 0; i < names.Count; i++)
            {
                Label label = new Label();
                label.Name = "label" + names[i];
                label.Text = methodOptions.GetDescription(names[i]) + ":";
                label.AutoSize = true;
                label.Location = new Point(10, 20 + i * 25);
                labels.Add(label);
                Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Name = "textBox" + names[i];

                object value = methodOptions.GetValue(names[i]);
                if (value is Double)
                    textBox.Text = ((double)value).ToString(CultureInfo.InvariantCulture);
                else
                    textBox.Text = value.ToString();

                textBox.Location = new Point(220, 20 + i * 25);
                textBoxes.Add(textBox);
                Controls.Add(textBox);
            }
        }
    }
}