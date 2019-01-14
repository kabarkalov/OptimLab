namespace OptimLab
{
    partial class FormOperationProperties
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.dataGridViewLegend = new System.Windows.Forms.DataGridView();
            this.ColumnColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEpsilon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMaxIters = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLegend)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(272, 640);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // dataGridViewLegend
            // 
            this.dataGridViewLegend.AllowUserToAddRows = false;
            this.dataGridViewLegend.AllowUserToDeleteRows = false;
            this.dataGridViewLegend.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewLegend.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLegend.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnColor,
            this.ColumnMethod,
            this.ColumnR,
            this.ColumnEpsilon,
            this.ColumnMaxIters});
            this.dataGridViewLegend.Location = new System.Drawing.Point(8, 464);
            this.dataGridViewLegend.Name = "dataGridViewLegend";
            this.dataGridViewLegend.ReadOnly = true;
            this.dataGridViewLegend.Size = new System.Drawing.Size(600, 168);
            this.dataGridViewLegend.TabIndex = 0;
            // 
            // ColumnColor
            // 
            this.ColumnColor.FillWeight = 30F;
            this.ColumnColor.HeaderText = "Цвет";
            this.ColumnColor.Name = "ColumnColor";
            this.ColumnColor.ReadOnly = true;
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
            // FormOperationProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(619, 669);
            this.Controls.Add(this.dataGridViewLegend);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormOperationProperties";
            this.Text = "Операционные характеристики";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLegend)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.DataGridView dataGridViewLegend;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEpsilon;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMaxIters;
    }
}