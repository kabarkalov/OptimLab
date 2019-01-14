namespace OptimLab
{
    partial class FormProblem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxTargetFunction = new System.Windows.Forms.GroupBox();
            this.textBoxTargetFunction = new System.Windows.Forms.TextBox();
            this.labelTargetFunction = new System.Windows.Forms.Label();
            this.groupBoxConstraints = new System.Windows.Forms.GroupBox();
            this.checkBoxRegenerate = new System.Windows.Forms.CheckBox();
            this.textBoxNumConstraints = new System.Windows.Forms.TextBox();
            this.labelNumConstraints = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxTargetFunction.SuspendLayout();
            this.groupBoxConstraints.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxTargetFunction
            // 
            this.groupBoxTargetFunction.Controls.Add(this.textBoxTargetFunction);
            this.groupBoxTargetFunction.Controls.Add(this.labelTargetFunction);
            this.groupBoxTargetFunction.Location = new System.Drawing.Point(8, 8);
            this.groupBoxTargetFunction.Name = "groupBoxTargetFunction";
            this.groupBoxTargetFunction.Size = new System.Drawing.Size(328, 80);
            this.groupBoxTargetFunction.TabIndex = 0;
            this.groupBoxTargetFunction.TabStop = false;
            this.groupBoxTargetFunction.Text = "Целевая функция";
            // 
            // textBoxTargetFunction
            // 
            this.textBoxTargetFunction.Location = new System.Drawing.Point(200, 32);
            this.textBoxTargetFunction.Name = "textBoxTargetFunction";
            this.textBoxTargetFunction.Size = new System.Drawing.Size(104, 20);
            this.textBoxTargetFunction.TabIndex = 1;
            // 
            // labelTargetFunction
            // 
            this.labelTargetFunction.AutoSize = true;
            this.labelTargetFunction.Location = new System.Drawing.Point(16, 32);
            this.labelTargetFunction.Name = "labelTargetFunction";
            this.labelTargetFunction.Size = new System.Drawing.Size(177, 13);
            this.labelTargetFunction.TabIndex = 0;
            this.labelTargetFunction.Text = "Номер целевой функции (1 - 100):";
            // 
            // groupBoxConstraints
            // 
            this.groupBoxConstraints.Controls.Add(this.checkBoxRegenerate);
            this.groupBoxConstraints.Controls.Add(this.textBoxNumConstraints);
            this.groupBoxConstraints.Controls.Add(this.labelNumConstraints);
            this.groupBoxConstraints.Location = new System.Drawing.Point(8, 96);
            this.groupBoxConstraints.Name = "groupBoxConstraints";
            this.groupBoxConstraints.Size = new System.Drawing.Size(328, 100);
            this.groupBoxConstraints.TabIndex = 1;
            this.groupBoxConstraints.TabStop = false;
            this.groupBoxConstraints.Text = "Ограничения";
            // 
            // checkBoxRegenerate
            // 
            this.checkBoxRegenerate.AutoSize = true;
            this.checkBoxRegenerate.Checked = true;
            this.checkBoxRegenerate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRegenerate.Location = new System.Drawing.Point(16, 64);
            this.checkBoxRegenerate.Name = "checkBoxRegenerate";
            this.checkBoxRegenerate.Size = new System.Drawing.Size(176, 17);
            this.checkBoxRegenerate.TabIndex = 2;
            this.checkBoxRegenerate.Text = "Регенерировать ограничения";
            this.checkBoxRegenerate.UseVisualStyleBackColor = true;
            // 
            // textBoxNumConstraints
            // 
            this.textBoxNumConstraints.Location = new System.Drawing.Point(216, 32);
            this.textBoxNumConstraints.Name = "textBoxNumConstraints";
            this.textBoxNumConstraints.Size = new System.Drawing.Size(88, 20);
            this.textBoxNumConstraints.TabIndex = 1;
            // 
            // labelNumConstraints
            // 
            this.labelNumConstraints.AutoSize = true;
            this.labelNumConstraints.Location = new System.Drawing.Point(16, 32);
            this.labelNumConstraints.Name = "labelNumConstraints";
            this.labelNumConstraints.Size = new System.Drawing.Size(189, 13);
            this.labelNumConstraints.TabIndex = 0;
            this.labelNumConstraints.Text = "Количество ограничений (от 1 до 3):";
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(64, 208);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(200, 208);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // FormProblem
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(344, 249);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxConstraints);
            this.Controls.Add(this.groupBoxTargetFunction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormProblem";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Задача";
            this.groupBoxTargetFunction.ResumeLayout(false);
            this.groupBoxTargetFunction.PerformLayout();
            this.groupBoxConstraints.ResumeLayout(false);
            this.groupBoxConstraints.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxTargetFunction;
        private System.Windows.Forms.TextBox textBoxTargetFunction;
        private System.Windows.Forms.Label labelTargetFunction;
        private System.Windows.Forms.GroupBox groupBoxConstraints;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxNumConstraints;
        private System.Windows.Forms.Label labelNumConstraints;
        private System.Windows.Forms.CheckBox checkBoxRegenerate;
    }
}