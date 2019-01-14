namespace OptimLab
{
    partial class FormExperiments
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
            this.dataGridViewExperiments = new System.Windows.Forms.DataGridView();
            this.ColumnMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEpsilon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMaxIters = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExperiments)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewExperiments
            // 
            this.dataGridViewExperiments.AllowUserToAddRows = false;
            this.dataGridViewExperiments.AllowUserToDeleteRows = false;
            this.dataGridViewExperiments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewExperiments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExperiments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnMethod,
            this.ColumnR,
            this.ColumnEpsilon,
            this.ColumnMaxIters});
            this.dataGridViewExperiments.Location = new System.Drawing.Point(8, 8);
            this.dataGridViewExperiments.Name = "dataGridViewExperiments";
            this.dataGridViewExperiments.ReadOnly = true;
            this.dataGridViewExperiments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewExperiments.Size = new System.Drawing.Size(530, 240);
            this.dataGridViewExperiments.TabIndex = 0;
            // 
            // ColumnMethod
            // 
            this.ColumnMethod.HeaderText = "Метод";
            this.ColumnMethod.Name = "ColumnMethod";
            this.ColumnMethod.ReadOnly = true;
            // 
            // ColumnR
            // 
            this.ColumnR.HeaderText = "Параметр метода";
            this.ColumnR.Name = "ColumnR";
            this.ColumnR.ReadOnly = true;
            // 
            // ColumnEpsilon
            // 
            this.ColumnEpsilon.HeaderText = "Требуемая точность";
            this.ColumnEpsilon.Name = "ColumnEpsilon";
            this.ColumnEpsilon.ReadOnly = true;
            // 
            // ColumnMaxIters
            // 
            this.ColumnMaxIters.HeaderText = "Макс. кол-во итераций";
            this.ColumnMaxIters.Name = "ColumnMaxIters";
            this.ColumnMaxIters.ReadOnly = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(160, 256);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(312, 256);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // FormExperiments
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(544, 289);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.dataGridViewExperiments);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormExperiments";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Эксперименты";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExperiments)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewExperiments;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEpsilon;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMaxIters;
    }
}